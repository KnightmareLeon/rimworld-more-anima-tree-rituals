using System;
using System.Collections.Generic;
using RimWorld;
using UnityEngine;
using Verse;

namespace MoreAnimaTreeRituals.Rituals
{
    public class RitualOutcomeComp_NaturalParticipantCount : RitualOutcomeComp_ParticipantCount
    {
        protected bool NaturalCounts(RitualRoleAssignments assignments, Pawn p)
        {
            return Counts(assignments, p) && MeditationFocusDefOf.Natural.CanPawnUse(p);
        }

        public override float Count(LordJob_Ritual ritual, RitualOutcomeComp_Data data)
        {
            int num = 0;
            RitualOutcomeComp_DataThingPresence obj = (RitualOutcomeComp_DataThingPresence)data;
            float num2 = (ritual.DurationTicks != 0) ? ritual.DurationTicks : ritual.TicksPassedWithProgress;
            foreach (KeyValuePair<Thing, float> presentForTick in obj.presentForTicks)
            {
                Pawn p = (Pawn)presentForTick.Key;
                if (NaturalCounts(ritual.assignments, p) && presentForTick.Value >= num2 / 2f)
                {
                    num++;
                }
            }
            return (curve != null) ? ((int)Math.Min(num, curve.Points[curve.PointsCount - 1].x)) : num;
        }

        public override QualityFactor GetQualityFactor(Precept_Ritual ritual, TargetInfo ritualTarget, RitualObligation obligation, RitualRoleAssignments assignments, RitualOutcomeComp_Data data)
		{
			int num = assignments.Participants.Count(p => NaturalCounts(assignments, p));
			float quality = curve.Evaluate(num);
			return new QualityFactor
			{
				label = label.CapitalizeFirst(),
				count = num + " / " + Mathf.Max(MaxValue, num),
				qualityChange = ExpectedOffsetDesc(positive: true, quality),
				quality = quality,
				positive = true,
				priority = 4f
			};
		}
    }
}