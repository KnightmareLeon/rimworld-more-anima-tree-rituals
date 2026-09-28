using System.Linq;
using RimWorld;
using UnityEngine;
using Verse;

namespace MoreAnimaTreeRituals.Rituals
{
	public class RitualBehaviorWorker_AnimaTreeNaturalBlessingBestowal : RitualBehaviorWorker
	{
		public RitualBehaviorWorker_AnimaTreeNaturalBlessingBestowal()
		{
		}

		public RitualBehaviorWorker_AnimaTreeNaturalBlessingBestowal(RitualBehaviorDef def)
			: base(def)
		{
		}

		public override string GetExplanation(Precept_Ritual ritual, RitualRoleAssignments assignments, float quality)
		{
			TaggedString taggedString = "";
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
