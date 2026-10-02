using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;

namespace MoreAnimaTreeRituals.Rituals
{
	public class RitualObligationTargetWorker_AnimaTreeSootheConjuration : RitualObligationTargetFilter
	{
        private const int AnimaGrassCountForSootheRitual = 15;
		public RitualObligationTargetWorker_AnimaTreeSootheConjuration()
		{
		}

		public RitualObligationTargetWorker_AnimaTreeSootheConjuration(RitualObligationTargetFilterDef def)
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
				if (compPsylinkable.Props.requiredFocus.CanPawnUse(item))
				{
					flag = true;
				}
			}
			if (compPsylinkable.CompSubplant.SubplantsForReading.Count < AnimaGrassCountForSootheRitual)
			{
				return "RitualTargetAnimaTreeNotEnoughAnimaGrass".Translate(AnimaGrassCountForSootheRitual);
			}
			if (!flag)
			{
				return "RitualTargetAnimaTreeNoPawnsWithNatureFocus".Translate();
			}
			return true;
		}

		public override IEnumerable<string> GetTargetInfos(RitualObligation obligation)
		{
			yield return "RitualTargetAnimaTreeInfo".Translate();
		}
	}
}
