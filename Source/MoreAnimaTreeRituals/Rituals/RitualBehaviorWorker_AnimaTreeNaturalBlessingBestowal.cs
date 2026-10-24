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
			int spectatorCount = assignments.SpectatorsForReading.Count ;
			int naturalSpectators = 0;
			foreach(Pawn p in assignments.SpectatorsForReading) if (MeditationFocusDefOf.Natural.CanPawnUse(p)) naturalSpectators++;
			TaggedString taggedString = "MATR.AnimaTreeNaturalBlessingBestowalnExplanationBase".Translate(spectatorCount, naturalSpectators);
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
