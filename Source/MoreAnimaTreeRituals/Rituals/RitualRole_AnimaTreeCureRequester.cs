using MoreAnimaTreeRituals.Defs;
using RimWorld;
using Verse;

namespace MoreAnimaTreeRituals.Rituals
{
	public class RitualRole_AnimaTreeCureRequester : RitualRole_AnimaTreeOrganizerBase
	{
		public override bool AppliesToPawn(Pawn p, out string reason, TargetInfo selectedTarget, LordJob_Ritual ritual = null, RitualRoleAssignments assignments = null, Precept_Ritual precept = null, bool skipReason = false)
		{
			bool baseRes = base.AppliesToPawn(p, out reason, selectedTarget, ritual: ritual, assignments: assignments, skipReason: skipReason);

			if(!baseRes) return baseRes;

			if (!HealthUtility.TryGetWorstHealthCondition(p, out Hediff hediff, out BodyPartRecord part))
			{
				if (!skipReason) reason = "MATR.RitualRoleNoWorstHealthCondition".Translate();
				return false;
			}

			return true;
		}

	}
}
