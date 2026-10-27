using System.Collections.Generic;
using System.Linq;
using Verse;
using RimWorld;

namespace MoreAnimaTreeRituals.Rituals
{
	public class RitualObligationTargetWorker_AnimaTreeCuringRitual : RitualObligationTargetFilter
	{
		private const int CuringRitualGrassCount = 25;
		public RitualObligationTargetWorker_AnimaTreeCuringRitual()
		{
		}

		public RitualObligationTargetWorker_AnimaTreeCuringRitual(RitualObligationTargetFilterDef def)
			: base(def)
		{
		}

		public override IEnumerable<TargetInfo> GetTargets(RitualObligation obligation, Map map)
		{
			return Enumerable.Empty<TargetInfo>();
		}

		protected override RitualTargetUseReport CanUseTargetInternal(TargetInfo target, RitualObligation obligation)
		{
			CompPsylinkable compPsylinkable = target.Thing.TryGetComp<CompPsylinkable>();
			if (compPsylinkable == null)
			{
				return false;
			}
			bool flag = false;
			foreach (Pawn item in target.Map.mapPawns.FreeColonistsSpawned)
			{
				if (compPsylinkable.Props.requiredFocus.CanPawnUse(item) && HealthUtility.TryGetWorstHealthCondition(item, out Hediff hediff, out BodyPartRecord part))
				{
					flag = true;
				}
			}
			if (compPsylinkable.CompSubplant.SubplantsForReading.Count < CuringRitualGrassCount)
			{
				return "RitualTargetAnimaTreeNotEnoughAnimaGrass".Translate(CuringRitualGrassCount);
			}
			if (!flag)
			{
				return "MATR.RitualTargetAnimaTreeAllHaveNoWorstHealthCondition".Translate();
			}
			return true;
		}

		public override IEnumerable<string> GetTargetInfos(RitualObligation obligation)
		{	

			yield return "RitualTargetAnimaTreeInfo".Translate();
		}
	}
}
