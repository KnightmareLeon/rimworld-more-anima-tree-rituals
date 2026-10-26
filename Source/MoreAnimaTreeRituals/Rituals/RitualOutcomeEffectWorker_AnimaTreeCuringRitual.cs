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
			LetterDef letterDef; string text = "";
            if(outcome.Positive)
            {
				letterDef = LetterDefOf.RitualOutcomePositive;
            } 
			else
			{
				letterDef = LetterDefOf.RitualOutcomeNegative;
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