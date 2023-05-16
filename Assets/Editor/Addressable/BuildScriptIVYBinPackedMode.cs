using UnityEditor.AddressableAssets.Settings;
using UnityEditor.AddressableAssets.Settings.GroupSchemas;
using UnityEngine;
using UnityEngine.Build.Pipeline;
using UnityEditor.AddressableAssets.Build.DataBuilders;

namespace Ivy.Addressable.Editor
{
    /// <summary>
    /// 通过此打包模式，将AppendHash模式的bundle包的后缀格式改成.bin
    /// </summary>
    [CreateAssetMenu(fileName = "BuildScriptIVYBinPackedMode.asset", menuName = "Addressables/Content Builders/IVY Build Script (.bin)")]
    public class BuildScriptIVYBinPackedMode : BuildScriptPackedMode
    {
        /// <inheritdoc />
        public override string Name
        {
            get
            {
                return "IVY Build Script (.bin)";
            }
        }

        /// <summary>
        /// 会将AppendHash模式的bundle包的后缀格式改成.bin
        /// </summary>
        protected override string ConstructAssetBundleName(AddressableAssetGroup assetGroup, BundledAssetGroupSchema schema, BundleDetails info, string assetBundleName)
        {
            string bundleNameWithHashing = base.ConstructAssetBundleName(assetGroup, schema, info, assetBundleName);
            bundleNameWithHashing = bundleNameWithHashing.Replace(".bundle", ".bin");
            return bundleNameWithHashing;
        }
    }
}
