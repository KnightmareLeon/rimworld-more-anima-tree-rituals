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
			Pawn pawn = jobRitual.PawnWithRole("organizer");
            RitualOutcomePossibility outcome = GetOutcome(quality, jobRitual);
			LetterDef letterDef; string text = "";
            if(outcome.Positive)
            {
                pawn.health.AddHediff(MATR_HediffDefOf.MATR_AnimaTreeNaturalBlessing);
				letterDef = LetterDefOf.RitualOutcomePositive;
				text += "MATR.LetterTextAnimaTreeNaturalBlessingBestowalPositive".Translate(pawn.Named("PAWN"));
				FleckEffects.GreenGlowEffect(pawn);
				FleckEffects.SpawnLeaves(pawn);
            } 
			else
			{
				letterDef = LetterDefOf.RitualOutcomeNegative;
				text += "MATR.LetterTextAnimaTreeNaturalBlessingBestowalNegative".Translate(pawn.Named("PAWN"));
            } 
			text += "\n\n" + OutcomeQualityBreakdownDesc(quality, progress, jobRitual);
			Find.LetterStack.ReceiveLetter("MATR.LetterLabelAnimaTreeNaturalBlessingBestowalCompleted".Translate(), text, letterDef, new LookTargets(pawn, jobRitual.selectedTarget.Thing));
		}
	}
}