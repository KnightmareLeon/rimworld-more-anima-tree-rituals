using System.Collections.Generic;
using System.Linq;
using Verse;
using RimWorld;
using MoreAnimaTreeRituals.Defs;

namespace MoreAnimaTreeRituals.Rituals
{
	public class RitualObligationTargetWorker_AnimaTree_Alt : RitualObligationTargetFilter
	{
		public RitualObligationTargetWorker_AnimaTree_Alt()
		{
		}

		public RitualObligationTargetWorker_AnimaTree_Alt(RitualObligationTargetFilterDef def)
			: base(def)
		{
		}

		public override IEnumerable<TargetInfo> GetTargets(RitualObligation obligation, Map map)
		{
			return Enumerable.Empty<TargetInfo>();
		}

		protected override RitualTargetUseReport CanUseTargetInternal(TargetInfo target, RitualObligation obligation)
		{
			return target.Thing.def == MATR_ThingDefOf.Plant_TreeAnima;
		}

		public override IEnumerable<string> GetTargetInfos(RitualObligation obligation)
		{	

			yield return "RitualTargetAnimaTreeInfo".Translate();
		}
	}
}
