using System.Collections.Generic;
using RimWorld;
using UnityEngine;
using Verse;
using Verse.AI;
using Verse.AI.Group;
using Verse.Sound;

namespace MoreAnimaTreeRituals.Rituals
{
	public class JobDriver_ConjureAnimaSoothe : JobDriver
	{
		public const int ConjuringTimeTicks = 15000;

		public const int EffectsTickInterval = 720;

		protected const TargetIndex AnimaSootherInd = TargetIndex.A;

		protected const TargetIndex ConjuringRequestSpotInd = TargetIndex.B;
        private Thing AnimaTreeThing => TargetA.Thing;
        private CompPsylinkable Psylinkable => AnimaTreeThing.TryGetComp<CompPsylinkable>();
		private LocalTargetInfo RequestSpot => job.targetB;

		public override bool TryMakePreToilReservations(bool errorOnFailed)
		{
			if (pawn.Reserve(AnimaTreeThing, job, 1, -1, null, errorOnFailed))
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
			Toil toil = Toils_General.Wait(ConjuringTimeTicks);
			toil.tickIntervalAction = delegate(int delta)
			{
				pawn.rotationTracker.FaceTarget(AnimaTreeThing);
				if (pawn.IsHashIntervalTick(EffectsTickInterval, delta))
				{
					Vector3 vector = pawn.TrueCenter();
					vector += (AnimaTreeThing.TrueCenter() - vector) * Rand.Value;
					FleckMaker.Static(vector, pawn.Map, FleckDefOf.PsycastAreaEffect, 0.5f);
					FleckMaker.Static(vector, pawn.Map, FleckDefOf.PsycastAreaEffect, 0.2f);
					FleckMaker.Static(vector, pawn.Map, FleckDefOf.PsycastAreaEffect, 0.3f);
					Psylinkable.Props.linkSound.PlayOneShot(SoundInfo.InMap(new TargetInfo(AnimaTreeThing)));
				}
			};
			toil.handlingFacing = false;
			toil.socialMode = RandomSocialMode.Off;
			yield return toil;
		}
	}
}
