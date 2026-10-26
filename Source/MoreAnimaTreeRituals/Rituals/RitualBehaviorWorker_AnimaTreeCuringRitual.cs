using System.Linq;
using RimWorld;
#if RIMWORLD_17
using Waylith.Math;
#else
using UnityEngine;
#endif
using Verse;

namespace MoreAnimaTreeRituals.Rituals
{
	public class RitualBehaviorWorker_AnimaTreeCuringRitual : RitualBehaviorWorker
	{
		public RitualBehaviorWorker_AnimaTreeCuringRitual()
		{
		}

		public RitualBehaviorWorker_AnimaTreeCuringRitual(RitualBehaviorDef def)
			: base(def)
		{
		}

		public override string GetExplanation(Precept_Ritual ritual, RitualRoleAssignments assignments, float quality)
		{
			int prtpCount = assignments.SpectatorsForReading.Count;
			float psyStat = assignments.FirstAssignedPawn(assignments.GetRole("organizer")).psychicEntropy.PsychicSensitivity;
			float num = RitualOutcomeEffectWorker_AnimaTreeSootheConjuration.RestoredGrassFromQuality.Evaluate(quality);
			TaggedString taggedString = "MATR.AnimaTreeCuringRitualExplanationBase".Translate(prtpCount, psyStat, num);
			if (assignments.ExtraRequiredPawnsForReading.Any())
			{
				TaggedString psylinkAffectedByTraitsNegativelyWarning = RoyalTitleUtility.GetPsylinkAffectedByTraitsNegativelyWarning(assignments.ExtraRequiredPawnsForReading.FirstOrDefault());
				if (psylinkAffectedByTraitsNegativelyWarning.RawText != null)
				{
					taggedString += "\n\n" + psylinkAffectedByTraitsNegativelyWarning.Resolve();
				}
			}
			return taggedString;
		}

		public override string ExpectedDuration(Precept_Ritual ritual, RitualRoleAssignments assignments, float quality)
		{
			int count = assignments.SpectatorsForReading.Count;
			return Mathf.RoundToInt(ritual.behavior.def.durationTicks.max / RitualStage_AnimaNaturalBlessingBestowal.ProgressPerParticipantCurve.Evaluate(count + 1)).ToStringTicksToPeriod(allowSeconds: false);
		}
	}
}
