using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace I2.Loc
{
    public static partial class LocalizationManager
    {
        public static void AddGlobalLanguageSourceAsset(LanguageSourceAsset sourceAsset)
        {
            if (sourceAsset == null || sourceAsset.mSource == null)
                return;

            if (!sourceAsset.mSource.mIsGlobalSource)
                sourceAsset.mSource.mIsGlobalSource = true;
            sourceAsset.mSource.owner = sourceAsset;
            AddSource(sourceAsset.mSource);
        }
    }
}
