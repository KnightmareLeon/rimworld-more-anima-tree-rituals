using System.Collections.Generic;
using MoreAnimaTreeRituals.Defs;
using MoreAnimaTreeRituals.Effects;
using RimWorld;
using UnityEngine;
using Verse;

namespace MoreAnimaTreeRituals.Rituals
{
	public class RitualOutcomeEffectWorker_AnimaTreeNaturalBlessingBestowal : RitualOutcomeEffectWorker_FromQuality
	{

		public override bool SupportsAttachableOutcomeEffect => false;

		public RitualOutcomeEffectWorker_AnimaTreeNaturalBlessingBestowal()
		{
		}

		public RitualOutcomeEffectWorker_AnimaTreeNaturalBlessingBestowal(RitualOutcomeEffectDef def)
			: base(def)
		{
		}

		public override void Apply(float progress, Dictionary<Pawn, int> totalPresence, LordJob_Ritual jobRitual)
		{
			float quality = GetQuality(jobRitual, progress);
			Pawn organizer = jobRitual.PawnWithRole("organizer");
            RitualOutcomePossibility outcome = GetOutcome(quality, jobRitual);
			TaggedString outcomeText = "\n\n" + OutcomeQualityBreakdownDesc(quality, progress, jobRitual);
			LetterDef letterDef; string text;
            if(outcome.Positive)
            {
                organizer.health.AddHediff(MATR_HediffDefOf.MATR_AnimaTreeNaturalBlessing);
				organizer.needs.mood.thoughts.memories.TryGainMemory(MATR_ThoughtDefOf.MATR_ReceivedNaturalBlessing);
				letterDef = LetterDefOf.RitualOutcomePositive;
				text = "MATR.LetterTextAnimaTreeNaturalBlessingBestowalPositive".Translate(organizer.Named("PAWN"));
				FleckEffects.GreenGlowEffect(organizer);
				FleckEffects.SpawnLeaves(organizer);
				if(outcome != def.BestOutcome)
				{
					organizer.health.AddHediff(HediffDefOf.PsychicShock);
					text = "MATR.LetterTextAnimaTreeNaturalBlessingBestowalNotBest".Translate(organizer.Named("PAWN"));
				}
            } 
			else
			{
				organizer.needs.mood.thoughts.memories.TryGainMemory(MATR_ThoughtDefOf.MATR_RejectedNaturalBlessing);
				letterDef = LetterDefOf.RitualOutcomeNegative;
				text = "MATR.LetterTextAnimaTreeNaturalBlessingBestowalNegative".Translate(organizer.Named("PAWN"));
				if(outcome == def.WorstOutcome)
				{
					FleckEffects.GreenGlowEffect(organizer);
					organizer.health.AddHediff(HediffDefOf.Abasia);
					organizer.needs.mood.thoughts.memories.TryGainMemory(MATR_ThoughtDefOf.MATR_PunishedByAnimaTree);
					text = "MATR.LetterTextAnimaTreeNaturalBlessingBestowalWorst".Translate(organizer.Named("PAWN"));
				}
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