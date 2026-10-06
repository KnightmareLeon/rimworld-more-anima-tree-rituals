using MoreAnimaTreeRituals.Defs;
using RimWorld;
using Verse;

namespace MoreAnimaTreeRituals.Rituals
{
	public class RitualRole_AnimaTreeSootheConjurer : RitualRole_AnimaTreeOrganizerBase
	{
		public override bool AppliesToPawn(Pawn p, out string reason, TargetInfo selectedTarget, LordJob_Ritual ritual = null, RitualRoleAssignments assignments = null, Precept_Ritual precept = null, bool skipReason = false)
		{
			bool baseRes = base.AppliesToPawn(p, out reason, selectedTarget, ritual: ritual, assignments: assignments, skipReason: skipReason);

			if(!baseRes) return baseRes;

            if (!MeditationFocusDefOf.Natural.CanPawnUse(p))
			{
				if (!skipReason) reason = "RitualTargetAnimaTreeMustBeCapableOfNature".Translate();
				return false;
			}
			return true;
		}
	}
}