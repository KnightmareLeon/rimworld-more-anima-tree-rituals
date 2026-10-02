using System.Collections.Generic;
using System.Linq;
using MoreAnimaTreeRituals.Defs;
using MoreAnimaTreeRituals.Effects;
using RimWorld;
using UnityEngine;
using Verse;
using Verse.Sound;

namespace MoreAnimaTreeRituals.Rituals
{
	public class RitualOutcomeEffectWorker_AnimaTreeSootheConjuration : RitualOutcomeEffectWorker_FromQuality
	{
		public static readonly SimpleCurve RestoredGrassFromQuality = new SimpleCurve
		{
			new CurvePoint(0.2f, 0f),
			new CurvePoint(0.4f, 1f),
			new CurvePoint(0.6f, 2f),
			new CurvePoint(0.8f, 3f),
			new CurvePoint(1f, 4f)
		};

		public override bool SupportsAttachableOutcomeEffect => false;

		public RitualOutcomeEffectWorker_AnimaTreeSootheConjuration()
		{
		}

		public RitualOutcomeEffectWorker_AnimaTreeSootheConjuration(RitualOutcomeEffectDef def)
			: base(def)
		{
		}

		public override void Apply(float progress, Dictionary<Pawn, int> totalPresence, LordJob_Ritual jobRitual)
        {
			float quality = GetQuality(jobRitual, progress);
			Pawn organizer = jobRitual.PawnWithRole("organizer");
            RitualOutcomePossibility outcome = GetOutcome(quality, jobRitual);
			string outcomeText = "\n\n" + OutcomeQualityBreakdownDesc(quality, progress, jobRitual);
			LetterDef letterDef; string text;
			CompPsylinkable obj = jobRitual.selectedTarget.Thing?.TryGetComp<CompPsylinkable>();
			if(outcome.Positive)
			{
				Find.CameraDriver.shaker.DoShake(1f);
				FleckMaker.Static(obj.parent.Position, organizer.Map, FleckDefOf.PsycastAreaEffect, 10f);
				SoundDefOf.PsycastPsychicPulse.PlayOneShot(new TargetInfo(obj.parent));
				foreach(Pawn pawn in organizer.MapHeld.mapPawns.FreeColonistsSpawned)
				{
					pawn.needs.mood.thoughts.memories.TryGainMemory(
						MeditationFocusDefOf.Natural.CanPawnUse(pawn) ?
						MATR_ThoughtDefOf.MATR_AnimaTreeSootheMoodBoostNatural :
						MATR_ThoughtDefOf.MATR_AnimaTreeSootheMoodBoostBase
					);
				}
				if(outcome == def.BestOutcome)
				{
					int num = 15 - (int)RestoredGrassFromQuality.Evaluate(quality);
					List<Thing> list = obj.CompSubplant.SubplantsForReading.OrderByDescending((Thing p) => p.Position.DistanceTo(obj.parent.Position)).ToList();
					for (int num2 = 0; num2 < num && num2 < list.Count; num2++)
					{
						list[num2].Destroy();
					}
					obj.CompSubplant.Cleanup();
				}
			}
			else
			{
				
			}


        }
	}
}