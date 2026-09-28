using RimWorld;
using Verse;

namespace MoreAnimaTreeRituals.Defs
{
    [DefOf]
    public static class MATR_ThingDefOf
    {
        public static ThingDef Plant_TreeAnima;

        static MATR_ThingDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(MATR_ThingDefOf));
        }
    }
}