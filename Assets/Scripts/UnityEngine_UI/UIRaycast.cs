// ILSpyBased#2
namespace UnityEngine.UI
{
    [AddComponentMenu("UI/UIRaycast")]
    public class UIRaycast : MaskableGraphic
    {
        protected UIRaycast()
        {
            base.useLegacyMeshGeneration = false;
        }

        protected override void OnPopulateMesh(VertexHelper vh)
        {
            vh.Clear();
        }
    }
}


