using System.Linq;
using RimWorld;
using UnityEngine;
using Verse;

namespace MoreAnimaTreeRituals.Rituals
{
	public class RitualBehaviorWorker_AnimaTreePsywielderBlessingBestowal : RitualBehaviorWorker
	{
		public RitualBehaviorWorker_AnimaTreePsywielderBlessingBestowal()
		{
		}

		public RitualBehaviorWorker_AnimaTreePsywielderBlessingBestowal(RitualBehaviorDef def)
			: base(def)
		{
		}

		public override string GetExplanation(Precept_Ritual ritual, RitualRoleAssignments assignments, float quality)
		{
			int count = assignments.SpectatorsForReading.Count;
			float num = RitualOutcomeEffectWorker_AnimaTreeSootheConjuration.RestoredGrassFromQuality.Evaluate(quality);
			TaggedString taggedString = "MATR.AnimaTreePsywielderBlessingBestowalExplanationBase".Translate(count, num);
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
			return Mathf.RoundToInt(ritual.behavior.def.durationTicks.max / RitualStage_AnimaTreeLinking.ProgressPerParticipantCurve.Evaluate(count + 1)).ToStringTicksToPeriod(allowSeconds: false);
		}
	}
}
