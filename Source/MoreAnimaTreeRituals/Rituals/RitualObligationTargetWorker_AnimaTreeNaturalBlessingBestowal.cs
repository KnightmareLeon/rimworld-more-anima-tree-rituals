using System.Collections.Generic;
using System.Linq;
using Verse;
using RimWorld;
using MoreAnimaTreeRituals.Defs;

namespace MoreAnimaTreeRituals.Rituals
{
	public class RitualObligationTargetWorker_AnimaTreeNaturalBlessingBestowal : RitualObligationTargetFilter
	{
		public RitualObligationTargetWorker_AnimaTreeNaturalBlessingBestowal()
		{
		}

		public RitualObligationTargetWorker_AnimaTreeNaturalBlessingBestowal(RitualObligationTargetFilterDef def)
			: base(def)
		{
		}

		public override IEnumerable<TargetInfo> GetTargets(RitualObligation obligation, Map map)
		{
			return Enumerable.Empty<TargetInfo>();
		}

		protected override RitualTargetUseReport CanUseTargetInternal(TargetInfo target, RitualObligation obligation)
		{
			if(target.Thing.def != MATR_ThingDefOf.Plant_TreeAnima) return false;
			int pTotal = target.Map.mapPawns.FreeColonistsSpawned.Count;
			int pTotalCannotUse = 0;
			foreach(Pawn p in target.Map.mapPawns.FreeColonistsSpawned)
			{
				if(p.health.hediffSet.HasHediff(MATR_HediffDefOf.MATR_AnimaTreeNaturalBlessing))
				{
					pTotalCannotUse++; continue;
				}
				if(!p.ageTracker.Adult)
				{
					pTotalCannotUse++; continue;
				}
				if(!p.psychicEntropy.IsPsychicallySensitive)
				{
					pTotalCannotUse++; continue;
				}
			}
			if(pTotal == pTotalCannotUse) return "MATR.RitualTargetAnimaTreeCannotBeUsedByColonistsForNaturalBlessingBestowal".Translate();
			return true; 
		}

		public override IEnumerable<string> GetTargetInfos(RitualObligation obligation)
		{	

			yield return "RitualTargetAnimaTreeInfo".Translate();
		}
	}
}
