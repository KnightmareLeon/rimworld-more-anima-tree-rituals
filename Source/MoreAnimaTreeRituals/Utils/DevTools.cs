using LudeonTK;
using MoreAnimaTreeRituals.Defs;
using RimWorld;
using Verse;

namespace MoreAnimaTreeRituals.Utils
{
    public static class DevTools
    {
        [DebugAction(
            "More Anima Tree Rituals",
            "Force Anima Grass",
            actionType = DebugActionType.Action
        )]
        public static void ForceAnimaGrass()
        {
            foreach (Map map in Find.Maps)
            {
                foreach (Thing thing in map.listerThings.ThingsOfDef(MATR_ThingDefOf.Plant_TreeAnima))
                {
                    CompSpawnSubplant comp = thing.TryGetComp<CompSpawnSubplant>();

                    if (comp != null)
                    {
                        comp.AddProgress(1f, ignoreMultiplier: true);
                        return;
                    }
                }
            }

            Messages.Message(
                "No anima tree found.",
                MessageTypeDefOf.RejectInput,
                historical: false
            );
        }
    }
}