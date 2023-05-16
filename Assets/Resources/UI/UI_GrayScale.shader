Shader "_old_UI/GrayScale"
{
  Properties
  {
      _MainTex ("Sprite Texture",2D) = "white"{}
      _Color ("Tint",Color) = (1,1,1,1)
      _AdjustTex ("Adjust Curve Tex",2D) = "white"{}
      _AdjustPercentage ("Adjust Percentage",Range(0,1)) = 1
      __RampOffset ("Ramp Offset",Range(0,1)) = 0.5
  }
  SubShader
  {
      Tags
      { 
        "CanUseSpriteAtlas" = "true"
        "IGNOREPROJECTOR" = "true"
        "PreviewType" = "Plane"
        "QUEUE" = "Transparent"
        "RenderType" = "Transparent"
      }
      LOD 0
      Pass // ind: 1, name: DEFAULT
      {
        Name "DEFAULT"
        Tags
        { 
        "CanUseSpriteAtlas" = "true"
        "IGNOREPROJECTOR" = "true"
        "PreviewType" = "Plane"
        "QUEUE" = "Transparent"
        "RenderType" = "Transparent"
        }
        LOD 0
        // m_ProgramMask = 6
        CGPROGRAM
#pragma target 3.0
#pragma vertex vertexShader
#pragma fragment fragmentShader
#pragma multi_compile UNITY_UI_ALPHACLIP

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
