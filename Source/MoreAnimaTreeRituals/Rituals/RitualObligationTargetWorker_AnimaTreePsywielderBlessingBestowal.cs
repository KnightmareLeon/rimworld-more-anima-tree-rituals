using System.Collections.Generic;
using System.Linq;
using MoreAnimaTreeRituals.Defs;
using RimWorld;
using Verse;

namespace MoreAnimaTreeRituals.Rituals
{
	public class RitualObligationTargetWorker_AnimaTreePsywielderBlessingBestowal : RitualObligationTargetFilter
	{
        private const int PsywielderBlessingGrassCount = 20;
		public RitualObligationTargetWorker_AnimaTreePsywielderBlessingBestowal()
		{
		}

		public RitualObligationTargetWorker_AnimaTreePsywielderBlessingBestowal(RitualObligationTargetFilterDef def)
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
            bool flag2 = false;
			foreach (Pawn item in target.Map.mapPawns.FreeColonistsSpawned)
			{
				if (compPsylinkable.Props.requiredFocus.CanPawnUse(item))
				{
					flag = true;
				}
                if (!item.health.hediffSet.HasHediff(MATR_HediffDefOf.MATR_AnimaTreePsywielderBlessing))
                {
                    flag2 = true;
                }
			}
			if (compPsylinkable.CompSubplant.SubplantsForReading.Count < PsywielderBlessingGrassCount)
			{
				return "RitualTargetAnimaTreeNotEnoughAnimaGrass".Translate(PsywielderBlessingGrassCount);
			}
			if (!flag)
			{
				return "RitualTargetAnimaTreeNoPawnsWithNatureFocus".Translate();
			}
            if (!flag2)
            {
                return "MATR.RitualTargetAnimaTreeAllHavePsywielderBlessing".Translate();
            }
			return true;
		}

		public override IEnumerable<string> GetTargetInfos(RitualObligation obligation)
		{
			yield return "RitualTargetAnimaTreeInfo".Translate();
		}
	}
}
