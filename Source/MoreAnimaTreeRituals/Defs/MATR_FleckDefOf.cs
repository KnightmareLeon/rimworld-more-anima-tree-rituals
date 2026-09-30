using RimWorld;
using Verse;

namespace MoreAnimaTreeRituals.Defs
{
    [DefOf]
    public static class MATR_FleckDefOf
    {
        public static FleckDef MATR_GreenGlow;
        public static FleckDef MATR_NaturalBlessingLeaves;

        static MATR_FleckDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(MATR_FleckDefOf));
        }
    }
}