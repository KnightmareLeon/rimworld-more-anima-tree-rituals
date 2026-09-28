using System.Collections.Generic;
using MoreAnimaTreeRituals.Defs;
using RimWorld;
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
			Pawn pawn = jobRitual.PawnWithRole("organizer");
            RitualOutcomePossibility outcome = GetOutcome(quality, jobRitual);
            if(outcome.Positive)
            {
                pawn.health.AddHediff(MATR_HediffDefOf.MATR_AnimaTreeNaturalBlessing);
            }
                
			//string text = "LetterTextLinkingRitualCompleted".Translate(pawn.Named("PAWN"), jobRitual.selectedTarget.Thing.Named("LINKABLE"));
            string text = "";
			text = text + "\n\n" + OutcomeQualityBreakdownDesc(quality, progress, jobRitual);
			//Find.LetterStack.ReceiveLetter("LetterLabelLinkingRitualCompleted".Translate(), text, LetterDefOf.RitualOutcomePositive, new LookTargets(pawn, jobRitual.selectedTarget.Thing));
		}
	}
}