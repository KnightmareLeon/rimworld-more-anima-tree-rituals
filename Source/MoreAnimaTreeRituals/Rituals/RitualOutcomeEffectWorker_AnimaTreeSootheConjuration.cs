using System.Collections.Generic;
using System.Linq;
using MoreAnimaTreeRituals.Defs;
using MoreAnimaTreeRituals.Effects;
using RimWorld;
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
			string text = "MATR.AnimaSootheConjurationCompletionBase".Translate();
			CompPsylinkable obj = jobRitual.selectedTarget.Thing?.TryGetComp<CompPsylinkable>();
			
			AnimaSoothe(outcome, organizer, obj);	
			
			int grassDestroyed = 15;
			if(outcome == def.BestOutcome)
			{
				grassDestroyed -= (int)RestoredGrassFromQuality.Evaluate(quality);

				text += "\n\n" + "MATR.AnimaSootheConjurationCompletionMasterful".Translate();
			}

			if(!outcome.Positive) text += "\n\n" + "MATR.AnimaSootheConjurationCompletionNegative".Translate();
			
			if(outcome == def.WorstOutcome)
			{
				FleckEffects.GreenGlowEffect(organizer);
				organizer.health.AddHediff(HediffDefOf.PsychicShock);
				text += " " + "MATR.AnimaSootheConjurationCompletionWorst".Translate();
			}

			List<Thing> list = obj.CompSubplant.SubplantsForReading.OrderByDescending((Thing p) => p.Position.DistanceTo(obj.parent.Position)).ToList();
			for (int num = 0; num < grassDestroyed && num < list.Count; num++)
			{
				list[num].Destroy();
			}
			obj.CompSubplant.Cleanup();
			text += outcomeText;
			Find.LetterStack.ReceiveLetter("MATR.LetterLabelAnimaSootheConjurationCompleted".Translate(outcome.Label), text, LetterDefOf.RitualOutcomePositive, new LookTargets(organizer, jobRitual.selectedTarget.Thing));
		}

		private void AnimaSoothe(RitualOutcomePossibility outcome, Pawn organizer, CompPsylinkable obj)
		{
			ThoughtDef naturalThought = outcome.Positive ? MATR_ThoughtDefOf.MATR_AnimaTreeSootheMoodBoostNatural : MATR_ThoughtDefOf.MATR_LesserAnimaTreeSootheMoodBoostNatural;
			ThoughtDef baseThought = outcome.Positive ? MATR_ThoughtDefOf.MATR_AnimaTreeSootheMoodBoostBase : MATR_ThoughtDefOf.MATR_LesserAnimaTreeSootheMoodBoost;
			Find.CameraDriver.shaker.DoShake(1f);
			FleckMaker.Static(obj.parent.Position, organizer.Map, FleckDefOf.PsycastAreaEffect, 10f);
			SoundDefOf.PsycastPsychicPulse.PlayOneShot(new TargetInfo(obj.parent));
			foreach(Pawn pawn in organizer.MapHeld.mapPawns.FreeColonistsSpawned)
			{
				pawn.needs.mood.thoughts.memories.TryGainMemory(
					MeditationFocusDefOf.Natural.CanPawnUse(pawn) ?naturalThought :baseThought
				);
			}
        }
	}
}