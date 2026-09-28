using MoreAnimaTreeRituals.Defs;
using Verse;
using Verse.AI;

namespace MoreAnimaTreeRituals.Rituals
{
	public class JobGiver_RequestBlessing : ThinkNode_JobGiver
	{
		protected override Job TryGiveJob(Pawn pawn)
		{
			PawnDuty duty = pawn.mindState.duty;
			if (duty == null)
			{
				return null;
			}
			if (!pawn.CanReserveAndReach(duty.focus, PathEndMode.OnCell, Danger.Deadly))
			{
				return null;
			}
			return JobMaker.MakeJob(MATR_JobDefOf.MATR_RequestingBlessing, duty.focusSecond, duty.focus);
		}
	}
}
