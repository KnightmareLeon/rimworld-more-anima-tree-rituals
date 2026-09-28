using RimWorld;
using Verse;

namespace MoreAnimaTreeRituals.Defs
{
    [DefOf]
    public static class MATR_JobDefOf
    {
        public static JobDef MATR_RequestingBlessing;

        static MATR_JobDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(MATR_JobDefOf));
        }
    }
}