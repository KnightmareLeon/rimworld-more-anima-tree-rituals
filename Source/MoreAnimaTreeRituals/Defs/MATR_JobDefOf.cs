using RimWorld;
using Verse;

namespace MoreAnimaTreeRituals.Defs
{
    [DefOf]
    public static class MATR_JobDefOf
    {
        public static JobDef MATR_RequestingBoon;
        public static JobDef MATR_ConjuraAnimaSoothe;

        static MATR_JobDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(MATR_JobDefOf));
        }
    }
}