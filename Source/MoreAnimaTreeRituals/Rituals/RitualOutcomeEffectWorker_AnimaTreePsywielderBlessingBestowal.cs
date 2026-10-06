using System.Collections.Generic;
using System.Linq;
using MoreAnimaTreeRituals.Defs;
using MoreAnimaTreeRituals.Effects;
using RimWorld;
using UnityEngine;
using Verse;

namespace MoreAnimaTreeRituals.Rituals
{
	public class RitualOutcomeEffectWorker_AnimaTreePsywielderBlessingBestowal : RitualOutcomeEffectWorker_FromQuality
	{
		public static readonly SimpleCurve RestoredGrassFromQuality = new SimpleCurve
		{
			new CurvePoint(0.2f, 0f),
			new CurvePoint(0.4f, 1f),
			new CurvePoint(0.6f, 3f),
			new CurvePoint(0.8f, 5f),
			new CurvePoint(1f, 8f)
		};

		public override bool SupportsAttachableOutcomeEffect => false;

		public RitualOutcomeEffectWorker_AnimaTreePsywielderBlessingBestowal()
		{
		}

		public RitualOutcomeEffectWorker_AnimaTreePsywielderBlessingBestowal(RitualOutcomeEffectDef def)
			: base(def)
		{
		}

		public override void Apply(float progress, Dictionary<Pawn, int> totalPresence, LordJob_Ritual jobRitual)
		{
			float quality = GetQuality(jobRitual, progress);
			Pawn organizer = jobRitual.PawnWithRole("organizer");
			TaggedString outcomeText = "\n\n" + OutcomeQualityBreakdownDesc(quality, progress, jobRitual);

            CompPsylinkable obj = jobRitual.selectedTarget.Thing?.TryGetComp<CompPsylinkable>();
            int grassRestored = (int)RestoredGrassFromQuality.Evaluate(quality);
            int grassDestroyed = 20 - grassRestored;

            List<Thing> list = obj.CompSubplant.SubplantsForReading.OrderByDescending(p => p.Position.DistanceTo(obj.parent.Position)).ToList();
			for (int num = 0; num < grassDestroyed && num < list.Count; num++)
			{
				list[num].Destroy();
			}
			obj.CompSubplant.Cleanup();

            string text = "MATR.LetterTextAnimaPsywielderBlessingBestowalCompleted".Translate(organizer.Named("PAWN"));
            if (grassRestored > 0)
			{
				text += " " + "MATR.LetterTextRitualCompletedAnimaGrass".Translate(grassRestored);
			}
            text += outcomeText;
			Find.LetterStack.ReceiveLetter("MATR.LetterLabelAnimaPsywielderBlessingBestowalCompleted".Translate(), text, LetterDefOf.RitualOutcomePositive, new LookTargets(organizer, jobRitual.selectedTarget.Thing));
		}
	}
}