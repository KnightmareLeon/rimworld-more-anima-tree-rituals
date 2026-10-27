using RimWorld;

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
        public static ThoughtDef MATR_ReceivedAnimaCure;
        public static ThoughtDef MATR_ReceivedCompleteAnimaCure;
        public static ThoughtDef MATR_ReceivedAnimaCuringSeed;
        public static ThoughtDef MATR_FailedAnimaCuring;

        static MATR_ThoughtDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(MATR_ThoughtDefOf));
        }
    }
}