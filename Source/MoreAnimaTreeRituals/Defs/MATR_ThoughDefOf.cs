using RimWorld;
using Verse;

namespace MoreAnimaTreeRituals.Defs
{
    [DefOf]
    public static class MATR_ThoughtDefOf
    {
        public static ThoughtDef MATR_ReceivedNaturalBlessing;
        public static ThoughtDef MATR_RejectedNaturalBlessing;
        public static ThoughtDef MATR_PunishedByAnimaTree;
        public static ThoughtDef MATR_AnimaTreeSootheMoodBoostBase;
        public static ThoughtDef MATR_LesserAnimaTreeSootheMoodBoost;
        public static ThoughtDef MATR_LesserAnimaTreeSootheMoodBoostNatural;
        public static ThoughtDef MATR_AnimaTreeSootheMoodBoostNatural;

        static MATR_ThoughtDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(MATR_ThoughtDefOf));
        }
    }
}