using System.Collections.Generic;
using RimWorld;
using UnityEngine;
using Verse;
using Verse.AI;
using Verse.AI.Group;
using Verse.Sound;

namespace MoreAnimaTreeRituals.Rituals
{
	public class JobDriver_RequestingBlessing : JobDriver
	{
		public const int RequestingTimeTicks = 15000;

		public const int EffectsTickInterval = 720;

		protected const TargetIndex BlessingGrantorInd = TargetIndex.A;

		protected const TargetIndex BlessingRequestSpotInd = TargetIndex.B;
        private Thing BlessingGrantorThing => TargetA.Thing;
        private CompPsylinkable Psylinkable => BlessingGrantorThing.TryGetComp<CompPsylinkable>();
		private LocalTargetInfo RequestSpot => job.targetB;

		public override bool TryMakePreToilReservations(bool errorOnFailed)
		{
			if (pawn.Reserve(BlessingGrantorThing, job, 1, -1, null, errorOnFailed))
			{
				return pawn.Reserve(RequestSpot, job, 1, -1, null, errorOnFailed);
			}
			return false;
		}

		protected override IEnumerable<Toil> MakeNewToils()
		{
			if (!ModLister.CheckRoyalty("Psylinkable"))
			{
				yield break;
			}
			yield return Toils_Goto.GotoCell(TargetIndex.B, PathEndMode.OnCell);
			Toil toil = Toils_General.Wait(RequestingTimeTicks);
			toil.tickIntervalAction = delegate(int delta)
			{
				pawn.rotationTracker.FaceTarget(BlessingGrantorThing);
				if (pawn.IsHashIntervalTick(720, delta))
				{
					Vector3 vector = pawn.TrueCenter();
					vector += (BlessingGrantorThing.TrueCenter() - vector) * Rand.Value;
					FleckMaker.Static(vector, pawn.Map, FleckDefOf.PsycastAreaEffect, 0.5f);
					Psylinkable.Props.linkSound.PlayOneShot(SoundInfo.InMap(new TargetInfo(BlessingGrantorThing)));
				}
			};
			toil.handlingFacing = false;
			toil.socialMode = RandomSocialMode.Off;
			yield return toil;
		}
	}
}
