Shader "_old_MaskedTexture"
{
  Properties
  {
      _MainTex ("Base (RGB)",2D) = "white"{}
      _MaskTex ("Culling Mask",2D) = "white"{}
      _Cutoff ("Alpha cutoff",Range(0,1)) = 0.1
  }
  SubShader
  {
      Tags
      { 
        "QUEUE" = "Transparent"
      }
      LOD 0
      Pass // ind: 1, name: 
      {
        Tags
        { 
        "QUEUE" = "Transparent"
        }
        LOD 0
        // m_ProgramMask = 6
        CGPROGRAM
#pragma target 3.0
#pragma vertex vertexShader
#pragma fragment fragmentShader

        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }

        fixed4 fragmentShader() : SV_Target
        {
            return fixed4(1,1,1,1); // IS STUB!!!!
        }

        ENDCG
      } // end phase
  }
  FallBack ""
}
