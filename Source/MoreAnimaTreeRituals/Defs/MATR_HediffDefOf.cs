using RimWorld;
using Verse;

namespace MoreAnimaTreeRituals.Defs
{
    [DefOf]
    public static class MATR_HediffDefOf
    {
        public static HediffDef MATR_AnimaTreeNaturalBlessing;

        static MATR_HediffDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(MATR_HediffDefOf));
        }
    }
}