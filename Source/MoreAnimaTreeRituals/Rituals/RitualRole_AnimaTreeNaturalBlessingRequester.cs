using MoreAnimaTreeRituals.Defs;
using RimWorld;
using Verse;

namespace MoreAnimaTreeRituals.Rituals
{
	public class RitualRole_AnimaTreeNaturalBlessingRequester : RitualRole_AnimaTreeOrganizerBase
	{
		public override bool AppliesToPawn(Pawn p, out string reason, TargetInfo selectedTarget, LordJob_Ritual ritual = null, RitualRoleAssignments assignments = null, Precept_Ritual precept = null, bool skipReason = false)
		{
			bool baseRes = base.AppliesToPawn(p, out reason, selectedTarget, ritual: ritual, assignments: assignments, skipReason: skipReason);

			if(!baseRes) return baseRes;

			if (p.health.hediffSet.HasHediff(MATR_HediffDefOf.MATR_AnimaTreeNaturalBlessing))
			{
				if (!skipReason) reason = "MATR.RitualRoleAlreadyNaturalBlessed".Translate();
				return false;
			}

			return true;
		}

	}
}
