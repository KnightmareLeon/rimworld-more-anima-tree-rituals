using System.Collections.Generic;
using MoreAnimaTreeRituals.Defs;
using MoreAnimaTreeRituals.Effects;
using RimWorld;
using Verse;

namespace MoreAnimaTreeRituals.Rituals
{
	public class RitualOutcomeEffectWorker_AnimaTreeCuringRitual : RitualOutcomeEffectWorker_FromQuality
	{

		public override bool SupportsAttachableOutcomeEffect => false;

		public RitualOutcomeEffectWorker_AnimaTreeCuringRitual()
		{
		}

		public RitualOutcomeEffectWorker_AnimaTreeCuringRitual(RitualOutcomeEffectDef def)
			: base(def)
		{
		}

		public override void Apply(float progress, Dictionary<Pawn, int> totalPresence, LordJob_Ritual jobRitual)
		{
			float quality = GetQuality(jobRitual, progress);
			Pawn organizer = jobRitual.PawnWithRole("organizer");
            RitualOutcomePossibility outcome = GetOutcome(quality, jobRitual);
			TaggedString outcomeText = "\n\n" + OutcomeQualityBreakdownDesc(quality, progress, jobRitual);
			LetterDef letterDef = outcome.Positive ? LetterDefOf.RitualOutcomePositive : LetterDefOf.RitualOutcomeNegative;
			string text;
            if(outcome == def.BestOutcome)
			{
				organizer.needs.mood.thoughts.memories.TryGainMemory(MATR_ThoughtDefOf.MATR_ReceivedCompleteAnimaCure);
				bool hasBadHediff = HealthUtility.TryGetWorstHealthCondition(organizer, out Hediff h, out BodyPartRecord b);
				while(hasBadHediff)
				{
					TaggedString notNull = HealthUtility.FixWorstHealthCondition(organizer);
					hasBadHediff = notNull != null;
				}
				FleckEffects.GreenGlowEffect(organizer);
				text = "MATR.LetterTextAnimaCuringRitualCompletionComplete".Translate();
			}
			else if (outcome.Positive)
			{
				organizer.needs.mood.thoughts.memories.TryGainMemory(MATR_ThoughtDefOf.MATR_ReceivedAnimaCure);
				HealthUtility.FixWorstHealthCondition(organizer);
				FleckEffects.GreenGlowEffect(organizer);
				text = "MATR.LetterTextAnimaCuringRitualCompletionSuccessful".Translate();
			}
			else if (outcome == def.WorstOutcome)
			{
				organizer.needs.mood.thoughts.memories.TryGainMemory(MATR_ThoughtDefOf.MATR_ReceivedAnimaCuringSeed);
				text = "MATR.LetterTextAnimaCuringRitualCompletionFailed".Translate();
			}
			else
			{
				organizer.needs.mood.thoughts.memories.TryGainMemory(MATR_ThoughtDefOf.MATR_FailedAnimaCuring);
				FleckEffects.GreenGlowEffect(organizer);
				organizer.health.AddHediff(MATR_HediffDefOf.MATR_AnimaCuringSeed);
				text = "MATR.LetterTextAnimaCuringRitualCompletionFlawed".Translate();
			}

			foreach (KeyValuePair<Pawn, int> item in totalPresence)
			{
				Pawn pawn = item.Key;
				if (pawn != organizer) pawn.needs.mood.thoughts.memories.TryGainMemory(outcome.memory);
			}

			text += outcomeText;
			Find.LetterStack.ReceiveLetter("MATR.LetterLabelAnimaTreeNaturalBlessingBestowalCompleted".Translate(outcome.Label), text, letterDef, new LookTargets(organizer, jobRitual.selectedTarget.Thing));
		}
	}
}