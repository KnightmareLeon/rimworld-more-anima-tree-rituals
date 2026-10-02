using MoreAnimaTreeRituals.Defs;
using RimWorld;
using Verse;
using Verse.AI;

namespace MoreAnimaTreeRituals.Rituals
{
	public class JobGiver_ConjureAnimaSoothe : ThinkNode_JobGiver
	{
		protected override Job TryGiveJob(Pawn pawn)
		{
			PawnDuty duty = pawn.mindState.duty;
			if (duty == null) return null;
			if (!pawn.CanReserveAndReach(duty.focus, PathEndMode.OnCell, Danger.Deadly)) return null;
			if (pawn.Dead || pawn.Faction != Faction.OfPlayer) return null;
			if (!pawn.psychicEntropy.IsPsychicallySensitive) return null;
			if (!pawn.Map.reservationManager.CanReserve(pawn, duty.focusSecond.Thing)) return null;
			CompPsylinkable compPsylinkable = duty.focusSecond.Thing?.TryGetComp<CompPsylinkable>();
			if (!compPsylinkable.TryFindLinkSpot(pawn, out LocalTargetInfo spot)) return null;
			if (compPsylinkable.CompSubplant.SubplantsForReading.Count < 15) return null;
            if (!MeditationFocusDefOf.Natural.CanPawnUse(pawn)) return null;
            return JobMaker.MakeJob(MATR_JobDefOf.MATR_ConjuraAnimaSoothe, duty.focusSecond, duty.focus);
		}
	}
}
