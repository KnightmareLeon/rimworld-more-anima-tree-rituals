using JetBrains.Annotations;
using MoreAnimaTreeRituals.Defs;
using RimWorld;
using UnityEngine;
using Verse;

namespace MoreAnimaTreeRituals.Effects
{
    public static class FleckEffects
    {
        public static void SpawnLeaves(Thing t)
		{
			Map map = t.Map;
			
			for (int i = 0; i < 20; i++)
			{
				Vector3 position = t.DrawPos;
				position.x += Rand.Range(-0.2f, 0.2f);
				position.z += Rand.Range(-0.2f, 0.2f);
				FleckCreationData data = FleckMaker.GetDataStatic(position, map, MATR_FleckDefOf.MATR_NaturalBlessingLeaves);
				data.velocityAngle = Rand.Range(0f, 360f);
				data.velocitySpeed = Rand.Range(0.5f, 1.2f);
				data.rotationRate = Rand.Range(-120f, 120f);
				data.airTimeLeft = Rand.Range(0.5f, 1.2f);
				data.scale = Rand.Range(0.6f, 1.0f);
				map.flecks.CreateFleck(data);
			}

		}

        public static void GreenGlowEffect(Thing t)
        {
            FleckMaker.AttachedOverlay(t, MATR_FleckDefOf.MATR_GreenGlow, Vector3.zero, scale: 1.5f);
        }
    }
}