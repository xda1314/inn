Shader "_old_Standard"
{
  Properties
  {
      _Color ("Color",Color) = (1,1,1,1)
      _MainTex ("Albedo",2D) = "white"{}
      _Cutoff ("Alpha Cutoff",Range(0,1)) = 0.5
      _Glossiness ("Smoothness",Range(0,1)) = 0.5
      _GlossMapScale ("Smoothness Scale",Range(0,1)) = 1
      _Metallic ("Metallic",Range(0,1)) = 0
      _MetallicGlossMap ("Metallic",2D) = "white"{}
      _BumpMap ("Normal Map",2D) = "bump"{}
      _Parallax ("Height Scale",Range(0.005,0.08)) = 0.02
      _ParallaxMap ("Height Map",2D) = "black"{}
      _OcclusionStrength ("Strength",Range(0,1)) = 1
      _OcclusionMap ("Occlusion",2D) = "white"{}
      _EmissionColor ("Color",Color) = (0,0,0,1)
      _EmissionMap ("Emission",2D) = "white"{}
      _DetailMask ("Detail Mask",2D) = "white"{}
      _DetailAlbedoMap ("Detail Albedo x2",2D) = "grey"{}
      _DetailNormalMap ("Normal Map",2D) = "bump"{}
  }
  SubShader
  {
      Tags
      { 
        "PerformanceChecks" = "False"
        "RenderType" = "Opaque"
      }
      LOD 300
      Pass // ind: 1, name: FORWARD
      {
        Name "FORWARD"
        Tags
        { 
        "LIGHTMODE" = "FORWARDBASE"
        "PerformanceChecks" = "False"
        "RenderType" = "Opaque"
        "SHADOWSUPPORT" = "true"
        }
        LOD 300
        // m_ProgramMask = 6
        CGPROGRAM
#pragma target 3.0
#pragma vertex vertexShader
#pragma fragment fragmentShader
#pragma multi_compile DIRECTIONAL SHADOWS_SCREEN VERTEXLIGHT_ON

        // ======================
        // SerializedProgram: progVertex

        // ++++++++++++++++
        // Sub Program: 1 for m_GpuProgramType: #5
        // m_BlobIndex: 0
        // m_ShaderHardwareTier: 0

#ifdef DIRECTIONAL

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // DIRECTIONAL
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 2 for m_GpuProgramType: #5
        // m_BlobIndex: 1
        // m_ShaderHardwareTier: 1

#ifdef DIRECTIONAL

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // DIRECTIONAL
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 4 for m_GpuProgramType: #4
        // m_BlobIndex: 0
        // m_ShaderHardwareTier: 0

#ifdef DIRECTIONAL

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // DIRECTIONAL
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 5 for m_GpuProgramType: #4
        // m_BlobIndex: 1
        // m_ShaderHardwareTier: 1

#ifdef DIRECTIONAL

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // DIRECTIONAL
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 7 for m_GpuProgramType: #25
        // m_BlobIndex: 0
        // m_ShaderHardwareTier: 0

#ifdef DIRECTIONAL

        // m_TextureParams
        sampler2D _MainTex; // index: 134217728, m_SamplerIndex: 0
        sampler2D _OcclusionMap; // index: 134217729, m_SamplerIndex: 1
        sampler2D unity_NHxRoughness; // index: 134217730, m_SamplerIndex: 2
        sampler4D unity_SpecCube0; // index: 134217731, m_SamplerIndex: 3

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // DIRECTIONAL
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 8 for m_GpuProgramType: #25
        // m_BlobIndex: 1
        // m_ShaderHardwareTier: 1

#ifdef DIRECTIONAL

        // m_TextureParams
        sampler2D _MainTex; // index: 134217728, m_SamplerIndex: 0
        sampler2D _OcclusionMap; // index: 134217729, m_SamplerIndex: 1
        sampler4D unity_SpecCube0; // index: 134217730, m_SamplerIndex: 2

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // DIRECTIONAL
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 10 for m_GpuProgramType: #5
        // m_BlobIndex: 2
        // m_ShaderHardwareTier: 0

#ifdef DIRECTIONAL
#ifdef SHADOWS_SCREEN

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // SHADOWS_SCREEN
#endif // DIRECTIONAL
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 11 for m_GpuProgramType: #5
        // m_BlobIndex: 3
        // m_ShaderHardwareTier: 1

#ifdef DIRECTIONAL
#ifdef SHADOWS_SCREEN

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // SHADOWS_SCREEN
#endif // DIRECTIONAL
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 13 for m_GpuProgramType: #4
        // m_BlobIndex: 2
        // m_ShaderHardwareTier: 0

#ifdef DIRECTIONAL
#ifdef SHADOWS_SCREEN

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // SHADOWS_SCREEN
#endif // DIRECTIONAL
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 14 for m_GpuProgramType: #4
        // m_BlobIndex: 3
        // m_ShaderHardwareTier: 1

#ifdef DIRECTIONAL
#ifdef SHADOWS_SCREEN

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // SHADOWS_SCREEN
#endif // DIRECTIONAL
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 16 for m_GpuProgramType: #25
        // m_BlobIndex: 2
        // m_ShaderHardwareTier: 0

#ifdef DIRECTIONAL
#ifdef SHADOWS_SCREEN

        // m_TextureParams
        sampler2D _MainTex; // index: 134217728, m_SamplerIndex: 0
        sampler2D _OcclusionMap; // index: 134217729, m_SamplerIndex: 1
        sampler2D _ShadowMapTexture; // index: 134217732, m_SamplerIndex: 4
        sampler2D unity_NHxRoughness; // index: 134217730, m_SamplerIndex: 2
        sampler4D unity_SpecCube0; // index: 134217731, m_SamplerIndex: 3

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // SHADOWS_SCREEN
#endif // DIRECTIONAL
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 17 for m_GpuProgramType: #25
        // m_BlobIndex: 3
        // m_ShaderHardwareTier: 1

#ifdef DIRECTIONAL
#ifdef SHADOWS_SCREEN

        // m_TextureParams
        sampler2D _MainTex; // index: 134217728, m_SamplerIndex: 0
        sampler2D _OcclusionMap; // index: 134217729, m_SamplerIndex: 1
        sampler2D _ShadowMapTexture; // index: 134217731, m_SamplerIndex: 3
        sampler4D unity_SpecCube0; // index: 134217730, m_SamplerIndex: 2

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // SHADOWS_SCREEN
#endif // DIRECTIONAL
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 19 for m_GpuProgramType: #5
        // m_BlobIndex: 4
        // m_ShaderHardwareTier: 0

#ifdef DIRECTIONAL
#ifdef VERTEXLIGHT_ON

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // VERTEXLIGHT_ON
#endif // DIRECTIONAL
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 20 for m_GpuProgramType: #5
        // m_BlobIndex: 5
        // m_ShaderHardwareTier: 1

#ifdef DIRECTIONAL
#ifdef VERTEXLIGHT_ON

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // VERTEXLIGHT_ON
#endif // DIRECTIONAL
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 22 for m_GpuProgramType: #4
        // m_BlobIndex: 4
        // m_ShaderHardwareTier: 0

#ifdef DIRECTIONAL
#ifdef VERTEXLIGHT_ON

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // VERTEXLIGHT_ON
#endif // DIRECTIONAL
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 23 for m_GpuProgramType: #4
        // m_BlobIndex: 5
        // m_ShaderHardwareTier: 1

#ifdef DIRECTIONAL
#ifdef VERTEXLIGHT_ON

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // VERTEXLIGHT_ON
#endif // DIRECTIONAL
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 25 for m_GpuProgramType: #25
        // m_BlobIndex: 4
        // m_ShaderHardwareTier: 0

#ifdef DIRECTIONAL
#ifdef VERTEXLIGHT_ON

        // m_TextureParams
        sampler2D _MainTex; // index: 134217728, m_SamplerIndex: 0
        sampler2D _OcclusionMap; // index: 134217729, m_SamplerIndex: 1
        sampler2D unity_NHxRoughness; // index: 134217730, m_SamplerIndex: 2
        sampler4D unity_SpecCube0; // index: 134217731, m_SamplerIndex: 3

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // VERTEXLIGHT_ON
#endif // DIRECTIONAL
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 26 for m_GpuProgramType: #25
        // m_BlobIndex: 5
        // m_ShaderHardwareTier: 1

#ifdef DIRECTIONAL
#ifdef VERTEXLIGHT_ON

        // m_TextureParams
        sampler2D _MainTex; // index: 134217728, m_SamplerIndex: 0
        sampler2D _OcclusionMap; // index: 134217729, m_SamplerIndex: 1
        sampler4D unity_SpecCube0; // index: 134217730, m_SamplerIndex: 2

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // VERTEXLIGHT_ON
#endif // DIRECTIONAL
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 28 for m_GpuProgramType: #5
        // m_BlobIndex: 6
        // m_ShaderHardwareTier: 0

#ifdef DIRECTIONAL
#ifdef SHADOWS_SCREEN
#ifdef VERTEXLIGHT_ON

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // VERTEXLIGHT_ON
#endif // SHADOWS_SCREEN
#endif // DIRECTIONAL
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 29 for m_GpuProgramType: #5
        // m_BlobIndex: 7
        // m_ShaderHardwareTier: 1

#ifdef DIRECTIONAL
#ifdef SHADOWS_SCREEN
#ifdef VERTEXLIGHT_ON

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // VERTEXLIGHT_ON
#endif // SHADOWS_SCREEN
#endif // DIRECTIONAL
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 31 for m_GpuProgramType: #4
        // m_BlobIndex: 6
        // m_ShaderHardwareTier: 0

#ifdef DIRECTIONAL
#ifdef SHADOWS_SCREEN
#ifdef VERTEXLIGHT_ON

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // VERTEXLIGHT_ON
#endif // SHADOWS_SCREEN
#endif // DIRECTIONAL
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 32 for m_GpuProgramType: #4
        // m_BlobIndex: 7
        // m_ShaderHardwareTier: 1

#ifdef DIRECTIONAL
#ifdef SHADOWS_SCREEN
#ifdef VERTEXLIGHT_ON

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // VERTEXLIGHT_ON
#endif // SHADOWS_SCREEN
#endif // DIRECTIONAL
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 34 for m_GpuProgramType: #25
        // m_BlobIndex: 6
        // m_ShaderHardwareTier: 0

#ifdef DIRECTIONAL
#ifdef SHADOWS_SCREEN
#ifdef VERTEXLIGHT_ON

        // m_TextureParams
        sampler2D _MainTex; // index: 134217728, m_SamplerIndex: 0
        sampler2D _OcclusionMap; // index: 134217729, m_SamplerIndex: 1
        sampler2D _ShadowMapTexture; // index: 134217732, m_SamplerIndex: 4
        sampler2D unity_NHxRoughness; // index: 134217730, m_SamplerIndex: 2
        sampler4D unity_SpecCube0; // index: 134217731, m_SamplerIndex: 3

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // VERTEXLIGHT_ON
#endif // SHADOWS_SCREEN
#endif // DIRECTIONAL
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 35 for m_GpuProgramType: #25
        // m_BlobIndex: 7
        // m_ShaderHardwareTier: 1

#ifdef DIRECTIONAL
#ifdef SHADOWS_SCREEN
#ifdef VERTEXLIGHT_ON

        // m_TextureParams
        sampler2D _MainTex; // index: 134217728, m_SamplerIndex: 0
        sampler2D _OcclusionMap; // index: 134217729, m_SamplerIndex: 1
        sampler2D _ShadowMapTexture; // index: 134217731, m_SamplerIndex: 3
        sampler4D unity_SpecCube0; // index: 134217730, m_SamplerIndex: 2

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // VERTEXLIGHT_ON
#endif // SHADOWS_SCREEN
#endif // DIRECTIONAL
        //
        // ++++++++++++++++

        //
        // ======================


        // ======================
        // SerializedProgram: progFragment

        // ++++++++++++++++
        // Sub Program: 1 for m_GpuProgramType: #5
        // m_BlobIndex: 8
        // m_ShaderHardwareTier: 0

#ifdef DIRECTIONAL

        //  m_ShaderRequirements: 
        fixed4 fragmentShader() : SV_Target
        {
            return fixed4(1,1,1,1); // IS STUB!!!!
        }
#endif // DIRECTIONAL
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 10 for m_GpuProgramType: #5
        // m_BlobIndex: 9
        // m_ShaderHardwareTier: 0

#ifdef DIRECTIONAL
#ifdef SHADOWS_SCREEN

        //  m_ShaderRequirements: 
        fixed4 fragmentShader() : SV_Target
        {
            return fixed4(1,1,1,1); // IS STUB!!!!
        }
#endif // SHADOWS_SCREEN
#endif // DIRECTIONAL
        //
        // ++++++++++++++++

        //
        // ======================


        ENDCG
      } // end phase
      Pass // ind: 2, name: FORWARD_DELTA
      {
        Name "FORWARD_DELTA"
        Tags
        { 
        "LIGHTMODE" = "FORWARDADD"
        "PerformanceChecks" = "False"
        "RenderType" = "Opaque"
        "SHADOWSUPPORT" = "true"
        }
        LOD 300
        // m_ProgramMask = 6
        CGPROGRAM
#pragma target 3.0
#pragma vertex vertexShader
#pragma fragment fragmentShader
#pragma multi_compile POINT DIRECTIONAL SPOT POINT_COOKIE DIRECTIONAL_COOKIE SHADOWS_DEPTH SHADOWS_SOFT SHADOWS_SCREEN SHADOWS_CUBE

        // ======================
        // SerializedProgram: progVertex

        // ++++++++++++++++
        // Sub Program: 1 for m_GpuProgramType: #5
        // m_BlobIndex: 10
        // m_ShaderHardwareTier: 0

#ifdef POINT

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // POINT
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 2 for m_GpuProgramType: #5
        // m_BlobIndex: 11
        // m_ShaderHardwareTier: 1

#ifdef POINT

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // POINT
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 4 for m_GpuProgramType: #4
        // m_BlobIndex: 10
        // m_ShaderHardwareTier: 0

#ifdef POINT

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // POINT
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 5 for m_GpuProgramType: #4
        // m_BlobIndex: 11
        // m_ShaderHardwareTier: 1

#ifdef POINT

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // POINT
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 7 for m_GpuProgramType: #25
        // m_BlobIndex: 10
        // m_ShaderHardwareTier: 0

#ifdef POINT

        // m_TextureParams
        sampler2D _LightTexture0; // index: 134217730, m_SamplerIndex: 2
        sampler2D _MainTex; // index: 134217728, m_SamplerIndex: 0
        sampler2D unity_NHxRoughness; // index: 134217729, m_SamplerIndex: 1

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // POINT
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 8 for m_GpuProgramType: #25
        // m_BlobIndex: 11
        // m_ShaderHardwareTier: 1

#ifdef POINT

        // m_TextureParams
        sampler2D _LightTexture0; // index: 134217729, m_SamplerIndex: 1
        sampler2D _MainTex; // index: 134217728, m_SamplerIndex: 0

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // POINT
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 10 for m_GpuProgramType: #5
        // m_BlobIndex: 12
        // m_ShaderHardwareTier: 0

#ifdef DIRECTIONAL

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // DIRECTIONAL
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 11 for m_GpuProgramType: #5
        // m_BlobIndex: 13
        // m_ShaderHardwareTier: 1

#ifdef DIRECTIONAL

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // DIRECTIONAL
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 13 for m_GpuProgramType: #4
        // m_BlobIndex: 12
        // m_ShaderHardwareTier: 0

#ifdef DIRECTIONAL

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // DIRECTIONAL
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 14 for m_GpuProgramType: #4
        // m_BlobIndex: 13
        // m_ShaderHardwareTier: 1

#ifdef DIRECTIONAL

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // DIRECTIONAL
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 16 for m_GpuProgramType: #25
        // m_BlobIndex: 12
        // m_ShaderHardwareTier: 0

#ifdef DIRECTIONAL

        // m_TextureParams
        sampler2D _MainTex; // index: 134217728, m_SamplerIndex: 0
        sampler2D unity_NHxRoughness; // index: 134217729, m_SamplerIndex: 1

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // DIRECTIONAL
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 17 for m_GpuProgramType: #25
        // m_BlobIndex: 13
        // m_ShaderHardwareTier: 1

#ifdef DIRECTIONAL

        // m_TextureParams
        sampler2D _MainTex; // index: 134217728, m_SamplerIndex: 0

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // DIRECTIONAL
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 19 for m_GpuProgramType: #5
        // m_BlobIndex: 14
        // m_ShaderHardwareTier: 0

#ifdef SPOT

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // SPOT
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 20 for m_GpuProgramType: #5
        // m_BlobIndex: 15
        // m_ShaderHardwareTier: 1

#ifdef SPOT

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // SPOT
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 22 for m_GpuProgramType: #4
        // m_BlobIndex: 14
        // m_ShaderHardwareTier: 0

#ifdef SPOT

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // SPOT
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 23 for m_GpuProgramType: #4
        // m_BlobIndex: 15
        // m_ShaderHardwareTier: 1

#ifdef SPOT

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // SPOT
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 25 for m_GpuProgramType: #25
        // m_BlobIndex: 14
        // m_ShaderHardwareTier: 0

#ifdef SPOT

        // m_TextureParams
        sampler2D _LightTexture0; // index: 134217730, m_SamplerIndex: 2
        sampler2D _LightTextureB0; // index: 134217731, m_SamplerIndex: 3
        sampler2D _MainTex; // index: 134217728, m_SamplerIndex: 0
        sampler2D unity_NHxRoughness; // index: 134217729, m_SamplerIndex: 1

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // SPOT
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 26 for m_GpuProgramType: #25
        // m_BlobIndex: 15
        // m_ShaderHardwareTier: 1

#ifdef SPOT

        // m_TextureParams
        sampler2D _LightTexture0; // index: 134217729, m_SamplerIndex: 1
        sampler2D _LightTextureB0; // index: 134217730, m_SamplerIndex: 2
        sampler2D _MainTex; // index: 134217728, m_SamplerIndex: 0

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // SPOT
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 28 for m_GpuProgramType: #5
        // m_BlobIndex: 16
        // m_ShaderHardwareTier: 0

#ifdef POINT_COOKIE

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // POINT_COOKIE
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 29 for m_GpuProgramType: #5
        // m_BlobIndex: 17
        // m_ShaderHardwareTier: 1

#ifdef POINT_COOKIE

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // POINT_COOKIE
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 31 for m_GpuProgramType: #4
        // m_BlobIndex: 16
        // m_ShaderHardwareTier: 0

#ifdef POINT_COOKIE

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // POINT_COOKIE
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 32 for m_GpuProgramType: #4
        // m_BlobIndex: 17
        // m_ShaderHardwareTier: 1

#ifdef POINT_COOKIE

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // POINT_COOKIE
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 34 for m_GpuProgramType: #25
        // m_BlobIndex: 16
        // m_ShaderHardwareTier: 0

#ifdef POINT_COOKIE

        // m_TextureParams
        sampler4D _LightTexture0; // index: 134217731, m_SamplerIndex: 3
        sampler2D _LightTextureB0; // index: 134217730, m_SamplerIndex: 2
        sampler2D _MainTex; // index: 134217728, m_SamplerIndex: 0
        sampler2D unity_NHxRoughness; // index: 134217729, m_SamplerIndex: 1

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // POINT_COOKIE
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 35 for m_GpuProgramType: #25
        // m_BlobIndex: 17
        // m_ShaderHardwareTier: 1

#ifdef POINT_COOKIE

        // m_TextureParams
        sampler4D _LightTexture0; // index: 134217730, m_SamplerIndex: 2
        sampler2D _LightTextureB0; // index: 134217729, m_SamplerIndex: 1
        sampler2D _MainTex; // index: 134217728, m_SamplerIndex: 0

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // POINT_COOKIE
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 37 for m_GpuProgramType: #5
        // m_BlobIndex: 18
        // m_ShaderHardwareTier: 0

#ifdef DIRECTIONAL_COOKIE

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // DIRECTIONAL_COOKIE
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 38 for m_GpuProgramType: #5
        // m_BlobIndex: 19
        // m_ShaderHardwareTier: 1

#ifdef DIRECTIONAL_COOKIE

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // DIRECTIONAL_COOKIE
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 40 for m_GpuProgramType: #4
        // m_BlobIndex: 18
        // m_ShaderHardwareTier: 0

#ifdef DIRECTIONAL_COOKIE

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // DIRECTIONAL_COOKIE
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 41 for m_GpuProgramType: #4
        // m_BlobIndex: 19
        // m_ShaderHardwareTier: 1

#ifdef DIRECTIONAL_COOKIE

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // DIRECTIONAL_COOKIE
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 43 for m_GpuProgramType: #25
        // m_BlobIndex: 18
        // m_ShaderHardwareTier: 0

#ifdef DIRECTIONAL_COOKIE

        // m_TextureParams
        sampler2D _LightTexture0; // index: 134217730, m_SamplerIndex: 2
        sampler2D _MainTex; // index: 134217728, m_SamplerIndex: 0
        sampler2D unity_NHxRoughness; // index: 134217729, m_SamplerIndex: 1

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // DIRECTIONAL_COOKIE
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 44 for m_GpuProgramType: #25
        // m_BlobIndex: 19
        // m_ShaderHardwareTier: 1

#ifdef DIRECTIONAL_COOKIE

        // m_TextureParams
        sampler2D _LightTexture0; // index: 134217729, m_SamplerIndex: 1
        sampler2D _MainTex; // index: 134217728, m_SamplerIndex: 0

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // DIRECTIONAL_COOKIE
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 46 for m_GpuProgramType: #5
        // m_BlobIndex: 20
        // m_ShaderHardwareTier: 0

#ifdef SPOT
#ifdef SHADOWS_DEPTH

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // SHADOWS_DEPTH
#endif // SPOT
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 47 for m_GpuProgramType: #5
        // m_BlobIndex: 21
        // m_ShaderHardwareTier: 1

#ifdef SPOT
#ifdef SHADOWS_DEPTH

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // SHADOWS_DEPTH
#endif // SPOT
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 49 for m_GpuProgramType: #4
        // m_BlobIndex: 20
        // m_ShaderHardwareTier: 0

#ifdef SPOT
#ifdef SHADOWS_DEPTH

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // SHADOWS_DEPTH
#endif // SPOT
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 50 for m_GpuProgramType: #4
        // m_BlobIndex: 21
        // m_ShaderHardwareTier: 1

#ifdef SPOT
#ifdef SHADOWS_DEPTH

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // SHADOWS_DEPTH
#endif // SPOT
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 52 for m_GpuProgramType: #25
        // m_BlobIndex: 20
        // m_ShaderHardwareTier: 0

#ifdef SPOT
#ifdef SHADOWS_DEPTH

        // m_TextureParams
        sampler2D _LightTexture0; // index: 134217730, m_SamplerIndex: 2
        sampler2D _LightTextureB0; // index: 134217731, m_SamplerIndex: 3
        sampler2D _MainTex; // index: 134217728, m_SamplerIndex: 0
        sampler2D _ShadowMapTexture; // index: 134217732, m_SamplerIndex: 4
        sampler2D unity_NHxRoughness; // index: 134217729, m_SamplerIndex: 1

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // SHADOWS_DEPTH
#endif // SPOT
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 53 for m_GpuProgramType: #25
        // m_BlobIndex: 21
        // m_ShaderHardwareTier: 1

#ifdef SPOT
#ifdef SHADOWS_DEPTH

        // m_TextureParams
        sampler2D _LightTexture0; // index: 134217729, m_SamplerIndex: 1
        sampler2D _LightTextureB0; // index: 134217730, m_SamplerIndex: 2
        sampler2D _MainTex; // index: 134217728, m_SamplerIndex: 0
        sampler2D _ShadowMapTexture; // index: 134217731, m_SamplerIndex: 3

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // SHADOWS_DEPTH
#endif // SPOT
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 55 for m_GpuProgramType: #5
        // m_BlobIndex: 22
        // m_ShaderHardwareTier: 0

#ifdef SPOT
#ifdef SHADOWS_DEPTH
#ifdef SHADOWS_SOFT

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // SHADOWS_SOFT
#endif // SHADOWS_DEPTH
#endif // SPOT
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 56 for m_GpuProgramType: #5
        // m_BlobIndex: 23
        // m_ShaderHardwareTier: 1

#ifdef SPOT
#ifdef SHADOWS_DEPTH
#ifdef SHADOWS_SOFT

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // SHADOWS_SOFT
#endif // SHADOWS_DEPTH
#endif // SPOT
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 58 for m_GpuProgramType: #4
        // m_BlobIndex: 22
        // m_ShaderHardwareTier: 0

#ifdef SPOT
#ifdef SHADOWS_DEPTH
#ifdef SHADOWS_SOFT

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // SHADOWS_SOFT
#endif // SHADOWS_DEPTH
#endif // SPOT
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 59 for m_GpuProgramType: #4
        // m_BlobIndex: 23
        // m_ShaderHardwareTier: 1

#ifdef SPOT
#ifdef SHADOWS_DEPTH
#ifdef SHADOWS_SOFT

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // SHADOWS_SOFT
#endif // SHADOWS_DEPTH
#endif // SPOT
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 61 for m_GpuProgramType: #25
        // m_BlobIndex: 22
        // m_ShaderHardwareTier: 0

#ifdef SPOT
#ifdef SHADOWS_DEPTH
#ifdef SHADOWS_SOFT

        // m_TextureParams
        sampler2D _LightTexture0; // index: 134217730, m_SamplerIndex: 2
        sampler2D _LightTextureB0; // index: 134217731, m_SamplerIndex: 3
        sampler2D _MainTex; // index: 134217728, m_SamplerIndex: 0
        sampler2D _ShadowMapTexture; // index: 134217732, m_SamplerIndex: 4
        sampler2D unity_NHxRoughness; // index: 134217729, m_SamplerIndex: 1

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // SHADOWS_SOFT
#endif // SHADOWS_DEPTH
#endif // SPOT
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 62 for m_GpuProgramType: #25
        // m_BlobIndex: 23
        // m_ShaderHardwareTier: 1

#ifdef SPOT
#ifdef SHADOWS_DEPTH
#ifdef SHADOWS_SOFT

        // m_TextureParams
        sampler2D _LightTexture0; // index: 134217729, m_SamplerIndex: 1
        sampler2D _LightTextureB0; // index: 134217730, m_SamplerIndex: 2
        sampler2D _MainTex; // index: 134217728, m_SamplerIndex: 0
        sampler2D _ShadowMapTexture; // index: 134217731, m_SamplerIndex: 3

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // SHADOWS_SOFT
#endif // SHADOWS_DEPTH
#endif // SPOT
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 64 for m_GpuProgramType: #5
        // m_BlobIndex: 24
        // m_ShaderHardwareTier: 0

#ifdef DIRECTIONAL
#ifdef SHADOWS_SCREEN

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // SHADOWS_SCREEN
#endif // DIRECTIONAL
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 65 for m_GpuProgramType: #5
        // m_BlobIndex: 25
        // m_ShaderHardwareTier: 1

#ifdef DIRECTIONAL
#ifdef SHADOWS_SCREEN

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // SHADOWS_SCREEN
#endif // DIRECTIONAL
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 67 for m_GpuProgramType: #4
        // m_BlobIndex: 24
        // m_ShaderHardwareTier: 0

#ifdef DIRECTIONAL
#ifdef SHADOWS_SCREEN

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // SHADOWS_SCREEN
#endif // DIRECTIONAL
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 68 for m_GpuProgramType: #4
        // m_BlobIndex: 25
        // m_ShaderHardwareTier: 1

#ifdef DIRECTIONAL
#ifdef SHADOWS_SCREEN

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // SHADOWS_SCREEN
#endif // DIRECTIONAL
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 70 for m_GpuProgramType: #25
        // m_BlobIndex: 24
        // m_ShaderHardwareTier: 0

#ifdef DIRECTIONAL
#ifdef SHADOWS_SCREEN

        // m_TextureParams
        sampler2D _MainTex; // index: 134217728, m_SamplerIndex: 0
        sampler2D _ShadowMapTexture; // index: 134217730, m_SamplerIndex: 2
        sampler2D unity_NHxRoughness; // index: 134217729, m_SamplerIndex: 1

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // SHADOWS_SCREEN
#endif // DIRECTIONAL
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 71 for m_GpuProgramType: #25
        // m_BlobIndex: 25
        // m_ShaderHardwareTier: 1

#ifdef DIRECTIONAL
#ifdef SHADOWS_SCREEN

        // m_TextureParams
        sampler2D _MainTex; // index: 134217728, m_SamplerIndex: 0
        sampler2D _ShadowMapTexture; // index: 134217729, m_SamplerIndex: 1

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // SHADOWS_SCREEN
#endif // DIRECTIONAL
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 73 for m_GpuProgramType: #5
        // m_BlobIndex: 26
        // m_ShaderHardwareTier: 0

#ifdef DIRECTIONAL_COOKIE
#ifdef SHADOWS_SCREEN

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // SHADOWS_SCREEN
#endif // DIRECTIONAL_COOKIE
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 74 for m_GpuProgramType: #5
        // m_BlobIndex: 27
        // m_ShaderHardwareTier: 1

#ifdef DIRECTIONAL_COOKIE
#ifdef SHADOWS_SCREEN

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // SHADOWS_SCREEN
#endif // DIRECTIONAL_COOKIE
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 76 for m_GpuProgramType: #4
        // m_BlobIndex: 26
        // m_ShaderHardwareTier: 0

#ifdef DIRECTIONAL_COOKIE
#ifdef SHADOWS_SCREEN

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // SHADOWS_SCREEN
#endif // DIRECTIONAL_COOKIE
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 77 for m_GpuProgramType: #4
        // m_BlobIndex: 27
        // m_ShaderHardwareTier: 1

#ifdef DIRECTIONAL_COOKIE
#ifdef SHADOWS_SCREEN

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // SHADOWS_SCREEN
#endif // DIRECTIONAL_COOKIE
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 79 for m_GpuProgramType: #25
        // m_BlobIndex: 26
        // m_ShaderHardwareTier: 0

#ifdef DIRECTIONAL_COOKIE
#ifdef SHADOWS_SCREEN

        // m_TextureParams
        sampler2D _LightTexture0; // index: 134217730, m_SamplerIndex: 2
        sampler2D _MainTex; // index: 134217728, m_SamplerIndex: 0
        sampler2D _ShadowMapTexture; // index: 134217731, m_SamplerIndex: 3
        sampler2D unity_NHxRoughness; // index: 134217729, m_SamplerIndex: 1

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // SHADOWS_SCREEN
#endif // DIRECTIONAL_COOKIE
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 80 for m_GpuProgramType: #25
        // m_BlobIndex: 27
        // m_ShaderHardwareTier: 1

#ifdef DIRECTIONAL_COOKIE
#ifdef SHADOWS_SCREEN

        // m_TextureParams
        sampler2D _LightTexture0; // index: 134217729, m_SamplerIndex: 1
        sampler2D _MainTex; // index: 134217728, m_SamplerIndex: 0
        sampler2D _ShadowMapTexture; // index: 134217730, m_SamplerIndex: 2

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // SHADOWS_SCREEN
#endif // DIRECTIONAL_COOKIE
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 82 for m_GpuProgramType: #5
        // m_BlobIndex: 28
        // m_ShaderHardwareTier: 0

#ifdef POINT
#ifdef SHADOWS_CUBE

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // SHADOWS_CUBE
#endif // POINT
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 83 for m_GpuProgramType: #5
        // m_BlobIndex: 29
        // m_ShaderHardwareTier: 1

#ifdef POINT
#ifdef SHADOWS_CUBE

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // SHADOWS_CUBE
#endif // POINT
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 85 for m_GpuProgramType: #4
        // m_BlobIndex: 28
        // m_ShaderHardwareTier: 0

#ifdef POINT
#ifdef SHADOWS_CUBE

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // SHADOWS_CUBE
#endif // POINT
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 86 for m_GpuProgramType: #4
        // m_BlobIndex: 29
        // m_ShaderHardwareTier: 1

#ifdef POINT
#ifdef SHADOWS_CUBE

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // SHADOWS_CUBE
#endif // POINT
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 88 for m_GpuProgramType: #25
        // m_BlobIndex: 28
        // m_ShaderHardwareTier: 0

#ifdef POINT
#ifdef SHADOWS_CUBE

        // m_TextureParams
        sampler2D _LightTexture0; // index: 134217731, m_SamplerIndex: 3
        sampler2D _MainTex; // index: 134217728, m_SamplerIndex: 0
        sampler4D _ShadowMapTexture; // index: 134217730, m_SamplerIndex: 2
        sampler2D unity_NHxRoughness; // index: 134217729, m_SamplerIndex: 1

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // SHADOWS_CUBE
#endif // POINT
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 89 for m_GpuProgramType: #25
        // m_BlobIndex: 29
        // m_ShaderHardwareTier: 1

#ifdef POINT
#ifdef SHADOWS_CUBE

        // m_TextureParams
        sampler2D _LightTexture0; // index: 134217730, m_SamplerIndex: 2
        sampler2D _MainTex; // index: 134217728, m_SamplerIndex: 0
        sampler4D _ShadowMapTexture; // index: 134217729, m_SamplerIndex: 1

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // SHADOWS_CUBE
#endif // POINT
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 91 for m_GpuProgramType: #5
        // m_BlobIndex: 30
        // m_ShaderHardwareTier: 0

#ifdef POINT
#ifdef SHADOWS_CUBE
#ifdef SHADOWS_SOFT

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // SHADOWS_SOFT
#endif // SHADOWS_CUBE
#endif // POINT
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 92 for m_GpuProgramType: #5
        // m_BlobIndex: 31
        // m_ShaderHardwareTier: 1

#ifdef POINT
#ifdef SHADOWS_CUBE
#ifdef SHADOWS_SOFT

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // SHADOWS_SOFT
#endif // SHADOWS_CUBE
#endif // POINT
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 94 for m_GpuProgramType: #4
        // m_BlobIndex: 30
        // m_ShaderHardwareTier: 0

#ifdef POINT
#ifdef SHADOWS_CUBE
#ifdef SHADOWS_SOFT

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // SHADOWS_SOFT
#endif // SHADOWS_CUBE
#endif // POINT
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 95 for m_GpuProgramType: #4
        // m_BlobIndex: 31
        // m_ShaderHardwareTier: 1

#ifdef POINT
#ifdef SHADOWS_CUBE
#ifdef SHADOWS_SOFT

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // SHADOWS_SOFT
#endif // SHADOWS_CUBE
#endif // POINT
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 97 for m_GpuProgramType: #25
        // m_BlobIndex: 30
        // m_ShaderHardwareTier: 0

#ifdef POINT
#ifdef SHADOWS_CUBE
#ifdef SHADOWS_SOFT

        // m_TextureParams
        sampler2D _LightTexture0; // index: 134217731, m_SamplerIndex: 3
        sampler2D _MainTex; // index: 134217728, m_SamplerIndex: 0
        sampler4D _ShadowMapTexture; // index: 134217730, m_SamplerIndex: 2
        sampler2D unity_NHxRoughness; // index: 134217729, m_SamplerIndex: 1

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // SHADOWS_SOFT
#endif // SHADOWS_CUBE
#endif // POINT
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 98 for m_GpuProgramType: #25
        // m_BlobIndex: 31
        // m_ShaderHardwareTier: 1

#ifdef POINT
#ifdef SHADOWS_CUBE
#ifdef SHADOWS_SOFT

        // m_TextureParams
        sampler2D _LightTexture0; // index: 134217730, m_SamplerIndex: 2
        sampler2D _MainTex; // index: 134217728, m_SamplerIndex: 0
        sampler4D _ShadowMapTexture; // index: 134217729, m_SamplerIndex: 1

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // SHADOWS_SOFT
#endif // SHADOWS_CUBE
#endif // POINT
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 100 for m_GpuProgramType: #5
        // m_BlobIndex: 32
        // m_ShaderHardwareTier: 0

#ifdef POINT_COOKIE
#ifdef SHADOWS_CUBE

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // SHADOWS_CUBE
#endif // POINT_COOKIE
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 101 for m_GpuProgramType: #5
        // m_BlobIndex: 33
        // m_ShaderHardwareTier: 1

#ifdef POINT_COOKIE
#ifdef SHADOWS_CUBE

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // SHADOWS_CUBE
#endif // POINT_COOKIE
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 103 for m_GpuProgramType: #4
        // m_BlobIndex: 32
        // m_ShaderHardwareTier: 0

#ifdef POINT_COOKIE
#ifdef SHADOWS_CUBE

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // SHADOWS_CUBE
#endif // POINT_COOKIE
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 104 for m_GpuProgramType: #4
        // m_BlobIndex: 33
        // m_ShaderHardwareTier: 1

#ifdef POINT_COOKIE
#ifdef SHADOWS_CUBE

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // SHADOWS_CUBE
#endif // POINT_COOKIE
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 106 for m_GpuProgramType: #25
        // m_BlobIndex: 32
        // m_ShaderHardwareTier: 0

#ifdef POINT_COOKIE
#ifdef SHADOWS_CUBE

        // m_TextureParams
        sampler4D _LightTexture0; // index: 134217732, m_SamplerIndex: 4
        sampler2D _LightTextureB0; // index: 134217731, m_SamplerIndex: 3
        sampler2D _MainTex; // index: 134217728, m_SamplerIndex: 0
        sampler4D _ShadowMapTexture; // index: 134217730, m_SamplerIndex: 2
        sampler2D unity_NHxRoughness; // index: 134217729, m_SamplerIndex: 1

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // SHADOWS_CUBE
#endif // POINT_COOKIE
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 107 for m_GpuProgramType: #25
        // m_BlobIndex: 33
        // m_ShaderHardwareTier: 1

#ifdef POINT_COOKIE
#ifdef SHADOWS_CUBE

        // m_TextureParams
        sampler4D _LightTexture0; // index: 134217731, m_SamplerIndex: 3
        sampler2D _LightTextureB0; // index: 134217730, m_SamplerIndex: 2
        sampler2D _MainTex; // index: 134217728, m_SamplerIndex: 0
        sampler4D _ShadowMapTexture; // index: 134217729, m_SamplerIndex: 1

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // SHADOWS_CUBE
#endif // POINT_COOKIE
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 109 for m_GpuProgramType: #5
        // m_BlobIndex: 34
        // m_ShaderHardwareTier: 0

#ifdef POINT_COOKIE
#ifdef SHADOWS_CUBE
#ifdef SHADOWS_SOFT

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // SHADOWS_SOFT
#endif // SHADOWS_CUBE
#endif // POINT_COOKIE
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 110 for m_GpuProgramType: #5
        // m_BlobIndex: 35
        // m_ShaderHardwareTier: 1

#ifdef POINT_COOKIE
#ifdef SHADOWS_CUBE
#ifdef SHADOWS_SOFT

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // SHADOWS_SOFT
#endif // SHADOWS_CUBE
#endif // POINT_COOKIE
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 112 for m_GpuProgramType: #4
        // m_BlobIndex: 34
        // m_ShaderHardwareTier: 0

#ifdef POINT_COOKIE
#ifdef SHADOWS_CUBE
#ifdef SHADOWS_SOFT

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // SHADOWS_SOFT
#endif // SHADOWS_CUBE
#endif // POINT_COOKIE
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 113 for m_GpuProgramType: #4
        // m_BlobIndex: 35
        // m_ShaderHardwareTier: 1

#ifdef POINT_COOKIE
#ifdef SHADOWS_CUBE
#ifdef SHADOWS_SOFT

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // SHADOWS_SOFT
#endif // SHADOWS_CUBE
#endif // POINT_COOKIE
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 115 for m_GpuProgramType: #25
        // m_BlobIndex: 34
        // m_ShaderHardwareTier: 0

#ifdef POINT_COOKIE
#ifdef SHADOWS_CUBE
#ifdef SHADOWS_SOFT

        // m_TextureParams
        sampler4D _LightTexture0; // index: 134217732, m_SamplerIndex: 4
        sampler2D _LightTextureB0; // index: 134217731, m_SamplerIndex: 3
        sampler2D _MainTex; // index: 134217728, m_SamplerIndex: 0
        sampler4D _ShadowMapTexture; // index: 134217730, m_SamplerIndex: 2
        sampler2D unity_NHxRoughness; // index: 134217729, m_SamplerIndex: 1

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // SHADOWS_SOFT
#endif // SHADOWS_CUBE
#endif // POINT_COOKIE
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 116 for m_GpuProgramType: #25
        // m_BlobIndex: 35
        // m_ShaderHardwareTier: 1

#ifdef POINT_COOKIE
#ifdef SHADOWS_CUBE
#ifdef SHADOWS_SOFT

        // m_TextureParams
        sampler4D _LightTexture0; // index: 134217731, m_SamplerIndex: 3
        sampler2D _LightTextureB0; // index: 134217730, m_SamplerIndex: 2
        sampler2D _MainTex; // index: 134217728, m_SamplerIndex: 0
        sampler4D _ShadowMapTexture; // index: 134217729, m_SamplerIndex: 1

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // SHADOWS_SOFT
#endif // SHADOWS_CUBE
#endif // POINT_COOKIE
        //
        // ++++++++++++++++

        //
        // ======================


        // ======================
        // SerializedProgram: progFragment

        // ++++++++++++++++
        // Sub Program: 1 for m_GpuProgramType: #5
        // m_BlobIndex: 36
        // m_ShaderHardwareTier: 0

#ifdef POINT

        //  m_ShaderRequirements: 
        fixed4 fragmentShader() : SV_Target
        {
            return fixed4(1,1,1,1); // IS STUB!!!!
        }
#endif // POINT
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 10 for m_GpuProgramType: #5
        // m_BlobIndex: 8
        // m_ShaderHardwareTier: 0

#ifdef DIRECTIONAL

        //  m_ShaderRequirements: 
        fixed4 fragmentShader() : SV_Target
        {
            return fixed4(1,1,1,1); // IS STUB!!!!
        }
#endif // DIRECTIONAL
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 19 for m_GpuProgramType: #5
        // m_BlobIndex: 37
        // m_ShaderHardwareTier: 0

#ifdef SPOT

        //  m_ShaderRequirements: 
        fixed4 fragmentShader() : SV_Target
        {
            return fixed4(1,1,1,1); // IS STUB!!!!
        }
#endif // SPOT
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 28 for m_GpuProgramType: #5
        // m_BlobIndex: 38
        // m_ShaderHardwareTier: 0

#ifdef POINT_COOKIE

        //  m_ShaderRequirements: 
        fixed4 fragmentShader() : SV_Target
        {
            return fixed4(1,1,1,1); // IS STUB!!!!
        }
#endif // POINT_COOKIE
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 37 for m_GpuProgramType: #5
        // m_BlobIndex: 39
        // m_ShaderHardwareTier: 0

#ifdef DIRECTIONAL_COOKIE

        //  m_ShaderRequirements: 
        fixed4 fragmentShader() : SV_Target
        {
            return fixed4(1,1,1,1); // IS STUB!!!!
        }
#endif // DIRECTIONAL_COOKIE
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 46 for m_GpuProgramType: #5
        // m_BlobIndex: 40
        // m_ShaderHardwareTier: 0

#ifdef SPOT
#ifdef SHADOWS_DEPTH

        //  m_ShaderRequirements: 
        fixed4 fragmentShader() : SV_Target
        {
            return fixed4(1,1,1,1); // IS STUB!!!!
        }
#endif // SHADOWS_DEPTH
#endif // SPOT
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 55 for m_GpuProgramType: #5
        // m_BlobIndex: 41
        // m_ShaderHardwareTier: 0

#ifdef SPOT
#ifdef SHADOWS_DEPTH
#ifdef SHADOWS_SOFT

        //  m_ShaderRequirements: 
        fixed4 fragmentShader() : SV_Target
        {
            return fixed4(1,1,1,1); // IS STUB!!!!
        }
#endif // SHADOWS_SOFT
#endif // SHADOWS_DEPTH
#endif // SPOT
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 64 for m_GpuProgramType: #5
        // m_BlobIndex: 9
        // m_ShaderHardwareTier: 0

#ifdef DIRECTIONAL
#ifdef SHADOWS_SCREEN

        //  m_ShaderRequirements: 
        fixed4 fragmentShader() : SV_Target
        {
            return fixed4(1,1,1,1); // IS STUB!!!!
        }
#endif // SHADOWS_SCREEN
#endif // DIRECTIONAL
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 73 for m_GpuProgramType: #5
        // m_BlobIndex: 42
        // m_ShaderHardwareTier: 0

#ifdef DIRECTIONAL_COOKIE
#ifdef SHADOWS_SCREEN

        //  m_ShaderRequirements: 
        fixed4 fragmentShader() : SV_Target
        {
            return fixed4(1,1,1,1); // IS STUB!!!!
        }
#endif // SHADOWS_SCREEN
#endif // DIRECTIONAL_COOKIE
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 82 for m_GpuProgramType: #5
        // m_BlobIndex: 43
        // m_ShaderHardwareTier: 0

#ifdef POINT
#ifdef SHADOWS_CUBE

        //  m_ShaderRequirements: 
        fixed4 fragmentShader() : SV_Target
        {
            return fixed4(1,1,1,1); // IS STUB!!!!
        }
#endif // SHADOWS_CUBE
#endif // POINT
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 91 for m_GpuProgramType: #5
        // m_BlobIndex: 44
        // m_ShaderHardwareTier: 0

#ifdef POINT
#ifdef SHADOWS_CUBE
#ifdef SHADOWS_SOFT

        //  m_ShaderRequirements: 
        fixed4 fragmentShader() : SV_Target
        {
            return fixed4(1,1,1,1); // IS STUB!!!!
        }
#endif // SHADOWS_SOFT
#endif // SHADOWS_CUBE
#endif // POINT
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 100 for m_GpuProgramType: #5
        // m_BlobIndex: 45
        // m_ShaderHardwareTier: 0

#ifdef POINT_COOKIE
#ifdef SHADOWS_CUBE

        //  m_ShaderRequirements: 
        fixed4 fragmentShader() : SV_Target
        {
            return fixed4(1,1,1,1); // IS STUB!!!!
        }
#endif // SHADOWS_CUBE
#endif // POINT_COOKIE
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 109 for m_GpuProgramType: #5
        // m_BlobIndex: 46
        // m_ShaderHardwareTier: 0

#ifdef POINT_COOKIE
#ifdef SHADOWS_CUBE
#ifdef SHADOWS_SOFT

        //  m_ShaderRequirements: 
        fixed4 fragmentShader() : SV_Target
        {
            return fixed4(1,1,1,1); // IS STUB!!!!
        }
#endif // SHADOWS_SOFT
#endif // SHADOWS_CUBE
#endif // POINT_COOKIE
        //
        // ++++++++++++++++

        //
        // ======================


        ENDCG
      } // end phase
      Pass // ind: 3, name: SHADOWCASTER
      {
        Name "SHADOWCASTER"
        Tags
        { 
        "LIGHTMODE" = "SHADOWCASTER"
        "PerformanceChecks" = "False"
        "RenderType" = "Opaque"
        "SHADOWSUPPORT" = "true"
        }
        LOD 300
        // m_ProgramMask = 6
        CGPROGRAM
#pragma target 3.0
#pragma vertex vertexShader
#pragma fragment fragmentShader
#pragma multi_compile SHADOWS_DEPTH SHADOWS_CUBE

        // ======================
        // SerializedProgram: progVertex

        // ++++++++++++++++
        // Sub Program: 1 for m_GpuProgramType: #5
        // m_BlobIndex: 47
        // m_ShaderHardwareTier: 0

#ifdef SHADOWS_DEPTH

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // SHADOWS_DEPTH
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 10 for m_GpuProgramType: #5
        // m_BlobIndex: 48
        // m_ShaderHardwareTier: 0

#ifdef SHADOWS_CUBE

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // SHADOWS_CUBE
        //
        // ++++++++++++++++

        //
        // ======================


        // ======================
        // SerializedProgram: progFragment

        // ++++++++++++++++
        // Sub Program: 1 for m_GpuProgramType: #5
        // m_BlobIndex: 49
        // m_ShaderHardwareTier: 0

#ifdef SHADOWS_DEPTH

        //  m_ShaderRequirements: 
        fixed4 fragmentShader() : SV_Target
        {
            return fixed4(1,1,1,1); // IS STUB!!!!
        }
#endif // SHADOWS_DEPTH
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 10 for m_GpuProgramType: #5
        // m_BlobIndex: 50
        // m_ShaderHardwareTier: 0

#ifdef SHADOWS_CUBE

        //  m_ShaderRequirements: 
        fixed4 fragmentShader() : SV_Target
        {
            return fixed4(1,1,1,1); // IS STUB!!!!
        }
#endif // SHADOWS_CUBE
        //
        // ++++++++++++++++

        //
        // ======================


        ENDCG
      } // end phase
      Pass // ind: 4, name: DEFERRED
      {
        Name "DEFERRED"
        Tags
        { 
        "LIGHTMODE" = "DEFERRED"
        "PerformanceChecks" = "False"
        "RenderType" = "Opaque"
        }
        LOD 300
        // m_ProgramMask = 6
        CGPROGRAM
#pragma target 3.0
#pragma vertex vertexShader
#pragma fragment fragmentShader
#pragma multi_compile UNITY_HDR_ON

        // ======================
        // SerializedProgram: progVertex

        // ++++++++++++++++
        // Sub Program: 1 for m_GpuProgramType: #5
        // m_BlobIndex: 51
        // m_ShaderHardwareTier: 0


        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 2 for m_GpuProgramType: #5
        // m_BlobIndex: 52
        // m_ShaderHardwareTier: 1


        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 4 for m_GpuProgramType: #4
        // m_BlobIndex: 51
        // m_ShaderHardwareTier: 0


        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 10 for m_GpuProgramType: #5
        // m_BlobIndex: 53
        // m_ShaderHardwareTier: 0

#ifdef UNITY_HDR_ON

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // UNITY_HDR_ON
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 13 for m_GpuProgramType: #4
        // m_BlobIndex: 52
        // m_ShaderHardwareTier: 0

#ifdef UNITY_HDR_ON

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // UNITY_HDR_ON
        //
        // ++++++++++++++++

        //
        // ======================


        // ======================
        // SerializedProgram: progFragment

        // ++++++++++++++++
        // Sub Program: 1 for m_GpuProgramType: #5
        // m_BlobIndex: 54
        // m_ShaderHardwareTier: 0


        //  m_ShaderRequirements: 
        fixed4 fragmentShader() : SV_Target
        {
            return fixed4(1,1,1,1); // IS STUB!!!!
        }
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 4 for m_GpuProgramType: #4
        // m_BlobIndex: 53
        // m_ShaderHardwareTier: 0


        //  m_ShaderRequirements: 
        fixed4 fragmentShader() : SV_Target
        {
            return fixed4(1,1,1,1); // IS STUB!!!!
        }
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 10 for m_GpuProgramType: #5
        // m_BlobIndex: 55
        // m_ShaderHardwareTier: 0

#ifdef UNITY_HDR_ON

        //  m_ShaderRequirements: 
        fixed4 fragmentShader() : SV_Target
        {
            return fixed4(1,1,1,1); // IS STUB!!!!
        }
#endif // UNITY_HDR_ON
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 13 for m_GpuProgramType: #4
        // m_BlobIndex: 54
        // m_ShaderHardwareTier: 0

#ifdef UNITY_HDR_ON

        //  m_ShaderRequirements: 
        fixed4 fragmentShader() : SV_Target
        {
            return fixed4(1,1,1,1); // IS STUB!!!!
        }
#endif // UNITY_HDR_ON
        //
        // ++++++++++++++++

        //
        // ======================


        ENDCG
      } // end phase
      Pass // ind: 5, name: META
      {
        Name "META"
        Tags
        { 
        "LIGHTMODE" = "META"
        "PerformanceChecks" = "False"
        "RenderType" = "Opaque"
        }
        LOD 300
        // m_ProgramMask = 6
        CGPROGRAM
#pragma target 3.0
#pragma vertex vertexShader
#pragma fragment fragmentShader

        // ======================
        // SerializedProgram: progVertex

        // ++++++++++++++++
        // Sub Program: 1 for m_GpuProgramType: #5
        // m_BlobIndex: 56
        // m_ShaderHardwareTier: 0


        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 4 for m_GpuProgramType: #4
        // m_BlobIndex: 55
        // m_ShaderHardwareTier: 0


        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
        //
        // ++++++++++++++++

        //
        // ======================


        // ======================
        // SerializedProgram: progFragment

        // ++++++++++++++++
        // Sub Program: 1 for m_GpuProgramType: #5
        // m_BlobIndex: 54
        // m_ShaderHardwareTier: 0


        //  m_ShaderRequirements: 
        fixed4 fragmentShader() : SV_Target
        {
            return fixed4(1,1,1,1); // IS STUB!!!!
        }
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 4 for m_GpuProgramType: #4
        // m_BlobIndex: 53
        // m_ShaderHardwareTier: 0


        //  m_ShaderRequirements: 
        fixed4 fragmentShader() : SV_Target
        {
            return fixed4(1,1,1,1); // IS STUB!!!!
        }
        //
        // ++++++++++++++++

        //
        // ======================


        ENDCG
      } // end phase
  }
  SubShader
  {
      Tags
      { 
        "PerformanceChecks" = "False"
        "RenderType" = "Opaque"
      }
      LOD 150
      Pass // ind: 1, name: FORWARD
      {
        Name "FORWARD"
        Tags
        { 
        "LIGHTMODE" = "FORWARDBASE"
        "PerformanceChecks" = "False"
        "RenderType" = "Opaque"
        "SHADOWSUPPORT" = "true"
        }
        LOD 150
        // m_ProgramMask = 6
        CGPROGRAM
#pragma target 3.0
#pragma vertex vertexShader
#pragma fragment fragmentShader
#pragma multi_compile DIRECTIONAL SHADOWS_SCREEN VERTEXLIGHT_ON

        // ======================
        // SerializedProgram: progVertex

        // ++++++++++++++++
        // Sub Program: 1 for m_GpuProgramType: #5
        // m_BlobIndex: 57
        // m_ShaderHardwareTier: 0

#ifdef DIRECTIONAL

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // DIRECTIONAL
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 2 for m_GpuProgramType: #5
        // m_BlobIndex: 58
        // m_ShaderHardwareTier: 1

#ifdef DIRECTIONAL

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // DIRECTIONAL
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 4 for m_GpuProgramType: #4
        // m_BlobIndex: 56
        // m_ShaderHardwareTier: 0

#ifdef DIRECTIONAL

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // DIRECTIONAL
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 5 for m_GpuProgramType: #4
        // m_BlobIndex: 57
        // m_ShaderHardwareTier: 1

#ifdef DIRECTIONAL

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // DIRECTIONAL
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 7 for m_GpuProgramType: #25
        // m_BlobIndex: 56
        // m_ShaderHardwareTier: 0

#ifdef DIRECTIONAL

        // m_TextureParams
        sampler2D _MainTex; // index: 134217728, m_SamplerIndex: 0
        sampler2D _OcclusionMap; // index: 134217729, m_SamplerIndex: 1
        sampler2D unity_NHxRoughness; // index: 134217730, m_SamplerIndex: 2
        sampler4D unity_SpecCube0; // index: 134217731, m_SamplerIndex: 3

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // DIRECTIONAL
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 8 for m_GpuProgramType: #25
        // m_BlobIndex: 57
        // m_ShaderHardwareTier: 1

#ifdef DIRECTIONAL

        // m_TextureParams
        sampler2D _MainTex; // index: 134217728, m_SamplerIndex: 0
        sampler2D _OcclusionMap; // index: 134217729, m_SamplerIndex: 1
        sampler2D unity_NHxRoughness; // index: 134217730, m_SamplerIndex: 2
        sampler4D unity_SpecCube0; // index: 134217731, m_SamplerIndex: 3

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // DIRECTIONAL
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 10 for m_GpuProgramType: #5
        // m_BlobIndex: 2
        // m_ShaderHardwareTier: 0

#ifdef DIRECTIONAL
#ifdef SHADOWS_SCREEN

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // SHADOWS_SCREEN
#endif // DIRECTIONAL
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 11 for m_GpuProgramType: #5
        // m_BlobIndex: 3
        // m_ShaderHardwareTier: 1

#ifdef DIRECTIONAL
#ifdef SHADOWS_SCREEN

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // SHADOWS_SCREEN
#endif // DIRECTIONAL
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 13 for m_GpuProgramType: #4
        // m_BlobIndex: 2
        // m_ShaderHardwareTier: 0

#ifdef DIRECTIONAL
#ifdef SHADOWS_SCREEN

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // SHADOWS_SCREEN
#endif // DIRECTIONAL
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 14 for m_GpuProgramType: #4
        // m_BlobIndex: 3
        // m_ShaderHardwareTier: 1

#ifdef DIRECTIONAL
#ifdef SHADOWS_SCREEN

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // SHADOWS_SCREEN
#endif // DIRECTIONAL
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 16 for m_GpuProgramType: #25
        // m_BlobIndex: 2
        // m_ShaderHardwareTier: 0

#ifdef DIRECTIONAL
#ifdef SHADOWS_SCREEN

        // m_TextureParams
        sampler2D _MainTex; // index: 134217728, m_SamplerIndex: 0
        sampler2D _OcclusionMap; // index: 134217729, m_SamplerIndex: 1
        sampler2D _ShadowMapTexture; // index: 134217732, m_SamplerIndex: 4
        sampler2D unity_NHxRoughness; // index: 134217730, m_SamplerIndex: 2
        sampler4D unity_SpecCube0; // index: 134217731, m_SamplerIndex: 3

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // SHADOWS_SCREEN
#endif // DIRECTIONAL
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 17 for m_GpuProgramType: #25
        // m_BlobIndex: 3
        // m_ShaderHardwareTier: 1

#ifdef DIRECTIONAL
#ifdef SHADOWS_SCREEN

        // m_TextureParams
        sampler2D _MainTex; // index: 134217728, m_SamplerIndex: 0
        sampler2D _OcclusionMap; // index: 134217729, m_SamplerIndex: 1
        sampler2D _ShadowMapTexture; // index: 134217731, m_SamplerIndex: 3
        sampler4D unity_SpecCube0; // index: 134217730, m_SamplerIndex: 2

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // SHADOWS_SCREEN
#endif // DIRECTIONAL
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 19 for m_GpuProgramType: #5
        // m_BlobIndex: 59
        // m_ShaderHardwareTier: 0

#ifdef DIRECTIONAL
#ifdef VERTEXLIGHT_ON

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // VERTEXLIGHT_ON
#endif // DIRECTIONAL
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 20 for m_GpuProgramType: #5
        // m_BlobIndex: 60
        // m_ShaderHardwareTier: 1

#ifdef DIRECTIONAL
#ifdef VERTEXLIGHT_ON

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // VERTEXLIGHT_ON
#endif // DIRECTIONAL
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 22 for m_GpuProgramType: #4
        // m_BlobIndex: 58
        // m_ShaderHardwareTier: 0

#ifdef DIRECTIONAL
#ifdef VERTEXLIGHT_ON

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // VERTEXLIGHT_ON
#endif // DIRECTIONAL
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 23 for m_GpuProgramType: #4
        // m_BlobIndex: 59
        // m_ShaderHardwareTier: 1

#ifdef DIRECTIONAL
#ifdef VERTEXLIGHT_ON

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // VERTEXLIGHT_ON
#endif // DIRECTIONAL
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 25 for m_GpuProgramType: #25
        // m_BlobIndex: 58
        // m_ShaderHardwareTier: 0

#ifdef DIRECTIONAL
#ifdef VERTEXLIGHT_ON

        // m_TextureParams
        sampler2D _MainTex; // index: 134217728, m_SamplerIndex: 0
        sampler2D _OcclusionMap; // index: 134217729, m_SamplerIndex: 1
        sampler2D unity_NHxRoughness; // index: 134217730, m_SamplerIndex: 2
        sampler4D unity_SpecCube0; // index: 134217731, m_SamplerIndex: 3

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // VERTEXLIGHT_ON
#endif // DIRECTIONAL
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 26 for m_GpuProgramType: #25
        // m_BlobIndex: 59
        // m_ShaderHardwareTier: 1

#ifdef DIRECTIONAL
#ifdef VERTEXLIGHT_ON

        // m_TextureParams
        sampler2D _MainTex; // index: 134217728, m_SamplerIndex: 0
        sampler2D _OcclusionMap; // index: 134217729, m_SamplerIndex: 1
        sampler2D unity_NHxRoughness; // index: 134217730, m_SamplerIndex: 2
        sampler4D unity_SpecCube0; // index: 134217731, m_SamplerIndex: 3

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // VERTEXLIGHT_ON
#endif // DIRECTIONAL
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 28 for m_GpuProgramType: #5
        // m_BlobIndex: 6
        // m_ShaderHardwareTier: 0

#ifdef DIRECTIONAL
#ifdef SHADOWS_SCREEN
#ifdef VERTEXLIGHT_ON

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // VERTEXLIGHT_ON
#endif // SHADOWS_SCREEN
#endif // DIRECTIONAL
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 29 for m_GpuProgramType: #5
        // m_BlobIndex: 7
        // m_ShaderHardwareTier: 1

#ifdef DIRECTIONAL
#ifdef SHADOWS_SCREEN
#ifdef VERTEXLIGHT_ON

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // VERTEXLIGHT_ON
#endif // SHADOWS_SCREEN
#endif // DIRECTIONAL
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 31 for m_GpuProgramType: #4
        // m_BlobIndex: 6
        // m_ShaderHardwareTier: 0

#ifdef DIRECTIONAL
#ifdef SHADOWS_SCREEN
#ifdef VERTEXLIGHT_ON

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // VERTEXLIGHT_ON
#endif // SHADOWS_SCREEN
#endif // DIRECTIONAL
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 32 for m_GpuProgramType: #4
        // m_BlobIndex: 7
        // m_ShaderHardwareTier: 1

#ifdef DIRECTIONAL
#ifdef SHADOWS_SCREEN
#ifdef VERTEXLIGHT_ON

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // VERTEXLIGHT_ON
#endif // SHADOWS_SCREEN
#endif // DIRECTIONAL
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 34 for m_GpuProgramType: #25
        // m_BlobIndex: 6
        // m_ShaderHardwareTier: 0

#ifdef DIRECTIONAL
#ifdef SHADOWS_SCREEN
#ifdef VERTEXLIGHT_ON

        // m_TextureParams
        sampler2D _MainTex; // index: 134217728, m_SamplerIndex: 0
        sampler2D _OcclusionMap; // index: 134217729, m_SamplerIndex: 1
        sampler2D _ShadowMapTexture; // index: 134217732, m_SamplerIndex: 4
        sampler2D unity_NHxRoughness; // index: 134217730, m_SamplerIndex: 2
        sampler4D unity_SpecCube0; // index: 134217731, m_SamplerIndex: 3

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // VERTEXLIGHT_ON
#endif // SHADOWS_SCREEN
#endif // DIRECTIONAL
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 35 for m_GpuProgramType: #25
        // m_BlobIndex: 7
        // m_ShaderHardwareTier: 1

#ifdef DIRECTIONAL
#ifdef SHADOWS_SCREEN
#ifdef VERTEXLIGHT_ON

        // m_TextureParams
        sampler2D _MainTex; // index: 134217728, m_SamplerIndex: 0
        sampler2D _OcclusionMap; // index: 134217729, m_SamplerIndex: 1
        sampler2D _ShadowMapTexture; // index: 134217731, m_SamplerIndex: 3
        sampler4D unity_SpecCube0; // index: 134217730, m_SamplerIndex: 2

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // VERTEXLIGHT_ON
#endif // SHADOWS_SCREEN
#endif // DIRECTIONAL
        //
        // ++++++++++++++++

        //
        // ======================


        // ======================
        // SerializedProgram: progFragment

        // ++++++++++++++++
        // Sub Program: 1 for m_GpuProgramType: #5
        // m_BlobIndex: 8
        // m_ShaderHardwareTier: 0

#ifdef DIRECTIONAL

        //  m_ShaderRequirements: 
        fixed4 fragmentShader() : SV_Target
        {
            return fixed4(1,1,1,1); // IS STUB!!!!
        }
#endif // DIRECTIONAL
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 10 for m_GpuProgramType: #5
        // m_BlobIndex: 9
        // m_ShaderHardwareTier: 0

#ifdef DIRECTIONAL
#ifdef SHADOWS_SCREEN

        //  m_ShaderRequirements: 
        fixed4 fragmentShader() : SV_Target
        {
            return fixed4(1,1,1,1); // IS STUB!!!!
        }
#endif // SHADOWS_SCREEN
#endif // DIRECTIONAL
        //
        // ++++++++++++++++

        //
        // ======================


        ENDCG
      } // end phase
      Pass // ind: 2, name: FORWARD_DELTA
      {
        Name "FORWARD_DELTA"
        Tags
        { 
        "LIGHTMODE" = "FORWARDADD"
        "PerformanceChecks" = "False"
        "RenderType" = "Opaque"
        "SHADOWSUPPORT" = "true"
        }
        LOD 150
        // m_ProgramMask = 6
        CGPROGRAM
#pragma target 3.0
#pragma vertex vertexShader
#pragma fragment fragmentShader
#pragma multi_compile POINT DIRECTIONAL SPOT POINT_COOKIE DIRECTIONAL_COOKIE SHADOWS_DEPTH SHADOWS_SCREEN SHADOWS_CUBE

        // ======================
        // SerializedProgram: progVertex

        // ++++++++++++++++
        // Sub Program: 1 for m_GpuProgramType: #5
        // m_BlobIndex: 10
        // m_ShaderHardwareTier: 0

#ifdef POINT

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // POINT
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 2 for m_GpuProgramType: #5
        // m_BlobIndex: 61
        // m_ShaderHardwareTier: 1

#ifdef POINT

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // POINT
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 4 for m_GpuProgramType: #4
        // m_BlobIndex: 10
        // m_ShaderHardwareTier: 0

#ifdef POINT

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // POINT
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 5 for m_GpuProgramType: #4
        // m_BlobIndex: 60
        // m_ShaderHardwareTier: 1

#ifdef POINT

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // POINT
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 7 for m_GpuProgramType: #25
        // m_BlobIndex: 10
        // m_ShaderHardwareTier: 0

#ifdef POINT

        // m_TextureParams
        sampler2D _LightTexture0; // index: 134217730, m_SamplerIndex: 2
        sampler2D _MainTex; // index: 134217728, m_SamplerIndex: 0
        sampler2D unity_NHxRoughness; // index: 134217729, m_SamplerIndex: 1

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // POINT
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 8 for m_GpuProgramType: #25
        // m_BlobIndex: 60
        // m_ShaderHardwareTier: 1

#ifdef POINT

        // m_TextureParams
        sampler2D _LightTexture0; // index: 134217729, m_SamplerIndex: 1
        sampler2D _MainTex; // index: 134217728, m_SamplerIndex: 0
        sampler2D unity_NHxRoughness; // index: 134217730, m_SamplerIndex: 2

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // POINT
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 10 for m_GpuProgramType: #5
        // m_BlobIndex: 12
        // m_ShaderHardwareTier: 0

#ifdef DIRECTIONAL

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // DIRECTIONAL
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 11 for m_GpuProgramType: #5
        // m_BlobIndex: 62
        // m_ShaderHardwareTier: 1

#ifdef DIRECTIONAL

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // DIRECTIONAL
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 13 for m_GpuProgramType: #4
        // m_BlobIndex: 12
        // m_ShaderHardwareTier: 0

#ifdef DIRECTIONAL

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // DIRECTIONAL
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 14 for m_GpuProgramType: #4
        // m_BlobIndex: 61
        // m_ShaderHardwareTier: 1

#ifdef DIRECTIONAL

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // DIRECTIONAL
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 16 for m_GpuProgramType: #25
        // m_BlobIndex: 12
        // m_ShaderHardwareTier: 0

#ifdef DIRECTIONAL

        // m_TextureParams
        sampler2D _MainTex; // index: 134217728, m_SamplerIndex: 0
        sampler2D unity_NHxRoughness; // index: 134217729, m_SamplerIndex: 1

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // DIRECTIONAL
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 17 for m_GpuProgramType: #25
        // m_BlobIndex: 61
        // m_ShaderHardwareTier: 1

#ifdef DIRECTIONAL

        // m_TextureParams
        sampler2D _MainTex; // index: 134217728, m_SamplerIndex: 0
        sampler2D unity_NHxRoughness; // index: 134217729, m_SamplerIndex: 1

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // DIRECTIONAL
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 19 for m_GpuProgramType: #5
        // m_BlobIndex: 14
        // m_ShaderHardwareTier: 0

#ifdef SPOT

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // SPOT
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 20 for m_GpuProgramType: #5
        // m_BlobIndex: 63
        // m_ShaderHardwareTier: 1

#ifdef SPOT

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // SPOT
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 22 for m_GpuProgramType: #4
        // m_BlobIndex: 14
        // m_ShaderHardwareTier: 0

#ifdef SPOT

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // SPOT
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 23 for m_GpuProgramType: #4
        // m_BlobIndex: 62
        // m_ShaderHardwareTier: 1

#ifdef SPOT

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // SPOT
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 25 for m_GpuProgramType: #25
        // m_BlobIndex: 14
        // m_ShaderHardwareTier: 0

#ifdef SPOT

        // m_TextureParams
        sampler2D _LightTexture0; // index: 134217730, m_SamplerIndex: 2
        sampler2D _LightTextureB0; // index: 134217731, m_SamplerIndex: 3
        sampler2D _MainTex; // index: 134217728, m_SamplerIndex: 0
        sampler2D unity_NHxRoughness; // index: 134217729, m_SamplerIndex: 1

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // SPOT
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 26 for m_GpuProgramType: #25
        // m_BlobIndex: 62
        // m_ShaderHardwareTier: 1

#ifdef SPOT

        // m_TextureParams
        sampler2D _LightTexture0; // index: 134217729, m_SamplerIndex: 1
        sampler2D _LightTextureB0; // index: 134217730, m_SamplerIndex: 2
        sampler2D _MainTex; // index: 134217728, m_SamplerIndex: 0
        sampler2D unity_NHxRoughness; // index: 134217731, m_SamplerIndex: 3

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // SPOT
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 28 for m_GpuProgramType: #5
        // m_BlobIndex: 16
        // m_ShaderHardwareTier: 0

#ifdef POINT_COOKIE

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // POINT_COOKIE
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 29 for m_GpuProgramType: #5
        // m_BlobIndex: 64
        // m_ShaderHardwareTier: 1

#ifdef POINT_COOKIE

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // POINT_COOKIE
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 31 for m_GpuProgramType: #4
        // m_BlobIndex: 16
        // m_ShaderHardwareTier: 0

#ifdef POINT_COOKIE

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // POINT_COOKIE
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 32 for m_GpuProgramType: #4
        // m_BlobIndex: 63
        // m_ShaderHardwareTier: 1

#ifdef POINT_COOKIE

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // POINT_COOKIE
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 34 for m_GpuProgramType: #25
        // m_BlobIndex: 16
        // m_ShaderHardwareTier: 0

#ifdef POINT_COOKIE

        // m_TextureParams
        sampler4D _LightTexture0; // index: 134217731, m_SamplerIndex: 3
        sampler2D _LightTextureB0; // index: 134217730, m_SamplerIndex: 2
        sampler2D _MainTex; // index: 134217728, m_SamplerIndex: 0
        sampler2D unity_NHxRoughness; // index: 134217729, m_SamplerIndex: 1

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // POINT_COOKIE
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 35 for m_GpuProgramType: #25
        // m_BlobIndex: 63
        // m_ShaderHardwareTier: 1

#ifdef POINT_COOKIE

        // m_TextureParams
        sampler4D _LightTexture0; // index: 134217730, m_SamplerIndex: 2
        sampler2D _LightTextureB0; // index: 134217729, m_SamplerIndex: 1
        sampler2D _MainTex; // index: 134217728, m_SamplerIndex: 0
        sampler2D unity_NHxRoughness; // index: 134217731, m_SamplerIndex: 3

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // POINT_COOKIE
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 37 for m_GpuProgramType: #5
        // m_BlobIndex: 18
        // m_ShaderHardwareTier: 0

#ifdef DIRECTIONAL_COOKIE

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // DIRECTIONAL_COOKIE
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 38 for m_GpuProgramType: #5
        // m_BlobIndex: 65
        // m_ShaderHardwareTier: 1

#ifdef DIRECTIONAL_COOKIE

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // DIRECTIONAL_COOKIE
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 40 for m_GpuProgramType: #4
        // m_BlobIndex: 18
        // m_ShaderHardwareTier: 0

#ifdef DIRECTIONAL_COOKIE

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // DIRECTIONAL_COOKIE
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 41 for m_GpuProgramType: #4
        // m_BlobIndex: 64
        // m_ShaderHardwareTier: 1

#ifdef DIRECTIONAL_COOKIE

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // DIRECTIONAL_COOKIE
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 43 for m_GpuProgramType: #25
        // m_BlobIndex: 18
        // m_ShaderHardwareTier: 0

#ifdef DIRECTIONAL_COOKIE

        // m_TextureParams
        sampler2D _LightTexture0; // index: 134217730, m_SamplerIndex: 2
        sampler2D _MainTex; // index: 134217728, m_SamplerIndex: 0
        sampler2D unity_NHxRoughness; // index: 134217729, m_SamplerIndex: 1

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // DIRECTIONAL_COOKIE
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 44 for m_GpuProgramType: #25
        // m_BlobIndex: 64
        // m_ShaderHardwareTier: 1

#ifdef DIRECTIONAL_COOKIE

        // m_TextureParams
        sampler2D _LightTexture0; // index: 134217729, m_SamplerIndex: 1
        sampler2D _MainTex; // index: 134217728, m_SamplerIndex: 0
        sampler2D unity_NHxRoughness; // index: 134217730, m_SamplerIndex: 2

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // DIRECTIONAL_COOKIE
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 46 for m_GpuProgramType: #5
        // m_BlobIndex: 20
        // m_ShaderHardwareTier: 0

#ifdef SPOT
#ifdef SHADOWS_DEPTH

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // SHADOWS_DEPTH
#endif // SPOT
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 47 for m_GpuProgramType: #5
        // m_BlobIndex: 66
        // m_ShaderHardwareTier: 1

#ifdef SPOT
#ifdef SHADOWS_DEPTH

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // SHADOWS_DEPTH
#endif // SPOT
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 49 for m_GpuProgramType: #4
        // m_BlobIndex: 20
        // m_ShaderHardwareTier: 0

#ifdef SPOT
#ifdef SHADOWS_DEPTH

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // SHADOWS_DEPTH
#endif // SPOT
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 50 for m_GpuProgramType: #4
        // m_BlobIndex: 65
        // m_ShaderHardwareTier: 1

#ifdef SPOT
#ifdef SHADOWS_DEPTH

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // SHADOWS_DEPTH
#endif // SPOT
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 52 for m_GpuProgramType: #25
        // m_BlobIndex: 20
        // m_ShaderHardwareTier: 0

#ifdef SPOT
#ifdef SHADOWS_DEPTH

        // m_TextureParams
        sampler2D _LightTexture0; // index: 134217730, m_SamplerIndex: 2
        sampler2D _LightTextureB0; // index: 134217731, m_SamplerIndex: 3
        sampler2D _MainTex; // index: 134217728, m_SamplerIndex: 0
        sampler2D _ShadowMapTexture; // index: 134217732, m_SamplerIndex: 4
        sampler2D unity_NHxRoughness; // index: 134217729, m_SamplerIndex: 1

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // SHADOWS_DEPTH
#endif // SPOT
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 53 for m_GpuProgramType: #25
        // m_BlobIndex: 65
        // m_ShaderHardwareTier: 1

#ifdef SPOT
#ifdef SHADOWS_DEPTH

        // m_TextureParams
        sampler2D _LightTexture0; // index: 134217729, m_SamplerIndex: 1
        sampler2D _LightTextureB0; // index: 134217730, m_SamplerIndex: 2
        sampler2D _MainTex; // index: 134217728, m_SamplerIndex: 0
        sampler2D _ShadowMapTexture; // index: 134217732, m_SamplerIndex: 4
        sampler2D unity_NHxRoughness; // index: 134217731, m_SamplerIndex: 3

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // SHADOWS_DEPTH
#endif // SPOT
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 55 for m_GpuProgramType: #5
        // m_BlobIndex: 24
        // m_ShaderHardwareTier: 0

#ifdef DIRECTIONAL
#ifdef SHADOWS_SCREEN

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // SHADOWS_SCREEN
#endif // DIRECTIONAL
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 56 for m_GpuProgramType: #5
        // m_BlobIndex: 25
        // m_ShaderHardwareTier: 1

#ifdef DIRECTIONAL
#ifdef SHADOWS_SCREEN

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // SHADOWS_SCREEN
#endif // DIRECTIONAL
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 58 for m_GpuProgramType: #4
        // m_BlobIndex: 24
        // m_ShaderHardwareTier: 0

#ifdef DIRECTIONAL
#ifdef SHADOWS_SCREEN

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // SHADOWS_SCREEN
#endif // DIRECTIONAL
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 59 for m_GpuProgramType: #4
        // m_BlobIndex: 25
        // m_ShaderHardwareTier: 1

#ifdef DIRECTIONAL
#ifdef SHADOWS_SCREEN

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // SHADOWS_SCREEN
#endif // DIRECTIONAL
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 61 for m_GpuProgramType: #25
        // m_BlobIndex: 24
        // m_ShaderHardwareTier: 0

#ifdef DIRECTIONAL
#ifdef SHADOWS_SCREEN

        // m_TextureParams
        sampler2D _MainTex; // index: 134217728, m_SamplerIndex: 0
        sampler2D _ShadowMapTexture; // index: 134217730, m_SamplerIndex: 2
        sampler2D unity_NHxRoughness; // index: 134217729, m_SamplerIndex: 1

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // SHADOWS_SCREEN
#endif // DIRECTIONAL
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 62 for m_GpuProgramType: #25
        // m_BlobIndex: 25
        // m_ShaderHardwareTier: 1

#ifdef DIRECTIONAL
#ifdef SHADOWS_SCREEN

        // m_TextureParams
        sampler2D _MainTex; // index: 134217728, m_SamplerIndex: 0
        sampler2D _ShadowMapTexture; // index: 134217729, m_SamplerIndex: 1

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // SHADOWS_SCREEN
#endif // DIRECTIONAL
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 64 for m_GpuProgramType: #5
        // m_BlobIndex: 26
        // m_ShaderHardwareTier: 0

#ifdef DIRECTIONAL_COOKIE
#ifdef SHADOWS_SCREEN

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // SHADOWS_SCREEN
#endif // DIRECTIONAL_COOKIE
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 65 for m_GpuProgramType: #5
        // m_BlobIndex: 27
        // m_ShaderHardwareTier: 1

#ifdef DIRECTIONAL_COOKIE
#ifdef SHADOWS_SCREEN

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // SHADOWS_SCREEN
#endif // DIRECTIONAL_COOKIE
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 67 for m_GpuProgramType: #4
        // m_BlobIndex: 26
        // m_ShaderHardwareTier: 0

#ifdef DIRECTIONAL_COOKIE
#ifdef SHADOWS_SCREEN

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // SHADOWS_SCREEN
#endif // DIRECTIONAL_COOKIE
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 68 for m_GpuProgramType: #4
        // m_BlobIndex: 27
        // m_ShaderHardwareTier: 1

#ifdef DIRECTIONAL_COOKIE
#ifdef SHADOWS_SCREEN

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // SHADOWS_SCREEN
#endif // DIRECTIONAL_COOKIE
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 70 for m_GpuProgramType: #25
        // m_BlobIndex: 26
        // m_ShaderHardwareTier: 0

#ifdef DIRECTIONAL_COOKIE
#ifdef SHADOWS_SCREEN

        // m_TextureParams
        sampler2D _LightTexture0; // index: 134217730, m_SamplerIndex: 2
        sampler2D _MainTex; // index: 134217728, m_SamplerIndex: 0
        sampler2D _ShadowMapTexture; // index: 134217731, m_SamplerIndex: 3
        sampler2D unity_NHxRoughness; // index: 134217729, m_SamplerIndex: 1

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // SHADOWS_SCREEN
#endif // DIRECTIONAL_COOKIE
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 71 for m_GpuProgramType: #25
        // m_BlobIndex: 27
        // m_ShaderHardwareTier: 1

#ifdef DIRECTIONAL_COOKIE
#ifdef SHADOWS_SCREEN

        // m_TextureParams
        sampler2D _LightTexture0; // index: 134217729, m_SamplerIndex: 1
        sampler2D _MainTex; // index: 134217728, m_SamplerIndex: 0
        sampler2D _ShadowMapTexture; // index: 134217730, m_SamplerIndex: 2

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // SHADOWS_SCREEN
#endif // DIRECTIONAL_COOKIE
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 73 for m_GpuProgramType: #5
        // m_BlobIndex: 28
        // m_ShaderHardwareTier: 0

#ifdef POINT
#ifdef SHADOWS_CUBE

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // SHADOWS_CUBE
#endif // POINT
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 74 for m_GpuProgramType: #5
        // m_BlobIndex: 67
        // m_ShaderHardwareTier: 1

#ifdef POINT
#ifdef SHADOWS_CUBE

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // SHADOWS_CUBE
#endif // POINT
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 76 for m_GpuProgramType: #4
        // m_BlobIndex: 28
        // m_ShaderHardwareTier: 0

#ifdef POINT
#ifdef SHADOWS_CUBE

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // SHADOWS_CUBE
#endif // POINT
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 77 for m_GpuProgramType: #4
        // m_BlobIndex: 66
        // m_ShaderHardwareTier: 1

#ifdef POINT
#ifdef SHADOWS_CUBE

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // SHADOWS_CUBE
#endif // POINT
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 80 for m_GpuProgramType: #25
        // m_BlobIndex: 67
        // m_ShaderHardwareTier: 1

#ifdef POINT
#ifdef SHADOWS_CUBE

        // m_TextureParams
        sampler2D _LightTexture0; // index: 134217730, m_SamplerIndex: 2
        sampler2D _MainTex; // index: 134217728, m_SamplerIndex: 0
        sampler4D _ShadowMapTexture; // index: 134217729, m_SamplerIndex: 1
        sampler2D unity_NHxRoughness; // index: 134217731, m_SamplerIndex: 3

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // SHADOWS_CUBE
#endif // POINT
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 82 for m_GpuProgramType: #5
        // m_BlobIndex: 32
        // m_ShaderHardwareTier: 0

#ifdef POINT_COOKIE
#ifdef SHADOWS_CUBE

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // SHADOWS_CUBE
#endif // POINT_COOKIE
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 83 for m_GpuProgramType: #5
        // m_BlobIndex: 68
        // m_ShaderHardwareTier: 1

#ifdef POINT_COOKIE
#ifdef SHADOWS_CUBE

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // SHADOWS_CUBE
#endif // POINT_COOKIE
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 85 for m_GpuProgramType: #4
        // m_BlobIndex: 32
        // m_ShaderHardwareTier: 0

#ifdef POINT_COOKIE
#ifdef SHADOWS_CUBE

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // SHADOWS_CUBE
#endif // POINT_COOKIE
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 86 for m_GpuProgramType: #4
        // m_BlobIndex: 67
        // m_ShaderHardwareTier: 1

#ifdef POINT_COOKIE
#ifdef SHADOWS_CUBE

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // SHADOWS_CUBE
#endif // POINT_COOKIE
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 88 for m_GpuProgramType: #25
        // m_BlobIndex: 68
        // m_ShaderHardwareTier: 0

#ifdef POINT_COOKIE
#ifdef SHADOWS_CUBE

        // m_TextureParams
        sampler4D _LightTexture0; // index: 134217732, m_SamplerIndex: 4
        sampler2D _LightTextureB0; // index: 134217731, m_SamplerIndex: 3
        sampler2D _MainTex; // index: 134217728, m_SamplerIndex: 0
        sampler4D _ShadowMapTexture; // index: 134217730, m_SamplerIndex: 2
        sampler2D unity_NHxRoughness; // index: 134217729, m_SamplerIndex: 1

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // SHADOWS_CUBE
#endif // POINT_COOKIE
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 89 for m_GpuProgramType: #25
        // m_BlobIndex: 69
        // m_ShaderHardwareTier: 1

#ifdef POINT_COOKIE
#ifdef SHADOWS_CUBE

        // m_TextureParams
        sampler4D _LightTexture0; // index: 134217731, m_SamplerIndex: 3
        sampler2D _LightTextureB0; // index: 134217730, m_SamplerIndex: 2
        sampler2D _MainTex; // index: 134217728, m_SamplerIndex: 0
        sampler4D _ShadowMapTexture; // index: 134217729, m_SamplerIndex: 1
        sampler2D unity_NHxRoughness; // index: 134217732, m_SamplerIndex: 4

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // SHADOWS_CUBE
#endif // POINT_COOKIE
        //
        // ++++++++++++++++

        //
        // ======================


        // ======================
        // SerializedProgram: progFragment

        // ++++++++++++++++
        // Sub Program: 1 for m_GpuProgramType: #5
        // m_BlobIndex: 36
        // m_ShaderHardwareTier: 0

#ifdef POINT

        //  m_ShaderRequirements: 
        fixed4 fragmentShader() : SV_Target
        {
            return fixed4(1,1,1,1); // IS STUB!!!!
        }
#endif // POINT
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 10 for m_GpuProgramType: #5
        // m_BlobIndex: 8
        // m_ShaderHardwareTier: 0

#ifdef DIRECTIONAL

        //  m_ShaderRequirements: 
        fixed4 fragmentShader() : SV_Target
        {
            return fixed4(1,1,1,1); // IS STUB!!!!
        }
#endif // DIRECTIONAL
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 19 for m_GpuProgramType: #5
        // m_BlobIndex: 37
        // m_ShaderHardwareTier: 0

#ifdef SPOT

        //  m_ShaderRequirements: 
        fixed4 fragmentShader() : SV_Target
        {
            return fixed4(1,1,1,1); // IS STUB!!!!
        }
#endif // SPOT
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 28 for m_GpuProgramType: #5
        // m_BlobIndex: 38
        // m_ShaderHardwareTier: 0

#ifdef POINT_COOKIE

        //  m_ShaderRequirements: 
        fixed4 fragmentShader() : SV_Target
        {
            return fixed4(1,1,1,1); // IS STUB!!!!
        }
#endif // POINT_COOKIE
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 37 for m_GpuProgramType: #5
        // m_BlobIndex: 39
        // m_ShaderHardwareTier: 0

#ifdef DIRECTIONAL_COOKIE

        //  m_ShaderRequirements: 
        fixed4 fragmentShader() : SV_Target
        {
            return fixed4(1,1,1,1); // IS STUB!!!!
        }
#endif // DIRECTIONAL_COOKIE
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 46 for m_GpuProgramType: #5
        // m_BlobIndex: 40
        // m_ShaderHardwareTier: 0

#ifdef SPOT
#ifdef SHADOWS_DEPTH

        //  m_ShaderRequirements: 
        fixed4 fragmentShader() : SV_Target
        {
            return fixed4(1,1,1,1); // IS STUB!!!!
        }
#endif // SHADOWS_DEPTH
#endif // SPOT
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 55 for m_GpuProgramType: #5
        // m_BlobIndex: 9
        // m_ShaderHardwareTier: 0

#ifdef DIRECTIONAL
#ifdef SHADOWS_SCREEN

        //  m_ShaderRequirements: 
        fixed4 fragmentShader() : SV_Target
        {
            return fixed4(1,1,1,1); // IS STUB!!!!
        }
#endif // SHADOWS_SCREEN
#endif // DIRECTIONAL
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 64 for m_GpuProgramType: #5
        // m_BlobIndex: 42
        // m_ShaderHardwareTier: 0

#ifdef DIRECTIONAL_COOKIE
#ifdef SHADOWS_SCREEN

        //  m_ShaderRequirements: 
        fixed4 fragmentShader() : SV_Target
        {
            return fixed4(1,1,1,1); // IS STUB!!!!
        }
#endif // SHADOWS_SCREEN
#endif // DIRECTIONAL_COOKIE
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 73 for m_GpuProgramType: #5
        // m_BlobIndex: 43
        // m_ShaderHardwareTier: 0

#ifdef POINT
#ifdef SHADOWS_CUBE

        //  m_ShaderRequirements: 
        fixed4 fragmentShader() : SV_Target
        {
            return fixed4(1,1,1,1); // IS STUB!!!!
        }
#endif // SHADOWS_CUBE
#endif // POINT
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 82 for m_GpuProgramType: #5
        // m_BlobIndex: 45
        // m_ShaderHardwareTier: 0

#ifdef POINT_COOKIE
#ifdef SHADOWS_CUBE

        //  m_ShaderRequirements: 
        fixed4 fragmentShader() : SV_Target
        {
            return fixed4(1,1,1,1); // IS STUB!!!!
        }
#endif // SHADOWS_CUBE
#endif // POINT_COOKIE
        //
        // ++++++++++++++++

        //
        // ======================


        ENDCG
      } // end phase
      Pass // ind: 3, name: SHADOWCASTER
      {
        Name "SHADOWCASTER"
        Tags
        { 
        "LIGHTMODE" = "SHADOWCASTER"
        "PerformanceChecks" = "False"
        "RenderType" = "Opaque"
        "SHADOWSUPPORT" = "true"
        }
        LOD 150
        // m_ProgramMask = 6
        CGPROGRAM
#pragma target 3.0
#pragma vertex vertexShader
#pragma fragment fragmentShader
#pragma multi_compile SHADOWS_DEPTH SHADOWS_CUBE

        // ======================
        // SerializedProgram: progVertex

        // ++++++++++++++++
        // Sub Program: 1 for m_GpuProgramType: #5
        // m_BlobIndex: 47
        // m_ShaderHardwareTier: 0

#ifdef SHADOWS_DEPTH

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // SHADOWS_DEPTH
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 10 for m_GpuProgramType: #5
        // m_BlobIndex: 48
        // m_ShaderHardwareTier: 0

#ifdef SHADOWS_CUBE

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // SHADOWS_CUBE
        //
        // ++++++++++++++++

        //
        // ======================


        // ======================
        // SerializedProgram: progFragment

        // ++++++++++++++++
        // Sub Program: 1 for m_GpuProgramType: #5
        // m_BlobIndex: 49
        // m_ShaderHardwareTier: 0

#ifdef SHADOWS_DEPTH

        //  m_ShaderRequirements: 
        fixed4 fragmentShader() : SV_Target
        {
            return fixed4(1,1,1,1); // IS STUB!!!!
        }
#endif // SHADOWS_DEPTH
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 10 for m_GpuProgramType: #5
        // m_BlobIndex: 50
        // m_ShaderHardwareTier: 0

#ifdef SHADOWS_CUBE

        //  m_ShaderRequirements: 
        fixed4 fragmentShader() : SV_Target
        {
            return fixed4(1,1,1,1); // IS STUB!!!!
        }
#endif // SHADOWS_CUBE
        //
        // ++++++++++++++++

        //
        // ======================


        ENDCG
      } // end phase
      Pass // ind: 4, name: META
      {
        Name "META"
        Tags
        { 
        "LIGHTMODE" = "META"
        "PerformanceChecks" = "False"
        "RenderType" = "Opaque"
        }
        LOD 150
        // m_ProgramMask = 6
        CGPROGRAM
#pragma target 3.0
#pragma vertex vertexShader
#pragma fragment fragmentShader

        // ======================
        // SerializedProgram: progVertex

        // ++++++++++++++++
        // Sub Program: 1 for m_GpuProgramType: #5
        // m_BlobIndex: 56
        // m_ShaderHardwareTier: 0


        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 4 for m_GpuProgramType: #4
        // m_BlobIndex: 55
        // m_ShaderHardwareTier: 0


        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
        //
        // ++++++++++++++++

        //
        // ======================


        // ======================
        // SerializedProgram: progFragment

        // ++++++++++++++++
        // Sub Program: 1 for m_GpuProgramType: #5
        // m_BlobIndex: 54
        // m_ShaderHardwareTier: 0


        //  m_ShaderRequirements: 
        fixed4 fragmentShader() : SV_Target
        {
            return fixed4(1,1,1,1); // IS STUB!!!!
        }
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 4 for m_GpuProgramType: #4
        // m_BlobIndex: 53
        // m_ShaderHardwareTier: 0


        //  m_ShaderRequirements: 
        fixed4 fragmentShader() : SV_Target
        {
            return fixed4(1,1,1,1); // IS STUB!!!!
        }
        //
        // ++++++++++++++++

        //
        // ======================


        ENDCG
      } // end phase
  }
  FallBack "VertexLit"
  /* Disabemble: 
   https://blogs.unity3d.com/ru/2015/08/27/plans-for-graphics-features-deprecation/


 Block#0 Platform: 5 raw_size: 313744
4500000020360400041A0000E09D0100041D0000743F0300F41C000000CF01005C230000B83B00004021000050000400A8240000908004002C240000685C0300FC2A0000448C00004C0000000466040060000000F8240400281100001427020038140000CCBA0400C40E000090C802008C1100006C300100741200002450040088150000B8A20000881100008CA8030098140000A06202003C100000EC4E02004C130000BCA404009015000080C70000FC1B0000E8CD0300D8190000741B000044200000B087030010120000C4F2010000180000588E0200641300002C0200004819000070570100B8130000646604002C1A0000C0E7030090180000D00D0300D01E00002C78000018140000A45D0000881A0000286B0100F01800001CDA02002C1F0000E4BA0100480000004CBA040044000000648703004C000000F85C000054000000AC6504005800000038620200680000001C0D0300680000004C5D0000580000005CF2010068000000849D01005C000000240401006C000000C40A0200C8050000BCA102001C040000B0CE010050000000283F03004C0000002CBB01008413000048F90200D4130000901C0100DC13000090BA04003C000000840D03004C000000C0990300CC0E0000188401006C190000DC7202007C1B00007CE30000A8200000D8A50200B822000040B400004013000024BD0300C4100000E0420100901400004C3B0200A0130000A02C0300881200009004010000180000908C0000281600008C10020088160000324F040C050000003A0000000300000000000000000000000200000012000000444952454354494F4E414C5F434F4F4B494500000E000000534841444F57535F53435245454E0000DF1800002376657273696F6E203130300A0A236966646566205645525445580A6174747269627574652076656334205F676C65735665727465783B0A6174747269627574652076656333205F676C65734E6F726D616C3B0A6174747269627574652076656334205F676C65734D756C7469546578436F6F7264303B0A6174747269627574652076656334205F676C65734D756C7469546578436F6F7264313B0A756E69666F726D2068696768702076656333205F576F726C64537061636543616D657261506F733B0A756E69666F726D206D656469756D702076656334205F576F726C6453706163654C69676874506F73303B0A756E69666F726D206869676870206D61743420756E6974795F4F626A656374546F576F726C643B0A756E69666F726D206869676870206D61743420756E6974795F576F726C64546F4F626A6563743B0A756E69666F726D206869676870206D61743420756E6974795F4D617472697856503B0A756E69666F726D2068696768702076656334205F4D61696E5465785F53543B0A756E69666F726D2068696768702076656334205F44657461696C416C6265646F4D61705F53543B0A756E69666F726D206D656469756D7020666C6F6174205F55565365633B0A76617279696E67206869676870207665633420786C765F544558434F4F5244303B0A76617279696E67206D656469756D70207665633320786C765F544558434F4F5244313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244323B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244325F313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244325F323B0A76617279696E67206869676870207665633320786C765F544558434F4F5244353B0A76617279696E67206869676870207665633220786C765F544558434F4F5244363B0A766F6964206D61696E2028290A7B0A20206D656469756D70207665633320746D707661725F313B0A2020746D707661725F31203D205F676C65734E6F726D616C3B0A20206869676870207665633220746D707661725F323B0A2020746D707661725F32203D205F676C65734D756C7469546578436F6F7264302E78793B0A20206869676870207665633220746D707661725F333B0A2020746D707661725F33203D205F676C65734D756C7469546578436F6F7264312E78793B0A20206D656469756D70207665633420746D707661725F343B0A20206D656469756D70207665633420746D707661725F353B0A20206D656469756D70207665633420746D707661725F363B0A20206869676870207665633220746D707661725F373B0A20206869676870207665633420746D707661725F383B0A2020746D707661725F38203D2028756E6974795F4F626A656374546F576F726C64202A205F676C6573566572746578293B0A20206869676870207665633420746D707661725F393B0A20206869676870207665633420746D707661725F31303B0A2020746D707661725F31302E77203D20312E303B0A2020746D707661725F31302E78797A203D205F676C65735665727465782E78797A3B0A2020746D707661725F39203D2028756E6974795F4D61747269785650202A2028756E6974795F4F626A656374546F576F726C64202A20746D707661725F313029293B0A20206869676870207665633420746578636F6F72645F31313B0A2020746578636F6F72645F31312E7879203D2028285F676C65734D756C7469546578436F6F7264302E7879202A205F4D61696E5465785F53542E787929202B205F4D61696E5465785F53542E7A77293B0A20206869676870207665633220746D707661725F31323B0A202069662028285F5556536563203D3D20302E302929207B0A20202020746D707661725F3132203D20746D707661725F323B0A20207D20656C7365207B0A20202020746D707661725F3132203D20746D707661725F333B0A20207D3B0A2020746578636F6F72645F31312E7A77203D202828746D707661725F3132202A205F44657461696C416C6265646F4D61705F53542E787929202B205F44657461696C416C6265646F4D61705F53542E7A77293B0A20206D656469756D70207665633320746D707661725F31333B0A202068696768702076656333206E5F31343B0A20206E5F3134203D2028746D707661725F382E78797A202D205F576F726C64537061636543616D657261506F73293B0A2020746D707661725F3133203D206E5F31343B0A202068696768702076656333206E6F726D5F31353B0A20206E6F726D5F3135203D20746D707661725F313B0A20206869676870206D61743320746D707661725F31363B0A2020746D707661725F31365B305D203D20756E6974795F576F726C64546F4F626A6563745B305D2E78797A3B0A2020746D707661725F31365B315D203D20756E6974795F576F726C64546F4F626A6563745B315D2E78797A3B0A2020746D707661725F31365B325D203D20756E6974795F576F726C64546F4F626A6563745B325D2E78797A3B0A20206869676870207665633320746D707661725F31373B0A2020746D707661725F3137203D206E6F726D616C697A6528286E6F726D5F3135202A20746D707661725F313629293B0A2020746D707661725F342E78797A203D207665633328302E302C20302E302C20302E30293B0A2020746D707661725F352E78797A203D207665633328302E302C20302E302C20302E30293B0A2020746D707661725F362E78797A203D20746D707661725F31373B0A20206869676870207665633320746D707661725F31383B0A2020746D707661725F3138203D20285F576F726C6453706163654C69676874506F73302E78797A202D2028746D707661725F382E78797A202A205F576F726C6453706163654C69676874506F73302E7729293B0A2020746D707661725F342E77203D20746D707661725F31382E783B0A2020746D707661725F352E77203D20746D707661725F31382E793B0A2020746D707661725F362E77203D20746D707661725F31382E7A3B0A2020676C5F506F736974696F6E203D20746D707661725F393B0A2020786C765F544558434F4F524430203D20746578636F6F72645F31313B0A2020786C765F544558434F4F524431203D20746D707661725F31333B0A2020786C765F544558434F4F524432203D20746D707661725F343B0A2020786C765F544558434F4F5244325F31203D20746D707661725F353B0A2020786C765F544558434F4F5244325F32203D20746D707661725F363B0A2020786C765F544558434F4F524435203D20746D707661725F382E78797A3B0A2020786C765F544558434F4F524436203D20746D707661725F373B0A7D0A0A0A23656E6469660A23696664656620465241474D454E540A756E69666F726D2068696768702076656333205F576F726C64537061636543616D657261506F733B0A756E69666F726D206869676870206D61743420756E6974795F576F726C64546F536861646F775B345D3B0A756E69666F726D206D656469756D702076656334205F4C69676874536861646F77446174613B0A756E69666F726D206869676870207665633420756E6974795F536861646F774661646543656E746572416E64547970653B0A756E69666F726D206869676870206D61743420756E6974795F4D6174726978563B0A756E69666F726D206C6F77702076656334205F4C69676874436F6C6F72303B0A756E69666F726D206D656469756D702076656334205F436F6C6F723B0A756E69666F726D2073616D706C65723244205F4D61696E5465783B0A756E69666F726D206D656469756D7020666C6F6174205F4D6574616C6C69633B0A756E69666F726D206D656469756D7020666C6F6174205F476C6F7373696E6573733B0A756E69666F726D2068696768702073616D706C65723244205F536861646F774D6170546578747572653B0A756E69666F726D2073616D706C65723244205F4C6967687454657874757265303B0A756E69666F726D206869676870206D61743420756E6974795F576F726C64546F4C696768743B0A76617279696E67206869676870207665633420786C765F544558434F4F5244303B0A76617279696E67206D656469756D70207665633320786C765F544558434F4F5244313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244323B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244325F313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244325F323B0A76617279696E67206869676870207665633320786C765F544558434F4F5244353B0A766F6964206D61696E2028290A7B0A20206C6F777020666C6F617420736861646F775F313B0A20206C6F7770207665633420746D707661725F323B0A2020746D707661725F32203D2074657874757265324420285F4D61696E5465782C20786C765F544558434F4F5244302E7879293B0A20206D656469756D70207665633320746D707661725F333B0A2020746D707661725F33203D20285F436F6C6F722E78797A202A20746D707661725F322E78797A293B0A20206D656469756D70207665633320746D707661725F343B0A2020746D707661725F34203D206E6F726D616C697A6528786C765F544558434F4F5244325F322E78797A293B0A20206869676870207665633420746D707661725F353B0A2020746D707661725F352E77203D20312E303B0A2020746D707661725F352E78797A203D20786C765F544558434F4F5244353B0A20206869676870207665633220746D707661725F363B0A2020746D707661725F36203D2028756E6974795F576F726C64546F4C69676874202A20746D707661725F35292E78793B0A20206D656469756D7020666C6F6174207265616C74696D65536861646F77417474656E756174696F6E5F373B0A20206869676870207665633420765F383B0A2020765F382E78203D20756E6974795F4D6174726978565B305D2E7A3B0A2020765F382E79203D20756E6974795F4D6174726978565B315D2E7A3B0A2020765F382E7A203D20756E6974795F4D6174726978565B325D2E7A3B0A2020765F382E77203D20756E6974795F4D6174726978565B335D2E7A3B0A20206869676870207665633320746D707661725F393B0A2020746D707661725F39203D2028786C765F544558434F4F524435202D20756E6974795F536861646F774661646543656E746572416E64547970652E78797A293B0A20206D656469756D7020666C6F617420746D707661725F31303B0A2020686967687020666C6F617420746D707661725F31313B0A2020746D707661725F3131203D20636C616D70202828280A202020206D69782028646F742028285F576F726C64537061636543616D657261506F73202D20786C765F544558434F4F524435292C20765F382E78797A292C207371727428646F742028746D707661725F392C20746D707661725F3929292C20756E6974795F536861646F774661646543656E746572416E64547970652E77290A2020202A205F4C69676874536861646F77446174612E7A29202B205F4C69676874536861646F77446174612E77292C20302E302C20312E30293B0A2020746D707661725F3130203D20746D707661725F31313B0A20206869676870207665633420746D707661725F31323B0A2020746D707661725F31322E77203D20312E303B0A2020746D707661725F31322E78797A203D20786C765F544558434F4F5244353B0A20206C6F777020666C6F617420746D707661725F31333B0A20206869676870207665633420736861646F77436F6F72645F31343B0A2020736861646F77436F6F72645F3134203D2028756E6974795F576F726C64546F536861646F775B305D202A20746D707661725F3132293B0A2020686967687020666C6F6174206C69676874536861646F7744617461585F31353B0A20206D656469756D7020666C6F617420746D707661725F31363B0A2020746D707661725F3136203D205F4C69676874536861646F77446174612E783B0A20206C69676874536861646F7744617461585F3135203D20746D707661725F31363B0A2020686967687020666C6F617420746D707661725F31373B0A2020746D707661725F3137203D206D61782028666C6F6174282874657874757265324420285F536861646F774D6170546578747572652C20736861646F77436F6F72645F31342E7879292E78203E20736861646F77436F6F72645F31342E7A29292C206C69676874536861646F7744617461585F3135293B0A2020746D707661725F3133203D20746D707661725F31373B0A20207265616C74696D65536861646F77417474656E756174696F6E5F37203D20746D707661725F31333B0A20206D656469756D7020666C6F617420746D707661725F31383B0A2020746D707661725F3138203D20636C616D702028287265616C74696D65536861646F77417474656E756174696F6E5F37202B20746D707661725F3130292C20302E302C20312E30293B0A2020736861646F775F31203D20746D707661725F31383B0A20206C6F777020666C6F617420746D707661725F31393B0A2020746D707661725F3139203D202874657874757265324420285F4C6967687454657874757265302C20746D707661725F36292E77202A20736861646F775F31293B0A20206D656469756D70207665633320746D707661725F32303B0A2020746D707661725F32302E78203D20786C765F544558434F4F5244322E773B0A2020746D707661725F32302E79203D20786C765F544558434F4F5244325F312E773B0A2020746D707661725F32302E7A203D20786C765F544558434F4F5244325F322E773B0A20206D656469756D7020666C6F617420617474656E5F32313B0A2020617474656E5F3231203D20746D707661725F31393B0A20206D656469756D70207665633320746D707661725F32323B0A2020746D707661725F3232203D205F4C69676874436F6C6F72302E78797A3B0A2020746D707661725F3232203D2028746D707661725F3232202A20617474656E5F3231293B0A20206D656469756D7020666C6F61742073706563756C61725465726D5F32333B0A20206D656469756D70207665633320746D707661725F32343B0A20206D656469756D70207665633320696E5665635F32353B0A2020696E5665635F3235203D2028746D707661725F3230202D206E6F726D616C697A6528786C765F544558434F4F52443129293B0A2020746D707661725F3234203D2028696E5665635F3235202A20696E766572736573717274286D61782028302E3030312C200A20202020646F742028696E5665635F32352C20696E5665635F3235290A20202929293B0A20206D656469756D7020666C6F617420746D707661725F32363B0A2020746D707661725F3236203D20636C616D702028646F742028746D707661725F342C20746D707661725F3234292C20302E302C20312E30293B0A20206D656469756D7020666C6F617420746D707661725F32373B0A2020746D707661725F3237203D2028312E30202D205F476C6F7373696E657373293B0A20206D656469756D7020666C6F617420746D707661725F32383B0A2020746D707661725F3238203D2028746D707661725F3237202A20746D707661725F3237293B0A202073706563756C61725465726D5F3233203D202828746D707661725F3238202F20280A20202020286D61782028302E33322C20636C616D702028646F742028746D707661725F32302C20746D707661725F3234292C20302E302C20312E302929202A2028312E35202B20746D707661725F323829290A2020202A200A20202020282828746D707661725F3236202A20746D707661725F323629202A202828746D707661725F3238202A20746D707661725F323829202D20312E302929202B20312E3030303031290A20202929202D20302E30303031293B0A20206D656469756D7020666C6F617420746D707661725F32393B0A2020746D707661725F3239203D20636C616D70202873706563756C61725465726D5F32332C20302E302C203130302E30293B0A202073706563756C61725465726D5F3233203D20746D707661725F32393B0A20206D656469756D70207665633420746D707661725F33303B0A2020746D707661725F33302E77203D20312E303B0A2020746D707661725F33302E78797A203D202828280A2020202028746D707661725F33202A2028302E37373930383337202D20285F4D6574616C6C6963202A20302E373739303833372929290A2020202B200A2020202028746D707661725F3239202A206D697820287665633328302E323230393136332C20302E323230393136332C20302E32323039313633292C20746D707661725F332C2076656333285F4D6574616C6C69632929290A202029202A20746D707661725F323229202A20636C616D702028646F742028746D707661725F342C20746D707661725F3230292C20302E302C20312E3029293B0A20206D656469756D70207665633420786C61745F7661726F75747075745F33313B0A2020786C61745F7661726F75747075745F33312E78797A203D20746D707661725F33302E78797A3B0A2020786C61745F7661726F75747075745F33312E77203D20312E303B0A2020676C5F46726167446174615B305D203D20786C61745F7661726F75747075745F33313B0A7D0A0A0A23656E6469660A001B000000000000000100000000000000000000000000000000000000324F040C050000004B000000070000000400000000000000030000000400000053504F540D000000534841444F57535F44455054480000000C000000534841444F57535F534F4654DB1F00002376657273696F6E203130300A0A236966646566205645525445580A6174747269627574652076656334205F676C65735665727465783B0A6174747269627574652076656333205F676C65734E6F726D616C3B0A6174747269627574652076656334205F676C65734D756C7469546578436F6F7264303B0A6174747269627574652076656334205F676C65734D756C7469546578436F6F7264313B0A756E69666F726D2068696768702076656333205F576F726C64537061636543616D657261506F733B0A756E69666F726D2068696768702076656334205F576F726C6453706163654C69676874506F73303B0A756E69666F726D206869676870206D61743420756E6974795F4F626A656374546F576F726C643B0A756E69666F726D206869676870206D61743420756E6974795F576F726C64546F4F626A6563743B0A756E69666F726D206869676870206D61743420756E6974795F4D617472697856503B0A756E69666F726D2068696768702076656334205F4D61696E5465785F53543B0A756E69666F726D2068696768702076656334205F44657461696C416C6265646F4D61705F53543B0A756E69666F726D206D656469756D7020666C6F6174205F55565365633B0A76617279696E67206869676870207665633420786C765F544558434F4F5244303B0A76617279696E67206D656469756D70207665633320786C765F544558434F4F5244313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244323B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244325F313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244325F323B0A76617279696E67206869676870207665633320786C765F544558434F4F5244353B0A76617279696E67206869676870207665633220786C765F544558434F4F5244363B0A766F6964206D61696E2028290A7B0A20206D656469756D70207665633320746D707661725F313B0A2020746D707661725F31203D205F676C65734E6F726D616C3B0A20206869676870207665633220746D707661725F323B0A2020746D707661725F32203D205F676C65734D756C7469546578436F6F7264302E78793B0A20206869676870207665633220746D707661725F333B0A2020746D707661725F33203D205F676C65734D756C7469546578436F6F7264312E78793B0A202068696768702076656333206C696768744469725F343B0A20206D656469756D70207665633420746D707661725F353B0A20206D656469756D70207665633420746D707661725F363B0A20206D656469756D70207665633420746D707661725F373B0A20206869676870207665633220746D707661725F383B0A20206869676870207665633420746D707661725F393B0A2020746D707661725F39203D2028756E6974795F4F626A656374546F576F726C64202A205F676C6573566572746578293B0A20206869676870207665633420746D707661725F31303B0A20206869676870207665633420746D707661725F31313B0A2020746D707661725F31312E77203D20312E303B0A2020746D707661725F31312E78797A203D205F676C65735665727465782E78797A3B0A2020746D707661725F3130203D2028756E6974795F4D61747269785650202A2028756E6974795F4F626A656374546F576F726C64202A20746D707661725F313129293B0A20206869676870207665633420746578636F6F72645F31323B0A2020746578636F6F72645F31322E7879203D2028285F676C65734D756C7469546578436F6F7264302E7879202A205F4D61696E5465785F53542E787929202B205F4D61696E5465785F53542E7A77293B0A20206869676870207665633220746D707661725F31333B0A202069662028285F5556536563203D3D20302E302929207B0A20202020746D707661725F3133203D20746D707661725F323B0A20207D20656C7365207B0A20202020746D707661725F3133203D20746D707661725F333B0A20207D3B0A2020746578636F6F72645F31322E7A77203D202828746D707661725F3133202A205F44657461696C416C6265646F4D61705F53542E787929202B205F44657461696C416C6265646F4D61705F53542E7A77293B0A20206D656469756D70207665633320746D707661725F31343B0A202068696768702076656333206E5F31353B0A20206E5F3135203D2028746D707661725F392E78797A202D205F576F726C64537061636543616D657261506F73293B0A2020746D707661725F3134203D206E5F31353B0A202068696768702076656333206E6F726D5F31363B0A20206E6F726D5F3136203D20746D707661725F313B0A20206869676870206D61743320746D707661725F31373B0A2020746D707661725F31375B305D203D20756E6974795F576F726C64546F4F626A6563745B305D2E78797A3B0A2020746D707661725F31375B315D203D20756E6974795F576F726C64546F4F626A6563745B315D2E78797A3B0A2020746D707661725F31375B325D203D20756E6974795F576F726C64546F4F626A6563745B325D2E78797A3B0A20206869676870207665633320746D707661725F31383B0A2020746D707661725F3138203D206E6F726D616C697A6528286E6F726D5F3136202A20746D707661725F313729293B0A2020746D707661725F352E78797A203D207665633328302E302C20302E302C20302E30293B0A2020746D707661725F362E78797A203D207665633328302E302C20302E302C20302E30293B0A2020746D707661725F372E78797A203D20746D707661725F31383B0A20206869676870207665633320746D707661725F31393B0A2020746D707661725F3139203D20285F576F726C6453706163654C69676874506F73302E78797A202D2028746D707661725F392E78797A202A205F576F726C6453706163654C69676874506F73302E7729293B0A20206D656469756D70207665633320746D707661725F32303B0A2020746D707661725F3230203D20746D707661725F31393B0A20206C696768744469725F34203D20746D707661725F32303B0A2020746D707661725F352E77203D206C696768744469725F342E783B0A2020746D707661725F362E77203D206C696768744469725F342E793B0A2020746D707661725F372E77203D206C696768744469725F342E7A3B0A2020676C5F506F736974696F6E203D20746D707661725F31303B0A2020786C765F544558434F4F524430203D20746578636F6F72645F31323B0A2020786C765F544558434F4F524431203D20746D707661725F31343B0A2020786C765F544558434F4F524432203D20746D707661725F353B0A2020786C765F544558434F4F5244325F31203D20746D707661725F363B0A2020786C765F544558434F4F5244325F32203D20746D707661725F373B0A2020786C765F544558434F4F524435203D20746D707661725F392E78797A3B0A2020786C765F544558434F4F524436203D20746D707661725F383B0A7D0A0A0A23656E6469660A23696664656620465241474D454E540A756E69666F726D2068696768702076656333205F576F726C64537061636543616D657261506F733B0A756E69666F726D206869676870206D61743420756E6974795F576F726C64546F536861646F775B345D3B0A756E69666F726D206D656469756D702076656334205F4C69676874536861646F77446174613B0A756E69666F726D206869676870207665633420756E6974795F536861646F774661646543656E746572416E64547970653B0A756E69666F726D206869676870206D61743420756E6974795F4D6174726978563B0A756E69666F726D206C6F77702076656334205F4C69676874436F6C6F72303B0A756E69666F726D2068696768702073616D706C65723244205F536861646F774D6170546578747572653B0A756E69666F726D2068696768702076656334205F536861646F774F6666736574735B345D3B0A756E69666F726D206D656469756D702076656334205F436F6C6F723B0A756E69666F726D2073616D706C65723244205F4D61696E5465783B0A756E69666F726D206D656469756D7020666C6F6174205F4D6574616C6C69633B0A756E69666F726D206D656469756D7020666C6F6174205F476C6F7373696E6573733B0A756E69666F726D2073616D706C65723244205F4C6967687454657874757265303B0A756E69666F726D206869676870206D61743420756E6974795F576F726C64546F4C696768743B0A756E69666F726D2073616D706C65723244205F4C696768745465787475726542303B0A76617279696E67206869676870207665633420786C765F544558434F4F5244303B0A76617279696E67206D656469756D70207665633320786C765F544558434F4F5244313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244323B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244325F313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244325F323B0A76617279696E67206869676870207665633320786C765F544558434F4F5244353B0A766F6964206D61696E2028290A7B0A20206C6F777020666C6F617420617474656E5F313B0A20206C6F777020666C6F617420736861646F775F323B0A20206C6F7770207665633420746D707661725F333B0A2020746D707661725F33203D2074657874757265324420285F4D61696E5465782C20786C765F544558434F4F5244302E7879293B0A20206D656469756D70207665633320746D707661725F343B0A2020746D707661725F34203D20285F436F6C6F722E78797A202A20746D707661725F332E78797A293B0A20206D656469756D70207665633320746D707661725F353B0A20206D656469756D70207665633320746D707661725F363B0A2020746D707661725F36203D206D697820287665633328302E323230393136332C20302E323230393136332C20302E32323039313633292C20746D707661725F342C2076656333285F4D6574616C6C696329293B0A2020746D707661725F35203D2028746D707661725F34202A2028302E37373930383337202D20285F4D6574616C6C6963202A20302E373739303833372929293B0A20206D656469756D70207665633320746D707661725F373B0A2020746D707661725F37203D206E6F726D616C697A6528786C765F544558434F4F5244325F322E78797A293B0A20206D656469756D70207665633320746D707661725F383B0A2020746D707661725F38203D206E6F726D616C697A6528786C765F544558434F4F524431293B0A20206869676870207665633420746D707661725F393B0A2020746D707661725F392E77203D20312E303B0A2020746D707661725F392E78797A203D20786C765F544558434F4F5244353B0A20206869676870207665633420746D707661725F31303B0A2020746D707661725F3130203D2028756E6974795F576F726C64546F4C69676874202A20746D707661725F39293B0A20206D656469756D7020666C6F6174207265616C74696D65536861646F77417474656E756174696F6E5F31313B0A20206869676870207665633420765F31323B0A2020765F31322E78203D20756E6974795F4D6174726978565B305D2E7A3B0A2020765F31322E79203D20756E6974795F4D6174726978565B315D2E7A3B0A2020765F31322E7A203D20756E6974795F4D6174726978565B325D2E7A3B0A2020765F31322E77203D20756E6974795F4D6174726978565B335D2E7A3B0A20206869676870207665633320746D707661725F31333B0A2020746D707661725F3133203D2028786C765F544558434F4F524435202D20756E6974795F536861646F774661646543656E746572416E64547970652E78797A293B0A20206D656469756D7020666C6F617420746D707661725F31343B0A2020686967687020666C6F617420746D707661725F31353B0A2020746D707661725F3135203D20636C616D70202828280A202020206D69782028646F742028285F576F726C64537061636543616D657261506F73202D20786C765F544558434F4F524435292C20765F31322E78797A292C207371727428646F742028746D707661725F31332C20746D707661725F313329292C20756E6974795F536861646F774661646543656E746572416E64547970652E77290A2020202A205F4C69676874536861646F77446174612E7A29202B205F4C69676874536861646F77446174612E77292C20302E302C20312E30293B0A2020746D707661725F3134203D20746D707661725F31353B0A20207265616C74696D65536861646F77417474656E756174696F6E5F3131203D20312E303B0A20206869676870207665633420746D707661725F31363B0A2020746D707661725F31362E77203D20312E303B0A2020746D707661725F31362E78797A203D20786C765F544558434F4F5244353B0A20206869676870207665633420746D707661725F31373B0A2020746D707661725F3137203D2028756E6974795F576F726C64546F536861646F775B305D202A20746D707661725F3136293B0A20206C6F777020666C6F617420746D707661725F31383B0A20206869676870207665633420736861646F7756616C735F31393B0A20206869676870207665633320746D707661725F32303B0A2020746D707661725F3230203D2028746D707661725F31372E78797A202F20746D707661725F31372E77293B0A2020736861646F7756616C735F31392E78203D2074657874757265324420285F536861646F774D6170546578747572652C2028746D707661725F32302E7879202B205F536861646F774F6666736574735B305D2E787929292E783B0A2020736861646F7756616C735F31392E79203D2074657874757265324420285F536861646F774D6170546578747572652C2028746D707661725F32302E7879202B205F536861646F774F6666736574735B315D2E787929292E783B0A2020736861646F7756616C735F31392E7A203D2074657874757265324420285F536861646F774D6170546578747572652C2028746D707661725F32302E7879202B205F536861646F774F6666736574735B325D2E787929292E783B0A2020736861646F7756616C735F31392E77203D2074657874757265324420285F536861646F774D6170546578747572652C2028746D707661725F32302E7879202B205F536861646F774F6666736574735B335D2E787929292E783B0A2020627665633420746D707661725F32313B0A2020746D707661725F3231203D206C6573735468616E2028736861646F7756616C735F31392C20746D707661725F32302E7A7A7A7A293B0A20206D656469756D70207665633420746D707661725F32323B0A2020746D707661725F3232203D205F4C69676874536861646F77446174612E787878783B0A20206D656469756D7020666C6F617420746D707661725F32333B0A202069662028746D707661725F32312E7829207B0A20202020746D707661725F3233203D20746D707661725F32322E783B0A20207D20656C7365207B0A20202020746D707661725F3233203D20312E303B0A20207D3B0A20206D656469756D7020666C6F617420746D707661725F32343B0A202069662028746D707661725F32312E7929207B0A20202020746D707661725F3234203D20746D707661725F32322E793B0A20207D20656C7365207B0A20202020746D707661725F3234203D20312E303B0A20207D3B0A20206D656469756D7020666C6F617420746D707661725F32353B0A202069662028746D707661725F32312E7A29207B0A20202020746D707661725F3235203D20746D707661725F32322E7A3B0A20207D20656C7365207B0A20202020746D707661725F3235203D20312E303B0A20207D3B0A20206D656469756D7020666C6F617420746D707661725F32363B0A202069662028746D707661725F32312E7729207B0A20202020746D707661725F3236203D20746D707661725F32322E773B0A20207D20656C7365207B0A20202020746D707661725F3236203D20312E303B0A20207D3B0A20206D656469756D70207665633420746D707661725F32373B0A2020746D707661725F32372E78203D20746D707661725F32333B0A2020746D707661725F32372E79203D20746D707661725F32343B0A2020746D707661725F32372E7A203D20746D707661725F32353B0A2020746D707661725F32372E77203D20746D707661725F32363B0A20206D656469756D7020666C6F617420746D707661725F32383B0A2020746D707661725F3238203D20646F742028746D707661725F32372C207665633428302E32352C20302E32352C20302E32352C20302E323529293B0A2020746D707661725F3138203D20746D707661725F32383B0A20207265616C74696D65536861646F77417474656E756174696F6E5F3131203D20746D707661725F31383B0A20206D656469756D7020666C6F617420746D707661725F32393B0A2020746D707661725F3239203D20636C616D702028287265616C74696D65536861646F77417474656E756174696F6E5F3131202B20746D707661725F3134292C20302E302C20312E30293B0A2020736861646F775F32203D20746D707661725F32393B0A20206C6F7770207665633420746D707661725F33303B0A20206869676870207665633220505F33313B0A2020505F3331203D202828746D707661725F31302E7879202F20746D707661725F31302E7729202B20302E35293B0A2020746D707661725F3330203D2074657874757265324420285F4C6967687454657874757265302C20505F3331293B0A2020686967687020666C6F617420746D707661725F33323B0A2020746D707661725F3332203D20646F742028746D707661725F31302E78797A2C20746D707661725F31302E78797A293B0A20206C6F7770207665633420746D707661725F33333B0A2020746D707661725F3333203D2074657874757265324420285F4C696768745465787475726542302C207665633228746D707661725F333229293B0A2020686967687020666C6F617420746D707661725F33343B0A2020746D707661725F3334203D202828280A20202020666C6F61742828746D707661725F31302E7A203E20302E3029290A2020202A20746D707661725F33302E7729202A20746D707661725F33332E7729202A20736861646F775F32293B0A2020617474656E5F31203D20746D707661725F33343B0A20206D656469756D70207665633320746D707661725F33353B0A2020746D707661725F33352E78203D20786C765F544558434F4F5244322E773B0A2020746D707661725F33352E79203D20786C765F544558434F4F5244325F312E773B0A2020746D707661725F33352E7A203D20786C765F544558434F4F5244325F322E773B0A20206D656469756D7020666C6F617420617474656E5F33363B0A2020617474656E5F3336203D20617474656E5F313B0A20206D656469756D70207665633320746D707661725F33373B0A2020746D707661725F3337203D205F4C69676874436F6C6F72302E78797A3B0A20206D656469756D70207665633320746D707661725F33383B0A2020746D707661725F3338203D206E6F726D616C697A6528746D707661725F3335293B0A2020746D707661725F3337203D2028746D707661725F3337202A20617474656E5F3336293B0A20206D656469756D7020666C6F61742073706563756C61725465726D5F33393B0A20206D656469756D70207665633320746D707661725F34303B0A20206D656469756D70207665633320696E5665635F34313B0A2020696E5665635F3431203D2028746D707661725F3338202D20746D707661725F38293B0A2020746D707661725F3430203D2028696E5665635F3431202A20696E766572736573717274286D61782028302E3030312C200A20202020646F742028696E5665635F34312C20696E5665635F3431290A20202929293B0A20206D656469756D7020666C6F617420746D707661725F34323B0A2020746D707661725F3432203D20636C616D702028646F742028746D707661725F372C20746D707661725F3430292C20302E302C20312E30293B0A20206D656469756D7020666C6F617420746D707661725F34333B0A2020746D707661725F3433203D2028312E30202D205F476C6F7373696E657373293B0A20206D656469756D7020666C6F617420746D707661725F34343B0A2020746D707661725F3434203D2028746D707661725F3433202A20746D707661725F3433293B0A202073706563756C61725465726D5F3339203D202828746D707661725F3434202F20280A20202020286D61782028302E33322C20636C616D702028646F742028746D707661725F33382C20746D707661725F3430292C20302E302C20312E302929202A2028312E35202B20746D707661725F343429290A2020202A200A20202020282828746D707661725F3432202A20746D707661725F343229202A202828746D707661725F3434202A20746D707661725F343429202D20312E302929202B20312E3030303031290A20202929202D20302E30303031293B0A20206D656469756D7020666C6F617420746D707661725F34353B0A2020746D707661725F3435203D20636C616D70202873706563756C61725465726D5F33392C20302E302C203130302E30293B0A202073706563756C61725465726D5F3339203D20746D707661725F34353B0A20206D656469756D70207665633420746D707661725F34363B0A2020746D707661725F34362E77203D20312E303B0A2020746D707661725F34362E78797A203D20282828746D707661725F35202B200A2020202028746D707661725F3435202A20746D707661725F36290A202029202A20746D707661725F333729202A20636C616D702028646F742028746D707661725F372C20746D707661725F3338292C20302E302C20312E3029293B0A20206D656469756D70207665633420786C61745F7661726F75747075745F34373B0A2020786C61745F7661726F75747075745F34372E78797A203D20746D707661725F34362E78797A3B0A2020786C61745F7661726F75747075745F34372E77203D20312E303B0A2020676C5F46726167446174615B305D203D20786C61745F7661726F75747075745F34373B0A7D0A0A0A23656E6469660A001B000000000000000100000000000000000000000000000000000000324F040C0500000026000000040000000000000000000000020000000B000000444952454354494F4E414C000E0000005645525445584C494748545F4F4E0000DD2000002376657273696F6E203130300A0A236966646566205645525445580A6174747269627574652076656334205F676C65735665727465783B0A6174747269627574652076656333205F676C65734E6F726D616C3B0A6174747269627574652076656334205F676C65734D756C7469546578436F6F7264303B0A6174747269627574652076656334205F676C65734D756C7469546578436F6F7264313B0A756E69666F726D2068696768702076656333205F576F726C64537061636543616D657261506F733B0A756E69666F726D206869676870207665633420756E6974795F344C69676874506F7358303B0A756E69666F726D206869676870207665633420756E6974795F344C69676874506F7359303B0A756E69666F726D206869676870207665633420756E6974795F344C69676874506F735A303B0A756E69666F726D206D656469756D70207665633420756E6974795F344C69676874417474656E303B0A756E69666F726D206D656469756D70207665633420756E6974795F4C69676874436F6C6F725B385D3B0A756E69666F726D206D656469756D70207665633420756E6974795F534841723B0A756E69666F726D206D656469756D70207665633420756E6974795F534841673B0A756E69666F726D206D656469756D70207665633420756E6974795F534841623B0A756E69666F726D206D656469756D70207665633420756E6974795F534842723B0A756E69666F726D206D656469756D70207665633420756E6974795F534842673B0A756E69666F726D206D656469756D70207665633420756E6974795F534842623B0A756E69666F726D206D656469756D70207665633420756E6974795F5348433B0A756E69666F726D206869676870206D61743420756E6974795F4F626A656374546F576F726C643B0A756E69666F726D206869676870206D61743420756E6974795F576F726C64546F4F626A6563743B0A756E69666F726D206869676870206D61743420756E6974795F4D617472697856503B0A756E69666F726D2068696768702076656334205F4D61696E5465785F53543B0A756E69666F726D2068696768702076656334205F44657461696C416C6265646F4D61705F53543B0A756E69666F726D206D656469756D7020666C6F6174205F4D6574616C6C69633B0A756E69666F726D206D656469756D7020666C6F6174205F476C6F7373696E6573733B0A756E69666F726D206D656469756D7020666C6F6174205F55565365633B0A76617279696E67206869676870207665633420786C765F544558434F4F5244303B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244323B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244343B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244353B0A766F6964206D61696E2028290A7B0A20206D656469756D70207665633320746D707661725F313B0A2020746D707661725F31203D205F676C65734E6F726D616C3B0A20206869676870207665633220746D707661725F323B0A2020746D707661725F32203D205F676C65734D756C7469546578436F6F7264302E78793B0A20206869676870207665633220746D707661725F333B0A2020746D707661725F33203D205F676C65734D756C7469546578436F6F7264312E78793B0A20206D656469756D702076656333206E6F726D616C576F726C645F343B0A20206D656469756D702076656333206579655665635F353B0A20206D656469756D70207665633420746D707661725F363B0A20206D656469756D70207665633420746D707661725F373B0A20206D656469756D70207665633420746D707661725F383B0A20206869676870207665633420746D707661725F393B0A2020746D707661725F39203D2028756E6974795F4F626A656374546F576F726C64202A205F676C6573566572746578293B0A20206869676870207665633420746D707661725F31303B0A20206869676870207665633420746D707661725F31313B0A2020746D707661725F31312E77203D20312E303B0A2020746D707661725F31312E78797A203D205F676C65735665727465782E78797A3B0A2020746D707661725F3130203D2028756E6974795F4D61747269785650202A2028756E6974795F4F626A656374546F576F726C64202A20746D707661725F313129293B0A20206869676870207665633420746578636F6F72645F31323B0A2020746578636F6F72645F31322E7879203D2028285F676C65734D756C7469546578436F6F7264302E7879202A205F4D61696E5465785F53542E787929202B205F4D61696E5465785F53542E7A77293B0A20206869676870207665633220746D707661725F31333B0A202069662028285F5556536563203D3D20302E302929207B0A20202020746D707661725F3133203D20746D707661725F323B0A20207D20656C7365207B0A20202020746D707661725F3133203D20746D707661725F333B0A20207D3B0A2020746578636F6F72645F31322E7A77203D202828746D707661725F3133202A205F44657461696C416C6265646F4D61705F53542E787929202B205F44657461696C416C6265646F4D61705F53542E7A77293B0A20206869676870207665633320746D707661725F31343B0A2020746D707661725F3134203D206E6F726D616C697A652828746D707661725F392E78797A202D205F576F726C64537061636543616D657261506F7329293B0A20206579655665635F35203D20746D707661725F31343B0A202068696768702076656333206E6F726D5F31353B0A20206E6F726D5F3135203D20746D707661725F313B0A20206869676870206D61743320746D707661725F31363B0A2020746D707661725F31365B305D203D20756E6974795F576F726C64546F4F626A6563745B305D2E78797A3B0A2020746D707661725F31365B315D203D20756E6974795F576F726C64546F4F626A6563745B315D2E78797A3B0A2020746D707661725F31365B325D203D20756E6974795F576F726C64546F4F626A6563745B325D2E78797A3B0A20206869676870207665633320746D707661725F31373B0A2020746D707661725F3137203D206E6F726D616C697A6528286E6F726D5F3135202A20746D707661725F313629293B0A20206E6F726D616C576F726C645F34203D20746D707661725F31373B0A2020746D707661725F382E78797A203D206E6F726D616C576F726C645F343B0A2020746D707661725F362E78797A203D206579655665635F353B0A20206D656469756D70207665633420616D6269656E744F724C696768746D617055565F31383B0A2020616D6269656E744F724C696768746D617055565F31382E77203D20302E303B0A202068696768702076656333206C69676874436F6C6F72305F31393B0A20206C69676874436F6C6F72305F3139203D20756E6974795F4C69676874436F6C6F725B305D2E78797A3B0A202068696768702076656333206C69676874436F6C6F72315F32303B0A20206C69676874436F6C6F72315F3230203D20756E6974795F4C69676874436F6C6F725B315D2E78797A3B0A202068696768702076656333206C69676874436F6C6F72325F32313B0A20206C69676874436F6C6F72325F3231203D20756E6974795F4C69676874436F6C6F725B325D2E78797A3B0A202068696768702076656333206C69676874436F6C6F72335F32323B0A20206C69676874436F6C6F72335F3232203D20756E6974795F4C69676874436F6C6F725B335D2E78797A3B0A202068696768702076656334206C69676874417474656E53715F32333B0A20206C69676874417474656E53715F3233203D20756E6974795F344C69676874417474656E303B0A202068696768702076656333206E6F726D616C5F32343B0A20206E6F726D616C5F3234203D206E6F726D616C576F726C645F343B0A20206869676870207665633320636F6C5F32353B0A202068696768702076656334206E646F746C5F32363B0A202068696768702076656334206C656E67746853715F32373B0A20206869676870207665633420746D707661725F32383B0A2020746D707661725F3238203D2028756E6974795F344C69676874506F735830202D20746D707661725F392E78293B0A20206869676870207665633420746D707661725F32393B0A2020746D707661725F3239203D2028756E6974795F344C69676874506F735930202D20746D707661725F392E79293B0A20206869676870207665633420746D707661725F33303B0A2020746D707661725F3330203D2028756E6974795F344C69676874506F735A30202D20746D707661725F392E7A293B0A20206C656E67746853715F3237203D2028746D707661725F3238202A20746D707661725F3238293B0A20206C656E67746853715F3237203D20286C656E67746853715F3237202B2028746D707661725F3239202A20746D707661725F323929293B0A20206C656E67746853715F3237203D20286C656E67746853715F3237202B2028746D707661725F3330202A20746D707661725F333029293B0A20206869676870207665633420746D707661725F33313B0A2020746D707661725F3331203D206D617820286C656E67746853715F32372C20766563342831652D30362C2031652D30362C2031652D30362C2031652D303629293B0A20206C656E67746853715F3237203D20746D707661725F33313B0A20206E646F746C5F3236203D2028746D707661725F3238202A206E6F726D616C5F32342E78293B0A20206E646F746C5F3236203D20286E646F746C5F3236202B2028746D707661725F3239202A206E6F726D616C5F32342E7929293B0A20206E646F746C5F3236203D20286E646F746C5F3236202B2028746D707661725F3330202A206E6F726D616C5F32342E7A29293B0A20206869676870207665633420746D707661725F33323B0A2020746D707661725F3332203D206D617820287665633428302E302C20302E302C20302E302C20302E30292C20286E646F746C5F3236202A20696E76657273657371727428746D707661725F33312929293B0A20206E646F746C5F3236203D20746D707661725F33323B0A20206869676870207665633420746D707661725F33333B0A2020746D707661725F3333203D2028746D707661725F3332202A2028312E302F2828312E30202B200A2020202028746D707661725F3331202A206C69676874417474656E53715F3233290A2020292929293B0A2020636F6C5F3235203D20286C69676874436F6C6F72305F3139202A20746D707661725F33332E78293B0A2020636F6C5F3235203D2028636F6C5F3235202B20286C69676874436F6C6F72315F3230202A20746D707661725F33332E7929293B0A2020636F6C5F3235203D2028636F6C5F3235202B20286C69676874436F6C6F72325F3231202A20746D707661725F33332E7A29293B0A2020636F6C5F3235203D2028636F6C5F3235202B20286C69676874436F6C6F72335F3232202A20746D707661725F33332E7729293B0A2020616D6269656E744F724C696768746D617055565F31382E78797A203D20636F6C5F32353B0A20206D656469756D70207665633420746D707661725F33343B0A2020746D707661725F33342E77203D20312E303B0A2020746D707661725F33342E78797A203D206E6F726D616C576F726C645F343B0A20206D656469756D702076656333207265735F33353B0A20206D656469756D70207665633320785F33363B0A2020785F33362E78203D20646F742028756E6974795F534841722C20746D707661725F3334293B0A2020785F33362E79203D20646F742028756E6974795F534841672C20746D707661725F3334293B0A2020785F33362E7A203D20646F742028756E6974795F534841622C20746D707661725F3334293B0A20206D656469756D7020766563332078315F33373B0A20206D656469756D70207665633420746D707661725F33383B0A2020746D707661725F3338203D20286E6F726D616C576F726C645F342E78797A7A202A206E6F726D616C576F726C645F342E797A7A78293B0A202078315F33372E78203D20646F742028756E6974795F534842722C20746D707661725F3338293B0A202078315F33372E79203D20646F742028756E6974795F534842672C20746D707661725F3338293B0A202078315F33372E7A203D20646F742028756E6974795F534842622C20746D707661725F3338293B0A20207265735F3335203D2028785F3336202B202878315F3337202B2028756E6974795F5348432E78797A202A200A2020202028286E6F726D616C576F726C645F342E78202A206E6F726D616C576F726C645F342E7829202D20286E6F726D616C576F726C645F342E79202A206E6F726D616C576F726C645F342E7929290A20202929293B0A20206D656469756D70207665633320746D707661725F33393B0A2020746D707661725F3339203D206D617820282828312E303535202A200A20202020706F7720286D617820287265735F33352C207665633328302E302C20302E302C20302E3029292C207665633328302E343136363636372C20302E343136363636372C20302E3431363636363729290A202029202D20302E303535292C207665633328302E302C20302E302C20302E3029293B0A20207265735F3335203D20746D707661725F33393B0A2020616D6269656E744F724C696768746D617055565F31382E78797A203D2028616D6269656E744F724C696768746D617055565F31382E78797A202B206D617820287665633328302E302C20302E302C20302E30292C20746D707661725F333929293B0A2020746D707661725F372E797A77203D20286579655665635F35202D2028322E30202A20280A20202020646F7420286E6F726D616C576F726C645F342C206579655665635F35290A2020202A206E6F726D616C576F726C645F342929293B0A20206D656469756D7020666C6F617420785F34303B0A2020785F3430203D2028312E30202D20636C616D702028646F7420286E6F726D616C576F726C645F342C200A202020202D286579655665635F35290A2020292C20302E302C20312E3029293B0A2020746D707661725F382E77203D202828785F3430202A20785F343029202A2028785F3430202A20785F343029293B0A2020746D707661725F362E77203D20636C616D702028285F476C6F7373696E657373202B2028312E30202D200A2020202028302E37373930383337202D20285F4D6574616C6C6963202A20302E3737393038333729290A202029292C20302E302C20312E30293B0A2020676C5F506F736974696F6E203D20746D707661725F31303B0A2020786C765F544558434F4F524430203D20746578636F6F72645F31323B0A2020786C765F544558434F4F524431203D20746D707661725F363B0A2020786C765F544558434F4F524432203D20616D6269656E744F724C696768746D617055565F31383B0A2020786C765F544558434F4F524434203D20746D707661725F373B0A2020786C765F544558434F4F524435203D20746D707661725F383B0A7D0A0A0A23656E6469660A23696664656620465241474D454E540A23657874656E73696F6E20474C5F4558545F7368616465725F746578747572655F6C6F64203A20656E61626C650A6C6F7770207665633420696D706C5F6C6F775F74657874757265437562654C6F64455854286C6F77702073616D706C6572437562652073616D706C65722C206869676870207665633320636F6F72642C206D656469756D7020666C6F6174206C6F64290A7B0A23696620646566696E656428474C5F4558545F7368616465725F746578747572655F6C6F64290A0972657475726E2074657874757265437562654C6F644558542873616D706C65722C20636F6F72642C206C6F64293B0A23656C73650A0972657475726E2074657874757265437562652873616D706C65722C20636F6F72642C206C6F64293B0A23656E6469660A7D0A0A756E69666F726D206D656469756D702076656334205F576F726C6453706163654C69676874506F73303B0A756E69666F726D206C6F77702073616D706C65724375626520756E6974795F5370656343756265303B0A756E69666F726D206D656469756D70207665633420756E6974795F5370656343756265305F4844523B0A756E69666F726D206C6F77702076656334205F4C69676874436F6C6F72303B0A756E69666F726D2073616D706C6572324420756E6974795F4E4878526F7567686E6573733B0A756E69666F726D206D656469756D702076656334205F436F6C6F723B0A756E69666F726D2073616D706C65723244205F4D61696E5465783B0A756E69666F726D206D656469756D7020666C6F6174205F4D6574616C6C69633B0A756E69666F726D206D656469756D7020666C6F6174205F476C6F7373696E6573733B0A756E69666F726D2073616D706C65723244205F4F63636C7573696F6E4D61703B0A756E69666F726D206D656469756D7020666C6F6174205F4F63636C7573696F6E537472656E6774683B0A76617279696E67206869676870207665633420786C765F544558434F4F5244303B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244323B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244343B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244353B0A766F6964206D61696E2028290A7B0A20206D656469756D7020666C6F617420726C5F313B0A20206C6F7770207665633420746D707661725F323B0A2020746D707661725F32203D2074657874757265324420285F4D61696E5465782C20786C765F544558434F4F5244302E7879293B0A20206D656469756D70207665633320746D707661725F333B0A2020746D707661725F33203D20285F436F6C6F722E78797A202A20746D707661725F322E78797A293B0A20206D656469756D70207665633320746D707661725F343B0A20206D656469756D70207665633320746D707661725F353B0A2020746D707661725F35203D206D697820287665633328302E323230393136332C20302E323230393136332C20302E32323039313633292C20746D707661725F332C2076656333285F4D6574616C6C696329293B0A2020746D707661725F34203D2028746D707661725F33202A2028302E37373930383337202D20285F4D6574616C6C6963202A20302E373739303833372929293B0A20206D656469756D70207665633320746D707661725F363B0A2020746D707661725F36203D205F4C69676874436F6C6F72302E78797A3B0A20206D656469756D7020666C6F6174206F63635F373B0A20206C6F777020666C6F617420746D707661725F383B0A2020746D707661725F38203D2074657874757265324420285F4F63636C7573696F6E4D61702C20786C765F544558434F4F5244302E7879292E793B0A20206F63635F37203D20746D707661725F383B0A2020726C5F31203D20646F742028786C765F544558434F4F5244342E797A772C205F576F726C6453706163654C69676874506F73302E78797A293B0A20206D656469756D7020666C6F6174206F63636C7573696F6E5F393B0A20206F63636C7573696F6E5F39203D202828312E30202D205F4F63636C7573696F6E537472656E67746829202B20286F63635F37202A205F4F63636C7573696F6E537472656E67746829293B0A20206869676870207665633420746D707661725F31303B0A2020746D707661725F3130203D20756E6974795F5370656343756265305F4844523B0A20206D656469756D7020666C6F617420746D707661725F31313B0A2020746D707661725F3131203D2028312E30202D205F476C6F7373696E657373293B0A20206D656469756D702076656334206864725F31323B0A20206864725F3132203D20746D707661725F31303B0A20206D656469756D70207665633420746D707661725F31333B0A2020746D707661725F31332E78797A203D20786C765F544558434F4F5244342E797A773B0A2020746D707661725F31332E77203D202828746D707661725F3131202A2028312E37202D200A2020202028302E37202A20746D707661725F3131290A20202929202A20362E30293B0A20206C6F7770207665633420746D707661725F31343B0A2020746D707661725F3134203D20696D706C5F6C6F775F74657874757265437562654C6F644558542028756E6974795F5370656343756265302C20786C765F544558434F4F5244342E797A772C20746D707661725F31332E77293B0A20206D656469756D70207665633420746D707661725F31353B0A2020746D707661725F3135203D20746D707661725F31343B0A20206D656469756D70207665633220746D707661725F31363B0A2020746D707661725F31362E78203D202828726C5F31202A20726C5F3129202A2028726C5F31202A20726C5F3129293B0A2020746D707661725F31362E79203D20746D707661725F31313B0A20206C6F7770207665633420746D707661725F31373B0A2020746D707661725F3137203D207465787475726532442028756E6974795F4E4878526F7567686E6573732C20746D707661725F3136293B0A20206D656469756D70207665633420746D707661725F31383B0A2020746D707661725F31382E77203D20312E303B0A2020746D707661725F31382E78797A203D202828280A2020202028786C765F544558434F4F5244322E78797A202A206F63636C7573696F6E5F39290A2020202A20746D707661725F3429202B20280A202020202828286864725F31322E78202A20280A202020202020286864725F31322E77202A2028746D707661725F31352E77202D20312E3029290A20202020202B20312E302929202A20746D707661725F31352E78797A29202A206F63636C7573696F6E5F39290A2020202A200A202020206D69782028746D707661725F352C20786C765F544558434F4F5244312E7777772C20786C765F544558434F4F5244352E777777290A20202929202B202828746D707661725F34202B200A202020202828746D707661725F31372E77202A2031362E3029202A20746D707661725F35290A202029202A2028746D707661725F36202A200A20202020636C616D702028646F742028786C765F544558434F4F5244352E78797A2C205F576F726C6453706163654C69676874506F73302E78797A292C20302E302C20312E30290A20202929293B0A20206D656469756D70207665633420786C61745F7661726F75747075745F31393B0A2020786C61745F7661726F75747075745F31392E78797A203D20746D707661725F31382E78797A3B0A2020786C61745F7661726F75747075745F31392E77203D20312E303B0A2020676C5F46726167446174615B305D203D20786C61745F7661726F75747075745F31393B0A7D0A0A0A23656E6469660A0000001B000000000000000100000000000000000000000000000000000000324F040C05000000000000000000000000000000000000000100000012000000444952454354494F4E414C5F434F4F4B494500000000000000000000000000000100000000000000000000000000000000000000324F040C05000000000000000000000000000000000000000200000005000000504F494E540000000C000000534841444F57535F435542450000000000000000000000000100000000000000000000000000000000000000324F040C0500000040000000040000000100000000000000020000000C000000504F494E545F434F4F4B49450C000000534841444F57535F435542452C1A00002376657273696F6E203130300A0A236966646566205645525445580A6174747269627574652076656334205F676C65735665727465783B0A6174747269627574652076656333205F676C65734E6F726D616C3B0A6174747269627574652076656334205F676C65734D756C7469546578436F6F7264303B0A6174747269627574652076656334205F676C65734D756C7469546578436F6F7264313B0A756E69666F726D2068696768702076656333205F576F726C64537061636543616D657261506F733B0A756E69666F726D2068696768702076656334205F576F726C6453706163654C69676874506F73303B0A756E69666F726D206869676870206D61743420756E6974795F4F626A656374546F576F726C643B0A756E69666F726D206869676870206D61743420756E6974795F576F726C64546F4F626A6563743B0A756E69666F726D206869676870206D61743420756E6974795F4D617472697856503B0A756E69666F726D2068696768702076656334205F4D61696E5465785F53543B0A756E69666F726D2068696768702076656334205F44657461696C416C6265646F4D61705F53543B0A756E69666F726D206D656469756D7020666C6F6174205F55565365633B0A76617279696E67206869676870207665633420786C765F544558434F4F5244303B0A76617279696E67206D656469756D70207665633320786C765F544558434F4F5244313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244323B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244325F313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244325F323B0A76617279696E67206869676870207665633320786C765F544558434F4F5244353B0A76617279696E67206869676870207665633220786C765F544558434F4F5244363B0A766F6964206D61696E2028290A7B0A20206D656469756D70207665633320746D707661725F313B0A2020746D707661725F31203D205F676C65734E6F726D616C3B0A20206869676870207665633220746D707661725F323B0A2020746D707661725F32203D205F676C65734D756C7469546578436F6F7264302E78793B0A20206869676870207665633220746D707661725F333B0A2020746D707661725F33203D205F676C65734D756C7469546578436F6F7264312E78793B0A202068696768702076656333206C696768744469725F343B0A20206D656469756D70207665633420746D707661725F353B0A20206D656469756D70207665633420746D707661725F363B0A20206D656469756D70207665633420746D707661725F373B0A20206869676870207665633220746D707661725F383B0A20206869676870207665633420746D707661725F393B0A2020746D707661725F39203D2028756E6974795F4F626A656374546F576F726C64202A205F676C6573566572746578293B0A20206869676870207665633420746D707661725F31303B0A20206869676870207665633420746D707661725F31313B0A2020746D707661725F31312E77203D20312E303B0A2020746D707661725F31312E78797A203D205F676C65735665727465782E78797A3B0A2020746D707661725F3130203D2028756E6974795F4D61747269785650202A2028756E6974795F4F626A656374546F576F726C64202A20746D707661725F313129293B0A20206869676870207665633420746578636F6F72645F31323B0A2020746578636F6F72645F31322E7879203D2028285F676C65734D756C7469546578436F6F7264302E7879202A205F4D61696E5465785F53542E787929202B205F4D61696E5465785F53542E7A77293B0A20206869676870207665633220746D707661725F31333B0A202069662028285F5556536563203D3D20302E302929207B0A20202020746D707661725F3133203D20746D707661725F323B0A20207D20656C7365207B0A20202020746D707661725F3133203D20746D707661725F333B0A20207D3B0A2020746578636F6F72645F31322E7A77203D202828746D707661725F3133202A205F44657461696C416C6265646F4D61705F53542E787929202B205F44657461696C416C6265646F4D61705F53542E7A77293B0A20206D656469756D70207665633320746D707661725F31343B0A202068696768702076656333206E5F31353B0A20206E5F3135203D2028746D707661725F392E78797A202D205F576F726C64537061636543616D657261506F73293B0A2020746D707661725F3134203D206E5F31353B0A202068696768702076656333206E6F726D5F31363B0A20206E6F726D5F3136203D20746D707661725F313B0A20206869676870206D61743320746D707661725F31373B0A2020746D707661725F31375B305D203D20756E6974795F576F726C64546F4F626A6563745B305D2E78797A3B0A2020746D707661725F31375B315D203D20756E6974795F576F726C64546F4F626A6563745B315D2E78797A3B0A2020746D707661725F31375B325D203D20756E6974795F576F726C64546F4F626A6563745B325D2E78797A3B0A20206869676870207665633320746D707661725F31383B0A2020746D707661725F3138203D206E6F726D616C697A6528286E6F726D5F3136202A20746D707661725F313729293B0A2020746D707661725F352E78797A203D207665633328302E302C20302E302C20302E30293B0A2020746D707661725F362E78797A203D207665633328302E302C20302E302C20302E30293B0A2020746D707661725F372E78797A203D20746D707661725F31383B0A20206869676870207665633320746D707661725F31393B0A2020746D707661725F3139203D20285F576F726C6453706163654C69676874506F73302E78797A202D2028746D707661725F392E78797A202A205F576F726C6453706163654C69676874506F73302E7729293B0A20206D656469756D70207665633320746D707661725F32303B0A2020746D707661725F3230203D20746D707661725F31393B0A20206C696768744469725F34203D20746D707661725F32303B0A2020746D707661725F352E77203D206C696768744469725F342E783B0A2020746D707661725F362E77203D206C696768744469725F342E793B0A2020746D707661725F372E77203D206C696768744469725F342E7A3B0A2020676C5F506F736974696F6E203D20746D707661725F31303B0A2020786C765F544558434F4F524430203D20746578636F6F72645F31323B0A2020786C765F544558434F4F524431203D20746D707661725F31343B0A2020786C765F544558434F4F524432203D20746D707661725F353B0A2020786C765F544558434F4F5244325F31203D20746D707661725F363B0A2020786C765F544558434F4F5244325F32203D20746D707661725F373B0A2020786C765F544558434F4F524435203D20746D707661725F392E78797A3B0A2020786C765F544558434F4F524436203D20746D707661725F383B0A7D0A0A0A23656E6469660A23696664656620465241474D454E540A756E69666F726D2068696768702076656333205F576F726C64537061636543616D657261506F733B0A756E69666F726D2068696768702076656334205F4C69676874506F736974696F6E52616E67653B0A756E69666F726D206D656469756D702076656334205F4C69676874536861646F77446174613B0A756E69666F726D206869676870207665633420756E6974795F536861646F774661646543656E746572416E64547970653B0A756E69666F726D206869676870206D61743420756E6974795F4D6174726978563B0A756E69666F726D206C6F77702076656334205F4C69676874436F6C6F72303B0A756E69666F726D2068696768702073616D706C657243756265205F536861646F774D6170546578747572653B0A756E69666F726D206D656469756D702076656334205F436F6C6F723B0A756E69666F726D2073616D706C65723244205F4D61696E5465783B0A756E69666F726D206D656469756D7020666C6F6174205F4D6574616C6C69633B0A756E69666F726D206D656469756D7020666C6F6174205F476C6F7373696E6573733B0A756E69666F726D206C6F77702073616D706C657243756265205F4C6967687454657874757265303B0A756E69666F726D206869676870206D61743420756E6974795F576F726C64546F4C696768743B0A756E69666F726D2073616D706C65723244205F4C696768745465787475726542303B0A76617279696E67206869676870207665633420786C765F544558434F4F5244303B0A76617279696E67206D656469756D70207665633320786C765F544558434F4F5244313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244323B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244325F313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244325F323B0A76617279696E67206869676870207665633320786C765F544558434F4F5244353B0A766F6964206D61696E2028290A7B0A20206C6F777020666C6F617420736861646F775F313B0A20206C6F7770207665633420746D707661725F323B0A2020746D707661725F32203D2074657874757265324420285F4D61696E5465782C20786C765F544558434F4F5244302E7879293B0A20206D656469756D70207665633320746D707661725F333B0A2020746D707661725F33203D20285F436F6C6F722E78797A202A20746D707661725F322E78797A293B0A20206D656469756D70207665633320746D707661725F343B0A20206D656469756D70207665633320746D707661725F353B0A2020746D707661725F35203D206D697820287665633328302E323230393136332C20302E323230393136332C20302E32323039313633292C20746D707661725F332C2076656333285F4D6574616C6C696329293B0A2020746D707661725F34203D2028746D707661725F33202A2028302E37373930383337202D20285F4D6574616C6C6963202A20302E373739303833372929293B0A20206D656469756D70207665633320746D707661725F363B0A2020746D707661725F36203D206E6F726D616C697A6528786C765F544558434F4F5244325F322E78797A293B0A20206D656469756D70207665633320746D707661725F373B0A2020746D707661725F37203D206E6F726D616C697A6528786C765F544558434F4F524431293B0A20206869676870207665633420746D707661725F383B0A2020746D707661725F382E77203D20312E303B0A2020746D707661725F382E78797A203D20786C765F544558434F4F5244353B0A20206869676870207665633320746D707661725F393B0A2020746D707661725F39203D2028756E6974795F576F726C64546F4C69676874202A20746D707661725F38292E78797A3B0A20206869676870207665633420765F31303B0A2020765F31302E78203D20756E6974795F4D6174726978565B305D2E7A3B0A2020765F31302E79203D20756E6974795F4D6174726978565B315D2E7A3B0A2020765F31302E7A203D20756E6974795F4D6174726978565B325D2E7A3B0A2020765F31302E77203D20756E6974795F4D6174726978565B335D2E7A3B0A20206869676870207665633320746D707661725F31313B0A2020746D707661725F3131203D2028786C765F544558434F4F524435202D20756E6974795F536861646F774661646543656E746572416E64547970652E78797A293B0A20206D656469756D7020666C6F617420746D707661725F31323B0A2020686967687020666C6F617420746D707661725F31333B0A2020746D707661725F3133203D20636C616D70202828280A202020206D69782028646F742028285F576F726C64537061636543616D657261506F73202D20786C765F544558434F4F524435292C20765F31302E78797A292C207371727428646F742028746D707661725F31312C20746D707661725F313129292C20756E6974795F536861646F774661646543656E746572416E64547970652E77290A2020202A205F4C69676874536861646F77446174612E7A29202B205F4C69676874536861646F77446174612E77292C20302E302C20312E30293B0A2020746D707661725F3132203D20746D707661725F31333B0A202068696768702076656333207665635F31343B0A20207665635F3134203D2028786C765F544558434F4F524435202D205F4C69676874506F736974696F6E52616E67652E78797A293B0A2020686967687020666C6F6174206D79646973745F31353B0A20206D79646973745F3135203D20282873717274280A20202020646F7420287665635F31342C207665635F3134290A202029202A205F4C69676874506F736974696F6E52616E67652E7729202A20302E3937293B0A2020686967687020666C6F617420746D707661725F31363B0A2020746D707661725F3136203D20646F742028746578747572654375626520285F536861646F774D6170546578747572652C207665635F3134292C207665633428312E302C20302E3030333932313536392C20312E3533373837652D30352C20362E303330383633652D303829293B0A20206D656469756D7020666C6F617420746D707661725F31373B0A20206966202828746D707661725F3136203C206D79646973745F31352929207B0A20202020746D707661725F3137203D205F4C69676874536861646F77446174612E783B0A20207D20656C7365207B0A20202020746D707661725F3137203D20312E303B0A20207D3B0A20206D656469756D7020666C6F617420746D707661725F31383B0A2020746D707661725F3138203D20636C616D70202828746D707661725F3137202B20746D707661725F3132292C20302E302C20312E30293B0A2020736861646F775F31203D20746D707661725F31383B0A2020686967687020666C6F617420746D707661725F31393B0A2020746D707661725F3139203D20646F742028746D707661725F392C20746D707661725F39293B0A20206C6F777020666C6F617420746D707661725F32303B0A2020746D707661725F3230203D20282874657874757265324420285F4C696768745465787475726542302C207665633228746D707661725F313929292E77202A20746578747572654375626520285F4C6967687454657874757265302C20746D707661725F39292E7729202A20736861646F775F31293B0A20206D656469756D70207665633320746D707661725F32313B0A2020746D707661725F32312E78203D20786C765F544558434F4F5244322E773B0A2020746D707661725F32312E79203D20786C765F544558434F4F5244325F312E773B0A2020746D707661725F32312E7A203D20786C765F544558434F4F5244325F322E773B0A20206D656469756D7020666C6F617420617474656E5F32323B0A2020617474656E5F3232203D20746D707661725F32303B0A20206D656469756D70207665633320746D707661725F32333B0A2020746D707661725F3233203D205F4C69676874436F6C6F72302E78797A3B0A20206D656469756D70207665633320746D707661725F32343B0A2020746D707661725F3234203D206E6F726D616C697A6528746D707661725F3231293B0A2020746D707661725F3233203D2028746D707661725F3233202A20617474656E5F3232293B0A20206D656469756D7020666C6F61742073706563756C61725465726D5F32353B0A20206D656469756D70207665633320746D707661725F32363B0A20206D656469756D70207665633320696E5665635F32373B0A2020696E5665635F3237203D2028746D707661725F3234202D20746D707661725F37293B0A2020746D707661725F3236203D2028696E5665635F3237202A20696E766572736573717274286D61782028302E3030312C200A20202020646F742028696E5665635F32372C20696E5665635F3237290A20202929293B0A20206D656469756D7020666C6F617420746D707661725F32383B0A2020746D707661725F3238203D20636C616D702028646F742028746D707661725F362C20746D707661725F3236292C20302E302C20312E30293B0A20206D656469756D7020666C6F617420746D707661725F32393B0A2020746D707661725F3239203D2028312E30202D205F476C6F7373696E657373293B0A20206D656469756D7020666C6F617420746D707661725F33303B0A2020746D707661725F3330203D2028746D707661725F3239202A20746D707661725F3239293B0A202073706563756C61725465726D5F3235203D202828746D707661725F3330202F20280A20202020286D61782028302E33322C20636C616D702028646F742028746D707661725F32342C20746D707661725F3236292C20302E302C20312E302929202A2028312E35202B20746D707661725F333029290A2020202A200A20202020282828746D707661725F3238202A20746D707661725F323829202A202828746D707661725F3330202A20746D707661725F333029202D20312E302929202B20312E3030303031290A20202929202D20302E30303031293B0A20206D656469756D7020666C6F617420746D707661725F33313B0A2020746D707661725F3331203D20636C616D70202873706563756C61725465726D5F32352C20302E302C203130302E30293B0A202073706563756C61725465726D5F3235203D20746D707661725F33313B0A20206D656469756D70207665633420746D707661725F33323B0A2020746D707661725F33322E77203D20312E303B0A2020746D707661725F33322E78797A203D20282828746D707661725F34202B200A2020202028746D707661725F3331202A20746D707661725F35290A202029202A20746D707661725F323329202A20636C616D702028646F742028746D707661725F362C20746D707661725F3234292C20302E302C20312E3029293B0A20206D656469756D70207665633420786C61745F7661726F75747075745F33333B0A2020786C61745F7661726F75747075745F33332E78797A203D20746D707661725F33322E78797A3B0A2020786C61745F7661726F75747075745F33332E77203D20312E303B0A2020676C5F46726167446174615B305D203D20786C61745F7661726F75747075745F33333B0A7D0A0A0A23656E6469660A1B000000000000000100000000000000000000000000000000000000324F040C0500000021000000050000000100000000000000020000000C000000504F494E545F434F4F4B49450C000000534841444F57535F43554245BC1300002376657273696F6E203130300A0A236966646566205645525445580A6174747269627574652076656334205F676C65735665727465783B0A6174747269627574652076656333205F676C65734E6F726D616C3B0A6174747269627574652076656334205F676C65734D756C7469546578436F6F7264303B0A6174747269627574652076656334205F676C65734D756C7469546578436F6F7264313B0A756E69666F726D2068696768702076656333205F576F726C64537061636543616D657261506F733B0A756E69666F726D2068696768702076656334205F576F726C6453706163654C69676874506F73303B0A756E69666F726D206869676870206D61743420756E6974795F4F626A656374546F576F726C643B0A756E69666F726D206869676870206D61743420756E6974795F576F726C64546F4F626A6563743B0A756E69666F726D206869676870206D61743420756E6974795F4D617472697856503B0A756E69666F726D2068696768702076656334205F4D61696E5465785F53543B0A756E69666F726D2068696768702076656334205F44657461696C416C6265646F4D61705F53543B0A756E69666F726D206D656469756D7020666C6F6174205F55565365633B0A76617279696E67206869676870207665633420786C765F544558434F4F5244303B0A76617279696E67206869676870207665633320786C765F544558434F4F5244313B0A76617279696E67206869676870207665633220786C765F544558434F4F5244323B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244333B0A76617279696E67206D656469756D70207665633320786C765F544558434F4F5244343B0A76617279696E67206D656469756D70207665633320786C765F544558434F4F5244353B0A766F6964206D61696E2028290A7B0A20206D656469756D70207665633320746D707661725F313B0A2020746D707661725F31203D205F676C65734E6F726D616C3B0A20206869676870207665633220746D707661725F323B0A2020746D707661725F32203D205F676C65734D756C7469546578436F6F7264302E78793B0A20206869676870207665633220746D707661725F333B0A2020746D707661725F33203D205F676C65734D756C7469546578436F6F7264312E78793B0A20206D656469756D702076656333206E6F726D616C576F726C645F343B0A20206D656469756D702076656333206579655665635F353B0A20206D656469756D702076656333206C696768744469725F363B0A20206869676870207665633220746D707661725F373B0A20206D656469756D70207665633420746D707661725F383B0A20206869676870207665633420746D707661725F393B0A2020746D707661725F39203D2028756E6974795F4F626A656374546F576F726C64202A205F676C6573566572746578293B0A20206869676870207665633420746D707661725F31303B0A20206869676870207665633420746D707661725F31313B0A2020746D707661725F31312E77203D20312E303B0A2020746D707661725F31312E78797A203D205F676C65735665727465782E78797A3B0A2020746D707661725F3130203D2028756E6974795F4D61747269785650202A2028756E6974795F4F626A656374546F576F726C64202A20746D707661725F313129293B0A20206869676870207665633420746578636F6F72645F31323B0A2020746578636F6F72645F31322E7879203D2028285F676C65734D756C7469546578436F6F7264302E7879202A205F4D61696E5465785F53542E787929202B205F4D61696E5465785F53542E7A77293B0A20206869676870207665633220746D707661725F31333B0A202069662028285F5556536563203D3D20302E302929207B0A20202020746D707661725F3133203D20746D707661725F323B0A20207D20656C7365207B0A20202020746D707661725F3133203D20746D707661725F333B0A20207D3B0A2020746578636F6F72645F31322E7A77203D202828746D707661725F3133202A205F44657461696C416C6265646F4D61705F53542E787929202B205F44657461696C416C6265646F4D61705F53542E7A77293B0A20206869676870207665633320746D707661725F31343B0A2020746D707661725F3134203D20285F576F726C6453706163654C69676874506F73302E78797A202D2028746D707661725F392E78797A202A205F576F726C6453706163654C69676874506F73302E7729293B0A20206C696768744469725F36203D20746D707661725F31343B0A20206D656469756D70207665633320746D707661725F31353B0A202068696768702076656333206E5F31363B0A20206E5F3136203D206C696768744469725F363B0A20206869676870207665633320746D707661725F31373B0A2020746D707661725F3137203D206E6F726D616C697A65286E5F3136293B0A2020746D707661725F3135203D20746D707661725F31373B0A20206C696768744469725F36203D20746D707661725F31353B0A20206869676870207665633320746D707661725F31383B0A2020746D707661725F3138203D206E6F726D616C697A652828746D707661725F392E78797A202D205F576F726C64537061636543616D657261506F7329293B0A20206579655665635F35203D20746D707661725F31383B0A202068696768702076656333206E6F726D5F31393B0A20206E6F726D5F3139203D20746D707661725F313B0A20206869676870206D61743320746D707661725F32303B0A2020746D707661725F32305B305D203D20756E6974795F576F726C64546F4F626A6563745B305D2E78797A3B0A2020746D707661725F32305B315D203D20756E6974795F576F726C64546F4F626A6563745B315D2E78797A3B0A2020746D707661725F32305B325D203D20756E6974795F576F726C64546F4F626A6563745B325D2E78797A3B0A20206869676870207665633320746D707661725F32313B0A2020746D707661725F3231203D206E6F726D616C697A6528286E6F726D5F3139202A20746D707661725F323029293B0A20206E6F726D616C576F726C645F34203D20746D707661725F32313B0A2020746D707661725F382E797A77203D20286579655665635F35202D2028322E30202A20280A20202020646F7420286E6F726D616C576F726C645F342C206579655665635F35290A2020202A206E6F726D616C576F726C645F342929293B0A2020676C5F506F736974696F6E203D20746D707661725F31303B0A2020786C765F544558434F4F524430203D20746578636F6F72645F31323B0A2020786C765F544558434F4F524431203D20746D707661725F392E78797A3B0A2020786C765F544558434F4F524432203D20746D707661725F373B0A2020786C765F544558434F4F524433203D20746D707661725F383B0A2020786C765F544558434F4F524434203D20746D707661725F31353B0A2020786C765F544558434F4F524435203D206E6F726D616C576F726C645F343B0A7D0A0A0A23656E6469660A23696664656620465241474D454E540A756E69666F726D2068696768702076656334205F4C69676874506F736974696F6E52616E67653B0A756E69666F726D206D656469756D702076656334205F4C69676874536861646F77446174613B0A756E69666F726D206C6F77702076656334205F4C69676874436F6C6F72303B0A756E69666F726D2073616D706C6572324420756E6974795F4E4878526F7567686E6573733B0A756E69666F726D2068696768702073616D706C657243756265205F536861646F774D6170546578747572653B0A756E69666F726D206D656469756D702076656334205F436F6C6F723B0A756E69666F726D2073616D706C65723244205F4D61696E5465783B0A756E69666F726D206D656469756D7020666C6F6174205F4D6574616C6C69633B0A756E69666F726D206D656469756D7020666C6F6174205F476C6F7373696E6573733B0A756E69666F726D206C6F77702073616D706C657243756265205F4C6967687454657874757265303B0A756E69666F726D206869676870206D61743420756E6974795F576F726C64546F4C696768743B0A756E69666F726D2073616D706C65723244205F4C696768745465787475726542303B0A76617279696E67206869676870207665633420786C765F544558434F4F5244303B0A76617279696E67206869676870207665633320786C765F544558434F4F5244313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244333B0A76617279696E67206D656469756D70207665633320786C765F544558434F4F5244343B0A76617279696E67206D656469756D70207665633320786C765F544558434F4F5244353B0A766F6964206D61696E2028290A7B0A20206C6F777020666C6F617420736861646F775F313B0A202068696768702076656333206C69676874436F6F72645F323B0A20206D656469756D70207665633320635F333B0A20206C6F7770207665633420746D707661725F343B0A2020746D707661725F34203D2074657874757265324420285F4D61696E5465782C20786C765F544558434F4F5244302E7879293B0A20206D656469756D70207665633320746D707661725F353B0A2020746D707661725F35203D20285F436F6C6F722E78797A202A20746D707661725F342E78797A293B0A20206D656469756D7020666C6F617420746D707661725F363B0A2020746D707661725F36203D20646F742028786C765F544558434F4F5244332E797A772C20786C765F544558434F4F524434293B0A20206D656469756D70207665633220746D707661725F373B0A2020746D707661725F372E78203D202828746D707661725F36202A20746D707661725F3629202A2028746D707661725F36202A20746D707661725F3629293B0A2020746D707661725F372E79203D2028312E30202D205F476C6F7373696E657373293B0A20206C6F7770207665633420746D707661725F383B0A2020746D707661725F38203D207465787475726532442028756E6974795F4E4878526F7567686E6573732C20746D707661725F37293B0A2020635F33203D20282828746D707661725F35202A200A2020202028302E37373930383337202D20285F4D6574616C6C6963202A20302E3737393038333729290A202029202B20280A2020202028746D707661725F382E77202A2031362E30290A2020202A200A202020206D697820287665633328302E323230393136332C20302E323230393136332C20302E32323039313633292C20746D707661725F352C2076656333285F4D6574616C6C696329290A20202929202A205F4C69676874436F6C6F72302E78797A293B0A20206869676870207665633420746D707661725F393B0A2020746D707661725F392E77203D20312E303B0A2020746D707661725F392E78797A203D20786C765F544558434F4F5244313B0A20206C69676874436F6F72645F32203D2028756E6974795F576F726C64546F4C69676874202A20746D707661725F39292E78797A3B0A202068696768702076656333207665635F31303B0A20207665635F3130203D2028786C765F544558434F4F524431202D205F4C69676874506F736974696F6E52616E67652E78797A293B0A2020686967687020666C6F6174206D79646973745F31313B0A20206D79646973745F3131203D20282873717274280A20202020646F7420287665635F31302C207665635F3130290A202029202A205F4C69676874506F736974696F6E52616E67652E7729202A20302E3937293B0A2020686967687020666C6F617420746D707661725F31323B0A2020746D707661725F3132203D20646F742028746578747572654375626520285F536861646F774D6170546578747572652C207665635F3130292C207665633428312E302C20302E3030333932313536392C20312E3533373837652D30352C20362E303330383633652D303829293B0A20206D656469756D7020666C6F617420746D707661725F31333B0A20206966202828746D707661725F3132203C206D79646973745F31312929207B0A20202020746D707661725F3133203D205F4C69676874536861646F77446174612E783B0A20207D20656C7365207B0A20202020746D707661725F3133203D20312E303B0A20207D3B0A2020736861646F775F31203D20746D707661725F31333B0A2020686967687020666C6F617420746D707661725F31343B0A2020746D707661725F3134203D20646F7420286C69676874436F6F72645F322C206C69676874436F6F72645F32293B0A20206C6F777020666C6F617420746D707661725F31353B0A2020746D707661725F3135203D20282874657874757265324420285F4C696768745465787475726542302C207665633228746D707661725F313429292E77202A20746578747572654375626520285F4C6967687454657874757265302C206C69676874436F6F72645F32292E7729202A20736861646F775F31293B0A2020635F33203D2028635F33202A2028746D707661725F3135202A20636C616D7020280A20202020646F742028786C765F544558434F4F5244352C20786C765F544558434F4F524434290A20202C20302E302C20312E302929293B0A20206D656469756D70207665633420746D707661725F31363B0A2020746D707661725F31362E77203D20312E303B0A2020746D707661725F31362E78797A203D20635F333B0A20206D656469756D70207665633420786C61745F7661726F75747075745F31373B0A2020786C61745F7661726F75747075745F31372E78797A203D20746D707661725F31362E78797A3B0A2020786C61745F7661726F75747075745F31372E77203D20312E303B0A2020676C5F46726167446174615B305D203D20786C61745F7661726F75747075745F31373B0A7D0A0A0A23656E6469660A1B000000000000000100000000000000000000000000000000000000324F040C0500000000000000000000000000000000000000010000000B000000444952454354494F4E414C000000000000000000000000000100000000000000000000000000000000000000324F040C05000000290000000400000001000000000000000200000005000000504F494E540000000C000000534841444F57535F43554245CF1500002376657273696F6E203130300A0A236966646566205645525445580A6174747269627574652076656334205F676C65735665727465783B0A6174747269627574652076656333205F676C65734E6F726D616C3B0A6174747269627574652076656334205F676C65734D756C7469546578436F6F7264303B0A6174747269627574652076656334205F676C65734D756C7469546578436F6F7264313B0A756E69666F726D2068696768702076656333205F576F726C64537061636543616D657261506F733B0A756E69666F726D2068696768702076656334205F576F726C6453706163654C69676874506F73303B0A756E69666F726D206869676870206D61743420756E6974795F4F626A656374546F576F726C643B0A756E69666F726D206869676870206D61743420756E6974795F576F726C64546F4F626A6563743B0A756E69666F726D206869676870206D61743420756E6974795F4D617472697856503B0A756E69666F726D2068696768702076656334205F4D61696E5465785F53543B0A756E69666F726D2068696768702076656334205F44657461696C416C6265646F4D61705F53543B0A756E69666F726D206D656469756D7020666C6F6174205F55565365633B0A76617279696E67206869676870207665633420786C765F544558434F4F5244303B0A76617279696E67206D656469756D70207665633320786C765F544558434F4F5244313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244323B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244325F313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244325F323B0A76617279696E67206869676870207665633320786C765F544558434F4F5244353B0A76617279696E67206869676870207665633220786C765F544558434F4F5244363B0A766F6964206D61696E2028290A7B0A20206D656469756D70207665633320746D707661725F313B0A2020746D707661725F31203D205F676C65734E6F726D616C3B0A20206869676870207665633220746D707661725F323B0A2020746D707661725F32203D205F676C65734D756C7469546578436F6F7264302E78793B0A20206869676870207665633220746D707661725F333B0A2020746D707661725F33203D205F676C65734D756C7469546578436F6F7264312E78793B0A202068696768702076656333206C696768744469725F343B0A20206D656469756D70207665633420746D707661725F353B0A20206D656469756D70207665633420746D707661725F363B0A20206D656469756D70207665633420746D707661725F373B0A20206869676870207665633220746D707661725F383B0A20206869676870207665633420746D707661725F393B0A2020746D707661725F39203D2028756E6974795F4F626A656374546F576F726C64202A205F676C6573566572746578293B0A20206869676870207665633420746D707661725F31303B0A20206869676870207665633420746D707661725F31313B0A2020746D707661725F31312E77203D20312E303B0A2020746D707661725F31312E78797A203D205F676C65735665727465782E78797A3B0A2020746D707661725F3130203D2028756E6974795F4D61747269785650202A2028756E6974795F4F626A656374546F576F726C64202A20746D707661725F313129293B0A20206869676870207665633420746578636F6F72645F31323B0A2020746578636F6F72645F31322E7879203D2028285F676C65734D756C7469546578436F6F7264302E7879202A205F4D61696E5465785F53542E787929202B205F4D61696E5465785F53542E7A77293B0A20206869676870207665633220746D707661725F31333B0A202069662028285F5556536563203D3D20302E302929207B0A20202020746D707661725F3133203D20746D707661725F323B0A20207D20656C7365207B0A20202020746D707661725F3133203D20746D707661725F333B0A20207D3B0A2020746578636F6F72645F31322E7A77203D202828746D707661725F3133202A205F44657461696C416C6265646F4D61705F53542E787929202B205F44657461696C416C6265646F4D61705F53542E7A77293B0A20206D656469756D70207665633320746D707661725F31343B0A20206869676870207665633320746D707661725F31353B0A2020746D707661725F3135203D206E6F726D616C697A652828746D707661725F392E78797A202D205F576F726C64537061636543616D657261506F7329293B0A2020746D707661725F3134203D20746D707661725F31353B0A202068696768702076656333206E6F726D5F31363B0A20206E6F726D5F3136203D20746D707661725F313B0A20206869676870206D61743320746D707661725F31373B0A2020746D707661725F31375B305D203D20756E6974795F576F726C64546F4F626A6563745B305D2E78797A3B0A2020746D707661725F31375B315D203D20756E6974795F576F726C64546F4F626A6563745B315D2E78797A3B0A2020746D707661725F31375B325D203D20756E6974795F576F726C64546F4F626A6563745B325D2E78797A3B0A20206869676870207665633320746D707661725F31383B0A2020746D707661725F3138203D206E6F726D616C697A6528286E6F726D5F3136202A20746D707661725F313729293B0A2020746D707661725F352E78797A203D207665633328302E302C20302E302C20302E30293B0A2020746D707661725F362E78797A203D207665633328302E302C20302E302C20302E30293B0A2020746D707661725F372E78797A203D20746D707661725F31383B0A20206D656469756D70207665633320746D707661725F31393B0A20206869676870207665633320746D707661725F32303B0A2020746D707661725F3230203D206E6F726D616C697A6528285F576F726C6453706163654C69676874506F73302E78797A202D2028746D707661725F392E78797A202A205F576F726C6453706163654C69676874506F73302E772929293B0A2020746D707661725F3139203D20746D707661725F32303B0A20206C696768744469725F34203D20746D707661725F31393B0A2020746D707661725F352E77203D206C696768744469725F342E783B0A2020746D707661725F362E77203D206C696768744469725F342E793B0A2020746D707661725F372E77203D206C696768744469725F342E7A3B0A2020676C5F506F736974696F6E203D20746D707661725F31303B0A2020786C765F544558434F4F524430203D20746578636F6F72645F31323B0A2020786C765F544558434F4F524431203D20746D707661725F31343B0A2020786C765F544558434F4F524432203D20746D707661725F353B0A2020786C765F544558434F4F5244325F31203D20746D707661725F363B0A2020786C765F544558434F4F5244325F32203D20746D707661725F373B0A2020786C765F544558434F4F524435203D20746D707661725F392E78797A3B0A2020786C765F544558434F4F524436203D20746D707661725F383B0A7D0A0A0A23656E6469660A23696664656620465241474D454E540A756E69666F726D2068696768702076656334205F4C69676874506F736974696F6E52616E67653B0A756E69666F726D206D656469756D702076656334205F4C69676874536861646F77446174613B0A756E69666F726D206C6F77702076656334205F4C69676874436F6C6F72303B0A756E69666F726D2073616D706C6572324420756E6974795F4E4878526F7567686E6573733B0A756E69666F726D2068696768702073616D706C657243756265205F536861646F774D6170546578747572653B0A756E69666F726D206D656469756D702076656334205F436F6C6F723B0A756E69666F726D2073616D706C65723244205F4D61696E5465783B0A756E69666F726D206D656469756D7020666C6F6174205F4D6574616C6C69633B0A756E69666F726D206D656469756D7020666C6F6174205F476C6F7373696E6573733B0A756E69666F726D2073616D706C65723244205F4C6967687454657874757265303B0A756E69666F726D206869676870206D61743420756E6974795F576F726C64546F4C696768743B0A76617279696E67206869676870207665633420786C765F544558434F4F5244303B0A76617279696E67206D656469756D70207665633320786C765F544558434F4F5244313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244323B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244325F313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244325F323B0A76617279696E67206869676870207665633320786C765F544558434F4F5244353B0A766F6964206D61696E2028290A7B0A20206C6F777020666C6F617420736861646F775F313B0A202068696768702076656333206C69676874436F6F72645F323B0A20206C6F7770207665633420746D707661725F333B0A2020746D707661725F33203D2074657874757265324420285F4D61696E5465782C20786C765F544558434F4F5244302E7879293B0A20206D656469756D70207665633320746D707661725F343B0A2020746D707661725F34203D20285F436F6C6F722E78797A202A20746D707661725F332E78797A293B0A20206D656469756D70207665633320746D707661725F353B0A20206D656469756D70207665633320746D707661725F363B0A2020746D707661725F36203D206D697820287665633328302E323230393136332C20302E323230393136332C20302E32323039313633292C20746D707661725F342C2076656333285F4D6574616C6C696329293B0A2020746D707661725F35203D2028746D707661725F34202A2028302E37373930383337202D20285F4D6574616C6C6963202A20302E373739303833372929293B0A20206D656469756D70207665633320746D707661725F373B0A2020746D707661725F37203D206E6F726D616C697A6528786C765F544558434F4F5244325F322E78797A293B0A20206869676870207665633420746D707661725F383B0A2020746D707661725F382E77203D20312E303B0A2020746D707661725F382E78797A203D20786C765F544558434F4F5244353B0A20206C69676874436F6F72645F32203D2028756E6974795F576F726C64546F4C69676874202A20746D707661725F38292E78797A3B0A202068696768702076656333207665635F393B0A20207665635F39203D2028786C765F544558434F4F524435202D205F4C69676874506F736974696F6E52616E67652E78797A293B0A2020686967687020666C6F6174206D79646973745F31303B0A20206D79646973745F3130203D20282873717274280A20202020646F7420287665635F392C207665635F39290A202029202A205F4C69676874506F736974696F6E52616E67652E7729202A20302E3937293B0A2020686967687020666C6F617420746D707661725F31313B0A2020746D707661725F3131203D20646F742028746578747572654375626520285F536861646F774D6170546578747572652C207665635F39292C207665633428312E302C20302E3030333932313536392C20312E3533373837652D30352C20362E303330383633652D303829293B0A20206D656469756D7020666C6F617420746D707661725F31323B0A20206966202828746D707661725F3131203C206D79646973745F31302929207B0A20202020746D707661725F3132203D205F4C69676874536861646F77446174612E783B0A20207D20656C7365207B0A20202020746D707661725F3132203D20312E303B0A20207D3B0A2020736861646F775F31203D20746D707661725F31323B0A2020686967687020666C6F617420746D707661725F31333B0A2020746D707661725F3133203D20646F7420286C69676874436F6F72645F322C206C69676874436F6F72645F32293B0A20206C6F777020666C6F617420746D707661725F31343B0A2020746D707661725F3134203D202874657874757265324420285F4C6967687454657874757265302C207665633228746D707661725F313329292E77202A20736861646F775F31293B0A20206D656469756D70207665633320746D707661725F31353B0A2020746D707661725F31352E78203D20786C765F544558434F4F5244322E773B0A2020746D707661725F31352E79203D20786C765F544558434F4F5244325F312E773B0A2020746D707661725F31352E7A203D20786C765F544558434F4F5244325F322E773B0A20206D656469756D7020666C6F617420617474656E5F31363B0A2020617474656E5F3136203D20746D707661725F31343B0A20206D656469756D70207665633320746D707661725F31373B0A2020746D707661725F3137203D205F4C69676874436F6C6F72302E78797A3B0A2020746D707661725F3137203D2028746D707661725F3137202A20617474656E5F3136293B0A20206D656469756D70207665633320766965774469725F31383B0A2020766965774469725F3138203D202D28786C765F544558434F4F524431293B0A20206D656469756D70207665633220746D707661725F31393B0A2020746D707661725F31392E78203D20646F74202828766965774469725F3138202D2028322E30202A200A2020202028646F742028746D707661725F372C20766965774469725F313829202A20746D707661725F37290A202029292C20746D707661725F3135293B0A2020746D707661725F31392E79203D2028312E30202D20636C616D702028646F742028746D707661725F372C20766965774469725F3138292C20302E302C20312E3029293B0A20206D656469756D70207665633220746D707661725F32303B0A2020746D707661725F32302E78203D202828746D707661725F3139202A20746D707661725F313929202A2028746D707661725F3139202A20746D707661725F313929292E783B0A2020746D707661725F32302E79203D2028312E30202D205F476C6F7373696E657373293B0A20206C6F7770207665633420746D707661725F32313B0A2020746D707661725F3231203D207465787475726532442028756E6974795F4E4878526F7567686E6573732C20746D707661725F3230293B0A20206D656469756D70207665633420746D707661725F32323B0A2020746D707661725F32322E77203D20312E303B0A2020746D707661725F32322E78797A203D202828746D707661725F35202B20280A2020202028746D707661725F32312E77202A2031362E30290A2020202A20746D707661725F362929202A2028746D707661725F3137202A20636C616D7020280A20202020646F742028746D707661725F372C20746D707661725F3135290A20202C20302E302C20312E302929293B0A20206D656469756D70207665633420786C61745F7661726F75747075745F32333B0A2020786C61745F7661726F75747075745F32332E78797A203D20746D707661725F32322E78797A3B0A2020786C61745F7661726F75747075745F32332E77203D20312E303B0A2020676C5F46726167446174615B305D203D20786C61745F7661726F75747075745F32333B0A7D0A0A0A23656E6469660A001B000000000000000100000000000000000000000000000000000000324F040C0500000018000000040000000000000000000000010000000C000000504F494E545F434F4F4B49453A1100002376657273696F6E203130300A0A236966646566205645525445580A6174747269627574652076656334205F676C65735665727465783B0A6174747269627574652076656333205F676C65734E6F726D616C3B0A6174747269627574652076656334205F676C65734D756C7469546578436F6F7264303B0A6174747269627574652076656334205F676C65734D756C7469546578436F6F7264313B0A756E69666F726D2068696768702076656333205F576F726C64537061636543616D657261506F733B0A756E69666F726D2068696768702076656334205F576F726C6453706163654C69676874506F73303B0A756E69666F726D206869676870206D61743420756E6974795F4F626A656374546F576F726C643B0A756E69666F726D206869676870206D61743420756E6974795F576F726C64546F4F626A6563743B0A756E69666F726D206869676870206D61743420756E6974795F4D617472697856503B0A756E69666F726D2068696768702076656334205F4D61696E5465785F53543B0A756E69666F726D2068696768702076656334205F44657461696C416C6265646F4D61705F53543B0A756E69666F726D206D656469756D7020666C6F6174205F55565365633B0A76617279696E67206869676870207665633420786C765F544558434F4F5244303B0A76617279696E67206869676870207665633320786C765F544558434F4F5244313B0A76617279696E67206869676870207665633220786C765F544558434F4F5244323B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244333B0A76617279696E67206D656469756D70207665633320786C765F544558434F4F5244343B0A76617279696E67206D656469756D70207665633320786C765F544558434F4F5244353B0A766F6964206D61696E2028290A7B0A20206D656469756D70207665633320746D707661725F313B0A2020746D707661725F31203D205F676C65734E6F726D616C3B0A20206869676870207665633220746D707661725F323B0A2020746D707661725F32203D205F676C65734D756C7469546578436F6F7264302E78793B0A20206869676870207665633220746D707661725F333B0A2020746D707661725F33203D205F676C65734D756C7469546578436F6F7264312E78793B0A20206D656469756D702076656333206E6F726D616C576F726C645F343B0A20206D656469756D702076656333206579655665635F353B0A20206D656469756D702076656333206C696768744469725F363B0A20206869676870207665633220746D707661725F373B0A20206D656469756D70207665633420746D707661725F383B0A20206869676870207665633420746D707661725F393B0A2020746D707661725F39203D2028756E6974795F4F626A656374546F576F726C64202A205F676C6573566572746578293B0A20206869676870207665633420746D707661725F31303B0A20206869676870207665633420746D707661725F31313B0A2020746D707661725F31312E77203D20312E303B0A2020746D707661725F31312E78797A203D205F676C65735665727465782E78797A3B0A2020746D707661725F3130203D2028756E6974795F4D61747269785650202A2028756E6974795F4F626A656374546F576F726C64202A20746D707661725F313129293B0A20206869676870207665633420746578636F6F72645F31323B0A2020746578636F6F72645F31322E7879203D2028285F676C65734D756C7469546578436F6F7264302E7879202A205F4D61696E5465785F53542E787929202B205F4D61696E5465785F53542E7A77293B0A20206869676870207665633220746D707661725F31333B0A202069662028285F5556536563203D3D20302E302929207B0A20202020746D707661725F3133203D20746D707661725F323B0A20207D20656C7365207B0A20202020746D707661725F3133203D20746D707661725F333B0A20207D3B0A2020746578636F6F72645F31322E7A77203D202828746D707661725F3133202A205F44657461696C416C6265646F4D61705F53542E787929202B205F44657461696C416C6265646F4D61705F53542E7A77293B0A20206869676870207665633320746D707661725F31343B0A2020746D707661725F3134203D20285F576F726C6453706163654C69676874506F73302E78797A202D2028746D707661725F392E78797A202A205F576F726C6453706163654C69676874506F73302E7729293B0A20206C696768744469725F36203D20746D707661725F31343B0A20206D656469756D70207665633320746D707661725F31353B0A202068696768702076656333206E5F31363B0A20206E5F3136203D206C696768744469725F363B0A20206869676870207665633320746D707661725F31373B0A2020746D707661725F3137203D206E6F726D616C697A65286E5F3136293B0A2020746D707661725F3135203D20746D707661725F31373B0A20206C696768744469725F36203D20746D707661725F31353B0A20206869676870207665633320746D707661725F31383B0A2020746D707661725F3138203D206E6F726D616C697A652828746D707661725F392E78797A202D205F576F726C64537061636543616D657261506F7329293B0A20206579655665635F35203D20746D707661725F31383B0A202068696768702076656333206E6F726D5F31393B0A20206E6F726D5F3139203D20746D707661725F313B0A20206869676870206D61743320746D707661725F32303B0A2020746D707661725F32305B305D203D20756E6974795F576F726C64546F4F626A6563745B305D2E78797A3B0A2020746D707661725F32305B315D203D20756E6974795F576F726C64546F4F626A6563745B315D2E78797A3B0A2020746D707661725F32305B325D203D20756E6974795F576F726C64546F4F626A6563745B325D2E78797A3B0A20206869676870207665633320746D707661725F32313B0A2020746D707661725F3231203D206E6F726D616C697A6528286E6F726D5F3139202A20746D707661725F323029293B0A20206E6F726D616C576F726C645F34203D20746D707661725F32313B0A2020746D707661725F382E797A77203D20286579655665635F35202D2028322E30202A20280A20202020646F7420286E6F726D616C576F726C645F342C206579655665635F35290A2020202A206E6F726D616C576F726C645F342929293B0A2020676C5F506F736974696F6E203D20746D707661725F31303B0A2020786C765F544558434F4F524430203D20746578636F6F72645F31323B0A2020786C765F544558434F4F524431203D20746D707661725F392E78797A3B0A2020786C765F544558434F4F524432203D20746D707661725F373B0A2020786C765F544558434F4F524433203D20746D707661725F383B0A2020786C765F544558434F4F524434203D20746D707661725F31353B0A2020786C765F544558434F4F524435203D206E6F726D616C576F726C645F343B0A7D0A0A0A23656E6469660A23696664656620465241474D454E540A756E69666F726D206C6F77702076656334205F4C69676874436F6C6F72303B0A756E69666F726D2073616D706C6572324420756E6974795F4E4878526F7567686E6573733B0A756E69666F726D206D656469756D702076656334205F436F6C6F723B0A756E69666F726D2073616D706C65723244205F4D61696E5465783B0A756E69666F726D206D656469756D7020666C6F6174205F4D6574616C6C69633B0A756E69666F726D206D656469756D7020666C6F6174205F476C6F7373696E6573733B0A756E69666F726D206C6F77702073616D706C657243756265205F4C6967687454657874757265303B0A756E69666F726D206869676870206D61743420756E6974795F576F726C64546F4C696768743B0A756E69666F726D2073616D706C65723244205F4C696768745465787475726542303B0A76617279696E67206869676870207665633420786C765F544558434F4F5244303B0A76617279696E67206869676870207665633320786C765F544558434F4F5244313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244333B0A76617279696E67206D656469756D70207665633320786C765F544558434F4F5244343B0A76617279696E67206D656469756D70207665633320786C765F544558434F4F5244353B0A766F6964206D61696E2028290A7B0A202068696768702076656333206C69676874436F6F72645F313B0A20206D656469756D70207665633320635F323B0A20206C6F7770207665633420746D707661725F333B0A2020746D707661725F33203D2074657874757265324420285F4D61696E5465782C20786C765F544558434F4F5244302E7879293B0A20206D656469756D70207665633320746D707661725F343B0A2020746D707661725F34203D20285F436F6C6F722E78797A202A20746D707661725F332E78797A293B0A20206D656469756D7020666C6F617420746D707661725F353B0A2020746D707661725F35203D20646F742028786C765F544558434F4F5244332E797A772C20786C765F544558434F4F524434293B0A20206D656469756D70207665633220746D707661725F363B0A2020746D707661725F362E78203D202828746D707661725F35202A20746D707661725F3529202A2028746D707661725F35202A20746D707661725F3529293B0A2020746D707661725F362E79203D2028312E30202D205F476C6F7373696E657373293B0A20206C6F7770207665633420746D707661725F373B0A2020746D707661725F37203D207465787475726532442028756E6974795F4E4878526F7567686E6573732C20746D707661725F36293B0A2020635F32203D20282828746D707661725F34202A200A2020202028302E37373930383337202D20285F4D6574616C6C6963202A20302E3737393038333729290A202029202B20280A2020202028746D707661725F372E77202A2031362E30290A2020202A200A202020206D697820287665633328302E323230393136332C20302E323230393136332C20302E32323039313633292C20746D707661725F342C2076656333285F4D6574616C6C696329290A20202929202A205F4C69676874436F6C6F72302E78797A293B0A20206869676870207665633420746D707661725F383B0A2020746D707661725F382E77203D20312E303B0A2020746D707661725F382E78797A203D20786C765F544558434F4F5244313B0A20206C69676874436F6F72645F31203D2028756E6974795F576F726C64546F4C69676874202A20746D707661725F38292E78797A3B0A2020686967687020666C6F617420746D707661725F393B0A2020746D707661725F39203D20646F7420286C69676874436F6F72645F312C206C69676874436F6F72645F31293B0A20206C6F777020666C6F617420746D707661725F31303B0A2020746D707661725F3130203D202874657874757265324420285F4C696768745465787475726542302C207665633228746D707661725F3929292E77202A20746578747572654375626520285F4C6967687454657874757265302C206C69676874436F6F72645F31292E77293B0A2020635F32203D2028635F32202A2028746D707661725F3130202A20636C616D7020280A20202020646F742028786C765F544558434F4F5244352C20786C765F544558434F4F524434290A20202C20302E302C20312E302929293B0A20206D656469756D70207665633420746D707661725F31313B0A2020746D707661725F31312E77203D20312E303B0A2020746D707661725F31312E78797A203D20635F323B0A20206D656469756D70207665633420786C61745F7661726F75747075745F31323B0A2020786C61745F7661726F75747075745F31322E78797A203D20746D707661725F31312E78797A3B0A2020786C61745F7661726F75747075745F31322E77203D20312E303B0A2020676C5F46726167446174615B305D203D20786C61745F7661726F75747075745F31323B0A7D0A0A0A23656E6469660A00001B000000000000000100000000000000000000000000000000000000324F040C05000000200000000300000000000000000000000100000005000000504F494E54000000F61200002376657273696F6E203130300A0A236966646566205645525445580A6174747269627574652076656334205F676C65735665727465783B0A6174747269627574652076656333205F676C65734E6F726D616C3B0A6174747269627574652076656334205F676C65734D756C7469546578436F6F7264303B0A6174747269627574652076656334205F676C65734D756C7469546578436F6F7264313B0A756E69666F726D2068696768702076656333205F576F726C64537061636543616D657261506F733B0A756E69666F726D2068696768702076656334205F576F726C6453706163654C69676874506F73303B0A756E69666F726D206869676870206D61743420756E6974795F4F626A656374546F576F726C643B0A756E69666F726D206869676870206D61743420756E6974795F576F726C64546F4F626A6563743B0A756E69666F726D206869676870206D61743420756E6974795F4D617472697856503B0A756E69666F726D2068696768702076656334205F4D61696E5465785F53543B0A756E69666F726D2068696768702076656334205F44657461696C416C6265646F4D61705F53543B0A756E69666F726D206D656469756D7020666C6F6174205F55565365633B0A76617279696E67206869676870207665633420786C765F544558434F4F5244303B0A76617279696E67206D656469756D70207665633320786C765F544558434F4F5244313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244323B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244325F313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244325F323B0A76617279696E67206869676870207665633320786C765F544558434F4F5244353B0A76617279696E67206869676870207665633220786C765F544558434F4F5244363B0A766F6964206D61696E2028290A7B0A20206D656469756D70207665633320746D707661725F313B0A2020746D707661725F31203D205F676C65734E6F726D616C3B0A20206869676870207665633220746D707661725F323B0A2020746D707661725F32203D205F676C65734D756C7469546578436F6F7264302E78793B0A20206869676870207665633220746D707661725F333B0A2020746D707661725F33203D205F676C65734D756C7469546578436F6F7264312E78793B0A202068696768702076656333206C696768744469725F343B0A20206D656469756D70207665633420746D707661725F353B0A20206D656469756D70207665633420746D707661725F363B0A20206D656469756D70207665633420746D707661725F373B0A20206869676870207665633220746D707661725F383B0A20206869676870207665633420746D707661725F393B0A2020746D707661725F39203D2028756E6974795F4F626A656374546F576F726C64202A205F676C6573566572746578293B0A20206869676870207665633420746D707661725F31303B0A20206869676870207665633420746D707661725F31313B0A2020746D707661725F31312E77203D20312E303B0A2020746D707661725F31312E78797A203D205F676C65735665727465782E78797A3B0A2020746D707661725F3130203D2028756E6974795F4D61747269785650202A2028756E6974795F4F626A656374546F576F726C64202A20746D707661725F313129293B0A20206869676870207665633420746578636F6F72645F31323B0A2020746578636F6F72645F31322E7879203D2028285F676C65734D756C7469546578436F6F7264302E7879202A205F4D61696E5465785F53542E787929202B205F4D61696E5465785F53542E7A77293B0A20206869676870207665633220746D707661725F31333B0A202069662028285F5556536563203D3D20302E302929207B0A20202020746D707661725F3133203D20746D707661725F323B0A20207D20656C7365207B0A20202020746D707661725F3133203D20746D707661725F333B0A20207D3B0A2020746578636F6F72645F31322E7A77203D202828746D707661725F3133202A205F44657461696C416C6265646F4D61705F53542E787929202B205F44657461696C416C6265646F4D61705F53542E7A77293B0A20206D656469756D70207665633320746D707661725F31343B0A20206869676870207665633320746D707661725F31353B0A2020746D707661725F3135203D206E6F726D616C697A652828746D707661725F392E78797A202D205F576F726C64537061636543616D657261506F7329293B0A2020746D707661725F3134203D20746D707661725F31353B0A202068696768702076656333206E6F726D5F31363B0A20206E6F726D5F3136203D20746D707661725F313B0A20206869676870206D61743320746D707661725F31373B0A2020746D707661725F31375B305D203D20756E6974795F576F726C64546F4F626A6563745B305D2E78797A3B0A2020746D707661725F31375B315D203D20756E6974795F576F726C64546F4F626A6563745B315D2E78797A3B0A2020746D707661725F31375B325D203D20756E6974795F576F726C64546F4F626A6563745B325D2E78797A3B0A20206869676870207665633320746D707661725F31383B0A2020746D707661725F3138203D206E6F726D616C697A6528286E6F726D5F3136202A20746D707661725F313729293B0A2020746D707661725F352E78797A203D207665633328302E302C20302E302C20302E30293B0A2020746D707661725F362E78797A203D207665633328302E302C20302E302C20302E30293B0A2020746D707661725F372E78797A203D20746D707661725F31383B0A20206D656469756D70207665633320746D707661725F31393B0A20206869676870207665633320746D707661725F32303B0A2020746D707661725F3230203D206E6F726D616C697A6528285F576F726C6453706163654C69676874506F73302E78797A202D2028746D707661725F392E78797A202A205F576F726C6453706163654C69676874506F73302E772929293B0A2020746D707661725F3139203D20746D707661725F32303B0A20206C696768744469725F34203D20746D707661725F31393B0A2020746D707661725F352E77203D206C696768744469725F342E783B0A2020746D707661725F362E77203D206C696768744469725F342E793B0A2020746D707661725F372E77203D206C696768744469725F342E7A3B0A2020676C5F506F736974696F6E203D20746D707661725F31303B0A2020786C765F544558434F4F524430203D20746578636F6F72645F31323B0A2020786C765F544558434F4F524431203D20746D707661725F31343B0A2020786C765F544558434F4F524432203D20746D707661725F353B0A2020786C765F544558434F4F5244325F31203D20746D707661725F363B0A2020786C765F544558434F4F5244325F32203D20746D707661725F373B0A2020786C765F544558434F4F524435203D20746D707661725F392E78797A3B0A2020786C765F544558434F4F524436203D20746D707661725F383B0A7D0A0A0A23656E6469660A23696664656620465241474D454E540A756E69666F726D206C6F77702076656334205F4C69676874436F6C6F72303B0A756E69666F726D2073616D706C6572324420756E6974795F4E4878526F7567686E6573733B0A756E69666F726D206D656469756D702076656334205F436F6C6F723B0A756E69666F726D2073616D706C65723244205F4D61696E5465783B0A756E69666F726D206D656469756D7020666C6F6174205F4D6574616C6C69633B0A756E69666F726D206D656469756D7020666C6F6174205F476C6F7373696E6573733B0A756E69666F726D2073616D706C65723244205F4C6967687454657874757265303B0A756E69666F726D206869676870206D61743420756E6974795F576F726C64546F4C696768743B0A76617279696E67206869676870207665633420786C765F544558434F4F5244303B0A76617279696E67206D656469756D70207665633320786C765F544558434F4F5244313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244323B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244325F313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244325F323B0A76617279696E67206869676870207665633320786C765F544558434F4F5244353B0A766F6964206D61696E2028290A7B0A202068696768702076656333206C69676874436F6F72645F313B0A20206C6F7770207665633420746D707661725F323B0A2020746D707661725F32203D2074657874757265324420285F4D61696E5465782C20786C765F544558434F4F5244302E7879293B0A20206D656469756D70207665633320746D707661725F333B0A2020746D707661725F33203D20285F436F6C6F722E78797A202A20746D707661725F322E78797A293B0A20206D656469756D70207665633320746D707661725F343B0A2020746D707661725F34203D206E6F726D616C697A6528786C765F544558434F4F5244325F322E78797A293B0A20206869676870207665633420746D707661725F353B0A2020746D707661725F352E77203D20312E303B0A2020746D707661725F352E78797A203D20786C765F544558434F4F5244353B0A20206C69676874436F6F72645F31203D2028756E6974795F576F726C64546F4C69676874202A20746D707661725F35292E78797A3B0A2020686967687020666C6F617420746D707661725F363B0A2020746D707661725F36203D20646F7420286C69676874436F6F72645F312C206C69676874436F6F72645F31293B0A20206C6F777020666C6F617420746D707661725F373B0A2020746D707661725F37203D2074657874757265324420285F4C6967687454657874757265302C207665633228746D707661725F3629292E773B0A20206D656469756D70207665633320746D707661725F383B0A2020746D707661725F382E78203D20786C765F544558434F4F5244322E773B0A2020746D707661725F382E79203D20786C765F544558434F4F5244325F312E773B0A2020746D707661725F382E7A203D20786C765F544558434F4F5244325F322E773B0A20206D656469756D7020666C6F617420617474656E5F393B0A2020617474656E5F39203D20746D707661725F373B0A20206D656469756D70207665633320746D707661725F31303B0A2020746D707661725F3130203D205F4C69676874436F6C6F72302E78797A3B0A2020746D707661725F3130203D2028746D707661725F3130202A20617474656E5F39293B0A20206D656469756D70207665633320766965774469725F31313B0A2020766965774469725F3131203D202D28786C765F544558434F4F524431293B0A20206D656469756D70207665633220746D707661725F31323B0A2020746D707661725F31322E78203D20646F74202828766965774469725F3131202D2028322E30202A200A2020202028646F742028746D707661725F342C20766965774469725F313129202A20746D707661725F34290A202029292C20746D707661725F38293B0A2020746D707661725F31322E79203D2028312E30202D20636C616D702028646F742028746D707661725F342C20766965774469725F3131292C20302E302C20312E3029293B0A20206D656469756D70207665633220746D707661725F31333B0A2020746D707661725F31332E78203D202828746D707661725F3132202A20746D707661725F313229202A2028746D707661725F3132202A20746D707661725F313229292E783B0A2020746D707661725F31332E79203D2028312E30202D205F476C6F7373696E657373293B0A20206C6F7770207665633420746D707661725F31343B0A2020746D707661725F3134203D207465787475726532442028756E6974795F4E4878526F7567686E6573732C20746D707661725F3133293B0A20206D656469756D70207665633420746D707661725F31353B0A2020746D707661725F31352E77203D20312E303B0A2020746D707661725F31352E78797A203D20282828746D707661725F33202A200A2020202028302E37373930383337202D20285F4D6574616C6C6963202A20302E3737393038333729290A202029202B20280A2020202028746D707661725F31342E77202A2031362E30290A2020202A200A202020206D697820287665633328302E323230393136332C20302E323230393136332C20302E32323039313633292C20746D707661725F332C2076656333285F4D6574616C6C696329290A20202929202A2028746D707661725F3130202A20636C616D7020280A20202020646F742028746D707661725F342C20746D707661725F38290A20202C20302E302C20312E302929293B0A20206D656469756D70207665633420786C61745F7661726F75747075745F31363B0A2020786C61745F7661726F75747075745F31362E78797A203D20746D707661725F31352E78797A3B0A2020786C61745F7661726F75747075745F31362E77203D20312E303B0A2020676C5F46726167446174615B305D203D20786C61745F7661726F75747075745F31363B0A7D0A0A0A23656E6469660A00001B000000000000000100000000000000000000000000000000000000324F040C0500000043000000040000000100000000000000020000000400000053504F540D000000534841444F57535F4445505448000000A11B00002376657273696F6E203130300A0A236966646566205645525445580A6174747269627574652076656334205F676C65735665727465783B0A6174747269627574652076656333205F676C65734E6F726D616C3B0A6174747269627574652076656334205F676C65734D756C7469546578436F6F7264303B0A6174747269627574652076656334205F676C65734D756C7469546578436F6F7264313B0A756E69666F726D2068696768702076656333205F576F726C64537061636543616D657261506F733B0A756E69666F726D2068696768702076656334205F576F726C6453706163654C69676874506F73303B0A756E69666F726D206869676870206D61743420756E6974795F4F626A656374546F576F726C643B0A756E69666F726D206869676870206D61743420756E6974795F576F726C64546F4F626A6563743B0A756E69666F726D206869676870206D61743420756E6974795F4D617472697856503B0A756E69666F726D2068696768702076656334205F4D61696E5465785F53543B0A756E69666F726D2068696768702076656334205F44657461696C416C6265646F4D61705F53543B0A756E69666F726D206D656469756D7020666C6F6174205F55565365633B0A76617279696E67206869676870207665633420786C765F544558434F4F5244303B0A76617279696E67206D656469756D70207665633320786C765F544558434F4F5244313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244323B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244325F313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244325F323B0A76617279696E67206869676870207665633320786C765F544558434F4F5244353B0A76617279696E67206869676870207665633220786C765F544558434F4F5244363B0A766F6964206D61696E2028290A7B0A20206D656469756D70207665633320746D707661725F313B0A2020746D707661725F31203D205F676C65734E6F726D616C3B0A20206869676870207665633220746D707661725F323B0A2020746D707661725F32203D205F676C65734D756C7469546578436F6F7264302E78793B0A20206869676870207665633220746D707661725F333B0A2020746D707661725F33203D205F676C65734D756C7469546578436F6F7264312E78793B0A202068696768702076656333206C696768744469725F343B0A20206D656469756D70207665633420746D707661725F353B0A20206D656469756D70207665633420746D707661725F363B0A20206D656469756D70207665633420746D707661725F373B0A20206869676870207665633220746D707661725F383B0A20206869676870207665633420746D707661725F393B0A2020746D707661725F39203D2028756E6974795F4F626A656374546F576F726C64202A205F676C6573566572746578293B0A20206869676870207665633420746D707661725F31303B0A20206869676870207665633420746D707661725F31313B0A2020746D707661725F31312E77203D20312E303B0A2020746D707661725F31312E78797A203D205F676C65735665727465782E78797A3B0A2020746D707661725F3130203D2028756E6974795F4D61747269785650202A2028756E6974795F4F626A656374546F576F726C64202A20746D707661725F313129293B0A20206869676870207665633420746578636F6F72645F31323B0A2020746578636F6F72645F31322E7879203D2028285F676C65734D756C7469546578436F6F7264302E7879202A205F4D61696E5465785F53542E787929202B205F4D61696E5465785F53542E7A77293B0A20206869676870207665633220746D707661725F31333B0A202069662028285F5556536563203D3D20302E302929207B0A20202020746D707661725F3133203D20746D707661725F323B0A20207D20656C7365207B0A20202020746D707661725F3133203D20746D707661725F333B0A20207D3B0A2020746578636F6F72645F31322E7A77203D202828746D707661725F3133202A205F44657461696C416C6265646F4D61705F53542E787929202B205F44657461696C416C6265646F4D61705F53542E7A77293B0A20206D656469756D70207665633320746D707661725F31343B0A202068696768702076656333206E5F31353B0A20206E5F3135203D2028746D707661725F392E78797A202D205F576F726C64537061636543616D657261506F73293B0A2020746D707661725F3134203D206E5F31353B0A202068696768702076656333206E6F726D5F31363B0A20206E6F726D5F3136203D20746D707661725F313B0A20206869676870206D61743320746D707661725F31373B0A2020746D707661725F31375B305D203D20756E6974795F576F726C64546F4F626A6563745B305D2E78797A3B0A2020746D707661725F31375B315D203D20756E6974795F576F726C64546F4F626A6563745B315D2E78797A3B0A2020746D707661725F31375B325D203D20756E6974795F576F726C64546F4F626A6563745B325D2E78797A3B0A20206869676870207665633320746D707661725F31383B0A2020746D707661725F3138203D206E6F726D616C697A6528286E6F726D5F3136202A20746D707661725F313729293B0A2020746D707661725F352E78797A203D207665633328302E302C20302E302C20302E30293B0A2020746D707661725F362E78797A203D207665633328302E302C20302E302C20302E30293B0A2020746D707661725F372E78797A203D20746D707661725F31383B0A20206869676870207665633320746D707661725F31393B0A2020746D707661725F3139203D20285F576F726C6453706163654C69676874506F73302E78797A202D2028746D707661725F392E78797A202A205F576F726C6453706163654C69676874506F73302E7729293B0A20206D656469756D70207665633320746D707661725F32303B0A2020746D707661725F3230203D20746D707661725F31393B0A20206C696768744469725F34203D20746D707661725F32303B0A2020746D707661725F352E77203D206C696768744469725F342E783B0A2020746D707661725F362E77203D206C696768744469725F342E793B0A2020746D707661725F372E77203D206C696768744469725F342E7A3B0A2020676C5F506F736974696F6E203D20746D707661725F31303B0A2020786C765F544558434F4F524430203D20746578636F6F72645F31323B0A2020786C765F544558434F4F524431203D20746D707661725F31343B0A2020786C765F544558434F4F524432203D20746D707661725F353B0A2020786C765F544558434F4F5244325F31203D20746D707661725F363B0A2020786C765F544558434F4F5244325F32203D20746D707661725F373B0A2020786C765F544558434F4F524435203D20746D707661725F392E78797A3B0A2020786C765F544558434F4F524436203D20746D707661725F383B0A7D0A0A0A23656E6469660A23696664656620465241474D454E540A756E69666F726D2068696768702076656333205F576F726C64537061636543616D657261506F733B0A756E69666F726D206869676870206D61743420756E6974795F576F726C64546F536861646F775B345D3B0A756E69666F726D206D656469756D702076656334205F4C69676874536861646F77446174613B0A756E69666F726D206869676870207665633420756E6974795F536861646F774661646543656E746572416E64547970653B0A756E69666F726D206869676870206D61743420756E6974795F4D6174726978563B0A756E69666F726D206C6F77702076656334205F4C69676874436F6C6F72303B0A756E69666F726D2068696768702073616D706C65723244205F536861646F774D6170546578747572653B0A756E69666F726D206D656469756D702076656334205F436F6C6F723B0A756E69666F726D2073616D706C65723244205F4D61696E5465783B0A756E69666F726D206D656469756D7020666C6F6174205F4D6574616C6C69633B0A756E69666F726D206D656469756D7020666C6F6174205F476C6F7373696E6573733B0A756E69666F726D2073616D706C65723244205F4C6967687454657874757265303B0A756E69666F726D206869676870206D61743420756E6974795F576F726C64546F4C696768743B0A756E69666F726D2073616D706C65723244205F4C696768745465787475726542303B0A76617279696E67206869676870207665633420786C765F544558434F4F5244303B0A76617279696E67206D656469756D70207665633320786C765F544558434F4F5244313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244323B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244325F313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244325F323B0A76617279696E67206869676870207665633320786C765F544558434F4F5244353B0A766F6964206D61696E2028290A7B0A20206C6F777020666C6F617420617474656E5F313B0A20206C6F777020666C6F617420736861646F775F323B0A20206C6F7770207665633420746D707661725F333B0A2020746D707661725F33203D2074657874757265324420285F4D61696E5465782C20786C765F544558434F4F5244302E7879293B0A20206D656469756D70207665633320746D707661725F343B0A2020746D707661725F34203D20285F436F6C6F722E78797A202A20746D707661725F332E78797A293B0A20206D656469756D70207665633320746D707661725F353B0A20206D656469756D70207665633320746D707661725F363B0A2020746D707661725F36203D206D697820287665633328302E323230393136332C20302E323230393136332C20302E32323039313633292C20746D707661725F342C2076656333285F4D6574616C6C696329293B0A2020746D707661725F35203D2028746D707661725F34202A2028302E37373930383337202D20285F4D6574616C6C6963202A20302E373739303833372929293B0A20206D656469756D70207665633320746D707661725F373B0A2020746D707661725F37203D206E6F726D616C697A6528786C765F544558434F4F5244325F322E78797A293B0A20206D656469756D70207665633320746D707661725F383B0A2020746D707661725F38203D206E6F726D616C697A6528786C765F544558434F4F524431293B0A20206869676870207665633420746D707661725F393B0A2020746D707661725F392E77203D20312E303B0A2020746D707661725F392E78797A203D20786C765F544558434F4F5244353B0A20206869676870207665633420746D707661725F31303B0A2020746D707661725F3130203D2028756E6974795F576F726C64546F4C69676874202A20746D707661725F39293B0A20206D656469756D7020666C6F6174207265616C74696D65536861646F77417474656E756174696F6E5F31313B0A20206869676870207665633420765F31323B0A2020765F31322E78203D20756E6974795F4D6174726978565B305D2E7A3B0A2020765F31322E79203D20756E6974795F4D6174726978565B315D2E7A3B0A2020765F31322E7A203D20756E6974795F4D6174726978565B325D2E7A3B0A2020765F31322E77203D20756E6974795F4D6174726978565B335D2E7A3B0A20206869676870207665633320746D707661725F31333B0A2020746D707661725F3133203D2028786C765F544558434F4F524435202D20756E6974795F536861646F774661646543656E746572416E64547970652E78797A293B0A20206D656469756D7020666C6F617420746D707661725F31343B0A2020686967687020666C6F617420746D707661725F31353B0A2020746D707661725F3135203D20636C616D70202828280A202020206D69782028646F742028285F576F726C64537061636543616D657261506F73202D20786C765F544558434F4F524435292C20765F31322E78797A292C207371727428646F742028746D707661725F31332C20746D707661725F313329292C20756E6974795F536861646F774661646543656E746572416E64547970652E77290A2020202A205F4C69676874536861646F77446174612E7A29202B205F4C69676874536861646F77446174612E77292C20302E302C20312E30293B0A2020746D707661725F3134203D20746D707661725F31353B0A20207265616C74696D65536861646F77417474656E756174696F6E5F3131203D20312E303B0A20206869676870207665633420746D707661725F31363B0A2020746D707661725F31362E77203D20312E303B0A2020746D707661725F31362E78797A203D20786C765F544558434F4F5244353B0A20206869676870207665633420746D707661725F31373B0A2020746D707661725F3137203D2028756E6974795F576F726C64546F536861646F775B305D202A20746D707661725F3136293B0A20206C6F777020666C6F617420746D707661725F31383B0A20206869676870207665633420746D707661725F31393B0A2020746D707661725F3139203D2074657874757265324450726F6A20285F536861646F774D6170546578747572652C20746D707661725F3137293B0A20206D656469756D7020666C6F617420746D707661725F32303B0A20206966202828746D707661725F31392E78203C2028746D707661725F31372E7A202F20746D707661725F31372E77292929207B0A20202020746D707661725F3230203D205F4C69676874536861646F77446174612E783B0A20207D20656C7365207B0A20202020746D707661725F3230203D20312E303B0A20207D3B0A2020746D707661725F3138203D20746D707661725F32303B0A20207265616C74696D65536861646F77417474656E756174696F6E5F3131203D20746D707661725F31383B0A20206D656469756D7020666C6F617420746D707661725F32313B0A2020746D707661725F3231203D20636C616D702028287265616C74696D65536861646F77417474656E756174696F6E5F3131202B20746D707661725F3134292C20302E302C20312E30293B0A2020736861646F775F32203D20746D707661725F32313B0A20206C6F7770207665633420746D707661725F32323B0A20206869676870207665633220505F32333B0A2020505F3233203D202828746D707661725F31302E7879202F20746D707661725F31302E7729202B20302E35293B0A2020746D707661725F3232203D2074657874757265324420285F4C6967687454657874757265302C20505F3233293B0A2020686967687020666C6F617420746D707661725F32343B0A2020746D707661725F3234203D20646F742028746D707661725F31302E78797A2C20746D707661725F31302E78797A293B0A20206C6F7770207665633420746D707661725F32353B0A2020746D707661725F3235203D2074657874757265324420285F4C696768745465787475726542302C207665633228746D707661725F323429293B0A2020686967687020666C6F617420746D707661725F32363B0A2020746D707661725F3236203D202828280A20202020666C6F61742828746D707661725F31302E7A203E20302E3029290A2020202A20746D707661725F32322E7729202A20746D707661725F32352E7729202A20736861646F775F32293B0A2020617474656E5F31203D20746D707661725F32363B0A20206D656469756D70207665633320746D707661725F32373B0A2020746D707661725F32372E78203D20786C765F544558434F4F5244322E773B0A2020746D707661725F32372E79203D20786C765F544558434F4F5244325F312E773B0A2020746D707661725F32372E7A203D20786C765F544558434F4F5244325F322E773B0A20206D656469756D7020666C6F617420617474656E5F32383B0A2020617474656E5F3238203D20617474656E5F313B0A20206D656469756D70207665633320746D707661725F32393B0A2020746D707661725F3239203D205F4C69676874436F6C6F72302E78797A3B0A20206D656469756D70207665633320746D707661725F33303B0A2020746D707661725F3330203D206E6F726D616C697A6528746D707661725F3237293B0A2020746D707661725F3239203D2028746D707661725F3239202A20617474656E5F3238293B0A20206D656469756D7020666C6F61742073706563756C61725465726D5F33313B0A20206D656469756D70207665633320746D707661725F33323B0A20206D656469756D70207665633320696E5665635F33333B0A2020696E5665635F3333203D2028746D707661725F3330202D20746D707661725F38293B0A2020746D707661725F3332203D2028696E5665635F3333202A20696E766572736573717274286D61782028302E3030312C200A20202020646F742028696E5665635F33332C20696E5665635F3333290A20202929293B0A20206D656469756D7020666C6F617420746D707661725F33343B0A2020746D707661725F3334203D20636C616D702028646F742028746D707661725F372C20746D707661725F3332292C20302E302C20312E30293B0A20206D656469756D7020666C6F617420746D707661725F33353B0A2020746D707661725F3335203D2028312E30202D205F476C6F7373696E657373293B0A20206D656469756D7020666C6F617420746D707661725F33363B0A2020746D707661725F3336203D2028746D707661725F3335202A20746D707661725F3335293B0A202073706563756C61725465726D5F3331203D202828746D707661725F3336202F20280A20202020286D61782028302E33322C20636C616D702028646F742028746D707661725F33302C20746D707661725F3332292C20302E302C20312E302929202A2028312E35202B20746D707661725F333629290A2020202A200A20202020282828746D707661725F3334202A20746D707661725F333429202A202828746D707661725F3336202A20746D707661725F333629202D20312E302929202B20312E3030303031290A20202929202D20302E30303031293B0A20206D656469756D7020666C6F617420746D707661725F33373B0A2020746D707661725F3337203D20636C616D70202873706563756C61725465726D5F33312C20302E302C203130302E30293B0A202073706563756C61725465726D5F3331203D20746D707661725F33373B0A20206D656469756D70207665633420746D707661725F33383B0A2020746D707661725F33382E77203D20312E303B0A2020746D707661725F33382E78797A203D20282828746D707661725F35202B200A2020202028746D707661725F3337202A20746D707661725F36290A202029202A20746D707661725F323929202A20636C616D702028646F742028746D707661725F372C20746D707661725F3330292C20302E302C20312E3029293B0A20206D656469756D70207665633420786C61745F7661726F75747075745F33393B0A2020786C61745F7661726F75747075745F33392E78797A203D20746D707661725F33382E78797A3B0A2020786C61745F7661726F75747075745F33392E77203D20312E303B0A2020676C5F46726167446174615B305D203D20786C61745F7661726F75747075745F33393B0A7D0A0A0A23656E6469660A0000001B000000000000000100000000000000000000000000000000000000324F040C0500000023000000040000000000000000000000020000000B000000444952454354494F4E414C000E0000005645525445584C494748545F4F4E0000452000002376657273696F6E203130300A0A236966646566205645525445580A6174747269627574652076656334205F676C65735665727465783B0A6174747269627574652076656333205F676C65734E6F726D616C3B0A6174747269627574652076656334205F676C65734D756C7469546578436F6F7264303B0A6174747269627574652076656334205F676C65734D756C7469546578436F6F7264313B0A756E69666F726D2068696768702076656333205F576F726C64537061636543616D657261506F733B0A756E69666F726D206869676870207665633420756E6974795F344C69676874506F7358303B0A756E69666F726D206869676870207665633420756E6974795F344C69676874506F7359303B0A756E69666F726D206869676870207665633420756E6974795F344C69676874506F735A303B0A756E69666F726D206D656469756D70207665633420756E6974795F344C69676874417474656E303B0A756E69666F726D206D656469756D70207665633420756E6974795F4C69676874436F6C6F725B385D3B0A756E69666F726D206D656469756D70207665633420756E6974795F534841723B0A756E69666F726D206D656469756D70207665633420756E6974795F534841673B0A756E69666F726D206D656469756D70207665633420756E6974795F534841623B0A756E69666F726D206D656469756D70207665633420756E6974795F534842723B0A756E69666F726D206D656469756D70207665633420756E6974795F534842673B0A756E69666F726D206D656469756D70207665633420756E6974795F534842623B0A756E69666F726D206D656469756D70207665633420756E6974795F5348433B0A756E69666F726D206869676870206D61743420756E6974795F4F626A656374546F576F726C643B0A756E69666F726D206869676870206D61743420756E6974795F576F726C64546F4F626A6563743B0A756E69666F726D206869676870206D61743420756E6974795F4D617472697856503B0A756E69666F726D2068696768702076656334205F4D61696E5465785F53543B0A756E69666F726D2068696768702076656334205F44657461696C416C6265646F4D61705F53543B0A756E69666F726D206D656469756D7020666C6F6174205F4D6574616C6C69633B0A756E69666F726D206D656469756D7020666C6F6174205F476C6F7373696E6573733B0A756E69666F726D206D656469756D7020666C6F6174205F55565365633B0A76617279696E67206869676870207665633420786C765F544558434F4F5244303B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244323B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244343B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244353B0A766F6964206D61696E2028290A7B0A20206D656469756D70207665633320746D707661725F313B0A2020746D707661725F31203D205F676C65734E6F726D616C3B0A20206869676870207665633220746D707661725F323B0A2020746D707661725F32203D205F676C65734D756C7469546578436F6F7264302E78793B0A20206869676870207665633220746D707661725F333B0A2020746D707661725F33203D205F676C65734D756C7469546578436F6F7264312E78793B0A20206D656469756D702076656333206E6F726D616C576F726C645F343B0A20206D656469756D702076656333206579655665635F353B0A20206D656469756D70207665633420746D707661725F363B0A20206D656469756D70207665633420746D707661725F373B0A20206D656469756D70207665633420746D707661725F383B0A20206869676870207665633420746D707661725F393B0A2020746D707661725F39203D2028756E6974795F4F626A656374546F576F726C64202A205F676C6573566572746578293B0A20206869676870207665633420746D707661725F31303B0A20206869676870207665633420746D707661725F31313B0A2020746D707661725F31312E77203D20312E303B0A2020746D707661725F31312E78797A203D205F676C65735665727465782E78797A3B0A2020746D707661725F3130203D2028756E6974795F4D61747269785650202A2028756E6974795F4F626A656374546F576F726C64202A20746D707661725F313129293B0A20206869676870207665633420746578636F6F72645F31323B0A2020746578636F6F72645F31322E7879203D2028285F676C65734D756C7469546578436F6F7264302E7879202A205F4D61696E5465785F53542E787929202B205F4D61696E5465785F53542E7A77293B0A20206869676870207665633220746D707661725F31333B0A202069662028285F5556536563203D3D20302E302929207B0A20202020746D707661725F3133203D20746D707661725F323B0A20207D20656C7365207B0A20202020746D707661725F3133203D20746D707661725F333B0A20207D3B0A2020746578636F6F72645F31322E7A77203D202828746D707661725F3133202A205F44657461696C416C6265646F4D61705F53542E787929202B205F44657461696C416C6265646F4D61705F53542E7A77293B0A20206869676870207665633320746D707661725F31343B0A2020746D707661725F3134203D206E6F726D616C697A652828746D707661725F392E78797A202D205F576F726C64537061636543616D657261506F7329293B0A20206579655665635F35203D20746D707661725F31343B0A202068696768702076656333206E6F726D5F31353B0A20206E6F726D5F3135203D20746D707661725F313B0A20206869676870206D61743320746D707661725F31363B0A2020746D707661725F31365B305D203D20756E6974795F576F726C64546F4F626A6563745B305D2E78797A3B0A2020746D707661725F31365B315D203D20756E6974795F576F726C64546F4F626A6563745B315D2E78797A3B0A2020746D707661725F31365B325D203D20756E6974795F576F726C64546F4F626A6563745B325D2E78797A3B0A20206869676870207665633320746D707661725F31373B0A2020746D707661725F3137203D206E6F726D616C697A6528286E6F726D5F3135202A20746D707661725F313629293B0A20206E6F726D616C576F726C645F34203D20746D707661725F31373B0A2020746D707661725F382E78797A203D206E6F726D616C576F726C645F343B0A2020746D707661725F362E78797A203D206579655665635F353B0A20206D656469756D70207665633420616D6269656E744F724C696768746D617055565F31383B0A2020616D6269656E744F724C696768746D617055565F31382E77203D20302E303B0A202068696768702076656333206C69676874436F6C6F72305F31393B0A20206C69676874436F6C6F72305F3139203D20756E6974795F4C69676874436F6C6F725B305D2E78797A3B0A202068696768702076656333206C69676874436F6C6F72315F32303B0A20206C69676874436F6C6F72315F3230203D20756E6974795F4C69676874436F6C6F725B315D2E78797A3B0A202068696768702076656333206C69676874436F6C6F72325F32313B0A20206C69676874436F6C6F72325F3231203D20756E6974795F4C69676874436F6C6F725B325D2E78797A3B0A202068696768702076656333206C69676874436F6C6F72335F32323B0A20206C69676874436F6C6F72335F3232203D20756E6974795F4C69676874436F6C6F725B335D2E78797A3B0A202068696768702076656334206C69676874417474656E53715F32333B0A20206C69676874417474656E53715F3233203D20756E6974795F344C69676874417474656E303B0A202068696768702076656333206E6F726D616C5F32343B0A20206E6F726D616C5F3234203D206E6F726D616C576F726C645F343B0A20206869676870207665633320636F6C5F32353B0A202068696768702076656334206E646F746C5F32363B0A202068696768702076656334206C656E67746853715F32373B0A20206869676870207665633420746D707661725F32383B0A2020746D707661725F3238203D2028756E6974795F344C69676874506F735830202D20746D707661725F392E78293B0A20206869676870207665633420746D707661725F32393B0A2020746D707661725F3239203D2028756E6974795F344C69676874506F735930202D20746D707661725F392E79293B0A20206869676870207665633420746D707661725F33303B0A2020746D707661725F3330203D2028756E6974795F344C69676874506F735A30202D20746D707661725F392E7A293B0A20206C656E67746853715F3237203D2028746D707661725F3238202A20746D707661725F3238293B0A20206C656E67746853715F3237203D20286C656E67746853715F3237202B2028746D707661725F3239202A20746D707661725F323929293B0A20206C656E67746853715F3237203D20286C656E67746853715F3237202B2028746D707661725F3330202A20746D707661725F333029293B0A20206869676870207665633420746D707661725F33313B0A2020746D707661725F3331203D206D617820286C656E67746853715F32372C20766563342831652D30362C2031652D30362C2031652D30362C2031652D303629293B0A20206C656E67746853715F3237203D20746D707661725F33313B0A20206E646F746C5F3236203D2028746D707661725F3238202A206E6F726D616C5F32342E78293B0A20206E646F746C5F3236203D20286E646F746C5F3236202B2028746D707661725F3239202A206E6F726D616C5F32342E7929293B0A20206E646F746C5F3236203D20286E646F746C5F3236202B2028746D707661725F3330202A206E6F726D616C5F32342E7A29293B0A20206869676870207665633420746D707661725F33323B0A2020746D707661725F3332203D206D617820287665633428302E302C20302E302C20302E302C20302E30292C20286E646F746C5F3236202A20696E76657273657371727428746D707661725F33312929293B0A20206E646F746C5F3236203D20746D707661725F33323B0A20206869676870207665633420746D707661725F33333B0A2020746D707661725F3333203D2028746D707661725F3332202A2028312E302F2828312E30202B200A2020202028746D707661725F3331202A206C69676874417474656E53715F3233290A2020292929293B0A2020636F6C5F3235203D20286C69676874436F6C6F72305F3139202A20746D707661725F33332E78293B0A2020636F6C5F3235203D2028636F6C5F3235202B20286C69676874436F6C6F72315F3230202A20746D707661725F33332E7929293B0A2020636F6C5F3235203D2028636F6C5F3235202B20286C69676874436F6C6F72325F3231202A20746D707661725F33332E7A29293B0A2020636F6C5F3235203D2028636F6C5F3235202B20286C69676874436F6C6F72335F3232202A20746D707661725F33332E7729293B0A2020616D6269656E744F724C696768746D617055565F31382E78797A203D20636F6C5F32353B0A20206D656469756D70207665633420746D707661725F33343B0A2020746D707661725F33342E77203D20312E303B0A2020746D707661725F33342E78797A203D206E6F726D616C576F726C645F343B0A20206D656469756D702076656333207265735F33353B0A20206D656469756D70207665633320785F33363B0A2020785F33362E78203D20646F742028756E6974795F534841722C20746D707661725F3334293B0A2020785F33362E79203D20646F742028756E6974795F534841672C20746D707661725F3334293B0A2020785F33362E7A203D20646F742028756E6974795F534841622C20746D707661725F3334293B0A20206D656469756D7020766563332078315F33373B0A20206D656469756D70207665633420746D707661725F33383B0A2020746D707661725F3338203D20286E6F726D616C576F726C645F342E78797A7A202A206E6F726D616C576F726C645F342E797A7A78293B0A202078315F33372E78203D20646F742028756E6974795F534842722C20746D707661725F3338293B0A202078315F33372E79203D20646F742028756E6974795F534842672C20746D707661725F3338293B0A202078315F33372E7A203D20646F742028756E6974795F534842622C20746D707661725F3338293B0A20207265735F3335203D2028785F3336202B202878315F3337202B2028756E6974795F5348432E78797A202A200A2020202028286E6F726D616C576F726C645F342E78202A206E6F726D616C576F726C645F342E7829202D20286E6F726D616C576F726C645F342E79202A206E6F726D616C576F726C645F342E7929290A20202929293B0A20206D656469756D70207665633320746D707661725F33393B0A2020746D707661725F3339203D206D617820282828312E303535202A200A20202020706F7720286D617820287265735F33352C207665633328302E302C20302E302C20302E3029292C207665633328302E343136363636372C20302E343136363636372C20302E3431363636363729290A202029202D20302E303535292C207665633328302E302C20302E302C20302E3029293B0A20207265735F3335203D20746D707661725F33393B0A2020616D6269656E744F724C696768746D617055565F31382E78797A203D2028616D6269656E744F724C696768746D617055565F31382E78797A202B206D617820287665633328302E302C20302E302C20302E30292C20746D707661725F333929293B0A2020746D707661725F372E797A77203D20286579655665635F35202D2028322E30202A20280A20202020646F7420286E6F726D616C576F726C645F342C206579655665635F35290A2020202A206E6F726D616C576F726C645F342929293B0A20206D656469756D7020666C6F617420785F34303B0A2020785F3430203D2028312E30202D20636C616D702028646F7420286E6F726D616C576F726C645F342C200A202020202D286579655665635F35290A2020292C20302E302C20312E3029293B0A2020746D707661725F382E77203D202828785F3430202A20785F343029202A2028785F3430202A20785F343029293B0A2020746D707661725F362E77203D20636C616D702028285F476C6F7373696E657373202B2028312E30202D200A2020202028302E37373930383337202D20285F4D6574616C6C6963202A20302E3737393038333729290A202029292C20302E302C20312E30293B0A2020676C5F506F736974696F6E203D20746D707661725F31303B0A2020786C765F544558434F4F524430203D20746578636F6F72645F31323B0A2020786C765F544558434F4F524431203D20746D707661725F363B0A2020786C765F544558434F4F524432203D20616D6269656E744F724C696768746D617055565F31383B0A2020786C765F544558434F4F524434203D20746D707661725F373B0A2020786C765F544558434F4F524435203D20746D707661725F383B0A7D0A0A0A23656E6469660A23696664656620465241474D454E540A23657874656E73696F6E20474C5F4558545F7368616465725F746578747572655F6C6F64203A20656E61626C650A6C6F7770207665633420696D706C5F6C6F775F74657874757265437562654C6F64455854286C6F77702073616D706C6572437562652073616D706C65722C206869676870207665633320636F6F72642C206D656469756D7020666C6F6174206C6F64290A7B0A23696620646566696E656428474C5F4558545F7368616465725F746578747572655F6C6F64290A0972657475726E2074657874757265437562654C6F644558542873616D706C65722C20636F6F72642C206C6F64293B0A23656C73650A0972657475726E2074657874757265437562652873616D706C65722C20636F6F72642C206C6F64293B0A23656E6469660A7D0A0A756E69666F726D206D656469756D702076656334205F576F726C6453706163654C69676874506F73303B0A756E69666F726D206C6F77702073616D706C65724375626520756E6974795F5370656343756265303B0A756E69666F726D206D656469756D70207665633420756E6974795F5370656343756265305F4844523B0A756E69666F726D206C6F77702076656334205F4C69676874436F6C6F72303B0A756E69666F726D2073616D706C6572324420756E6974795F4E4878526F7567686E6573733B0A756E69666F726D206D656469756D702076656334205F436F6C6F723B0A756E69666F726D2073616D706C65723244205F4D61696E5465783B0A756E69666F726D206D656469756D7020666C6F6174205F4D6574616C6C69633B0A756E69666F726D206D656469756D7020666C6F6174205F476C6F7373696E6573733B0A756E69666F726D2073616D706C65723244205F4F63636C7573696F6E4D61703B0A76617279696E67206869676870207665633420786C765F544558434F4F5244303B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244323B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244343B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244353B0A766F6964206D61696E2028290A7B0A20206D656469756D7020666C6F617420726C5F313B0A20206C6F7770207665633420746D707661725F323B0A2020746D707661725F32203D2074657874757265324420285F4D61696E5465782C20786C765F544558434F4F5244302E7879293B0A20206D656469756D70207665633320746D707661725F333B0A2020746D707661725F33203D20285F436F6C6F722E78797A202A20746D707661725F322E78797A293B0A20206D656469756D70207665633320746D707661725F343B0A20206D656469756D70207665633320746D707661725F353B0A2020746D707661725F35203D206D697820287665633328302E323230393136332C20302E323230393136332C20302E32323039313633292C20746D707661725F332C2076656333285F4D6574616C6C696329293B0A2020746D707661725F34203D2028746D707661725F33202A2028302E37373930383337202D20285F4D6574616C6C6963202A20302E373739303833372929293B0A20206D656469756D70207665633320746D707661725F363B0A2020746D707661725F36203D205F4C69676874436F6C6F72302E78797A3B0A20206D656469756D7020666C6F617420746D707661725F373B0A20206C6F7770207665633420746D707661725F383B0A2020746D707661725F38203D2074657874757265324420285F4F63636C7573696F6E4D61702C20786C765F544558434F4F5244302E7879293B0A2020746D707661725F37203D20746D707661725F382E793B0A2020726C5F31203D20646F742028786C765F544558434F4F5244342E797A772C205F576F726C6453706163654C69676874506F73302E78797A293B0A20206869676870207665633420746D707661725F393B0A2020746D707661725F39203D20756E6974795F5370656343756265305F4844523B0A20206D656469756D7020666C6F617420746D707661725F31303B0A2020746D707661725F3130203D2028312E30202D205F476C6F7373696E657373293B0A20206D656469756D702076656334206864725F31313B0A20206864725F3131203D20746D707661725F393B0A20206D656469756D70207665633420746D707661725F31323B0A2020746D707661725F31322E78797A203D20786C765F544558434F4F5244342E797A773B0A2020746D707661725F31322E77203D202828746D707661725F3130202A2028312E37202D200A2020202028302E37202A20746D707661725F3130290A20202929202A20362E30293B0A20206C6F7770207665633420746D707661725F31333B0A2020746D707661725F3133203D20696D706C5F6C6F775F74657874757265437562654C6F644558542028756E6974795F5370656343756265302C20786C765F544558434F4F5244342E797A772C20746D707661725F31322E77293B0A20206D656469756D70207665633420746D707661725F31343B0A2020746D707661725F3134203D20746D707661725F31333B0A20206D656469756D70207665633220746D707661725F31353B0A2020746D707661725F31352E78203D202828726C5F31202A20726C5F3129202A2028726C5F31202A20726C5F3129293B0A2020746D707661725F31352E79203D20746D707661725F31303B0A20206C6F7770207665633420746D707661725F31363B0A2020746D707661725F3136203D207465787475726532442028756E6974795F4E4878526F7567686E6573732C20746D707661725F3135293B0A20206D656469756D70207665633420746D707661725F31373B0A2020746D707661725F31372E77203D20312E303B0A2020746D707661725F31372E78797A203D202828280A2020202028786C765F544558434F4F5244322E78797A202A20746D707661725F37290A2020202A20746D707661725F3429202B20280A202020202828286864725F31312E78202A20280A202020202020286864725F31312E77202A2028746D707661725F31342E77202D20312E3029290A20202020202B20312E302929202A20746D707661725F31342E78797A29202A20746D707661725F37290A2020202A200A202020206D69782028746D707661725F352C20786C765F544558434F4F5244312E7777772C20786C765F544558434F4F5244352E777777290A20202929202B202828746D707661725F34202B200A202020202828746D707661725F31362E77202A2031362E3029202A20746D707661725F35290A202029202A2028746D707661725F36202A200A20202020636C616D702028646F742028786C765F544558434F4F5244352E78797A2C205F576F726C6453706163654C69676874506F73302E78797A292C20302E302C20312E30290A20202929293B0A20206D656469756D70207665633420786C61745F7661726F75747075745F31383B0A2020786C61745F7661726F75747075745F31382E78797A203D20746D707661725F31372E78797A3B0A2020786C61745F7661726F75747075745F31382E77203D20312E303B0A2020676C5F46726167446174615B305D203D20786C61745F7661726F75747075745F31383B0A7D0A0A0A23656E6469660A0000001B000000000000000100000000000000000000000000000000000000324F040C0500000000000000000000000000000000000000030000000C000000504F494E545F434F4F4B49450C000000534841444F57535F435542450C000000534841444F57535F534F46540000000000000000000000000100000000000000000000000000000000000000324F040C050000002D000000050000000100000000000000020000000400000053504F540D000000534841444F57535F4445505448000000A61700002376657273696F6E203130300A0A236966646566205645525445580A6174747269627574652076656334205F676C65735665727465783B0A6174747269627574652076656333205F676C65734E6F726D616C3B0A6174747269627574652076656334205F676C65734D756C7469546578436F6F7264303B0A6174747269627574652076656334205F676C65734D756C7469546578436F6F7264313B0A756E69666F726D2068696768702076656333205F576F726C64537061636543616D657261506F733B0A756E69666F726D2068696768702076656334205F576F726C6453706163654C69676874506F73303B0A756E69666F726D206869676870206D61743420756E6974795F4F626A656374546F576F726C643B0A756E69666F726D206869676870206D61743420756E6974795F576F726C64546F4F626A6563743B0A756E69666F726D206869676870206D61743420756E6974795F4D617472697856503B0A756E69666F726D2068696768702076656334205F4D61696E5465785F53543B0A756E69666F726D2068696768702076656334205F44657461696C416C6265646F4D61705F53543B0A756E69666F726D206D656469756D7020666C6F6174205F55565365633B0A76617279696E67206869676870207665633420786C765F544558434F4F5244303B0A76617279696E67206D656469756D70207665633320786C765F544558434F4F5244313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244323B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244325F313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244325F323B0A76617279696E67206869676870207665633320786C765F544558434F4F5244353B0A76617279696E67206869676870207665633220786C765F544558434F4F5244363B0A766F6964206D61696E2028290A7B0A20206D656469756D70207665633320746D707661725F313B0A2020746D707661725F31203D205F676C65734E6F726D616C3B0A20206869676870207665633220746D707661725F323B0A2020746D707661725F32203D205F676C65734D756C7469546578436F6F7264302E78793B0A20206869676870207665633220746D707661725F333B0A2020746D707661725F33203D205F676C65734D756C7469546578436F6F7264312E78793B0A202068696768702076656333206C696768744469725F343B0A20206D656469756D70207665633420746D707661725F353B0A20206D656469756D70207665633420746D707661725F363B0A20206D656469756D70207665633420746D707661725F373B0A20206869676870207665633220746D707661725F383B0A20206869676870207665633420746D707661725F393B0A2020746D707661725F39203D2028756E6974795F4F626A656374546F576F726C64202A205F676C6573566572746578293B0A20206869676870207665633420746D707661725F31303B0A20206869676870207665633420746D707661725F31313B0A2020746D707661725F31312E77203D20312E303B0A2020746D707661725F31312E78797A203D205F676C65735665727465782E78797A3B0A2020746D707661725F3130203D2028756E6974795F4D61747269785650202A2028756E6974795F4F626A656374546F576F726C64202A20746D707661725F313129293B0A20206869676870207665633420746578636F6F72645F31323B0A2020746578636F6F72645F31322E7879203D2028285F676C65734D756C7469546578436F6F7264302E7879202A205F4D61696E5465785F53542E787929202B205F4D61696E5465785F53542E7A77293B0A20206869676870207665633220746D707661725F31333B0A202069662028285F5556536563203D3D20302E302929207B0A20202020746D707661725F3133203D20746D707661725F323B0A20207D20656C7365207B0A20202020746D707661725F3133203D20746D707661725F333B0A20207D3B0A2020746578636F6F72645F31322E7A77203D202828746D707661725F3133202A205F44657461696C416C6265646F4D61705F53542E787929202B205F44657461696C416C6265646F4D61705F53542E7A77293B0A20206D656469756D70207665633320746D707661725F31343B0A20206869676870207665633320746D707661725F31353B0A2020746D707661725F3135203D206E6F726D616C697A652828746D707661725F392E78797A202D205F576F726C64537061636543616D657261506F7329293B0A2020746D707661725F3134203D20746D707661725F31353B0A202068696768702076656333206E6F726D5F31363B0A20206E6F726D5F3136203D20746D707661725F313B0A20206869676870206D61743320746D707661725F31373B0A2020746D707661725F31375B305D203D20756E6974795F576F726C64546F4F626A6563745B305D2E78797A3B0A2020746D707661725F31375B315D203D20756E6974795F576F726C64546F4F626A6563745B315D2E78797A3B0A2020746D707661725F31375B325D203D20756E6974795F576F726C64546F4F626A6563745B325D2E78797A3B0A20206869676870207665633320746D707661725F31383B0A2020746D707661725F3138203D206E6F726D616C697A6528286E6F726D5F3136202A20746D707661725F313729293B0A2020746D707661725F352E78797A203D207665633328302E302C20302E302C20302E30293B0A2020746D707661725F362E78797A203D207665633328302E302C20302E302C20302E30293B0A2020746D707661725F372E78797A203D20746D707661725F31383B0A20206D656469756D70207665633320746D707661725F31393B0A20206869676870207665633320746D707661725F32303B0A2020746D707661725F3230203D206E6F726D616C697A6528285F576F726C6453706163654C69676874506F73302E78797A202D2028746D707661725F392E78797A202A205F576F726C6453706163654C69676874506F73302E772929293B0A2020746D707661725F3139203D20746D707661725F32303B0A20206C696768744469725F34203D20746D707661725F31393B0A2020746D707661725F352E77203D206C696768744469725F342E783B0A2020746D707661725F362E77203D206C696768744469725F342E793B0A2020746D707661725F372E77203D206C696768744469725F342E7A3B0A2020676C5F506F736974696F6E203D20746D707661725F31303B0A2020786C765F544558434F4F524430203D20746578636F6F72645F31323B0A2020786C765F544558434F4F524431203D20746D707661725F31343B0A2020786C765F544558434F4F524432203D20746D707661725F353B0A2020786C765F544558434F4F5244325F31203D20746D707661725F363B0A2020786C765F544558434F4F5244325F32203D20746D707661725F373B0A2020786C765F544558434F4F524435203D20746D707661725F392E78797A3B0A2020786C765F544558434F4F524436203D20746D707661725F383B0A7D0A0A0A23656E6469660A23696664656620465241474D454E540A756E69666F726D206869676870206D61743420756E6974795F576F726C64546F536861646F775B345D3B0A756E69666F726D206D656469756D702076656334205F4C69676874536861646F77446174613B0A756E69666F726D206C6F77702076656334205F4C69676874436F6C6F72303B0A756E69666F726D2073616D706C6572324420756E6974795F4E4878526F7567686E6573733B0A756E69666F726D2068696768702073616D706C65723244205F536861646F774D6170546578747572653B0A756E69666F726D206D656469756D702076656334205F436F6C6F723B0A756E69666F726D2073616D706C65723244205F4D61696E5465783B0A756E69666F726D206D656469756D7020666C6F6174205F4D6574616C6C69633B0A756E69666F726D206D656469756D7020666C6F6174205F476C6F7373696E6573733B0A756E69666F726D2073616D706C65723244205F4C6967687454657874757265303B0A756E69666F726D206869676870206D61743420756E6974795F576F726C64546F4C696768743B0A756E69666F726D2073616D706C65723244205F4C696768745465787475726542303B0A76617279696E67206869676870207665633420786C765F544558434F4F5244303B0A76617279696E67206D656469756D70207665633320786C765F544558434F4F5244313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244323B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244325F313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244325F323B0A76617279696E67206869676870207665633320786C765F544558434F4F5244353B0A766F6964206D61696E2028290A7B0A20206C6F777020666C6F617420617474656E5F313B0A20206C6F777020666C6F617420736861646F775F323B0A202068696768702076656334206C69676874436F6F72645F333B0A20206C6F7770207665633420746D707661725F343B0A2020746D707661725F34203D2074657874757265324420285F4D61696E5465782C20786C765F544558434F4F5244302E7879293B0A20206D656469756D70207665633320746D707661725F353B0A2020746D707661725F35203D20285F436F6C6F722E78797A202A20746D707661725F342E78797A293B0A20206D656469756D70207665633320746D707661725F363B0A20206D656469756D70207665633320746D707661725F373B0A2020746D707661725F37203D206D697820287665633328302E323230393136332C20302E323230393136332C20302E32323039313633292C20746D707661725F352C2076656333285F4D6574616C6C696329293B0A2020746D707661725F36203D2028746D707661725F35202A2028302E37373930383337202D20285F4D6574616C6C6963202A20302E373739303833372929293B0A20206D656469756D70207665633320746D707661725F383B0A2020746D707661725F38203D206E6F726D616C697A6528786C765F544558434F4F5244325F322E78797A293B0A20206869676870207665633420746D707661725F393B0A2020746D707661725F392E77203D20312E303B0A2020746D707661725F392E78797A203D20786C765F544558434F4F5244353B0A20206C69676874436F6F72645F33203D2028756E6974795F576F726C64546F4C69676874202A20746D707661725F39293B0A20206D656469756D7020666C6F6174207265616C74696D65536861646F77417474656E756174696F6E5F31303B0A20207265616C74696D65536861646F77417474656E756174696F6E5F3130203D20312E303B0A20206869676870207665633420746D707661725F31313B0A2020746D707661725F31312E77203D20312E303B0A2020746D707661725F31312E78797A203D20786C765F544558434F4F5244353B0A20206869676870207665633420746D707661725F31323B0A2020746D707661725F3132203D2028756E6974795F576F726C64546F536861646F775B305D202A20746D707661725F3131293B0A20206C6F777020666C6F617420746D707661725F31333B0A20206869676870207665633420746D707661725F31343B0A2020746D707661725F3134203D2074657874757265324450726F6A20285F536861646F774D6170546578747572652C20746D707661725F3132293B0A20206D656469756D7020666C6F617420746D707661725F31353B0A20206966202828746D707661725F31342E78203C2028746D707661725F31322E7A202F20746D707661725F31322E77292929207B0A20202020746D707661725F3135203D205F4C69676874536861646F77446174612E783B0A20207D20656C7365207B0A20202020746D707661725F3135203D20312E303B0A20207D3B0A2020746D707661725F3133203D20746D707661725F31353B0A20207265616C74696D65536861646F77417474656E756174696F6E5F3130203D20746D707661725F31333B0A2020736861646F775F32203D207265616C74696D65536861646F77417474656E756174696F6E5F31303B0A20206C6F7770207665633420746D707661725F31363B0A20206869676870207665633220505F31373B0A2020505F3137203D2028286C69676874436F6F72645F332E7879202F206C69676874436F6F72645F332E7729202B20302E35293B0A2020746D707661725F3136203D2074657874757265324420285F4C6967687454657874757265302C20505F3137293B0A2020686967687020666C6F617420746D707661725F31383B0A2020746D707661725F3138203D20646F7420286C69676874436F6F72645F332E78797A2C206C69676874436F6F72645F332E78797A293B0A20206C6F7770207665633420746D707661725F31393B0A2020746D707661725F3139203D2074657874757265324420285F4C696768745465787475726542302C207665633228746D707661725F313829293B0A2020686967687020666C6F617420746D707661725F32303B0A2020746D707661725F3230203D202828280A20202020666C6F617428286C69676874436F6F72645F332E7A203E20302E3029290A2020202A20746D707661725F31362E7729202A20746D707661725F31392E7729202A20736861646F775F32293B0A2020617474656E5F31203D20746D707661725F32303B0A20206D656469756D70207665633320746D707661725F32313B0A2020746D707661725F32312E78203D20786C765F544558434F4F5244322E773B0A2020746D707661725F32312E79203D20786C765F544558434F4F5244325F312E773B0A2020746D707661725F32312E7A203D20786C765F544558434F4F5244325F322E773B0A20206D656469756D7020666C6F617420617474656E5F32323B0A2020617474656E5F3232203D20617474656E5F313B0A20206D656469756D70207665633320746D707661725F32333B0A2020746D707661725F3233203D205F4C69676874436F6C6F72302E78797A3B0A2020746D707661725F3233203D2028746D707661725F3233202A20617474656E5F3232293B0A20206D656469756D70207665633320766965774469725F32343B0A2020766965774469725F3234203D202D28786C765F544558434F4F524431293B0A20206D656469756D70207665633220746D707661725F32353B0A2020746D707661725F32352E78203D20646F74202828766965774469725F3234202D2028322E30202A200A2020202028646F742028746D707661725F382C20766965774469725F323429202A20746D707661725F38290A202029292C20746D707661725F3231293B0A2020746D707661725F32352E79203D2028312E30202D20636C616D702028646F742028746D707661725F382C20766965774469725F3234292C20302E302C20312E3029293B0A20206D656469756D70207665633220746D707661725F32363B0A2020746D707661725F32362E78203D202828746D707661725F3235202A20746D707661725F323529202A2028746D707661725F3235202A20746D707661725F323529292E783B0A2020746D707661725F32362E79203D2028312E30202D205F476C6F7373696E657373293B0A20206C6F7770207665633420746D707661725F32373B0A2020746D707661725F3237203D207465787475726532442028756E6974795F4E4878526F7567686E6573732C20746D707661725F3236293B0A20206D656469756D70207665633420746D707661725F32383B0A2020746D707661725F32382E77203D20312E303B0A2020746D707661725F32382E78797A203D202828746D707661725F36202B20280A2020202028746D707661725F32372E77202A2031362E30290A2020202A20746D707661725F372929202A2028746D707661725F3233202A20636C616D7020280A20202020646F742028746D707661725F382C20746D707661725F3231290A20202C20302E302C20312E302929293B0A20206D656469756D70207665633420786C61745F7661726F75747075745F32393B0A2020786C61745F7661726F75747075745F32392E78797A203D20746D707661725F32382E78797A3B0A2020786C61745F7661726F75747075745F32392E77203D20312E303B0A2020676C5F46726167446174615B305D203D20786C61745F7661726F75747075745F32393B0A7D0A0A0A23656E6469660A00001B000000000000000100000000000000000000000000000000000000324F040C050000001A000000020000000000000000000000010000000C000000554E4954595F4844525F4F4E8F1300002376657273696F6E203130300A0A236966646566205645525445580A6174747269627574652076656334205F676C65735665727465783B0A6174747269627574652076656333205F676C65734E6F726D616C3B0A6174747269627574652076656334205F676C65734D756C7469546578436F6F7264303B0A6174747269627574652076656334205F676C65734D756C7469546578436F6F7264313B0A756E69666F726D2068696768702076656333205F576F726C64537061636543616D657261506F733B0A756E69666F726D206D656469756D70207665633420756E6974795F534842723B0A756E69666F726D206D656469756D70207665633420756E6974795F534842673B0A756E69666F726D206D656469756D70207665633420756E6974795F534842623B0A756E69666F726D206D656469756D70207665633420756E6974795F5348433B0A756E69666F726D206869676870206D61743420756E6974795F4F626A656374546F576F726C643B0A756E69666F726D206869676870206D61743420756E6974795F576F726C64546F4F626A6563743B0A756E69666F726D206869676870206D61743420756E6974795F4D617472697856503B0A756E69666F726D2068696768702076656334205F4D61696E5465785F53543B0A756E69666F726D2068696768702076656334205F44657461696C416C6265646F4D61705F53543B0A756E69666F726D206D656469756D7020666C6F6174205F55565365633B0A76617279696E67206869676870207665633420786C765F544558434F4F5244303B0A76617279696E67206D656469756D70207665633320786C765F544558434F4F5244313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244323B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244325F313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244325F323B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244353B0A76617279696E67206869676870207665633320786C765F544558434F4F5244363B0A766F6964206D61696E2028290A7B0A20206D656469756D70207665633320746D707661725F313B0A2020746D707661725F31203D205F676C65734E6F726D616C3B0A20206869676870207665633220746D707661725F323B0A2020746D707661725F32203D205F676C65734D756C7469546578436F6F7264302E78793B0A20206869676870207665633220746D707661725F333B0A2020746D707661725F33203D205F676C65734D756C7469546578436F6F7264312E78793B0A20206D656469756D70207665633420746D707661725F343B0A20206D656469756D70207665633420746D707661725F353B0A20206D656469756D70207665633420746D707661725F363B0A20206D656469756D70207665633420746D707661725F373B0A20206869676870207665633320746D707661725F383B0A20206869676870207665633420746D707661725F393B0A2020746D707661725F39203D2028756E6974795F4F626A656374546F576F726C64202A205F676C6573566572746578293B0A2020746D707661725F38203D20746D707661725F392E78797A3B0A20206869676870207665633420746D707661725F31303B0A20206869676870207665633420746D707661725F31313B0A2020746D707661725F31312E77203D20312E303B0A2020746D707661725F31312E78797A203D205F676C65735665727465782E78797A3B0A2020746D707661725F3130203D2028756E6974795F4D61747269785650202A2028756E6974795F4F626A656374546F576F726C64202A20746D707661725F313129293B0A20206869676870207665633420746578636F6F72645F31323B0A2020746578636F6F72645F31322E7879203D2028285F676C65734D756C7469546578436F6F7264302E7879202A205F4D61696E5465785F53542E787929202B205F4D61696E5465785F53542E7A77293B0A20206869676870207665633220746D707661725F31333B0A202069662028285F5556536563203D3D20302E302929207B0A20202020746D707661725F3133203D20746D707661725F323B0A20207D20656C7365207B0A20202020746D707661725F3133203D20746D707661725F333B0A20207D3B0A2020746578636F6F72645F31322E7A77203D202828746D707661725F3133202A205F44657461696C416C6265646F4D61705F53542E787929202B205F44657461696C416C6265646F4D61705F53542E7A77293B0A20206D656469756D70207665633320746D707661725F31343B0A202068696768702076656333206E5F31353B0A20206E5F3135203D2028746D707661725F392E78797A202D205F576F726C64537061636543616D657261506F73293B0A2020746D707661725F3134203D206E5F31353B0A202068696768702076656333206E6F726D5F31363B0A20206E6F726D5F3136203D20746D707661725F313B0A20206869676870206D61743320746D707661725F31373B0A2020746D707661725F31375B305D203D20756E6974795F576F726C64546F4F626A6563745B305D2E78797A3B0A2020746D707661725F31375B315D203D20756E6974795F576F726C64546F4F626A6563745B315D2E78797A3B0A2020746D707661725F31375B325D203D20756E6974795F576F726C64546F4F626A6563745B325D2E78797A3B0A20206869676870207665633320746D707661725F31383B0A2020746D707661725F3138203D206E6F726D616C697A6528286E6F726D5F3136202A20746D707661725F313729293B0A2020746D707661725F342E78797A203D207665633328302E302C20302E302C20302E30293B0A2020746D707661725F352E78797A203D207665633328302E302C20302E302C20302E30293B0A2020746D707661725F362E78797A203D20746D707661725F31383B0A2020746D707661725F372E77203D20302E303B0A20206D656469756D702076656333206E6F726D616C5F31393B0A20206E6F726D616C5F3139203D20746D707661725F31383B0A20206D656469756D7020766563332078315F32303B0A20206D656469756D70207665633420746D707661725F32313B0A2020746D707661725F3231203D20286E6F726D616C5F31392E78797A7A202A206E6F726D616C5F31392E797A7A78293B0A202078315F32302E78203D20646F742028756E6974795F534842722C20746D707661725F3231293B0A202078315F32302E79203D20646F742028756E6974795F534842672C20746D707661725F3231293B0A202078315F32302E7A203D20646F742028756E6974795F534842622C20746D707661725F3231293B0A2020746D707661725F372E78797A203D202878315F3230202B2028756E6974795F5348432E78797A202A20280A20202020286E6F726D616C5F31392E78202A206E6F726D616C5F31392E78290A2020202D200A20202020286E6F726D616C5F31392E79202A206E6F726D616C5F31392E79290A20202929293B0A2020676C5F506F736974696F6E203D20746D707661725F31303B0A2020786C765F544558434F4F524430203D20746578636F6F72645F31323B0A2020786C765F544558434F4F524431203D20746D707661725F31343B0A2020786C765F544558434F4F524432203D20746D707661725F343B0A2020786C765F544558434F4F5244325F31203D20746D707661725F353B0A2020786C765F544558434F4F5244325F32203D20746D707661725F363B0A2020786C765F544558434F4F524435203D20746D707661725F373B0A2020786C765F544558434F4F524436203D20746D707661725F383B0A7D0A0A0A23656E6469660A23696664656620465241474D454E540A23657874656E73696F6E20474C5F4558545F7368616465725F746578747572655F6C6F64203A20656E61626C650A23657874656E73696F6E20474C5F4558545F647261775F62756666657273203A20656E61626C650A756E69666F726D206D656469756D70207665633420756E6974795F534841723B0A756E69666F726D206D656469756D70207665633420756E6974795F534841673B0A756E69666F726D206D656469756D70207665633420756E6974795F534841623B0A756E69666F726D206D656469756D702076656334205F436F6C6F723B0A756E69666F726D2073616D706C65723244205F4D61696E5465783B0A756E69666F726D206D656469756D7020666C6F6174205F4D6574616C6C69633B0A756E69666F726D206D656469756D7020666C6F6174205F476C6F7373696E6573733B0A756E69666F726D2073616D706C65723244205F4F63636C7573696F6E4D61703B0A756E69666F726D206D656469756D7020666C6F6174205F4F63636C7573696F6E537472656E6774683B0A76617279696E67206869676870207665633420786C765F544558434F4F5244303B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244325F323B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244353B0A766F6964206D61696E2028290A7B0A20206C6F7770207665633420746D707661725F313B0A2020746D707661725F31203D2074657874757265324420285F4D61696E5465782C20786C765F544558434F4F5244302E7879293B0A20206D656469756D702076656332206D675F323B0A20206D675F322E78203D205F4D6574616C6C69633B0A20206D675F322E79203D205F476C6F7373696E6573733B0A20206D656469756D70207665633320746D707661725F333B0A2020746D707661725F33203D20285F436F6C6F722E78797A202A20746D707661725F312E78797A293B0A20206D656469756D70207665633320746D707661725F343B0A2020746D707661725F34203D2028746D707661725F33202A2028302E37373930383337202D20285F4D6574616C6C6963202A20302E373739303833372929293B0A20206D656469756D70207665633320746D707661725F353B0A2020746D707661725F35203D206E6F726D616C697A6528786C765F544558434F4F5244325F322E78797A293B0A20206D656469756D7020666C6F6174206F63635F363B0A20206C6F777020666C6F617420746D707661725F373B0A2020746D707661725F37203D2074657874757265324420285F4F63636C7573696F6E4D61702C20786C765F544558434F4F5244302E7879292E793B0A20206F63635F36203D20746D707661725F373B0A20206D656469756D7020666C6F617420746D707661725F383B0A2020746D707661725F38203D202828312E30202D205F4F63636C7573696F6E537472656E67746829202B20286F63635F36202A205F4F63636C7573696F6E537472656E67746829293B0A20206D656469756D70207665633320746D707661725F393B0A20206D656469756D70207665633420746D707661725F31303B0A2020746D707661725F31302E77203D20312E303B0A2020746D707661725F31302E78797A203D20746D707661725F353B0A20206D656469756D70207665633320785F31313B0A2020785F31312E78203D20646F742028756E6974795F534841722C20746D707661725F3130293B0A2020785F31312E79203D20646F742028756E6974795F534841672C20746D707661725F3130293B0A2020785F31312E7A203D20646F742028756E6974795F534841622C20746D707661725F3130293B0A2020746D707661725F39203D206D617820282828312E303535202A200A20202020706F7720286D617820287665633328302E302C20302E302C20302E30292C2028786C765F544558434F4F5244352E78797A202B20785F313129292C207665633328302E343136363636372C20302E343136363636372C20302E3431363636363729290A202029202D20302E303535292C207665633328302E302C20302E302C20302E3029293B0A2020746D707661725F39203D2028746D707661725F39202A20746D707661725F38293B0A20206D656469756D70207665633420746D707661725F31323B0A2020746D707661725F31322E77203D20312E303B0A2020746D707661725F31322E78797A203D2028746D707661725F39202A20746D707661725F34293B0A20206D656469756D70207665633420746D707661725F31333B0A2020746D707661725F31332E78797A203D20746D707661725F343B0A2020746D707661725F31332E77203D20746D707661725F383B0A20206D656469756D70207665633420746D707661725F31343B0A2020746D707661725F31342E78797A203D206D697820287665633328302E323230393136332C20302E323230393136332C20302E32323039313633292C20746D707661725F332C2076656333285F4D6574616C6C696329293B0A2020746D707661725F31342E77203D206D675F322E793B0A20206D656469756D70207665633420746D707661725F31353B0A2020746D707661725F31352E77203D20312E303B0A2020746D707661725F31352E78797A203D202828746D707661725F35202A20302E3529202B20302E35293B0A20206D656469756D70207665633420746D707661725F31363B0A2020746D707661725F31362E77203D20312E303B0A2020746D707661725F31362E78797A203D20746D707661725F31322E78797A3B0A2020676C5F46726167446174615B305D203D20746D707661725F31333B0A2020676C5F46726167446174615B315D203D20746D707661725F31343B0A2020676C5F46726167446174615B325D203D20746D707661725F31353B0A2020676C5F46726167446174615B335D203D20746D707661725F31363B0A7D0A0A0A23656E6469660A001B000000000000000100000000000000000000000000000000000000324F040C050000001D000000040000000000000000000000010000000400000053504F542E1200002376657273696F6E203130300A0A236966646566205645525445580A6174747269627574652076656334205F676C65735665727465783B0A6174747269627574652076656333205F676C65734E6F726D616C3B0A6174747269627574652076656334205F676C65734D756C7469546578436F6F7264303B0A6174747269627574652076656334205F676C65734D756C7469546578436F6F7264313B0A756E69666F726D2068696768702076656333205F576F726C64537061636543616D657261506F733B0A756E69666F726D2068696768702076656334205F576F726C6453706163654C69676874506F73303B0A756E69666F726D206869676870206D61743420756E6974795F4F626A656374546F576F726C643B0A756E69666F726D206869676870206D61743420756E6974795F576F726C64546F4F626A6563743B0A756E69666F726D206869676870206D61743420756E6974795F4D617472697856503B0A756E69666F726D2068696768702076656334205F4D61696E5465785F53543B0A756E69666F726D2068696768702076656334205F44657461696C416C6265646F4D61705F53543B0A756E69666F726D206D656469756D7020666C6F6174205F55565365633B0A76617279696E67206869676870207665633420786C765F544558434F4F5244303B0A76617279696E67206869676870207665633320786C765F544558434F4F5244313B0A76617279696E67206869676870207665633220786C765F544558434F4F5244323B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244333B0A76617279696E67206D656469756D70207665633320786C765F544558434F4F5244343B0A76617279696E67206D656469756D70207665633320786C765F544558434F4F5244353B0A766F6964206D61696E2028290A7B0A20206D656469756D70207665633320746D707661725F313B0A2020746D707661725F31203D205F676C65734E6F726D616C3B0A20206869676870207665633220746D707661725F323B0A2020746D707661725F32203D205F676C65734D756C7469546578436F6F7264302E78793B0A20206869676870207665633220746D707661725F333B0A2020746D707661725F33203D205F676C65734D756C7469546578436F6F7264312E78793B0A20206D656469756D702076656333206E6F726D616C576F726C645F343B0A20206D656469756D702076656333206579655665635F353B0A20206D656469756D702076656333206C696768744469725F363B0A20206869676870207665633220746D707661725F373B0A20206D656469756D70207665633420746D707661725F383B0A20206869676870207665633420746D707661725F393B0A2020746D707661725F39203D2028756E6974795F4F626A656374546F576F726C64202A205F676C6573566572746578293B0A20206869676870207665633420746D707661725F31303B0A20206869676870207665633420746D707661725F31313B0A2020746D707661725F31312E77203D20312E303B0A2020746D707661725F31312E78797A203D205F676C65735665727465782E78797A3B0A2020746D707661725F3130203D2028756E6974795F4D61747269785650202A2028756E6974795F4F626A656374546F576F726C64202A20746D707661725F313129293B0A20206869676870207665633420746578636F6F72645F31323B0A2020746578636F6F72645F31322E7879203D2028285F676C65734D756C7469546578436F6F7264302E7879202A205F4D61696E5465785F53542E787929202B205F4D61696E5465785F53542E7A77293B0A20206869676870207665633220746D707661725F31333B0A202069662028285F5556536563203D3D20302E302929207B0A20202020746D707661725F3133203D20746D707661725F323B0A20207D20656C7365207B0A20202020746D707661725F3133203D20746D707661725F333B0A20207D3B0A2020746578636F6F72645F31322E7A77203D202828746D707661725F3133202A205F44657461696C416C6265646F4D61705F53542E787929202B205F44657461696C416C6265646F4D61705F53542E7A77293B0A20206869676870207665633320746D707661725F31343B0A2020746D707661725F3134203D20285F576F726C6453706163654C69676874506F73302E78797A202D2028746D707661725F392E78797A202A205F576F726C6453706163654C69676874506F73302E7729293B0A20206C696768744469725F36203D20746D707661725F31343B0A20206D656469756D70207665633320746D707661725F31353B0A202068696768702076656333206E5F31363B0A20206E5F3136203D206C696768744469725F363B0A20206869676870207665633320746D707661725F31373B0A2020746D707661725F3137203D206E6F726D616C697A65286E5F3136293B0A2020746D707661725F3135203D20746D707661725F31373B0A20206C696768744469725F36203D20746D707661725F31353B0A20206869676870207665633320746D707661725F31383B0A2020746D707661725F3138203D206E6F726D616C697A652828746D707661725F392E78797A202D205F576F726C64537061636543616D657261506F7329293B0A20206579655665635F35203D20746D707661725F31383B0A202068696768702076656333206E6F726D5F31393B0A20206E6F726D5F3139203D20746D707661725F313B0A20206869676870206D61743320746D707661725F32303B0A2020746D707661725F32305B305D203D20756E6974795F576F726C64546F4F626A6563745B305D2E78797A3B0A2020746D707661725F32305B315D203D20756E6974795F576F726C64546F4F626A6563745B315D2E78797A3B0A2020746D707661725F32305B325D203D20756E6974795F576F726C64546F4F626A6563745B325D2E78797A3B0A20206869676870207665633320746D707661725F32313B0A2020746D707661725F3231203D206E6F726D616C697A6528286E6F726D5F3139202A20746D707661725F323029293B0A20206E6F726D616C576F726C645F34203D20746D707661725F32313B0A2020746D707661725F382E797A77203D20286579655665635F35202D2028322E30202A20280A20202020646F7420286E6F726D616C576F726C645F342C206579655665635F35290A2020202A206E6F726D616C576F726C645F342929293B0A2020676C5F506F736974696F6E203D20746D707661725F31303B0A2020786C765F544558434F4F524430203D20746578636F6F72645F31323B0A2020786C765F544558434F4F524431203D20746D707661725F392E78797A3B0A2020786C765F544558434F4F524432203D20746D707661725F373B0A2020786C765F544558434F4F524433203D20746D707661725F383B0A2020786C765F544558434F4F524434203D20746D707661725F31353B0A2020786C765F544558434F4F524435203D206E6F726D616C576F726C645F343B0A7D0A0A0A23656E6469660A23696664656620465241474D454E540A756E69666F726D206C6F77702076656334205F4C69676874436F6C6F72303B0A756E69666F726D2073616D706C6572324420756E6974795F4E4878526F7567686E6573733B0A756E69666F726D206D656469756D702076656334205F436F6C6F723B0A756E69666F726D2073616D706C65723244205F4D61696E5465783B0A756E69666F726D206D656469756D7020666C6F6174205F4D6574616C6C69633B0A756E69666F726D206D656469756D7020666C6F6174205F476C6F7373696E6573733B0A756E69666F726D2073616D706C65723244205F4C6967687454657874757265303B0A756E69666F726D206869676870206D61743420756E6974795F576F726C64546F4C696768743B0A756E69666F726D2073616D706C65723244205F4C696768745465787475726542303B0A76617279696E67206869676870207665633420786C765F544558434F4F5244303B0A76617279696E67206869676870207665633320786C765F544558434F4F5244313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244333B0A76617279696E67206D656469756D70207665633320786C765F544558434F4F5244343B0A76617279696E67206D656469756D70207665633320786C765F544558434F4F5244353B0A766F6964206D61696E2028290A7B0A20206C6F777020666C6F617420617474656E5F313B0A202068696768702076656334206C69676874436F6F72645F323B0A20206D656469756D70207665633320635F333B0A20206C6F7770207665633420746D707661725F343B0A2020746D707661725F34203D2074657874757265324420285F4D61696E5465782C20786C765F544558434F4F5244302E7879293B0A20206D656469756D70207665633320746D707661725F353B0A2020746D707661725F35203D20285F436F6C6F722E78797A202A20746D707661725F342E78797A293B0A20206D656469756D7020666C6F617420746D707661725F363B0A2020746D707661725F36203D20646F742028786C765F544558434F4F5244332E797A772C20786C765F544558434F4F524434293B0A20206D656469756D70207665633220746D707661725F373B0A2020746D707661725F372E78203D202828746D707661725F36202A20746D707661725F3629202A2028746D707661725F36202A20746D707661725F3629293B0A2020746D707661725F372E79203D2028312E30202D205F476C6F7373696E657373293B0A20206C6F7770207665633420746D707661725F383B0A2020746D707661725F38203D207465787475726532442028756E6974795F4E4878526F7567686E6573732C20746D707661725F37293B0A2020635F33203D20282828746D707661725F35202A200A2020202028302E37373930383337202D20285F4D6574616C6C6963202A20302E3737393038333729290A202029202B20280A2020202028746D707661725F382E77202A2031362E30290A2020202A200A202020206D697820287665633328302E323230393136332C20302E323230393136332C20302E32323039313633292C20746D707661725F352C2076656333285F4D6574616C6C696329290A20202929202A205F4C69676874436F6C6F72302E78797A293B0A20206869676870207665633420746D707661725F393B0A2020746D707661725F392E77203D20312E303B0A2020746D707661725F392E78797A203D20786C765F544558434F4F5244313B0A20206C69676874436F6F72645F32203D2028756E6974795F576F726C64546F4C69676874202A20746D707661725F39293B0A20206C6F7770207665633420746D707661725F31303B0A20206869676870207665633220505F31313B0A2020505F3131203D2028286C69676874436F6F72645F322E7879202F206C69676874436F6F72645F322E7729202B20302E35293B0A2020746D707661725F3130203D2074657874757265324420285F4C6967687454657874757265302C20505F3131293B0A2020686967687020666C6F617420746D707661725F31323B0A2020746D707661725F3132203D20646F7420286C69676874436F6F72645F322E78797A2C206C69676874436F6F72645F322E78797A293B0A20206C6F7770207665633420746D707661725F31333B0A2020746D707661725F3133203D2074657874757265324420285F4C696768745465787475726542302C207665633228746D707661725F313229293B0A2020686967687020666C6F617420746D707661725F31343B0A2020746D707661725F3134203D202828666C6F6174280A20202020286C69676874436F6F72645F322E7A203E20302E30290A202029202A20746D707661725F31302E7729202A20746D707661725F31332E77293B0A2020617474656E5F31203D20746D707661725F31343B0A2020635F33203D2028635F33202A2028617474656E5F31202A20636C616D7020280A20202020646F742028786C765F544558434F4F5244352C20786C765F544558434F4F524434290A20202C20302E302C20312E302929293B0A20206D656469756D70207665633420746D707661725F31353B0A2020746D707661725F31352E77203D20312E303B0A2020746D707661725F31352E78797A203D20635F333B0A20206D656469756D70207665633420786C61745F7661726F75747075745F31363B0A2020786C61745F7661726F75747075745F31362E78797A203D20746D707661725F31352E78797A3B0A2020786C61745F7661726F75747075745F31362E77203D20312E303B0A2020676C5F46726167446174615B305D203D20786C61745F7661726F75747075745F31363B0A7D0A0A0A23656E6469660A00001B000000000000000100000000000000000000000000000000000000324F040C0500000026000000040000000000000000000000010000000400000053504F54491400002376657273696F6E203130300A0A236966646566205645525445580A6174747269627574652076656334205F676C65735665727465783B0A6174747269627574652076656333205F676C65734E6F726D616C3B0A6174747269627574652076656334205F676C65734D756C7469546578436F6F7264303B0A6174747269627574652076656334205F676C65734D756C7469546578436F6F7264313B0A756E69666F726D2068696768702076656333205F576F726C64537061636543616D657261506F733B0A756E69666F726D2068696768702076656334205F576F726C6453706163654C69676874506F73303B0A756E69666F726D206869676870206D61743420756E6974795F4F626A656374546F576F726C643B0A756E69666F726D206869676870206D61743420756E6974795F576F726C64546F4F626A6563743B0A756E69666F726D206869676870206D61743420756E6974795F4D617472697856503B0A756E69666F726D2068696768702076656334205F4D61696E5465785F53543B0A756E69666F726D2068696768702076656334205F44657461696C416C6265646F4D61705F53543B0A756E69666F726D206D656469756D7020666C6F6174205F55565365633B0A76617279696E67206869676870207665633420786C765F544558434F4F5244303B0A76617279696E67206D656469756D70207665633320786C765F544558434F4F5244313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244323B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244325F313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244325F323B0A76617279696E67206869676870207665633320786C765F544558434F4F5244353B0A76617279696E67206869676870207665633220786C765F544558434F4F5244363B0A766F6964206D61696E2028290A7B0A20206D656469756D70207665633320746D707661725F313B0A2020746D707661725F31203D205F676C65734E6F726D616C3B0A20206869676870207665633220746D707661725F323B0A2020746D707661725F32203D205F676C65734D756C7469546578436F6F7264302E78793B0A20206869676870207665633220746D707661725F333B0A2020746D707661725F33203D205F676C65734D756C7469546578436F6F7264312E78793B0A202068696768702076656333206C696768744469725F343B0A20206D656469756D70207665633420746D707661725F353B0A20206D656469756D70207665633420746D707661725F363B0A20206D656469756D70207665633420746D707661725F373B0A20206869676870207665633220746D707661725F383B0A20206869676870207665633420746D707661725F393B0A2020746D707661725F39203D2028756E6974795F4F626A656374546F576F726C64202A205F676C6573566572746578293B0A20206869676870207665633420746D707661725F31303B0A20206869676870207665633420746D707661725F31313B0A2020746D707661725F31312E77203D20312E303B0A2020746D707661725F31312E78797A203D205F676C65735665727465782E78797A3B0A2020746D707661725F3130203D2028756E6974795F4D61747269785650202A2028756E6974795F4F626A656374546F576F726C64202A20746D707661725F313129293B0A20206869676870207665633420746578636F6F72645F31323B0A2020746578636F6F72645F31322E7879203D2028285F676C65734D756C7469546578436F6F7264302E7879202A205F4D61696E5465785F53542E787929202B205F4D61696E5465785F53542E7A77293B0A20206869676870207665633220746D707661725F31333B0A202069662028285F5556536563203D3D20302E302929207B0A20202020746D707661725F3133203D20746D707661725F323B0A20207D20656C7365207B0A20202020746D707661725F3133203D20746D707661725F333B0A20207D3B0A2020746578636F6F72645F31322E7A77203D202828746D707661725F3133202A205F44657461696C416C6265646F4D61705F53542E787929202B205F44657461696C416C6265646F4D61705F53542E7A77293B0A20206D656469756D70207665633320746D707661725F31343B0A20206869676870207665633320746D707661725F31353B0A2020746D707661725F3135203D206E6F726D616C697A652828746D707661725F392E78797A202D205F576F726C64537061636543616D657261506F7329293B0A2020746D707661725F3134203D20746D707661725F31353B0A202068696768702076656333206E6F726D5F31363B0A20206E6F726D5F3136203D20746D707661725F313B0A20206869676870206D61743320746D707661725F31373B0A2020746D707661725F31375B305D203D20756E6974795F576F726C64546F4F626A6563745B305D2E78797A3B0A2020746D707661725F31375B315D203D20756E6974795F576F726C64546F4F626A6563745B315D2E78797A3B0A2020746D707661725F31375B325D203D20756E6974795F576F726C64546F4F626A6563745B325D2E78797A3B0A20206869676870207665633320746D707661725F31383B0A2020746D707661725F3138203D206E6F726D616C697A6528286E6F726D5F3136202A20746D707661725F313729293B0A2020746D707661725F352E78797A203D207665633328302E302C20302E302C20302E30293B0A2020746D707661725F362E78797A203D207665633328302E302C20302E302C20302E30293B0A2020746D707661725F372E78797A203D20746D707661725F31383B0A20206D656469756D70207665633320746D707661725F31393B0A20206869676870207665633320746D707661725F32303B0A2020746D707661725F3230203D206E6F726D616C697A6528285F576F726C6453706163654C69676874506F73302E78797A202D2028746D707661725F392E78797A202A205F576F726C6453706163654C69676874506F73302E772929293B0A2020746D707661725F3139203D20746D707661725F32303B0A20206C696768744469725F34203D20746D707661725F31393B0A2020746D707661725F352E77203D206C696768744469725F342E783B0A2020746D707661725F362E77203D206C696768744469725F342E793B0A2020746D707661725F372E77203D206C696768744469725F342E7A3B0A2020676C5F506F736974696F6E203D20746D707661725F31303B0A2020786C765F544558434F4F524430203D20746578636F6F72645F31323B0A2020786C765F544558434F4F524431203D20746D707661725F31343B0A2020786C765F544558434F4F524432203D20746D707661725F353B0A2020786C765F544558434F4F5244325F31203D20746D707661725F363B0A2020786C765F544558434F4F5244325F32203D20746D707661725F373B0A2020786C765F544558434F4F524435203D20746D707661725F392E78797A3B0A2020786C765F544558434F4F524436203D20746D707661725F383B0A7D0A0A0A23656E6469660A23696664656620465241474D454E540A756E69666F726D206C6F77702076656334205F4C69676874436F6C6F72303B0A756E69666F726D2073616D706C6572324420756E6974795F4E4878526F7567686E6573733B0A756E69666F726D206D656469756D702076656334205F436F6C6F723B0A756E69666F726D2073616D706C65723244205F4D61696E5465783B0A756E69666F726D206D656469756D7020666C6F6174205F4D6574616C6C69633B0A756E69666F726D206D656469756D7020666C6F6174205F476C6F7373696E6573733B0A756E69666F726D2073616D706C65723244205F4C6967687454657874757265303B0A756E69666F726D206869676870206D61743420756E6974795F576F726C64546F4C696768743B0A756E69666F726D2073616D706C65723244205F4C696768745465787475726542303B0A76617279696E67206869676870207665633420786C765F544558434F4F5244303B0A76617279696E67206D656469756D70207665633320786C765F544558434F4F5244313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244323B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244325F313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244325F323B0A76617279696E67206869676870207665633320786C765F544558434F4F5244353B0A766F6964206D61696E2028290A7B0A20206C6F777020666C6F617420617474656E5F313B0A202068696768702076656334206C69676874436F6F72645F323B0A20206C6F7770207665633420746D707661725F333B0A2020746D707661725F33203D2074657874757265324420285F4D61696E5465782C20786C765F544558434F4F5244302E7879293B0A20206D656469756D70207665633320746D707661725F343B0A2020746D707661725F34203D20285F436F6C6F722E78797A202A20746D707661725F332E78797A293B0A20206D656469756D70207665633320746D707661725F353B0A2020746D707661725F35203D206E6F726D616C697A6528786C765F544558434F4F5244325F322E78797A293B0A20206869676870207665633420746D707661725F363B0A2020746D707661725F362E77203D20312E303B0A2020746D707661725F362E78797A203D20786C765F544558434F4F5244353B0A20206C69676874436F6F72645F32203D2028756E6974795F576F726C64546F4C69676874202A20746D707661725F36293B0A20206C6F7770207665633420746D707661725F373B0A20206869676870207665633220505F383B0A2020505F38203D2028286C69676874436F6F72645F322E7879202F206C69676874436F6F72645F322E7729202B20302E35293B0A2020746D707661725F37203D2074657874757265324420285F4C6967687454657874757265302C20505F38293B0A2020686967687020666C6F617420746D707661725F393B0A2020746D707661725F39203D20646F7420286C69676874436F6F72645F322E78797A2C206C69676874436F6F72645F322E78797A293B0A20206C6F7770207665633420746D707661725F31303B0A2020746D707661725F3130203D2074657874757265324420285F4C696768745465787475726542302C207665633228746D707661725F3929293B0A2020686967687020666C6F617420746D707661725F31313B0A2020746D707661725F3131203D202828666C6F6174280A20202020286C69676874436F6F72645F322E7A203E20302E30290A202029202A20746D707661725F372E7729202A20746D707661725F31302E77293B0A2020617474656E5F31203D20746D707661725F31313B0A20206D656469756D70207665633320746D707661725F31323B0A2020746D707661725F31322E78203D20786C765F544558434F4F5244322E773B0A2020746D707661725F31322E79203D20786C765F544558434F4F5244325F312E773B0A2020746D707661725F31322E7A203D20786C765F544558434F4F5244325F322E773B0A20206D656469756D7020666C6F617420617474656E5F31333B0A2020617474656E5F3133203D20617474656E5F313B0A20206D656469756D70207665633320746D707661725F31343B0A2020746D707661725F3134203D205F4C69676874436F6C6F72302E78797A3B0A2020746D707661725F3134203D2028746D707661725F3134202A20617474656E5F3133293B0A20206D656469756D70207665633320766965774469725F31353B0A2020766965774469725F3135203D202D28786C765F544558434F4F524431293B0A20206D656469756D70207665633220746D707661725F31363B0A2020746D707661725F31362E78203D20646F74202828766965774469725F3135202D2028322E30202A200A2020202028646F742028746D707661725F352C20766965774469725F313529202A20746D707661725F35290A202029292C20746D707661725F3132293B0A2020746D707661725F31362E79203D2028312E30202D20636C616D702028646F742028746D707661725F352C20766965774469725F3135292C20302E302C20312E3029293B0A20206D656469756D70207665633220746D707661725F31373B0A2020746D707661725F31372E78203D202828746D707661725F3136202A20746D707661725F313629202A2028746D707661725F3136202A20746D707661725F313629292E783B0A2020746D707661725F31372E79203D2028312E30202D205F476C6F7373696E657373293B0A20206C6F7770207665633420746D707661725F31383B0A2020746D707661725F3138203D207465787475726532442028756E6974795F4E4878526F7567686E6573732C20746D707661725F3137293B0A20206D656469756D70207665633420746D707661725F31393B0A2020746D707661725F31392E77203D20312E303B0A2020746D707661725F31392E78797A203D20282828746D707661725F34202A200A2020202028302E37373930383337202D20285F4D6574616C6C6963202A20302E3737393038333729290A202029202B20280A2020202028746D707661725F31382E77202A2031362E30290A2020202A200A202020206D697820287665633328302E323230393136332C20302E323230393136332C20302E32323039313633292C20746D707661725F342C2076656333285F4D6574616C6C696329290A20202929202A2028746D707661725F3134202A20636C616D7020280A20202020646F742028746D707661725F352C20746D707661725F3132290A20202C20302E302C20312E302929293B0A20206D656469756D70207665633420786C61745F7661726F75747075745F32303B0A2020786C61745F7661726F75747075745F32302E78797A203D20746D707661725F31392E78797A3B0A2020786C61745F7661726F75747075745F32302E77203D20312E303B0A2020676C5F46726167446174615B305D203D20786C61745F7661726F75747075745F32303B0A7D0A0A0A23656E6469660A0000001B000000000000000100000000000000000000000000000000000000324F040C05000000200000000400000001000000000000000200000005000000504F494E540000000C000000534841444F57535F43554245601300002376657273696F6E203130300A0A236966646566205645525445580A6174747269627574652076656334205F676C65735665727465783B0A6174747269627574652076656333205F676C65734E6F726D616C3B0A6174747269627574652076656334205F676C65734D756C7469546578436F6F7264303B0A6174747269627574652076656334205F676C65734D756C7469546578436F6F7264313B0A756E69666F726D2068696768702076656333205F576F726C64537061636543616D657261506F733B0A756E69666F726D2068696768702076656334205F576F726C6453706163654C69676874506F73303B0A756E69666F726D206869676870206D61743420756E6974795F4F626A656374546F576F726C643B0A756E69666F726D206869676870206D61743420756E6974795F576F726C64546F4F626A6563743B0A756E69666F726D206869676870206D61743420756E6974795F4D617472697856503B0A756E69666F726D2068696768702076656334205F4D61696E5465785F53543B0A756E69666F726D2068696768702076656334205F44657461696C416C6265646F4D61705F53543B0A756E69666F726D206D656469756D7020666C6F6174205F55565365633B0A76617279696E67206869676870207665633420786C765F544558434F4F5244303B0A76617279696E67206869676870207665633320786C765F544558434F4F5244313B0A76617279696E67206869676870207665633220786C765F544558434F4F5244323B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244333B0A76617279696E67206D656469756D70207665633320786C765F544558434F4F5244343B0A76617279696E67206D656469756D70207665633320786C765F544558434F4F5244353B0A766F6964206D61696E2028290A7B0A20206D656469756D70207665633320746D707661725F313B0A2020746D707661725F31203D205F676C65734E6F726D616C3B0A20206869676870207665633220746D707661725F323B0A2020746D707661725F32203D205F676C65734D756C7469546578436F6F7264302E78793B0A20206869676870207665633220746D707661725F333B0A2020746D707661725F33203D205F676C65734D756C7469546578436F6F7264312E78793B0A20206D656469756D702076656333206E6F726D616C576F726C645F343B0A20206D656469756D702076656333206579655665635F353B0A20206D656469756D702076656333206C696768744469725F363B0A20206869676870207665633220746D707661725F373B0A20206D656469756D70207665633420746D707661725F383B0A20206869676870207665633420746D707661725F393B0A2020746D707661725F39203D2028756E6974795F4F626A656374546F576F726C64202A205F676C6573566572746578293B0A20206869676870207665633420746D707661725F31303B0A20206869676870207665633420746D707661725F31313B0A2020746D707661725F31312E77203D20312E303B0A2020746D707661725F31312E78797A203D205F676C65735665727465782E78797A3B0A2020746D707661725F3130203D2028756E6974795F4D61747269785650202A2028756E6974795F4F626A656374546F576F726C64202A20746D707661725F313129293B0A20206869676870207665633420746578636F6F72645F31323B0A2020746578636F6F72645F31322E7879203D2028285F676C65734D756C7469546578436F6F7264302E7879202A205F4D61696E5465785F53542E787929202B205F4D61696E5465785F53542E7A77293B0A20206869676870207665633220746D707661725F31333B0A202069662028285F5556536563203D3D20302E302929207B0A20202020746D707661725F3133203D20746D707661725F323B0A20207D20656C7365207B0A20202020746D707661725F3133203D20746D707661725F333B0A20207D3B0A2020746578636F6F72645F31322E7A77203D202828746D707661725F3133202A205F44657461696C416C6265646F4D61705F53542E787929202B205F44657461696C416C6265646F4D61705F53542E7A77293B0A20206869676870207665633320746D707661725F31343B0A2020746D707661725F3134203D20285F576F726C6453706163654C69676874506F73302E78797A202D2028746D707661725F392E78797A202A205F576F726C6453706163654C69676874506F73302E7729293B0A20206C696768744469725F36203D20746D707661725F31343B0A20206D656469756D70207665633320746D707661725F31353B0A202068696768702076656333206E5F31363B0A20206E5F3136203D206C696768744469725F363B0A20206869676870207665633320746D707661725F31373B0A2020746D707661725F3137203D206E6F726D616C697A65286E5F3136293B0A2020746D707661725F3135203D20746D707661725F31373B0A20206C696768744469725F36203D20746D707661725F31353B0A20206869676870207665633320746D707661725F31383B0A2020746D707661725F3138203D206E6F726D616C697A652828746D707661725F392E78797A202D205F576F726C64537061636543616D657261506F7329293B0A20206579655665635F35203D20746D707661725F31383B0A202068696768702076656333206E6F726D5F31393B0A20206E6F726D5F3139203D20746D707661725F313B0A20206869676870206D61743320746D707661725F32303B0A2020746D707661725F32305B305D203D20756E6974795F576F726C64546F4F626A6563745B305D2E78797A3B0A2020746D707661725F32305B315D203D20756E6974795F576F726C64546F4F626A6563745B315D2E78797A3B0A2020746D707661725F32305B325D203D20756E6974795F576F726C64546F4F626A6563745B325D2E78797A3B0A20206869676870207665633320746D707661725F32313B0A2020746D707661725F3231203D206E6F726D616C697A6528286E6F726D5F3139202A20746D707661725F323029293B0A20206E6F726D616C576F726C645F34203D20746D707661725F32313B0A2020746D707661725F382E797A77203D20286579655665635F35202D2028322E30202A20280A20202020646F7420286E6F726D616C576F726C645F342C206579655665635F35290A2020202A206E6F726D616C576F726C645F342929293B0A2020676C5F506F736974696F6E203D20746D707661725F31303B0A2020786C765F544558434F4F524430203D20746578636F6F72645F31323B0A2020786C765F544558434F4F524431203D20746D707661725F392E78797A3B0A2020786C765F544558434F4F524432203D20746D707661725F373B0A2020786C765F544558434F4F524433203D20746D707661725F383B0A2020786C765F544558434F4F524434203D20746D707661725F31353B0A2020786C765F544558434F4F524435203D206E6F726D616C576F726C645F343B0A7D0A0A0A23656E6469660A23696664656620465241474D454E540A756E69666F726D2068696768702076656334205F4C69676874506F736974696F6E52616E67653B0A756E69666F726D206D656469756D702076656334205F4C69676874536861646F77446174613B0A756E69666F726D206C6F77702076656334205F4C69676874436F6C6F72303B0A756E69666F726D2073616D706C6572324420756E6974795F4E4878526F7567686E6573733B0A756E69666F726D2068696768702073616D706C657243756265205F536861646F774D6170546578747572653B0A756E69666F726D206D656469756D702076656334205F436F6C6F723B0A756E69666F726D2073616D706C65723244205F4D61696E5465783B0A756E69666F726D206D656469756D7020666C6F6174205F4D6574616C6C69633B0A756E69666F726D206D656469756D7020666C6F6174205F476C6F7373696E6573733B0A756E69666F726D2073616D706C65723244205F4C6967687454657874757265303B0A756E69666F726D206869676870206D61743420756E6974795F576F726C64546F4C696768743B0A76617279696E67206869676870207665633420786C765F544558434F4F5244303B0A76617279696E67206869676870207665633320786C765F544558434F4F5244313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244333B0A76617279696E67206D656469756D70207665633320786C765F544558434F4F5244343B0A76617279696E67206D656469756D70207665633320786C765F544558434F4F5244353B0A766F6964206D61696E2028290A7B0A20206C6F777020666C6F617420736861646F775F313B0A202068696768702076656333206C69676874436F6F72645F323B0A20206D656469756D70207665633320635F333B0A20206C6F7770207665633420746D707661725F343B0A2020746D707661725F34203D2074657874757265324420285F4D61696E5465782C20786C765F544558434F4F5244302E7879293B0A20206D656469756D70207665633320746D707661725F353B0A2020746D707661725F35203D20285F436F6C6F722E78797A202A20746D707661725F342E78797A293B0A20206D656469756D7020666C6F617420746D707661725F363B0A2020746D707661725F36203D20646F742028786C765F544558434F4F5244332E797A772C20786C765F544558434F4F524434293B0A20206D656469756D70207665633220746D707661725F373B0A2020746D707661725F372E78203D202828746D707661725F36202A20746D707661725F3629202A2028746D707661725F36202A20746D707661725F3629293B0A2020746D707661725F372E79203D2028312E30202D205F476C6F7373696E657373293B0A20206C6F7770207665633420746D707661725F383B0A2020746D707661725F38203D207465787475726532442028756E6974795F4E4878526F7567686E6573732C20746D707661725F37293B0A2020635F33203D20282828746D707661725F35202A200A2020202028302E37373930383337202D20285F4D6574616C6C6963202A20302E3737393038333729290A202029202B20280A2020202028746D707661725F382E77202A2031362E30290A2020202A200A202020206D697820287665633328302E323230393136332C20302E323230393136332C20302E32323039313633292C20746D707661725F352C2076656333285F4D6574616C6C696329290A20202929202A205F4C69676874436F6C6F72302E78797A293B0A20206869676870207665633420746D707661725F393B0A2020746D707661725F392E77203D20312E303B0A2020746D707661725F392E78797A203D20786C765F544558434F4F5244313B0A20206C69676874436F6F72645F32203D2028756E6974795F576F726C64546F4C69676874202A20746D707661725F39292E78797A3B0A202068696768702076656333207665635F31303B0A20207665635F3130203D2028786C765F544558434F4F524431202D205F4C69676874506F736974696F6E52616E67652E78797A293B0A2020686967687020666C6F6174206D79646973745F31313B0A20206D79646973745F3131203D20282873717274280A20202020646F7420287665635F31302C207665635F3130290A202029202A205F4C69676874506F736974696F6E52616E67652E7729202A20302E3937293B0A2020686967687020666C6F617420746D707661725F31323B0A2020746D707661725F3132203D20646F742028746578747572654375626520285F536861646F774D6170546578747572652C207665635F3130292C207665633428312E302C20302E3030333932313536392C20312E3533373837652D30352C20362E303330383633652D303829293B0A20206D656469756D7020666C6F617420746D707661725F31333B0A20206966202828746D707661725F3132203C206D79646973745F31312929207B0A20202020746D707661725F3133203D205F4C69676874536861646F77446174612E783B0A20207D20656C7365207B0A20202020746D707661725F3133203D20312E303B0A20207D3B0A2020736861646F775F31203D20746D707661725F31333B0A2020686967687020666C6F617420746D707661725F31343B0A2020746D707661725F3134203D20646F7420286C69676874436F6F72645F322C206C69676874436F6F72645F32293B0A20206C6F777020666C6F617420746D707661725F31353B0A2020746D707661725F3135203D202874657874757265324420285F4C6967687454657874757265302C207665633228746D707661725F313429292E77202A20736861646F775F31293B0A2020635F33203D2028635F33202A2028746D707661725F3135202A20636C616D7020280A20202020646F742028786C765F544558434F4F5244352C20786C765F544558434F4F524434290A20202C20302E302C20312E302929293B0A20206D656469756D70207665633420746D707661725F31363B0A2020746D707661725F31362E77203D20312E303B0A2020746D707661725F31362E78797A203D20635F333B0A20206D656469756D70207665633420786C61745F7661726F75747075745F31373B0A2020786C61745F7661726F75747075745F31372E78797A203D20746D707661725F31362E78797A3B0A2020786C61745F7661726F75747075745F31372E77203D20312E303B0A2020676C5F46726167446174615B305D203D20786C61745F7661726F75747075745F31373B0A7D0A0A0A23656E6469660A1B000000000000000100000000000000000000000000000000000000324F040C050000002C000000080000000400000000000000030000000C000000504F494E545F434F4F4B49450C000000534841444F57535F435542450C000000534841444F57535F534F4654811800002376657273696F6E203130300A0A236966646566205645525445580A6174747269627574652076656334205F676C65735665727465783B0A6174747269627574652076656333205F676C65734E6F726D616C3B0A6174747269627574652076656334205F676C65734D756C7469546578436F6F7264303B0A6174747269627574652076656334205F676C65734D756C7469546578436F6F7264313B0A756E69666F726D2068696768702076656333205F576F726C64537061636543616D657261506F733B0A756E69666F726D2068696768702076656334205F576F726C6453706163654C69676874506F73303B0A756E69666F726D206869676870206D61743420756E6974795F4F626A656374546F576F726C643B0A756E69666F726D206869676870206D61743420756E6974795F576F726C64546F4F626A6563743B0A756E69666F726D206869676870206D61743420756E6974795F4D617472697856503B0A756E69666F726D2068696768702076656334205F4D61696E5465785F53543B0A756E69666F726D2068696768702076656334205F44657461696C416C6265646F4D61705F53543B0A756E69666F726D206D656469756D7020666C6F6174205F55565365633B0A76617279696E67206869676870207665633420786C765F544558434F4F5244303B0A76617279696E67206869676870207665633320786C765F544558434F4F5244313B0A76617279696E67206869676870207665633220786C765F544558434F4F5244323B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244333B0A76617279696E67206D656469756D70207665633320786C765F544558434F4F5244343B0A76617279696E67206D656469756D70207665633320786C765F544558434F4F5244353B0A766F6964206D61696E2028290A7B0A20206D656469756D70207665633320746D707661725F313B0A2020746D707661725F31203D205F676C65734E6F726D616C3B0A20206869676870207665633220746D707661725F323B0A2020746D707661725F32203D205F676C65734D756C7469546578436F6F7264302E78793B0A20206869676870207665633220746D707661725F333B0A2020746D707661725F33203D205F676C65734D756C7469546578436F6F7264312E78793B0A20206D656469756D702076656333206E6F726D616C576F726C645F343B0A20206D656469756D702076656333206579655665635F353B0A20206D656469756D702076656333206C696768744469725F363B0A20206869676870207665633220746D707661725F373B0A20206D656469756D70207665633420746D707661725F383B0A20206869676870207665633420746D707661725F393B0A2020746D707661725F39203D2028756E6974795F4F626A656374546F576F726C64202A205F676C6573566572746578293B0A20206869676870207665633420746D707661725F31303B0A20206869676870207665633420746D707661725F31313B0A2020746D707661725F31312E77203D20312E303B0A2020746D707661725F31312E78797A203D205F676C65735665727465782E78797A3B0A2020746D707661725F3130203D2028756E6974795F4D61747269785650202A2028756E6974795F4F626A656374546F576F726C64202A20746D707661725F313129293B0A20206869676870207665633420746578636F6F72645F31323B0A2020746578636F6F72645F31322E7879203D2028285F676C65734D756C7469546578436F6F7264302E7879202A205F4D61696E5465785F53542E787929202B205F4D61696E5465785F53542E7A77293B0A20206869676870207665633220746D707661725F31333B0A202069662028285F5556536563203D3D20302E302929207B0A20202020746D707661725F3133203D20746D707661725F323B0A20207D20656C7365207B0A20202020746D707661725F3133203D20746D707661725F333B0A20207D3B0A2020746578636F6F72645F31322E7A77203D202828746D707661725F3133202A205F44657461696C416C6265646F4D61705F53542E787929202B205F44657461696C416C6265646F4D61705F53542E7A77293B0A20206869676870207665633320746D707661725F31343B0A2020746D707661725F3134203D20285F576F726C6453706163654C69676874506F73302E78797A202D2028746D707661725F392E78797A202A205F576F726C6453706163654C69676874506F73302E7729293B0A20206C696768744469725F36203D20746D707661725F31343B0A20206D656469756D70207665633320746D707661725F31353B0A202068696768702076656333206E5F31363B0A20206E5F3136203D206C696768744469725F363B0A20206869676870207665633320746D707661725F31373B0A2020746D707661725F3137203D206E6F726D616C697A65286E5F3136293B0A2020746D707661725F3135203D20746D707661725F31373B0A20206C696768744469725F36203D20746D707661725F31353B0A20206869676870207665633320746D707661725F31383B0A2020746D707661725F3138203D206E6F726D616C697A652828746D707661725F392E78797A202D205F576F726C64537061636543616D657261506F7329293B0A20206579655665635F35203D20746D707661725F31383B0A202068696768702076656333206E6F726D5F31393B0A20206E6F726D5F3139203D20746D707661725F313B0A20206869676870206D61743320746D707661725F32303B0A2020746D707661725F32305B305D203D20756E6974795F576F726C64546F4F626A6563745B305D2E78797A3B0A2020746D707661725F32305B315D203D20756E6974795F576F726C64546F4F626A6563745B315D2E78797A3B0A2020746D707661725F32305B325D203D20756E6974795F576F726C64546F4F626A6563745B325D2E78797A3B0A20206869676870207665633320746D707661725F32313B0A2020746D707661725F3231203D206E6F726D616C697A6528286E6F726D5F3139202A20746D707661725F323029293B0A20206E6F726D616C576F726C645F34203D20746D707661725F32313B0A2020746D707661725F382E797A77203D20286579655665635F35202D2028322E30202A20280A20202020646F7420286E6F726D616C576F726C645F342C206579655665635F35290A2020202A206E6F726D616C576F726C645F342929293B0A2020676C5F506F736974696F6E203D20746D707661725F31303B0A2020786C765F544558434F4F524430203D20746578636F6F72645F31323B0A2020786C765F544558434F4F524431203D20746D707661725F392E78797A3B0A2020786C765F544558434F4F524432203D20746D707661725F373B0A2020786C765F544558434F4F524433203D20746D707661725F383B0A2020786C765F544558434F4F524434203D20746D707661725F31353B0A2020786C765F544558434F4F524435203D206E6F726D616C576F726C645F343B0A7D0A0A0A23656E6469660A23696664656620465241474D454E540A756E69666F726D2068696768702076656334205F4C69676874506F736974696F6E52616E67653B0A756E69666F726D206D656469756D702076656334205F4C69676874536861646F77446174613B0A756E69666F726D206C6F77702076656334205F4C69676874436F6C6F72303B0A756E69666F726D2073616D706C6572324420756E6974795F4E4878526F7567686E6573733B0A756E69666F726D2068696768702073616D706C657243756265205F536861646F774D6170546578747572653B0A756E69666F726D206D656469756D702076656334205F436F6C6F723B0A756E69666F726D2073616D706C65723244205F4D61696E5465783B0A756E69666F726D206D656469756D7020666C6F6174205F4D6574616C6C69633B0A756E69666F726D206D656469756D7020666C6F6174205F476C6F7373696E6573733B0A756E69666F726D206C6F77702073616D706C657243756265205F4C6967687454657874757265303B0A756E69666F726D206869676870206D61743420756E6974795F576F726C64546F4C696768743B0A756E69666F726D2073616D706C65723244205F4C696768745465787475726542303B0A76617279696E67206869676870207665633420786C765F544558434F4F5244303B0A76617279696E67206869676870207665633320786C765F544558434F4F5244313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244333B0A76617279696E67206D656469756D70207665633320786C765F544558434F4F5244343B0A76617279696E67206D656469756D70207665633320786C765F544558434F4F5244353B0A766F6964206D61696E2028290A7B0A20206C6F777020666C6F617420736861646F775F313B0A202068696768702076656333206C69676874436F6F72645F323B0A20206D656469756D70207665633320635F333B0A20206C6F7770207665633420746D707661725F343B0A2020746D707661725F34203D2074657874757265324420285F4D61696E5465782C20786C765F544558434F4F5244302E7879293B0A20206D656469756D70207665633320746D707661725F353B0A2020746D707661725F35203D20285F436F6C6F722E78797A202A20746D707661725F342E78797A293B0A20206D656469756D7020666C6F617420746D707661725F363B0A2020746D707661725F36203D20646F742028786C765F544558434F4F5244332E797A772C20786C765F544558434F4F524434293B0A20206D656469756D70207665633220746D707661725F373B0A2020746D707661725F372E78203D202828746D707661725F36202A20746D707661725F3629202A2028746D707661725F36202A20746D707661725F3629293B0A2020746D707661725F372E79203D2028312E30202D205F476C6F7373696E657373293B0A20206C6F7770207665633420746D707661725F383B0A2020746D707661725F38203D207465787475726532442028756E6974795F4E4878526F7567686E6573732C20746D707661725F37293B0A2020635F33203D20282828746D707661725F35202A200A2020202028302E37373930383337202D20285F4D6574616C6C6963202A20302E3737393038333729290A202029202B20280A2020202028746D707661725F382E77202A2031362E30290A2020202A200A202020206D697820287665633328302E323230393136332C20302E323230393136332C20302E32323039313633292C20746D707661725F352C2076656333285F4D6574616C6C696329290A20202929202A205F4C69676874436F6C6F72302E78797A293B0A20206869676870207665633420746D707661725F393B0A2020746D707661725F392E77203D20312E303B0A2020746D707661725F392E78797A203D20786C765F544558434F4F5244313B0A20206C69676874436F6F72645F32203D2028756E6974795F576F726C64546F4C69676874202A20746D707661725F39292E78797A3B0A202068696768702076656333207665635F31303B0A20207665635F3130203D2028786C765F544558434F4F524431202D205F4C69676874506F736974696F6E52616E67652E78797A293B0A20206869676870207665633420736861646F7756616C735F31313B0A2020686967687020666C6F6174206D79646973745F31323B0A20206D79646973745F3132203D20282873717274280A20202020646F7420287665635F31302C207665635F3130290A202029202A205F4C69676874506F736974696F6E52616E67652E7729202A20302E3937293B0A2020736861646F7756616C735F31312E78203D20646F742028746578747572654375626520285F536861646F774D6170546578747572652C20287665635F3130202B207665633328302E303037383132352C20302E303037383132352C20302E303037383132352929292C207665633428312E302C20302E3030333932313536392C20312E3533373837652D30352C20362E303330383633652D303829293B0A2020736861646F7756616C735F31312E79203D20646F742028746578747572654375626520285F536861646F774D6170546578747572652C20287665635F3130202B2076656333282D302E303037383132352C202D302E303037383132352C20302E303037383132352929292C207665633428312E302C20302E3030333932313536392C20312E3533373837652D30352C20362E303330383633652D303829293B0A2020736861646F7756616C735F31312E7A203D20646F742028746578747572654375626520285F536861646F774D6170546578747572652C20287665635F3130202B2076656333282D302E303037383132352C20302E303037383132352C202D302E303037383132352929292C207665633428312E302C20302E3030333932313536392C20312E3533373837652D30352C20362E303330383633652D303829293B0A2020736861646F7756616C735F31312E77203D20646F742028746578747572654375626520285F536861646F774D6170546578747572652C20287665635F3130202B207665633328302E303037383132352C202D302E303037383132352C202D302E303037383132352929292C207665633428312E302C20302E3030333932313536392C20312E3533373837652D30352C20362E303330383633652D303829293B0A2020627665633420746D707661725F31333B0A2020746D707661725F3133203D206C6573735468616E2028736861646F7756616C735F31312C2076656334286D79646973745F313229293B0A20206D656469756D70207665633420746D707661725F31343B0A2020746D707661725F3134203D205F4C69676874536861646F77446174612E787878783B0A20206D656469756D7020666C6F617420746D707661725F31353B0A202069662028746D707661725F31332E7829207B0A20202020746D707661725F3135203D20746D707661725F31342E783B0A20207D20656C7365207B0A20202020746D707661725F3135203D20312E303B0A20207D3B0A20206D656469756D7020666C6F617420746D707661725F31363B0A202069662028746D707661725F31332E7929207B0A20202020746D707661725F3136203D20746D707661725F31342E793B0A20207D20656C7365207B0A20202020746D707661725F3136203D20312E303B0A20207D3B0A20206D656469756D7020666C6F617420746D707661725F31373B0A202069662028746D707661725F31332E7A29207B0A20202020746D707661725F3137203D20746D707661725F31342E7A3B0A20207D20656C7365207B0A20202020746D707661725F3137203D20312E303B0A20207D3B0A20206D656469756D7020666C6F617420746D707661725F31383B0A202069662028746D707661725F31332E7729207B0A20202020746D707661725F3138203D20746D707661725F31342E773B0A20207D20656C7365207B0A20202020746D707661725F3138203D20312E303B0A20207D3B0A20206D656469756D70207665633420746D707661725F31393B0A2020746D707661725F31392E78203D20746D707661725F31353B0A2020746D707661725F31392E79203D20746D707661725F31363B0A2020746D707661725F31392E7A203D20746D707661725F31373B0A2020746D707661725F31392E77203D20746D707661725F31383B0A20206D656469756D7020666C6F617420746D707661725F32303B0A2020746D707661725F3230203D20646F742028746D707661725F31392C207665633428302E32352C20302E32352C20302E32352C20302E323529293B0A2020736861646F775F31203D20746D707661725F32303B0A2020686967687020666C6F617420746D707661725F32313B0A2020746D707661725F3231203D20646F7420286C69676874436F6F72645F322C206C69676874436F6F72645F32293B0A20206C6F777020666C6F617420746D707661725F32323B0A2020746D707661725F3232203D20282874657874757265324420285F4C696768745465787475726542302C207665633228746D707661725F323129292E77202A20746578747572654375626520285F4C6967687454657874757265302C206C69676874436F6F72645F32292E7729202A20736861646F775F31293B0A2020635F33203D2028635F33202A2028746D707661725F3232202A20636C616D7020280A20202020646F742028786C765F544558434F4F5244352C20786C765F544558434F4F524434290A20202C20302E302C20312E302929293B0A20206D656469756D70207665633420746D707661725F32333B0A2020746D707661725F32332E77203D20312E303B0A2020746D707661725F32332E78797A203D20635F333B0A20206D656469756D70207665633420786C61745F7661726F75747075745F32343B0A2020786C61745F7661726F75747075745F32342E78797A203D20746D707661725F32332E78797A3B0A2020786C61745F7661726F75747075745F32342E77203D20312E303B0A2020676C5F46726167446174615B305D203D20786C61745F7661726F75747075745F32343B0A7D0A0A0A23656E6469660A0000001B000000000000000100000000000000000000000000000000000000324F040C0500000023000000040000000000000000000000010000000B000000444952454354494F4E414C001F1900002376657273696F6E203130300A0A236966646566205645525445580A6174747269627574652076656334205F676C65735665727465783B0A6174747269627574652076656333205F676C65734E6F726D616C3B0A6174747269627574652076656334205F676C65734D756C7469546578436F6F7264303B0A6174747269627574652076656334205F676C65734D756C7469546578436F6F7264313B0A756E69666F726D2068696768702076656333205F576F726C64537061636543616D657261506F733B0A756E69666F726D206D656469756D70207665633420756E6974795F534841723B0A756E69666F726D206D656469756D70207665633420756E6974795F534841673B0A756E69666F726D206D656469756D70207665633420756E6974795F534841623B0A756E69666F726D206D656469756D70207665633420756E6974795F534842723B0A756E69666F726D206D656469756D70207665633420756E6974795F534842673B0A756E69666F726D206D656469756D70207665633420756E6974795F534842623B0A756E69666F726D206D656469756D70207665633420756E6974795F5348433B0A756E69666F726D206869676870206D61743420756E6974795F4F626A656374546F576F726C643B0A756E69666F726D206869676870206D61743420756E6974795F576F726C64546F4F626A6563743B0A756E69666F726D206869676870206D61743420756E6974795F4D617472697856503B0A756E69666F726D2068696768702076656334205F4D61696E5465785F53543B0A756E69666F726D2068696768702076656334205F44657461696C416C6265646F4D61705F53543B0A756E69666F726D206D656469756D7020666C6F6174205F4D6574616C6C69633B0A756E69666F726D206D656469756D7020666C6F6174205F476C6F7373696E6573733B0A756E69666F726D206D656469756D7020666C6F6174205F55565365633B0A76617279696E67206869676870207665633420786C765F544558434F4F5244303B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244323B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244343B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244353B0A766F6964206D61696E2028290A7B0A20206D656469756D70207665633320746D707661725F313B0A2020746D707661725F31203D205F676C65734E6F726D616C3B0A20206869676870207665633220746D707661725F323B0A2020746D707661725F32203D205F676C65734D756C7469546578436F6F7264302E78793B0A20206869676870207665633220746D707661725F333B0A2020746D707661725F33203D205F676C65734D756C7469546578436F6F7264312E78793B0A20206D656469756D702076656333206E6F726D616C576F726C645F343B0A20206D656469756D702076656333206579655665635F353B0A20206D656469756D70207665633420746D707661725F363B0A20206D656469756D70207665633420746D707661725F373B0A20206D656469756D70207665633420746D707661725F383B0A20206869676870207665633420746D707661725F393B0A2020746D707661725F39203D2028756E6974795F4F626A656374546F576F726C64202A205F676C6573566572746578293B0A20206869676870207665633420746D707661725F31303B0A20206869676870207665633420746D707661725F31313B0A2020746D707661725F31312E77203D20312E303B0A2020746D707661725F31312E78797A203D205F676C65735665727465782E78797A3B0A2020746D707661725F3130203D2028756E6974795F4D61747269785650202A2028756E6974795F4F626A656374546F576F726C64202A20746D707661725F313129293B0A20206869676870207665633420746578636F6F72645F31323B0A2020746578636F6F72645F31322E7879203D2028285F676C65734D756C7469546578436F6F7264302E7879202A205F4D61696E5465785F53542E787929202B205F4D61696E5465785F53542E7A77293B0A20206869676870207665633220746D707661725F31333B0A202069662028285F5556536563203D3D20302E302929207B0A20202020746D707661725F3133203D20746D707661725F323B0A20207D20656C7365207B0A20202020746D707661725F3133203D20746D707661725F333B0A20207D3B0A2020746578636F6F72645F31322E7A77203D202828746D707661725F3133202A205F44657461696C416C6265646F4D61705F53542E787929202B205F44657461696C416C6265646F4D61705F53542E7A77293B0A20206869676870207665633320746D707661725F31343B0A2020746D707661725F3134203D206E6F726D616C697A652828746D707661725F392E78797A202D205F576F726C64537061636543616D657261506F7329293B0A20206579655665635F35203D20746D707661725F31343B0A202068696768702076656333206E6F726D5F31353B0A20206E6F726D5F3135203D20746D707661725F313B0A20206869676870206D61743320746D707661725F31363B0A2020746D707661725F31365B305D203D20756E6974795F576F726C64546F4F626A6563745B305D2E78797A3B0A2020746D707661725F31365B315D203D20756E6974795F576F726C64546F4F626A6563745B315D2E78797A3B0A2020746D707661725F31365B325D203D20756E6974795F576F726C64546F4F626A6563745B325D2E78797A3B0A20206869676870207665633320746D707661725F31373B0A2020746D707661725F3137203D206E6F726D616C697A6528286E6F726D5F3135202A20746D707661725F313629293B0A20206E6F726D616C576F726C645F34203D20746D707661725F31373B0A2020746D707661725F382E78797A203D206E6F726D616C576F726C645F343B0A2020746D707661725F362E78797A203D206579655665635F353B0A20206D656469756D70207665633420616D6269656E744F724C696768746D617055565F31383B0A2020616D6269656E744F724C696768746D617055565F31382E77203D20302E303B0A20206D656469756D70207665633420746D707661725F31393B0A2020746D707661725F31392E77203D20312E303B0A2020746D707661725F31392E78797A203D206E6F726D616C576F726C645F343B0A20206D656469756D702076656333207265735F32303B0A20206D656469756D70207665633320785F32313B0A2020785F32312E78203D20646F742028756E6974795F534841722C20746D707661725F3139293B0A2020785F32312E79203D20646F742028756E6974795F534841672C20746D707661725F3139293B0A2020785F32312E7A203D20646F742028756E6974795F534841622C20746D707661725F3139293B0A20206D656469756D7020766563332078315F32323B0A20206D656469756D70207665633420746D707661725F32333B0A2020746D707661725F3233203D20286E6F726D616C576F726C645F342E78797A7A202A206E6F726D616C576F726C645F342E797A7A78293B0A202078315F32322E78203D20646F742028756E6974795F534842722C20746D707661725F3233293B0A202078315F32322E79203D20646F742028756E6974795F534842672C20746D707661725F3233293B0A202078315F32322E7A203D20646F742028756E6974795F534842622C20746D707661725F3233293B0A20207265735F3230203D2028785F3231202B202878315F3232202B2028756E6974795F5348432E78797A202A200A2020202028286E6F726D616C576F726C645F342E78202A206E6F726D616C576F726C645F342E7829202D20286E6F726D616C576F726C645F342E79202A206E6F726D616C576F726C645F342E7929290A20202929293B0A20206D656469756D70207665633320746D707661725F32343B0A2020746D707661725F3234203D206D617820282828312E303535202A200A20202020706F7720286D617820287265735F32302C207665633328302E302C20302E302C20302E3029292C207665633328302E343136363636372C20302E343136363636372C20302E3431363636363729290A202029202D20302E303535292C207665633328302E302C20302E302C20302E3029293B0A20207265735F3230203D20746D707661725F32343B0A2020616D6269656E744F724C696768746D617055565F31382E78797A203D206D617820287665633328302E302C20302E302C20302E30292C20746D707661725F3234293B0A2020746D707661725F372E797A77203D20286579655665635F35202D2028322E30202A20280A20202020646F7420286E6F726D616C576F726C645F342C206579655665635F35290A2020202A206E6F726D616C576F726C645F342929293B0A20206D656469756D7020666C6F617420785F32353B0A2020785F3235203D2028312E30202D20636C616D702028646F7420286E6F726D616C576F726C645F342C200A202020202D286579655665635F35290A2020292C20302E302C20312E3029293B0A2020746D707661725F382E77203D202828785F3235202A20785F323529202A2028785F3235202A20785F323529293B0A2020746D707661725F362E77203D20636C616D702028285F476C6F7373696E657373202B2028312E30202D200A2020202028302E37373930383337202D20285F4D6574616C6C6963202A20302E3737393038333729290A202029292C20302E302C20312E30293B0A2020676C5F506F736974696F6E203D20746D707661725F31303B0A2020786C765F544558434F4F524430203D20746578636F6F72645F31323B0A2020786C765F544558434F4F524431203D20746D707661725F363B0A2020786C765F544558434F4F524432203D20616D6269656E744F724C696768746D617055565F31383B0A2020786C765F544558434F4F524434203D20746D707661725F373B0A2020786C765F544558434F4F524435203D20746D707661725F383B0A7D0A0A0A23656E6469660A23696664656620465241474D454E540A23657874656E73696F6E20474C5F4558545F7368616465725F746578747572655F6C6F64203A20656E61626C650A6C6F7770207665633420696D706C5F6C6F775F74657874757265437562654C6F64455854286C6F77702073616D706C6572437562652073616D706C65722C206869676870207665633320636F6F72642C206D656469756D7020666C6F6174206C6F64290A7B0A23696620646566696E656428474C5F4558545F7368616465725F746578747572655F6C6F64290A0972657475726E2074657874757265437562654C6F644558542873616D706C65722C20636F6F72642C206C6F64293B0A23656C73650A0972657475726E2074657874757265437562652873616D706C65722C20636F6F72642C206C6F64293B0A23656E6469660A7D0A0A756E69666F726D206D656469756D702076656334205F576F726C6453706163654C69676874506F73303B0A756E69666F726D206C6F77702073616D706C65724375626520756E6974795F5370656343756265303B0A756E69666F726D206D656469756D70207665633420756E6974795F5370656343756265305F4844523B0A756E69666F726D206C6F77702076656334205F4C69676874436F6C6F72303B0A756E69666F726D2073616D706C6572324420756E6974795F4E4878526F7567686E6573733B0A756E69666F726D206D656469756D702076656334205F436F6C6F723B0A756E69666F726D2073616D706C65723244205F4D61696E5465783B0A756E69666F726D206D656469756D7020666C6F6174205F4D6574616C6C69633B0A756E69666F726D206D656469756D7020666C6F6174205F476C6F7373696E6573733B0A756E69666F726D2073616D706C65723244205F4F63636C7573696F6E4D61703B0A76617279696E67206869676870207665633420786C765F544558434F4F5244303B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244323B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244343B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244353B0A766F6964206D61696E2028290A7B0A20206D656469756D7020666C6F617420726C5F313B0A20206C6F7770207665633420746D707661725F323B0A2020746D707661725F32203D2074657874757265324420285F4D61696E5465782C20786C765F544558434F4F5244302E7879293B0A20206D656469756D70207665633320746D707661725F333B0A2020746D707661725F33203D20285F436F6C6F722E78797A202A20746D707661725F322E78797A293B0A20206D656469756D70207665633320746D707661725F343B0A20206D656469756D70207665633320746D707661725F353B0A2020746D707661725F35203D206D697820287665633328302E323230393136332C20302E323230393136332C20302E32323039313633292C20746D707661725F332C2076656333285F4D6574616C6C696329293B0A2020746D707661725F34203D2028746D707661725F33202A2028302E37373930383337202D20285F4D6574616C6C6963202A20302E373739303833372929293B0A20206D656469756D70207665633320746D707661725F363B0A2020746D707661725F36203D205F4C69676874436F6C6F72302E78797A3B0A20206D656469756D7020666C6F617420746D707661725F373B0A20206C6F7770207665633420746D707661725F383B0A2020746D707661725F38203D2074657874757265324420285F4F63636C7573696F6E4D61702C20786C765F544558434F4F5244302E7879293B0A2020746D707661725F37203D20746D707661725F382E793B0A2020726C5F31203D20646F742028786C765F544558434F4F5244342E797A772C205F576F726C6453706163654C69676874506F73302E78797A293B0A20206869676870207665633420746D707661725F393B0A2020746D707661725F39203D20756E6974795F5370656343756265305F4844523B0A20206D656469756D7020666C6F617420746D707661725F31303B0A2020746D707661725F3130203D2028312E30202D205F476C6F7373696E657373293B0A20206D656469756D702076656334206864725F31313B0A20206864725F3131203D20746D707661725F393B0A20206D656469756D70207665633420746D707661725F31323B0A2020746D707661725F31322E78797A203D20786C765F544558434F4F5244342E797A773B0A2020746D707661725F31322E77203D202828746D707661725F3130202A2028312E37202D200A2020202028302E37202A20746D707661725F3130290A20202929202A20362E30293B0A20206C6F7770207665633420746D707661725F31333B0A2020746D707661725F3133203D20696D706C5F6C6F775F74657874757265437562654C6F644558542028756E6974795F5370656343756265302C20786C765F544558434F4F5244342E797A772C20746D707661725F31322E77293B0A20206D656469756D70207665633420746D707661725F31343B0A2020746D707661725F3134203D20746D707661725F31333B0A20206D656469756D70207665633220746D707661725F31353B0A2020746D707661725F31352E78203D202828726C5F31202A20726C5F3129202A2028726C5F31202A20726C5F3129293B0A2020746D707661725F31352E79203D20746D707661725F31303B0A20206C6F7770207665633420746D707661725F31363B0A2020746D707661725F3136203D207465787475726532442028756E6974795F4E4878526F7567686E6573732C20746D707661725F3135293B0A20206D656469756D70207665633420746D707661725F31373B0A2020746D707661725F31372E77203D20312E303B0A2020746D707661725F31372E78797A203D202828280A2020202028786C765F544558434F4F5244322E78797A202A20746D707661725F37290A2020202A20746D707661725F3429202B20280A202020202828286864725F31312E78202A20280A202020202020286864725F31312E77202A2028746D707661725F31342E77202D20312E3029290A20202020202B20312E302929202A20746D707661725F31342E78797A29202A20746D707661725F37290A2020202A200A202020206D69782028746D707661725F352C20786C765F544558434F4F5244312E7777772C20786C765F544558434F4F5244352E777777290A20202929202B202828746D707661725F34202B200A202020202828746D707661725F31362E77202A2031362E3029202A20746D707661725F35290A202029202A2028746D707661725F36202A200A20202020636C616D702028646F742028786C765F544558434F4F5244352E78797A2C205F576F726C6453706163654C69676874506F73302E78797A292C20302E302C20312E30290A20202929293B0A20206D656469756D70207665633420786C61745F7661726F75747075745F31383B0A2020786C61745F7661726F75747075745F31382E78797A203D20746D707661725F31372E78797A3B0A2020786C61745F7661726F75747075745F31382E77203D20312E303B0A2020676C5F46726167446174615B305D203D20786C61745F7661726F75747075745F31383B0A7D0A0A0A23656E6469660A001B000000000000000100000000000000000000000000000000000000324F040C0500000000000000000000000000000000000000020000000C000000504F494E545F434F4F4B49450C000000534841444F57535F435542450000000000000000000000000100000000000000000000000000000000000000324F040C0500000055000000030000000000000000000000010000000B000000444952454354494F4E414C00B71C00002376657273696F6E203130300A0A236966646566205645525445580A6174747269627574652076656334205F676C65735665727465783B0A6174747269627574652076656333205F676C65734E6F726D616C3B0A6174747269627574652076656334205F676C65734D756C7469546578436F6F7264303B0A6174747269627574652076656334205F676C65734D756C7469546578436F6F7264313B0A756E69666F726D2068696768702076656333205F576F726C64537061636543616D657261506F733B0A756E69666F726D206D656469756D70207665633420756E6974795F534842723B0A756E69666F726D206D656469756D70207665633420756E6974795F534842673B0A756E69666F726D206D656469756D70207665633420756E6974795F534842623B0A756E69666F726D206D656469756D70207665633420756E6974795F5348433B0A756E69666F726D206869676870206D61743420756E6974795F4F626A656374546F576F726C643B0A756E69666F726D206869676870206D61743420756E6974795F576F726C64546F4F626A6563743B0A756E69666F726D206869676870206D61743420756E6974795F4D617472697856503B0A756E69666F726D2068696768702076656334205F4D61696E5465785F53543B0A756E69666F726D2068696768702076656334205F44657461696C416C6265646F4D61705F53543B0A756E69666F726D206D656469756D7020666C6F6174205F55565365633B0A76617279696E67206869676870207665633420786C765F544558434F4F5244303B0A76617279696E67206D656469756D70207665633320786C765F544558434F4F5244313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244323B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244325F313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244325F323B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244353B0A76617279696E67206869676870207665633220786C765F544558434F4F5244363B0A76617279696E67206869676870207665633320786C765F544558434F4F5244383B0A766F6964206D61696E2028290A7B0A20206D656469756D70207665633320746D707661725F313B0A2020746D707661725F31203D205F676C65734E6F726D616C3B0A20206869676870207665633220746D707661725F323B0A2020746D707661725F32203D205F676C65734D756C7469546578436F6F7264302E78793B0A20206869676870207665633220746D707661725F333B0A2020746D707661725F33203D205F676C65734D756C7469546578436F6F7264312E78793B0A20206D656469756D70207665633420746D707661725F343B0A20206D656469756D70207665633420746D707661725F353B0A20206D656469756D70207665633420746D707661725F363B0A20206869676870207665633220746D707661725F373B0A20206869676870207665633320746D707661725F383B0A20206869676870207665633420746D707661725F393B0A2020746D707661725F39203D2028756E6974795F4F626A656374546F576F726C64202A205F676C6573566572746578293B0A2020746D707661725F38203D20746D707661725F392E78797A3B0A20206869676870207665633420746D707661725F31303B0A20206869676870207665633420746D707661725F31313B0A2020746D707661725F31312E77203D20312E303B0A2020746D707661725F31312E78797A203D205F676C65735665727465782E78797A3B0A2020746D707661725F3130203D2028756E6974795F4D61747269785650202A2028756E6974795F4F626A656374546F576F726C64202A20746D707661725F313129293B0A20206869676870207665633420746578636F6F72645F31323B0A2020746578636F6F72645F31322E7879203D2028285F676C65734D756C7469546578436F6F7264302E7879202A205F4D61696E5465785F53542E787929202B205F4D61696E5465785F53542E7A77293B0A20206869676870207665633220746D707661725F31333B0A202069662028285F5556536563203D3D20302E302929207B0A20202020746D707661725F3133203D20746D707661725F323B0A20207D20656C7365207B0A20202020746D707661725F3133203D20746D707661725F333B0A20207D3B0A2020746578636F6F72645F31322E7A77203D202828746D707661725F3133202A205F44657461696C416C6265646F4D61705F53542E787929202B205F44657461696C416C6265646F4D61705F53542E7A77293B0A20206D656469756D70207665633320746D707661725F31343B0A202068696768702076656333206E5F31353B0A20206E5F3135203D2028746D707661725F392E78797A202D205F576F726C64537061636543616D657261506F73293B0A2020746D707661725F3134203D206E5F31353B0A202068696768702076656333206E6F726D5F31363B0A20206E6F726D5F3136203D20746D707661725F313B0A20206869676870206D61743320746D707661725F31373B0A2020746D707661725F31375B305D203D20756E6974795F576F726C64546F4F626A6563745B305D2E78797A3B0A2020746D707661725F31375B315D203D20756E6974795F576F726C64546F4F626A6563745B315D2E78797A3B0A2020746D707661725F31375B325D203D20756E6974795F576F726C64546F4F626A6563745B325D2E78797A3B0A20206869676870207665633320746D707661725F31383B0A2020746D707661725F3138203D206E6F726D616C697A6528286E6F726D5F3136202A20746D707661725F313729293B0A2020746D707661725F342E78797A203D207665633328302E302C20302E302C20302E30293B0A2020746D707661725F352E78797A203D207665633328302E302C20302E302C20302E30293B0A2020746D707661725F362E78797A203D20746D707661725F31383B0A20206D656469756D702076656333206E6F726D616C576F726C645F31393B0A20206E6F726D616C576F726C645F3139203D20746D707661725F31383B0A20206D656469756D70207665633420616D6269656E744F724C696768746D617055565F32303B0A2020616D6269656E744F724C696768746D617055565F32302E77203D20302E303B0A20206D656469756D7020766563332078315F32313B0A20206D656469756D70207665633420746D707661725F32323B0A2020746D707661725F3232203D20286E6F726D616C576F726C645F31392E78797A7A202A206E6F726D616C576F726C645F31392E797A7A78293B0A202078315F32312E78203D20646F742028756E6974795F534842722C20746D707661725F3232293B0A202078315F32312E79203D20646F742028756E6974795F534842672C20746D707661725F3232293B0A202078315F32312E7A203D20646F742028756E6974795F534842622C20746D707661725F3232293B0A2020616D6269656E744F724C696768746D617055565F32302E78797A203D202878315F3231202B2028756E6974795F5348432E78797A202A20280A20202020286E6F726D616C576F726C645F31392E78202A206E6F726D616C576F726C645F31392E78290A2020202D200A20202020286E6F726D616C576F726C645F31392E79202A206E6F726D616C576F726C645F31392E79290A20202929293B0A2020676C5F506F736974696F6E203D20746D707661725F31303B0A2020786C765F544558434F4F524430203D20746578636F6F72645F31323B0A2020786C765F544558434F4F524431203D20746D707661725F31343B0A2020786C765F544558434F4F524432203D20746D707661725F343B0A2020786C765F544558434F4F5244325F31203D20746D707661725F353B0A2020786C765F544558434F4F5244325F32203D20746D707661725F363B0A2020786C765F544558434F4F524435203D20616D6269656E744F724C696768746D617055565F32303B0A2020786C765F544558434F4F524436203D20746D707661725F373B0A2020786C765F544558434F4F524438203D20746D707661725F383B0A7D0A0A0A23656E6469660A23696664656620465241474D454E540A23657874656E73696F6E20474C5F4558545F7368616465725F746578747572655F6C6F64203A20656E61626C650A6C6F7770207665633420696D706C5F6C6F775F74657874757265437562654C6F64455854286C6F77702073616D706C6572437562652073616D706C65722C206869676870207665633320636F6F72642C206D656469756D7020666C6F6174206C6F64290A7B0A23696620646566696E656428474C5F4558545F7368616465725F746578747572655F6C6F64290A0972657475726E2074657874757265437562654C6F644558542873616D706C65722C20636F6F72642C206C6F64293B0A23656C73650A0972657475726E2074657874757265437562652873616D706C65722C20636F6F72642C206C6F64293B0A23656E6469660A7D0A0A756E69666F726D206D656469756D702076656334205F576F726C6453706163654C69676874506F73303B0A756E69666F726D206D656469756D70207665633420756E6974795F534841723B0A756E69666F726D206D656469756D70207665633420756E6974795F534841673B0A756E69666F726D206D656469756D70207665633420756E6974795F534841623B0A756E69666F726D206C6F77702073616D706C65724375626520756E6974795F5370656343756265303B0A756E69666F726D206D656469756D70207665633420756E6974795F5370656343756265305F4844523B0A756E69666F726D206C6F77702076656334205F4C69676874436F6C6F72303B0A756E69666F726D206D656469756D702076656334205F436F6C6F723B0A756E69666F726D2073616D706C65723244205F4D61696E5465783B0A756E69666F726D206D656469756D7020666C6F6174205F4D6574616C6C69633B0A756E69666F726D206D656469756D7020666C6F6174205F476C6F7373696E6573733B0A756E69666F726D2073616D706C65723244205F4F63636C7573696F6E4D61703B0A756E69666F726D206D656469756D7020666C6F6174205F4F63636C7573696F6E537472656E6774683B0A76617279696E67206869676870207665633420786C765F544558434F4F5244303B0A76617279696E67206D656469756D70207665633320786C765F544558434F4F5244313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244325F323B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244353B0A766F6964206D61696E2028290A7B0A20206D656469756D70207665633420635F313B0A20206C6F7770207665633420746D707661725F323B0A2020746D707661725F32203D2074657874757265324420285F4D61696E5465782C20786C765F544558434F4F5244302E7879293B0A20206D656469756D70207665633320746D707661725F333B0A2020746D707661725F33203D20285F436F6C6F722E78797A202A20746D707661725F322E78797A293B0A20206D656469756D70207665633320746D707661725F343B0A20206D656469756D70207665633320746D707661725F353B0A2020746D707661725F35203D206D697820287665633328302E323230393136332C20302E323230393136332C20302E32323039313633292C20746D707661725F332C2076656333285F4D6574616C6C696329293B0A20206D656469756D7020666C6F617420746D707661725F363B0A2020746D707661725F36203D2028302E37373930383337202D20285F4D6574616C6C6963202A20302E3737393038333729293B0A2020746D707661725F34203D2028746D707661725F33202A20746D707661725F36293B0A20206D656469756D70207665633320746D707661725F373B0A2020746D707661725F37203D206E6F726D616C697A6528786C765F544558434F4F5244325F322E78797A293B0A20206D656469756D70207665633320746D707661725F383B0A2020746D707661725F38203D206E6F726D616C697A6528786C765F544558434F4F524431293B0A20206D656469756D70207665633320746D707661725F393B0A2020746D707661725F39203D205F4C69676874436F6C6F72302E78797A3B0A20206D656469756D7020666C6F6174206F63635F31303B0A20206C6F777020666C6F617420746D707661725F31313B0A2020746D707661725F3131203D2074657874757265324420285F4F63636C7573696F6E4D61702C20786C765F544558434F4F5244302E7879292E793B0A20206F63635F3130203D20746D707661725F31313B0A20206D656469756D7020666C6F6174206F63636C7573696F6E5F31323B0A20206F63636C7573696F6E5F3132203D202828312E30202D205F4F63636C7573696F6E537472656E67746829202B20286F63635F3130202A205F4F63636C7573696F6E537472656E67746829293B0A20206869676870207665633420746D707661725F31333B0A2020746D707661725F3133203D20756E6974795F5370656343756265305F4844523B0A20206D656469756D70207665633420746D707661725F31343B0A2020746D707661725F31342E77203D20312E303B0A2020746D707661725F31342E78797A203D20746D707661725F373B0A20206D656469756D70207665633320785F31353B0A2020785F31352E78203D20646F742028756E6974795F534841722C20746D707661725F3134293B0A2020785F31352E79203D20646F742028756E6974795F534841672C20746D707661725F3134293B0A2020785F31352E7A203D20646F742028756E6974795F534841622C20746D707661725F3134293B0A20206D656469756D70207665633320746D707661725F31363B0A20206D656469756D7020666C6F617420746D707661725F31373B0A2020746D707661725F3137203D2028312E30202D205F476C6F7373696E657373293B0A2020746D707661725F3136203D2028746D707661725F38202D2028322E30202A20280A20202020646F742028746D707661725F372C20746D707661725F38290A2020202A20746D707661725F372929293B0A20206D656469756D702076656334206864725F31383B0A20206864725F3138203D20746D707661725F31333B0A20206D656469756D70207665633420746D707661725F31393B0A2020746D707661725F31392E78797A203D20746D707661725F31363B0A2020746D707661725F31392E77203D202828746D707661725F3137202A2028312E37202D200A2020202028302E37202A20746D707661725F3137290A20202929202A20362E30293B0A20206C6F7770207665633420746D707661725F32303B0A2020746D707661725F3230203D20696D706C5F6C6F775F74657874757265437562654C6F644558542028756E6974795F5370656343756265302C20746D707661725F31362C20746D707661725F31392E77293B0A20206D656469756D70207665633420746D707661725F32313B0A2020746D707661725F3231203D20746D707661725F32303B0A20206D656469756D70207665633320766965774469725F32323B0A2020766965774469725F3232203D202D28746D707661725F38293B0A20206D656469756D7020666C6F61742073706563756C61725465726D5F32333B0A20206D656469756D70207665633320746D707661725F32343B0A20206D656469756D70207665633320696E5665635F32353B0A2020696E5665635F3235203D20285F576F726C6453706163654C69676874506F73302E78797A202B20766965774469725F3232293B0A2020746D707661725F3234203D2028696E5665635F3235202A20696E766572736573717274286D61782028302E3030312C200A20202020646F742028696E5665635F32352C20696E5665635F3235290A20202929293B0A20206D656469756D7020666C6F617420746D707661725F32363B0A2020746D707661725F3236203D20636C616D702028646F742028746D707661725F372C20746D707661725F3234292C20302E302C20312E30293B0A20206D656469756D7020666C6F617420746D707661725F32373B0A2020746D707661725F3237203D2028746D707661725F3137202A20746D707661725F3137293B0A202073706563756C61725465726D5F3233203D202828746D707661725F3237202F20280A20202020286D61782028302E33322C20636C616D702028646F7420285F576F726C6453706163654C69676874506F73302E78797A2C20746D707661725F3234292C20302E302C20312E302929202A2028312E35202B20746D707661725F323729290A2020202A200A20202020282828746D707661725F3236202A20746D707661725F323629202A202828746D707661725F3237202A20746D707661725F323729202D20312E302929202B20312E3030303031290A20202929202D20302E30303031293B0A20206D656469756D7020666C6F617420746D707661725F32383B0A2020746D707661725F3238203D20636C616D70202873706563756C61725465726D5F32332C20302E302C203130302E30293B0A202073706563756C61725465726D5F3233203D20746D707661725F32383B0A20206D656469756D7020666C6F617420785F32393B0A2020785F3239203D2028312E30202D20636C616D702028646F742028746D707661725F372C20766965774469725F3232292C20302E302C20312E3029293B0A20206D656469756D70207665633320746D707661725F33303B0A2020746D707661725F3330203D202828280A202020202828746D707661725F34202B2028746D707661725F3238202A20746D707661725F352929202A20746D707661725F39290A2020202A200A20202020636C616D702028646F742028746D707661725F372C205F576F726C6453706163654C69676874506F73302E78797A292C20302E302C20312E30290A202029202B20280A20202020286D617820282828312E303535202A200A202020202020706F7720286D617820287665633328302E302C20302E302C20302E30292C2028786C765F544558434F4F5244352E78797A202B20785F313529292C207665633328302E343136363636372C20302E343136363636372C20302E3431363636363729290A2020202029202D20302E303535292C207665633328302E302C20302E302C20302E302929202A206F63636C7573696F6E5F3132290A2020202A20746D707661725F342929202B2028280A2020202028312E30202D202828746D707661725F3237202A20746D707661725F313729202A20302E323829290A2020202A200A202020202828286864725F31382E78202A20280A202020202020286864725F31382E77202A2028746D707661725F32312E77202D20312E3029290A20202020202B20312E302929202A20746D707661725F32312E78797A29202A206F63636C7573696F6E5F3132290A202029202A206D69782028746D707661725F352C2076656333280A20202020636C616D702028285F476C6F7373696E657373202B2028312E30202D20746D707661725F3629292C20302E302C20312E30290A2020292C2076656333280A202020202828785F3239202A20785F323929202A2028785F3239202A20785F323929290A2020292929293B0A20206D656469756D70207665633420746D707661725F33313B0A2020746D707661725F33312E77203D20312E303B0A2020746D707661725F33312E78797A203D20746D707661725F33303B0A2020635F312E77203D20746D707661725F33312E773B0A2020635F312E78797A203D20746D707661725F33303B0A20206D656469756D70207665633420786C61745F7661726F75747075745F33323B0A2020786C61745F7661726F75747075745F33322E78797A203D20635F312E78797A3B0A2020786C61745F7661726F75747075745F33322E77203D20312E303B0A2020676C5F46726167446174615B305D203D20786C61745F7661726F75747075745F33323B0A7D0A0A0A23656E6469660A001B000000000000000100000000000000000000000000000000000000324F040C05000000000000000000000000000000000000000100000005000000504F494E540000000000000000000000000000000100000000000000000000000000000000000000324F040C050000001B00000002000000000000000000000000000000481300002376657273696F6E203130300A0A236966646566205645525445580A6174747269627574652076656334205F676C65735665727465783B0A6174747269627574652076656333205F676C65734E6F726D616C3B0A6174747269627574652076656334205F676C65734D756C7469546578436F6F7264303B0A6174747269627574652076656334205F676C65734D756C7469546578436F6F7264313B0A756E69666F726D2068696768702076656333205F576F726C64537061636543616D657261506F733B0A756E69666F726D206D656469756D70207665633420756E6974795F534842723B0A756E69666F726D206D656469756D70207665633420756E6974795F534842673B0A756E69666F726D206D656469756D70207665633420756E6974795F534842623B0A756E69666F726D206D656469756D70207665633420756E6974795F5348433B0A756E69666F726D206869676870206D61743420756E6974795F4F626A656374546F576F726C643B0A756E69666F726D206869676870206D61743420756E6974795F576F726C64546F4F626A6563743B0A756E69666F726D206869676870206D61743420756E6974795F4D617472697856503B0A756E69666F726D2068696768702076656334205F4D61696E5465785F53543B0A756E69666F726D2068696768702076656334205F44657461696C416C6265646F4D61705F53543B0A756E69666F726D206D656469756D7020666C6F6174205F55565365633B0A76617279696E67206869676870207665633420786C765F544558434F4F5244303B0A76617279696E67206D656469756D70207665633320786C765F544558434F4F5244313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244323B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244325F313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244325F323B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244353B0A76617279696E67206869676870207665633320786C765F544558434F4F5244363B0A766F6964206D61696E2028290A7B0A20206D656469756D70207665633320746D707661725F313B0A2020746D707661725F31203D205F676C65734E6F726D616C3B0A20206869676870207665633220746D707661725F323B0A2020746D707661725F32203D205F676C65734D756C7469546578436F6F7264302E78793B0A20206869676870207665633220746D707661725F333B0A2020746D707661725F33203D205F676C65734D756C7469546578436F6F7264312E78793B0A20206D656469756D70207665633420746D707661725F343B0A20206D656469756D70207665633420746D707661725F353B0A20206D656469756D70207665633420746D707661725F363B0A20206D656469756D70207665633420746D707661725F373B0A20206869676870207665633320746D707661725F383B0A20206869676870207665633420746D707661725F393B0A2020746D707661725F39203D2028756E6974795F4F626A656374546F576F726C64202A205F676C6573566572746578293B0A2020746D707661725F38203D20746D707661725F392E78797A3B0A20206869676870207665633420746D707661725F31303B0A20206869676870207665633420746D707661725F31313B0A2020746D707661725F31312E77203D20312E303B0A2020746D707661725F31312E78797A203D205F676C65735665727465782E78797A3B0A2020746D707661725F3130203D2028756E6974795F4D61747269785650202A2028756E6974795F4F626A656374546F576F726C64202A20746D707661725F313129293B0A20206869676870207665633420746578636F6F72645F31323B0A2020746578636F6F72645F31322E7879203D2028285F676C65734D756C7469546578436F6F7264302E7879202A205F4D61696E5465785F53542E787929202B205F4D61696E5465785F53542E7A77293B0A20206869676870207665633220746D707661725F31333B0A202069662028285F5556536563203D3D20302E302929207B0A20202020746D707661725F3133203D20746D707661725F323B0A20207D20656C7365207B0A20202020746D707661725F3133203D20746D707661725F333B0A20207D3B0A2020746578636F6F72645F31322E7A77203D202828746D707661725F3133202A205F44657461696C416C6265646F4D61705F53542E787929202B205F44657461696C416C6265646F4D61705F53542E7A77293B0A20206D656469756D70207665633320746D707661725F31343B0A202068696768702076656333206E5F31353B0A20206E5F3135203D2028746D707661725F392E78797A202D205F576F726C64537061636543616D657261506F73293B0A2020746D707661725F3134203D206E5F31353B0A202068696768702076656333206E6F726D5F31363B0A20206E6F726D5F3136203D20746D707661725F313B0A20206869676870206D61743320746D707661725F31373B0A2020746D707661725F31375B305D203D20756E6974795F576F726C64546F4F626A6563745B305D2E78797A3B0A2020746D707661725F31375B315D203D20756E6974795F576F726C64546F4F626A6563745B315D2E78797A3B0A2020746D707661725F31375B325D203D20756E6974795F576F726C64546F4F626A6563745B325D2E78797A3B0A20206869676870207665633320746D707661725F31383B0A2020746D707661725F3138203D206E6F726D616C697A6528286E6F726D5F3136202A20746D707661725F313729293B0A2020746D707661725F342E78797A203D207665633328302E302C20302E302C20302E30293B0A2020746D707661725F352E78797A203D207665633328302E302C20302E302C20302E30293B0A2020746D707661725F362E78797A203D20746D707661725F31383B0A2020746D707661725F372E77203D20302E303B0A20206D656469756D702076656333206E6F726D616C5F31393B0A20206E6F726D616C5F3139203D20746D707661725F31383B0A20206D656469756D7020766563332078315F32303B0A20206D656469756D70207665633420746D707661725F32313B0A2020746D707661725F3231203D20286E6F726D616C5F31392E78797A7A202A206E6F726D616C5F31392E797A7A78293B0A202078315F32302E78203D20646F742028756E6974795F534842722C20746D707661725F3231293B0A202078315F32302E79203D20646F742028756E6974795F534842672C20746D707661725F3231293B0A202078315F32302E7A203D20646F742028756E6974795F534842622C20746D707661725F3231293B0A2020746D707661725F372E78797A203D202878315F3230202B2028756E6974795F5348432E78797A202A20280A20202020286E6F726D616C5F31392E78202A206E6F726D616C5F31392E78290A2020202D200A20202020286E6F726D616C5F31392E79202A206E6F726D616C5F31392E79290A20202929293B0A2020676C5F506F736974696F6E203D20746D707661725F31303B0A2020786C765F544558434F4F524430203D20746578636F6F72645F31323B0A2020786C765F544558434F4F524431203D20746D707661725F31343B0A2020786C765F544558434F4F524432203D20746D707661725F343B0A2020786C765F544558434F4F5244325F31203D20746D707661725F353B0A2020786C765F544558434F4F5244325F32203D20746D707661725F363B0A2020786C765F544558434F4F524435203D20746D707661725F373B0A2020786C765F544558434F4F524436203D20746D707661725F383B0A7D0A0A0A23656E6469660A23696664656620465241474D454E540A23657874656E73696F6E20474C5F4558545F7368616465725F746578747572655F6C6F64203A20656E61626C650A23657874656E73696F6E20474C5F4558545F647261775F62756666657273203A20656E61626C650A756E69666F726D206D656469756D70207665633420756E6974795F534841723B0A756E69666F726D206D656469756D70207665633420756E6974795F534841673B0A756E69666F726D206D656469756D70207665633420756E6974795F534841623B0A756E69666F726D206D656469756D702076656334205F436F6C6F723B0A756E69666F726D2073616D706C65723244205F4D61696E5465783B0A756E69666F726D206D656469756D7020666C6F6174205F4D6574616C6C69633B0A756E69666F726D206D656469756D7020666C6F6174205F476C6F7373696E6573733B0A756E69666F726D2073616D706C65723244205F4F63636C7573696F6E4D61703B0A756E69666F726D206D656469756D7020666C6F6174205F4F63636C7573696F6E537472656E6774683B0A76617279696E67206869676870207665633420786C765F544558434F4F5244303B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244325F323B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244353B0A766F6964206D61696E2028290A7B0A20206C6F7770207665633420746D707661725F313B0A2020746D707661725F31203D2074657874757265324420285F4D61696E5465782C20786C765F544558434F4F5244302E7879293B0A20206D656469756D702076656332206D675F323B0A20206D675F322E78203D205F4D6574616C6C69633B0A20206D675F322E79203D205F476C6F7373696E6573733B0A20206D656469756D70207665633320746D707661725F333B0A2020746D707661725F33203D20285F436F6C6F722E78797A202A20746D707661725F312E78797A293B0A20206D656469756D70207665633320746D707661725F343B0A2020746D707661725F34203D2028746D707661725F33202A2028302E37373930383337202D20285F4D6574616C6C6963202A20302E373739303833372929293B0A20206D656469756D70207665633320746D707661725F353B0A2020746D707661725F35203D206E6F726D616C697A6528786C765F544558434F4F5244325F322E78797A293B0A20206D656469756D7020666C6F6174206F63635F363B0A20206C6F777020666C6F617420746D707661725F373B0A2020746D707661725F37203D2074657874757265324420285F4F63636C7573696F6E4D61702C20786C765F544558434F4F5244302E7879292E793B0A20206F63635F36203D20746D707661725F373B0A20206D656469756D7020666C6F617420746D707661725F383B0A2020746D707661725F38203D202828312E30202D205F4F63636C7573696F6E537472656E67746829202B20286F63635F36202A205F4F63636C7573696F6E537472656E67746829293B0A20206D656469756D70207665633320746D707661725F393B0A20206D656469756D70207665633420746D707661725F31303B0A2020746D707661725F31302E77203D20312E303B0A2020746D707661725F31302E78797A203D20746D707661725F353B0A20206D656469756D70207665633320785F31313B0A2020785F31312E78203D20646F742028756E6974795F534841722C20746D707661725F3130293B0A2020785F31312E79203D20646F742028756E6974795F534841672C20746D707661725F3130293B0A2020785F31312E7A203D20646F742028756E6974795F534841622C20746D707661725F3130293B0A2020746D707661725F39203D206D617820282828312E303535202A200A20202020706F7720286D617820287665633328302E302C20302E302C20302E30292C2028786C765F544558434F4F5244352E78797A202B20785F313129292C207665633328302E343136363636372C20302E343136363636372C20302E3431363636363729290A202029202D20302E303535292C207665633328302E302C20302E302C20302E3029293B0A2020746D707661725F39203D2028746D707661725F39202A20746D707661725F38293B0A20206D656469756D70207665633420746D707661725F31323B0A2020746D707661725F31322E78797A203D20746D707661725F343B0A2020746D707661725F31322E77203D20746D707661725F383B0A20206D656469756D70207665633420746D707661725F31333B0A2020746D707661725F31332E78797A203D206D697820287665633328302E323230393136332C20302E323230393136332C20302E32323039313633292C20746D707661725F332C2076656333285F4D6574616C6C696329293B0A2020746D707661725F31332E77203D206D675F322E793B0A20206D656469756D70207665633420746D707661725F31343B0A2020746D707661725F31342E77203D20312E303B0A2020746D707661725F31342E78797A203D202828746D707661725F35202A20302E3529202B20302E35293B0A20206D656469756D70207665633420746D707661725F31353B0A2020746D707661725F31352E77203D20312E303B0A2020746D707661725F31352E78797A203D2065787032282D2828746D707661725F39202A20746D707661725F342929293B0A2020676C5F46726167446174615B305D203D20746D707661725F31323B0A2020676C5F46726167446174615B315D203D20746D707661725F31333B0A2020676C5F46726167446174615B325D203D20746D707661725F31343B0A2020676C5F46726167446174615B335D203D20746D707661725F31353B0A7D0A0A0A23656E6469660A1B000000000000000100000000000000000000000000000000000000324F040C0500000000000000000000000000000000000000010000000D000000534841444F57535F44455054480000000000000000000000000000000100000000000000000000000000000000000000324F040C0500000066000000040000000000000000000000020000000B000000444952454354494F4E414C000E000000534841444F57535F53435245454E0000F92200002376657273696F6E203130300A0A236966646566205645525445580A6174747269627574652076656334205F676C65735665727465783B0A6174747269627574652076656333205F676C65734E6F726D616C3B0A6174747269627574652076656334205F676C65734D756C7469546578436F6F7264303B0A6174747269627574652076656334205F676C65734D756C7469546578436F6F7264313B0A756E69666F726D2068696768702076656333205F576F726C64537061636543616D657261506F733B0A756E69666F726D206D656469756D70207665633420756E6974795F534842723B0A756E69666F726D206D656469756D70207665633420756E6974795F534842673B0A756E69666F726D206D656469756D70207665633420756E6974795F534842623B0A756E69666F726D206D656469756D70207665633420756E6974795F5348433B0A756E69666F726D206869676870206D61743420756E6974795F4F626A656374546F576F726C643B0A756E69666F726D206869676870206D61743420756E6974795F576F726C64546F4F626A6563743B0A756E69666F726D206869676870206D61743420756E6974795F4D617472697856503B0A756E69666F726D2068696768702076656334205F4D61696E5465785F53543B0A756E69666F726D2068696768702076656334205F44657461696C416C6265646F4D61705F53543B0A756E69666F726D206D656469756D7020666C6F6174205F55565365633B0A76617279696E67206869676870207665633420786C765F544558434F4F5244303B0A76617279696E67206D656469756D70207665633320786C765F544558434F4F5244313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244323B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244325F313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244325F323B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244353B0A76617279696E67206869676870207665633220786C765F544558434F4F5244363B0A76617279696E67206869676870207665633320786C765F544558434F4F5244383B0A766F6964206D61696E2028290A7B0A20206D656469756D70207665633320746D707661725F313B0A2020746D707661725F31203D205F676C65734E6F726D616C3B0A20206869676870207665633220746D707661725F323B0A2020746D707661725F32203D205F676C65734D756C7469546578436F6F7264302E78793B0A20206869676870207665633220746D707661725F333B0A2020746D707661725F33203D205F676C65734D756C7469546578436F6F7264312E78793B0A20206D656469756D70207665633420746D707661725F343B0A20206D656469756D70207665633420746D707661725F353B0A20206D656469756D70207665633420746D707661725F363B0A20206869676870207665633220746D707661725F373B0A20206869676870207665633320746D707661725F383B0A20206869676870207665633420746D707661725F393B0A2020746D707661725F39203D2028756E6974795F4F626A656374546F576F726C64202A205F676C6573566572746578293B0A2020746D707661725F38203D20746D707661725F392E78797A3B0A20206869676870207665633420746D707661725F31303B0A20206869676870207665633420746D707661725F31313B0A2020746D707661725F31312E77203D20312E303B0A2020746D707661725F31312E78797A203D205F676C65735665727465782E78797A3B0A2020746D707661725F3130203D2028756E6974795F4D61747269785650202A2028756E6974795F4F626A656374546F576F726C64202A20746D707661725F313129293B0A20206869676870207665633420746578636F6F72645F31323B0A2020746578636F6F72645F31322E7879203D2028285F676C65734D756C7469546578436F6F7264302E7879202A205F4D61696E5465785F53542E787929202B205F4D61696E5465785F53542E7A77293B0A20206869676870207665633220746D707661725F31333B0A202069662028285F5556536563203D3D20302E302929207B0A20202020746D707661725F3133203D20746D707661725F323B0A20207D20656C7365207B0A20202020746D707661725F3133203D20746D707661725F333B0A20207D3B0A2020746578636F6F72645F31322E7A77203D202828746D707661725F3133202A205F44657461696C416C6265646F4D61705F53542E787929202B205F44657461696C416C6265646F4D61705F53542E7A77293B0A20206D656469756D70207665633320746D707661725F31343B0A202068696768702076656333206E5F31353B0A20206E5F3135203D2028746D707661725F392E78797A202D205F576F726C64537061636543616D657261506F73293B0A2020746D707661725F3134203D206E5F31353B0A202068696768702076656333206E6F726D5F31363B0A20206E6F726D5F3136203D20746D707661725F313B0A20206869676870206D61743320746D707661725F31373B0A2020746D707661725F31375B305D203D20756E6974795F576F726C64546F4F626A6563745B305D2E78797A3B0A2020746D707661725F31375B315D203D20756E6974795F576F726C64546F4F626A6563745B315D2E78797A3B0A2020746D707661725F31375B325D203D20756E6974795F576F726C64546F4F626A6563745B325D2E78797A3B0A20206869676870207665633320746D707661725F31383B0A2020746D707661725F3138203D206E6F726D616C697A6528286E6F726D5F3136202A20746D707661725F313729293B0A2020746D707661725F342E78797A203D207665633328302E302C20302E302C20302E30293B0A2020746D707661725F352E78797A203D207665633328302E302C20302E302C20302E30293B0A2020746D707661725F362E78797A203D20746D707661725F31383B0A20206D656469756D702076656333206E6F726D616C576F726C645F31393B0A20206E6F726D616C576F726C645F3139203D20746D707661725F31383B0A20206D656469756D70207665633420616D6269656E744F724C696768746D617055565F32303B0A2020616D6269656E744F724C696768746D617055565F32302E77203D20302E303B0A20206D656469756D7020766563332078315F32313B0A20206D656469756D70207665633420746D707661725F32323B0A2020746D707661725F3232203D20286E6F726D616C576F726C645F31392E78797A7A202A206E6F726D616C576F726C645F31392E797A7A78293B0A202078315F32312E78203D20646F742028756E6974795F534842722C20746D707661725F3232293B0A202078315F32312E79203D20646F742028756E6974795F534842672C20746D707661725F3232293B0A202078315F32312E7A203D20646F742028756E6974795F534842622C20746D707661725F3232293B0A2020616D6269656E744F724C696768746D617055565F32302E78797A203D202878315F3231202B2028756E6974795F5348432E78797A202A20280A20202020286E6F726D616C576F726C645F31392E78202A206E6F726D616C576F726C645F31392E78290A2020202D200A20202020286E6F726D616C576F726C645F31392E79202A206E6F726D616C576F726C645F31392E79290A20202929293B0A2020676C5F506F736974696F6E203D20746D707661725F31303B0A2020786C765F544558434F4F524430203D20746578636F6F72645F31323B0A2020786C765F544558434F4F524431203D20746D707661725F31343B0A2020786C765F544558434F4F524432203D20746D707661725F343B0A2020786C765F544558434F4F5244325F31203D20746D707661725F353B0A2020786C765F544558434F4F5244325F32203D20746D707661725F363B0A2020786C765F544558434F4F524435203D20616D6269656E744F724C696768746D617055565F32303B0A2020786C765F544558434F4F524436203D20746D707661725F373B0A2020786C765F544558434F4F524438203D20746D707661725F383B0A7D0A0A0A23656E6469660A23696664656620465241474D454E540A23657874656E73696F6E20474C5F4558545F7368616465725F746578747572655F6C6F64203A20656E61626C650A6C6F7770207665633420696D706C5F6C6F775F74657874757265437562654C6F64455854286C6F77702073616D706C6572437562652073616D706C65722C206869676870207665633320636F6F72642C206D656469756D7020666C6F6174206C6F64290A7B0A23696620646566696E656428474C5F4558545F7368616465725F746578747572655F6C6F64290A0972657475726E2074657874757265437562654C6F644558542873616D706C65722C20636F6F72642C206C6F64293B0A23656C73650A0972657475726E2074657874757265437562652873616D706C65722C20636F6F72642C206C6F64293B0A23656E6469660A7D0A0A756E69666F726D2068696768702076656333205F576F726C64537061636543616D657261506F733B0A756E69666F726D206D656469756D702076656334205F576F726C6453706163654C69676874506F73303B0A756E69666F726D206D656469756D70207665633420756E6974795F534841723B0A756E69666F726D206D656469756D70207665633420756E6974795F534841673B0A756E69666F726D206D656469756D70207665633420756E6974795F534841623B0A756E69666F726D206869676870206D61743420756E6974795F576F726C64546F536861646F775B345D3B0A756E69666F726D206D656469756D702076656334205F4C69676874536861646F77446174613B0A756E69666F726D206869676870207665633420756E6974795F536861646F774661646543656E746572416E64547970653B0A756E69666F726D206869676870206D61743420756E6974795F4D6174726978563B0A756E69666F726D206C6F77702073616D706C65724375626520756E6974795F5370656343756265303B0A756E69666F726D206D656469756D70207665633420756E6974795F5370656343756265305F4844523B0A756E69666F726D206C6F77702076656334205F4C69676874436F6C6F72303B0A756E69666F726D206D656469756D702076656334205F436F6C6F723B0A756E69666F726D2073616D706C65723244205F4D61696E5465783B0A756E69666F726D206D656469756D7020666C6F6174205F4D6574616C6C69633B0A756E69666F726D206D656469756D7020666C6F6174205F476C6F7373696E6573733B0A756E69666F726D2073616D706C65723244205F4F63636C7573696F6E4D61703B0A756E69666F726D206D656469756D7020666C6F6174205F4F63636C7573696F6E537472656E6774683B0A756E69666F726D2068696768702073616D706C65723244205F536861646F774D6170546578747572653B0A76617279696E67206869676870207665633420786C765F544558434F4F5244303B0A76617279696E67206D656469756D70207665633320786C765F544558434F4F5244313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244325F323B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244353B0A76617279696E67206869676870207665633320786C765F544558434F4F5244383B0A766F6964206D61696E2028290A7B0A20206D656469756D70207665633420635F313B0A20206C6F777020666C6F617420617474656E5F323B0A20206C6F7770207665633420746D707661725F333B0A2020746D707661725F33203D2074657874757265324420285F4D61696E5465782C20786C765F544558434F4F5244302E7879293B0A20206D656469756D70207665633320746D707661725F343B0A2020746D707661725F34203D20285F436F6C6F722E78797A202A20746D707661725F332E78797A293B0A20206D656469756D70207665633320746D707661725F353B0A20206D656469756D70207665633320746D707661725F363B0A2020746D707661725F36203D206D697820287665633328302E323230393136332C20302E323230393136332C20302E32323039313633292C20746D707661725F342C2076656333285F4D6574616C6C696329293B0A20206D656469756D7020666C6F617420746D707661725F373B0A2020746D707661725F37203D2028302E37373930383337202D20285F4D6574616C6C6963202A20302E3737393038333729293B0A2020746D707661725F35203D2028746D707661725F34202A20746D707661725F37293B0A20206D656469756D70207665633320746D707661725F383B0A2020746D707661725F38203D206E6F726D616C697A6528786C765F544558434F4F5244325F322E78797A293B0A20206D656469756D70207665633320746D707661725F393B0A2020746D707661725F39203D206E6F726D616C697A6528786C765F544558434F4F524431293B0A20206D656469756D70207665633320746D707661725F31303B0A2020746D707661725F3130203D205F4C69676874436F6C6F72302E78797A3B0A20206D656469756D7020666C6F6174207265616C74696D65536861646F77417474656E756174696F6E5F31313B0A20206869676870207665633420765F31323B0A2020765F31322E78203D20756E6974795F4D6174726978565B305D2E7A3B0A2020765F31322E79203D20756E6974795F4D6174726978565B315D2E7A3B0A2020765F31322E7A203D20756E6974795F4D6174726978565B325D2E7A3B0A2020765F31322E77203D20756E6974795F4D6174726978565B335D2E7A3B0A20206869676870207665633320746D707661725F31333B0A2020746D707661725F3133203D2028786C765F544558434F4F524438202D20756E6974795F536861646F774661646543656E746572416E64547970652E78797A293B0A20206D656469756D7020666C6F617420746D707661725F31343B0A2020686967687020666C6F617420746D707661725F31353B0A2020746D707661725F3135203D20636C616D70202828280A202020206D69782028646F742028285F576F726C64537061636543616D657261506F73202D20786C765F544558434F4F524438292C20765F31322E78797A292C207371727428646F742028746D707661725F31332C20746D707661725F313329292C20756E6974795F536861646F774661646543656E746572416E64547970652E77290A2020202A205F4C69676874536861646F77446174612E7A29202B205F4C69676874536861646F77446174612E77292C20302E302C20312E30293B0A2020746D707661725F3134203D20746D707661725F31353B0A20206869676870207665633420746D707661725F31363B0A2020746D707661725F31362E77203D20312E303B0A2020746D707661725F31362E78797A203D20786C765F544558434F4F5244383B0A20206C6F777020666C6F617420746D707661725F31373B0A20206869676870207665633420736861646F77436F6F72645F31383B0A2020736861646F77436F6F72645F3138203D2028756E6974795F576F726C64546F536861646F775B305D202A20746D707661725F3136293B0A2020686967687020666C6F6174206C69676874536861646F7744617461585F31393B0A20206D656469756D7020666C6F617420746D707661725F32303B0A2020746D707661725F3230203D205F4C69676874536861646F77446174612E783B0A20206C69676874536861646F7744617461585F3139203D20746D707661725F32303B0A2020686967687020666C6F617420746D707661725F32313B0A2020746D707661725F3231203D206D61782028666C6F6174282874657874757265324420285F536861646F774D6170546578747572652C20736861646F77436F6F72645F31382E7879292E78203E20736861646F77436F6F72645F31382E7A29292C206C69676874536861646F7744617461585F3139293B0A2020746D707661725F3137203D20746D707661725F32313B0A20207265616C74696D65536861646F77417474656E756174696F6E5F3131203D20746D707661725F31373B0A20206D656469756D7020666C6F617420746D707661725F32323B0A2020746D707661725F3232203D20636C616D702028287265616C74696D65536861646F77417474656E756174696F6E5F3131202B20746D707661725F3134292C20302E302C20312E30293B0A2020617474656E5F32203D20746D707661725F32323B0A20206D656469756D7020666C6F6174206F63635F32333B0A20206C6F777020666C6F617420746D707661725F32343B0A2020746D707661725F3234203D2074657874757265324420285F4F63636C7573696F6E4D61702C20786C765F544558434F4F5244302E7879292E793B0A20206F63635F3233203D20746D707661725F32343B0A20206D656469756D7020666C6F617420617474656E5F32353B0A2020617474656E5F3235203D20617474656E5F323B0A20206D656469756D7020666C6F6174206F63636C7573696F6E5F32363B0A20206F63636C7573696F6E5F3236203D202828312E30202D205F4F63636C7573696F6E537472656E67746829202B20286F63635F3233202A205F4F63636C7573696F6E537472656E67746829293B0A20206869676870207665633420746D707661725F32373B0A2020746D707661725F3237203D20756E6974795F5370656343756265305F4844523B0A20206D656469756D70207665633420746D707661725F32383B0A2020746D707661725F32382E77203D20312E303B0A2020746D707661725F32382E78797A203D20746D707661725F383B0A20206D656469756D70207665633320785F32393B0A2020785F32392E78203D20646F742028756E6974795F534841722C20746D707661725F3238293B0A2020785F32392E79203D20646F742028756E6974795F534841672C20746D707661725F3238293B0A2020785F32392E7A203D20646F742028756E6974795F534841622C20746D707661725F3238293B0A20206D656469756D70207665633320746D707661725F33303B0A20206D656469756D7020666C6F617420746D707661725F33313B0A2020746D707661725F3331203D2028312E30202D205F476C6F7373696E657373293B0A2020746D707661725F3330203D2028746D707661725F39202D2028322E30202A20280A20202020646F742028746D707661725F382C20746D707661725F39290A2020202A20746D707661725F382929293B0A20206D656469756D702076656334206864725F33323B0A20206864725F3332203D20746D707661725F32373B0A20206D656469756D70207665633420746D707661725F33333B0A2020746D707661725F33332E78797A203D20746D707661725F33303B0A2020746D707661725F33332E77203D202828746D707661725F3331202A2028312E37202D200A2020202028302E37202A20746D707661725F3331290A20202929202A20362E30293B0A20206C6F7770207665633420746D707661725F33343B0A2020746D707661725F3334203D20696D706C5F6C6F775F74657874757265437562654C6F644558542028756E6974795F5370656343756265302C20746D707661725F33302C20746D707661725F33332E77293B0A20206D656469756D70207665633420746D707661725F33353B0A2020746D707661725F3335203D20746D707661725F33343B0A20206D656469756D70207665633320766965774469725F33363B0A2020766965774469725F3336203D202D28746D707661725F39293B0A20206D656469756D7020666C6F61742073706563756C61725465726D5F33373B0A20206D656469756D70207665633320746D707661725F33383B0A20206D656469756D70207665633320696E5665635F33393B0A2020696E5665635F3339203D20285F576F726C6453706163654C69676874506F73302E78797A202B20766965774469725F3336293B0A2020746D707661725F3338203D2028696E5665635F3339202A20696E766572736573717274286D61782028302E3030312C200A20202020646F742028696E5665635F33392C20696E5665635F3339290A20202929293B0A20206D656469756D7020666C6F617420746D707661725F34303B0A2020746D707661725F3430203D20636C616D702028646F742028746D707661725F382C20746D707661725F3338292C20302E302C20312E30293B0A20206D656469756D7020666C6F617420746D707661725F34313B0A2020746D707661725F3431203D2028746D707661725F3331202A20746D707661725F3331293B0A202073706563756C61725465726D5F3337203D202828746D707661725F3431202F20280A20202020286D61782028302E33322C20636C616D702028646F7420285F576F726C6453706163654C69676874506F73302E78797A2C20746D707661725F3338292C20302E302C20312E302929202A2028312E35202B20746D707661725F343129290A2020202A200A20202020282828746D707661725F3430202A20746D707661725F343029202A202828746D707661725F3431202A20746D707661725F343129202D20312E302929202B20312E3030303031290A20202929202D20302E30303031293B0A20206D656469756D7020666C6F617420746D707661725F34323B0A2020746D707661725F3432203D20636C616D70202873706563756C61725465726D5F33372C20302E302C203130302E30293B0A202073706563756C61725465726D5F3337203D20746D707661725F34323B0A20206D656469756D7020666C6F617420785F34333B0A2020785F3433203D2028312E30202D20636C616D702028646F742028746D707661725F382C20766965774469725F3336292C20302E302C20312E3029293B0A20206D656469756D70207665633320746D707661725F34343B0A2020746D707661725F3434203D202828280A202020202828746D707661725F35202B2028746D707661725F3432202A20746D707661725F362929202A2028746D707661725F3130202A20617474656E5F323529290A2020202A200A20202020636C616D702028646F742028746D707661725F382C205F576F726C6453706163654C69676874506F73302E78797A292C20302E302C20312E30290A202029202B20280A20202020286D617820282828312E303535202A200A202020202020706F7720286D617820287665633328302E302C20302E302C20302E30292C2028786C765F544558434F4F5244352E78797A202B20785F323929292C207665633328302E343136363636372C20302E343136363636372C20302E3431363636363729290A2020202029202D20302E303535292C207665633328302E302C20302E302C20302E302929202A206F63636C7573696F6E5F3236290A2020202A20746D707661725F352929202B2028280A2020202028312E30202D202828746D707661725F3431202A20746D707661725F333129202A20302E323829290A2020202A200A202020202828286864725F33322E78202A20280A202020202020286864725F33322E77202A2028746D707661725F33352E77202D20312E3029290A20202020202B20312E302929202A20746D707661725F33352E78797A29202A206F63636C7573696F6E5F3236290A202029202A206D69782028746D707661725F362C2076656333280A20202020636C616D702028285F476C6F7373696E657373202B2028312E30202D20746D707661725F3729292C20302E302C20312E30290A2020292C2076656333280A202020202828785F3433202A20785F343329202A2028785F3433202A20785F343329290A2020292929293B0A20206D656469756D70207665633420746D707661725F34353B0A2020746D707661725F34352E77203D20312E303B0A2020746D707661725F34352E78797A203D20746D707661725F34343B0A2020635F312E77203D20746D707661725F34352E773B0A2020635F312E78797A203D20746D707661725F34343B0A20206D656469756D70207665633420786C61745F7661726F75747075745F34363B0A2020786C61745F7661726F75747075745F34362E78797A203D20635F312E78797A3B0A2020786C61745F7661726F75747075745F34362E77203D20312E303B0A2020676C5F46726167446174615B305D203D20786C61745F7661726F75747075745F34363B0A7D0A0A0A23656E6469660A0000001B000000000000000100000000000000000000000000000000000000324F040C05000000000000000000000000000000000000000300000005000000504F494E540000000C000000534841444F57535F435542450C000000534841444F57535F534F46540000000000000000000000000100000000000000000000000000000000000000324F040C0500000037000000020000000000000000000000020000000B000000444952454354494F4E414C000E000000534841444F57535F53435245454E00009E1700002376657273696F6E203130300A0A236966646566205645525445580A6174747269627574652076656334205F676C65735665727465783B0A6174747269627574652076656333205F676C65734E6F726D616C3B0A6174747269627574652076656334205F676C65734D756C7469546578436F6F7264303B0A6174747269627574652076656334205F676C65734D756C7469546578436F6F7264313B0A756E69666F726D2068696768702076656333205F576F726C64537061636543616D657261506F733B0A756E69666F726D206D656469756D702076656334205F576F726C6453706163654C69676874506F73303B0A756E69666F726D206869676870206D61743420756E6974795F4F626A656374546F576F726C643B0A756E69666F726D206869676870206D61743420756E6974795F576F726C64546F4F626A6563743B0A756E69666F726D206869676870206D61743420756E6974795F4D617472697856503B0A756E69666F726D2068696768702076656334205F4D61696E5465785F53543B0A756E69666F726D2068696768702076656334205F44657461696C416C6265646F4D61705F53543B0A756E69666F726D206D656469756D7020666C6F6174205F55565365633B0A76617279696E67206869676870207665633420786C765F544558434F4F5244303B0A76617279696E67206D656469756D70207665633320786C765F544558434F4F5244313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244323B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244325F313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244325F323B0A76617279696E67206869676870207665633320786C765F544558434F4F5244353B0A76617279696E67206869676870207665633220786C765F544558434F4F5244363B0A766F6964206D61696E2028290A7B0A20206D656469756D70207665633320746D707661725F313B0A2020746D707661725F31203D205F676C65734E6F726D616C3B0A20206869676870207665633220746D707661725F323B0A2020746D707661725F32203D205F676C65734D756C7469546578436F6F7264302E78793B0A20206869676870207665633220746D707661725F333B0A2020746D707661725F33203D205F676C65734D756C7469546578436F6F7264312E78793B0A20206D656469756D70207665633420746D707661725F343B0A20206D656469756D70207665633420746D707661725F353B0A20206D656469756D70207665633420746D707661725F363B0A20206869676870207665633220746D707661725F373B0A20206869676870207665633420746D707661725F383B0A2020746D707661725F38203D2028756E6974795F4F626A656374546F576F726C64202A205F676C6573566572746578293B0A20206869676870207665633420746D707661725F393B0A20206869676870207665633420746D707661725F31303B0A2020746D707661725F31302E77203D20312E303B0A2020746D707661725F31302E78797A203D205F676C65735665727465782E78797A3B0A2020746D707661725F39203D2028756E6974795F4D61747269785650202A2028756E6974795F4F626A656374546F576F726C64202A20746D707661725F313029293B0A20206869676870207665633420746578636F6F72645F31313B0A2020746578636F6F72645F31312E7879203D2028285F676C65734D756C7469546578436F6F7264302E7879202A205F4D61696E5465785F53542E787929202B205F4D61696E5465785F53542E7A77293B0A20206869676870207665633220746D707661725F31323B0A202069662028285F5556536563203D3D20302E302929207B0A20202020746D707661725F3132203D20746D707661725F323B0A20207D20656C7365207B0A20202020746D707661725F3132203D20746D707661725F333B0A20207D3B0A2020746578636F6F72645F31312E7A77203D202828746D707661725F3132202A205F44657461696C416C6265646F4D61705F53542E787929202B205F44657461696C416C6265646F4D61705F53542E7A77293B0A20206D656469756D70207665633320746D707661725F31333B0A202068696768702076656333206E5F31343B0A20206E5F3134203D2028746D707661725F382E78797A202D205F576F726C64537061636543616D657261506F73293B0A2020746D707661725F3133203D206E5F31343B0A202068696768702076656333206E6F726D5F31353B0A20206E6F726D5F3135203D20746D707661725F313B0A20206869676870206D61743320746D707661725F31363B0A2020746D707661725F31365B305D203D20756E6974795F576F726C64546F4F626A6563745B305D2E78797A3B0A2020746D707661725F31365B315D203D20756E6974795F576F726C64546F4F626A6563745B315D2E78797A3B0A2020746D707661725F31365B325D203D20756E6974795F576F726C64546F4F626A6563745B325D2E78797A3B0A20206869676870207665633320746D707661725F31373B0A2020746D707661725F3137203D206E6F726D616C697A6528286E6F726D5F3135202A20746D707661725F313629293B0A2020746D707661725F342E78797A203D207665633328302E302C20302E302C20302E30293B0A2020746D707661725F352E78797A203D207665633328302E302C20302E302C20302E30293B0A2020746D707661725F362E78797A203D20746D707661725F31373B0A20206869676870207665633320746D707661725F31383B0A2020746D707661725F3138203D20285F576F726C6453706163654C69676874506F73302E78797A202D2028746D707661725F382E78797A202A205F576F726C6453706163654C69676874506F73302E7729293B0A2020746D707661725F342E77203D20746D707661725F31382E783B0A2020746D707661725F352E77203D20746D707661725F31382E793B0A2020746D707661725F362E77203D20746D707661725F31382E7A3B0A2020676C5F506F736974696F6E203D20746D707661725F393B0A2020786C765F544558434F4F524430203D20746578636F6F72645F31313B0A2020786C765F544558434F4F524431203D20746D707661725F31333B0A2020786C765F544558434F4F524432203D20746D707661725F343B0A2020786C765F544558434F4F5244325F31203D20746D707661725F353B0A2020786C765F544558434F4F5244325F32203D20746D707661725F363B0A2020786C765F544558434F4F524435203D20746D707661725F382E78797A3B0A2020786C765F544558434F4F524436203D20746D707661725F373B0A7D0A0A0A23656E6469660A23696664656620465241474D454E540A756E69666F726D2068696768702076656333205F576F726C64537061636543616D657261506F733B0A756E69666F726D206869676870206D61743420756E6974795F576F726C64546F536861646F775B345D3B0A756E69666F726D206D656469756D702076656334205F4C69676874536861646F77446174613B0A756E69666F726D206869676870207665633420756E6974795F536861646F774661646543656E746572416E64547970653B0A756E69666F726D206869676870206D61743420756E6974795F4D6174726978563B0A756E69666F726D206C6F77702076656334205F4C69676874436F6C6F72303B0A756E69666F726D206D656469756D702076656334205F436F6C6F723B0A756E69666F726D2073616D706C65723244205F4D61696E5465783B0A756E69666F726D206D656469756D7020666C6F6174205F4D6574616C6C69633B0A756E69666F726D206D656469756D7020666C6F6174205F476C6F7373696E6573733B0A756E69666F726D2068696768702073616D706C65723244205F536861646F774D6170546578747572653B0A76617279696E67206869676870207665633420786C765F544558434F4F5244303B0A76617279696E67206D656469756D70207665633320786C765F544558434F4F5244313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244323B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244325F313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244325F323B0A76617279696E67206869676870207665633320786C765F544558434F4F5244353B0A766F6964206D61696E2028290A7B0A20206C6F777020666C6F617420617474656E5F313B0A20206C6F7770207665633420746D707661725F323B0A2020746D707661725F32203D2074657874757265324420285F4D61696E5465782C20786C765F544558434F4F5244302E7879293B0A20206D656469756D70207665633320746D707661725F333B0A2020746D707661725F33203D20285F436F6C6F722E78797A202A20746D707661725F322E78797A293B0A20206D656469756D70207665633320746D707661725F343B0A2020746D707661725F34203D206E6F726D616C697A6528786C765F544558434F4F5244325F322E78797A293B0A20206D656469756D7020666C6F6174207265616C74696D65536861646F77417474656E756174696F6E5F353B0A20206869676870207665633420765F363B0A2020765F362E78203D20756E6974795F4D6174726978565B305D2E7A3B0A2020765F362E79203D20756E6974795F4D6174726978565B315D2E7A3B0A2020765F362E7A203D20756E6974795F4D6174726978565B325D2E7A3B0A2020765F362E77203D20756E6974795F4D6174726978565B335D2E7A3B0A20206869676870207665633320746D707661725F373B0A2020746D707661725F37203D2028786C765F544558434F4F524435202D20756E6974795F536861646F774661646543656E746572416E64547970652E78797A293B0A20206D656469756D7020666C6F617420746D707661725F383B0A2020686967687020666C6F617420746D707661725F393B0A2020746D707661725F39203D20636C616D70202828280A202020206D69782028646F742028285F576F726C64537061636543616D657261506F73202D20786C765F544558434F4F524435292C20765F362E78797A292C207371727428646F742028746D707661725F372C20746D707661725F3729292C20756E6974795F536861646F774661646543656E746572416E64547970652E77290A2020202A205F4C69676874536861646F77446174612E7A29202B205F4C69676874536861646F77446174612E77292C20302E302C20312E30293B0A2020746D707661725F38203D20746D707661725F393B0A20206869676870207665633420746D707661725F31303B0A2020746D707661725F31302E77203D20312E303B0A2020746D707661725F31302E78797A203D20786C765F544558434F4F5244353B0A20206C6F777020666C6F617420746D707661725F31313B0A20206869676870207665633420736861646F77436F6F72645F31323B0A2020736861646F77436F6F72645F3132203D2028756E6974795F576F726C64546F536861646F775B305D202A20746D707661725F3130293B0A2020686967687020666C6F6174206C69676874536861646F7744617461585F31333B0A20206D656469756D7020666C6F617420746D707661725F31343B0A2020746D707661725F3134203D205F4C69676874536861646F77446174612E783B0A20206C69676874536861646F7744617461585F3133203D20746D707661725F31343B0A2020686967687020666C6F617420746D707661725F31353B0A2020746D707661725F3135203D206D61782028666C6F6174282874657874757265324420285F536861646F774D6170546578747572652C20736861646F77436F6F72645F31322E7879292E78203E20736861646F77436F6F72645F31322E7A29292C206C69676874536861646F7744617461585F3133293B0A2020746D707661725F3131203D20746D707661725F31353B0A20207265616C74696D65536861646F77417474656E756174696F6E5F35203D20746D707661725F31313B0A20206D656469756D7020666C6F617420746D707661725F31363B0A2020746D707661725F3136203D20636C616D702028287265616C74696D65536861646F77417474656E756174696F6E5F35202B20746D707661725F38292C20302E302C20312E30293B0A2020617474656E5F31203D20746D707661725F31363B0A20206D656469756D70207665633320746D707661725F31373B0A2020746D707661725F31372E78203D20786C765F544558434F4F5244322E773B0A2020746D707661725F31372E79203D20786C765F544558434F4F5244325F312E773B0A2020746D707661725F31372E7A203D20786C765F544558434F4F5244325F322E773B0A20206D656469756D7020666C6F617420617474656E5F31383B0A2020617474656E5F3138203D20617474656E5F313B0A20206D656469756D70207665633320746D707661725F31393B0A2020746D707661725F3139203D205F4C69676874436F6C6F72302E78797A3B0A2020746D707661725F3139203D2028746D707661725F3139202A20617474656E5F3138293B0A20206D656469756D7020666C6F61742073706563756C61725465726D5F32303B0A20206D656469756D70207665633320746D707661725F32313B0A20206D656469756D70207665633320696E5665635F32323B0A2020696E5665635F3232203D2028746D707661725F3137202D206E6F726D616C697A6528786C765F544558434F4F52443129293B0A2020746D707661725F3231203D2028696E5665635F3232202A20696E766572736573717274286D61782028302E3030312C200A20202020646F742028696E5665635F32322C20696E5665635F3232290A20202929293B0A20206D656469756D7020666C6F617420746D707661725F32333B0A2020746D707661725F3233203D20636C616D702028646F742028746D707661725F342C20746D707661725F3231292C20302E302C20312E30293B0A20206D656469756D7020666C6F617420746D707661725F32343B0A2020746D707661725F3234203D2028312E30202D205F476C6F7373696E657373293B0A20206D656469756D7020666C6F617420746D707661725F32353B0A2020746D707661725F3235203D2028746D707661725F3234202A20746D707661725F3234293B0A202073706563756C61725465726D5F3230203D202828746D707661725F3235202F20280A20202020286D61782028302E33322C20636C616D702028646F742028746D707661725F31372C20746D707661725F3231292C20302E302C20312E302929202A2028312E35202B20746D707661725F323529290A2020202A200A20202020282828746D707661725F3233202A20746D707661725F323329202A202828746D707661725F3235202A20746D707661725F323529202D20312E302929202B20312E3030303031290A20202929202D20302E30303031293B0A20206D656469756D7020666C6F617420746D707661725F32363B0A2020746D707661725F3236203D20636C616D70202873706563756C61725465726D5F32302C20302E302C203130302E30293B0A202073706563756C61725465726D5F3230203D20746D707661725F32363B0A20206D656469756D70207665633420746D707661725F32373B0A2020746D707661725F32372E77203D20312E303B0A2020746D707661725F32372E78797A203D202828280A2020202028746D707661725F33202A2028302E37373930383337202D20285F4D6574616C6C6963202A20302E373739303833372929290A2020202B200A2020202028746D707661725F3236202A206D697820287665633328302E323230393136332C20302E323230393136332C20302E32323039313633292C20746D707661725F332C2076656333285F4D6574616C6C69632929290A202029202A20746D707661725F313929202A20636C616D702028646F742028746D707661725F342C20746D707661725F3137292C20302E302C20312E3029293B0A20206D656469756D70207665633420786C61745F7661726F75747075745F32383B0A2020786C61745F7661726F75747075745F32382E78797A203D20746D707661725F32372E78797A3B0A2020786C61745F7661726F75747075745F32382E77203D20312E303B0A2020676C5F46726167446174615B305D203D20786C61745F7661726F75747075745F32383B0A7D0A0A0A23656E6469660A00001B000000000000000100000000000000000000000000000000000000324F040C0500000001000000000000000000000000000000010000000D000000534841444F57535F4445505448000000780500002376657273696F6E203130300A0A236966646566205645525445580A6174747269627574652076656334205F676C65735665727465783B0A6174747269627574652076656333205F676C65734E6F726D616C3B0A756E69666F726D2068696768702076656334205F576F726C6453706163654C69676874506F73303B0A756E69666F726D206869676870207665633420756E6974795F4C69676874536861646F77426961733B0A756E69666F726D206869676870206D61743420756E6974795F4F626A656374546F576F726C643B0A756E69666F726D206869676870206D61743420756E6974795F576F726C64546F4F626A6563743B0A756E69666F726D206869676870206D61743420756E6974795F4D617472697856503B0A766F6964206D61696E2028290A7B0A20206869676870207665633420746D707661725F313B0A2020686967687020766563342077506F735F323B0A20206869676870207665633420746D707661725F333B0A2020746D707661725F33203D2028756E6974795F4F626A656374546F576F726C64202A205F676C6573566572746578293B0A202077506F735F32203D20746D707661725F333B0A20206966202828756E6974795F4C69676874536861646F77426961732E7A20213D20302E302929207B0A202020206869676870206D61743320746D707661725F343B0A20202020746D707661725F345B305D203D20756E6974795F576F726C64546F4F626A6563745B305D2E78797A3B0A20202020746D707661725F345B315D203D20756E6974795F576F726C64546F4F626A6563745B315D2E78797A3B0A20202020746D707661725F345B325D203D20756E6974795F576F726C64546F4F626A6563745B325D2E78797A3B0A202020206869676870207665633320746D707661725F353B0A20202020746D707661725F35203D206E6F726D616C697A6528285F676C65734E6F726D616C202A20746D707661725F3429293B0A20202020686967687020666C6F617420746D707661725F363B0A20202020746D707661725F36203D20646F742028746D707661725F352C206E6F726D616C697A6528285F576F726C6453706163654C69676874506F73302E78797A202D200A20202020202028746D707661725F332E78797A202A205F576F726C6453706163654C69676874506F73302E77290A202020202929293B0A2020202077506F735F322E78797A203D2028746D707661725F332E78797A202D2028746D707661725F35202A2028756E6974795F4C69676874536861646F77426961732E7A202A200A202020202020737172742828312E30202D2028746D707661725F36202A20746D707661725F362929290A202020202929293B0A20207D3B0A2020746D707661725F31203D2028756E6974795F4D61747269785650202A2077506F735F32293B0A20206869676870207665633420636C6970506F735F373B0A2020636C6970506F735F372E787977203D20746D707661725F312E7879773B0A2020636C6970506F735F372E7A203D2028746D707661725F312E7A202B20636C616D70202828756E6974795F4C69676874536861646F77426961732E78202F20746D707661725F312E77292C20302E302C20312E3029293B0A2020636C6970506F735F372E7A203D206D69782028636C6970506F735F372E7A2C206D61782028636C6970506F735F372E7A2C202D28746D707661725F312E7729292C20756E6974795F4C69676874536861646F77426961732E79293B0A2020676C5F506F736974696F6E203D20636C6970506F735F373B0A7D0A0A0A23656E6469660A23696664656620465241474D454E540A766F6964206D61696E2028290A7B0A2020676C5F46726167446174615B305D203D207665633428302E302C20302E302C20302E302C20302E30293B0A7D0A0A0A23656E6469660A03000000000000000100000000000000000000000000000000000000324F040C050000002A000000050000000100000000000000020000000C000000504F494E545F434F4F4B49450C000000534841444F57535F435542452B1600002376657273696F6E203130300A0A236966646566205645525445580A6174747269627574652076656334205F676C65735665727465783B0A6174747269627574652076656333205F676C65734E6F726D616C3B0A6174747269627574652076656334205F676C65734D756C7469546578436F6F7264303B0A6174747269627574652076656334205F676C65734D756C7469546578436F6F7264313B0A756E69666F726D2068696768702076656333205F576F726C64537061636543616D657261506F733B0A756E69666F726D2068696768702076656334205F576F726C6453706163654C69676874506F73303B0A756E69666F726D206869676870206D61743420756E6974795F4F626A656374546F576F726C643B0A756E69666F726D206869676870206D61743420756E6974795F576F726C64546F4F626A6563743B0A756E69666F726D206869676870206D61743420756E6974795F4D617472697856503B0A756E69666F726D2068696768702076656334205F4D61696E5465785F53543B0A756E69666F726D2068696768702076656334205F44657461696C416C6265646F4D61705F53543B0A756E69666F726D206D656469756D7020666C6F6174205F55565365633B0A76617279696E67206869676870207665633420786C765F544558434F4F5244303B0A76617279696E67206D656469756D70207665633320786C765F544558434F4F5244313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244323B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244325F313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244325F323B0A76617279696E67206869676870207665633320786C765F544558434F4F5244353B0A76617279696E67206869676870207665633220786C765F544558434F4F5244363B0A766F6964206D61696E2028290A7B0A20206D656469756D70207665633320746D707661725F313B0A2020746D707661725F31203D205F676C65734E6F726D616C3B0A20206869676870207665633220746D707661725F323B0A2020746D707661725F32203D205F676C65734D756C7469546578436F6F7264302E78793B0A20206869676870207665633220746D707661725F333B0A2020746D707661725F33203D205F676C65734D756C7469546578436F6F7264312E78793B0A202068696768702076656333206C696768744469725F343B0A20206D656469756D70207665633420746D707661725F353B0A20206D656469756D70207665633420746D707661725F363B0A20206D656469756D70207665633420746D707661725F373B0A20206869676870207665633220746D707661725F383B0A20206869676870207665633420746D707661725F393B0A2020746D707661725F39203D2028756E6974795F4F626A656374546F576F726C64202A205F676C6573566572746578293B0A20206869676870207665633420746D707661725F31303B0A20206869676870207665633420746D707661725F31313B0A2020746D707661725F31312E77203D20312E303B0A2020746D707661725F31312E78797A203D205F676C65735665727465782E78797A3B0A2020746D707661725F3130203D2028756E6974795F4D61747269785650202A2028756E6974795F4F626A656374546F576F726C64202A20746D707661725F313129293B0A20206869676870207665633420746578636F6F72645F31323B0A2020746578636F6F72645F31322E7879203D2028285F676C65734D756C7469546578436F6F7264302E7879202A205F4D61696E5465785F53542E787929202B205F4D61696E5465785F53542E7A77293B0A20206869676870207665633220746D707661725F31333B0A202069662028285F5556536563203D3D20302E302929207B0A20202020746D707661725F3133203D20746D707661725F323B0A20207D20656C7365207B0A20202020746D707661725F3133203D20746D707661725F333B0A20207D3B0A2020746578636F6F72645F31322E7A77203D202828746D707661725F3133202A205F44657461696C416C6265646F4D61705F53542E787929202B205F44657461696C416C6265646F4D61705F53542E7A77293B0A20206D656469756D70207665633320746D707661725F31343B0A20206869676870207665633320746D707661725F31353B0A2020746D707661725F3135203D206E6F726D616C697A652828746D707661725F392E78797A202D205F576F726C64537061636543616D657261506F7329293B0A2020746D707661725F3134203D20746D707661725F31353B0A202068696768702076656333206E6F726D5F31363B0A20206E6F726D5F3136203D20746D707661725F313B0A20206869676870206D61743320746D707661725F31373B0A2020746D707661725F31375B305D203D20756E6974795F576F726C64546F4F626A6563745B305D2E78797A3B0A2020746D707661725F31375B315D203D20756E6974795F576F726C64546F4F626A6563745B315D2E78797A3B0A2020746D707661725F31375B325D203D20756E6974795F576F726C64546F4F626A6563745B325D2E78797A3B0A20206869676870207665633320746D707661725F31383B0A2020746D707661725F3138203D206E6F726D616C697A6528286E6F726D5F3136202A20746D707661725F313729293B0A2020746D707661725F352E78797A203D207665633328302E302C20302E302C20302E30293B0A2020746D707661725F362E78797A203D207665633328302E302C20302E302C20302E30293B0A2020746D707661725F372E78797A203D20746D707661725F31383B0A20206D656469756D70207665633320746D707661725F31393B0A20206869676870207665633320746D707661725F32303B0A2020746D707661725F3230203D206E6F726D616C697A6528285F576F726C6453706163654C69676874506F73302E78797A202D2028746D707661725F392E78797A202A205F576F726C6453706163654C69676874506F73302E772929293B0A2020746D707661725F3139203D20746D707661725F32303B0A20206C696768744469725F34203D20746D707661725F31393B0A2020746D707661725F352E77203D206C696768744469725F342E783B0A2020746D707661725F362E77203D206C696768744469725F342E793B0A2020746D707661725F372E77203D206C696768744469725F342E7A3B0A2020676C5F506F736974696F6E203D20746D707661725F31303B0A2020786C765F544558434F4F524430203D20746578636F6F72645F31323B0A2020786C765F544558434F4F524431203D20746D707661725F31343B0A2020786C765F544558434F4F524432203D20746D707661725F353B0A2020786C765F544558434F4F5244325F31203D20746D707661725F363B0A2020786C765F544558434F4F5244325F32203D20746D707661725F373B0A2020786C765F544558434F4F524435203D20746D707661725F392E78797A3B0A2020786C765F544558434F4F524436203D20746D707661725F383B0A7D0A0A0A23656E6469660A23696664656620465241474D454E540A756E69666F726D2068696768702076656334205F4C69676874506F736974696F6E52616E67653B0A756E69666F726D206D656469756D702076656334205F4C69676874536861646F77446174613B0A756E69666F726D206C6F77702076656334205F4C69676874436F6C6F72303B0A756E69666F726D2073616D706C6572324420756E6974795F4E4878526F7567686E6573733B0A756E69666F726D2068696768702073616D706C657243756265205F536861646F774D6170546578747572653B0A756E69666F726D206D656469756D702076656334205F436F6C6F723B0A756E69666F726D2073616D706C65723244205F4D61696E5465783B0A756E69666F726D206D656469756D7020666C6F6174205F4D6574616C6C69633B0A756E69666F726D206D656469756D7020666C6F6174205F476C6F7373696E6573733B0A756E69666F726D206C6F77702073616D706C657243756265205F4C6967687454657874757265303B0A756E69666F726D206869676870206D61743420756E6974795F576F726C64546F4C696768743B0A756E69666F726D2073616D706C65723244205F4C696768745465787475726542303B0A76617279696E67206869676870207665633420786C765F544558434F4F5244303B0A76617279696E67206D656469756D70207665633320786C765F544558434F4F5244313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244323B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244325F313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244325F323B0A76617279696E67206869676870207665633320786C765F544558434F4F5244353B0A766F6964206D61696E2028290A7B0A20206C6F777020666C6F617420736861646F775F313B0A202068696768702076656333206C69676874436F6F72645F323B0A20206C6F7770207665633420746D707661725F333B0A2020746D707661725F33203D2074657874757265324420285F4D61696E5465782C20786C765F544558434F4F5244302E7879293B0A20206D656469756D70207665633320746D707661725F343B0A2020746D707661725F34203D20285F436F6C6F722E78797A202A20746D707661725F332E78797A293B0A20206D656469756D70207665633320746D707661725F353B0A20206D656469756D70207665633320746D707661725F363B0A2020746D707661725F36203D206D697820287665633328302E323230393136332C20302E323230393136332C20302E32323039313633292C20746D707661725F342C2076656333285F4D6574616C6C696329293B0A2020746D707661725F35203D2028746D707661725F34202A2028302E37373930383337202D20285F4D6574616C6C6963202A20302E373739303833372929293B0A20206D656469756D70207665633320746D707661725F373B0A2020746D707661725F37203D206E6F726D616C697A6528786C765F544558434F4F5244325F322E78797A293B0A20206869676870207665633420746D707661725F383B0A2020746D707661725F382E77203D20312E303B0A2020746D707661725F382E78797A203D20786C765F544558434F4F5244353B0A20206C69676874436F6F72645F32203D2028756E6974795F576F726C64546F4C69676874202A20746D707661725F38292E78797A3B0A202068696768702076656333207665635F393B0A20207665635F39203D2028786C765F544558434F4F524435202D205F4C69676874506F736974696F6E52616E67652E78797A293B0A2020686967687020666C6F6174206D79646973745F31303B0A20206D79646973745F3130203D20282873717274280A20202020646F7420287665635F392C207665635F39290A202029202A205F4C69676874506F736974696F6E52616E67652E7729202A20302E3937293B0A2020686967687020666C6F617420746D707661725F31313B0A2020746D707661725F3131203D20646F742028746578747572654375626520285F536861646F774D6170546578747572652C207665635F39292C207665633428312E302C20302E3030333932313536392C20312E3533373837652D30352C20362E303330383633652D303829293B0A20206D656469756D7020666C6F617420746D707661725F31323B0A20206966202828746D707661725F3131203C206D79646973745F31302929207B0A20202020746D707661725F3132203D205F4C69676874536861646F77446174612E783B0A20207D20656C7365207B0A20202020746D707661725F3132203D20312E303B0A20207D3B0A2020736861646F775F31203D20746D707661725F31323B0A2020686967687020666C6F617420746D707661725F31333B0A2020746D707661725F3133203D20646F7420286C69676874436F6F72645F322C206C69676874436F6F72645F32293B0A20206C6F777020666C6F617420746D707661725F31343B0A2020746D707661725F3134203D20282874657874757265324420285F4C696768745465787475726542302C207665633228746D707661725F313329292E77202A20746578747572654375626520285F4C6967687454657874757265302C206C69676874436F6F72645F32292E7729202A20736861646F775F31293B0A20206D656469756D70207665633320746D707661725F31353B0A2020746D707661725F31352E78203D20786C765F544558434F4F5244322E773B0A2020746D707661725F31352E79203D20786C765F544558434F4F5244325F312E773B0A2020746D707661725F31352E7A203D20786C765F544558434F4F5244325F322E773B0A20206D656469756D7020666C6F617420617474656E5F31363B0A2020617474656E5F3136203D20746D707661725F31343B0A20206D656469756D70207665633320746D707661725F31373B0A2020746D707661725F3137203D205F4C69676874436F6C6F72302E78797A3B0A2020746D707661725F3137203D2028746D707661725F3137202A20617474656E5F3136293B0A20206D656469756D70207665633320766965774469725F31383B0A2020766965774469725F3138203D202D28786C765F544558434F4F524431293B0A20206D656469756D70207665633220746D707661725F31393B0A2020746D707661725F31392E78203D20646F74202828766965774469725F3138202D2028322E30202A200A2020202028646F742028746D707661725F372C20766965774469725F313829202A20746D707661725F37290A202029292C20746D707661725F3135293B0A2020746D707661725F31392E79203D2028312E30202D20636C616D702028646F742028746D707661725F372C20766965774469725F3138292C20302E302C20312E3029293B0A20206D656469756D70207665633220746D707661725F32303B0A2020746D707661725F32302E78203D202828746D707661725F3139202A20746D707661725F313929202A2028746D707661725F3139202A20746D707661725F313929292E783B0A2020746D707661725F32302E79203D2028312E30202D205F476C6F7373696E657373293B0A20206C6F7770207665633420746D707661725F32313B0A2020746D707661725F3231203D207465787475726532442028756E6974795F4E4878526F7567686E6573732C20746D707661725F3230293B0A20206D656469756D70207665633420746D707661725F32323B0A2020746D707661725F32322E77203D20312E303B0A2020746D707661725F32322E78797A203D202828746D707661725F35202B20280A2020202028746D707661725F32312E77202A2031362E30290A2020202A20746D707661725F362929202A2028746D707661725F3137202A20636C616D7020280A20202020646F742028746D707661725F372C20746D707661725F3135290A20202C20302E302C20312E302929293B0A20206D656469756D70207665633420786C61745F7661726F75747075745F32333B0A2020786C61745F7661726F75747075745F32332E78797A203D20746D707661725F32322E78797A3B0A2020786C61745F7661726F75747075745F32332E77203D20312E303B0A2020676C5F46726167446174615B305D203D20786C61745F7661726F75747075745F32333B0A7D0A0A0A23656E6469660A001B000000000000000100000000000000000000000000000000000000324F040C050000002B0000000200000000000000000000000100000005000000504F494E54000000F01300002376657273696F6E203130300A0A236966646566205645525445580A6174747269627574652076656334205F676C65735665727465783B0A6174747269627574652076656333205F676C65734E6F726D616C3B0A6174747269627574652076656334205F676C65734D756C7469546578436F6F7264303B0A6174747269627574652076656334205F676C65734D756C7469546578436F6F7264313B0A756E69666F726D2068696768702076656333205F576F726C64537061636543616D657261506F733B0A756E69666F726D2068696768702076656334205F576F726C6453706163654C69676874506F73303B0A756E69666F726D206869676870206D61743420756E6974795F4F626A656374546F576F726C643B0A756E69666F726D206869676870206D61743420756E6974795F576F726C64546F4F626A6563743B0A756E69666F726D206869676870206D61743420756E6974795F4D617472697856503B0A756E69666F726D2068696768702076656334205F4D61696E5465785F53543B0A756E69666F726D2068696768702076656334205F44657461696C416C6265646F4D61705F53543B0A756E69666F726D206D656469756D7020666C6F6174205F55565365633B0A76617279696E67206869676870207665633420786C765F544558434F4F5244303B0A76617279696E67206D656469756D70207665633320786C765F544558434F4F5244313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244323B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244325F313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244325F323B0A76617279696E67206869676870207665633320786C765F544558434F4F5244353B0A76617279696E67206869676870207665633220786C765F544558434F4F5244363B0A766F6964206D61696E2028290A7B0A20206D656469756D70207665633320746D707661725F313B0A2020746D707661725F31203D205F676C65734E6F726D616C3B0A20206869676870207665633220746D707661725F323B0A2020746D707661725F32203D205F676C65734D756C7469546578436F6F7264302E78793B0A20206869676870207665633220746D707661725F333B0A2020746D707661725F33203D205F676C65734D756C7469546578436F6F7264312E78793B0A202068696768702076656333206C696768744469725F343B0A20206D656469756D70207665633420746D707661725F353B0A20206D656469756D70207665633420746D707661725F363B0A20206D656469756D70207665633420746D707661725F373B0A20206869676870207665633220746D707661725F383B0A20206869676870207665633420746D707661725F393B0A2020746D707661725F39203D2028756E6974795F4F626A656374546F576F726C64202A205F676C6573566572746578293B0A20206869676870207665633420746D707661725F31303B0A20206869676870207665633420746D707661725F31313B0A2020746D707661725F31312E77203D20312E303B0A2020746D707661725F31312E78797A203D205F676C65735665727465782E78797A3B0A2020746D707661725F3130203D2028756E6974795F4D61747269785650202A2028756E6974795F4F626A656374546F576F726C64202A20746D707661725F313129293B0A20206869676870207665633420746578636F6F72645F31323B0A2020746578636F6F72645F31322E7879203D2028285F676C65734D756C7469546578436F6F7264302E7879202A205F4D61696E5465785F53542E787929202B205F4D61696E5465785F53542E7A77293B0A20206869676870207665633220746D707661725F31333B0A202069662028285F5556536563203D3D20302E302929207B0A20202020746D707661725F3133203D20746D707661725F323B0A20207D20656C7365207B0A20202020746D707661725F3133203D20746D707661725F333B0A20207D3B0A2020746578636F6F72645F31322E7A77203D202828746D707661725F3133202A205F44657461696C416C6265646F4D61705F53542E787929202B205F44657461696C416C6265646F4D61705F53542E7A77293B0A20206D656469756D70207665633320746D707661725F31343B0A202068696768702076656333206E5F31353B0A20206E5F3135203D2028746D707661725F392E78797A202D205F576F726C64537061636543616D657261506F73293B0A2020746D707661725F3134203D206E5F31353B0A202068696768702076656333206E6F726D5F31363B0A20206E6F726D5F3136203D20746D707661725F313B0A20206869676870206D61743320746D707661725F31373B0A2020746D707661725F31375B305D203D20756E6974795F576F726C64546F4F626A6563745B305D2E78797A3B0A2020746D707661725F31375B315D203D20756E6974795F576F726C64546F4F626A6563745B315D2E78797A3B0A2020746D707661725F31375B325D203D20756E6974795F576F726C64546F4F626A6563745B325D2E78797A3B0A20206869676870207665633320746D707661725F31383B0A2020746D707661725F3138203D206E6F726D616C697A6528286E6F726D5F3136202A20746D707661725F313729293B0A2020746D707661725F352E78797A203D207665633328302E302C20302E302C20302E30293B0A2020746D707661725F362E78797A203D207665633328302E302C20302E302C20302E30293B0A2020746D707661725F372E78797A203D20746D707661725F31383B0A20206869676870207665633320746D707661725F31393B0A2020746D707661725F3139203D20285F576F726C6453706163654C69676874506F73302E78797A202D2028746D707661725F392E78797A202A205F576F726C6453706163654C69676874506F73302E7729293B0A20206D656469756D70207665633320746D707661725F32303B0A2020746D707661725F3230203D20746D707661725F31393B0A20206C696768744469725F34203D20746D707661725F32303B0A2020746D707661725F352E77203D206C696768744469725F342E783B0A2020746D707661725F362E77203D206C696768744469725F342E793B0A2020746D707661725F372E77203D206C696768744469725F342E7A3B0A2020676C5F506F736974696F6E203D20746D707661725F31303B0A2020786C765F544558434F4F524430203D20746578636F6F72645F31323B0A2020786C765F544558434F4F524431203D20746D707661725F31343B0A2020786C765F544558434F4F524432203D20746D707661725F353B0A2020786C765F544558434F4F5244325F31203D20746D707661725F363B0A2020786C765F544558434F4F5244325F32203D20746D707661725F373B0A2020786C765F544558434F4F524435203D20746D707661725F392E78797A3B0A2020786C765F544558434F4F524436203D20746D707661725F383B0A7D0A0A0A23656E6469660A23696664656620465241474D454E540A756E69666F726D206C6F77702076656334205F4C69676874436F6C6F72303B0A756E69666F726D206D656469756D702076656334205F436F6C6F723B0A756E69666F726D2073616D706C65723244205F4D61696E5465783B0A756E69666F726D206D656469756D7020666C6F6174205F4D6574616C6C69633B0A756E69666F726D206D656469756D7020666C6F6174205F476C6F7373696E6573733B0A756E69666F726D2073616D706C65723244205F4C6967687454657874757265303B0A756E69666F726D206869676870206D61743420756E6974795F576F726C64546F4C696768743B0A76617279696E67206869676870207665633420786C765F544558434F4F5244303B0A76617279696E67206D656469756D70207665633320786C765F544558434F4F5244313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244323B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244325F313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244325F323B0A76617279696E67206869676870207665633320786C765F544558434F4F5244353B0A766F6964206D61696E2028290A7B0A202068696768702076656333206C69676874436F6F72645F313B0A20206C6F7770207665633420746D707661725F323B0A2020746D707661725F32203D2074657874757265324420285F4D61696E5465782C20786C765F544558434F4F5244302E7879293B0A20206D656469756D70207665633320746D707661725F333B0A2020746D707661725F33203D20285F436F6C6F722E78797A202A20746D707661725F322E78797A293B0A20206D656469756D70207665633320746D707661725F343B0A2020746D707661725F34203D206E6F726D616C697A6528786C765F544558434F4F5244325F322E78797A293B0A20206869676870207665633420746D707661725F353B0A2020746D707661725F352E77203D20312E303B0A2020746D707661725F352E78797A203D20786C765F544558434F4F5244353B0A20206C69676874436F6F72645F31203D2028756E6974795F576F726C64546F4C69676874202A20746D707661725F35292E78797A3B0A2020686967687020666C6F617420746D707661725F363B0A2020746D707661725F36203D20646F7420286C69676874436F6F72645F312C206C69676874436F6F72645F31293B0A20206C6F777020666C6F617420746D707661725F373B0A2020746D707661725F37203D2074657874757265324420285F4C6967687454657874757265302C207665633228746D707661725F3629292E773B0A20206D656469756D70207665633320746D707661725F383B0A2020746D707661725F382E78203D20786C765F544558434F4F5244322E773B0A2020746D707661725F382E79203D20786C765F544558434F4F5244325F312E773B0A2020746D707661725F382E7A203D20786C765F544558434F4F5244325F322E773B0A20206D656469756D7020666C6F617420617474656E5F393B0A2020617474656E5F39203D20746D707661725F373B0A20206D656469756D70207665633320746D707661725F31303B0A2020746D707661725F3130203D205F4C69676874436F6C6F72302E78797A3B0A20206D656469756D70207665633320746D707661725F31313B0A2020746D707661725F3131203D206E6F726D616C697A6528746D707661725F38293B0A2020746D707661725F3130203D2028746D707661725F3130202A20617474656E5F39293B0A20206D656469756D7020666C6F61742073706563756C61725465726D5F31323B0A20206D656469756D70207665633320746D707661725F31333B0A20206D656469756D70207665633320696E5665635F31343B0A2020696E5665635F3134203D2028746D707661725F3131202D206E6F726D616C697A6528786C765F544558434F4F52443129293B0A2020746D707661725F3133203D2028696E5665635F3134202A20696E766572736573717274286D61782028302E3030312C200A20202020646F742028696E5665635F31342C20696E5665635F3134290A20202929293B0A20206D656469756D7020666C6F617420746D707661725F31353B0A2020746D707661725F3135203D20636C616D702028646F742028746D707661725F342C20746D707661725F3133292C20302E302C20312E30293B0A20206D656469756D7020666C6F617420746D707661725F31363B0A2020746D707661725F3136203D2028312E30202D205F476C6F7373696E657373293B0A20206D656469756D7020666C6F617420746D707661725F31373B0A2020746D707661725F3137203D2028746D707661725F3136202A20746D707661725F3136293B0A202073706563756C61725465726D5F3132203D202828746D707661725F3137202F20280A20202020286D61782028302E33322C20636C616D702028646F742028746D707661725F31312C20746D707661725F3133292C20302E302C20312E302929202A2028312E35202B20746D707661725F313729290A2020202A200A20202020282828746D707661725F3135202A20746D707661725F313529202A202828746D707661725F3137202A20746D707661725F313729202D20312E302929202B20312E3030303031290A20202929202D20302E30303031293B0A20206D656469756D7020666C6F617420746D707661725F31383B0A2020746D707661725F3138203D20636C616D70202873706563756C61725465726D5F31322C20302E302C203130302E30293B0A202073706563756C61725465726D5F3132203D20746D707661725F31383B0A20206D656469756D70207665633420746D707661725F31393B0A2020746D707661725F31392E77203D20312E303B0A2020746D707661725F31392E78797A203D202828280A2020202028746D707661725F33202A2028302E37373930383337202D20285F4D6574616C6C6963202A20302E373739303833372929290A2020202B200A2020202028746D707661725F3138202A206D697820287665633328302E323230393136332C20302E323230393136332C20302E32323039313633292C20746D707661725F332C2076656333285F4D6574616C6C69632929290A202029202A20746D707661725F313029202A20636C616D702028646F742028746D707661725F342C20746D707661725F3131292C20302E302C20312E3029293B0A20206D656469756D70207665633420786C61745F7661726F75747075745F32303B0A2020786C61745F7661726F75747075745F32302E78797A203D20746D707661725F31392E78797A3B0A2020786C61745F7661726F75747075745F32302E77203D20312E303B0A2020676C5F46726167446174615B305D203D20786C61745F7661726F75747075745F32303B0A7D0A0A0A23656E6469660A1B000000000000000100000000000000000000000000000000000000324F040C0500000021000000040000000000000000000000010000000C000000504F494E545F434F4F4B4945521300002376657273696F6E203130300A0A236966646566205645525445580A6174747269627574652076656334205F676C65735665727465783B0A6174747269627574652076656333205F676C65734E6F726D616C3B0A6174747269627574652076656334205F676C65734D756C7469546578436F6F7264303B0A6174747269627574652076656334205F676C65734D756C7469546578436F6F7264313B0A756E69666F726D2068696768702076656333205F576F726C64537061636543616D657261506F733B0A756E69666F726D2068696768702076656334205F576F726C6453706163654C69676874506F73303B0A756E69666F726D206869676870206D61743420756E6974795F4F626A656374546F576F726C643B0A756E69666F726D206869676870206D61743420756E6974795F576F726C64546F4F626A6563743B0A756E69666F726D206869676870206D61743420756E6974795F4D617472697856503B0A756E69666F726D2068696768702076656334205F4D61696E5465785F53543B0A756E69666F726D2068696768702076656334205F44657461696C416C6265646F4D61705F53543B0A756E69666F726D206D656469756D7020666C6F6174205F55565365633B0A76617279696E67206869676870207665633420786C765F544558434F4F5244303B0A76617279696E67206D656469756D70207665633320786C765F544558434F4F5244313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244323B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244325F313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244325F323B0A76617279696E67206869676870207665633320786C765F544558434F4F5244353B0A76617279696E67206869676870207665633220786C765F544558434F4F5244363B0A766F6964206D61696E2028290A7B0A20206D656469756D70207665633320746D707661725F313B0A2020746D707661725F31203D205F676C65734E6F726D616C3B0A20206869676870207665633220746D707661725F323B0A2020746D707661725F32203D205F676C65734D756C7469546578436F6F7264302E78793B0A20206869676870207665633220746D707661725F333B0A2020746D707661725F33203D205F676C65734D756C7469546578436F6F7264312E78793B0A202068696768702076656333206C696768744469725F343B0A20206D656469756D70207665633420746D707661725F353B0A20206D656469756D70207665633420746D707661725F363B0A20206D656469756D70207665633420746D707661725F373B0A20206869676870207665633220746D707661725F383B0A20206869676870207665633420746D707661725F393B0A2020746D707661725F39203D2028756E6974795F4F626A656374546F576F726C64202A205F676C6573566572746578293B0A20206869676870207665633420746D707661725F31303B0A20206869676870207665633420746D707661725F31313B0A2020746D707661725F31312E77203D20312E303B0A2020746D707661725F31312E78797A203D205F676C65735665727465782E78797A3B0A2020746D707661725F3130203D2028756E6974795F4D61747269785650202A2028756E6974795F4F626A656374546F576F726C64202A20746D707661725F313129293B0A20206869676870207665633420746578636F6F72645F31323B0A2020746578636F6F72645F31322E7879203D2028285F676C65734D756C7469546578436F6F7264302E7879202A205F4D61696E5465785F53542E787929202B205F4D61696E5465785F53542E7A77293B0A20206869676870207665633220746D707661725F31333B0A202069662028285F5556536563203D3D20302E302929207B0A20202020746D707661725F3133203D20746D707661725F323B0A20207D20656C7365207B0A20202020746D707661725F3133203D20746D707661725F333B0A20207D3B0A2020746578636F6F72645F31322E7A77203D202828746D707661725F3133202A205F44657461696C416C6265646F4D61705F53542E787929202B205F44657461696C416C6265646F4D61705F53542E7A77293B0A20206D656469756D70207665633320746D707661725F31343B0A20206869676870207665633320746D707661725F31353B0A2020746D707661725F3135203D206E6F726D616C697A652828746D707661725F392E78797A202D205F576F726C64537061636543616D657261506F7329293B0A2020746D707661725F3134203D20746D707661725F31353B0A202068696768702076656333206E6F726D5F31363B0A20206E6F726D5F3136203D20746D707661725F313B0A20206869676870206D61743320746D707661725F31373B0A2020746D707661725F31375B305D203D20756E6974795F576F726C64546F4F626A6563745B305D2E78797A3B0A2020746D707661725F31375B315D203D20756E6974795F576F726C64546F4F626A6563745B315D2E78797A3B0A2020746D707661725F31375B325D203D20756E6974795F576F726C64546F4F626A6563745B325D2E78797A3B0A20206869676870207665633320746D707661725F31383B0A2020746D707661725F3138203D206E6F726D616C697A6528286E6F726D5F3136202A20746D707661725F313729293B0A2020746D707661725F352E78797A203D207665633328302E302C20302E302C20302E30293B0A2020746D707661725F362E78797A203D207665633328302E302C20302E302C20302E30293B0A2020746D707661725F372E78797A203D20746D707661725F31383B0A20206D656469756D70207665633320746D707661725F31393B0A20206869676870207665633320746D707661725F32303B0A2020746D707661725F3230203D206E6F726D616C697A6528285F576F726C6453706163654C69676874506F73302E78797A202D2028746D707661725F392E78797A202A205F576F726C6453706163654C69676874506F73302E772929293B0A2020746D707661725F3139203D20746D707661725F32303B0A20206C696768744469725F34203D20746D707661725F31393B0A2020746D707661725F352E77203D206C696768744469725F342E783B0A2020746D707661725F362E77203D206C696768744469725F342E793B0A2020746D707661725F372E77203D206C696768744469725F342E7A3B0A2020676C5F506F736974696F6E203D20746D707661725F31303B0A2020786C765F544558434F4F524430203D20746578636F6F72645F31323B0A2020786C765F544558434F4F524431203D20746D707661725F31343B0A2020786C765F544558434F4F524432203D20746D707661725F353B0A2020786C765F544558434F4F5244325F31203D20746D707661725F363B0A2020786C765F544558434F4F5244325F32203D20746D707661725F373B0A2020786C765F544558434F4F524435203D20746D707661725F392E78797A3B0A2020786C765F544558434F4F524436203D20746D707661725F383B0A7D0A0A0A23656E6469660A23696664656620465241474D454E540A756E69666F726D206C6F77702076656334205F4C69676874436F6C6F72303B0A756E69666F726D2073616D706C6572324420756E6974795F4E4878526F7567686E6573733B0A756E69666F726D206D656469756D702076656334205F436F6C6F723B0A756E69666F726D2073616D706C65723244205F4D61696E5465783B0A756E69666F726D206D656469756D7020666C6F6174205F4D6574616C6C69633B0A756E69666F726D206D656469756D7020666C6F6174205F476C6F7373696E6573733B0A756E69666F726D206C6F77702073616D706C657243756265205F4C6967687454657874757265303B0A756E69666F726D206869676870206D61743420756E6974795F576F726C64546F4C696768743B0A756E69666F726D2073616D706C65723244205F4C696768745465787475726542303B0A76617279696E67206869676870207665633420786C765F544558434F4F5244303B0A76617279696E67206D656469756D70207665633320786C765F544558434F4F5244313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244323B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244325F313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244325F323B0A76617279696E67206869676870207665633320786C765F544558434F4F5244353B0A766F6964206D61696E2028290A7B0A202068696768702076656333206C69676874436F6F72645F313B0A20206C6F7770207665633420746D707661725F323B0A2020746D707661725F32203D2074657874757265324420285F4D61696E5465782C20786C765F544558434F4F5244302E7879293B0A20206D656469756D70207665633320746D707661725F333B0A2020746D707661725F33203D20285F436F6C6F722E78797A202A20746D707661725F322E78797A293B0A20206D656469756D70207665633320746D707661725F343B0A2020746D707661725F34203D206E6F726D616C697A6528786C765F544558434F4F5244325F322E78797A293B0A20206869676870207665633420746D707661725F353B0A2020746D707661725F352E77203D20312E303B0A2020746D707661725F352E78797A203D20786C765F544558434F4F5244353B0A20206C69676874436F6F72645F31203D2028756E6974795F576F726C64546F4C69676874202A20746D707661725F35292E78797A3B0A2020686967687020666C6F617420746D707661725F363B0A2020746D707661725F36203D20646F7420286C69676874436F6F72645F312C206C69676874436F6F72645F31293B0A20206C6F777020666C6F617420746D707661725F373B0A2020746D707661725F37203D202874657874757265324420285F4C696768745465787475726542302C207665633228746D707661725F3629292E77202A20746578747572654375626520285F4C6967687454657874757265302C206C69676874436F6F72645F31292E77293B0A20206D656469756D70207665633320746D707661725F383B0A2020746D707661725F382E78203D20786C765F544558434F4F5244322E773B0A2020746D707661725F382E79203D20786C765F544558434F4F5244325F312E773B0A2020746D707661725F382E7A203D20786C765F544558434F4F5244325F322E773B0A20206D656469756D7020666C6F617420617474656E5F393B0A2020617474656E5F39203D20746D707661725F373B0A20206D656469756D70207665633320746D707661725F31303B0A2020746D707661725F3130203D205F4C69676874436F6C6F72302E78797A3B0A2020746D707661725F3130203D2028746D707661725F3130202A20617474656E5F39293B0A20206D656469756D70207665633320766965774469725F31313B0A2020766965774469725F3131203D202D28786C765F544558434F4F524431293B0A20206D656469756D70207665633220746D707661725F31323B0A2020746D707661725F31322E78203D20646F74202828766965774469725F3131202D2028322E30202A200A2020202028646F742028746D707661725F342C20766965774469725F313129202A20746D707661725F34290A202029292C20746D707661725F38293B0A2020746D707661725F31322E79203D2028312E30202D20636C616D702028646F742028746D707661725F342C20766965774469725F3131292C20302E302C20312E3029293B0A20206D656469756D70207665633220746D707661725F31333B0A2020746D707661725F31332E78203D202828746D707661725F3132202A20746D707661725F313229202A2028746D707661725F3132202A20746D707661725F313229292E783B0A2020746D707661725F31332E79203D2028312E30202D205F476C6F7373696E657373293B0A20206C6F7770207665633420746D707661725F31343B0A2020746D707661725F3134203D207465787475726532442028756E6974795F4E4878526F7567686E6573732C20746D707661725F3133293B0A20206D656469756D70207665633420746D707661725F31353B0A2020746D707661725F31352E77203D20312E303B0A2020746D707661725F31352E78797A203D20282828746D707661725F33202A200A2020202028302E37373930383337202D20285F4D6574616C6C6963202A20302E3737393038333729290A202029202B20280A2020202028746D707661725F31342E77202A2031362E30290A2020202A200A202020206D697820287665633328302E323230393136332C20302E323230393136332C20302E32323039313633292C20746D707661725F332C2076656333285F4D6574616C6C696329290A20202929202A2028746D707661725F3130202A20636C616D7020280A20202020646F742028746D707661725F342C20746D707661725F38290A20202C20302E302C20312E302929293B0A20206D656469756D70207665633420786C61745F7661726F75747075745F31363B0A2020786C61745F7661726F75747075745F31362E78797A203D20746D707661725F31352E78797A3B0A2020786C61745F7661726F75747075745F31362E77203D20312E303B0A2020676C5F46726167446174615B305D203D20786C61745F7661726F75747075745F31363B0A7D0A0A0A23656E6469660A00001B000000000000000100000000000000000000000000000000000000324F040C05000000290000000200000000000000000000000100000012000000444952454354494F4E414C5F434F4F4B49450000F71200002376657273696F6E203130300A0A236966646566205645525445580A6174747269627574652076656334205F676C65735665727465783B0A6174747269627574652076656333205F676C65734E6F726D616C3B0A6174747269627574652076656334205F676C65734D756C7469546578436F6F7264303B0A6174747269627574652076656334205F676C65734D756C7469546578436F6F7264313B0A756E69666F726D2068696768702076656333205F576F726C64537061636543616D657261506F733B0A756E69666F726D206D656469756D702076656334205F576F726C6453706163654C69676874506F73303B0A756E69666F726D206869676870206D61743420756E6974795F4F626A656374546F576F726C643B0A756E69666F726D206869676870206D61743420756E6974795F576F726C64546F4F626A6563743B0A756E69666F726D206869676870206D61743420756E6974795F4D617472697856503B0A756E69666F726D2068696768702076656334205F4D61696E5465785F53543B0A756E69666F726D2068696768702076656334205F44657461696C416C6265646F4D61705F53543B0A756E69666F726D206D656469756D7020666C6F6174205F55565365633B0A76617279696E67206869676870207665633420786C765F544558434F4F5244303B0A76617279696E67206D656469756D70207665633320786C765F544558434F4F5244313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244323B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244325F313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244325F323B0A76617279696E67206869676870207665633320786C765F544558434F4F5244353B0A76617279696E67206869676870207665633220786C765F544558434F4F5244363B0A766F6964206D61696E2028290A7B0A20206D656469756D70207665633320746D707661725F313B0A2020746D707661725F31203D205F676C65734E6F726D616C3B0A20206869676870207665633220746D707661725F323B0A2020746D707661725F32203D205F676C65734D756C7469546578436F6F7264302E78793B0A20206869676870207665633220746D707661725F333B0A2020746D707661725F33203D205F676C65734D756C7469546578436F6F7264312E78793B0A20206D656469756D70207665633420746D707661725F343B0A20206D656469756D70207665633420746D707661725F353B0A20206D656469756D70207665633420746D707661725F363B0A20206869676870207665633220746D707661725F373B0A20206869676870207665633420746D707661725F383B0A2020746D707661725F38203D2028756E6974795F4F626A656374546F576F726C64202A205F676C6573566572746578293B0A20206869676870207665633420746D707661725F393B0A20206869676870207665633420746D707661725F31303B0A2020746D707661725F31302E77203D20312E303B0A2020746D707661725F31302E78797A203D205F676C65735665727465782E78797A3B0A2020746D707661725F39203D2028756E6974795F4D61747269785650202A2028756E6974795F4F626A656374546F576F726C64202A20746D707661725F313029293B0A20206869676870207665633420746578636F6F72645F31313B0A2020746578636F6F72645F31312E7879203D2028285F676C65734D756C7469546578436F6F7264302E7879202A205F4D61696E5465785F53542E787929202B205F4D61696E5465785F53542E7A77293B0A20206869676870207665633220746D707661725F31323B0A202069662028285F5556536563203D3D20302E302929207B0A20202020746D707661725F3132203D20746D707661725F323B0A20207D20656C7365207B0A20202020746D707661725F3132203D20746D707661725F333B0A20207D3B0A2020746578636F6F72645F31312E7A77203D202828746D707661725F3132202A205F44657461696C416C6265646F4D61705F53542E787929202B205F44657461696C416C6265646F4D61705F53542E7A77293B0A20206D656469756D70207665633320746D707661725F31333B0A202068696768702076656333206E5F31343B0A20206E5F3134203D2028746D707661725F382E78797A202D205F576F726C64537061636543616D657261506F73293B0A2020746D707661725F3133203D206E5F31343B0A202068696768702076656333206E6F726D5F31353B0A20206E6F726D5F3135203D20746D707661725F313B0A20206869676870206D61743320746D707661725F31363B0A2020746D707661725F31365B305D203D20756E6974795F576F726C64546F4F626A6563745B305D2E78797A3B0A2020746D707661725F31365B315D203D20756E6974795F576F726C64546F4F626A6563745B315D2E78797A3B0A2020746D707661725F31365B325D203D20756E6974795F576F726C64546F4F626A6563745B325D2E78797A3B0A20206869676870207665633320746D707661725F31373B0A2020746D707661725F3137203D206E6F726D616C697A6528286E6F726D5F3135202A20746D707661725F313629293B0A2020746D707661725F342E78797A203D207665633328302E302C20302E302C20302E30293B0A2020746D707661725F352E78797A203D207665633328302E302C20302E302C20302E30293B0A2020746D707661725F362E78797A203D20746D707661725F31373B0A20206869676870207665633320746D707661725F31383B0A2020746D707661725F3138203D20285F576F726C6453706163654C69676874506F73302E78797A202D2028746D707661725F382E78797A202A205F576F726C6453706163654C69676874506F73302E7729293B0A2020746D707661725F342E77203D20746D707661725F31382E783B0A2020746D707661725F352E77203D20746D707661725F31382E793B0A2020746D707661725F362E77203D20746D707661725F31382E7A3B0A2020676C5F506F736974696F6E203D20746D707661725F393B0A2020786C765F544558434F4F524430203D20746578636F6F72645F31313B0A2020786C765F544558434F4F524431203D20746D707661725F31333B0A2020786C765F544558434F4F524432203D20746D707661725F343B0A2020786C765F544558434F4F5244325F31203D20746D707661725F353B0A2020786C765F544558434F4F5244325F32203D20746D707661725F363B0A2020786C765F544558434F4F524435203D20746D707661725F382E78797A3B0A2020786C765F544558434F4F524436203D20746D707661725F373B0A7D0A0A0A23656E6469660A23696664656620465241474D454E540A756E69666F726D206C6F77702076656334205F4C69676874436F6C6F72303B0A756E69666F726D206D656469756D702076656334205F436F6C6F723B0A756E69666F726D2073616D706C65723244205F4D61696E5465783B0A756E69666F726D206D656469756D7020666C6F6174205F4D6574616C6C69633B0A756E69666F726D206D656469756D7020666C6F6174205F476C6F7373696E6573733B0A756E69666F726D2073616D706C65723244205F4C6967687454657874757265303B0A756E69666F726D206869676870206D61743420756E6974795F576F726C64546F4C696768743B0A76617279696E67206869676870207665633420786C765F544558434F4F5244303B0A76617279696E67206D656469756D70207665633320786C765F544558434F4F5244313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244323B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244325F313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244325F323B0A76617279696E67206869676870207665633320786C765F544558434F4F5244353B0A766F6964206D61696E2028290A7B0A202068696768702076656332206C69676874436F6F72645F313B0A20206C6F7770207665633420746D707661725F323B0A2020746D707661725F32203D2074657874757265324420285F4D61696E5465782C20786C765F544558434F4F5244302E7879293B0A20206D656469756D70207665633320746D707661725F333B0A2020746D707661725F33203D20285F436F6C6F722E78797A202A20746D707661725F322E78797A293B0A20206D656469756D70207665633320746D707661725F343B0A2020746D707661725F34203D206E6F726D616C697A6528786C765F544558434F4F5244325F322E78797A293B0A20206869676870207665633420746D707661725F353B0A2020746D707661725F352E77203D20312E303B0A2020746D707661725F352E78797A203D20786C765F544558434F4F5244353B0A20206C69676874436F6F72645F31203D2028756E6974795F576F726C64546F4C69676874202A20746D707661725F35292E78793B0A20206C6F777020666C6F617420746D707661725F363B0A2020746D707661725F36203D2074657874757265324420285F4C6967687454657874757265302C206C69676874436F6F72645F31292E773B0A20206D656469756D70207665633320746D707661725F373B0A2020746D707661725F372E78203D20786C765F544558434F4F5244322E773B0A2020746D707661725F372E79203D20786C765F544558434F4F5244325F312E773B0A2020746D707661725F372E7A203D20786C765F544558434F4F5244325F322E773B0A20206D656469756D7020666C6F617420617474656E5F383B0A2020617474656E5F38203D20746D707661725F363B0A20206D656469756D70207665633320746D707661725F393B0A2020746D707661725F39203D205F4C69676874436F6C6F72302E78797A3B0A2020746D707661725F39203D2028746D707661725F39202A20617474656E5F38293B0A20206D656469756D7020666C6F61742073706563756C61725465726D5F31303B0A20206D656469756D70207665633320746D707661725F31313B0A20206D656469756D70207665633320696E5665635F31323B0A2020696E5665635F3132203D2028746D707661725F37202D206E6F726D616C697A6528786C765F544558434F4F52443129293B0A2020746D707661725F3131203D2028696E5665635F3132202A20696E766572736573717274286D61782028302E3030312C200A20202020646F742028696E5665635F31322C20696E5665635F3132290A20202929293B0A20206D656469756D7020666C6F617420746D707661725F31333B0A2020746D707661725F3133203D20636C616D702028646F742028746D707661725F342C20746D707661725F3131292C20302E302C20312E30293B0A20206D656469756D7020666C6F617420746D707661725F31343B0A2020746D707661725F3134203D2028312E30202D205F476C6F7373696E657373293B0A20206D656469756D7020666C6F617420746D707661725F31353B0A2020746D707661725F3135203D2028746D707661725F3134202A20746D707661725F3134293B0A202073706563756C61725465726D5F3130203D202828746D707661725F3135202F20280A20202020286D61782028302E33322C20636C616D702028646F742028746D707661725F372C20746D707661725F3131292C20302E302C20312E302929202A2028312E35202B20746D707661725F313529290A2020202A200A20202020282828746D707661725F3133202A20746D707661725F313329202A202828746D707661725F3135202A20746D707661725F313529202D20312E302929202B20312E3030303031290A20202929202D20302E30303031293B0A20206D656469756D7020666C6F617420746D707661725F31363B0A2020746D707661725F3136203D20636C616D70202873706563756C61725465726D5F31302C20302E302C203130302E30293B0A202073706563756C61725465726D5F3130203D20746D707661725F31363B0A20206D656469756D70207665633420746D707661725F31373B0A2020746D707661725F31372E77203D20312E303B0A2020746D707661725F31372E78797A203D202828280A2020202028746D707661725F33202A2028302E37373930383337202D20285F4D6574616C6C6963202A20302E373739303833372929290A2020202B200A2020202028746D707661725F3136202A206D697820287665633328302E323230393136332C20302E323230393136332C20302E32323039313633292C20746D707661725F332C2076656333285F4D6574616C6C69632929290A202029202A20746D707661725F3929202A20636C616D702028646F742028746D707661725F342C20746D707661725F37292C20302E302C20312E3029293B0A20206D656469756D70207665633420786C61745F7661726F75747075745F31383B0A2020786C61745F7661726F75747075745F31382E78797A203D20746D707661725F31372E78797A3B0A2020786C61745F7661726F75747075745F31382E77203D20312E303B0A2020676C5F46726167446174615B305D203D20786C61745F7661726F75747075745F31383B0A7D0A0A0A23656E6469660A001B000000000000000100000000000000000000000000000000000000324F040C0500000000000000000000000000000000000000030000000400000053504F540D000000534841444F57535F44455054480000000C000000534841444F57535F534F46540000000000000000000000000100000000000000000000000000000000000000324F040C05000000160000000300000000000000000000000100000012000000444952454354494F4E414C5F434F4F4B49450000E80F00002376657273696F6E203130300A0A236966646566205645525445580A6174747269627574652076656334205F676C65735665727465783B0A6174747269627574652076656333205F676C65734E6F726D616C3B0A6174747269627574652076656334205F676C65734D756C7469546578436F6F7264303B0A6174747269627574652076656334205F676C65734D756C7469546578436F6F7264313B0A756E69666F726D2068696768702076656333205F576F726C64537061636543616D657261506F733B0A756E69666F726D206D656469756D702076656334205F576F726C6453706163654C69676874506F73303B0A756E69666F726D206869676870206D61743420756E6974795F4F626A656374546F576F726C643B0A756E69666F726D206869676870206D61743420756E6974795F576F726C64546F4F626A6563743B0A756E69666F726D206869676870206D61743420756E6974795F4D617472697856503B0A756E69666F726D2068696768702076656334205F4D61696E5465785F53543B0A756E69666F726D2068696768702076656334205F44657461696C416C6265646F4D61705F53543B0A756E69666F726D206D656469756D7020666C6F6174205F55565365633B0A76617279696E67206869676870207665633420786C765F544558434F4F5244303B0A76617279696E67206869676870207665633320786C765F544558434F4F5244313B0A76617279696E67206869676870207665633220786C765F544558434F4F5244323B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244333B0A76617279696E67206D656469756D70207665633320786C765F544558434F4F5244343B0A76617279696E67206D656469756D70207665633320786C765F544558434F4F5244353B0A766F6964206D61696E2028290A7B0A20206D656469756D70207665633320746D707661725F313B0A2020746D707661725F31203D205F676C65734E6F726D616C3B0A20206869676870207665633220746D707661725F323B0A2020746D707661725F32203D205F676C65734D756C7469546578436F6F7264302E78793B0A20206869676870207665633220746D707661725F333B0A2020746D707661725F33203D205F676C65734D756C7469546578436F6F7264312E78793B0A20206D656469756D702076656333206E6F726D616C576F726C645F343B0A20206D656469756D702076656333206579655665635F353B0A20206D656469756D702076656333206C696768744469725F363B0A20206869676870207665633220746D707661725F373B0A20206D656469756D70207665633420746D707661725F383B0A20206869676870207665633420746D707661725F393B0A2020746D707661725F39203D2028756E6974795F4F626A656374546F576F726C64202A205F676C6573566572746578293B0A20206869676870207665633420746D707661725F31303B0A20206869676870207665633420746D707661725F31313B0A2020746D707661725F31312E77203D20312E303B0A2020746D707661725F31312E78797A203D205F676C65735665727465782E78797A3B0A2020746D707661725F3130203D2028756E6974795F4D61747269785650202A2028756E6974795F4F626A656374546F576F726C64202A20746D707661725F313129293B0A20206869676870207665633420746578636F6F72645F31323B0A2020746578636F6F72645F31322E7879203D2028285F676C65734D756C7469546578436F6F7264302E7879202A205F4D61696E5465785F53542E787929202B205F4D61696E5465785F53542E7A77293B0A20206869676870207665633220746D707661725F31333B0A202069662028285F5556536563203D3D20302E302929207B0A20202020746D707661725F3133203D20746D707661725F323B0A20207D20656C7365207B0A20202020746D707661725F3133203D20746D707661725F333B0A20207D3B0A2020746578636F6F72645F31322E7A77203D202828746D707661725F3133202A205F44657461696C416C6265646F4D61705F53542E787929202B205F44657461696C416C6265646F4D61705F53542E7A77293B0A20206869676870207665633320746D707661725F31343B0A2020746D707661725F3134203D20285F576F726C6453706163654C69676874506F73302E78797A202D2028746D707661725F392E78797A202A205F576F726C6453706163654C69676874506F73302E7729293B0A20206C696768744469725F36203D20746D707661725F31343B0A20206869676870207665633320746D707661725F31353B0A2020746D707661725F3135203D206E6F726D616C697A652828746D707661725F392E78797A202D205F576F726C64537061636543616D657261506F7329293B0A20206579655665635F35203D20746D707661725F31353B0A202068696768702076656333206E6F726D5F31363B0A20206E6F726D5F3136203D20746D707661725F313B0A20206869676870206D61743320746D707661725F31373B0A2020746D707661725F31375B305D203D20756E6974795F576F726C64546F4F626A6563745B305D2E78797A3B0A2020746D707661725F31375B315D203D20756E6974795F576F726C64546F4F626A6563745B315D2E78797A3B0A2020746D707661725F31375B325D203D20756E6974795F576F726C64546F4F626A6563745B325D2E78797A3B0A20206869676870207665633320746D707661725F31383B0A2020746D707661725F3138203D206E6F726D616C697A6528286E6F726D5F3136202A20746D707661725F313729293B0A20206E6F726D616C576F726C645F34203D20746D707661725F31383B0A2020746D707661725F382E797A77203D20286579655665635F35202D2028322E30202A20280A20202020646F7420286E6F726D616C576F726C645F342C206579655665635F35290A2020202A206E6F726D616C576F726C645F342929293B0A2020676C5F506F736974696F6E203D20746D707661725F31303B0A2020786C765F544558434F4F524430203D20746578636F6F72645F31323B0A2020786C765F544558434F4F524431203D20746D707661725F392E78797A3B0A2020786C765F544558434F4F524432203D20746D707661725F373B0A2020786C765F544558434F4F524433203D20746D707661725F383B0A2020786C765F544558434F4F524434203D206C696768744469725F363B0A2020786C765F544558434F4F524435203D206E6F726D616C576F726C645F343B0A7D0A0A0A23656E6469660A23696664656620465241474D454E540A756E69666F726D206C6F77702076656334205F4C69676874436F6C6F72303B0A756E69666F726D2073616D706C6572324420756E6974795F4E4878526F7567686E6573733B0A756E69666F726D206D656469756D702076656334205F436F6C6F723B0A756E69666F726D2073616D706C65723244205F4D61696E5465783B0A756E69666F726D206D656469756D7020666C6F6174205F4D6574616C6C69633B0A756E69666F726D206D656469756D7020666C6F6174205F476C6F7373696E6573733B0A756E69666F726D2073616D706C65723244205F4C6967687454657874757265303B0A756E69666F726D206869676870206D61743420756E6974795F576F726C64546F4C696768743B0A76617279696E67206869676870207665633420786C765F544558434F4F5244303B0A76617279696E67206869676870207665633320786C765F544558434F4F5244313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244333B0A76617279696E67206D656469756D70207665633320786C765F544558434F4F5244343B0A76617279696E67206D656469756D70207665633320786C765F544558434F4F5244353B0A766F6964206D61696E2028290A7B0A202068696768702076656332206C69676874436F6F72645F313B0A20206D656469756D70207665633320635F323B0A20206C6F7770207665633420746D707661725F333B0A2020746D707661725F33203D2074657874757265324420285F4D61696E5465782C20786C765F544558434F4F5244302E7879293B0A20206D656469756D70207665633320746D707661725F343B0A2020746D707661725F34203D20285F436F6C6F722E78797A202A20746D707661725F332E78797A293B0A20206D656469756D7020666C6F617420746D707661725F353B0A2020746D707661725F35203D20646F742028786C765F544558434F4F5244332E797A772C20786C765F544558434F4F524434293B0A20206D656469756D70207665633220746D707661725F363B0A2020746D707661725F362E78203D202828746D707661725F35202A20746D707661725F3529202A2028746D707661725F35202A20746D707661725F3529293B0A2020746D707661725F362E79203D2028312E30202D205F476C6F7373696E657373293B0A20206C6F7770207665633420746D707661725F373B0A2020746D707661725F37203D207465787475726532442028756E6974795F4E4878526F7567686E6573732C20746D707661725F36293B0A2020635F32203D20282828746D707661725F34202A200A2020202028302E37373930383337202D20285F4D6574616C6C6963202A20302E3737393038333729290A202029202B20280A2020202028746D707661725F372E77202A2031362E30290A2020202A200A202020206D697820287665633328302E323230393136332C20302E323230393136332C20302E32323039313633292C20746D707661725F342C2076656333285F4D6574616C6C696329290A20202929202A205F4C69676874436F6C6F72302E78797A293B0A20206869676870207665633420746D707661725F383B0A2020746D707661725F382E77203D20312E303B0A2020746D707661725F382E78797A203D20786C765F544558434F4F5244313B0A20206C69676874436F6F72645F31203D2028756E6974795F576F726C64546F4C69676874202A20746D707661725F38292E78793B0A20206C6F777020666C6F617420746D707661725F393B0A2020746D707661725F39203D2074657874757265324420285F4C6967687454657874757265302C206C69676874436F6F72645F31292E773B0A2020635F32203D2028635F32202A2028746D707661725F39202A20636C616D7020280A20202020646F742028786C765F544558434F4F5244352C20786C765F544558434F4F524434290A20202C20302E302C20312E302929293B0A20206D656469756D70207665633420746D707661725F31303B0A2020746D707661725F31302E77203D20312E303B0A2020746D707661725F31302E78797A203D20635F323B0A20206D656469756D70207665633420786C61745F7661726F75747075745F31313B0A2020786C61745F7661726F75747075745F31312E78797A203D20746D707661725F31302E78797A3B0A2020786C61745F7661726F75747075745F31312E77203D20312E303B0A2020676C5F46726167446174615B305D203D20786C61745F7661726F75747075745F31313B0A7D0A0A0A23656E6469660A1B000000000000000100000000000000000000000000000000000000324F040C0500000033000000040000000000000000000000010000000B000000444952454354494F4E414C002F1B00002376657273696F6E203130300A0A236966646566205645525445580A6174747269627574652076656334205F676C65735665727465783B0A6174747269627574652076656333205F676C65734E6F726D616C3B0A6174747269627574652076656334205F676C65734D756C7469546578436F6F7264303B0A6174747269627574652076656334205F676C65734D756C7469546578436F6F7264313B0A756E69666F726D2068696768702076656333205F576F726C64537061636543616D657261506F733B0A756E69666F726D206D656469756D70207665633420756E6974795F534841723B0A756E69666F726D206D656469756D70207665633420756E6974795F534841673B0A756E69666F726D206D656469756D70207665633420756E6974795F534841623B0A756E69666F726D206D656469756D70207665633420756E6974795F534842723B0A756E69666F726D206D656469756D70207665633420756E6974795F534842673B0A756E69666F726D206D656469756D70207665633420756E6974795F534842623B0A756E69666F726D206D656469756D70207665633420756E6974795F5348433B0A756E69666F726D206869676870206D61743420756E6974795F4F626A656374546F576F726C643B0A756E69666F726D206869676870206D61743420756E6974795F576F726C64546F4F626A6563743B0A756E69666F726D206869676870206D61743420756E6974795F4D617472697856503B0A756E69666F726D2068696768702076656334205F4D61696E5465785F53543B0A756E69666F726D2068696768702076656334205F44657461696C416C6265646F4D61705F53543B0A756E69666F726D206D656469756D7020666C6F6174205F55565365633B0A76617279696E67206869676870207665633420786C765F544558434F4F5244303B0A76617279696E67206D656469756D70207665633320786C765F544558434F4F5244313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244323B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244325F313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244325F323B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244353B0A76617279696E67206869676870207665633220786C765F544558434F4F5244363B0A76617279696E67206869676870207665633320786C765F544558434F4F5244383B0A766F6964206D61696E2028290A7B0A20206D656469756D70207665633320746D707661725F313B0A2020746D707661725F31203D205F676C65734E6F726D616C3B0A20206869676870207665633220746D707661725F323B0A2020746D707661725F32203D205F676C65734D756C7469546578436F6F7264302E78793B0A20206869676870207665633220746D707661725F333B0A2020746D707661725F33203D205F676C65734D756C7469546578436F6F7264312E78793B0A20206D656469756D70207665633420746D707661725F343B0A20206D656469756D70207665633420746D707661725F353B0A20206D656469756D70207665633420746D707661725F363B0A20206869676870207665633220746D707661725F373B0A20206869676870207665633320746D707661725F383B0A20206869676870207665633420746D707661725F393B0A2020746D707661725F39203D2028756E6974795F4F626A656374546F576F726C64202A205F676C6573566572746578293B0A2020746D707661725F38203D20746D707661725F392E78797A3B0A20206869676870207665633420746D707661725F31303B0A20206869676870207665633420746D707661725F31313B0A2020746D707661725F31312E77203D20312E303B0A2020746D707661725F31312E78797A203D205F676C65735665727465782E78797A3B0A2020746D707661725F3130203D2028756E6974795F4D61747269785650202A2028756E6974795F4F626A656374546F576F726C64202A20746D707661725F313129293B0A20206869676870207665633420746578636F6F72645F31323B0A2020746578636F6F72645F31322E7879203D2028285F676C65734D756C7469546578436F6F7264302E7879202A205F4D61696E5465785F53542E787929202B205F4D61696E5465785F53542E7A77293B0A20206869676870207665633220746D707661725F31333B0A202069662028285F5556536563203D3D20302E302929207B0A20202020746D707661725F3133203D20746D707661725F323B0A20207D20656C7365207B0A20202020746D707661725F3133203D20746D707661725F333B0A20207D3B0A2020746578636F6F72645F31322E7A77203D202828746D707661725F3133202A205F44657461696C416C6265646F4D61705F53542E787929202B205F44657461696C416C6265646F4D61705F53542E7A77293B0A20206D656469756D70207665633320746D707661725F31343B0A20206869676870207665633320746D707661725F31353B0A2020746D707661725F3135203D206E6F726D616C697A652828746D707661725F392E78797A202D205F576F726C64537061636543616D657261506F7329293B0A2020746D707661725F3134203D20746D707661725F31353B0A202068696768702076656333206E6F726D5F31363B0A20206E6F726D5F3136203D20746D707661725F313B0A20206869676870206D61743320746D707661725F31373B0A2020746D707661725F31375B305D203D20756E6974795F576F726C64546F4F626A6563745B305D2E78797A3B0A2020746D707661725F31375B315D203D20756E6974795F576F726C64546F4F626A6563745B315D2E78797A3B0A2020746D707661725F31375B325D203D20756E6974795F576F726C64546F4F626A6563745B325D2E78797A3B0A20206869676870207665633320746D707661725F31383B0A2020746D707661725F3138203D206E6F726D616C697A6528286E6F726D5F3136202A20746D707661725F313729293B0A2020746D707661725F342E78797A203D207665633328302E302C20302E302C20302E30293B0A2020746D707661725F352E78797A203D207665633328302E302C20302E302C20302E30293B0A2020746D707661725F362E78797A203D20746D707661725F31383B0A20206D656469756D702076656333206E6F726D616C576F726C645F31393B0A20206E6F726D616C576F726C645F3139203D20746D707661725F31383B0A20206D656469756D70207665633420616D6269656E744F724C696768746D617055565F32303B0A2020616D6269656E744F724C696768746D617055565F32302E77203D20302E303B0A20206D656469756D70207665633420746D707661725F32313B0A2020746D707661725F32312E77203D20312E303B0A2020746D707661725F32312E78797A203D206E6F726D616C576F726C645F31393B0A20206D656469756D702076656333207265735F32323B0A20206D656469756D70207665633320785F32333B0A2020785F32332E78203D20646F742028756E6974795F534841722C20746D707661725F3231293B0A2020785F32332E79203D20646F742028756E6974795F534841672C20746D707661725F3231293B0A2020785F32332E7A203D20646F742028756E6974795F534841622C20746D707661725F3231293B0A20206D656469756D7020766563332078315F32343B0A20206D656469756D70207665633420746D707661725F32353B0A2020746D707661725F3235203D20286E6F726D616C576F726C645F31392E78797A7A202A206E6F726D616C576F726C645F31392E797A7A78293B0A202078315F32342E78203D20646F742028756E6974795F534842722C20746D707661725F3235293B0A202078315F32342E79203D20646F742028756E6974795F534842672C20746D707661725F3235293B0A202078315F32342E7A203D20646F742028756E6974795F534842622C20746D707661725F3235293B0A20207265735F3232203D2028785F3233202B202878315F3234202B2028756E6974795F5348432E78797A202A200A2020202028286E6F726D616C576F726C645F31392E78202A206E6F726D616C576F726C645F31392E7829202D20286E6F726D616C576F726C645F31392E79202A206E6F726D616C576F726C645F31392E7929290A20202929293B0A20206D656469756D70207665633320746D707661725F32363B0A2020746D707661725F3236203D206D617820282828312E303535202A200A20202020706F7720286D617820287265735F32322C207665633328302E302C20302E302C20302E3029292C207665633328302E343136363636372C20302E343136363636372C20302E3431363636363729290A202029202D20302E303535292C207665633328302E302C20302E302C20302E3029293B0A20207265735F3232203D20746D707661725F32363B0A2020616D6269656E744F724C696768746D617055565F32302E78797A203D206D617820287665633328302E302C20302E302C20302E30292C20746D707661725F3236293B0A2020676C5F506F736974696F6E203D20746D707661725F31303B0A2020786C765F544558434F4F524430203D20746578636F6F72645F31323B0A2020786C765F544558434F4F524431203D20746D707661725F31343B0A2020786C765F544558434F4F524432203D20746D707661725F343B0A2020786C765F544558434F4F5244325F31203D20746D707661725F353B0A2020786C765F544558434F4F5244325F32203D20746D707661725F363B0A2020786C765F544558434F4F524435203D20616D6269656E744F724C696768746D617055565F32303B0A2020786C765F544558434F4F524436203D20746D707661725F373B0A2020786C765F544558434F4F524438203D20746D707661725F383B0A7D0A0A0A23656E6469660A23696664656620465241474D454E540A23657874656E73696F6E20474C5F4558545F7368616465725F746578747572655F6C6F64203A20656E61626C650A6C6F7770207665633420696D706C5F6C6F775F74657874757265437562654C6F64455854286C6F77702073616D706C6572437562652073616D706C65722C206869676870207665633320636F6F72642C206D656469756D7020666C6F6174206C6F64290A7B0A23696620646566696E656428474C5F4558545F7368616465725F746578747572655F6C6F64290A0972657475726E2074657874757265437562654C6F644558542873616D706C65722C20636F6F72642C206C6F64293B0A23656C73650A0972657475726E2074657874757265437562652873616D706C65722C20636F6F72642C206C6F64293B0A23656E6469660A7D0A0A756E69666F726D206D656469756D702076656334205F576F726C6453706163654C69676874506F73303B0A756E69666F726D206C6F77702073616D706C65724375626520756E6974795F5370656343756265303B0A756E69666F726D206D656469756D70207665633420756E6974795F5370656343756265305F4844523B0A756E69666F726D206C6F77702076656334205F4C69676874436F6C6F72303B0A756E69666F726D2073616D706C6572324420756E6974795F4E4878526F7567686E6573733B0A756E69666F726D206D656469756D702076656334205F436F6C6F723B0A756E69666F726D2073616D706C65723244205F4D61696E5465783B0A756E69666F726D206D656469756D7020666C6F6174205F4D6574616C6C69633B0A756E69666F726D206D656469756D7020666C6F6174205F476C6F7373696E6573733B0A756E69666F726D2073616D706C65723244205F4F63636C7573696F6E4D61703B0A76617279696E67206869676870207665633420786C765F544558434F4F5244303B0A76617279696E67206D656469756D70207665633320786C765F544558434F4F5244313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244325F323B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244353B0A766F6964206D61696E2028290A7B0A20206D656469756D70207665633420635F313B0A20206C6F7770207665633420746D707661725F323B0A2020746D707661725F32203D2074657874757265324420285F4D61696E5465782C20786C765F544558434F4F5244302E7879293B0A20206D656469756D70207665633320746D707661725F333B0A2020746D707661725F33203D20285F436F6C6F722E78797A202A20746D707661725F322E78797A293B0A20206D656469756D70207665633320746D707661725F343B0A20206D656469756D70207665633320746D707661725F353B0A2020746D707661725F35203D206D697820287665633328302E323230393136332C20302E323230393136332C20302E32323039313633292C20746D707661725F332C2076656333285F4D6574616C6C696329293B0A20206D656469756D7020666C6F617420746D707661725F363B0A2020746D707661725F36203D2028302E37373930383337202D20285F4D6574616C6C6963202A20302E3737393038333729293B0A2020746D707661725F34203D2028746D707661725F33202A20746D707661725F36293B0A20206D656469756D70207665633320746D707661725F373B0A2020746D707661725F37203D206E6F726D616C697A6528786C765F544558434F4F5244325F322E78797A293B0A20206D656469756D70207665633320746D707661725F383B0A2020746D707661725F38203D205F4C69676874436F6C6F72302E78797A3B0A20206D656469756D7020666C6F617420746D707661725F393B0A20206C6F7770207665633420746D707661725F31303B0A2020746D707661725F3130203D2074657874757265324420285F4F63636C7573696F6E4D61702C20786C765F544558434F4F5244302E7879293B0A2020746D707661725F39203D20746D707661725F31302E793B0A20206869676870207665633420746D707661725F31313B0A2020746D707661725F3131203D20756E6974795F5370656343756265305F4844523B0A20206D656469756D70207665633320746D707661725F31323B0A20206D656469756D7020666C6F617420746D707661725F31333B0A2020746D707661725F3133203D2028312E30202D205F476C6F7373696E657373293B0A2020746D707661725F3132203D2028786C765F544558434F4F524431202D2028322E30202A20280A20202020646F742028746D707661725F372C20786C765F544558434F4F524431290A2020202A20746D707661725F372929293B0A20206D656469756D702076656334206864725F31343B0A20206864725F3134203D20746D707661725F31313B0A20206D656469756D70207665633420746D707661725F31353B0A2020746D707661725F31352E78797A203D20746D707661725F31323B0A2020746D707661725F31352E77203D202828746D707661725F3133202A2028312E37202D200A2020202028302E37202A20746D707661725F3133290A20202929202A20362E30293B0A20206C6F7770207665633420746D707661725F31363B0A2020746D707661725F3136203D20696D706C5F6C6F775F74657874757265437562654C6F644558542028756E6974795F5370656343756265302C20746D707661725F31322C20746D707661725F31352E77293B0A20206D656469756D70207665633420746D707661725F31373B0A2020746D707661725F3137203D20746D707661725F31363B0A20206D656469756D70207665633320766965774469725F31383B0A2020766965774469725F3138203D202D28786C765F544558434F4F524431293B0A20206D656469756D70207665633320636F6C6F725F31393B0A20206D656469756D70207665633220746D707661725F32303B0A2020746D707661725F32302E78203D20646F74202828766965774469725F3138202D2028322E30202A200A2020202028646F742028746D707661725F372C20766965774469725F313829202A20746D707661725F37290A202029292C205F576F726C6453706163654C69676874506F73302E78797A293B0A2020746D707661725F32302E79203D2028312E30202D20636C616D702028646F742028746D707661725F372C20766965774469725F3138292C20302E302C20312E3029293B0A20206D656469756D70207665633220746D707661725F32313B0A2020746D707661725F3231203D202828746D707661725F3230202A20746D707661725F323029202A2028746D707661725F3230202A20746D707661725F323029293B0A20206D656469756D70207665633220746D707661725F32323B0A2020746D707661725F32322E78203D20746D707661725F32312E783B0A2020746D707661725F32322E79203D20746D707661725F31333B0A20206C6F7770207665633420746D707661725F32333B0A2020746D707661725F3233203D207465787475726532442028756E6974795F4E4878526F7567686E6573732C20746D707661725F3232293B0A2020636F6C6F725F3139203D202828746D707661725F34202B20280A2020202028746D707661725F32332E77202A2031362E30290A2020202A20746D707661725F352929202A2028746D707661725F38202A20636C616D7020280A20202020646F742028746D707661725F372C205F576F726C6453706163654C69676874506F73302E78797A290A20202C20302E302C20312E302929293B0A2020636F6C6F725F3139203D2028636F6C6F725F3139202B2028280A2020202028786C765F544558434F4F5244352E78797A202A20746D707661725F39290A2020202A20746D707661725F3429202B20280A202020202828286864725F31342E78202A20280A202020202020286864725F31342E77202A2028746D707661725F31372E77202D20312E3029290A20202020202B20312E302929202A20746D707661725F31372E78797A29202A20746D707661725F39290A2020202A200A202020206D69782028746D707661725F352C207665633328636C616D702028285F476C6F7373696E657373202B2028312E30202D20746D707661725F3629292C20302E302C20312E3029292C20746D707661725F32312E797979290A20202929293B0A20206D656469756D70207665633420746D707661725F32343B0A2020746D707661725F32342E77203D20312E303B0A2020746D707661725F32342E78797A203D20636F6C6F725F31393B0A2020635F312E77203D20746D707661725F32342E773B0A2020635F312E78797A203D20636F6C6F725F31393B0A20206D656469756D70207665633420786C61745F7661726F75747075745F32353B0A2020786C61745F7661726F75747075745F32352E78797A203D20635F312E78797A3B0A2020786C61745F7661726F75747075745F32352E77203D20312E303B0A2020676C5F46726167446174615B305D203D20786C61745F7661726F75747075745F32353B0A7D0A0A0A23656E6469660A001B000000000000000100000000000000000000000000000000000000324F040C050000001C0000000400000000000000000000000200000012000000444952454354494F4E414C5F434F4F4B494500000E000000534841444F57535F53435245454E0000FB1200002376657273696F6E203130300A0A236966646566205645525445580A6174747269627574652076656334205F676C65735665727465783B0A6174747269627574652076656333205F676C65734E6F726D616C3B0A6174747269627574652076656334205F676C65734D756C7469546578436F6F7264303B0A6174747269627574652076656334205F676C65734D756C7469546578436F6F7264313B0A756E69666F726D2068696768702076656333205F576F726C64537061636543616D657261506F733B0A756E69666F726D206D656469756D702076656334205F576F726C6453706163654C69676874506F73303B0A756E69666F726D206869676870206D61743420756E6974795F4F626A656374546F576F726C643B0A756E69666F726D206869676870206D61743420756E6974795F576F726C64546F4F626A6563743B0A756E69666F726D206869676870206D61743420756E6974795F4D617472697856503B0A756E69666F726D2068696768702076656334205F4D61696E5465785F53543B0A756E69666F726D2068696768702076656334205F44657461696C416C6265646F4D61705F53543B0A756E69666F726D206D656469756D7020666C6F6174205F55565365633B0A76617279696E67206869676870207665633420786C765F544558434F4F5244303B0A76617279696E67206869676870207665633320786C765F544558434F4F5244313B0A76617279696E67206869676870207665633220786C765F544558434F4F5244323B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244333B0A76617279696E67206D656469756D70207665633320786C765F544558434F4F5244343B0A76617279696E67206D656469756D70207665633320786C765F544558434F4F5244353B0A766F6964206D61696E2028290A7B0A20206D656469756D70207665633320746D707661725F313B0A2020746D707661725F31203D205F676C65734E6F726D616C3B0A20206869676870207665633220746D707661725F323B0A2020746D707661725F32203D205F676C65734D756C7469546578436F6F7264302E78793B0A20206869676870207665633220746D707661725F333B0A2020746D707661725F33203D205F676C65734D756C7469546578436F6F7264312E78793B0A20206D656469756D702076656333206E6F726D616C576F726C645F343B0A20206D656469756D702076656333206579655665635F353B0A20206D656469756D702076656333206C696768744469725F363B0A20206869676870207665633220746D707661725F373B0A20206D656469756D70207665633420746D707661725F383B0A20206869676870207665633420746D707661725F393B0A2020746D707661725F39203D2028756E6974795F4F626A656374546F576F726C64202A205F676C6573566572746578293B0A20206869676870207665633420746D707661725F31303B0A20206869676870207665633420746D707661725F31313B0A2020746D707661725F31312E77203D20312E303B0A2020746D707661725F31312E78797A203D205F676C65735665727465782E78797A3B0A2020746D707661725F3130203D2028756E6974795F4D61747269785650202A2028756E6974795F4F626A656374546F576F726C64202A20746D707661725F313129293B0A20206869676870207665633420746578636F6F72645F31323B0A2020746578636F6F72645F31322E7879203D2028285F676C65734D756C7469546578436F6F7264302E7879202A205F4D61696E5465785F53542E787929202B205F4D61696E5465785F53542E7A77293B0A20206869676870207665633220746D707661725F31333B0A202069662028285F5556536563203D3D20302E302929207B0A20202020746D707661725F3133203D20746D707661725F323B0A20207D20656C7365207B0A20202020746D707661725F3133203D20746D707661725F333B0A20207D3B0A2020746578636F6F72645F31322E7A77203D202828746D707661725F3133202A205F44657461696C416C6265646F4D61705F53542E787929202B205F44657461696C416C6265646F4D61705F53542E7A77293B0A20206869676870207665633320746D707661725F31343B0A2020746D707661725F3134203D20285F576F726C6453706163654C69676874506F73302E78797A202D2028746D707661725F392E78797A202A205F576F726C6453706163654C69676874506F73302E7729293B0A20206C696768744469725F36203D20746D707661725F31343B0A20206869676870207665633320746D707661725F31353B0A2020746D707661725F3135203D206E6F726D616C697A652828746D707661725F392E78797A202D205F576F726C64537061636543616D657261506F7329293B0A20206579655665635F35203D20746D707661725F31353B0A202068696768702076656333206E6F726D5F31363B0A20206E6F726D5F3136203D20746D707661725F313B0A20206869676870206D61743320746D707661725F31373B0A2020746D707661725F31375B305D203D20756E6974795F576F726C64546F4F626A6563745B305D2E78797A3B0A2020746D707661725F31375B315D203D20756E6974795F576F726C64546F4F626A6563745B315D2E78797A3B0A2020746D707661725F31375B325D203D20756E6974795F576F726C64546F4F626A6563745B325D2E78797A3B0A20206869676870207665633320746D707661725F31383B0A2020746D707661725F3138203D206E6F726D616C697A6528286E6F726D5F3136202A20746D707661725F313729293B0A20206E6F726D616C576F726C645F34203D20746D707661725F31383B0A2020746D707661725F382E797A77203D20286579655665635F35202D2028322E30202A20280A20202020646F7420286E6F726D616C576F726C645F342C206579655665635F35290A2020202A206E6F726D616C576F726C645F342929293B0A2020676C5F506F736974696F6E203D20746D707661725F31303B0A2020786C765F544558434F4F524430203D20746578636F6F72645F31323B0A2020786C765F544558434F4F524431203D20746D707661725F392E78797A3B0A2020786C765F544558434F4F524432203D20746D707661725F373B0A2020786C765F544558434F4F524433203D20746D707661725F383B0A2020786C765F544558434F4F524434203D206C696768744469725F363B0A2020786C765F544558434F4F524435203D206E6F726D616C576F726C645F343B0A7D0A0A0A23656E6469660A23696664656620465241474D454E540A756E69666F726D206869676870206D61743420756E6974795F576F726C64546F536861646F775B345D3B0A756E69666F726D206D656469756D702076656334205F4C69676874536861646F77446174613B0A756E69666F726D206C6F77702076656334205F4C69676874436F6C6F72303B0A756E69666F726D2073616D706C6572324420756E6974795F4E4878526F7567686E6573733B0A756E69666F726D206D656469756D702076656334205F436F6C6F723B0A756E69666F726D2073616D706C65723244205F4D61696E5465783B0A756E69666F726D206D656469756D7020666C6F6174205F4D6574616C6C69633B0A756E69666F726D206D656469756D7020666C6F6174205F476C6F7373696E6573733B0A756E69666F726D2068696768702073616D706C65723244205F536861646F774D6170546578747572653B0A756E69666F726D2073616D706C65723244205F4C6967687454657874757265303B0A756E69666F726D206869676870206D61743420756E6974795F576F726C64546F4C696768743B0A76617279696E67206869676870207665633420786C765F544558434F4F5244303B0A76617279696E67206869676870207665633320786C765F544558434F4F5244313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244333B0A76617279696E67206D656469756D70207665633320786C765F544558434F4F5244343B0A76617279696E67206D656469756D70207665633320786C765F544558434F4F5244353B0A766F6964206D61696E2028290A7B0A20206C6F777020666C6F617420736861646F775F313B0A202068696768702076656332206C69676874436F6F72645F323B0A20206D656469756D70207665633320635F333B0A20206C6F7770207665633420746D707661725F343B0A2020746D707661725F34203D2074657874757265324420285F4D61696E5465782C20786C765F544558434F4F5244302E7879293B0A20206D656469756D70207665633320746D707661725F353B0A2020746D707661725F35203D20285F436F6C6F722E78797A202A20746D707661725F342E78797A293B0A20206D656469756D7020666C6F617420746D707661725F363B0A2020746D707661725F36203D20646F742028786C765F544558434F4F5244332E797A772C20786C765F544558434F4F524434293B0A20206D656469756D70207665633220746D707661725F373B0A2020746D707661725F372E78203D202828746D707661725F36202A20746D707661725F3629202A2028746D707661725F36202A20746D707661725F3629293B0A2020746D707661725F372E79203D2028312E30202D205F476C6F7373696E657373293B0A20206C6F7770207665633420746D707661725F383B0A2020746D707661725F38203D207465787475726532442028756E6974795F4E4878526F7567686E6573732C20746D707661725F37293B0A2020635F33203D20282828746D707661725F35202A200A2020202028302E37373930383337202D20285F4D6574616C6C6963202A20302E3737393038333729290A202029202B20280A2020202028746D707661725F382E77202A2031362E30290A2020202A200A202020206D697820287665633328302E323230393136332C20302E323230393136332C20302E32323039313633292C20746D707661725F352C2076656333285F4D6574616C6C696329290A20202929202A205F4C69676874436F6C6F72302E78797A293B0A20206869676870207665633420746D707661725F393B0A2020746D707661725F392E77203D20312E303B0A2020746D707661725F392E78797A203D20786C765F544558434F4F5244313B0A20206C69676874436F6F72645F32203D2028756E6974795F576F726C64546F4C69676874202A20746D707661725F39292E78793B0A20206D656469756D7020666C6F6174207265616C74696D65536861646F77417474656E756174696F6E5F31303B0A20206869676870207665633420746D707661725F31313B0A2020746D707661725F31312E77203D20312E303B0A2020746D707661725F31312E78797A203D20786C765F544558434F4F5244313B0A20206C6F777020666C6F617420746D707661725F31323B0A20206869676870207665633420736861646F77436F6F72645F31333B0A2020736861646F77436F6F72645F3133203D2028756E6974795F576F726C64546F536861646F775B305D202A20746D707661725F3131293B0A2020686967687020666C6F6174206C69676874536861646F7744617461585F31343B0A20206D656469756D7020666C6F617420746D707661725F31353B0A2020746D707661725F3135203D205F4C69676874536861646F77446174612E783B0A20206C69676874536861646F7744617461585F3134203D20746D707661725F31353B0A2020686967687020666C6F617420746D707661725F31363B0A2020746D707661725F3136203D206D61782028666C6F6174282874657874757265324420285F536861646F774D6170546578747572652C20736861646F77436F6F72645F31332E7879292E78203E20736861646F77436F6F72645F31332E7A29292C206C69676874536861646F7744617461585F3134293B0A2020746D707661725F3132203D20746D707661725F31363B0A20207265616C74696D65536861646F77417474656E756174696F6E5F3130203D20746D707661725F31323B0A2020736861646F775F31203D207265616C74696D65536861646F77417474656E756174696F6E5F31303B0A20206C6F777020666C6F617420746D707661725F31373B0A2020746D707661725F3137203D202874657874757265324420285F4C6967687454657874757265302C206C69676874436F6F72645F32292E77202A20736861646F775F31293B0A2020635F33203D2028635F33202A2028746D707661725F3137202A20636C616D7020280A20202020646F742028786C765F544558434F4F5244352C20786C765F544558434F4F524434290A20202C20302E302C20312E302929293B0A20206D656469756D70207665633420746D707661725F31383B0A2020746D707661725F31382E77203D20312E303B0A2020746D707661725F31382E78797A203D20635F333B0A20206D656469756D70207665633420786C61745F7661726F75747075745F31393B0A2020786C61745F7661726F75747075745F31392E78797A203D20746D707661725F31382E78797A3B0A2020786C61745F7661726F75747075745F31392E77203D20312E303B0A2020676C5F46726167446174615B305D203D20786C61745F7661726F75747075745F31393B0A7D0A0A0A23656E6469660A001B000000000000000100000000000000000000000000000000000000324F040C0500000009000000000000000000000000000000010000000C000000534841444F57535F43554245CE0300002376657273696F6E203130300A0A236966646566205645525445580A6174747269627574652076656334205F676C65735665727465783B0A756E69666F726D2068696768702076656334205F4C69676874506F736974696F6E52616E67653B0A756E69666F726D206869676870206D61743420756E6974795F4F626A656374546F576F726C643B0A756E69666F726D206869676870206D61743420756E6974795F4D617472697856503B0A76617279696E67206869676870207665633320786C765F544558434F4F5244303B0A766F6964206D61696E2028290A7B0A20206869676870207665633420746D707661725F313B0A2020746D707661725F312E77203D20312E303B0A2020746D707661725F312E78797A203D205F676C65735665727465782E78797A3B0A2020786C765F544558434F4F524430203D202828756E6974795F4F626A656374546F576F726C64202A205F676C6573566572746578292E78797A202D205F4C69676874506F736974696F6E52616E67652E78797A293B0A2020676C5F506F736974696F6E203D2028756E6974795F4D61747269785650202A2028756E6974795F4F626A656374546F576F726C64202A20746D707661725F3129293B0A7D0A0A0A23656E6469660A23696664656620465241474D454E540A756E69666F726D2068696768702076656334205F4C69676874506F736974696F6E52616E67653B0A756E69666F726D206869676870207665633420756E6974795F4C69676874536861646F77426961733B0A76617279696E67206869676870207665633320786C765F544558434F4F5244303B0A766F6964206D61696E2028290A7B0A20206D656469756D70207665633420746D707661725F313B0A20206869676870207665633420656E635F323B0A20206869676870207665633420746D707661725F333B0A2020746D707661725F33203D20667261637428287665633428312E302C203235352E302C2036353032352E302C20312E363538313338652B303729202A206D696E20280A2020202028287371727428646F742028786C765F544558434F4F5244302C20786C765F544558434F4F5244302929202B20756E6974795F4C69676874536861646F77426961732E7829202A205F4C69676874506F736974696F6E52616E67652E77290A20202C20302E3939392929293B0A2020656E635F32203D2028746D707661725F33202D2028746D707661725F332E797A7777202A20302E30303339323135363929293B0A2020746D707661725F31203D20656E635F323B0A2020676C5F46726167446174615B305D203D20746D707661725F313B0A7D0A0A0A23656E6469660A000001000000000000000100000000000000000000000000000000000000324F040C0500000033000000040000000000000000000000020000000B000000444952454354494F4E414C000E0000005645525445584C494748545F4F4E0000562200002376657273696F6E203130300A0A236966646566205645525445580A6174747269627574652076656334205F676C65735665727465783B0A6174747269627574652076656333205F676C65734E6F726D616C3B0A6174747269627574652076656334205F676C65734D756C7469546578436F6F7264303B0A6174747269627574652076656334205F676C65734D756C7469546578436F6F7264313B0A756E69666F726D2068696768702076656333205F576F726C64537061636543616D657261506F733B0A756E69666F726D206869676870207665633420756E6974795F344C69676874506F7358303B0A756E69666F726D206869676870207665633420756E6974795F344C69676874506F7359303B0A756E69666F726D206869676870207665633420756E6974795F344C69676874506F735A303B0A756E69666F726D206D656469756D70207665633420756E6974795F344C69676874417474656E303B0A756E69666F726D206D656469756D70207665633420756E6974795F4C69676874436F6C6F725B385D3B0A756E69666F726D206D656469756D70207665633420756E6974795F534841723B0A756E69666F726D206D656469756D70207665633420756E6974795F534841673B0A756E69666F726D206D656469756D70207665633420756E6974795F534841623B0A756E69666F726D206D656469756D70207665633420756E6974795F534842723B0A756E69666F726D206D656469756D70207665633420756E6974795F534842673B0A756E69666F726D206D656469756D70207665633420756E6974795F534842623B0A756E69666F726D206D656469756D70207665633420756E6974795F5348433B0A756E69666F726D206869676870206D61743420756E6974795F4F626A656374546F576F726C643B0A756E69666F726D206869676870206D61743420756E6974795F576F726C64546F4F626A6563743B0A756E69666F726D206869676870206D61743420756E6974795F4D617472697856503B0A756E69666F726D2068696768702076656334205F4D61696E5465785F53543B0A756E69666F726D2068696768702076656334205F44657461696C416C6265646F4D61705F53543B0A756E69666F726D206D656469756D7020666C6F6174205F55565365633B0A76617279696E67206869676870207665633420786C765F544558434F4F5244303B0A76617279696E67206D656469756D70207665633320786C765F544558434F4F5244313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244323B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244325F313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244325F323B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244353B0A76617279696E67206869676870207665633220786C765F544558434F4F5244363B0A76617279696E67206869676870207665633320786C765F544558434F4F5244383B0A766F6964206D61696E2028290A7B0A20206D656469756D70207665633320746D707661725F313B0A2020746D707661725F31203D205F676C65734E6F726D616C3B0A20206869676870207665633220746D707661725F323B0A2020746D707661725F32203D205F676C65734D756C7469546578436F6F7264302E78793B0A20206869676870207665633220746D707661725F333B0A2020746D707661725F33203D205F676C65734D756C7469546578436F6F7264312E78793B0A20206D656469756D70207665633420746D707661725F343B0A20206D656469756D70207665633420746D707661725F353B0A20206D656469756D70207665633420746D707661725F363B0A20206869676870207665633220746D707661725F373B0A20206869676870207665633320746D707661725F383B0A20206869676870207665633420746D707661725F393B0A2020746D707661725F39203D2028756E6974795F4F626A656374546F576F726C64202A205F676C6573566572746578293B0A2020746D707661725F38203D20746D707661725F392E78797A3B0A20206869676870207665633420746D707661725F31303B0A20206869676870207665633420746D707661725F31313B0A2020746D707661725F31312E77203D20312E303B0A2020746D707661725F31312E78797A203D205F676C65735665727465782E78797A3B0A2020746D707661725F3130203D2028756E6974795F4D61747269785650202A2028756E6974795F4F626A656374546F576F726C64202A20746D707661725F313129293B0A20206869676870207665633420746578636F6F72645F31323B0A2020746578636F6F72645F31322E7879203D2028285F676C65734D756C7469546578436F6F7264302E7879202A205F4D61696E5465785F53542E787929202B205F4D61696E5465785F53542E7A77293B0A20206869676870207665633220746D707661725F31333B0A202069662028285F5556536563203D3D20302E302929207B0A20202020746D707661725F3133203D20746D707661725F323B0A20207D20656C7365207B0A20202020746D707661725F3133203D20746D707661725F333B0A20207D3B0A2020746578636F6F72645F31322E7A77203D202828746D707661725F3133202A205F44657461696C416C6265646F4D61705F53542E787929202B205F44657461696C416C6265646F4D61705F53542E7A77293B0A20206D656469756D70207665633320746D707661725F31343B0A20206869676870207665633320746D707661725F31353B0A2020746D707661725F3135203D206E6F726D616C697A652828746D707661725F392E78797A202D205F576F726C64537061636543616D657261506F7329293B0A2020746D707661725F3134203D20746D707661725F31353B0A202068696768702076656333206E6F726D5F31363B0A20206E6F726D5F3136203D20746D707661725F313B0A20206869676870206D61743320746D707661725F31373B0A2020746D707661725F31375B305D203D20756E6974795F576F726C64546F4F626A6563745B305D2E78797A3B0A2020746D707661725F31375B315D203D20756E6974795F576F726C64546F4F626A6563745B315D2E78797A3B0A2020746D707661725F31375B325D203D20756E6974795F576F726C64546F4F626A6563745B325D2E78797A3B0A20206869676870207665633320746D707661725F31383B0A2020746D707661725F3138203D206E6F726D616C697A6528286E6F726D5F3136202A20746D707661725F313729293B0A2020746D707661725F342E78797A203D207665633328302E302C20302E302C20302E30293B0A2020746D707661725F352E78797A203D207665633328302E302C20302E302C20302E30293B0A2020746D707661725F362E78797A203D20746D707661725F31383B0A20206D656469756D702076656333206E6F726D616C576F726C645F31393B0A20206E6F726D616C576F726C645F3139203D20746D707661725F31383B0A20206D656469756D70207665633420616D6269656E744F724C696768746D617055565F32303B0A2020616D6269656E744F724C696768746D617055565F32302E77203D20302E303B0A202068696768702076656333206C69676874436F6C6F72305F32313B0A20206C69676874436F6C6F72305F3231203D20756E6974795F4C69676874436F6C6F725B305D2E78797A3B0A202068696768702076656333206C69676874436F6C6F72315F32323B0A20206C69676874436F6C6F72315F3232203D20756E6974795F4C69676874436F6C6F725B315D2E78797A3B0A202068696768702076656333206C69676874436F6C6F72325F32333B0A20206C69676874436F6C6F72325F3233203D20756E6974795F4C69676874436F6C6F725B325D2E78797A3B0A202068696768702076656333206C69676874436F6C6F72335F32343B0A20206C69676874436F6C6F72335F3234203D20756E6974795F4C69676874436F6C6F725B335D2E78797A3B0A202068696768702076656334206C69676874417474656E53715F32353B0A20206C69676874417474656E53715F3235203D20756E6974795F344C69676874417474656E303B0A202068696768702076656333206E6F726D616C5F32363B0A20206E6F726D616C5F3236203D206E6F726D616C576F726C645F31393B0A20206869676870207665633320636F6C5F32373B0A202068696768702076656334206E646F746C5F32383B0A202068696768702076656334206C656E67746853715F32393B0A20206869676870207665633420746D707661725F33303B0A2020746D707661725F3330203D2028756E6974795F344C69676874506F735830202D20746D707661725F392E78293B0A20206869676870207665633420746D707661725F33313B0A2020746D707661725F3331203D2028756E6974795F344C69676874506F735930202D20746D707661725F392E79293B0A20206869676870207665633420746D707661725F33323B0A2020746D707661725F3332203D2028756E6974795F344C69676874506F735A30202D20746D707661725F392E7A293B0A20206C656E67746853715F3239203D2028746D707661725F3330202A20746D707661725F3330293B0A20206C656E67746853715F3239203D20286C656E67746853715F3239202B2028746D707661725F3331202A20746D707661725F333129293B0A20206C656E67746853715F3239203D20286C656E67746853715F3239202B2028746D707661725F3332202A20746D707661725F333229293B0A20206869676870207665633420746D707661725F33333B0A2020746D707661725F3333203D206D617820286C656E67746853715F32392C20766563342831652D30362C2031652D30362C2031652D30362C2031652D303629293B0A20206C656E67746853715F3239203D20746D707661725F33333B0A20206E646F746C5F3238203D2028746D707661725F3330202A206E6F726D616C5F32362E78293B0A20206E646F746C5F3238203D20286E646F746C5F3238202B2028746D707661725F3331202A206E6F726D616C5F32362E7929293B0A20206E646F746C5F3238203D20286E646F746C5F3238202B2028746D707661725F3332202A206E6F726D616C5F32362E7A29293B0A20206869676870207665633420746D707661725F33343B0A2020746D707661725F3334203D206D617820287665633428302E302C20302E302C20302E302C20302E30292C20286E646F746C5F3238202A20696E76657273657371727428746D707661725F33332929293B0A20206E646F746C5F3238203D20746D707661725F33343B0A20206869676870207665633420746D707661725F33353B0A2020746D707661725F3335203D2028746D707661725F3334202A2028312E302F2828312E30202B200A2020202028746D707661725F3333202A206C69676874417474656E53715F3235290A2020292929293B0A2020636F6C5F3237203D20286C69676874436F6C6F72305F3231202A20746D707661725F33352E78293B0A2020636F6C5F3237203D2028636F6C5F3237202B20286C69676874436F6C6F72315F3232202A20746D707661725F33352E7929293B0A2020636F6C5F3237203D2028636F6C5F3237202B20286C69676874436F6C6F72325F3233202A20746D707661725F33352E7A29293B0A2020636F6C5F3237203D2028636F6C5F3237202B20286C69676874436F6C6F72335F3234202A20746D707661725F33352E7729293B0A2020616D6269656E744F724C696768746D617055565F32302E78797A203D20636F6C5F32373B0A20206D656469756D70207665633420746D707661725F33363B0A2020746D707661725F33362E77203D20312E303B0A2020746D707661725F33362E78797A203D206E6F726D616C576F726C645F31393B0A20206D656469756D702076656333207265735F33373B0A20206D656469756D70207665633320785F33383B0A2020785F33382E78203D20646F742028756E6974795F534841722C20746D707661725F3336293B0A2020785F33382E79203D20646F742028756E6974795F534841672C20746D707661725F3336293B0A2020785F33382E7A203D20646F742028756E6974795F534841622C20746D707661725F3336293B0A20206D656469756D7020766563332078315F33393B0A20206D656469756D70207665633420746D707661725F34303B0A2020746D707661725F3430203D20286E6F726D616C576F726C645F31392E78797A7A202A206E6F726D616C576F726C645F31392E797A7A78293B0A202078315F33392E78203D20646F742028756E6974795F534842722C20746D707661725F3430293B0A202078315F33392E79203D20646F742028756E6974795F534842672C20746D707661725F3430293B0A202078315F33392E7A203D20646F742028756E6974795F534842622C20746D707661725F3430293B0A20207265735F3337203D2028785F3338202B202878315F3339202B2028756E6974795F5348432E78797A202A200A2020202028286E6F726D616C576F726C645F31392E78202A206E6F726D616C576F726C645F31392E7829202D20286E6F726D616C576F726C645F31392E79202A206E6F726D616C576F726C645F31392E7929290A20202929293B0A20206D656469756D70207665633320746D707661725F34313B0A2020746D707661725F3431203D206D617820282828312E303535202A200A20202020706F7720286D617820287265735F33372C207665633328302E302C20302E302C20302E3029292C207665633328302E343136363636372C20302E343136363636372C20302E3431363636363729290A202029202D20302E303535292C207665633328302E302C20302E302C20302E3029293B0A20207265735F3337203D20746D707661725F34313B0A2020616D6269656E744F724C696768746D617055565F32302E78797A203D2028616D6269656E744F724C696768746D617055565F32302E78797A202B206D617820287665633328302E302C20302E302C20302E30292C20746D707661725F343129293B0A2020676C5F506F736974696F6E203D20746D707661725F31303B0A2020786C765F544558434F4F524430203D20746578636F6F72645F31323B0A2020786C765F544558434F4F524431203D20746D707661725F31343B0A2020786C765F544558434F4F524432203D20746D707661725F343B0A2020786C765F544558434F4F5244325F31203D20746D707661725F353B0A2020786C765F544558434F4F5244325F32203D20746D707661725F363B0A2020786C765F544558434F4F524435203D20616D6269656E744F724C696768746D617055565F32303B0A2020786C765F544558434F4F524436203D20746D707661725F373B0A2020786C765F544558434F4F524438203D20746D707661725F383B0A7D0A0A0A23656E6469660A23696664656620465241474D454E540A23657874656E73696F6E20474C5F4558545F7368616465725F746578747572655F6C6F64203A20656E61626C650A6C6F7770207665633420696D706C5F6C6F775F74657874757265437562654C6F64455854286C6F77702073616D706C6572437562652073616D706C65722C206869676870207665633320636F6F72642C206D656469756D7020666C6F6174206C6F64290A7B0A23696620646566696E656428474C5F4558545F7368616465725F746578747572655F6C6F64290A0972657475726E2074657874757265437562654C6F644558542873616D706C65722C20636F6F72642C206C6F64293B0A23656C73650A0972657475726E2074657874757265437562652873616D706C65722C20636F6F72642C206C6F64293B0A23656E6469660A7D0A0A756E69666F726D206D656469756D702076656334205F576F726C6453706163654C69676874506F73303B0A756E69666F726D206C6F77702073616D706C65724375626520756E6974795F5370656343756265303B0A756E69666F726D206D656469756D70207665633420756E6974795F5370656343756265305F4844523B0A756E69666F726D206C6F77702076656334205F4C69676874436F6C6F72303B0A756E69666F726D2073616D706C6572324420756E6974795F4E4878526F7567686E6573733B0A756E69666F726D206D656469756D702076656334205F436F6C6F723B0A756E69666F726D2073616D706C65723244205F4D61696E5465783B0A756E69666F726D206D656469756D7020666C6F6174205F4D6574616C6C69633B0A756E69666F726D206D656469756D7020666C6F6174205F476C6F7373696E6573733B0A756E69666F726D2073616D706C65723244205F4F63636C7573696F6E4D61703B0A76617279696E67206869676870207665633420786C765F544558434F4F5244303B0A76617279696E67206D656469756D70207665633320786C765F544558434F4F5244313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244325F323B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244353B0A766F6964206D61696E2028290A7B0A20206D656469756D70207665633420635F313B0A20206C6F7770207665633420746D707661725F323B0A2020746D707661725F32203D2074657874757265324420285F4D61696E5465782C20786C765F544558434F4F5244302E7879293B0A20206D656469756D70207665633320746D707661725F333B0A2020746D707661725F33203D20285F436F6C6F722E78797A202A20746D707661725F322E78797A293B0A20206D656469756D70207665633320746D707661725F343B0A20206D656469756D70207665633320746D707661725F353B0A2020746D707661725F35203D206D697820287665633328302E323230393136332C20302E323230393136332C20302E32323039313633292C20746D707661725F332C2076656333285F4D6574616C6C696329293B0A20206D656469756D7020666C6F617420746D707661725F363B0A2020746D707661725F36203D2028302E37373930383337202D20285F4D6574616C6C6963202A20302E3737393038333729293B0A2020746D707661725F34203D2028746D707661725F33202A20746D707661725F36293B0A20206D656469756D70207665633320746D707661725F373B0A2020746D707661725F37203D206E6F726D616C697A6528786C765F544558434F4F5244325F322E78797A293B0A20206D656469756D70207665633320746D707661725F383B0A2020746D707661725F38203D205F4C69676874436F6C6F72302E78797A3B0A20206D656469756D7020666C6F617420746D707661725F393B0A20206C6F7770207665633420746D707661725F31303B0A2020746D707661725F3130203D2074657874757265324420285F4F63636C7573696F6E4D61702C20786C765F544558434F4F5244302E7879293B0A2020746D707661725F39203D20746D707661725F31302E793B0A20206869676870207665633420746D707661725F31313B0A2020746D707661725F3131203D20756E6974795F5370656343756265305F4844523B0A20206D656469756D70207665633320746D707661725F31323B0A20206D656469756D7020666C6F617420746D707661725F31333B0A2020746D707661725F3133203D2028312E30202D205F476C6F7373696E657373293B0A2020746D707661725F3132203D2028786C765F544558434F4F524431202D2028322E30202A20280A20202020646F742028746D707661725F372C20786C765F544558434F4F524431290A2020202A20746D707661725F372929293B0A20206D656469756D702076656334206864725F31343B0A20206864725F3134203D20746D707661725F31313B0A20206D656469756D70207665633420746D707661725F31353B0A2020746D707661725F31352E78797A203D20746D707661725F31323B0A2020746D707661725F31352E77203D202828746D707661725F3133202A2028312E37202D200A2020202028302E37202A20746D707661725F3133290A20202929202A20362E30293B0A20206C6F7770207665633420746D707661725F31363B0A2020746D707661725F3136203D20696D706C5F6C6F775F74657874757265437562654C6F644558542028756E6974795F5370656343756265302C20746D707661725F31322C20746D707661725F31352E77293B0A20206D656469756D70207665633420746D707661725F31373B0A2020746D707661725F3137203D20746D707661725F31363B0A20206D656469756D70207665633320766965774469725F31383B0A2020766965774469725F3138203D202D28786C765F544558434F4F524431293B0A20206D656469756D70207665633320636F6C6F725F31393B0A20206D656469756D70207665633220746D707661725F32303B0A2020746D707661725F32302E78203D20646F74202828766965774469725F3138202D2028322E30202A200A2020202028646F742028746D707661725F372C20766965774469725F313829202A20746D707661725F37290A202029292C205F576F726C6453706163654C69676874506F73302E78797A293B0A2020746D707661725F32302E79203D2028312E30202D20636C616D702028646F742028746D707661725F372C20766965774469725F3138292C20302E302C20312E3029293B0A20206D656469756D70207665633220746D707661725F32313B0A2020746D707661725F3231203D202828746D707661725F3230202A20746D707661725F323029202A2028746D707661725F3230202A20746D707661725F323029293B0A20206D656469756D70207665633220746D707661725F32323B0A2020746D707661725F32322E78203D20746D707661725F32312E783B0A2020746D707661725F32322E79203D20746D707661725F31333B0A20206C6F7770207665633420746D707661725F32333B0A2020746D707661725F3233203D207465787475726532442028756E6974795F4E4878526F7567686E6573732C20746D707661725F3232293B0A2020636F6C6F725F3139203D202828746D707661725F34202B20280A2020202028746D707661725F32332E77202A2031362E30290A2020202A20746D707661725F352929202A2028746D707661725F38202A20636C616D7020280A20202020646F742028746D707661725F372C205F576F726C6453706163654C69676874506F73302E78797A290A20202C20302E302C20312E302929293B0A2020636F6C6F725F3139203D2028636F6C6F725F3139202B2028280A2020202028786C765F544558434F4F5244352E78797A202A20746D707661725F39290A2020202A20746D707661725F3429202B20280A202020202828286864725F31342E78202A20280A202020202020286864725F31342E77202A2028746D707661725F31372E77202D20312E3029290A20202020202B20312E302929202A20746D707661725F31372E78797A29202A20746D707661725F39290A2020202A200A202020206D69782028746D707661725F352C207665633328636C616D702028285F476C6F7373696E657373202B2028312E30202D20746D707661725F3629292C20302E302C20312E3029292C20746D707661725F32312E797979290A20202929293B0A20206D656469756D70207665633420746D707661725F32343B0A2020746D707661725F32342E77203D20312E303B0A2020746D707661725F32342E78797A203D20636F6C6F725F31393B0A2020635F312E77203D20746D707661725F32342E773B0A2020635F312E78797A203D20636F6C6F725F31393B0A20206D656469756D70207665633420786C61745F7661726F75747075745F32353B0A2020786C61745F7661726F75747075745F32352E78797A203D20635F312E78797A3B0A2020786C61745F7661726F75747075745F32352E77203D20312E303B0A2020676C5F46726167446174615B305D203D20786C61745F7661726F75747075745F32353B0A7D0A0A0A23656E6469660A00001B000000000000000100000000000000000000000000000000000000324F040C0500000026000000010000000000000000000000010000000B000000444952454354494F4E414C003E1100002376657273696F6E203130300A0A236966646566205645525445580A6174747269627574652076656334205F676C65735665727465783B0A6174747269627574652076656333205F676C65734E6F726D616C3B0A6174747269627574652076656334205F676C65734D756C7469546578436F6F7264303B0A6174747269627574652076656334205F676C65734D756C7469546578436F6F7264313B0A756E69666F726D2068696768702076656333205F576F726C64537061636543616D657261506F733B0A756E69666F726D206D656469756D702076656334205F576F726C6453706163654C69676874506F73303B0A756E69666F726D206869676870206D61743420756E6974795F4F626A656374546F576F726C643B0A756E69666F726D206869676870206D61743420756E6974795F576F726C64546F4F626A6563743B0A756E69666F726D206869676870206D61743420756E6974795F4D617472697856503B0A756E69666F726D2068696768702076656334205F4D61696E5465785F53543B0A756E69666F726D2068696768702076656334205F44657461696C416C6265646F4D61705F53543B0A756E69666F726D206D656469756D7020666C6F6174205F55565365633B0A76617279696E67206869676870207665633420786C765F544558434F4F5244303B0A76617279696E67206D656469756D70207665633320786C765F544558434F4F5244313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244323B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244325F313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244325F323B0A76617279696E67206869676870207665633320786C765F544558434F4F5244353B0A76617279696E67206869676870207665633220786C765F544558434F4F5244363B0A766F6964206D61696E2028290A7B0A20206D656469756D70207665633320746D707661725F313B0A2020746D707661725F31203D205F676C65734E6F726D616C3B0A20206869676870207665633220746D707661725F323B0A2020746D707661725F32203D205F676C65734D756C7469546578436F6F7264302E78793B0A20206869676870207665633220746D707661725F333B0A2020746D707661725F33203D205F676C65734D756C7469546578436F6F7264312E78793B0A20206D656469756D70207665633420746D707661725F343B0A20206D656469756D70207665633420746D707661725F353B0A20206D656469756D70207665633420746D707661725F363B0A20206869676870207665633220746D707661725F373B0A20206869676870207665633420746D707661725F383B0A2020746D707661725F38203D2028756E6974795F4F626A656374546F576F726C64202A205F676C6573566572746578293B0A20206869676870207665633420746D707661725F393B0A20206869676870207665633420746D707661725F31303B0A2020746D707661725F31302E77203D20312E303B0A2020746D707661725F31302E78797A203D205F676C65735665727465782E78797A3B0A2020746D707661725F39203D2028756E6974795F4D61747269785650202A2028756E6974795F4F626A656374546F576F726C64202A20746D707661725F313029293B0A20206869676870207665633420746578636F6F72645F31313B0A2020746578636F6F72645F31312E7879203D2028285F676C65734D756C7469546578436F6F7264302E7879202A205F4D61696E5465785F53542E787929202B205F4D61696E5465785F53542E7A77293B0A20206869676870207665633220746D707661725F31323B0A202069662028285F5556536563203D3D20302E302929207B0A20202020746D707661725F3132203D20746D707661725F323B0A20207D20656C7365207B0A20202020746D707661725F3132203D20746D707661725F333B0A20207D3B0A2020746578636F6F72645F31312E7A77203D202828746D707661725F3132202A205F44657461696C416C6265646F4D61705F53542E787929202B205F44657461696C416C6265646F4D61705F53542E7A77293B0A20206D656469756D70207665633320746D707661725F31333B0A202068696768702076656333206E5F31343B0A20206E5F3134203D2028746D707661725F382E78797A202D205F576F726C64537061636543616D657261506F73293B0A2020746D707661725F3133203D206E5F31343B0A202068696768702076656333206E6F726D5F31353B0A20206E6F726D5F3135203D20746D707661725F313B0A20206869676870206D61743320746D707661725F31363B0A2020746D707661725F31365B305D203D20756E6974795F576F726C64546F4F626A6563745B305D2E78797A3B0A2020746D707661725F31365B315D203D20756E6974795F576F726C64546F4F626A6563745B315D2E78797A3B0A2020746D707661725F31365B325D203D20756E6974795F576F726C64546F4F626A6563745B325D2E78797A3B0A20206869676870207665633320746D707661725F31373B0A2020746D707661725F3137203D206E6F726D616C697A6528286E6F726D5F3135202A20746D707661725F313629293B0A2020746D707661725F342E78797A203D207665633328302E302C20302E302C20302E30293B0A2020746D707661725F352E78797A203D207665633328302E302C20302E302C20302E30293B0A2020746D707661725F362E78797A203D20746D707661725F31373B0A20206869676870207665633320746D707661725F31383B0A2020746D707661725F3138203D20285F576F726C6453706163654C69676874506F73302E78797A202D2028746D707661725F382E78797A202A205F576F726C6453706163654C69676874506F73302E7729293B0A2020746D707661725F342E77203D20746D707661725F31382E783B0A2020746D707661725F352E77203D20746D707661725F31382E793B0A2020746D707661725F362E77203D20746D707661725F31382E7A3B0A2020676C5F506F736974696F6E203D20746D707661725F393B0A2020786C765F544558434F4F524430203D20746578636F6F72645F31313B0A2020786C765F544558434F4F524431203D20746D707661725F31333B0A2020786C765F544558434F4F524432203D20746D707661725F343B0A2020786C765F544558434F4F5244325F31203D20746D707661725F353B0A2020786C765F544558434F4F5244325F32203D20746D707661725F363B0A2020786C765F544558434F4F524435203D20746D707661725F382E78797A3B0A2020786C765F544558434F4F524436203D20746D707661725F373B0A7D0A0A0A23656E6469660A23696664656620465241474D454E540A756E69666F726D206C6F77702076656334205F4C69676874436F6C6F72303B0A756E69666F726D206D656469756D702076656334205F436F6C6F723B0A756E69666F726D2073616D706C65723244205F4D61696E5465783B0A756E69666F726D206D656469756D7020666C6F6174205F4D6574616C6C69633B0A756E69666F726D206D656469756D7020666C6F6174205F476C6F7373696E6573733B0A76617279696E67206869676870207665633420786C765F544558434F4F5244303B0A76617279696E67206D656469756D70207665633320786C765F544558434F4F5244313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244323B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244325F313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244325F323B0A766F6964206D61696E2028290A7B0A20206C6F7770207665633420746D707661725F313B0A2020746D707661725F31203D2074657874757265324420285F4D61696E5465782C20786C765F544558434F4F5244302E7879293B0A20206D656469756D70207665633320746D707661725F323B0A2020746D707661725F32203D20285F436F6C6F722E78797A202A20746D707661725F312E78797A293B0A20206D656469756D70207665633320746D707661725F333B0A2020746D707661725F33203D206E6F726D616C697A6528786C765F544558434F4F5244325F322E78797A293B0A20206D656469756D70207665633320746D707661725F343B0A2020746D707661725F342E78203D20786C765F544558434F4F5244322E773B0A2020746D707661725F342E79203D20786C765F544558434F4F5244325F312E773B0A2020746D707661725F342E7A203D20786C765F544558434F4F5244325F322E773B0A20206D656469756D70207665633320746D707661725F353B0A2020746D707661725F35203D205F4C69676874436F6C6F72302E78797A3B0A20206D656469756D7020666C6F61742073706563756C61725465726D5F363B0A20206D656469756D70207665633320746D707661725F373B0A20206D656469756D70207665633320696E5665635F383B0A2020696E5665635F38203D2028746D707661725F34202D206E6F726D616C697A6528786C765F544558434F4F52443129293B0A2020746D707661725F37203D2028696E5665635F38202A20696E766572736573717274286D61782028302E3030312C200A20202020646F742028696E5665635F382C20696E5665635F38290A20202929293B0A20206D656469756D7020666C6F617420746D707661725F393B0A2020746D707661725F39203D20636C616D702028646F742028746D707661725F332C20746D707661725F37292C20302E302C20312E30293B0A20206D656469756D7020666C6F617420746D707661725F31303B0A2020746D707661725F3130203D2028312E30202D205F476C6F7373696E657373293B0A20206D656469756D7020666C6F617420746D707661725F31313B0A2020746D707661725F3131203D2028746D707661725F3130202A20746D707661725F3130293B0A202073706563756C61725465726D5F36203D202828746D707661725F3131202F20280A20202020286D61782028302E33322C20636C616D702028646F742028746D707661725F342C20746D707661725F37292C20302E302C20312E302929202A2028312E35202B20746D707661725F313129290A2020202A200A20202020282828746D707661725F39202A20746D707661725F3929202A202828746D707661725F3131202A20746D707661725F313129202D20312E302929202B20312E3030303031290A20202929202D20302E30303031293B0A20206D656469756D7020666C6F617420746D707661725F31323B0A2020746D707661725F3132203D20636C616D70202873706563756C61725465726D5F362C20302E302C203130302E30293B0A202073706563756C61725465726D5F36203D20746D707661725F31323B0A20206D656469756D70207665633420746D707661725F31333B0A2020746D707661725F31332E77203D20312E303B0A2020746D707661725F31332E78797A203D202828280A2020202028746D707661725F32202A2028302E37373930383337202D20285F4D6574616C6C6963202A20302E373739303833372929290A2020202B200A2020202028746D707661725F3132202A206D697820287665633328302E323230393136332C20302E323230393136332C20302E32323039313633292C20746D707661725F322C2076656333285F4D6574616C6C69632929290A202029202A20746D707661725F3529202A20636C616D702028646F742028746D707661725F332C20746D707661725F34292C20302E302C20312E3029293B0A20206D656469756D70207665633420786C61745F7661726F75747075745F31343B0A2020786C61745F7661726F75747075745F31342E78797A203D20746D707661725F31332E78797A3B0A2020786C61745F7661726F75747075745F31342E77203D20312E303B0A2020676C5F46726167446174615B305D203D20786C61745F7661726F75747075745F31343B0A7D0A0A0A23656E6469660A00001B000000000000000100000000000000000000000000000000000000324F040C050000004B000000070000000400000000000000030000000C000000504F494E545F434F4F4B49450C000000534841444F57535F435542450C000000534841444F57535F534F4654BD1E00002376657273696F6E203130300A0A236966646566205645525445580A6174747269627574652076656334205F676C65735665727465783B0A6174747269627574652076656333205F676C65734E6F726D616C3B0A6174747269627574652076656334205F676C65734D756C7469546578436F6F7264303B0A6174747269627574652076656334205F676C65734D756C7469546578436F6F7264313B0A756E69666F726D2068696768702076656333205F576F726C64537061636543616D657261506F733B0A756E69666F726D2068696768702076656334205F576F726C6453706163654C69676874506F73303B0A756E69666F726D206869676870206D61743420756E6974795F4F626A656374546F576F726C643B0A756E69666F726D206869676870206D61743420756E6974795F576F726C64546F4F626A6563743B0A756E69666F726D206869676870206D61743420756E6974795F4D617472697856503B0A756E69666F726D2068696768702076656334205F4D61696E5465785F53543B0A756E69666F726D2068696768702076656334205F44657461696C416C6265646F4D61705F53543B0A756E69666F726D206D656469756D7020666C6F6174205F55565365633B0A76617279696E67206869676870207665633420786C765F544558434F4F5244303B0A76617279696E67206D656469756D70207665633320786C765F544558434F4F5244313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244323B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244325F313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244325F323B0A76617279696E67206869676870207665633320786C765F544558434F4F5244353B0A76617279696E67206869676870207665633220786C765F544558434F4F5244363B0A766F6964206D61696E2028290A7B0A20206D656469756D70207665633320746D707661725F313B0A2020746D707661725F31203D205F676C65734E6F726D616C3B0A20206869676870207665633220746D707661725F323B0A2020746D707661725F32203D205F676C65734D756C7469546578436F6F7264302E78793B0A20206869676870207665633220746D707661725F333B0A2020746D707661725F33203D205F676C65734D756C7469546578436F6F7264312E78793B0A202068696768702076656333206C696768744469725F343B0A20206D656469756D70207665633420746D707661725F353B0A20206D656469756D70207665633420746D707661725F363B0A20206D656469756D70207665633420746D707661725F373B0A20206869676870207665633220746D707661725F383B0A20206869676870207665633420746D707661725F393B0A2020746D707661725F39203D2028756E6974795F4F626A656374546F576F726C64202A205F676C6573566572746578293B0A20206869676870207665633420746D707661725F31303B0A20206869676870207665633420746D707661725F31313B0A2020746D707661725F31312E77203D20312E303B0A2020746D707661725F31312E78797A203D205F676C65735665727465782E78797A3B0A2020746D707661725F3130203D2028756E6974795F4D61747269785650202A2028756E6974795F4F626A656374546F576F726C64202A20746D707661725F313129293B0A20206869676870207665633420746578636F6F72645F31323B0A2020746578636F6F72645F31322E7879203D2028285F676C65734D756C7469546578436F6F7264302E7879202A205F4D61696E5465785F53542E787929202B205F4D61696E5465785F53542E7A77293B0A20206869676870207665633220746D707661725F31333B0A202069662028285F5556536563203D3D20302E302929207B0A20202020746D707661725F3133203D20746D707661725F323B0A20207D20656C7365207B0A20202020746D707661725F3133203D20746D707661725F333B0A20207D3B0A2020746578636F6F72645F31322E7A77203D202828746D707661725F3133202A205F44657461696C416C6265646F4D61705F53542E787929202B205F44657461696C416C6265646F4D61705F53542E7A77293B0A20206D656469756D70207665633320746D707661725F31343B0A202068696768702076656333206E5F31353B0A20206E5F3135203D2028746D707661725F392E78797A202D205F576F726C64537061636543616D657261506F73293B0A2020746D707661725F3134203D206E5F31353B0A202068696768702076656333206E6F726D5F31363B0A20206E6F726D5F3136203D20746D707661725F313B0A20206869676870206D61743320746D707661725F31373B0A2020746D707661725F31375B305D203D20756E6974795F576F726C64546F4F626A6563745B305D2E78797A3B0A2020746D707661725F31375B315D203D20756E6974795F576F726C64546F4F626A6563745B315D2E78797A3B0A2020746D707661725F31375B325D203D20756E6974795F576F726C64546F4F626A6563745B325D2E78797A3B0A20206869676870207665633320746D707661725F31383B0A2020746D707661725F3138203D206E6F726D616C697A6528286E6F726D5F3136202A20746D707661725F313729293B0A2020746D707661725F352E78797A203D207665633328302E302C20302E302C20302E30293B0A2020746D707661725F362E78797A203D207665633328302E302C20302E302C20302E30293B0A2020746D707661725F372E78797A203D20746D707661725F31383B0A20206869676870207665633320746D707661725F31393B0A2020746D707661725F3139203D20285F576F726C6453706163654C69676874506F73302E78797A202D2028746D707661725F392E78797A202A205F576F726C6453706163654C69676874506F73302E7729293B0A20206D656469756D70207665633320746D707661725F32303B0A2020746D707661725F3230203D20746D707661725F31393B0A20206C696768744469725F34203D20746D707661725F32303B0A2020746D707661725F352E77203D206C696768744469725F342E783B0A2020746D707661725F362E77203D206C696768744469725F342E793B0A2020746D707661725F372E77203D206C696768744469725F342E7A3B0A2020676C5F506F736974696F6E203D20746D707661725F31303B0A2020786C765F544558434F4F524430203D20746578636F6F72645F31323B0A2020786C765F544558434F4F524431203D20746D707661725F31343B0A2020786C765F544558434F4F524432203D20746D707661725F353B0A2020786C765F544558434F4F5244325F31203D20746D707661725F363B0A2020786C765F544558434F4F5244325F32203D20746D707661725F373B0A2020786C765F544558434F4F524435203D20746D707661725F392E78797A3B0A2020786C765F544558434F4F524436203D20746D707661725F383B0A7D0A0A0A23656E6469660A23696664656620465241474D454E540A756E69666F726D2068696768702076656333205F576F726C64537061636543616D657261506F733B0A756E69666F726D2068696768702076656334205F4C69676874506F736974696F6E52616E67653B0A756E69666F726D206D656469756D702076656334205F4C69676874536861646F77446174613B0A756E69666F726D206869676870207665633420756E6974795F536861646F774661646543656E746572416E64547970653B0A756E69666F726D206869676870206D61743420756E6974795F4D6174726978563B0A756E69666F726D206C6F77702076656334205F4C69676874436F6C6F72303B0A756E69666F726D2068696768702073616D706C657243756265205F536861646F774D6170546578747572653B0A756E69666F726D206D656469756D702076656334205F436F6C6F723B0A756E69666F726D2073616D706C65723244205F4D61696E5465783B0A756E69666F726D206D656469756D7020666C6F6174205F4D6574616C6C69633B0A756E69666F726D206D656469756D7020666C6F6174205F476C6F7373696E6573733B0A756E69666F726D206C6F77702073616D706C657243756265205F4C6967687454657874757265303B0A756E69666F726D206869676870206D61743420756E6974795F576F726C64546F4C696768743B0A756E69666F726D2073616D706C65723244205F4C696768745465787475726542303B0A76617279696E67206869676870207665633420786C765F544558434F4F5244303B0A76617279696E67206D656469756D70207665633320786C765F544558434F4F5244313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244323B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244325F313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244325F323B0A76617279696E67206869676870207665633320786C765F544558434F4F5244353B0A766F6964206D61696E2028290A7B0A20206C6F777020666C6F617420736861646F775F313B0A20206C6F7770207665633420746D707661725F323B0A2020746D707661725F32203D2074657874757265324420285F4D61696E5465782C20786C765F544558434F4F5244302E7879293B0A20206D656469756D70207665633320746D707661725F333B0A2020746D707661725F33203D20285F436F6C6F722E78797A202A20746D707661725F322E78797A293B0A20206D656469756D70207665633320746D707661725F343B0A20206D656469756D70207665633320746D707661725F353B0A2020746D707661725F35203D206D697820287665633328302E323230393136332C20302E323230393136332C20302E32323039313633292C20746D707661725F332C2076656333285F4D6574616C6C696329293B0A2020746D707661725F34203D2028746D707661725F33202A2028302E37373930383337202D20285F4D6574616C6C6963202A20302E373739303833372929293B0A20206D656469756D70207665633320746D707661725F363B0A2020746D707661725F36203D206E6F726D616C697A6528786C765F544558434F4F5244325F322E78797A293B0A20206D656469756D70207665633320746D707661725F373B0A2020746D707661725F37203D206E6F726D616C697A6528786C765F544558434F4F524431293B0A20206869676870207665633420746D707661725F383B0A2020746D707661725F382E77203D20312E303B0A2020746D707661725F382E78797A203D20786C765F544558434F4F5244353B0A20206869676870207665633320746D707661725F393B0A2020746D707661725F39203D2028756E6974795F576F726C64546F4C69676874202A20746D707661725F38292E78797A3B0A20206869676870207665633420765F31303B0A2020765F31302E78203D20756E6974795F4D6174726978565B305D2E7A3B0A2020765F31302E79203D20756E6974795F4D6174726978565B315D2E7A3B0A2020765F31302E7A203D20756E6974795F4D6174726978565B325D2E7A3B0A2020765F31302E77203D20756E6974795F4D6174726978565B335D2E7A3B0A20206869676870207665633320746D707661725F31313B0A2020746D707661725F3131203D2028786C765F544558434F4F524435202D20756E6974795F536861646F774661646543656E746572416E64547970652E78797A293B0A20206D656469756D7020666C6F617420746D707661725F31323B0A2020686967687020666C6F617420746D707661725F31333B0A2020746D707661725F3133203D20636C616D70202828280A202020206D69782028646F742028285F576F726C64537061636543616D657261506F73202D20786C765F544558434F4F524435292C20765F31302E78797A292C207371727428646F742028746D707661725F31312C20746D707661725F313129292C20756E6974795F536861646F774661646543656E746572416E64547970652E77290A2020202A205F4C69676874536861646F77446174612E7A29202B205F4C69676874536861646F77446174612E77292C20302E302C20312E30293B0A2020746D707661725F3132203D20746D707661725F31333B0A202068696768702076656333207665635F31343B0A20207665635F3134203D2028786C765F544558434F4F524435202D205F4C69676874506F736974696F6E52616E67652E78797A293B0A20206869676870207665633420736861646F7756616C735F31353B0A2020686967687020666C6F6174206D79646973745F31363B0A20206D79646973745F3136203D20282873717274280A20202020646F7420287665635F31342C207665635F3134290A202029202A205F4C69676874506F736974696F6E52616E67652E7729202A20302E3937293B0A2020736861646F7756616C735F31352E78203D20646F742028746578747572654375626520285F536861646F774D6170546578747572652C20287665635F3134202B207665633328302E303037383132352C20302E303037383132352C20302E303037383132352929292C207665633428312E302C20302E3030333932313536392C20312E3533373837652D30352C20362E303330383633652D303829293B0A2020736861646F7756616C735F31352E79203D20646F742028746578747572654375626520285F536861646F774D6170546578747572652C20287665635F3134202B2076656333282D302E303037383132352C202D302E303037383132352C20302E303037383132352929292C207665633428312E302C20302E3030333932313536392C20312E3533373837652D30352C20362E303330383633652D303829293B0A2020736861646F7756616C735F31352E7A203D20646F742028746578747572654375626520285F536861646F774D6170546578747572652C20287665635F3134202B2076656333282D302E303037383132352C20302E303037383132352C202D302E303037383132352929292C207665633428312E302C20302E3030333932313536392C20312E3533373837652D30352C20362E303330383633652D303829293B0A2020736861646F7756616C735F31352E77203D20646F742028746578747572654375626520285F536861646F774D6170546578747572652C20287665635F3134202B207665633328302E303037383132352C202D302E303037383132352C202D302E303037383132352929292C207665633428312E302C20302E3030333932313536392C20312E3533373837652D30352C20362E303330383633652D303829293B0A2020627665633420746D707661725F31373B0A2020746D707661725F3137203D206C6573735468616E2028736861646F7756616C735F31352C2076656334286D79646973745F313629293B0A20206D656469756D70207665633420746D707661725F31383B0A2020746D707661725F3138203D205F4C69676874536861646F77446174612E787878783B0A20206D656469756D7020666C6F617420746D707661725F31393B0A202069662028746D707661725F31372E7829207B0A20202020746D707661725F3139203D20746D707661725F31382E783B0A20207D20656C7365207B0A20202020746D707661725F3139203D20312E303B0A20207D3B0A20206D656469756D7020666C6F617420746D707661725F32303B0A202069662028746D707661725F31372E7929207B0A20202020746D707661725F3230203D20746D707661725F31382E793B0A20207D20656C7365207B0A20202020746D707661725F3230203D20312E303B0A20207D3B0A20206D656469756D7020666C6F617420746D707661725F32313B0A202069662028746D707661725F31372E7A29207B0A20202020746D707661725F3231203D20746D707661725F31382E7A3B0A20207D20656C7365207B0A20202020746D707661725F3231203D20312E303B0A20207D3B0A20206D656469756D7020666C6F617420746D707661725F32323B0A202069662028746D707661725F31372E7729207B0A20202020746D707661725F3232203D20746D707661725F31382E773B0A20207D20656C7365207B0A20202020746D707661725F3232203D20312E303B0A20207D3B0A20206D656469756D70207665633420746D707661725F32333B0A2020746D707661725F32332E78203D20746D707661725F31393B0A2020746D707661725F32332E79203D20746D707661725F32303B0A2020746D707661725F32332E7A203D20746D707661725F32313B0A2020746D707661725F32332E77203D20746D707661725F32323B0A20206D656469756D7020666C6F617420746D707661725F32343B0A2020746D707661725F3234203D20636C616D70202828646F742028746D707661725F32332C207665633428302E32352C20302E32352C20302E32352C20302E32352929202B20746D707661725F3132292C20302E302C20312E30293B0A2020736861646F775F31203D20746D707661725F32343B0A2020686967687020666C6F617420746D707661725F32353B0A2020746D707661725F3235203D20646F742028746D707661725F392C20746D707661725F39293B0A20206C6F777020666C6F617420746D707661725F32363B0A2020746D707661725F3236203D20282874657874757265324420285F4C696768745465787475726542302C207665633228746D707661725F323529292E77202A20746578747572654375626520285F4C6967687454657874757265302C20746D707661725F39292E7729202A20736861646F775F31293B0A20206D656469756D70207665633320746D707661725F32373B0A2020746D707661725F32372E78203D20786C765F544558434F4F5244322E773B0A2020746D707661725F32372E79203D20786C765F544558434F4F5244325F312E773B0A2020746D707661725F32372E7A203D20786C765F544558434F4F5244325F322E773B0A20206D656469756D7020666C6F617420617474656E5F32383B0A2020617474656E5F3238203D20746D707661725F32363B0A20206D656469756D70207665633320746D707661725F32393B0A2020746D707661725F3239203D205F4C69676874436F6C6F72302E78797A3B0A20206D656469756D70207665633320746D707661725F33303B0A2020746D707661725F3330203D206E6F726D616C697A6528746D707661725F3237293B0A2020746D707661725F3239203D2028746D707661725F3239202A20617474656E5F3238293B0A20206D656469756D7020666C6F61742073706563756C61725465726D5F33313B0A20206D656469756D70207665633320746D707661725F33323B0A20206D656469756D70207665633320696E5665635F33333B0A2020696E5665635F3333203D2028746D707661725F3330202D20746D707661725F37293B0A2020746D707661725F3332203D2028696E5665635F3333202A20696E766572736573717274286D61782028302E3030312C200A20202020646F742028696E5665635F33332C20696E5665635F3333290A20202929293B0A20206D656469756D7020666C6F617420746D707661725F33343B0A2020746D707661725F3334203D20636C616D702028646F742028746D707661725F362C20746D707661725F3332292C20302E302C20312E30293B0A20206D656469756D7020666C6F617420746D707661725F33353B0A2020746D707661725F3335203D2028312E30202D205F476C6F7373696E657373293B0A20206D656469756D7020666C6F617420746D707661725F33363B0A2020746D707661725F3336203D2028746D707661725F3335202A20746D707661725F3335293B0A202073706563756C61725465726D5F3331203D202828746D707661725F3336202F20280A20202020286D61782028302E33322C20636C616D702028646F742028746D707661725F33302C20746D707661725F3332292C20302E302C20312E302929202A2028312E35202B20746D707661725F333629290A2020202A200A20202020282828746D707661725F3334202A20746D707661725F333429202A202828746D707661725F3336202A20746D707661725F333629202D20312E302929202B20312E3030303031290A20202929202D20302E30303031293B0A20206D656469756D7020666C6F617420746D707661725F33373B0A2020746D707661725F3337203D20636C616D70202873706563756C61725465726D5F33312C20302E302C203130302E30293B0A202073706563756C61725465726D5F3331203D20746D707661725F33373B0A20206D656469756D70207665633420746D707661725F33383B0A2020746D707661725F33382E77203D20312E303B0A2020746D707661725F33382E78797A203D20282828746D707661725F34202B200A2020202028746D707661725F3337202A20746D707661725F35290A202029202A20746D707661725F323929202A20636C616D702028646F742028746D707661725F362C20746D707661725F3330292C20302E302C20312E3029293B0A20206D656469756D70207665633420786C61745F7661726F75747075745F33393B0A2020786C61745F7661726F75747075745F33392E78797A203D20746D707661725F33382E78797A3B0A2020786C61745F7661726F75747075745F33392E77203D20312E303B0A2020676C5F46726167446174615B305D203D20786C61745F7661726F75747075745F33393B0A7D0A0A0A23656E6469660A0000001B000000000000000100000000000000000000000000000000000000324F040C050000001C00000002000000000000000000000000000000981300002376657273696F6E203130300A0A236966646566205645525445580A6174747269627574652076656334205F676C65735665727465783B0A6174747269627574652076656333205F676C65734E6F726D616C3B0A6174747269627574652076656334205F676C65734D756C7469546578436F6F7264303B0A6174747269627574652076656334205F676C65734D756C7469546578436F6F7264313B0A756E69666F726D2068696768702076656333205F576F726C64537061636543616D657261506F733B0A756E69666F726D206D656469756D70207665633420756E6974795F534842723B0A756E69666F726D206D656469756D70207665633420756E6974795F534842673B0A756E69666F726D206D656469756D70207665633420756E6974795F534842623B0A756E69666F726D206D656469756D70207665633420756E6974795F5348433B0A756E69666F726D206869676870206D61743420756E6974795F4F626A656374546F576F726C643B0A756E69666F726D206869676870206D61743420756E6974795F576F726C64546F4F626A6563743B0A756E69666F726D206869676870206D61743420756E6974795F4D617472697856503B0A756E69666F726D2068696768702076656334205F4D61696E5465785F53543B0A756E69666F726D2068696768702076656334205F44657461696C416C6265646F4D61705F53543B0A756E69666F726D206D656469756D7020666C6F6174205F55565365633B0A76617279696E67206869676870207665633420786C765F544558434F4F5244303B0A76617279696E67206D656469756D70207665633320786C765F544558434F4F5244313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244323B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244325F313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244325F323B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244353B0A76617279696E67206869676870207665633320786C765F544558434F4F5244363B0A766F6964206D61696E2028290A7B0A20206D656469756D70207665633320746D707661725F313B0A2020746D707661725F31203D205F676C65734E6F726D616C3B0A20206869676870207665633220746D707661725F323B0A2020746D707661725F32203D205F676C65734D756C7469546578436F6F7264302E78793B0A20206869676870207665633220746D707661725F333B0A2020746D707661725F33203D205F676C65734D756C7469546578436F6F7264312E78793B0A20206D656469756D70207665633420746D707661725F343B0A20206D656469756D70207665633420746D707661725F353B0A20206D656469756D70207665633420746D707661725F363B0A20206D656469756D70207665633420746D707661725F373B0A20206869676870207665633320746D707661725F383B0A20206869676870207665633420746D707661725F393B0A2020746D707661725F39203D2028756E6974795F4F626A656374546F576F726C64202A205F676C6573566572746578293B0A2020746D707661725F38203D20746D707661725F392E78797A3B0A20206869676870207665633420746D707661725F31303B0A20206869676870207665633420746D707661725F31313B0A2020746D707661725F31312E77203D20312E303B0A2020746D707661725F31312E78797A203D205F676C65735665727465782E78797A3B0A2020746D707661725F3130203D2028756E6974795F4D61747269785650202A2028756E6974795F4F626A656374546F576F726C64202A20746D707661725F313129293B0A20206869676870207665633420746578636F6F72645F31323B0A2020746578636F6F72645F31322E7879203D2028285F676C65734D756C7469546578436F6F7264302E7879202A205F4D61696E5465785F53542E787929202B205F4D61696E5465785F53542E7A77293B0A20206869676870207665633220746D707661725F31333B0A202069662028285F5556536563203D3D20302E302929207B0A20202020746D707661725F3133203D20746D707661725F323B0A20207D20656C7365207B0A20202020746D707661725F3133203D20746D707661725F333B0A20207D3B0A2020746578636F6F72645F31322E7A77203D202828746D707661725F3133202A205F44657461696C416C6265646F4D61705F53542E787929202B205F44657461696C416C6265646F4D61705F53542E7A77293B0A20206D656469756D70207665633320746D707661725F31343B0A202068696768702076656333206E5F31353B0A20206E5F3135203D2028746D707661725F392E78797A202D205F576F726C64537061636543616D657261506F73293B0A2020746D707661725F3134203D206E5F31353B0A202068696768702076656333206E6F726D5F31363B0A20206E6F726D5F3136203D20746D707661725F313B0A20206869676870206D61743320746D707661725F31373B0A2020746D707661725F31375B305D203D20756E6974795F576F726C64546F4F626A6563745B305D2E78797A3B0A2020746D707661725F31375B315D203D20756E6974795F576F726C64546F4F626A6563745B315D2E78797A3B0A2020746D707661725F31375B325D203D20756E6974795F576F726C64546F4F626A6563745B325D2E78797A3B0A20206869676870207665633320746D707661725F31383B0A2020746D707661725F3138203D206E6F726D616C697A6528286E6F726D5F3136202A20746D707661725F313729293B0A2020746D707661725F342E78797A203D207665633328302E302C20302E302C20302E30293B0A2020746D707661725F352E78797A203D207665633328302E302C20302E302C20302E30293B0A2020746D707661725F362E78797A203D20746D707661725F31383B0A2020746D707661725F372E77203D20302E303B0A20206D656469756D702076656333206E6F726D616C5F31393B0A20206E6F726D616C5F3139203D20746D707661725F31383B0A20206D656469756D7020766563332078315F32303B0A20206D656469756D70207665633420746D707661725F32313B0A2020746D707661725F3231203D20286E6F726D616C5F31392E78797A7A202A206E6F726D616C5F31392E797A7A78293B0A202078315F32302E78203D20646F742028756E6974795F534842722C20746D707661725F3231293B0A202078315F32302E79203D20646F742028756E6974795F534842672C20746D707661725F3231293B0A202078315F32302E7A203D20646F742028756E6974795F534842622C20746D707661725F3231293B0A2020746D707661725F372E78797A203D202878315F3230202B2028756E6974795F5348432E78797A202A20280A20202020286E6F726D616C5F31392E78202A206E6F726D616C5F31392E78290A2020202D200A20202020286E6F726D616C5F31392E79202A206E6F726D616C5F31392E79290A20202929293B0A2020676C5F506F736974696F6E203D20746D707661725F31303B0A2020786C765F544558434F4F524430203D20746578636F6F72645F31323B0A2020786C765F544558434F4F524431203D20746D707661725F31343B0A2020786C765F544558434F4F524432203D20746D707661725F343B0A2020786C765F544558434F4F5244325F31203D20746D707661725F353B0A2020786C765F544558434F4F5244325F32203D20746D707661725F363B0A2020786C765F544558434F4F524435203D20746D707661725F373B0A2020786C765F544558434F4F524436203D20746D707661725F383B0A7D0A0A0A23656E6469660A23696664656620465241474D454E540A23657874656E73696F6E20474C5F4558545F7368616465725F746578747572655F6C6F64203A20656E61626C650A23657874656E73696F6E20474C5F4558545F647261775F62756666657273203A20656E61626C650A756E69666F726D206D656469756D70207665633420756E6974795F534841723B0A756E69666F726D206D656469756D70207665633420756E6974795F534841673B0A756E69666F726D206D656469756D70207665633420756E6974795F534841623B0A756E69666F726D206D656469756D702076656334205F436F6C6F723B0A756E69666F726D2073616D706C65723244205F4D61696E5465783B0A756E69666F726D206D656469756D7020666C6F6174205F4D6574616C6C69633B0A756E69666F726D206D656469756D7020666C6F6174205F476C6F7373696E6573733B0A756E69666F726D2073616D706C65723244205F4F63636C7573696F6E4D61703B0A756E69666F726D206D656469756D7020666C6F6174205F4F63636C7573696F6E537472656E6774683B0A76617279696E67206869676870207665633420786C765F544558434F4F5244303B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244325F323B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244353B0A766F6964206D61696E2028290A7B0A20206C6F7770207665633420746D707661725F313B0A2020746D707661725F31203D2074657874757265324420285F4D61696E5465782C20786C765F544558434F4F5244302E7879293B0A20206D656469756D702076656332206D675F323B0A20206D675F322E78203D205F4D6574616C6C69633B0A20206D675F322E79203D205F476C6F7373696E6573733B0A20206D656469756D70207665633320746D707661725F333B0A2020746D707661725F33203D20285F436F6C6F722E78797A202A20746D707661725F312E78797A293B0A20206D656469756D70207665633320746D707661725F343B0A2020746D707661725F34203D2028746D707661725F33202A2028302E37373930383337202D20285F4D6574616C6C6963202A20302E373739303833372929293B0A20206D656469756D70207665633320746D707661725F353B0A2020746D707661725F35203D206E6F726D616C697A6528786C765F544558434F4F5244325F322E78797A293B0A20206D656469756D7020666C6F6174206F63635F363B0A20206C6F777020666C6F617420746D707661725F373B0A2020746D707661725F37203D2074657874757265324420285F4F63636C7573696F6E4D61702C20786C765F544558434F4F5244302E7879292E793B0A20206F63635F36203D20746D707661725F373B0A20206D656469756D7020666C6F617420746D707661725F383B0A2020746D707661725F38203D202828312E30202D205F4F63636C7573696F6E537472656E67746829202B20286F63635F36202A205F4F63636C7573696F6E537472656E67746829293B0A20206D656469756D70207665633320746D707661725F393B0A20206D656469756D70207665633420746D707661725F31303B0A2020746D707661725F31302E77203D20312E303B0A2020746D707661725F31302E78797A203D20746D707661725F353B0A20206D656469756D70207665633320785F31313B0A2020785F31312E78203D20646F742028756E6974795F534841722C20746D707661725F3130293B0A2020785F31312E79203D20646F742028756E6974795F534841672C20746D707661725F3130293B0A2020785F31312E7A203D20646F742028756E6974795F534841622C20746D707661725F3130293B0A2020746D707661725F39203D206D617820282828312E303535202A200A20202020706F7720286D617820287665633328302E302C20302E302C20302E30292C2028786C765F544558434F4F5244352E78797A202B20785F313129292C207665633328302E343136363636372C20302E343136363636372C20302E3431363636363729290A202029202D20302E303535292C207665633328302E302C20302E302C20302E3029293B0A2020746D707661725F39203D2028746D707661725F39202A20746D707661725F38293B0A20206D656469756D70207665633420746D707661725F31323B0A2020746D707661725F31322E77203D20312E303B0A2020746D707661725F31322E78797A203D2028746D707661725F39202A20746D707661725F34293B0A20206D656469756D70207665633420746D707661725F31333B0A2020746D707661725F31332E78797A203D20746D707661725F343B0A2020746D707661725F31332E77203D20746D707661725F383B0A20206D656469756D70207665633420746D707661725F31343B0A2020746D707661725F31342E78797A203D206D697820287665633328302E323230393136332C20302E323230393136332C20302E32323039313633292C20746D707661725F332C2076656333285F4D6574616C6C696329293B0A2020746D707661725F31342E77203D206D675F322E793B0A20206D656469756D70207665633420746D707661725F31353B0A2020746D707661725F31352E77203D20312E303B0A2020746D707661725F31352E78797A203D202828746D707661725F35202A20302E3529202B20302E35293B0A20206D656469756D70207665633420746D707661725F31363B0A2020746D707661725F31362E77203D20312E303B0A2020746D707661725F31362E78797A203D2065787032282D28746D707661725F31322E78797A29293B0A2020676C5F46726167446174615B305D203D20746D707661725F31333B0A2020676C5F46726167446174615B315D203D20746D707661725F31343B0A2020676C5F46726167446174615B325D203D20746D707661725F31353B0A2020676C5F46726167446174615B335D203D20746D707661725F31363B0A7D0A0A0A23656E6469660A1B000000000000000100000000000000000000000000000000000000324F040C05000000000000000000000000000000000000000200000012000000444952454354494F4E414C5F434F4F4B494500000E000000534841444F57535F53435245454E00000000000000000000000000000100000000000000000000000000000000000000324F040C0500000000000000000000000000000000000000010000000C000000554E4954595F4844525F4F4E0000000000000000000000000100000000000000000000000000000000000000324F040C050000004A0000000600000004000000000000000300000005000000504F494E540000000C000000534841444F57535F435542450C000000534841444F57535F534F4654651E00002376657273696F6E203130300A0A236966646566205645525445580A6174747269627574652076656334205F676C65735665727465783B0A6174747269627574652076656333205F676C65734E6F726D616C3B0A6174747269627574652076656334205F676C65734D756C7469546578436F6F7264303B0A6174747269627574652076656334205F676C65734D756C7469546578436F6F7264313B0A756E69666F726D2068696768702076656333205F576F726C64537061636543616D657261506F733B0A756E69666F726D2068696768702076656334205F576F726C6453706163654C69676874506F73303B0A756E69666F726D206869676870206D61743420756E6974795F4F626A656374546F576F726C643B0A756E69666F726D206869676870206D61743420756E6974795F576F726C64546F4F626A6563743B0A756E69666F726D206869676870206D61743420756E6974795F4D617472697856503B0A756E69666F726D2068696768702076656334205F4D61696E5465785F53543B0A756E69666F726D2068696768702076656334205F44657461696C416C6265646F4D61705F53543B0A756E69666F726D206D656469756D7020666C6F6174205F55565365633B0A76617279696E67206869676870207665633420786C765F544558434F4F5244303B0A76617279696E67206D656469756D70207665633320786C765F544558434F4F5244313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244323B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244325F313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244325F323B0A76617279696E67206869676870207665633320786C765F544558434F4F5244353B0A76617279696E67206869676870207665633220786C765F544558434F4F5244363B0A766F6964206D61696E2028290A7B0A20206D656469756D70207665633320746D707661725F313B0A2020746D707661725F31203D205F676C65734E6F726D616C3B0A20206869676870207665633220746D707661725F323B0A2020746D707661725F32203D205F676C65734D756C7469546578436F6F7264302E78793B0A20206869676870207665633220746D707661725F333B0A2020746D707661725F33203D205F676C65734D756C7469546578436F6F7264312E78793B0A202068696768702076656333206C696768744469725F343B0A20206D656469756D70207665633420746D707661725F353B0A20206D656469756D70207665633420746D707661725F363B0A20206D656469756D70207665633420746D707661725F373B0A20206869676870207665633220746D707661725F383B0A20206869676870207665633420746D707661725F393B0A2020746D707661725F39203D2028756E6974795F4F626A656374546F576F726C64202A205F676C6573566572746578293B0A20206869676870207665633420746D707661725F31303B0A20206869676870207665633420746D707661725F31313B0A2020746D707661725F31312E77203D20312E303B0A2020746D707661725F31312E78797A203D205F676C65735665727465782E78797A3B0A2020746D707661725F3130203D2028756E6974795F4D61747269785650202A2028756E6974795F4F626A656374546F576F726C64202A20746D707661725F313129293B0A20206869676870207665633420746578636F6F72645F31323B0A2020746578636F6F72645F31322E7879203D2028285F676C65734D756C7469546578436F6F7264302E7879202A205F4D61696E5465785F53542E787929202B205F4D61696E5465785F53542E7A77293B0A20206869676870207665633220746D707661725F31333B0A202069662028285F5556536563203D3D20302E302929207B0A20202020746D707661725F3133203D20746D707661725F323B0A20207D20656C7365207B0A20202020746D707661725F3133203D20746D707661725F333B0A20207D3B0A2020746578636F6F72645F31322E7A77203D202828746D707661725F3133202A205F44657461696C416C6265646F4D61705F53542E787929202B205F44657461696C416C6265646F4D61705F53542E7A77293B0A20206D656469756D70207665633320746D707661725F31343B0A202068696768702076656333206E5F31353B0A20206E5F3135203D2028746D707661725F392E78797A202D205F576F726C64537061636543616D657261506F73293B0A2020746D707661725F3134203D206E5F31353B0A202068696768702076656333206E6F726D5F31363B0A20206E6F726D5F3136203D20746D707661725F313B0A20206869676870206D61743320746D707661725F31373B0A2020746D707661725F31375B305D203D20756E6974795F576F726C64546F4F626A6563745B305D2E78797A3B0A2020746D707661725F31375B315D203D20756E6974795F576F726C64546F4F626A6563745B315D2E78797A3B0A2020746D707661725F31375B325D203D20756E6974795F576F726C64546F4F626A6563745B325D2E78797A3B0A20206869676870207665633320746D707661725F31383B0A2020746D707661725F3138203D206E6F726D616C697A6528286E6F726D5F3136202A20746D707661725F313729293B0A2020746D707661725F352E78797A203D207665633328302E302C20302E302C20302E30293B0A2020746D707661725F362E78797A203D207665633328302E302C20302E302C20302E30293B0A2020746D707661725F372E78797A203D20746D707661725F31383B0A20206869676870207665633320746D707661725F31393B0A2020746D707661725F3139203D20285F576F726C6453706163654C69676874506F73302E78797A202D2028746D707661725F392E78797A202A205F576F726C6453706163654C69676874506F73302E7729293B0A20206D656469756D70207665633320746D707661725F32303B0A2020746D707661725F3230203D20746D707661725F31393B0A20206C696768744469725F34203D20746D707661725F32303B0A2020746D707661725F352E77203D206C696768744469725F342E783B0A2020746D707661725F362E77203D206C696768744469725F342E793B0A2020746D707661725F372E77203D206C696768744469725F342E7A3B0A2020676C5F506F736974696F6E203D20746D707661725F31303B0A2020786C765F544558434F4F524430203D20746578636F6F72645F31323B0A2020786C765F544558434F4F524431203D20746D707661725F31343B0A2020786C765F544558434F4F524432203D20746D707661725F353B0A2020786C765F544558434F4F5244325F31203D20746D707661725F363B0A2020786C765F544558434F4F5244325F32203D20746D707661725F373B0A2020786C765F544558434F4F524435203D20746D707661725F392E78797A3B0A2020786C765F544558434F4F524436203D20746D707661725F383B0A7D0A0A0A23656E6469660A23696664656620465241474D454E540A756E69666F726D2068696768702076656333205F576F726C64537061636543616D657261506F733B0A756E69666F726D2068696768702076656334205F4C69676874506F736974696F6E52616E67653B0A756E69666F726D206D656469756D702076656334205F4C69676874536861646F77446174613B0A756E69666F726D206869676870207665633420756E6974795F536861646F774661646543656E746572416E64547970653B0A756E69666F726D206869676870206D61743420756E6974795F4D6174726978563B0A756E69666F726D206C6F77702076656334205F4C69676874436F6C6F72303B0A756E69666F726D2068696768702073616D706C657243756265205F536861646F774D6170546578747572653B0A756E69666F726D206D656469756D702076656334205F436F6C6F723B0A756E69666F726D2073616D706C65723244205F4D61696E5465783B0A756E69666F726D206D656469756D7020666C6F6174205F4D6574616C6C69633B0A756E69666F726D206D656469756D7020666C6F6174205F476C6F7373696E6573733B0A756E69666F726D2073616D706C65723244205F4C6967687454657874757265303B0A756E69666F726D206869676870206D61743420756E6974795F576F726C64546F4C696768743B0A76617279696E67206869676870207665633420786C765F544558434F4F5244303B0A76617279696E67206D656469756D70207665633320786C765F544558434F4F5244313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244323B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244325F313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244325F323B0A76617279696E67206869676870207665633320786C765F544558434F4F5244353B0A766F6964206D61696E2028290A7B0A20206C6F777020666C6F617420736861646F775F313B0A20206C6F7770207665633420746D707661725F323B0A2020746D707661725F32203D2074657874757265324420285F4D61696E5465782C20786C765F544558434F4F5244302E7879293B0A20206D656469756D70207665633320746D707661725F333B0A2020746D707661725F33203D20285F436F6C6F722E78797A202A20746D707661725F322E78797A293B0A20206D656469756D70207665633320746D707661725F343B0A20206D656469756D70207665633320746D707661725F353B0A2020746D707661725F35203D206D697820287665633328302E323230393136332C20302E323230393136332C20302E32323039313633292C20746D707661725F332C2076656333285F4D6574616C6C696329293B0A2020746D707661725F34203D2028746D707661725F33202A2028302E37373930383337202D20285F4D6574616C6C6963202A20302E373739303833372929293B0A20206D656469756D70207665633320746D707661725F363B0A2020746D707661725F36203D206E6F726D616C697A6528786C765F544558434F4F5244325F322E78797A293B0A20206D656469756D70207665633320746D707661725F373B0A2020746D707661725F37203D206E6F726D616C697A6528786C765F544558434F4F524431293B0A20206869676870207665633420746D707661725F383B0A2020746D707661725F382E77203D20312E303B0A2020746D707661725F382E78797A203D20786C765F544558434F4F5244353B0A20206869676870207665633320746D707661725F393B0A2020746D707661725F39203D2028756E6974795F576F726C64546F4C69676874202A20746D707661725F38292E78797A3B0A20206869676870207665633420765F31303B0A2020765F31302E78203D20756E6974795F4D6174726978565B305D2E7A3B0A2020765F31302E79203D20756E6974795F4D6174726978565B315D2E7A3B0A2020765F31302E7A203D20756E6974795F4D6174726978565B325D2E7A3B0A2020765F31302E77203D20756E6974795F4D6174726978565B335D2E7A3B0A20206869676870207665633320746D707661725F31313B0A2020746D707661725F3131203D2028786C765F544558434F4F524435202D20756E6974795F536861646F774661646543656E746572416E64547970652E78797A293B0A20206D656469756D7020666C6F617420746D707661725F31323B0A2020686967687020666C6F617420746D707661725F31333B0A2020746D707661725F3133203D20636C616D70202828280A202020206D69782028646F742028285F576F726C64537061636543616D657261506F73202D20786C765F544558434F4F524435292C20765F31302E78797A292C207371727428646F742028746D707661725F31312C20746D707661725F313129292C20756E6974795F536861646F774661646543656E746572416E64547970652E77290A2020202A205F4C69676874536861646F77446174612E7A29202B205F4C69676874536861646F77446174612E77292C20302E302C20312E30293B0A2020746D707661725F3132203D20746D707661725F31333B0A202068696768702076656333207665635F31343B0A20207665635F3134203D2028786C765F544558434F4F524435202D205F4C69676874506F736974696F6E52616E67652E78797A293B0A20206869676870207665633420736861646F7756616C735F31353B0A2020686967687020666C6F6174206D79646973745F31363B0A20206D79646973745F3136203D20282873717274280A20202020646F7420287665635F31342C207665635F3134290A202029202A205F4C69676874506F736974696F6E52616E67652E7729202A20302E3937293B0A2020736861646F7756616C735F31352E78203D20646F742028746578747572654375626520285F536861646F774D6170546578747572652C20287665635F3134202B207665633328302E303037383132352C20302E303037383132352C20302E303037383132352929292C207665633428312E302C20302E3030333932313536392C20312E3533373837652D30352C20362E303330383633652D303829293B0A2020736861646F7756616C735F31352E79203D20646F742028746578747572654375626520285F536861646F774D6170546578747572652C20287665635F3134202B2076656333282D302E303037383132352C202D302E303037383132352C20302E303037383132352929292C207665633428312E302C20302E3030333932313536392C20312E3533373837652D30352C20362E303330383633652D303829293B0A2020736861646F7756616C735F31352E7A203D20646F742028746578747572654375626520285F536861646F774D6170546578747572652C20287665635F3134202B2076656333282D302E303037383132352C20302E303037383132352C202D302E303037383132352929292C207665633428312E302C20302E3030333932313536392C20312E3533373837652D30352C20362E303330383633652D303829293B0A2020736861646F7756616C735F31352E77203D20646F742028746578747572654375626520285F536861646F774D6170546578747572652C20287665635F3134202B207665633328302E303037383132352C202D302E303037383132352C202D302E303037383132352929292C207665633428312E302C20302E3030333932313536392C20312E3533373837652D30352C20362E303330383633652D303829293B0A2020627665633420746D707661725F31373B0A2020746D707661725F3137203D206C6573735468616E2028736861646F7756616C735F31352C2076656334286D79646973745F313629293B0A20206D656469756D70207665633420746D707661725F31383B0A2020746D707661725F3138203D205F4C69676874536861646F77446174612E787878783B0A20206D656469756D7020666C6F617420746D707661725F31393B0A202069662028746D707661725F31372E7829207B0A20202020746D707661725F3139203D20746D707661725F31382E783B0A20207D20656C7365207B0A20202020746D707661725F3139203D20312E303B0A20207D3B0A20206D656469756D7020666C6F617420746D707661725F32303B0A202069662028746D707661725F31372E7929207B0A20202020746D707661725F3230203D20746D707661725F31382E793B0A20207D20656C7365207B0A20202020746D707661725F3230203D20312E303B0A20207D3B0A20206D656469756D7020666C6F617420746D707661725F32313B0A202069662028746D707661725F31372E7A29207B0A20202020746D707661725F3231203D20746D707661725F31382E7A3B0A20207D20656C7365207B0A20202020746D707661725F3231203D20312E303B0A20207D3B0A20206D656469756D7020666C6F617420746D707661725F32323B0A202069662028746D707661725F31372E7729207B0A20202020746D707661725F3232203D20746D707661725F31382E773B0A20207D20656C7365207B0A20202020746D707661725F3232203D20312E303B0A20207D3B0A20206D656469756D70207665633420746D707661725F32333B0A2020746D707661725F32332E78203D20746D707661725F31393B0A2020746D707661725F32332E79203D20746D707661725F32303B0A2020746D707661725F32332E7A203D20746D707661725F32313B0A2020746D707661725F32332E77203D20746D707661725F32323B0A20206D656469756D7020666C6F617420746D707661725F32343B0A2020746D707661725F3234203D20636C616D70202828646F742028746D707661725F32332C207665633428302E32352C20302E32352C20302E32352C20302E32352929202B20746D707661725F3132292C20302E302C20312E30293B0A2020736861646F775F31203D20746D707661725F32343B0A2020686967687020666C6F617420746D707661725F32353B0A2020746D707661725F3235203D20646F742028746D707661725F392C20746D707661725F39293B0A20206C6F777020666C6F617420746D707661725F32363B0A2020746D707661725F3236203D202874657874757265324420285F4C6967687454657874757265302C207665633228746D707661725F323529292E77202A20736861646F775F31293B0A20206D656469756D70207665633320746D707661725F32373B0A2020746D707661725F32372E78203D20786C765F544558434F4F5244322E773B0A2020746D707661725F32372E79203D20786C765F544558434F4F5244325F312E773B0A2020746D707661725F32372E7A203D20786C765F544558434F4F5244325F322E773B0A20206D656469756D7020666C6F617420617474656E5F32383B0A2020617474656E5F3238203D20746D707661725F32363B0A20206D656469756D70207665633320746D707661725F32393B0A2020746D707661725F3239203D205F4C69676874436F6C6F72302E78797A3B0A20206D656469756D70207665633320746D707661725F33303B0A2020746D707661725F3330203D206E6F726D616C697A6528746D707661725F3237293B0A2020746D707661725F3239203D2028746D707661725F3239202A20617474656E5F3238293B0A20206D656469756D7020666C6F61742073706563756C61725465726D5F33313B0A20206D656469756D70207665633320746D707661725F33323B0A20206D656469756D70207665633320696E5665635F33333B0A2020696E5665635F3333203D2028746D707661725F3330202D20746D707661725F37293B0A2020746D707661725F3332203D2028696E5665635F3333202A20696E766572736573717274286D61782028302E3030312C200A20202020646F742028696E5665635F33332C20696E5665635F3333290A20202929293B0A20206D656469756D7020666C6F617420746D707661725F33343B0A2020746D707661725F3334203D20636C616D702028646F742028746D707661725F362C20746D707661725F3332292C20302E302C20312E30293B0A20206D656469756D7020666C6F617420746D707661725F33353B0A2020746D707661725F3335203D2028312E30202D205F476C6F7373696E657373293B0A20206D656469756D7020666C6F617420746D707661725F33363B0A2020746D707661725F3336203D2028746D707661725F3335202A20746D707661725F3335293B0A202073706563756C61725465726D5F3331203D202828746D707661725F3336202F20280A20202020286D61782028302E33322C20636C616D702028646F742028746D707661725F33302C20746D707661725F3332292C20302E302C20312E302929202A2028312E35202B20746D707661725F333629290A2020202A200A20202020282828746D707661725F3334202A20746D707661725F333429202A202828746D707661725F3336202A20746D707661725F333629202D20312E302929202B20312E3030303031290A20202929202D20302E30303031293B0A20206D656469756D7020666C6F617420746D707661725F33373B0A2020746D707661725F3337203D20636C616D70202873706563756C61725465726D5F33312C20302E302C203130302E30293B0A202073706563756C61725465726D5F3331203D20746D707661725F33373B0A20206D656469756D70207665633420746D707661725F33383B0A2020746D707661725F33382E77203D20312E303B0A2020746D707661725F33382E78797A203D20282828746D707661725F34202B200A2020202028746D707661725F3337202A20746D707661725F35290A202029202A20746D707661725F323929202A20636C616D702028646F742028746D707661725F362C20746D707661725F3330292C20302E302C20312E3029293B0A20206D656469756D70207665633420786C61745F7661726F75747075745F33393B0A2020786C61745F7661726F75747075745F33392E78797A203D20746D707661725F33382E78797A3B0A2020786C61745F7661726F75747075745F33392E77203D20312E303B0A2020676C5F46726167446174615B305D203D20786C61745F7661726F75747075745F33393B0A7D0A0A0A23656E6469660A0000001B000000000000000100000000000000000000000000000000000000324F040C050000001F0000000300000000000000000000000100000012000000444952454354494F4E414C5F434F4F4B49450000321200002376657273696F6E203130300A0A236966646566205645525445580A6174747269627574652076656334205F676C65735665727465783B0A6174747269627574652076656333205F676C65734E6F726D616C3B0A6174747269627574652076656334205F676C65734D756C7469546578436F6F7264303B0A6174747269627574652076656334205F676C65734D756C7469546578436F6F7264313B0A756E69666F726D2068696768702076656333205F576F726C64537061636543616D657261506F733B0A756E69666F726D206D656469756D702076656334205F576F726C6453706163654C69676874506F73303B0A756E69666F726D206869676870206D61743420756E6974795F4F626A656374546F576F726C643B0A756E69666F726D206869676870206D61743420756E6974795F576F726C64546F4F626A6563743B0A756E69666F726D206869676870206D61743420756E6974795F4D617472697856503B0A756E69666F726D2068696768702076656334205F4D61696E5465785F53543B0A756E69666F726D2068696768702076656334205F44657461696C416C6265646F4D61705F53543B0A756E69666F726D206D656469756D7020666C6F6174205F55565365633B0A76617279696E67206869676870207665633420786C765F544558434F4F5244303B0A76617279696E67206D656469756D70207665633320786C765F544558434F4F5244313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244323B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244325F313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244325F323B0A76617279696E67206869676870207665633320786C765F544558434F4F5244353B0A76617279696E67206869676870207665633220786C765F544558434F4F5244363B0A766F6964206D61696E2028290A7B0A20206D656469756D70207665633320746D707661725F313B0A2020746D707661725F31203D205F676C65734E6F726D616C3B0A20206869676870207665633220746D707661725F323B0A2020746D707661725F32203D205F676C65734D756C7469546578436F6F7264302E78793B0A20206869676870207665633220746D707661725F333B0A2020746D707661725F33203D205F676C65734D756C7469546578436F6F7264312E78793B0A20206D656469756D70207665633420746D707661725F343B0A20206D656469756D70207665633420746D707661725F353B0A20206D656469756D70207665633420746D707661725F363B0A20206869676870207665633220746D707661725F373B0A20206869676870207665633420746D707661725F383B0A2020746D707661725F38203D2028756E6974795F4F626A656374546F576F726C64202A205F676C6573566572746578293B0A20206869676870207665633420746D707661725F393B0A20206869676870207665633420746D707661725F31303B0A2020746D707661725F31302E77203D20312E303B0A2020746D707661725F31302E78797A203D205F676C65735665727465782E78797A3B0A2020746D707661725F39203D2028756E6974795F4D61747269785650202A2028756E6974795F4F626A656374546F576F726C64202A20746D707661725F313029293B0A20206869676870207665633420746578636F6F72645F31313B0A2020746578636F6F72645F31312E7879203D2028285F676C65734D756C7469546578436F6F7264302E7879202A205F4D61696E5465785F53542E787929202B205F4D61696E5465785F53542E7A77293B0A20206869676870207665633220746D707661725F31323B0A202069662028285F5556536563203D3D20302E302929207B0A20202020746D707661725F3132203D20746D707661725F323B0A20207D20656C7365207B0A20202020746D707661725F3132203D20746D707661725F333B0A20207D3B0A2020746578636F6F72645F31312E7A77203D202828746D707661725F3132202A205F44657461696C416C6265646F4D61705F53542E787929202B205F44657461696C416C6265646F4D61705F53542E7A77293B0A20206D656469756D70207665633320746D707661725F31333B0A20206869676870207665633320746D707661725F31343B0A2020746D707661725F3134203D206E6F726D616C697A652828746D707661725F382E78797A202D205F576F726C64537061636543616D657261506F7329293B0A2020746D707661725F3133203D20746D707661725F31343B0A202068696768702076656333206E6F726D5F31353B0A20206E6F726D5F3135203D20746D707661725F313B0A20206869676870206D61743320746D707661725F31363B0A2020746D707661725F31365B305D203D20756E6974795F576F726C64546F4F626A6563745B305D2E78797A3B0A2020746D707661725F31365B315D203D20756E6974795F576F726C64546F4F626A6563745B315D2E78797A3B0A2020746D707661725F31365B325D203D20756E6974795F576F726C64546F4F626A6563745B325D2E78797A3B0A20206869676870207665633320746D707661725F31373B0A2020746D707661725F3137203D206E6F726D616C697A6528286E6F726D5F3135202A20746D707661725F313629293B0A2020746D707661725F342E78797A203D207665633328302E302C20302E302C20302E30293B0A2020746D707661725F352E78797A203D207665633328302E302C20302E302C20302E30293B0A2020746D707661725F362E78797A203D20746D707661725F31373B0A20206869676870207665633320746D707661725F31383B0A2020746D707661725F3138203D20285F576F726C6453706163654C69676874506F73302E78797A202D2028746D707661725F382E78797A202A205F576F726C6453706163654C69676874506F73302E7729293B0A2020746D707661725F342E77203D20746D707661725F31382E783B0A2020746D707661725F352E77203D20746D707661725F31382E793B0A2020746D707661725F362E77203D20746D707661725F31382E7A3B0A2020676C5F506F736974696F6E203D20746D707661725F393B0A2020786C765F544558434F4F524430203D20746578636F6F72645F31313B0A2020786C765F544558434F4F524431203D20746D707661725F31333B0A2020786C765F544558434F4F524432203D20746D707661725F343B0A2020786C765F544558434F4F5244325F31203D20746D707661725F353B0A2020786C765F544558434F4F5244325F32203D20746D707661725F363B0A2020786C765F544558434F4F524435203D20746D707661725F382E78797A3B0A2020786C765F544558434F4F524436203D20746D707661725F373B0A7D0A0A0A23656E6469660A23696664656620465241474D454E540A756E69666F726D206C6F77702076656334205F4C69676874436F6C6F72303B0A756E69666F726D2073616D706C6572324420756E6974795F4E4878526F7567686E6573733B0A756E69666F726D206D656469756D702076656334205F436F6C6F723B0A756E69666F726D2073616D706C65723244205F4D61696E5465783B0A756E69666F726D206D656469756D7020666C6F6174205F4D6574616C6C69633B0A756E69666F726D206D656469756D7020666C6F6174205F476C6F7373696E6573733B0A756E69666F726D2073616D706C65723244205F4C6967687454657874757265303B0A756E69666F726D206869676870206D61743420756E6974795F576F726C64546F4C696768743B0A76617279696E67206869676870207665633420786C765F544558434F4F5244303B0A76617279696E67206D656469756D70207665633320786C765F544558434F4F5244313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244323B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244325F313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244325F323B0A76617279696E67206869676870207665633320786C765F544558434F4F5244353B0A766F6964206D61696E2028290A7B0A202068696768702076656332206C69676874436F6F72645F313B0A20206C6F7770207665633420746D707661725F323B0A2020746D707661725F32203D2074657874757265324420285F4D61696E5465782C20786C765F544558434F4F5244302E7879293B0A20206D656469756D70207665633320746D707661725F333B0A2020746D707661725F33203D20285F436F6C6F722E78797A202A20746D707661725F322E78797A293B0A20206D656469756D70207665633320746D707661725F343B0A2020746D707661725F34203D206E6F726D616C697A6528786C765F544558434F4F5244325F322E78797A293B0A20206869676870207665633420746D707661725F353B0A2020746D707661725F352E77203D20312E303B0A2020746D707661725F352E78797A203D20786C765F544558434F4F5244353B0A20206C69676874436F6F72645F31203D2028756E6974795F576F726C64546F4C69676874202A20746D707661725F35292E78793B0A20206C6F777020666C6F617420746D707661725F363B0A2020746D707661725F36203D2074657874757265324420285F4C6967687454657874757265302C206C69676874436F6F72645F31292E773B0A20206D656469756D70207665633320746D707661725F373B0A2020746D707661725F372E78203D20786C765F544558434F4F5244322E773B0A2020746D707661725F372E79203D20786C765F544558434F4F5244325F312E773B0A2020746D707661725F372E7A203D20786C765F544558434F4F5244325F322E773B0A20206D656469756D7020666C6F617420617474656E5F383B0A2020617474656E5F38203D20746D707661725F363B0A20206D656469756D70207665633320746D707661725F393B0A2020746D707661725F39203D205F4C69676874436F6C6F72302E78797A3B0A2020746D707661725F39203D2028746D707661725F39202A20617474656E5F38293B0A20206D656469756D70207665633320766965774469725F31303B0A2020766965774469725F3130203D202D28786C765F544558434F4F524431293B0A20206D656469756D70207665633220746D707661725F31313B0A2020746D707661725F31312E78203D20646F74202828766965774469725F3130202D2028322E30202A200A2020202028646F742028746D707661725F342C20766965774469725F313029202A20746D707661725F34290A202029292C20746D707661725F37293B0A2020746D707661725F31312E79203D2028312E30202D20636C616D702028646F742028746D707661725F342C20766965774469725F3130292C20302E302C20312E3029293B0A20206D656469756D70207665633220746D707661725F31323B0A2020746D707661725F31322E78203D202828746D707661725F3131202A20746D707661725F313129202A2028746D707661725F3131202A20746D707661725F313129292E783B0A2020746D707661725F31322E79203D2028312E30202D205F476C6F7373696E657373293B0A20206C6F7770207665633420746D707661725F31333B0A2020746D707661725F3133203D207465787475726532442028756E6974795F4E4878526F7567686E6573732C20746D707661725F3132293B0A20206D656469756D70207665633420746D707661725F31343B0A2020746D707661725F31342E77203D20312E303B0A2020746D707661725F31342E78797A203D20282828746D707661725F33202A200A2020202028302E37373930383337202D20285F4D6574616C6C6963202A20302E3737393038333729290A202029202B20280A2020202028746D707661725F31332E77202A2031362E30290A2020202A200A202020206D697820287665633328302E323230393136332C20302E323230393136332C20302E32323039313633292C20746D707661725F332C2076656333285F4D6574616C6C696329290A20202929202A2028746D707661725F39202A20636C616D7020280A20202020646F742028746D707661725F342C20746D707661725F37290A20202C20302E302C20312E302929293B0A20206D656469756D70207665633420786C61745F7661726F75747075745F31353B0A2020786C61745F7661726F75747075745F31352E78797A203D20746D707661725F31342E78797A3B0A2020786C61745F7661726F75747075745F31352E77203D20312E303B0A2020676C5F46726167446174615B305D203D20786C61745F7661726F75747075745F31353B0A7D0A0A0A23656E6469660A00001B000000000000000100000000000000000000000000000000000000324F040C0500000000000000000000000000000000000000010000000C000000534841444F57535F435542450000000000000000000000000100000000000000000000000000000000000000324F040C050000002A000000050000000000000000000000020000000B000000444952454354494F4E414C000E000000534841444F57535F53435245454E0000921C00002376657273696F6E203130300A0A236966646566205645525445580A6174747269627574652076656334205F676C65735665727465783B0A6174747269627574652076656333205F676C65734E6F726D616C3B0A6174747269627574652076656334205F676C65734D756C7469546578436F6F7264303B0A6174747269627574652076656334205F676C65734D756C7469546578436F6F7264313B0A756E69666F726D2068696768702076656333205F576F726C64537061636543616D657261506F733B0A756E69666F726D206D656469756D70207665633420756E6974795F534841723B0A756E69666F726D206D656469756D70207665633420756E6974795F534841673B0A756E69666F726D206D656469756D70207665633420756E6974795F534841623B0A756E69666F726D206D656469756D70207665633420756E6974795F534842723B0A756E69666F726D206D656469756D70207665633420756E6974795F534842673B0A756E69666F726D206D656469756D70207665633420756E6974795F534842623B0A756E69666F726D206D656469756D70207665633420756E6974795F5348433B0A756E69666F726D206869676870206D61743420756E6974795F576F726C64546F536861646F775B345D3B0A756E69666F726D206869676870206D61743420756E6974795F4F626A656374546F576F726C643B0A756E69666F726D206869676870206D61743420756E6974795F576F726C64546F4F626A6563743B0A756E69666F726D206869676870206D61743420756E6974795F4D617472697856503B0A756E69666F726D2068696768702076656334205F4D61696E5465785F53543B0A756E69666F726D2068696768702076656334205F44657461696C416C6265646F4D61705F53543B0A756E69666F726D206D656469756D7020666C6F6174205F4D6574616C6C69633B0A756E69666F726D206D656469756D7020666C6F6174205F476C6F7373696E6573733B0A756E69666F726D206D656469756D7020666C6F6174205F55565365633B0A76617279696E67206869676870207665633420786C765F544558434F4F5244303B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244323B0A76617279696E67206869676870207665633420786C765F544558434F4F5244333B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244343B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244353B0A766F6964206D61696E2028290A7B0A20206D656469756D70207665633320746D707661725F313B0A2020746D707661725F31203D205F676C65734E6F726D616C3B0A20206869676870207665633220746D707661725F323B0A2020746D707661725F32203D205F676C65734D756C7469546578436F6F7264302E78793B0A20206869676870207665633220746D707661725F333B0A2020746D707661725F33203D205F676C65734D756C7469546578436F6F7264312E78793B0A20206D656469756D702076656333206E6F726D616C576F726C645F343B0A20206D656469756D702076656333206579655665635F353B0A20206D656469756D70207665633420746D707661725F363B0A20206D656469756D70207665633420746D707661725F373B0A20206D656469756D70207665633420746D707661725F383B0A20206869676870207665633420746D707661725F393B0A2020746D707661725F39203D2028756E6974795F4F626A656374546F576F726C64202A205F676C6573566572746578293B0A20206869676870207665633420746D707661725F31303B0A20206869676870207665633420746D707661725F31313B0A2020746D707661725F31312E77203D20312E303B0A2020746D707661725F31312E78797A203D205F676C65735665727465782E78797A3B0A2020746D707661725F3130203D2028756E6974795F4D61747269785650202A2028756E6974795F4F626A656374546F576F726C64202A20746D707661725F313129293B0A20206869676870207665633420746578636F6F72645F31323B0A2020746578636F6F72645F31322E7879203D2028285F676C65734D756C7469546578436F6F7264302E7879202A205F4D61696E5465785F53542E787929202B205F4D61696E5465785F53542E7A77293B0A20206869676870207665633220746D707661725F31333B0A202069662028285F5556536563203D3D20302E302929207B0A20202020746D707661725F3133203D20746D707661725F323B0A20207D20656C7365207B0A20202020746D707661725F3133203D20746D707661725F333B0A20207D3B0A2020746578636F6F72645F31322E7A77203D202828746D707661725F3133202A205F44657461696C416C6265646F4D61705F53542E787929202B205F44657461696C416C6265646F4D61705F53542E7A77293B0A20206869676870207665633320746D707661725F31343B0A2020746D707661725F3134203D206E6F726D616C697A652828746D707661725F392E78797A202D205F576F726C64537061636543616D657261506F7329293B0A20206579655665635F35203D20746D707661725F31343B0A202068696768702076656333206E6F726D5F31353B0A20206E6F726D5F3135203D20746D707661725F313B0A20206869676870206D61743320746D707661725F31363B0A2020746D707661725F31365B305D203D20756E6974795F576F726C64546F4F626A6563745B305D2E78797A3B0A2020746D707661725F31365B315D203D20756E6974795F576F726C64546F4F626A6563745B315D2E78797A3B0A2020746D707661725F31365B325D203D20756E6974795F576F726C64546F4F626A6563745B325D2E78797A3B0A20206869676870207665633320746D707661725F31373B0A2020746D707661725F3137203D206E6F726D616C697A6528286E6F726D5F3135202A20746D707661725F313629293B0A20206E6F726D616C576F726C645F34203D20746D707661725F31373B0A2020746D707661725F382E78797A203D206E6F726D616C576F726C645F343B0A2020746D707661725F362E78797A203D206579655665635F353B0A20206D656469756D70207665633420616D6269656E744F724C696768746D617055565F31383B0A2020616D6269656E744F724C696768746D617055565F31382E77203D20302E303B0A20206D656469756D70207665633420746D707661725F31393B0A2020746D707661725F31392E77203D20312E303B0A2020746D707661725F31392E78797A203D206E6F726D616C576F726C645F343B0A20206D656469756D702076656333207265735F32303B0A20206D656469756D70207665633320785F32313B0A2020785F32312E78203D20646F742028756E6974795F534841722C20746D707661725F3139293B0A2020785F32312E79203D20646F742028756E6974795F534841672C20746D707661725F3139293B0A2020785F32312E7A203D20646F742028756E6974795F534841622C20746D707661725F3139293B0A20206D656469756D7020766563332078315F32323B0A20206D656469756D70207665633420746D707661725F32333B0A2020746D707661725F3233203D20286E6F726D616C576F726C645F342E78797A7A202A206E6F726D616C576F726C645F342E797A7A78293B0A202078315F32322E78203D20646F742028756E6974795F534842722C20746D707661725F3233293B0A202078315F32322E79203D20646F742028756E6974795F534842672C20746D707661725F3233293B0A202078315F32322E7A203D20646F742028756E6974795F534842622C20746D707661725F3233293B0A20207265735F3230203D2028785F3231202B202878315F3232202B2028756E6974795F5348432E78797A202A200A2020202028286E6F726D616C576F726C645F342E78202A206E6F726D616C576F726C645F342E7829202D20286E6F726D616C576F726C645F342E79202A206E6F726D616C576F726C645F342E7929290A20202929293B0A20206D656469756D70207665633320746D707661725F32343B0A2020746D707661725F3234203D206D617820282828312E303535202A200A20202020706F7720286D617820287265735F32302C207665633328302E302C20302E302C20302E3029292C207665633328302E343136363636372C20302E343136363636372C20302E3431363636363729290A202029202D20302E303535292C207665633328302E302C20302E302C20302E3029293B0A20207265735F3230203D20746D707661725F32343B0A2020616D6269656E744F724C696768746D617055565F31382E78797A203D206D617820287665633328302E302C20302E302C20302E30292C20746D707661725F3234293B0A2020746D707661725F372E797A77203D20286579655665635F35202D2028322E30202A20280A20202020646F7420286E6F726D616C576F726C645F342C206579655665635F35290A2020202A206E6F726D616C576F726C645F342929293B0A20206D656469756D7020666C6F617420785F32353B0A2020785F3235203D2028312E30202D20636C616D702028646F7420286E6F726D616C576F726C645F342C200A202020202D286579655665635F35290A2020292C20302E302C20312E3029293B0A2020746D707661725F382E77203D202828785F3235202A20785F323529202A2028785F3235202A20785F323529293B0A2020746D707661725F362E77203D20636C616D702028285F476C6F7373696E657373202B2028312E30202D200A2020202028302E37373930383337202D20285F4D6574616C6C6963202A20302E3737393038333729290A202029292C20302E302C20312E30293B0A2020676C5F506F736974696F6E203D20746D707661725F31303B0A2020786C765F544558434F4F524430203D20746578636F6F72645F31323B0A2020786C765F544558434F4F524431203D20746D707661725F363B0A2020786C765F544558434F4F524432203D20616D6269656E744F724C696768746D617055565F31383B0A2020786C765F544558434F4F524433203D2028756E6974795F576F726C64546F536861646F775B305D202A2028756E6974795F4F626A656374546F576F726C64202A205F676C657356657274657829293B0A2020786C765F544558434F4F524434203D20746D707661725F373B0A2020786C765F544558434F4F524435203D20746D707661725F383B0A7D0A0A0A23656E6469660A23696664656620465241474D454E540A23657874656E73696F6E20474C5F4558545F7368616465725F746578747572655F6C6F64203A20656E61626C650A6C6F7770207665633420696D706C5F6C6F775F74657874757265437562654C6F64455854286C6F77702073616D706C6572437562652073616D706C65722C206869676870207665633320636F6F72642C206D656469756D7020666C6F6174206C6F64290A7B0A23696620646566696E656428474C5F4558545F7368616465725F746578747572655F6C6F64290A0972657475726E2074657874757265437562654C6F644558542873616D706C65722C20636F6F72642C206C6F64293B0A23656C73650A0972657475726E2074657874757265437562652873616D706C65722C20636F6F72642C206C6F64293B0A23656E6469660A7D0A0A756E69666F726D206D656469756D702076656334205F576F726C6453706163654C69676874506F73303B0A756E69666F726D206D656469756D702076656334205F4C69676874536861646F77446174613B0A756E69666F726D206C6F77702073616D706C65724375626520756E6974795F5370656343756265303B0A756E69666F726D206D656469756D70207665633420756E6974795F5370656343756265305F4844523B0A756E69666F726D206C6F77702076656334205F4C69676874436F6C6F72303B0A756E69666F726D2073616D706C6572324420756E6974795F4E4878526F7567686E6573733B0A756E69666F726D206D656469756D702076656334205F436F6C6F723B0A756E69666F726D2073616D706C65723244205F4D61696E5465783B0A756E69666F726D206D656469756D7020666C6F6174205F4D6574616C6C69633B0A756E69666F726D206D656469756D7020666C6F6174205F476C6F7373696E6573733B0A756E69666F726D2073616D706C65723244205F4F63636C7573696F6E4D61703B0A756E69666F726D206D656469756D7020666C6F6174205F4F63636C7573696F6E537472656E6774683B0A756E69666F726D2068696768702073616D706C65723244205F536861646F774D6170546578747572653B0A76617279696E67206869676870207665633420786C765F544558434F4F5244303B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244323B0A76617279696E67206869676870207665633420786C765F544558434F4F5244333B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244343B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244353B0A766F6964206D61696E2028290A7B0A20206D656469756D7020666C6F617420726C5F313B0A20206D656469756D7020666C6F6174207265616C74696D65536861646F77417474656E756174696F6E5F323B0A20206C6F7770207665633420746D707661725F333B0A2020746D707661725F33203D2074657874757265324420285F4D61696E5465782C20786C765F544558434F4F5244302E7879293B0A20206D656469756D70207665633320746D707661725F343B0A2020746D707661725F34203D20285F436F6C6F722E78797A202A20746D707661725F332E78797A293B0A20206D656469756D70207665633320746D707661725F353B0A20206D656469756D70207665633320746D707661725F363B0A2020746D707661725F36203D206D697820287665633328302E323230393136332C20302E323230393136332C20302E32323039313633292C20746D707661725F342C2076656333285F4D6574616C6C696329293B0A2020746D707661725F35203D2028746D707661725F34202A2028302E37373930383337202D20285F4D6574616C6C6963202A20302E373739303833372929293B0A20206D656469756D70207665633320746D707661725F373B0A2020746D707661725F37203D205F4C69676874436F6C6F72302E78797A3B0A20206C6F777020666C6F617420746D707661725F383B0A2020686967687020666C6F6174206C69676874536861646F7744617461585F393B0A20206D656469756D7020666C6F617420746D707661725F31303B0A2020746D707661725F3130203D205F4C69676874536861646F77446174612E783B0A20206C69676874536861646F7744617461585F39203D20746D707661725F31303B0A2020686967687020666C6F617420746D707661725F31313B0A2020746D707661725F3131203D206D61782028666C6F6174282874657874757265324420285F536861646F774D6170546578747572652C20786C765F544558434F4F5244332E7879292E78203E20786C765F544558434F4F5244332E7A29292C206C69676874536861646F7744617461585F39293B0A2020746D707661725F38203D20746D707661725F31313B0A20207265616C74696D65536861646F77417474656E756174696F6E5F32203D20746D707661725F383B0A20206D656469756D7020666C6F6174206F63635F31323B0A20206C6F777020666C6F617420746D707661725F31333B0A2020746D707661725F3133203D2074657874757265324420285F4F63636C7573696F6E4D61702C20786C765F544558434F4F5244302E7879292E793B0A20206F63635F3132203D20746D707661725F31333B0A2020726C5F31203D20646F742028786C765F544558434F4F5244342E797A772C205F576F726C6453706163654C69676874506F73302E78797A293B0A20206D656469756D7020666C6F6174206F63636C7573696F6E5F31343B0A20206F63636C7573696F6E5F3134203D202828312E30202D205F4F63636C7573696F6E537472656E67746829202B20286F63635F3132202A205F4F63636C7573696F6E537472656E67746829293B0A20206869676870207665633420746D707661725F31353B0A2020746D707661725F3135203D20756E6974795F5370656343756265305F4844523B0A20206D656469756D7020666C6F617420746D707661725F31363B0A2020746D707661725F3136203D2028312E30202D205F476C6F7373696E657373293B0A20206D656469756D702076656334206864725F31373B0A20206864725F3137203D20746D707661725F31353B0A20206D656469756D70207665633420746D707661725F31383B0A2020746D707661725F31382E78797A203D20786C765F544558434F4F5244342E797A773B0A2020746D707661725F31382E77203D202828746D707661725F3136202A2028312E37202D200A2020202028302E37202A20746D707661725F3136290A20202929202A20362E30293B0A20206C6F7770207665633420746D707661725F31393B0A2020746D707661725F3139203D20696D706C5F6C6F775F74657874757265437562654C6F644558542028756E6974795F5370656343756265302C20786C765F544558434F4F5244342E797A772C20746D707661725F31382E77293B0A20206D656469756D70207665633420746D707661725F32303B0A2020746D707661725F3230203D20746D707661725F31393B0A20206D656469756D70207665633220746D707661725F32313B0A2020746D707661725F32312E78203D202828726C5F31202A20726C5F3129202A2028726C5F31202A20726C5F3129293B0A2020746D707661725F32312E79203D20746D707661725F31363B0A20206C6F7770207665633420746D707661725F32323B0A2020746D707661725F3232203D207465787475726532442028756E6974795F4E4878526F7567686E6573732C20746D707661725F3231293B0A20206D656469756D70207665633420746D707661725F32333B0A2020746D707661725F32332E77203D20312E303B0A2020746D707661725F32332E78797A203D202828280A2020202028786C765F544558434F4F5244322E78797A202A206F63636C7573696F6E5F3134290A2020202A20746D707661725F3529202B20280A202020202828286864725F31372E78202A20280A202020202020286864725F31372E77202A2028746D707661725F32302E77202D20312E3029290A20202020202B20312E302929202A20746D707661725F32302E78797A29202A206F63636C7573696F6E5F3134290A2020202A200A202020206D69782028746D707661725F362C20786C765F544558434F4F5244312E7777772C20786C765F544558434F4F5244352E777777290A20202929202B202828746D707661725F35202B200A202020202828746D707661725F32322E77202A2031362E3029202A20746D707661725F36290A202029202A20280A2020202028746D707661725F37202A207265616C74696D65536861646F77417474656E756174696F6E5F32290A2020202A200A20202020636C616D702028646F742028786C765F544558434F4F5244352E78797A2C205F576F726C6453706163654C69676874506F73302E78797A292C20302E302C20312E30290A20202929293B0A20206D656469756D70207665633420786C61745F7661726F75747075745F32343B0A2020786C61745F7661726F75747075745F32342E78797A203D20746D707661725F32332E78797A3B0A2020786C61745F7661726F75747075745F32342E77203D20312E303B0A2020676C5F46726167446174615B305D203D20786C61745F7661726F75747075745F32343B0A7D0A0A0A23656E6469660A00001B000000000000000100000000000000000000000000000000000000324F040C0500000066000000040000000000000000000000030000000B000000444952454354494F4E414C000E000000534841444F57535F53435245454E00000E0000005645525445584C494748545F4F4E0000882A00002376657273696F6E203130300A0A236966646566205645525445580A6174747269627574652076656334205F676C65735665727465783B0A6174747269627574652076656333205F676C65734E6F726D616C3B0A6174747269627574652076656334205F676C65734D756C7469546578436F6F7264303B0A6174747269627574652076656334205F676C65734D756C7469546578436F6F7264313B0A756E69666F726D2068696768702076656333205F576F726C64537061636543616D657261506F733B0A756E69666F726D206869676870207665633420756E6974795F344C69676874506F7358303B0A756E69666F726D206869676870207665633420756E6974795F344C69676874506F7359303B0A756E69666F726D206869676870207665633420756E6974795F344C69676874506F735A303B0A756E69666F726D206D656469756D70207665633420756E6974795F344C69676874417474656E303B0A756E69666F726D206D656469756D70207665633420756E6974795F4C69676874436F6C6F725B385D3B0A756E69666F726D206D656469756D70207665633420756E6974795F534842723B0A756E69666F726D206D656469756D70207665633420756E6974795F534842673B0A756E69666F726D206D656469756D70207665633420756E6974795F534842623B0A756E69666F726D206D656469756D70207665633420756E6974795F5348433B0A756E69666F726D206869676870206D61743420756E6974795F4F626A656374546F576F726C643B0A756E69666F726D206869676870206D61743420756E6974795F576F726C64546F4F626A6563743B0A756E69666F726D206869676870206D61743420756E6974795F4D617472697856503B0A756E69666F726D2068696768702076656334205F4D61696E5465785F53543B0A756E69666F726D2068696768702076656334205F44657461696C416C6265646F4D61705F53543B0A756E69666F726D206D656469756D7020666C6F6174205F55565365633B0A76617279696E67206869676870207665633420786C765F544558434F4F5244303B0A76617279696E67206D656469756D70207665633320786C765F544558434F4F5244313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244323B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244325F313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244325F323B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244353B0A76617279696E67206869676870207665633220786C765F544558434F4F5244363B0A76617279696E67206869676870207665633320786C765F544558434F4F5244383B0A766F6964206D61696E2028290A7B0A20206D656469756D70207665633320746D707661725F313B0A2020746D707661725F31203D205F676C65734E6F726D616C3B0A20206869676870207665633220746D707661725F323B0A2020746D707661725F32203D205F676C65734D756C7469546578436F6F7264302E78793B0A20206869676870207665633220746D707661725F333B0A2020746D707661725F33203D205F676C65734D756C7469546578436F6F7264312E78793B0A20206D656469756D70207665633420746D707661725F343B0A20206D656469756D70207665633420746D707661725F353B0A20206D656469756D70207665633420746D707661725F363B0A20206869676870207665633220746D707661725F373B0A20206869676870207665633320746D707661725F383B0A20206869676870207665633420746D707661725F393B0A2020746D707661725F39203D2028756E6974795F4F626A656374546F576F726C64202A205F676C6573566572746578293B0A2020746D707661725F38203D20746D707661725F392E78797A3B0A20206869676870207665633420746D707661725F31303B0A20206869676870207665633420746D707661725F31313B0A2020746D707661725F31312E77203D20312E303B0A2020746D707661725F31312E78797A203D205F676C65735665727465782E78797A3B0A2020746D707661725F3130203D2028756E6974795F4D61747269785650202A2028756E6974795F4F626A656374546F576F726C64202A20746D707661725F313129293B0A20206869676870207665633420746578636F6F72645F31323B0A2020746578636F6F72645F31322E7879203D2028285F676C65734D756C7469546578436F6F7264302E7879202A205F4D61696E5465785F53542E787929202B205F4D61696E5465785F53542E7A77293B0A20206869676870207665633220746D707661725F31333B0A202069662028285F5556536563203D3D20302E302929207B0A20202020746D707661725F3133203D20746D707661725F323B0A20207D20656C7365207B0A20202020746D707661725F3133203D20746D707661725F333B0A20207D3B0A2020746578636F6F72645F31322E7A77203D202828746D707661725F3133202A205F44657461696C416C6265646F4D61705F53542E787929202B205F44657461696C416C6265646F4D61705F53542E7A77293B0A20206D656469756D70207665633320746D707661725F31343B0A202068696768702076656333206E5F31353B0A20206E5F3135203D2028746D707661725F392E78797A202D205F576F726C64537061636543616D657261506F73293B0A2020746D707661725F3134203D206E5F31353B0A202068696768702076656333206E6F726D5F31363B0A20206E6F726D5F3136203D20746D707661725F313B0A20206869676870206D61743320746D707661725F31373B0A2020746D707661725F31375B305D203D20756E6974795F576F726C64546F4F626A6563745B305D2E78797A3B0A2020746D707661725F31375B315D203D20756E6974795F576F726C64546F4F626A6563745B315D2E78797A3B0A2020746D707661725F31375B325D203D20756E6974795F576F726C64546F4F626A6563745B325D2E78797A3B0A20206869676870207665633320746D707661725F31383B0A2020746D707661725F3138203D206E6F726D616C697A6528286E6F726D5F3136202A20746D707661725F313729293B0A2020746D707661725F342E78797A203D207665633328302E302C20302E302C20302E30293B0A2020746D707661725F352E78797A203D207665633328302E302C20302E302C20302E30293B0A2020746D707661725F362E78797A203D20746D707661725F31383B0A20206D656469756D702076656333206E6F726D616C576F726C645F31393B0A20206E6F726D616C576F726C645F3139203D20746D707661725F31383B0A20206D656469756D70207665633420616D6269656E744F724C696768746D617055565F32303B0A2020616D6269656E744F724C696768746D617055565F32302E77203D20302E303B0A202068696768702076656333206C69676874436F6C6F72305F32313B0A20206C69676874436F6C6F72305F3231203D20756E6974795F4C69676874436F6C6F725B305D2E78797A3B0A202068696768702076656333206C69676874436F6C6F72315F32323B0A20206C69676874436F6C6F72315F3232203D20756E6974795F4C69676874436F6C6F725B315D2E78797A3B0A202068696768702076656333206C69676874436F6C6F72325F32333B0A20206C69676874436F6C6F72325F3233203D20756E6974795F4C69676874436F6C6F725B325D2E78797A3B0A202068696768702076656333206C69676874436F6C6F72335F32343B0A20206C69676874436F6C6F72335F3234203D20756E6974795F4C69676874436F6C6F725B335D2E78797A3B0A202068696768702076656334206C69676874417474656E53715F32353B0A20206C69676874417474656E53715F3235203D20756E6974795F344C69676874417474656E303B0A202068696768702076656333206E6F726D616C5F32363B0A20206E6F726D616C5F3236203D206E6F726D616C576F726C645F31393B0A20206869676870207665633320636F6C5F32373B0A202068696768702076656334206E646F746C5F32383B0A202068696768702076656334206C656E67746853715F32393B0A20206869676870207665633420746D707661725F33303B0A2020746D707661725F3330203D2028756E6974795F344C69676874506F735830202D20746D707661725F392E78293B0A20206869676870207665633420746D707661725F33313B0A2020746D707661725F3331203D2028756E6974795F344C69676874506F735930202D20746D707661725F392E79293B0A20206869676870207665633420746D707661725F33323B0A2020746D707661725F3332203D2028756E6974795F344C69676874506F735A30202D20746D707661725F392E7A293B0A20206C656E67746853715F3239203D2028746D707661725F3330202A20746D707661725F3330293B0A20206C656E67746853715F3239203D20286C656E67746853715F3239202B2028746D707661725F3331202A20746D707661725F333129293B0A20206C656E67746853715F3239203D20286C656E67746853715F3239202B2028746D707661725F3332202A20746D707661725F333229293B0A20206869676870207665633420746D707661725F33333B0A2020746D707661725F3333203D206D617820286C656E67746853715F32392C20766563342831652D30362C2031652D30362C2031652D30362C2031652D303629293B0A20206C656E67746853715F3239203D20746D707661725F33333B0A20206E646F746C5F3238203D2028746D707661725F3330202A206E6F726D616C5F32362E78293B0A20206E646F746C5F3238203D20286E646F746C5F3238202B2028746D707661725F3331202A206E6F726D616C5F32362E7929293B0A20206E646F746C5F3238203D20286E646F746C5F3238202B2028746D707661725F3332202A206E6F726D616C5F32362E7A29293B0A20206869676870207665633420746D707661725F33343B0A2020746D707661725F3334203D206D617820287665633428302E302C20302E302C20302E302C20302E30292C20286E646F746C5F3238202A20696E76657273657371727428746D707661725F33332929293B0A20206E646F746C5F3238203D20746D707661725F33343B0A20206869676870207665633420746D707661725F33353B0A2020746D707661725F3335203D2028746D707661725F3334202A2028312E302F2828312E30202B200A2020202028746D707661725F3333202A206C69676874417474656E53715F3235290A2020292929293B0A2020636F6C5F3237203D20286C69676874436F6C6F72305F3231202A20746D707661725F33352E78293B0A2020636F6C5F3237203D2028636F6C5F3237202B20286C69676874436F6C6F72315F3232202A20746D707661725F33352E7929293B0A2020636F6C5F3237203D2028636F6C5F3237202B20286C69676874436F6C6F72325F3233202A20746D707661725F33352E7A29293B0A2020636F6C5F3237203D2028636F6C5F3237202B20286C69676874436F6C6F72335F3234202A20746D707661725F33352E7729293B0A2020616D6269656E744F724C696768746D617055565F32302E78797A203D20636F6C5F32373B0A20206D656469756D7020766563332078315F33363B0A20206D656469756D70207665633420746D707661725F33373B0A2020746D707661725F3337203D20286E6F726D616C576F726C645F31392E78797A7A202A206E6F726D616C576F726C645F31392E797A7A78293B0A202078315F33362E78203D20646F742028756E6974795F534842722C20746D707661725F3337293B0A202078315F33362E79203D20646F742028756E6974795F534842672C20746D707661725F3337293B0A202078315F33362E7A203D20646F742028756E6974795F534842622C20746D707661725F3337293B0A2020616D6269656E744F724C696768746D617055565F32302E78797A203D202828616D6269656E744F724C696768746D617055565F32302E78797A202A20280A2020202028616D6269656E744F724C696768746D617055565F32302E78797A202A202828616D6269656E744F724C696768746D617055565F32302E78797A202A20302E33303533303629202B20302E3638323137313129290A2020202B20302E30313235323238382929202B202878315F3336202B2028756E6974795F5348432E78797A202A200A2020202028286E6F726D616C576F726C645F31392E78202A206E6F726D616C576F726C645F31392E7829202D20286E6F726D616C576F726C645F31392E79202A206E6F726D616C576F726C645F31392E7929290A20202929293B0A2020676C5F506F736974696F6E203D20746D707661725F31303B0A2020786C765F544558434F4F524430203D20746578636F6F72645F31323B0A2020786C765F544558434F4F524431203D20746D707661725F31343B0A2020786C765F544558434F4F524432203D20746D707661725F343B0A2020786C765F544558434F4F5244325F31203D20746D707661725F353B0A2020786C765F544558434F4F5244325F32203D20746D707661725F363B0A2020786C765F544558434F4F524435203D20616D6269656E744F724C696768746D617055565F32303B0A2020786C765F544558434F4F524436203D20746D707661725F373B0A2020786C765F544558434F4F524438203D20746D707661725F383B0A7D0A0A0A23656E6469660A23696664656620465241474D454E540A23657874656E73696F6E20474C5F4558545F7368616465725F746578747572655F6C6F64203A20656E61626C650A6C6F7770207665633420696D706C5F6C6F775F74657874757265437562654C6F64455854286C6F77702073616D706C6572437562652073616D706C65722C206869676870207665633320636F6F72642C206D656469756D7020666C6F6174206C6F64290A7B0A23696620646566696E656428474C5F4558545F7368616465725F746578747572655F6C6F64290A0972657475726E2074657874757265437562654C6F644558542873616D706C65722C20636F6F72642C206C6F64293B0A23656C73650A0972657475726E2074657874757265437562652873616D706C65722C20636F6F72642C206C6F64293B0A23656E6469660A7D0A0A756E69666F726D2068696768702076656333205F576F726C64537061636543616D657261506F733B0A756E69666F726D206D656469756D702076656334205F576F726C6453706163654C69676874506F73303B0A756E69666F726D206D656469756D70207665633420756E6974795F534841723B0A756E69666F726D206D656469756D70207665633420756E6974795F534841673B0A756E69666F726D206D656469756D70207665633420756E6974795F534841623B0A756E69666F726D206869676870206D61743420756E6974795F576F726C64546F536861646F775B345D3B0A756E69666F726D206D656469756D702076656334205F4C69676874536861646F77446174613B0A756E69666F726D206869676870207665633420756E6974795F536861646F774661646543656E746572416E64547970653B0A756E69666F726D206869676870206D61743420756E6974795F4D6174726978563B0A756E69666F726D206C6F77702073616D706C65724375626520756E6974795F5370656343756265303B0A756E69666F726D206D656469756D70207665633420756E6974795F5370656343756265305F4844523B0A756E69666F726D206C6F77702076656334205F4C69676874436F6C6F72303B0A756E69666F726D206D656469756D702076656334205F436F6C6F723B0A756E69666F726D2073616D706C65723244205F4D61696E5465783B0A756E69666F726D206D656469756D7020666C6F6174205F4D6574616C6C69633B0A756E69666F726D206D656469756D7020666C6F6174205F476C6F7373696E6573733B0A756E69666F726D2073616D706C65723244205F4F63636C7573696F6E4D61703B0A756E69666F726D206D656469756D7020666C6F6174205F4F63636C7573696F6E537472656E6774683B0A756E69666F726D2068696768702073616D706C65723244205F536861646F774D6170546578747572653B0A76617279696E67206869676870207665633420786C765F544558434F4F5244303B0A76617279696E67206D656469756D70207665633320786C765F544558434F4F5244313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244325F323B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244353B0A76617279696E67206869676870207665633320786C765F544558434F4F5244383B0A766F6964206D61696E2028290A7B0A20206D656469756D70207665633420635F313B0A20206C6F777020666C6F617420617474656E5F323B0A20206C6F7770207665633420746D707661725F333B0A2020746D707661725F33203D2074657874757265324420285F4D61696E5465782C20786C765F544558434F4F5244302E7879293B0A20206D656469756D70207665633320746D707661725F343B0A2020746D707661725F34203D20285F436F6C6F722E78797A202A20746D707661725F332E78797A293B0A20206D656469756D70207665633320746D707661725F353B0A20206D656469756D70207665633320746D707661725F363B0A2020746D707661725F36203D206D697820287665633328302E323230393136332C20302E323230393136332C20302E32323039313633292C20746D707661725F342C2076656333285F4D6574616C6C696329293B0A20206D656469756D7020666C6F617420746D707661725F373B0A2020746D707661725F37203D2028302E37373930383337202D20285F4D6574616C6C6963202A20302E3737393038333729293B0A2020746D707661725F35203D2028746D707661725F34202A20746D707661725F37293B0A20206D656469756D70207665633320746D707661725F383B0A2020746D707661725F38203D206E6F726D616C697A6528786C765F544558434F4F5244325F322E78797A293B0A20206D656469756D70207665633320746D707661725F393B0A2020746D707661725F39203D206E6F726D616C697A6528786C765F544558434F4F524431293B0A20206D656469756D70207665633320746D707661725F31303B0A2020746D707661725F3130203D205F4C69676874436F6C6F72302E78797A3B0A20206D656469756D7020666C6F6174207265616C74696D65536861646F77417474656E756174696F6E5F31313B0A20206869676870207665633420765F31323B0A2020765F31322E78203D20756E6974795F4D6174726978565B305D2E7A3B0A2020765F31322E79203D20756E6974795F4D6174726978565B315D2E7A3B0A2020765F31322E7A203D20756E6974795F4D6174726978565B325D2E7A3B0A2020765F31322E77203D20756E6974795F4D6174726978565B335D2E7A3B0A20206869676870207665633320746D707661725F31333B0A2020746D707661725F3133203D2028786C765F544558434F4F524438202D20756E6974795F536861646F774661646543656E746572416E64547970652E78797A293B0A20206D656469756D7020666C6F617420746D707661725F31343B0A2020686967687020666C6F617420746D707661725F31353B0A2020746D707661725F3135203D20636C616D70202828280A202020206D69782028646F742028285F576F726C64537061636543616D657261506F73202D20786C765F544558434F4F524438292C20765F31322E78797A292C207371727428646F742028746D707661725F31332C20746D707661725F313329292C20756E6974795F536861646F774661646543656E746572416E64547970652E77290A2020202A205F4C69676874536861646F77446174612E7A29202B205F4C69676874536861646F77446174612E77292C20302E302C20312E30293B0A2020746D707661725F3134203D20746D707661725F31353B0A20206869676870207665633420746D707661725F31363B0A2020746D707661725F31362E77203D20312E303B0A2020746D707661725F31362E78797A203D20786C765F544558434F4F5244383B0A20206C6F777020666C6F617420746D707661725F31373B0A20206869676870207665633420736861646F77436F6F72645F31383B0A2020736861646F77436F6F72645F3138203D2028756E6974795F576F726C64546F536861646F775B305D202A20746D707661725F3136293B0A2020686967687020666C6F6174206C69676874536861646F7744617461585F31393B0A20206D656469756D7020666C6F617420746D707661725F32303B0A2020746D707661725F3230203D205F4C69676874536861646F77446174612E783B0A20206C69676874536861646F7744617461585F3139203D20746D707661725F32303B0A2020686967687020666C6F617420746D707661725F32313B0A2020746D707661725F3231203D206D61782028666C6F6174282874657874757265324420285F536861646F774D6170546578747572652C20736861646F77436F6F72645F31382E7879292E78203E20736861646F77436F6F72645F31382E7A29292C206C69676874536861646F7744617461585F3139293B0A2020746D707661725F3137203D20746D707661725F32313B0A20207265616C74696D65536861646F77417474656E756174696F6E5F3131203D20746D707661725F31373B0A20206D656469756D7020666C6F617420746D707661725F32323B0A2020746D707661725F3232203D20636C616D702028287265616C74696D65536861646F77417474656E756174696F6E5F3131202B20746D707661725F3134292C20302E302C20312E30293B0A2020617474656E5F32203D20746D707661725F32323B0A20206D656469756D7020666C6F6174206F63635F32333B0A20206C6F777020666C6F617420746D707661725F32343B0A2020746D707661725F3234203D2074657874757265324420285F4F63636C7573696F6E4D61702C20786C765F544558434F4F5244302E7879292E793B0A20206F63635F3233203D20746D707661725F32343B0A20206D656469756D7020666C6F617420617474656E5F32353B0A2020617474656E5F3235203D20617474656E5F323B0A20206D656469756D7020666C6F6174206F63636C7573696F6E5F32363B0A20206F63636C7573696F6E5F3236203D202828312E30202D205F4F63636C7573696F6E537472656E67746829202B20286F63635F3233202A205F4F63636C7573696F6E537472656E67746829293B0A20206869676870207665633420746D707661725F32373B0A2020746D707661725F3237203D20756E6974795F5370656343756265305F4844523B0A20206D656469756D70207665633420746D707661725F32383B0A2020746D707661725F32382E77203D20312E303B0A2020746D707661725F32382E78797A203D20746D707661725F383B0A20206D656469756D70207665633320785F32393B0A2020785F32392E78203D20646F742028756E6974795F534841722C20746D707661725F3238293B0A2020785F32392E79203D20646F742028756E6974795F534841672C20746D707661725F3238293B0A2020785F32392E7A203D20646F742028756E6974795F534841622C20746D707661725F3238293B0A20206D656469756D70207665633320746D707661725F33303B0A20206D656469756D7020666C6F617420746D707661725F33313B0A2020746D707661725F3331203D2028312E30202D205F476C6F7373696E657373293B0A2020746D707661725F3330203D2028746D707661725F39202D2028322E30202A20280A20202020646F742028746D707661725F382C20746D707661725F39290A2020202A20746D707661725F382929293B0A20206D656469756D702076656334206864725F33323B0A20206864725F3332203D20746D707661725F32373B0A20206D656469756D70207665633420746D707661725F33333B0A2020746D707661725F33332E78797A203D20746D707661725F33303B0A2020746D707661725F33332E77203D202828746D707661725F3331202A2028312E37202D200A2020202028302E37202A20746D707661725F3331290A20202929202A20362E30293B0A20206C6F7770207665633420746D707661725F33343B0A2020746D707661725F3334203D20696D706C5F6C6F775F74657874757265437562654C6F644558542028756E6974795F5370656343756265302C20746D707661725F33302C20746D707661725F33332E77293B0A20206D656469756D70207665633420746D707661725F33353B0A2020746D707661725F3335203D20746D707661725F33343B0A20206D656469756D70207665633320766965774469725F33363B0A2020766965774469725F3336203D202D28746D707661725F39293B0A20206D656469756D7020666C6F61742073706563756C61725465726D5F33373B0A20206D656469756D70207665633320746D707661725F33383B0A20206D656469756D70207665633320696E5665635F33393B0A2020696E5665635F3339203D20285F576F726C6453706163654C69676874506F73302E78797A202B20766965774469725F3336293B0A2020746D707661725F3338203D2028696E5665635F3339202A20696E766572736573717274286D61782028302E3030312C200A20202020646F742028696E5665635F33392C20696E5665635F3339290A20202929293B0A20206D656469756D7020666C6F617420746D707661725F34303B0A2020746D707661725F3430203D20636C616D702028646F742028746D707661725F382C20746D707661725F3338292C20302E302C20312E30293B0A20206D656469756D7020666C6F617420746D707661725F34313B0A2020746D707661725F3431203D2028746D707661725F3331202A20746D707661725F3331293B0A202073706563756C61725465726D5F3337203D202828746D707661725F3431202F20280A20202020286D61782028302E33322C20636C616D702028646F7420285F576F726C6453706163654C69676874506F73302E78797A2C20746D707661725F3338292C20302E302C20312E302929202A2028312E35202B20746D707661725F343129290A2020202A200A20202020282828746D707661725F3430202A20746D707661725F343029202A202828746D707661725F3431202A20746D707661725F343129202D20312E302929202B20312E3030303031290A20202929202D20302E30303031293B0A20206D656469756D7020666C6F617420746D707661725F34323B0A2020746D707661725F3432203D20636C616D70202873706563756C61725465726D5F33372C20302E302C203130302E30293B0A202073706563756C61725465726D5F3337203D20746D707661725F34323B0A20206D656469756D7020666C6F617420785F34333B0A2020785F3433203D2028312E30202D20636C616D702028646F742028746D707661725F382C20766965774469725F3336292C20302E302C20312E3029293B0A20206D656469756D70207665633320746D707661725F34343B0A2020746D707661725F3434203D202828280A202020202828746D707661725F35202B2028746D707661725F3432202A20746D707661725F362929202A2028746D707661725F3130202A20617474656E5F323529290A2020202A200A20202020636C616D702028646F742028746D707661725F382C205F576F726C6453706163654C69676874506F73302E78797A292C20302E302C20312E30290A202029202B20280A20202020286D617820282828312E303535202A200A202020202020706F7720286D617820287665633328302E302C20302E302C20302E30292C2028786C765F544558434F4F5244352E78797A202B20785F323929292C207665633328302E343136363636372C20302E343136363636372C20302E3431363636363729290A2020202029202D20302E303535292C207665633328302E302C20302E302C20302E302929202A206F63636C7573696F6E5F3236290A2020202A20746D707661725F352929202B2028280A2020202028312E30202D202828746D707661725F3431202A20746D707661725F333129202A20302E323829290A2020202A200A202020202828286864725F33322E78202A20280A202020202020286864725F33322E77202A2028746D707661725F33352E77202D20312E3029290A20202020202B20312E302929202A20746D707661725F33352E78797A29202A206F63636C7573696F6E5F3236290A202029202A206D69782028746D707661725F362C2076656333280A20202020636C616D702028285F476C6F7373696E657373202B2028312E30202D20746D707661725F3729292C20302E302C20312E30290A2020292C2076656333280A202020202828785F3433202A20785F343329202A2028785F3433202A20785F343329290A2020292929293B0A20206D656469756D70207665633420746D707661725F34353B0A2020746D707661725F34352E77203D20312E303B0A2020746D707661725F34352E78797A203D20746D707661725F34343B0A2020635F312E77203D20746D707661725F34352E773B0A2020635F312E78797A203D20746D707661725F34343B0A20206D656469756D70207665633420786C61745F7661726F75747075745F34363B0A2020786C61745F7661726F75747075745F34362E78797A203D20635F312E78797A3B0A2020786C61745F7661726F75747075745F34362E77203D20312E303B0A2020676C5F46726167446174615B305D203D20786C61745F7661726F75747075745F34363B0A7D0A0A0A23656E6469660A1B000000000000000100000000000000000000000000000000000000324F040C0500000000000000000000000000000000000000010000000C000000504F494E545F434F4F4B49450000000000000000000000000100000000000000000000000000000000000000324F040C0500000019000000030000000000000000000000020000000B000000444952454354494F4E414C000E000000534841444F57535F53435245454E0000AD1100002376657273696F6E203130300A0A236966646566205645525445580A6174747269627574652076656334205F676C65735665727465783B0A6174747269627574652076656333205F676C65734E6F726D616C3B0A6174747269627574652076656334205F676C65734D756C7469546578436F6F7264303B0A6174747269627574652076656334205F676C65734D756C7469546578436F6F7264313B0A756E69666F726D2068696768702076656333205F576F726C64537061636543616D657261506F733B0A756E69666F726D206D656469756D702076656334205F576F726C6453706163654C69676874506F73303B0A756E69666F726D206869676870206D61743420756E6974795F4F626A656374546F576F726C643B0A756E69666F726D206869676870206D61743420756E6974795F576F726C64546F4F626A6563743B0A756E69666F726D206869676870206D61743420756E6974795F4D617472697856503B0A756E69666F726D2068696768702076656334205F4D61696E5465785F53543B0A756E69666F726D2068696768702076656334205F44657461696C416C6265646F4D61705F53543B0A756E69666F726D206D656469756D7020666C6F6174205F55565365633B0A76617279696E67206869676870207665633420786C765F544558434F4F5244303B0A76617279696E67206869676870207665633320786C765F544558434F4F5244313B0A76617279696E67206869676870207665633220786C765F544558434F4F5244323B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244333B0A76617279696E67206D656469756D70207665633320786C765F544558434F4F5244343B0A76617279696E67206D656469756D70207665633320786C765F544558434F4F5244353B0A766F6964206D61696E2028290A7B0A20206D656469756D70207665633320746D707661725F313B0A2020746D707661725F31203D205F676C65734E6F726D616C3B0A20206869676870207665633220746D707661725F323B0A2020746D707661725F32203D205F676C65734D756C7469546578436F6F7264302E78793B0A20206869676870207665633220746D707661725F333B0A2020746D707661725F33203D205F676C65734D756C7469546578436F6F7264312E78793B0A20206D656469756D702076656333206E6F726D616C576F726C645F343B0A20206D656469756D702076656333206579655665635F353B0A20206D656469756D702076656333206C696768744469725F363B0A20206869676870207665633220746D707661725F373B0A20206D656469756D70207665633420746D707661725F383B0A20206869676870207665633420746D707661725F393B0A2020746D707661725F39203D2028756E6974795F4F626A656374546F576F726C64202A205F676C6573566572746578293B0A20206869676870207665633420746D707661725F31303B0A20206869676870207665633420746D707661725F31313B0A2020746D707661725F31312E77203D20312E303B0A2020746D707661725F31312E78797A203D205F676C65735665727465782E78797A3B0A2020746D707661725F3130203D2028756E6974795F4D61747269785650202A2028756E6974795F4F626A656374546F576F726C64202A20746D707661725F313129293B0A20206869676870207665633420746578636F6F72645F31323B0A2020746578636F6F72645F31322E7879203D2028285F676C65734D756C7469546578436F6F7264302E7879202A205F4D61696E5465785F53542E787929202B205F4D61696E5465785F53542E7A77293B0A20206869676870207665633220746D707661725F31333B0A202069662028285F5556536563203D3D20302E302929207B0A20202020746D707661725F3133203D20746D707661725F323B0A20207D20656C7365207B0A20202020746D707661725F3133203D20746D707661725F333B0A20207D3B0A2020746578636F6F72645F31322E7A77203D202828746D707661725F3133202A205F44657461696C416C6265646F4D61705F53542E787929202B205F44657461696C416C6265646F4D61705F53542E7A77293B0A20206869676870207665633320746D707661725F31343B0A2020746D707661725F3134203D20285F576F726C6453706163654C69676874506F73302E78797A202D2028746D707661725F392E78797A202A205F576F726C6453706163654C69676874506F73302E7729293B0A20206C696768744469725F36203D20746D707661725F31343B0A20206869676870207665633320746D707661725F31353B0A2020746D707661725F3135203D206E6F726D616C697A652828746D707661725F392E78797A202D205F576F726C64537061636543616D657261506F7329293B0A20206579655665635F35203D20746D707661725F31353B0A202068696768702076656333206E6F726D5F31363B0A20206E6F726D5F3136203D20746D707661725F313B0A20206869676870206D61743320746D707661725F31373B0A2020746D707661725F31375B305D203D20756E6974795F576F726C64546F4F626A6563745B305D2E78797A3B0A2020746D707661725F31375B315D203D20756E6974795F576F726C64546F4F626A6563745B315D2E78797A3B0A2020746D707661725F31375B325D203D20756E6974795F576F726C64546F4F626A6563745B325D2E78797A3B0A20206869676870207665633320746D707661725F31383B0A2020746D707661725F3138203D206E6F726D616C697A6528286E6F726D5F3136202A20746D707661725F313729293B0A20206E6F726D616C576F726C645F34203D20746D707661725F31383B0A2020746D707661725F382E797A77203D20286579655665635F35202D2028322E30202A20280A20202020646F7420286E6F726D616C576F726C645F342C206579655665635F35290A2020202A206E6F726D616C576F726C645F342929293B0A2020676C5F506F736974696F6E203D20746D707661725F31303B0A2020786C765F544558434F4F524430203D20746578636F6F72645F31323B0A2020786C765F544558434F4F524431203D20746D707661725F392E78797A3B0A2020786C765F544558434F4F524432203D20746D707661725F373B0A2020786C765F544558434F4F524433203D20746D707661725F383B0A2020786C765F544558434F4F524434203D206C696768744469725F363B0A2020786C765F544558434F4F524435203D206E6F726D616C576F726C645F343B0A7D0A0A0A23656E6469660A23696664656620465241474D454E540A756E69666F726D206869676870206D61743420756E6974795F576F726C64546F536861646F775B345D3B0A756E69666F726D206D656469756D702076656334205F4C69676874536861646F77446174613B0A756E69666F726D206C6F77702076656334205F4C69676874436F6C6F72303B0A756E69666F726D2073616D706C6572324420756E6974795F4E4878526F7567686E6573733B0A756E69666F726D206D656469756D702076656334205F436F6C6F723B0A756E69666F726D2073616D706C65723244205F4D61696E5465783B0A756E69666F726D206D656469756D7020666C6F6174205F4D6574616C6C69633B0A756E69666F726D206D656469756D7020666C6F6174205F476C6F7373696E6573733B0A756E69666F726D2068696768702073616D706C65723244205F536861646F774D6170546578747572653B0A76617279696E67206869676870207665633420786C765F544558434F4F5244303B0A76617279696E67206869676870207665633320786C765F544558434F4F5244313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244333B0A76617279696E67206D656469756D70207665633320786C765F544558434F4F5244343B0A76617279696E67206D656469756D70207665633320786C765F544558434F4F5244353B0A766F6964206D61696E2028290A7B0A20206C6F777020666C6F617420617474656E5F313B0A20206D656469756D70207665633320635F323B0A20206C6F7770207665633420746D707661725F333B0A2020746D707661725F33203D2074657874757265324420285F4D61696E5465782C20786C765F544558434F4F5244302E7879293B0A20206D656469756D70207665633320746D707661725F343B0A2020746D707661725F34203D20285F436F6C6F722E78797A202A20746D707661725F332E78797A293B0A20206D656469756D7020666C6F617420746D707661725F353B0A2020746D707661725F35203D20646F742028786C765F544558434F4F5244332E797A772C20786C765F544558434F4F524434293B0A20206D656469756D70207665633220746D707661725F363B0A2020746D707661725F362E78203D202828746D707661725F35202A20746D707661725F3529202A2028746D707661725F35202A20746D707661725F3529293B0A2020746D707661725F362E79203D2028312E30202D205F476C6F7373696E657373293B0A20206C6F7770207665633420746D707661725F373B0A2020746D707661725F37203D207465787475726532442028756E6974795F4E4878526F7567686E6573732C20746D707661725F36293B0A2020635F32203D20282828746D707661725F34202A200A2020202028302E37373930383337202D20285F4D6574616C6C6963202A20302E3737393038333729290A202029202B20280A2020202028746D707661725F372E77202A2031362E30290A2020202A200A202020206D697820287665633328302E323230393136332C20302E323230393136332C20302E32323039313633292C20746D707661725F342C2076656333285F4D6574616C6C696329290A20202929202A205F4C69676874436F6C6F72302E78797A293B0A20206D656469756D7020666C6F6174207265616C74696D65536861646F77417474656E756174696F6E5F383B0A20206869676870207665633420746D707661725F393B0A2020746D707661725F392E77203D20312E303B0A2020746D707661725F392E78797A203D20786C765F544558434F4F5244313B0A20206C6F777020666C6F617420746D707661725F31303B0A20206869676870207665633420736861646F77436F6F72645F31313B0A2020736861646F77436F6F72645F3131203D2028756E6974795F576F726C64546F536861646F775B305D202A20746D707661725F39293B0A2020686967687020666C6F6174206C69676874536861646F7744617461585F31323B0A20206D656469756D7020666C6F617420746D707661725F31333B0A2020746D707661725F3133203D205F4C69676874536861646F77446174612E783B0A20206C69676874536861646F7744617461585F3132203D20746D707661725F31333B0A2020686967687020666C6F617420746D707661725F31343B0A2020746D707661725F3134203D206D61782028666C6F6174282874657874757265324420285F536861646F774D6170546578747572652C20736861646F77436F6F72645F31312E7879292E78203E20736861646F77436F6F72645F31312E7A29292C206C69676874536861646F7744617461585F3132293B0A2020746D707661725F3130203D20746D707661725F31343B0A20207265616C74696D65536861646F77417474656E756174696F6E5F38203D20746D707661725F31303B0A2020617474656E5F31203D207265616C74696D65536861646F77417474656E756174696F6E5F383B0A2020635F32203D2028635F32202A2028617474656E5F31202A20636C616D7020280A20202020646F742028786C765F544558434F4F5244352C20786C765F544558434F4F524434290A20202C20302E302C20312E302929293B0A20206D656469756D70207665633420746D707661725F31353B0A2020746D707661725F31352E77203D20312E303B0A2020746D707661725F31352E78797A203D20635F323B0A20206D656469756D70207665633420786C61745F7661726F75747075745F31363B0A2020786C61745F7661726F75747075745F31362E78797A203D20746D707661725F31352E78797A3B0A2020786C61745F7661726F75747075745F31362E77203D20312E303B0A2020676C5F46726167446174615B305D203D20786C61745F7661726F75747075745F31363B0A7D0A0A0A23656E6469660A0000001B000000000000000100000000000000000000000000000000000000324F040C050000001B000000010000000300000000000000000000008E0E00002376657273696F6E203130300A0A236966646566205645525445580A6174747269627574652076656334205F676C65735665727465783B0A6174747269627574652076656334205F676C65734D756C7469546578436F6F7264303B0A6174747269627574652076656334205F676C65734D756C7469546578436F6F7264313B0A6174747269627574652076656334205F676C65734D756C7469546578436F6F7264323B0A756E69666F726D206869676870206D61743420756E6974795F4F626A656374546F576F726C643B0A756E69666F726D206869676870206D61743420756E6974795F4D617472697856503B0A756E69666F726D206869676870207665633420756E6974795F4C696768746D617053543B0A756E69666F726D206869676870207665633420756E6974795F44796E616D69634C696768746D617053543B0A756E69666F726D2068696768702076656334205F4D61696E5465785F53543B0A756E69666F726D2068696768702076656334205F44657461696C416C6265646F4D61705F53543B0A756E69666F726D206D656469756D7020666C6F6174205F55565365633B0A756E69666F726D20627665633420756E6974795F4D657461566572746578436F6E74726F6C3B0A76617279696E67206869676870207665633420786C765F544558434F4F5244303B0A766F6964206D61696E2028290A7B0A20206869676870207665633220746D707661725F313B0A2020746D707661725F31203D205F676C65734D756C7469546578436F6F7264302E78793B0A20206869676870207665633220746D707661725F323B0A2020746D707661725F32203D205F676C65734D756C7469546578436F6F7264312E78793B0A202068696768702076656334207665727465785F333B0A20207665727465785F33203D205F676C65735665727465783B0A202069662028756E6974795F4D657461566572746578436F6E74726F6C2E7829207B0A202020207665727465785F332E7879203D2028285F676C65734D756C7469546578436F6F7264312E7879202A20756E6974795F4C696768746D617053542E787929202B20756E6974795F4C696768746D617053542E7A77293B0A20202020686967687020666C6F617420746D707661725F343B0A2020202069662028285F676C65735665727465782E7A203E20302E302929207B0A202020202020746D707661725F34203D20302E303030313B0A202020207D20656C7365207B0A202020202020746D707661725F34203D20302E303B0A202020207D3B0A202020207665727465785F332E7A203D20746D707661725F343B0A20207D3B0A202069662028756E6974795F4D657461566572746578436F6E74726F6C2E7929207B0A202020207665727465785F332E7879203D2028285F676C65734D756C7469546578436F6F7264322E7879202A20756E6974795F44796E616D69634C696768746D617053542E787929202B20756E6974795F44796E616D69634C696768746D617053542E7A77293B0A20202020686967687020666C6F617420746D707661725F353B0A2020202069662028287665727465785F332E7A203E20302E302929207B0A202020202020746D707661725F35203D20302E303030313B0A202020207D20656C7365207B0A202020202020746D707661725F35203D20302E303B0A202020207D3B0A202020207665727465785F332E7A203D20746D707661725F353B0A20207D3B0A20206869676870207665633420746D707661725F363B0A20206869676870207665633420746D707661725F373B0A2020746D707661725F372E77203D20312E303B0A2020746D707661725F372E78797A203D207665727465785F332E78797A3B0A2020746D707661725F36203D2028756E6974795F4D61747269785650202A2028756E6974795F4F626A656374546F576F726C64202A20746D707661725F3729293B0A20206869676870207665633420746578636F6F72645F383B0A2020746578636F6F72645F382E7879203D2028285F676C65734D756C7469546578436F6F7264302E7879202A205F4D61696E5465785F53542E787929202B205F4D61696E5465785F53542E7A77293B0A20206869676870207665633220746D707661725F393B0A202069662028285F5556536563203D3D20302E302929207B0A20202020746D707661725F39203D20746D707661725F313B0A20207D20656C7365207B0A20202020746D707661725F39203D20746D707661725F323B0A20207D3B0A2020746578636F6F72645F382E7A77203D202828746D707661725F39202A205F44657461696C416C6265646F4D61705F53542E787929202B205F44657461696C416C6265646F4D61705F53542E7A77293B0A2020786C765F544558434F4F524430203D20746578636F6F72645F383B0A2020676C5F506F736974696F6E203D20746D707661725F363B0A7D0A0A0A23656E6469660A23696664656620465241474D454E540A756E69666F726D206D656469756D702076656334205F436F6C6F723B0A756E69666F726D2073616D706C65723244205F4D61696E5465783B0A756E69666F726D206D656469756D7020666C6F6174205F4D6574616C6C69633B0A756E69666F726D206D656469756D7020666C6F6174205F476C6F7373696E6573733B0A756E69666F726D20627665633420756E6974795F4D657461467261676D656E74436F6E74726F6C3B0A756E69666F726D20686967687020666C6F617420756E6974795F4F6E654F7665724F7574707574426F6F73743B0A756E69666F726D20686967687020666C6F617420756E6974795F4D61784F757470757456616C75653B0A756E69666F726D20686967687020666C6F617420756E6974795F5573654C696E65617253706163653B0A76617279696E67206869676870207665633420786C765F544558434F4F5244303B0A766F6964206D61696E2028290A7B0A20206869676870207665633420746D707661725F313B0A20206C6F7770207665633420746D707661725F323B0A2020746D707661725F32203D2074657874757265324420285F4D61696E5465782C20786C765F544558434F4F5244302E7879293B0A20206D656469756D70207665633320746D707661725F333B0A2020746D707661725F33203D20285F436F6C6F722E78797A202A20746D707661725F322E78797A293B0A20206D656469756D702076656333207265735F343B0A20206D656469756D7020666C6F617420746D707661725F353B0A2020746D707661725F35203D2028312E30202D205F476C6F7373696E657373293B0A20207265735F34203D202828746D707661725F33202A2028302E37373930383337202D200A20202020285F4D6574616C6C6963202A20302E37373930383337290A20202929202B2028280A202020206D697820287665633328302E323230393136332C20302E323230393136332C20302E32323039313633292C20746D707661725F332C2076656333285F4D6574616C6C696329290A2020202A200A2020202028746D707661725F35202A20746D707661725F35290A202029202A20302E3529293B0A20206D656469756D702076656334207265735F363B0A20207265735F36203D207665633428302E302C20302E302C20302E302C20302E30293B0A202069662028756E6974795F4D657461467261676D656E74436F6E74726F6C2E7829207B0A202020206D656469756D70207665633420746D707661725F373B0A20202020746D707661725F372E77203D20312E303B0A20202020746D707661725F372E78797A203D207265735F343B0A202020207265735F362E77203D20746D707661725F372E773B0A202020206869676870207665633320746D707661725F383B0A20202020746D707661725F38203D20636C616D702028706F7720287265735F342C207665633328636C616D702028756E6974795F4F6E654F7665724F7574707574426F6F73742C20302E302C20312E302929292C207665633328302E302C20302E302C20302E30292C207665633328756E6974795F4D61784F757470757456616C756529293B0A202020207265735F362E78797A203D20746D707661725F383B0A20207D3B0A202069662028756E6974795F4D657461467261676D656E74436F6E74726F6C2E7929207B0A202020206D656469756D70207665633320656D697373696F6E5F393B0A2020202069662028626F6F6C28756E6974795F5573654C696E65617253706163652929207B0A202020202020656D697373696F6E5F39203D207665633328302E302C20302E302C20302E30293B0A202020207D20656C7365207B0A202020202020656D697373696F6E5F39203D207665633328302E302C20302E302C20302E30293B0A202020207D3B0A202020206D656469756D70207665633420746D707661725F31303B0A20202020686967687020666C6F617420616C7068615F31313B0A202020206869676870207665633320746D707661725F31323B0A20202020746D707661725F3132203D2028656D697373696F6E5F39202A20302E3031303330393238293B0A20202020616C7068615F3131203D20286365696C28280A2020202020206D617820286D61782028746D707661725F31322E782C20746D707661725F31322E79292C206D61782028746D707661725F31322E7A2C20302E303229290A20202020202A203235352E302929202F203235352E30293B0A20202020686967687020666C6F617420746D707661725F31333B0A20202020746D707661725F3133203D206D61782028616C7068615F31312C20302E3032293B0A20202020616C7068615F3131203D20746D707661725F31333B0A202020206869676870207665633420746D707661725F31343B0A20202020746D707661725F31342E78797A203D2028746D707661725F3132202F20746D707661725F3133293B0A20202020746D707661725F31342E77203D20746D707661725F31333B0A20202020746D707661725F3130203D20746D707661725F31343B0A202020207265735F36203D20746D707661725F31303B0A20207D3B0A2020746D707661725F31203D207265735F363B0A2020676C5F46726167446174615B305D203D20746D707661725F313B0A7D0A0A0A23656E6469660A000039000000000000000100000000000000000000000000000000000000324F040C050000002C000000030000000000000000000000010000000C000000504F494E545F434F4F4B49454C1400002376657273696F6E203130300A0A236966646566205645525445580A6174747269627574652076656334205F676C65735665727465783B0A6174747269627574652076656333205F676C65734E6F726D616C3B0A6174747269627574652076656334205F676C65734D756C7469546578436F6F7264303B0A6174747269627574652076656334205F676C65734D756C7469546578436F6F7264313B0A756E69666F726D2068696768702076656333205F576F726C64537061636543616D657261506F733B0A756E69666F726D2068696768702076656334205F576F726C6453706163654C69676874506F73303B0A756E69666F726D206869676870206D61743420756E6974795F4F626A656374546F576F726C643B0A756E69666F726D206869676870206D61743420756E6974795F576F726C64546F4F626A6563743B0A756E69666F726D206869676870206D61743420756E6974795F4D617472697856503B0A756E69666F726D2068696768702076656334205F4D61696E5465785F53543B0A756E69666F726D2068696768702076656334205F44657461696C416C6265646F4D61705F53543B0A756E69666F726D206D656469756D7020666C6F6174205F55565365633B0A76617279696E67206869676870207665633420786C765F544558434F4F5244303B0A76617279696E67206D656469756D70207665633320786C765F544558434F4F5244313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244323B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244325F313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244325F323B0A76617279696E67206869676870207665633320786C765F544558434F4F5244353B0A76617279696E67206869676870207665633220786C765F544558434F4F5244363B0A766F6964206D61696E2028290A7B0A20206D656469756D70207665633320746D707661725F313B0A2020746D707661725F31203D205F676C65734E6F726D616C3B0A20206869676870207665633220746D707661725F323B0A2020746D707661725F32203D205F676C65734D756C7469546578436F6F7264302E78793B0A20206869676870207665633220746D707661725F333B0A2020746D707661725F33203D205F676C65734D756C7469546578436F6F7264312E78793B0A202068696768702076656333206C696768744469725F343B0A20206D656469756D70207665633420746D707661725F353B0A20206D656469756D70207665633420746D707661725F363B0A20206D656469756D70207665633420746D707661725F373B0A20206869676870207665633220746D707661725F383B0A20206869676870207665633420746D707661725F393B0A2020746D707661725F39203D2028756E6974795F4F626A656374546F576F726C64202A205F676C6573566572746578293B0A20206869676870207665633420746D707661725F31303B0A20206869676870207665633420746D707661725F31313B0A2020746D707661725F31312E77203D20312E303B0A2020746D707661725F31312E78797A203D205F676C65735665727465782E78797A3B0A2020746D707661725F3130203D2028756E6974795F4D61747269785650202A2028756E6974795F4F626A656374546F576F726C64202A20746D707661725F313129293B0A20206869676870207665633420746578636F6F72645F31323B0A2020746578636F6F72645F31322E7879203D2028285F676C65734D756C7469546578436F6F7264302E7879202A205F4D61696E5465785F53542E787929202B205F4D61696E5465785F53542E7A77293B0A20206869676870207665633220746D707661725F31333B0A202069662028285F5556536563203D3D20302E302929207B0A20202020746D707661725F3133203D20746D707661725F323B0A20207D20656C7365207B0A20202020746D707661725F3133203D20746D707661725F333B0A20207D3B0A2020746578636F6F72645F31322E7A77203D202828746D707661725F3133202A205F44657461696C416C6265646F4D61705F53542E787929202B205F44657461696C416C6265646F4D61705F53542E7A77293B0A20206D656469756D70207665633320746D707661725F31343B0A202068696768702076656333206E5F31353B0A20206E5F3135203D2028746D707661725F392E78797A202D205F576F726C64537061636543616D657261506F73293B0A2020746D707661725F3134203D206E5F31353B0A202068696768702076656333206E6F726D5F31363B0A20206E6F726D5F3136203D20746D707661725F313B0A20206869676870206D61743320746D707661725F31373B0A2020746D707661725F31375B305D203D20756E6974795F576F726C64546F4F626A6563745B305D2E78797A3B0A2020746D707661725F31375B315D203D20756E6974795F576F726C64546F4F626A6563745B315D2E78797A3B0A2020746D707661725F31375B325D203D20756E6974795F576F726C64546F4F626A6563745B325D2E78797A3B0A20206869676870207665633320746D707661725F31383B0A2020746D707661725F3138203D206E6F726D616C697A6528286E6F726D5F3136202A20746D707661725F313729293B0A2020746D707661725F352E78797A203D207665633328302E302C20302E302C20302E30293B0A2020746D707661725F362E78797A203D207665633328302E302C20302E302C20302E30293B0A2020746D707661725F372E78797A203D20746D707661725F31383B0A20206869676870207665633320746D707661725F31393B0A2020746D707661725F3139203D20285F576F726C6453706163654C69676874506F73302E78797A202D2028746D707661725F392E78797A202A205F576F726C6453706163654C69676874506F73302E7729293B0A20206D656469756D70207665633320746D707661725F32303B0A2020746D707661725F3230203D20746D707661725F31393B0A20206C696768744469725F34203D20746D707661725F32303B0A2020746D707661725F352E77203D206C696768744469725F342E783B0A2020746D707661725F362E77203D206C696768744469725F342E793B0A2020746D707661725F372E77203D206C696768744469725F342E7A3B0A2020676C5F506F736974696F6E203D20746D707661725F31303B0A2020786C765F544558434F4F524430203D20746578636F6F72645F31323B0A2020786C765F544558434F4F524431203D20746D707661725F31343B0A2020786C765F544558434F4F524432203D20746D707661725F353B0A2020786C765F544558434F4F5244325F31203D20746D707661725F363B0A2020786C765F544558434F4F5244325F32203D20746D707661725F373B0A2020786C765F544558434F4F524435203D20746D707661725F392E78797A3B0A2020786C765F544558434F4F524436203D20746D707661725F383B0A7D0A0A0A23656E6469660A23696664656620465241474D454E540A756E69666F726D206C6F77702076656334205F4C69676874436F6C6F72303B0A756E69666F726D206D656469756D702076656334205F436F6C6F723B0A756E69666F726D2073616D706C65723244205F4D61696E5465783B0A756E69666F726D206D656469756D7020666C6F6174205F4D6574616C6C69633B0A756E69666F726D206D656469756D7020666C6F6174205F476C6F7373696E6573733B0A756E69666F726D206C6F77702073616D706C657243756265205F4C6967687454657874757265303B0A756E69666F726D206869676870206D61743420756E6974795F576F726C64546F4C696768743B0A756E69666F726D2073616D706C65723244205F4C696768745465787475726542303B0A76617279696E67206869676870207665633420786C765F544558434F4F5244303B0A76617279696E67206D656469756D70207665633320786C765F544558434F4F5244313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244323B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244325F313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244325F323B0A76617279696E67206869676870207665633320786C765F544558434F4F5244353B0A766F6964206D61696E2028290A7B0A202068696768702076656333206C69676874436F6F72645F313B0A20206C6F7770207665633420746D707661725F323B0A2020746D707661725F32203D2074657874757265324420285F4D61696E5465782C20786C765F544558434F4F5244302E7879293B0A20206D656469756D70207665633320746D707661725F333B0A2020746D707661725F33203D20285F436F6C6F722E78797A202A20746D707661725F322E78797A293B0A20206D656469756D70207665633320746D707661725F343B0A2020746D707661725F34203D206E6F726D616C697A6528786C765F544558434F4F5244325F322E78797A293B0A20206869676870207665633420746D707661725F353B0A2020746D707661725F352E77203D20312E303B0A2020746D707661725F352E78797A203D20786C765F544558434F4F5244353B0A20206C69676874436F6F72645F31203D2028756E6974795F576F726C64546F4C69676874202A20746D707661725F35292E78797A3B0A2020686967687020666C6F617420746D707661725F363B0A2020746D707661725F36203D20646F7420286C69676874436F6F72645F312C206C69676874436F6F72645F31293B0A20206C6F777020666C6F617420746D707661725F373B0A2020746D707661725F37203D202874657874757265324420285F4C696768745465787475726542302C207665633228746D707661725F3629292E77202A20746578747572654375626520285F4C6967687454657874757265302C206C69676874436F6F72645F31292E77293B0A20206D656469756D70207665633320746D707661725F383B0A2020746D707661725F382E78203D20786C765F544558434F4F5244322E773B0A2020746D707661725F382E79203D20786C765F544558434F4F5244325F312E773B0A2020746D707661725F382E7A203D20786C765F544558434F4F5244325F322E773B0A20206D656469756D7020666C6F617420617474656E5F393B0A2020617474656E5F39203D20746D707661725F373B0A20206D656469756D70207665633320746D707661725F31303B0A2020746D707661725F3130203D205F4C69676874436F6C6F72302E78797A3B0A20206D656469756D70207665633320746D707661725F31313B0A2020746D707661725F3131203D206E6F726D616C697A6528746D707661725F38293B0A2020746D707661725F3130203D2028746D707661725F3130202A20617474656E5F39293B0A20206D656469756D7020666C6F61742073706563756C61725465726D5F31323B0A20206D656469756D70207665633320746D707661725F31333B0A20206D656469756D70207665633320696E5665635F31343B0A2020696E5665635F3134203D2028746D707661725F3131202D206E6F726D616C697A6528786C765F544558434F4F52443129293B0A2020746D707661725F3133203D2028696E5665635F3134202A20696E766572736573717274286D61782028302E3030312C200A20202020646F742028696E5665635F31342C20696E5665635F3134290A20202929293B0A20206D656469756D7020666C6F617420746D707661725F31353B0A2020746D707661725F3135203D20636C616D702028646F742028746D707661725F342C20746D707661725F3133292C20302E302C20312E30293B0A20206D656469756D7020666C6F617420746D707661725F31363B0A2020746D707661725F3136203D2028312E30202D205F476C6F7373696E657373293B0A20206D656469756D7020666C6F617420746D707661725F31373B0A2020746D707661725F3137203D2028746D707661725F3136202A20746D707661725F3136293B0A202073706563756C61725465726D5F3132203D202828746D707661725F3137202F20280A20202020286D61782028302E33322C20636C616D702028646F742028746D707661725F31312C20746D707661725F3133292C20302E302C20312E302929202A2028312E35202B20746D707661725F313729290A2020202A200A20202020282828746D707661725F3135202A20746D707661725F313529202A202828746D707661725F3137202A20746D707661725F313729202D20312E302929202B20312E3030303031290A20202929202D20302E30303031293B0A20206D656469756D7020666C6F617420746D707661725F31383B0A2020746D707661725F3138203D20636C616D70202873706563756C61725465726D5F31322C20302E302C203130302E30293B0A202073706563756C61725465726D5F3132203D20746D707661725F31383B0A20206D656469756D70207665633420746D707661725F31393B0A2020746D707661725F31392E77203D20312E303B0A2020746D707661725F31392E78797A203D202828280A2020202028746D707661725F33202A2028302E37373930383337202D20285F4D6574616C6C6963202A20302E373739303833372929290A2020202B200A2020202028746D707661725F3138202A206D697820287665633328302E323230393136332C20302E323230393136332C20302E32323039313633292C20746D707661725F332C2076656333285F4D6574616C6C69632929290A202029202A20746D707661725F313029202A20636C616D702028646F742028746D707661725F342C20746D707661725F3131292C20302E302C20312E3029293B0A20206D656469756D70207665633420786C61745F7661726F75747075745F32303B0A2020786C61745F7661726F75747075745F32302E78797A203D20746D707661725F31392E78797A3B0A2020786C61745F7661726F75747075745F32302E77203D20312E303B0A2020676C5F46726167446174615B305D203D20786C61745F7661726F75747075745F32303B0A7D0A0A0A23656E6469660A1B000000000000000100000000000000000000000000000000000000324F040C050000001C000000020000000000000000000000010000000B000000444952454354494F4E414C00771000002376657273696F6E203130300A0A236966646566205645525445580A6174747269627574652076656334205F676C65735665727465783B0A6174747269627574652076656333205F676C65734E6F726D616C3B0A6174747269627574652076656334205F676C65734D756C7469546578436F6F7264303B0A6174747269627574652076656334205F676C65734D756C7469546578436F6F7264313B0A756E69666F726D2068696768702076656333205F576F726C64537061636543616D657261506F733B0A756E69666F726D206D656469756D702076656334205F576F726C6453706163654C69676874506F73303B0A756E69666F726D206869676870206D61743420756E6974795F4F626A656374546F576F726C643B0A756E69666F726D206869676870206D61743420756E6974795F576F726C64546F4F626A6563743B0A756E69666F726D206869676870206D61743420756E6974795F4D617472697856503B0A756E69666F726D2068696768702076656334205F4D61696E5465785F53543B0A756E69666F726D2068696768702076656334205F44657461696C416C6265646F4D61705F53543B0A756E69666F726D206D656469756D7020666C6F6174205F55565365633B0A76617279696E67206869676870207665633420786C765F544558434F4F5244303B0A76617279696E67206D656469756D70207665633320786C765F544558434F4F5244313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244323B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244325F313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244325F323B0A76617279696E67206869676870207665633320786C765F544558434F4F5244353B0A76617279696E67206869676870207665633220786C765F544558434F4F5244363B0A766F6964206D61696E2028290A7B0A20206D656469756D70207665633320746D707661725F313B0A2020746D707661725F31203D205F676C65734E6F726D616C3B0A20206869676870207665633220746D707661725F323B0A2020746D707661725F32203D205F676C65734D756C7469546578436F6F7264302E78793B0A20206869676870207665633220746D707661725F333B0A2020746D707661725F33203D205F676C65734D756C7469546578436F6F7264312E78793B0A20206D656469756D70207665633420746D707661725F343B0A20206D656469756D70207665633420746D707661725F353B0A20206D656469756D70207665633420746D707661725F363B0A20206869676870207665633220746D707661725F373B0A20206869676870207665633420746D707661725F383B0A2020746D707661725F38203D2028756E6974795F4F626A656374546F576F726C64202A205F676C6573566572746578293B0A20206869676870207665633420746D707661725F393B0A20206869676870207665633420746D707661725F31303B0A2020746D707661725F31302E77203D20312E303B0A2020746D707661725F31302E78797A203D205F676C65735665727465782E78797A3B0A2020746D707661725F39203D2028756E6974795F4D61747269785650202A2028756E6974795F4F626A656374546F576F726C64202A20746D707661725F313029293B0A20206869676870207665633420746578636F6F72645F31313B0A2020746578636F6F72645F31312E7879203D2028285F676C65734D756C7469546578436F6F7264302E7879202A205F4D61696E5465785F53542E787929202B205F4D61696E5465785F53542E7A77293B0A20206869676870207665633220746D707661725F31323B0A202069662028285F5556536563203D3D20302E302929207B0A20202020746D707661725F3132203D20746D707661725F323B0A20207D20656C7365207B0A20202020746D707661725F3132203D20746D707661725F333B0A20207D3B0A2020746578636F6F72645F31312E7A77203D202828746D707661725F3132202A205F44657461696C416C6265646F4D61705F53542E787929202B205F44657461696C416C6265646F4D61705F53542E7A77293B0A20206D656469756D70207665633320746D707661725F31333B0A20206869676870207665633320746D707661725F31343B0A2020746D707661725F3134203D206E6F726D616C697A652828746D707661725F382E78797A202D205F576F726C64537061636543616D657261506F7329293B0A2020746D707661725F3133203D20746D707661725F31343B0A202068696768702076656333206E6F726D5F31353B0A20206E6F726D5F3135203D20746D707661725F313B0A20206869676870206D61743320746D707661725F31363B0A2020746D707661725F31365B305D203D20756E6974795F576F726C64546F4F626A6563745B305D2E78797A3B0A2020746D707661725F31365B315D203D20756E6974795F576F726C64546F4F626A6563745B315D2E78797A3B0A2020746D707661725F31365B325D203D20756E6974795F576F726C64546F4F626A6563745B325D2E78797A3B0A20206869676870207665633320746D707661725F31373B0A2020746D707661725F3137203D206E6F726D616C697A6528286E6F726D5F3135202A20746D707661725F313629293B0A2020746D707661725F342E78797A203D207665633328302E302C20302E302C20302E30293B0A2020746D707661725F352E78797A203D207665633328302E302C20302E302C20302E30293B0A2020746D707661725F362E78797A203D20746D707661725F31373B0A20206869676870207665633320746D707661725F31383B0A2020746D707661725F3138203D20285F576F726C6453706163654C69676874506F73302E78797A202D2028746D707661725F382E78797A202A205F576F726C6453706163654C69676874506F73302E7729293B0A2020746D707661725F342E77203D20746D707661725F31382E783B0A2020746D707661725F352E77203D20746D707661725F31382E793B0A2020746D707661725F362E77203D20746D707661725F31382E7A3B0A2020676C5F506F736974696F6E203D20746D707661725F393B0A2020786C765F544558434F4F524430203D20746578636F6F72645F31313B0A2020786C765F544558434F4F524431203D20746D707661725F31333B0A2020786C765F544558434F4F524432203D20746D707661725F343B0A2020786C765F544558434F4F5244325F31203D20746D707661725F353B0A2020786C765F544558434F4F5244325F32203D20746D707661725F363B0A2020786C765F544558434F4F524435203D20746D707661725F382E78797A3B0A2020786C765F544558434F4F524436203D20746D707661725F373B0A7D0A0A0A23656E6469660A23696664656620465241474D454E540A756E69666F726D206C6F77702076656334205F4C69676874436F6C6F72303B0A756E69666F726D2073616D706C6572324420756E6974795F4E4878526F7567686E6573733B0A756E69666F726D206D656469756D702076656334205F436F6C6F723B0A756E69666F726D2073616D706C65723244205F4D61696E5465783B0A756E69666F726D206D656469756D7020666C6F6174205F4D6574616C6C69633B0A756E69666F726D206D656469756D7020666C6F6174205F476C6F7373696E6573733B0A76617279696E67206869676870207665633420786C765F544558434F4F5244303B0A76617279696E67206D656469756D70207665633320786C765F544558434F4F5244313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244323B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244325F313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244325F323B0A766F6964206D61696E2028290A7B0A20206C6F7770207665633420746D707661725F313B0A2020746D707661725F31203D2074657874757265324420285F4D61696E5465782C20786C765F544558434F4F5244302E7879293B0A20206D656469756D70207665633320746D707661725F323B0A2020746D707661725F32203D20285F436F6C6F722E78797A202A20746D707661725F312E78797A293B0A20206D656469756D70207665633320746D707661725F333B0A2020746D707661725F33203D206E6F726D616C697A6528786C765F544558434F4F5244325F322E78797A293B0A20206D656469756D70207665633320746D707661725F343B0A2020746D707661725F342E78203D20786C765F544558434F4F5244322E773B0A2020746D707661725F342E79203D20786C765F544558434F4F5244325F312E773B0A2020746D707661725F342E7A203D20786C765F544558434F4F5244325F322E773B0A20206D656469756D70207665633320746D707661725F353B0A2020746D707661725F35203D205F4C69676874436F6C6F72302E78797A3B0A20206D656469756D70207665633320766965774469725F363B0A2020766965774469725F36203D202D28786C765F544558434F4F524431293B0A20206D656469756D70207665633220746D707661725F373B0A2020746D707661725F372E78203D20646F74202828766965774469725F36202D2028322E30202A200A2020202028646F742028746D707661725F332C20766965774469725F3629202A20746D707661725F33290A202029292C20746D707661725F34293B0A2020746D707661725F372E79203D2028312E30202D20636C616D702028646F742028746D707661725F332C20766965774469725F36292C20302E302C20312E3029293B0A20206D656469756D70207665633220746D707661725F383B0A2020746D707661725F382E78203D202828746D707661725F37202A20746D707661725F3729202A2028746D707661725F37202A20746D707661725F3729292E783B0A2020746D707661725F382E79203D2028312E30202D205F476C6F7373696E657373293B0A20206C6F7770207665633420746D707661725F393B0A2020746D707661725F39203D207465787475726532442028756E6974795F4E4878526F7567686E6573732C20746D707661725F38293B0A20206D656469756D70207665633420746D707661725F31303B0A2020746D707661725F31302E77203D20312E303B0A2020746D707661725F31302E78797A203D20282828746D707661725F32202A200A2020202028302E37373930383337202D20285F4D6574616C6C6963202A20302E3737393038333729290A202029202B20280A2020202028746D707661725F392E77202A2031362E30290A2020202A200A202020206D697820287665633328302E323230393136332C20302E323230393136332C20302E32323039313633292C20746D707661725F322C2076656333285F4D6574616C6C696329290A20202929202A2028746D707661725F35202A20636C616D7020280A20202020646F742028746D707661725F332C20746D707661725F34290A20202C20302E302C20312E302929293B0A20206D656469756D70207665633420786C61745F7661726F75747075745F31313B0A2020786C61745F7661726F75747075745F31312E78797A203D20746D707661725F31302E78797A3B0A2020786C61745F7661726F75747075745F31312E77203D20312E303B0A2020676C5F46726167446174615B305D203D20786C61745F7661726F75747075745F31313B0A7D0A0A0A23656E6469660A001B000000000000000100000000000000000000000000000000000000324F040C050000002C000000080000000400000000000000030000000400000053504F540D000000534841444F57535F44455054480000000C000000534841444F57535F534F4654701900002376657273696F6E203130300A0A236966646566205645525445580A6174747269627574652076656334205F676C65735665727465783B0A6174747269627574652076656333205F676C65734E6F726D616C3B0A6174747269627574652076656334205F676C65734D756C7469546578436F6F7264303B0A6174747269627574652076656334205F676C65734D756C7469546578436F6F7264313B0A756E69666F726D2068696768702076656333205F576F726C64537061636543616D657261506F733B0A756E69666F726D2068696768702076656334205F576F726C6453706163654C69676874506F73303B0A756E69666F726D206869676870206D61743420756E6974795F4F626A656374546F576F726C643B0A756E69666F726D206869676870206D61743420756E6974795F576F726C64546F4F626A6563743B0A756E69666F726D206869676870206D61743420756E6974795F4D617472697856503B0A756E69666F726D2068696768702076656334205F4D61696E5465785F53543B0A756E69666F726D2068696768702076656334205F44657461696C416C6265646F4D61705F53543B0A756E69666F726D206D656469756D7020666C6F6174205F55565365633B0A76617279696E67206869676870207665633420786C765F544558434F4F5244303B0A76617279696E67206869676870207665633320786C765F544558434F4F5244313B0A76617279696E67206869676870207665633220786C765F544558434F4F5244323B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244333B0A76617279696E67206D656469756D70207665633320786C765F544558434F4F5244343B0A76617279696E67206D656469756D70207665633320786C765F544558434F4F5244353B0A766F6964206D61696E2028290A7B0A20206D656469756D70207665633320746D707661725F313B0A2020746D707661725F31203D205F676C65734E6F726D616C3B0A20206869676870207665633220746D707661725F323B0A2020746D707661725F32203D205F676C65734D756C7469546578436F6F7264302E78793B0A20206869676870207665633220746D707661725F333B0A2020746D707661725F33203D205F676C65734D756C7469546578436F6F7264312E78793B0A20206D656469756D702076656333206E6F726D616C576F726C645F343B0A20206D656469756D702076656333206579655665635F353B0A20206D656469756D702076656333206C696768744469725F363B0A20206869676870207665633220746D707661725F373B0A20206D656469756D70207665633420746D707661725F383B0A20206869676870207665633420746D707661725F393B0A2020746D707661725F39203D2028756E6974795F4F626A656374546F576F726C64202A205F676C6573566572746578293B0A20206869676870207665633420746D707661725F31303B0A20206869676870207665633420746D707661725F31313B0A2020746D707661725F31312E77203D20312E303B0A2020746D707661725F31312E78797A203D205F676C65735665727465782E78797A3B0A2020746D707661725F3130203D2028756E6974795F4D61747269785650202A2028756E6974795F4F626A656374546F576F726C64202A20746D707661725F313129293B0A20206869676870207665633420746578636F6F72645F31323B0A2020746578636F6F72645F31322E7879203D2028285F676C65734D756C7469546578436F6F7264302E7879202A205F4D61696E5465785F53542E787929202B205F4D61696E5465785F53542E7A77293B0A20206869676870207665633220746D707661725F31333B0A202069662028285F5556536563203D3D20302E302929207B0A20202020746D707661725F3133203D20746D707661725F323B0A20207D20656C7365207B0A20202020746D707661725F3133203D20746D707661725F333B0A20207D3B0A2020746578636F6F72645F31322E7A77203D202828746D707661725F3133202A205F44657461696C416C6265646F4D61705F53542E787929202B205F44657461696C416C6265646F4D61705F53542E7A77293B0A20206869676870207665633320746D707661725F31343B0A2020746D707661725F3134203D20285F576F726C6453706163654C69676874506F73302E78797A202D2028746D707661725F392E78797A202A205F576F726C6453706163654C69676874506F73302E7729293B0A20206C696768744469725F36203D20746D707661725F31343B0A20206D656469756D70207665633320746D707661725F31353B0A202068696768702076656333206E5F31363B0A20206E5F3136203D206C696768744469725F363B0A20206869676870207665633320746D707661725F31373B0A2020746D707661725F3137203D206E6F726D616C697A65286E5F3136293B0A2020746D707661725F3135203D20746D707661725F31373B0A20206C696768744469725F36203D20746D707661725F31353B0A20206869676870207665633320746D707661725F31383B0A2020746D707661725F3138203D206E6F726D616C697A652828746D707661725F392E78797A202D205F576F726C64537061636543616D657261506F7329293B0A20206579655665635F35203D20746D707661725F31383B0A202068696768702076656333206E6F726D5F31393B0A20206E6F726D5F3139203D20746D707661725F313B0A20206869676870206D61743320746D707661725F32303B0A2020746D707661725F32305B305D203D20756E6974795F576F726C64546F4F626A6563745B305D2E78797A3B0A2020746D707661725F32305B315D203D20756E6974795F576F726C64546F4F626A6563745B315D2E78797A3B0A2020746D707661725F32305B325D203D20756E6974795F576F726C64546F4F626A6563745B325D2E78797A3B0A20206869676870207665633320746D707661725F32313B0A2020746D707661725F3231203D206E6F726D616C697A6528286E6F726D5F3139202A20746D707661725F323029293B0A20206E6F726D616C576F726C645F34203D20746D707661725F32313B0A2020746D707661725F382E797A77203D20286579655665635F35202D2028322E30202A20280A20202020646F7420286E6F726D616C576F726C645F342C206579655665635F35290A2020202A206E6F726D616C576F726C645F342929293B0A2020676C5F506F736974696F6E203D20746D707661725F31303B0A2020786C765F544558434F4F524430203D20746578636F6F72645F31323B0A2020786C765F544558434F4F524431203D20746D707661725F392E78797A3B0A2020786C765F544558434F4F524432203D20746D707661725F373B0A2020786C765F544558434F4F524433203D20746D707661725F383B0A2020786C765F544558434F4F524434203D20746D707661725F31353B0A2020786C765F544558434F4F524435203D206E6F726D616C576F726C645F343B0A7D0A0A0A23656E6469660A23696664656620465241474D454E540A756E69666F726D206869676870206D61743420756E6974795F576F726C64546F536861646F775B345D3B0A756E69666F726D206D656469756D702076656334205F4C69676874536861646F77446174613B0A756E69666F726D206C6F77702076656334205F4C69676874436F6C6F72303B0A756E69666F726D2073616D706C6572324420756E6974795F4E4878526F7567686E6573733B0A756E69666F726D2068696768702073616D706C65723244205F536861646F774D6170546578747572653B0A756E69666F726D2068696768702076656334205F536861646F774F6666736574735B345D3B0A756E69666F726D206D656469756D702076656334205F436F6C6F723B0A756E69666F726D2073616D706C65723244205F4D61696E5465783B0A756E69666F726D206D656469756D7020666C6F6174205F4D6574616C6C69633B0A756E69666F726D206D656469756D7020666C6F6174205F476C6F7373696E6573733B0A756E69666F726D2073616D706C65723244205F4C6967687454657874757265303B0A756E69666F726D206869676870206D61743420756E6974795F576F726C64546F4C696768743B0A756E69666F726D2073616D706C65723244205F4C696768745465787475726542303B0A76617279696E67206869676870207665633420786C765F544558434F4F5244303B0A76617279696E67206869676870207665633320786C765F544558434F4F5244313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244333B0A76617279696E67206D656469756D70207665633320786C765F544558434F4F5244343B0A76617279696E67206D656469756D70207665633320786C765F544558434F4F5244353B0A766F6964206D61696E2028290A7B0A20206C6F777020666C6F617420617474656E5F313B0A20206C6F777020666C6F617420736861646F775F323B0A202068696768702076656334206C69676874436F6F72645F333B0A20206D656469756D70207665633320635F343B0A20206C6F7770207665633420746D707661725F353B0A2020746D707661725F35203D2074657874757265324420285F4D61696E5465782C20786C765F544558434F4F5244302E7879293B0A20206D656469756D70207665633320746D707661725F363B0A2020746D707661725F36203D20285F436F6C6F722E78797A202A20746D707661725F352E78797A293B0A20206D656469756D7020666C6F617420746D707661725F373B0A2020746D707661725F37203D20646F742028786C765F544558434F4F5244332E797A772C20786C765F544558434F4F524434293B0A20206D656469756D70207665633220746D707661725F383B0A2020746D707661725F382E78203D202828746D707661725F37202A20746D707661725F3729202A2028746D707661725F37202A20746D707661725F3729293B0A2020746D707661725F382E79203D2028312E30202D205F476C6F7373696E657373293B0A20206C6F7770207665633420746D707661725F393B0A2020746D707661725F39203D207465787475726532442028756E6974795F4E4878526F7567686E6573732C20746D707661725F38293B0A2020635F34203D20282828746D707661725F36202A200A2020202028302E37373930383337202D20285F4D6574616C6C6963202A20302E3737393038333729290A202029202B20280A2020202028746D707661725F392E77202A2031362E30290A2020202A200A202020206D697820287665633328302E323230393136332C20302E323230393136332C20302E32323039313633292C20746D707661725F362C2076656333285F4D6574616C6C696329290A20202929202A205F4C69676874436F6C6F72302E78797A293B0A20206869676870207665633420746D707661725F31303B0A2020746D707661725F31302E77203D20312E303B0A2020746D707661725F31302E78797A203D20786C765F544558434F4F5244313B0A20206C69676874436F6F72645F33203D2028756E6974795F576F726C64546F4C69676874202A20746D707661725F3130293B0A20206D656469756D7020666C6F6174207265616C74696D65536861646F77417474656E756174696F6E5F31313B0A20207265616C74696D65536861646F77417474656E756174696F6E5F3131203D20312E303B0A20206869676870207665633420746D707661725F31323B0A2020746D707661725F31322E77203D20312E303B0A2020746D707661725F31322E78797A203D20786C765F544558434F4F5244313B0A20206869676870207665633420746D707661725F31333B0A2020746D707661725F3133203D2028756E6974795F576F726C64546F536861646F775B305D202A20746D707661725F3132293B0A20206C6F777020666C6F617420746D707661725F31343B0A20206869676870207665633420736861646F7756616C735F31353B0A20206869676870207665633320746D707661725F31363B0A2020746D707661725F3136203D2028746D707661725F31332E78797A202F20746D707661725F31332E77293B0A2020736861646F7756616C735F31352E78203D2074657874757265324420285F536861646F774D6170546578747572652C2028746D707661725F31362E7879202B205F536861646F774F6666736574735B305D2E787929292E783B0A2020736861646F7756616C735F31352E79203D2074657874757265324420285F536861646F774D6170546578747572652C2028746D707661725F31362E7879202B205F536861646F774F6666736574735B315D2E787929292E783B0A2020736861646F7756616C735F31352E7A203D2074657874757265324420285F536861646F774D6170546578747572652C2028746D707661725F31362E7879202B205F536861646F774F6666736574735B325D2E787929292E783B0A2020736861646F7756616C735F31352E77203D2074657874757265324420285F536861646F774D6170546578747572652C2028746D707661725F31362E7879202B205F536861646F774F6666736574735B335D2E787929292E783B0A2020627665633420746D707661725F31373B0A2020746D707661725F3137203D206C6573735468616E2028736861646F7756616C735F31352C20746D707661725F31362E7A7A7A7A293B0A20206D656469756D70207665633420746D707661725F31383B0A2020746D707661725F3138203D205F4C69676874536861646F77446174612E787878783B0A20206D656469756D7020666C6F617420746D707661725F31393B0A202069662028746D707661725F31372E7829207B0A20202020746D707661725F3139203D20746D707661725F31382E783B0A20207D20656C7365207B0A20202020746D707661725F3139203D20312E303B0A20207D3B0A20206D656469756D7020666C6F617420746D707661725F32303B0A202069662028746D707661725F31372E7929207B0A20202020746D707661725F3230203D20746D707661725F31382E793B0A20207D20656C7365207B0A20202020746D707661725F3230203D20312E303B0A20207D3B0A20206D656469756D7020666C6F617420746D707661725F32313B0A202069662028746D707661725F31372E7A29207B0A20202020746D707661725F3231203D20746D707661725F31382E7A3B0A20207D20656C7365207B0A20202020746D707661725F3231203D20312E303B0A20207D3B0A20206D656469756D7020666C6F617420746D707661725F32323B0A202069662028746D707661725F31372E7729207B0A20202020746D707661725F3232203D20746D707661725F31382E773B0A20207D20656C7365207B0A20202020746D707661725F3232203D20312E303B0A20207D3B0A20206D656469756D70207665633420746D707661725F32333B0A2020746D707661725F32332E78203D20746D707661725F31393B0A2020746D707661725F32332E79203D20746D707661725F32303B0A2020746D707661725F32332E7A203D20746D707661725F32313B0A2020746D707661725F32332E77203D20746D707661725F32323B0A20206D656469756D7020666C6F617420746D707661725F32343B0A2020746D707661725F3234203D20646F742028746D707661725F32332C207665633428302E32352C20302E32352C20302E32352C20302E323529293B0A2020746D707661725F3134203D20746D707661725F32343B0A20207265616C74696D65536861646F77417474656E756174696F6E5F3131203D20746D707661725F31343B0A2020736861646F775F32203D207265616C74696D65536861646F77417474656E756174696F6E5F31313B0A20206C6F7770207665633420746D707661725F32353B0A20206869676870207665633220505F32363B0A2020505F3236203D2028286C69676874436F6F72645F332E7879202F206C69676874436F6F72645F332E7729202B20302E35293B0A2020746D707661725F3235203D2074657874757265324420285F4C6967687454657874757265302C20505F3236293B0A2020686967687020666C6F617420746D707661725F32373B0A2020746D707661725F3237203D20646F7420286C69676874436F6F72645F332E78797A2C206C69676874436F6F72645F332E78797A293B0A20206C6F7770207665633420746D707661725F32383B0A2020746D707661725F3238203D2074657874757265324420285F4C696768745465787475726542302C207665633228746D707661725F323729293B0A2020686967687020666C6F617420746D707661725F32393B0A2020746D707661725F3239203D202828280A20202020666C6F617428286C69676874436F6F72645F332E7A203E20302E3029290A2020202A20746D707661725F32352E7729202A20746D707661725F32382E7729202A20736861646F775F32293B0A2020617474656E5F31203D20746D707661725F32393B0A2020635F34203D2028635F34202A2028617474656E5F31202A20636C616D7020280A20202020646F742028786C765F544558434F4F5244352C20786C765F544558434F4F524434290A20202C20302E302C20312E302929293B0A20206D656469756D70207665633420746D707661725F33303B0A2020746D707661725F33302E77203D20312E303B0A2020746D707661725F33302E78797A203D20635F343B0A20206D656469756D70207665633420786C61745F7661726F75747075745F33313B0A2020786C61745F7661726F75747075745F33312E78797A203D20746D707661725F33302E78797A3B0A2020786C61745F7661726F75747075745F33312E77203D20312E303B0A2020676C5F46726167446174615B305D203D20786C61745F7661726F75747075745F33313B0A7D0A0A0A23656E6469660A1B000000000000000100000000000000000000000000000000000000324F040C050000002B0000000700000004000000000000000300000005000000504F494E540000000C000000534841444F57535F435542450C000000534841444F57535F534F4654251800002376657273696F6E203130300A0A236966646566205645525445580A6174747269627574652076656334205F676C65735665727465783B0A6174747269627574652076656333205F676C65734E6F726D616C3B0A6174747269627574652076656334205F676C65734D756C7469546578436F6F7264303B0A6174747269627574652076656334205F676C65734D756C7469546578436F6F7264313B0A756E69666F726D2068696768702076656333205F576F726C64537061636543616D657261506F733B0A756E69666F726D2068696768702076656334205F576F726C6453706163654C69676874506F73303B0A756E69666F726D206869676870206D61743420756E6974795F4F626A656374546F576F726C643B0A756E69666F726D206869676870206D61743420756E6974795F576F726C64546F4F626A6563743B0A756E69666F726D206869676870206D61743420756E6974795F4D617472697856503B0A756E69666F726D2068696768702076656334205F4D61696E5465785F53543B0A756E69666F726D2068696768702076656334205F44657461696C416C6265646F4D61705F53543B0A756E69666F726D206D656469756D7020666C6F6174205F55565365633B0A76617279696E67206869676870207665633420786C765F544558434F4F5244303B0A76617279696E67206869676870207665633320786C765F544558434F4F5244313B0A76617279696E67206869676870207665633220786C765F544558434F4F5244323B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244333B0A76617279696E67206D656469756D70207665633320786C765F544558434F4F5244343B0A76617279696E67206D656469756D70207665633320786C765F544558434F4F5244353B0A766F6964206D61696E2028290A7B0A20206D656469756D70207665633320746D707661725F313B0A2020746D707661725F31203D205F676C65734E6F726D616C3B0A20206869676870207665633220746D707661725F323B0A2020746D707661725F32203D205F676C65734D756C7469546578436F6F7264302E78793B0A20206869676870207665633220746D707661725F333B0A2020746D707661725F33203D205F676C65734D756C7469546578436F6F7264312E78793B0A20206D656469756D702076656333206E6F726D616C576F726C645F343B0A20206D656469756D702076656333206579655665635F353B0A20206D656469756D702076656333206C696768744469725F363B0A20206869676870207665633220746D707661725F373B0A20206D656469756D70207665633420746D707661725F383B0A20206869676870207665633420746D707661725F393B0A2020746D707661725F39203D2028756E6974795F4F626A656374546F576F726C64202A205F676C6573566572746578293B0A20206869676870207665633420746D707661725F31303B0A20206869676870207665633420746D707661725F31313B0A2020746D707661725F31312E77203D20312E303B0A2020746D707661725F31312E78797A203D205F676C65735665727465782E78797A3B0A2020746D707661725F3130203D2028756E6974795F4D61747269785650202A2028756E6974795F4F626A656374546F576F726C64202A20746D707661725F313129293B0A20206869676870207665633420746578636F6F72645F31323B0A2020746578636F6F72645F31322E7879203D2028285F676C65734D756C7469546578436F6F7264302E7879202A205F4D61696E5465785F53542E787929202B205F4D61696E5465785F53542E7A77293B0A20206869676870207665633220746D707661725F31333B0A202069662028285F5556536563203D3D20302E302929207B0A20202020746D707661725F3133203D20746D707661725F323B0A20207D20656C7365207B0A20202020746D707661725F3133203D20746D707661725F333B0A20207D3B0A2020746578636F6F72645F31322E7A77203D202828746D707661725F3133202A205F44657461696C416C6265646F4D61705F53542E787929202B205F44657461696C416C6265646F4D61705F53542E7A77293B0A20206869676870207665633320746D707661725F31343B0A2020746D707661725F3134203D20285F576F726C6453706163654C69676874506F73302E78797A202D2028746D707661725F392E78797A202A205F576F726C6453706163654C69676874506F73302E7729293B0A20206C696768744469725F36203D20746D707661725F31343B0A20206D656469756D70207665633320746D707661725F31353B0A202068696768702076656333206E5F31363B0A20206E5F3136203D206C696768744469725F363B0A20206869676870207665633320746D707661725F31373B0A2020746D707661725F3137203D206E6F726D616C697A65286E5F3136293B0A2020746D707661725F3135203D20746D707661725F31373B0A20206C696768744469725F36203D20746D707661725F31353B0A20206869676870207665633320746D707661725F31383B0A2020746D707661725F3138203D206E6F726D616C697A652828746D707661725F392E78797A202D205F576F726C64537061636543616D657261506F7329293B0A20206579655665635F35203D20746D707661725F31383B0A202068696768702076656333206E6F726D5F31393B0A20206E6F726D5F3139203D20746D707661725F313B0A20206869676870206D61743320746D707661725F32303B0A2020746D707661725F32305B305D203D20756E6974795F576F726C64546F4F626A6563745B305D2E78797A3B0A2020746D707661725F32305B315D203D20756E6974795F576F726C64546F4F626A6563745B315D2E78797A3B0A2020746D707661725F32305B325D203D20756E6974795F576F726C64546F4F626A6563745B325D2E78797A3B0A20206869676870207665633320746D707661725F32313B0A2020746D707661725F3231203D206E6F726D616C697A6528286E6F726D5F3139202A20746D707661725F323029293B0A20206E6F726D616C576F726C645F34203D20746D707661725F32313B0A2020746D707661725F382E797A77203D20286579655665635F35202D2028322E30202A20280A20202020646F7420286E6F726D616C576F726C645F342C206579655665635F35290A2020202A206E6F726D616C576F726C645F342929293B0A2020676C5F506F736974696F6E203D20746D707661725F31303B0A2020786C765F544558434F4F524430203D20746578636F6F72645F31323B0A2020786C765F544558434F4F524431203D20746D707661725F392E78797A3B0A2020786C765F544558434F4F524432203D20746D707661725F373B0A2020786C765F544558434F4F524433203D20746D707661725F383B0A2020786C765F544558434F4F524434203D20746D707661725F31353B0A2020786C765F544558434F4F524435203D206E6F726D616C576F726C645F343B0A7D0A0A0A23656E6469660A23696664656620465241474D454E540A756E69666F726D2068696768702076656334205F4C69676874506F736974696F6E52616E67653B0A756E69666F726D206D656469756D702076656334205F4C69676874536861646F77446174613B0A756E69666F726D206C6F77702076656334205F4C69676874436F6C6F72303B0A756E69666F726D2073616D706C6572324420756E6974795F4E4878526F7567686E6573733B0A756E69666F726D2068696768702073616D706C657243756265205F536861646F774D6170546578747572653B0A756E69666F726D206D656469756D702076656334205F436F6C6F723B0A756E69666F726D2073616D706C65723244205F4D61696E5465783B0A756E69666F726D206D656469756D7020666C6F6174205F4D6574616C6C69633B0A756E69666F726D206D656469756D7020666C6F6174205F476C6F7373696E6573733B0A756E69666F726D2073616D706C65723244205F4C6967687454657874757265303B0A756E69666F726D206869676870206D61743420756E6974795F576F726C64546F4C696768743B0A76617279696E67206869676870207665633420786C765F544558434F4F5244303B0A76617279696E67206869676870207665633320786C765F544558434F4F5244313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244333B0A76617279696E67206D656469756D70207665633320786C765F544558434F4F5244343B0A76617279696E67206D656469756D70207665633320786C765F544558434F4F5244353B0A766F6964206D61696E2028290A7B0A20206C6F777020666C6F617420736861646F775F313B0A202068696768702076656333206C69676874436F6F72645F323B0A20206D656469756D70207665633320635F333B0A20206C6F7770207665633420746D707661725F343B0A2020746D707661725F34203D2074657874757265324420285F4D61696E5465782C20786C765F544558434F4F5244302E7879293B0A20206D656469756D70207665633320746D707661725F353B0A2020746D707661725F35203D20285F436F6C6F722E78797A202A20746D707661725F342E78797A293B0A20206D656469756D7020666C6F617420746D707661725F363B0A2020746D707661725F36203D20646F742028786C765F544558434F4F5244332E797A772C20786C765F544558434F4F524434293B0A20206D656469756D70207665633220746D707661725F373B0A2020746D707661725F372E78203D202828746D707661725F36202A20746D707661725F3629202A2028746D707661725F36202A20746D707661725F3629293B0A2020746D707661725F372E79203D2028312E30202D205F476C6F7373696E657373293B0A20206C6F7770207665633420746D707661725F383B0A2020746D707661725F38203D207465787475726532442028756E6974795F4E4878526F7567686E6573732C20746D707661725F37293B0A2020635F33203D20282828746D707661725F35202A200A2020202028302E37373930383337202D20285F4D6574616C6C6963202A20302E3737393038333729290A202029202B20280A2020202028746D707661725F382E77202A2031362E30290A2020202A200A202020206D697820287665633328302E323230393136332C20302E323230393136332C20302E32323039313633292C20746D707661725F352C2076656333285F4D6574616C6C696329290A20202929202A205F4C69676874436F6C6F72302E78797A293B0A20206869676870207665633420746D707661725F393B0A2020746D707661725F392E77203D20312E303B0A2020746D707661725F392E78797A203D20786C765F544558434F4F5244313B0A20206C69676874436F6F72645F32203D2028756E6974795F576F726C64546F4C69676874202A20746D707661725F39292E78797A3B0A202068696768702076656333207665635F31303B0A20207665635F3130203D2028786C765F544558434F4F524431202D205F4C69676874506F736974696F6E52616E67652E78797A293B0A20206869676870207665633420736861646F7756616C735F31313B0A2020686967687020666C6F6174206D79646973745F31323B0A20206D79646973745F3132203D20282873717274280A20202020646F7420287665635F31302C207665635F3130290A202029202A205F4C69676874506F736974696F6E52616E67652E7729202A20302E3937293B0A2020736861646F7756616C735F31312E78203D20646F742028746578747572654375626520285F536861646F774D6170546578747572652C20287665635F3130202B207665633328302E303037383132352C20302E303037383132352C20302E303037383132352929292C207665633428312E302C20302E3030333932313536392C20312E3533373837652D30352C20362E303330383633652D303829293B0A2020736861646F7756616C735F31312E79203D20646F742028746578747572654375626520285F536861646F774D6170546578747572652C20287665635F3130202B2076656333282D302E303037383132352C202D302E303037383132352C20302E303037383132352929292C207665633428312E302C20302E3030333932313536392C20312E3533373837652D30352C20362E303330383633652D303829293B0A2020736861646F7756616C735F31312E7A203D20646F742028746578747572654375626520285F536861646F774D6170546578747572652C20287665635F3130202B2076656333282D302E303037383132352C20302E303037383132352C202D302E303037383132352929292C207665633428312E302C20302E3030333932313536392C20312E3533373837652D30352C20362E303330383633652D303829293B0A2020736861646F7756616C735F31312E77203D20646F742028746578747572654375626520285F536861646F774D6170546578747572652C20287665635F3130202B207665633328302E303037383132352C202D302E303037383132352C202D302E303037383132352929292C207665633428312E302C20302E3030333932313536392C20312E3533373837652D30352C20362E303330383633652D303829293B0A2020627665633420746D707661725F31333B0A2020746D707661725F3133203D206C6573735468616E2028736861646F7756616C735F31312C2076656334286D79646973745F313229293B0A20206D656469756D70207665633420746D707661725F31343B0A2020746D707661725F3134203D205F4C69676874536861646F77446174612E787878783B0A20206D656469756D7020666C6F617420746D707661725F31353B0A202069662028746D707661725F31332E7829207B0A20202020746D707661725F3135203D20746D707661725F31342E783B0A20207D20656C7365207B0A20202020746D707661725F3135203D20312E303B0A20207D3B0A20206D656469756D7020666C6F617420746D707661725F31363B0A202069662028746D707661725F31332E7929207B0A20202020746D707661725F3136203D20746D707661725F31342E793B0A20207D20656C7365207B0A20202020746D707661725F3136203D20312E303B0A20207D3B0A20206D656469756D7020666C6F617420746D707661725F31373B0A202069662028746D707661725F31332E7A29207B0A20202020746D707661725F3137203D20746D707661725F31342E7A3B0A20207D20656C7365207B0A20202020746D707661725F3137203D20312E303B0A20207D3B0A20206D656469756D7020666C6F617420746D707661725F31383B0A202069662028746D707661725F31332E7729207B0A20202020746D707661725F3138203D20746D707661725F31342E773B0A20207D20656C7365207B0A20202020746D707661725F3138203D20312E303B0A20207D3B0A20206D656469756D70207665633420746D707661725F31393B0A2020746D707661725F31392E78203D20746D707661725F31353B0A2020746D707661725F31392E79203D20746D707661725F31363B0A2020746D707661725F31392E7A203D20746D707661725F31373B0A2020746D707661725F31392E77203D20746D707661725F31383B0A20206D656469756D7020666C6F617420746D707661725F32303B0A2020746D707661725F3230203D20646F742028746D707661725F31392C207665633428302E32352C20302E32352C20302E32352C20302E323529293B0A2020736861646F775F31203D20746D707661725F32303B0A2020686967687020666C6F617420746D707661725F32313B0A2020746D707661725F3231203D20646F7420286C69676874436F6F72645F322C206C69676874436F6F72645F32293B0A20206C6F777020666C6F617420746D707661725F32323B0A2020746D707661725F3232203D202874657874757265324420285F4C6967687454657874757265302C207665633228746D707661725F323129292E77202A20736861646F775F31293B0A2020635F33203D2028635F33202A2028746D707661725F3232202A20636C616D7020280A20202020646F742028786C765F544558434F4F5244352C20786C765F544558434F4F524434290A20202C20302E302C20312E302929293B0A20206D656469756D70207665633420746D707661725F32333B0A2020746D707661725F32332E77203D20312E303B0A2020746D707661725F32332E78797A203D20635F333B0A20206D656469756D70207665633420786C61745F7661726F75747075745F32343B0A2020786C61745F7661726F75747075745F32342E78797A203D20746D707661725F32332E78797A3B0A2020786C61745F7661726F75747075745F32342E77203D20312E303B0A2020676C5F46726167446174615B305D203D20786C61745F7661726F75747075745F32343B0A7D0A0A0A23656E6469660A0000001B000000000000000100000000000000000000000000000000000000324F040C0500000055000000030000000000000000000000020000000B000000444952454354494F4E414C000E0000005645525445584C494748545F4F4E0000462400002376657273696F6E203130300A0A236966646566205645525445580A6174747269627574652076656334205F676C65735665727465783B0A6174747269627574652076656333205F676C65734E6F726D616C3B0A6174747269627574652076656334205F676C65734D756C7469546578436F6F7264303B0A6174747269627574652076656334205F676C65734D756C7469546578436F6F7264313B0A756E69666F726D2068696768702076656333205F576F726C64537061636543616D657261506F733B0A756E69666F726D206869676870207665633420756E6974795F344C69676874506F7358303B0A756E69666F726D206869676870207665633420756E6974795F344C69676874506F7359303B0A756E69666F726D206869676870207665633420756E6974795F344C69676874506F735A303B0A756E69666F726D206D656469756D70207665633420756E6974795F344C69676874417474656E303B0A756E69666F726D206D656469756D70207665633420756E6974795F4C69676874436F6C6F725B385D3B0A756E69666F726D206D656469756D70207665633420756E6974795F534842723B0A756E69666F726D206D656469756D70207665633420756E6974795F534842673B0A756E69666F726D206D656469756D70207665633420756E6974795F534842623B0A756E69666F726D206D656469756D70207665633420756E6974795F5348433B0A756E69666F726D206869676870206D61743420756E6974795F4F626A656374546F576F726C643B0A756E69666F726D206869676870206D61743420756E6974795F576F726C64546F4F626A6563743B0A756E69666F726D206869676870206D61743420756E6974795F4D617472697856503B0A756E69666F726D2068696768702076656334205F4D61696E5465785F53543B0A756E69666F726D2068696768702076656334205F44657461696C416C6265646F4D61705F53543B0A756E69666F726D206D656469756D7020666C6F6174205F55565365633B0A76617279696E67206869676870207665633420786C765F544558434F4F5244303B0A76617279696E67206D656469756D70207665633320786C765F544558434F4F5244313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244323B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244325F313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244325F323B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244353B0A76617279696E67206869676870207665633220786C765F544558434F4F5244363B0A76617279696E67206869676870207665633320786C765F544558434F4F5244383B0A766F6964206D61696E2028290A7B0A20206D656469756D70207665633320746D707661725F313B0A2020746D707661725F31203D205F676C65734E6F726D616C3B0A20206869676870207665633220746D707661725F323B0A2020746D707661725F32203D205F676C65734D756C7469546578436F6F7264302E78793B0A20206869676870207665633220746D707661725F333B0A2020746D707661725F33203D205F676C65734D756C7469546578436F6F7264312E78793B0A20206D656469756D70207665633420746D707661725F343B0A20206D656469756D70207665633420746D707661725F353B0A20206D656469756D70207665633420746D707661725F363B0A20206869676870207665633220746D707661725F373B0A20206869676870207665633320746D707661725F383B0A20206869676870207665633420746D707661725F393B0A2020746D707661725F39203D2028756E6974795F4F626A656374546F576F726C64202A205F676C6573566572746578293B0A2020746D707661725F38203D20746D707661725F392E78797A3B0A20206869676870207665633420746D707661725F31303B0A20206869676870207665633420746D707661725F31313B0A2020746D707661725F31312E77203D20312E303B0A2020746D707661725F31312E78797A203D205F676C65735665727465782E78797A3B0A2020746D707661725F3130203D2028756E6974795F4D61747269785650202A2028756E6974795F4F626A656374546F576F726C64202A20746D707661725F313129293B0A20206869676870207665633420746578636F6F72645F31323B0A2020746578636F6F72645F31322E7879203D2028285F676C65734D756C7469546578436F6F7264302E7879202A205F4D61696E5465785F53542E787929202B205F4D61696E5465785F53542E7A77293B0A20206869676870207665633220746D707661725F31333B0A202069662028285F5556536563203D3D20302E302929207B0A20202020746D707661725F3133203D20746D707661725F323B0A20207D20656C7365207B0A20202020746D707661725F3133203D20746D707661725F333B0A20207D3B0A2020746578636F6F72645F31322E7A77203D202828746D707661725F3133202A205F44657461696C416C6265646F4D61705F53542E787929202B205F44657461696C416C6265646F4D61705F53542E7A77293B0A20206D656469756D70207665633320746D707661725F31343B0A202068696768702076656333206E5F31353B0A20206E5F3135203D2028746D707661725F392E78797A202D205F576F726C64537061636543616D657261506F73293B0A2020746D707661725F3134203D206E5F31353B0A202068696768702076656333206E6F726D5F31363B0A20206E6F726D5F3136203D20746D707661725F313B0A20206869676870206D61743320746D707661725F31373B0A2020746D707661725F31375B305D203D20756E6974795F576F726C64546F4F626A6563745B305D2E78797A3B0A2020746D707661725F31375B315D203D20756E6974795F576F726C64546F4F626A6563745B315D2E78797A3B0A2020746D707661725F31375B325D203D20756E6974795F576F726C64546F4F626A6563745B325D2E78797A3B0A20206869676870207665633320746D707661725F31383B0A2020746D707661725F3138203D206E6F726D616C697A6528286E6F726D5F3136202A20746D707661725F313729293B0A2020746D707661725F342E78797A203D207665633328302E302C20302E302C20302E30293B0A2020746D707661725F352E78797A203D207665633328302E302C20302E302C20302E30293B0A2020746D707661725F362E78797A203D20746D707661725F31383B0A20206D656469756D702076656333206E6F726D616C576F726C645F31393B0A20206E6F726D616C576F726C645F3139203D20746D707661725F31383B0A20206D656469756D70207665633420616D6269656E744F724C696768746D617055565F32303B0A2020616D6269656E744F724C696768746D617055565F32302E77203D20302E303B0A202068696768702076656333206C69676874436F6C6F72305F32313B0A20206C69676874436F6C6F72305F3231203D20756E6974795F4C69676874436F6C6F725B305D2E78797A3B0A202068696768702076656333206C69676874436F6C6F72315F32323B0A20206C69676874436F6C6F72315F3232203D20756E6974795F4C69676874436F6C6F725B315D2E78797A3B0A202068696768702076656333206C69676874436F6C6F72325F32333B0A20206C69676874436F6C6F72325F3233203D20756E6974795F4C69676874436F6C6F725B325D2E78797A3B0A202068696768702076656333206C69676874436F6C6F72335F32343B0A20206C69676874436F6C6F72335F3234203D20756E6974795F4C69676874436F6C6F725B335D2E78797A3B0A202068696768702076656334206C69676874417474656E53715F32353B0A20206C69676874417474656E53715F3235203D20756E6974795F344C69676874417474656E303B0A202068696768702076656333206E6F726D616C5F32363B0A20206E6F726D616C5F3236203D206E6F726D616C576F726C645F31393B0A20206869676870207665633320636F6C5F32373B0A202068696768702076656334206E646F746C5F32383B0A202068696768702076656334206C656E67746853715F32393B0A20206869676870207665633420746D707661725F33303B0A2020746D707661725F3330203D2028756E6974795F344C69676874506F735830202D20746D707661725F392E78293B0A20206869676870207665633420746D707661725F33313B0A2020746D707661725F3331203D2028756E6974795F344C69676874506F735930202D20746D707661725F392E79293B0A20206869676870207665633420746D707661725F33323B0A2020746D707661725F3332203D2028756E6974795F344C69676874506F735A30202D20746D707661725F392E7A293B0A20206C656E67746853715F3239203D2028746D707661725F3330202A20746D707661725F3330293B0A20206C656E67746853715F3239203D20286C656E67746853715F3239202B2028746D707661725F3331202A20746D707661725F333129293B0A20206C656E67746853715F3239203D20286C656E67746853715F3239202B2028746D707661725F3332202A20746D707661725F333229293B0A20206869676870207665633420746D707661725F33333B0A2020746D707661725F3333203D206D617820286C656E67746853715F32392C20766563342831652D30362C2031652D30362C2031652D30362C2031652D303629293B0A20206C656E67746853715F3239203D20746D707661725F33333B0A20206E646F746C5F3238203D2028746D707661725F3330202A206E6F726D616C5F32362E78293B0A20206E646F746C5F3238203D20286E646F746C5F3238202B2028746D707661725F3331202A206E6F726D616C5F32362E7929293B0A20206E646F746C5F3238203D20286E646F746C5F3238202B2028746D707661725F3332202A206E6F726D616C5F32362E7A29293B0A20206869676870207665633420746D707661725F33343B0A2020746D707661725F3334203D206D617820287665633428302E302C20302E302C20302E302C20302E30292C20286E646F746C5F3238202A20696E76657273657371727428746D707661725F33332929293B0A20206E646F746C5F3238203D20746D707661725F33343B0A20206869676870207665633420746D707661725F33353B0A2020746D707661725F3335203D2028746D707661725F3334202A2028312E302F2828312E30202B200A2020202028746D707661725F3333202A206C69676874417474656E53715F3235290A2020292929293B0A2020636F6C5F3237203D20286C69676874436F6C6F72305F3231202A20746D707661725F33352E78293B0A2020636F6C5F3237203D2028636F6C5F3237202B20286C69676874436F6C6F72315F3232202A20746D707661725F33352E7929293B0A2020636F6C5F3237203D2028636F6C5F3237202B20286C69676874436F6C6F72325F3233202A20746D707661725F33352E7A29293B0A2020636F6C5F3237203D2028636F6C5F3237202B20286C69676874436F6C6F72335F3234202A20746D707661725F33352E7729293B0A2020616D6269656E744F724C696768746D617055565F32302E78797A203D20636F6C5F32373B0A20206D656469756D7020766563332078315F33363B0A20206D656469756D70207665633420746D707661725F33373B0A2020746D707661725F3337203D20286E6F726D616C576F726C645F31392E78797A7A202A206E6F726D616C576F726C645F31392E797A7A78293B0A202078315F33362E78203D20646F742028756E6974795F534842722C20746D707661725F3337293B0A202078315F33362E79203D20646F742028756E6974795F534842672C20746D707661725F3337293B0A202078315F33362E7A203D20646F742028756E6974795F534842622C20746D707661725F3337293B0A2020616D6269656E744F724C696768746D617055565F32302E78797A203D202828616D6269656E744F724C696768746D617055565F32302E78797A202A20280A2020202028616D6269656E744F724C696768746D617055565F32302E78797A202A202828616D6269656E744F724C696768746D617055565F32302E78797A202A20302E33303533303629202B20302E3638323137313129290A2020202B20302E30313235323238382929202B202878315F3336202B2028756E6974795F5348432E78797A202A200A2020202028286E6F726D616C576F726C645F31392E78202A206E6F726D616C576F726C645F31392E7829202D20286E6F726D616C576F726C645F31392E79202A206E6F726D616C576F726C645F31392E7929290A20202929293B0A2020676C5F506F736974696F6E203D20746D707661725F31303B0A2020786C765F544558434F4F524430203D20746578636F6F72645F31323B0A2020786C765F544558434F4F524431203D20746D707661725F31343B0A2020786C765F544558434F4F524432203D20746D707661725F343B0A2020786C765F544558434F4F5244325F31203D20746D707661725F353B0A2020786C765F544558434F4F5244325F32203D20746D707661725F363B0A2020786C765F544558434F4F524435203D20616D6269656E744F724C696768746D617055565F32303B0A2020786C765F544558434F4F524436203D20746D707661725F373B0A2020786C765F544558434F4F524438203D20746D707661725F383B0A7D0A0A0A23656E6469660A23696664656620465241474D454E540A23657874656E73696F6E20474C5F4558545F7368616465725F746578747572655F6C6F64203A20656E61626C650A6C6F7770207665633420696D706C5F6C6F775F74657874757265437562654C6F64455854286C6F77702073616D706C6572437562652073616D706C65722C206869676870207665633320636F6F72642C206D656469756D7020666C6F6174206C6F64290A7B0A23696620646566696E656428474C5F4558545F7368616465725F746578747572655F6C6F64290A0972657475726E2074657874757265437562654C6F644558542873616D706C65722C20636F6F72642C206C6F64293B0A23656C73650A0972657475726E2074657874757265437562652873616D706C65722C20636F6F72642C206C6F64293B0A23656E6469660A7D0A0A756E69666F726D206D656469756D702076656334205F576F726C6453706163654C69676874506F73303B0A756E69666F726D206D656469756D70207665633420756E6974795F534841723B0A756E69666F726D206D656469756D70207665633420756E6974795F534841673B0A756E69666F726D206D656469756D70207665633420756E6974795F534841623B0A756E69666F726D206C6F77702073616D706C65724375626520756E6974795F5370656343756265303B0A756E69666F726D206D656469756D70207665633420756E6974795F5370656343756265305F4844523B0A756E69666F726D206C6F77702076656334205F4C69676874436F6C6F72303B0A756E69666F726D206D656469756D702076656334205F436F6C6F723B0A756E69666F726D2073616D706C65723244205F4D61696E5465783B0A756E69666F726D206D656469756D7020666C6F6174205F4D6574616C6C69633B0A756E69666F726D206D656469756D7020666C6F6174205F476C6F7373696E6573733B0A756E69666F726D2073616D706C65723244205F4F63636C7573696F6E4D61703B0A756E69666F726D206D656469756D7020666C6F6174205F4F63636C7573696F6E537472656E6774683B0A76617279696E67206869676870207665633420786C765F544558434F4F5244303B0A76617279696E67206D656469756D70207665633320786C765F544558434F4F5244313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244325F323B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244353B0A766F6964206D61696E2028290A7B0A20206D656469756D70207665633420635F313B0A20206C6F7770207665633420746D707661725F323B0A2020746D707661725F32203D2074657874757265324420285F4D61696E5465782C20786C765F544558434F4F5244302E7879293B0A20206D656469756D70207665633320746D707661725F333B0A2020746D707661725F33203D20285F436F6C6F722E78797A202A20746D707661725F322E78797A293B0A20206D656469756D70207665633320746D707661725F343B0A20206D656469756D70207665633320746D707661725F353B0A2020746D707661725F35203D206D697820287665633328302E323230393136332C20302E323230393136332C20302E32323039313633292C20746D707661725F332C2076656333285F4D6574616C6C696329293B0A20206D656469756D7020666C6F617420746D707661725F363B0A2020746D707661725F36203D2028302E37373930383337202D20285F4D6574616C6C6963202A20302E3737393038333729293B0A2020746D707661725F34203D2028746D707661725F33202A20746D707661725F36293B0A20206D656469756D70207665633320746D707661725F373B0A2020746D707661725F37203D206E6F726D616C697A6528786C765F544558434F4F5244325F322E78797A293B0A20206D656469756D70207665633320746D707661725F383B0A2020746D707661725F38203D206E6F726D616C697A6528786C765F544558434F4F524431293B0A20206D656469756D70207665633320746D707661725F393B0A2020746D707661725F39203D205F4C69676874436F6C6F72302E78797A3B0A20206D656469756D7020666C6F6174206F63635F31303B0A20206C6F777020666C6F617420746D707661725F31313B0A2020746D707661725F3131203D2074657874757265324420285F4F63636C7573696F6E4D61702C20786C765F544558434F4F5244302E7879292E793B0A20206F63635F3130203D20746D707661725F31313B0A20206D656469756D7020666C6F6174206F63636C7573696F6E5F31323B0A20206F63636C7573696F6E5F3132203D202828312E30202D205F4F63636C7573696F6E537472656E67746829202B20286F63635F3130202A205F4F63636C7573696F6E537472656E67746829293B0A20206869676870207665633420746D707661725F31333B0A2020746D707661725F3133203D20756E6974795F5370656343756265305F4844523B0A20206D656469756D70207665633420746D707661725F31343B0A2020746D707661725F31342E77203D20312E303B0A2020746D707661725F31342E78797A203D20746D707661725F373B0A20206D656469756D70207665633320785F31353B0A2020785F31352E78203D20646F742028756E6974795F534841722C20746D707661725F3134293B0A2020785F31352E79203D20646F742028756E6974795F534841672C20746D707661725F3134293B0A2020785F31352E7A203D20646F742028756E6974795F534841622C20746D707661725F3134293B0A20206D656469756D70207665633320746D707661725F31363B0A20206D656469756D7020666C6F617420746D707661725F31373B0A2020746D707661725F3137203D2028312E30202D205F476C6F7373696E657373293B0A2020746D707661725F3136203D2028746D707661725F38202D2028322E30202A20280A20202020646F742028746D707661725F372C20746D707661725F38290A2020202A20746D707661725F372929293B0A20206D656469756D702076656334206864725F31383B0A20206864725F3138203D20746D707661725F31333B0A20206D656469756D70207665633420746D707661725F31393B0A2020746D707661725F31392E78797A203D20746D707661725F31363B0A2020746D707661725F31392E77203D202828746D707661725F3137202A2028312E37202D200A2020202028302E37202A20746D707661725F3137290A20202929202A20362E30293B0A20206C6F7770207665633420746D707661725F32303B0A2020746D707661725F3230203D20696D706C5F6C6F775F74657874757265437562654C6F644558542028756E6974795F5370656343756265302C20746D707661725F31362C20746D707661725F31392E77293B0A20206D656469756D70207665633420746D707661725F32313B0A2020746D707661725F3231203D20746D707661725F32303B0A20206D656469756D70207665633320766965774469725F32323B0A2020766965774469725F3232203D202D28746D707661725F38293B0A20206D656469756D7020666C6F61742073706563756C61725465726D5F32333B0A20206D656469756D70207665633320746D707661725F32343B0A20206D656469756D70207665633320696E5665635F32353B0A2020696E5665635F3235203D20285F576F726C6453706163654C69676874506F73302E78797A202B20766965774469725F3232293B0A2020746D707661725F3234203D2028696E5665635F3235202A20696E766572736573717274286D61782028302E3030312C200A20202020646F742028696E5665635F32352C20696E5665635F3235290A20202929293B0A20206D656469756D7020666C6F617420746D707661725F32363B0A2020746D707661725F3236203D20636C616D702028646F742028746D707661725F372C20746D707661725F3234292C20302E302C20312E30293B0A20206D656469756D7020666C6F617420746D707661725F32373B0A2020746D707661725F3237203D2028746D707661725F3137202A20746D707661725F3137293B0A202073706563756C61725465726D5F3233203D202828746D707661725F3237202F20280A20202020286D61782028302E33322C20636C616D702028646F7420285F576F726C6453706163654C69676874506F73302E78797A2C20746D707661725F3234292C20302E302C20312E302929202A2028312E35202B20746D707661725F323729290A2020202A200A20202020282828746D707661725F3236202A20746D707661725F323629202A202828746D707661725F3237202A20746D707661725F323729202D20312E302929202B20312E3030303031290A20202929202D20302E30303031293B0A20206D656469756D7020666C6F617420746D707661725F32383B0A2020746D707661725F3238203D20636C616D70202873706563756C61725465726D5F32332C20302E302C203130302E30293B0A202073706563756C61725465726D5F3233203D20746D707661725F32383B0A20206D656469756D7020666C6F617420785F32393B0A2020785F3239203D2028312E30202D20636C616D702028646F742028746D707661725F372C20766965774469725F3232292C20302E302C20312E3029293B0A20206D656469756D70207665633320746D707661725F33303B0A2020746D707661725F3330203D202828280A202020202828746D707661725F34202B2028746D707661725F3238202A20746D707661725F352929202A20746D707661725F39290A2020202A200A20202020636C616D702028646F742028746D707661725F372C205F576F726C6453706163654C69676874506F73302E78797A292C20302E302C20312E30290A202029202B20280A20202020286D617820282828312E303535202A200A202020202020706F7720286D617820287665633328302E302C20302E302C20302E30292C2028786C765F544558434F4F5244352E78797A202B20785F313529292C207665633328302E343136363636372C20302E343136363636372C20302E3431363636363729290A2020202029202D20302E303535292C207665633328302E302C20302E302C20302E302929202A206F63636C7573696F6E5F3132290A2020202A20746D707661725F342929202B2028280A2020202028312E30202D202828746D707661725F3237202A20746D707661725F313729202A20302E323829290A2020202A200A202020202828286864725F31382E78202A20280A202020202020286864725F31382E77202A2028746D707661725F32312E77202D20312E3029290A20202020202B20312E302929202A20746D707661725F32312E78797A29202A206F63636C7573696F6E5F3132290A202029202A206D69782028746D707661725F352C2076656333280A20202020636C616D702028285F476C6F7373696E657373202B2028312E30202D20746D707661725F3629292C20302E302C20312E30290A2020292C2076656333280A202020202828785F3239202A20785F323929202A2028785F3239202A20785F323929290A2020292929293B0A20206D656469756D70207665633420746D707661725F33313B0A2020746D707661725F33312E77203D20312E303B0A2020746D707661725F33312E78797A203D20746D707661725F33303B0A2020635F312E77203D20746D707661725F33312E773B0A2020635F312E78797A203D20746D707661725F33303B0A20206D656469756D70207665633420786C61745F7661726F75747075745F33323B0A2020786C61745F7661726F75747075745F33322E78797A203D20635F312E78797A3B0A2020786C61745F7661726F75747075745F33322E77203D20312E303B0A2020676C5F46726167446174615B305D203D20786C61745F7661726F75747075745F33323B0A7D0A0A0A23656E6469660A00001B000000000000000100000000000000000000000000000000000000324F040C05000000170000000300000000000000000000000100000005000000504F494E54000000DE1000002376657273696F6E203130300A0A236966646566205645525445580A6174747269627574652076656334205F676C65735665727465783B0A6174747269627574652076656333205F676C65734E6F726D616C3B0A6174747269627574652076656334205F676C65734D756C7469546578436F6F7264303B0A6174747269627574652076656334205F676C65734D756C7469546578436F6F7264313B0A756E69666F726D2068696768702076656333205F576F726C64537061636543616D657261506F733B0A756E69666F726D2068696768702076656334205F576F726C6453706163654C69676874506F73303B0A756E69666F726D206869676870206D61743420756E6974795F4F626A656374546F576F726C643B0A756E69666F726D206869676870206D61743420756E6974795F576F726C64546F4F626A6563743B0A756E69666F726D206869676870206D61743420756E6974795F4D617472697856503B0A756E69666F726D2068696768702076656334205F4D61696E5465785F53543B0A756E69666F726D2068696768702076656334205F44657461696C416C6265646F4D61705F53543B0A756E69666F726D206D656469756D7020666C6F6174205F55565365633B0A76617279696E67206869676870207665633420786C765F544558434F4F5244303B0A76617279696E67206869676870207665633320786C765F544558434F4F5244313B0A76617279696E67206869676870207665633220786C765F544558434F4F5244323B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244333B0A76617279696E67206D656469756D70207665633320786C765F544558434F4F5244343B0A76617279696E67206D656469756D70207665633320786C765F544558434F4F5244353B0A766F6964206D61696E2028290A7B0A20206D656469756D70207665633320746D707661725F313B0A2020746D707661725F31203D205F676C65734E6F726D616C3B0A20206869676870207665633220746D707661725F323B0A2020746D707661725F32203D205F676C65734D756C7469546578436F6F7264302E78793B0A20206869676870207665633220746D707661725F333B0A2020746D707661725F33203D205F676C65734D756C7469546578436F6F7264312E78793B0A20206D656469756D702076656333206E6F726D616C576F726C645F343B0A20206D656469756D702076656333206579655665635F353B0A20206D656469756D702076656333206C696768744469725F363B0A20206869676870207665633220746D707661725F373B0A20206D656469756D70207665633420746D707661725F383B0A20206869676870207665633420746D707661725F393B0A2020746D707661725F39203D2028756E6974795F4F626A656374546F576F726C64202A205F676C6573566572746578293B0A20206869676870207665633420746D707661725F31303B0A20206869676870207665633420746D707661725F31313B0A2020746D707661725F31312E77203D20312E303B0A2020746D707661725F31312E78797A203D205F676C65735665727465782E78797A3B0A2020746D707661725F3130203D2028756E6974795F4D61747269785650202A2028756E6974795F4F626A656374546F576F726C64202A20746D707661725F313129293B0A20206869676870207665633420746578636F6F72645F31323B0A2020746578636F6F72645F31322E7879203D2028285F676C65734D756C7469546578436F6F7264302E7879202A205F4D61696E5465785F53542E787929202B205F4D61696E5465785F53542E7A77293B0A20206869676870207665633220746D707661725F31333B0A202069662028285F5556536563203D3D20302E302929207B0A20202020746D707661725F3133203D20746D707661725F323B0A20207D20656C7365207B0A20202020746D707661725F3133203D20746D707661725F333B0A20207D3B0A2020746578636F6F72645F31322E7A77203D202828746D707661725F3133202A205F44657461696C416C6265646F4D61705F53542E787929202B205F44657461696C416C6265646F4D61705F53542E7A77293B0A20206869676870207665633320746D707661725F31343B0A2020746D707661725F3134203D20285F576F726C6453706163654C69676874506F73302E78797A202D2028746D707661725F392E78797A202A205F576F726C6453706163654C69676874506F73302E7729293B0A20206C696768744469725F36203D20746D707661725F31343B0A20206D656469756D70207665633320746D707661725F31353B0A202068696768702076656333206E5F31363B0A20206E5F3136203D206C696768744469725F363B0A20206869676870207665633320746D707661725F31373B0A2020746D707661725F3137203D206E6F726D616C697A65286E5F3136293B0A2020746D707661725F3135203D20746D707661725F31373B0A20206C696768744469725F36203D20746D707661725F31353B0A20206869676870207665633320746D707661725F31383B0A2020746D707661725F3138203D206E6F726D616C697A652828746D707661725F392E78797A202D205F576F726C64537061636543616D657261506F7329293B0A20206579655665635F35203D20746D707661725F31383B0A202068696768702076656333206E6F726D5F31393B0A20206E6F726D5F3139203D20746D707661725F313B0A20206869676870206D61743320746D707661725F32303B0A2020746D707661725F32305B305D203D20756E6974795F576F726C64546F4F626A6563745B305D2E78797A3B0A2020746D707661725F32305B315D203D20756E6974795F576F726C64546F4F626A6563745B315D2E78797A3B0A2020746D707661725F32305B325D203D20756E6974795F576F726C64546F4F626A6563745B325D2E78797A3B0A20206869676870207665633320746D707661725F32313B0A2020746D707661725F3231203D206E6F726D616C697A6528286E6F726D5F3139202A20746D707661725F323029293B0A20206E6F726D616C576F726C645F34203D20746D707661725F32313B0A2020746D707661725F382E797A77203D20286579655665635F35202D2028322E30202A20280A20202020646F7420286E6F726D616C576F726C645F342C206579655665635F35290A2020202A206E6F726D616C576F726C645F342929293B0A2020676C5F506F736974696F6E203D20746D707661725F31303B0A2020786C765F544558434F4F524430203D20746578636F6F72645F31323B0A2020786C765F544558434F4F524431203D20746D707661725F392E78797A3B0A2020786C765F544558434F4F524432203D20746D707661725F373B0A2020786C765F544558434F4F524433203D20746D707661725F383B0A2020786C765F544558434F4F524434203D20746D707661725F31353B0A2020786C765F544558434F4F524435203D206E6F726D616C576F726C645F343B0A7D0A0A0A23656E6469660A23696664656620465241474D454E540A756E69666F726D206C6F77702076656334205F4C69676874436F6C6F72303B0A756E69666F726D2073616D706C6572324420756E6974795F4E4878526F7567686E6573733B0A756E69666F726D206D656469756D702076656334205F436F6C6F723B0A756E69666F726D2073616D706C65723244205F4D61696E5465783B0A756E69666F726D206D656469756D7020666C6F6174205F4D6574616C6C69633B0A756E69666F726D206D656469756D7020666C6F6174205F476C6F7373696E6573733B0A756E69666F726D2073616D706C65723244205F4C6967687454657874757265303B0A756E69666F726D206869676870206D61743420756E6974795F576F726C64546F4C696768743B0A76617279696E67206869676870207665633420786C765F544558434F4F5244303B0A76617279696E67206869676870207665633320786C765F544558434F4F5244313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244333B0A76617279696E67206D656469756D70207665633320786C765F544558434F4F5244343B0A76617279696E67206D656469756D70207665633320786C765F544558434F4F5244353B0A766F6964206D61696E2028290A7B0A202068696768702076656333206C69676874436F6F72645F313B0A20206D656469756D70207665633320635F323B0A20206C6F7770207665633420746D707661725F333B0A2020746D707661725F33203D2074657874757265324420285F4D61696E5465782C20786C765F544558434F4F5244302E7879293B0A20206D656469756D70207665633320746D707661725F343B0A2020746D707661725F34203D20285F436F6C6F722E78797A202A20746D707661725F332E78797A293B0A20206D656469756D7020666C6F617420746D707661725F353B0A2020746D707661725F35203D20646F742028786C765F544558434F4F5244332E797A772C20786C765F544558434F4F524434293B0A20206D656469756D70207665633220746D707661725F363B0A2020746D707661725F362E78203D202828746D707661725F35202A20746D707661725F3529202A2028746D707661725F35202A20746D707661725F3529293B0A2020746D707661725F362E79203D2028312E30202D205F476C6F7373696E657373293B0A20206C6F7770207665633420746D707661725F373B0A2020746D707661725F37203D207465787475726532442028756E6974795F4E4878526F7567686E6573732C20746D707661725F36293B0A2020635F32203D20282828746D707661725F34202A200A2020202028302E37373930383337202D20285F4D6574616C6C6963202A20302E3737393038333729290A202029202B20280A2020202028746D707661725F372E77202A2031362E30290A2020202A200A202020206D697820287665633328302E323230393136332C20302E323230393136332C20302E32323039313633292C20746D707661725F342C2076656333285F4D6574616C6C696329290A20202929202A205F4C69676874436F6C6F72302E78797A293B0A20206869676870207665633420746D707661725F383B0A2020746D707661725F382E77203D20312E303B0A2020746D707661725F382E78797A203D20786C765F544558434F4F5244313B0A20206C69676874436F6F72645F31203D2028756E6974795F576F726C64546F4C69676874202A20746D707661725F38292E78797A3B0A2020686967687020666C6F617420746D707661725F393B0A2020746D707661725F39203D20646F7420286C69676874436F6F72645F312C206C69676874436F6F72645F31293B0A20206C6F777020666C6F617420746D707661725F31303B0A2020746D707661725F3130203D2074657874757265324420285F4C6967687454657874757265302C207665633228746D707661725F3929292E773B0A2020635F32203D2028635F32202A2028746D707661725F3130202A20636C616D7020280A20202020646F742028786C765F544558434F4F5244352C20786C765F544558434F4F524434290A20202C20302E302C20312E302929293B0A20206D656469756D70207665633420746D707661725F31313B0A2020746D707661725F31312E77203D20312E303B0A2020746D707661725F31312E78797A203D20635F323B0A20206D656469756D70207665633420786C61745F7661726F75747075745F31323B0A2020786C61745F7661726F75747075745F31322E78797A203D20746D707661725F31312E78797A3B0A2020786C61745F7661726F75747075745F31322E77203D20312E303B0A2020676C5F46726167446174615B305D203D20786C61745F7661726F75747075745F31323B0A7D0A0A0A23656E6469660A00001B000000000000000100000000000000000000000000000000000000324F040C0500000026000000040000000000000000000000010000000B000000444952454354494F4E414C00B71900002376657273696F6E203130300A0A236966646566205645525445580A6174747269627574652076656334205F676C65735665727465783B0A6174747269627574652076656333205F676C65734E6F726D616C3B0A6174747269627574652076656334205F676C65734D756C7469546578436F6F7264303B0A6174747269627574652076656334205F676C65734D756C7469546578436F6F7264313B0A756E69666F726D2068696768702076656333205F576F726C64537061636543616D657261506F733B0A756E69666F726D206D656469756D70207665633420756E6974795F534841723B0A756E69666F726D206D656469756D70207665633420756E6974795F534841673B0A756E69666F726D206D656469756D70207665633420756E6974795F534841623B0A756E69666F726D206D656469756D70207665633420756E6974795F534842723B0A756E69666F726D206D656469756D70207665633420756E6974795F534842673B0A756E69666F726D206D656469756D70207665633420756E6974795F534842623B0A756E69666F726D206D656469756D70207665633420756E6974795F5348433B0A756E69666F726D206869676870206D61743420756E6974795F4F626A656374546F576F726C643B0A756E69666F726D206869676870206D61743420756E6974795F576F726C64546F4F626A6563743B0A756E69666F726D206869676870206D61743420756E6974795F4D617472697856503B0A756E69666F726D2068696768702076656334205F4D61696E5465785F53543B0A756E69666F726D2068696768702076656334205F44657461696C416C6265646F4D61705F53543B0A756E69666F726D206D656469756D7020666C6F6174205F4D6574616C6C69633B0A756E69666F726D206D656469756D7020666C6F6174205F476C6F7373696E6573733B0A756E69666F726D206D656469756D7020666C6F6174205F55565365633B0A76617279696E67206869676870207665633420786C765F544558434F4F5244303B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244323B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244343B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244353B0A766F6964206D61696E2028290A7B0A20206D656469756D70207665633320746D707661725F313B0A2020746D707661725F31203D205F676C65734E6F726D616C3B0A20206869676870207665633220746D707661725F323B0A2020746D707661725F32203D205F676C65734D756C7469546578436F6F7264302E78793B0A20206869676870207665633220746D707661725F333B0A2020746D707661725F33203D205F676C65734D756C7469546578436F6F7264312E78793B0A20206D656469756D702076656333206E6F726D616C576F726C645F343B0A20206D656469756D702076656333206579655665635F353B0A20206D656469756D70207665633420746D707661725F363B0A20206D656469756D70207665633420746D707661725F373B0A20206D656469756D70207665633420746D707661725F383B0A20206869676870207665633420746D707661725F393B0A2020746D707661725F39203D2028756E6974795F4F626A656374546F576F726C64202A205F676C6573566572746578293B0A20206869676870207665633420746D707661725F31303B0A20206869676870207665633420746D707661725F31313B0A2020746D707661725F31312E77203D20312E303B0A2020746D707661725F31312E78797A203D205F676C65735665727465782E78797A3B0A2020746D707661725F3130203D2028756E6974795F4D61747269785650202A2028756E6974795F4F626A656374546F576F726C64202A20746D707661725F313129293B0A20206869676870207665633420746578636F6F72645F31323B0A2020746578636F6F72645F31322E7879203D2028285F676C65734D756C7469546578436F6F7264302E7879202A205F4D61696E5465785F53542E787929202B205F4D61696E5465785F53542E7A77293B0A20206869676870207665633220746D707661725F31333B0A202069662028285F5556536563203D3D20302E302929207B0A20202020746D707661725F3133203D20746D707661725F323B0A20207D20656C7365207B0A20202020746D707661725F3133203D20746D707661725F333B0A20207D3B0A2020746578636F6F72645F31322E7A77203D202828746D707661725F3133202A205F44657461696C416C6265646F4D61705F53542E787929202B205F44657461696C416C6265646F4D61705F53542E7A77293B0A20206869676870207665633320746D707661725F31343B0A2020746D707661725F3134203D206E6F726D616C697A652828746D707661725F392E78797A202D205F576F726C64537061636543616D657261506F7329293B0A20206579655665635F35203D20746D707661725F31343B0A202068696768702076656333206E6F726D5F31353B0A20206E6F726D5F3135203D20746D707661725F313B0A20206869676870206D61743320746D707661725F31363B0A2020746D707661725F31365B305D203D20756E6974795F576F726C64546F4F626A6563745B305D2E78797A3B0A2020746D707661725F31365B315D203D20756E6974795F576F726C64546F4F626A6563745B315D2E78797A3B0A2020746D707661725F31365B325D203D20756E6974795F576F726C64546F4F626A6563745B325D2E78797A3B0A20206869676870207665633320746D707661725F31373B0A2020746D707661725F3137203D206E6F726D616C697A6528286E6F726D5F3135202A20746D707661725F313629293B0A20206E6F726D616C576F726C645F34203D20746D707661725F31373B0A2020746D707661725F382E78797A203D206E6F726D616C576F726C645F343B0A2020746D707661725F362E78797A203D206579655665635F353B0A20206D656469756D70207665633420616D6269656E744F724C696768746D617055565F31383B0A2020616D6269656E744F724C696768746D617055565F31382E77203D20302E303B0A20206D656469756D70207665633420746D707661725F31393B0A2020746D707661725F31392E77203D20312E303B0A2020746D707661725F31392E78797A203D206E6F726D616C576F726C645F343B0A20206D656469756D702076656333207265735F32303B0A20206D656469756D70207665633320785F32313B0A2020785F32312E78203D20646F742028756E6974795F534841722C20746D707661725F3139293B0A2020785F32312E79203D20646F742028756E6974795F534841672C20746D707661725F3139293B0A2020785F32312E7A203D20646F742028756E6974795F534841622C20746D707661725F3139293B0A20206D656469756D7020766563332078315F32323B0A20206D656469756D70207665633420746D707661725F32333B0A2020746D707661725F3233203D20286E6F726D616C576F726C645F342E78797A7A202A206E6F726D616C576F726C645F342E797A7A78293B0A202078315F32322E78203D20646F742028756E6974795F534842722C20746D707661725F3233293B0A202078315F32322E79203D20646F742028756E6974795F534842672C20746D707661725F3233293B0A202078315F32322E7A203D20646F742028756E6974795F534842622C20746D707661725F3233293B0A20207265735F3230203D2028785F3231202B202878315F3232202B2028756E6974795F5348432E78797A202A200A2020202028286E6F726D616C576F726C645F342E78202A206E6F726D616C576F726C645F342E7829202D20286E6F726D616C576F726C645F342E79202A206E6F726D616C576F726C645F342E7929290A20202929293B0A20206D656469756D70207665633320746D707661725F32343B0A2020746D707661725F3234203D206D617820282828312E303535202A200A20202020706F7720286D617820287265735F32302C207665633328302E302C20302E302C20302E3029292C207665633328302E343136363636372C20302E343136363636372C20302E3431363636363729290A202029202D20302E303535292C207665633328302E302C20302E302C20302E3029293B0A20207265735F3230203D20746D707661725F32343B0A2020616D6269656E744F724C696768746D617055565F31382E78797A203D206D617820287665633328302E302C20302E302C20302E30292C20746D707661725F3234293B0A2020746D707661725F372E797A77203D20286579655665635F35202D2028322E30202A20280A20202020646F7420286E6F726D616C576F726C645F342C206579655665635F35290A2020202A206E6F726D616C576F726C645F342929293B0A20206D656469756D7020666C6F617420785F32353B0A2020785F3235203D2028312E30202D20636C616D702028646F7420286E6F726D616C576F726C645F342C200A202020202D286579655665635F35290A2020292C20302E302C20312E3029293B0A2020746D707661725F382E77203D202828785F3235202A20785F323529202A2028785F3235202A20785F323529293B0A2020746D707661725F362E77203D20636C616D702028285F476C6F7373696E657373202B2028312E30202D200A2020202028302E37373930383337202D20285F4D6574616C6C6963202A20302E3737393038333729290A202029292C20302E302C20312E30293B0A2020676C5F506F736974696F6E203D20746D707661725F31303B0A2020786C765F544558434F4F524430203D20746578636F6F72645F31323B0A2020786C765F544558434F4F524431203D20746D707661725F363B0A2020786C765F544558434F4F524432203D20616D6269656E744F724C696768746D617055565F31383B0A2020786C765F544558434F4F524434203D20746D707661725F373B0A2020786C765F544558434F4F524435203D20746D707661725F383B0A7D0A0A0A23656E6469660A23696664656620465241474D454E540A23657874656E73696F6E20474C5F4558545F7368616465725F746578747572655F6C6F64203A20656E61626C650A6C6F7770207665633420696D706C5F6C6F775F74657874757265437562654C6F64455854286C6F77702073616D706C6572437562652073616D706C65722C206869676870207665633320636F6F72642C206D656469756D7020666C6F6174206C6F64290A7B0A23696620646566696E656428474C5F4558545F7368616465725F746578747572655F6C6F64290A0972657475726E2074657874757265437562654C6F644558542873616D706C65722C20636F6F72642C206C6F64293B0A23656C73650A0972657475726E2074657874757265437562652873616D706C65722C20636F6F72642C206C6F64293B0A23656E6469660A7D0A0A756E69666F726D206D656469756D702076656334205F576F726C6453706163654C69676874506F73303B0A756E69666F726D206C6F77702073616D706C65724375626520756E6974795F5370656343756265303B0A756E69666F726D206D656469756D70207665633420756E6974795F5370656343756265305F4844523B0A756E69666F726D206C6F77702076656334205F4C69676874436F6C6F72303B0A756E69666F726D2073616D706C6572324420756E6974795F4E4878526F7567686E6573733B0A756E69666F726D206D656469756D702076656334205F436F6C6F723B0A756E69666F726D2073616D706C65723244205F4D61696E5465783B0A756E69666F726D206D656469756D7020666C6F6174205F4D6574616C6C69633B0A756E69666F726D206D656469756D7020666C6F6174205F476C6F7373696E6573733B0A756E69666F726D2073616D706C65723244205F4F63636C7573696F6E4D61703B0A756E69666F726D206D656469756D7020666C6F6174205F4F63636C7573696F6E537472656E6774683B0A76617279696E67206869676870207665633420786C765F544558434F4F5244303B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244323B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244343B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244353B0A766F6964206D61696E2028290A7B0A20206D656469756D7020666C6F617420726C5F313B0A20206C6F7770207665633420746D707661725F323B0A2020746D707661725F32203D2074657874757265324420285F4D61696E5465782C20786C765F544558434F4F5244302E7879293B0A20206D656469756D70207665633320746D707661725F333B0A2020746D707661725F33203D20285F436F6C6F722E78797A202A20746D707661725F322E78797A293B0A20206D656469756D70207665633320746D707661725F343B0A20206D656469756D70207665633320746D707661725F353B0A2020746D707661725F35203D206D697820287665633328302E323230393136332C20302E323230393136332C20302E32323039313633292C20746D707661725F332C2076656333285F4D6574616C6C696329293B0A2020746D707661725F34203D2028746D707661725F33202A2028302E37373930383337202D20285F4D6574616C6C6963202A20302E373739303833372929293B0A20206D656469756D70207665633320746D707661725F363B0A2020746D707661725F36203D205F4C69676874436F6C6F72302E78797A3B0A20206D656469756D7020666C6F6174206F63635F373B0A20206C6F777020666C6F617420746D707661725F383B0A2020746D707661725F38203D2074657874757265324420285F4F63636C7573696F6E4D61702C20786C765F544558434F4F5244302E7879292E793B0A20206F63635F37203D20746D707661725F383B0A2020726C5F31203D20646F742028786C765F544558434F4F5244342E797A772C205F576F726C6453706163654C69676874506F73302E78797A293B0A20206D656469756D7020666C6F6174206F63636C7573696F6E5F393B0A20206F63636C7573696F6E5F39203D202828312E30202D205F4F63636C7573696F6E537472656E67746829202B20286F63635F37202A205F4F63636C7573696F6E537472656E67746829293B0A20206869676870207665633420746D707661725F31303B0A2020746D707661725F3130203D20756E6974795F5370656343756265305F4844523B0A20206D656469756D7020666C6F617420746D707661725F31313B0A2020746D707661725F3131203D2028312E30202D205F476C6F7373696E657373293B0A20206D656469756D702076656334206864725F31323B0A20206864725F3132203D20746D707661725F31303B0A20206D656469756D70207665633420746D707661725F31333B0A2020746D707661725F31332E78797A203D20786C765F544558434F4F5244342E797A773B0A2020746D707661725F31332E77203D202828746D707661725F3131202A2028312E37202D200A2020202028302E37202A20746D707661725F3131290A20202929202A20362E30293B0A20206C6F7770207665633420746D707661725F31343B0A2020746D707661725F3134203D20696D706C5F6C6F775F74657874757265437562654C6F644558542028756E6974795F5370656343756265302C20786C765F544558434F4F5244342E797A772C20746D707661725F31332E77293B0A20206D656469756D70207665633420746D707661725F31353B0A2020746D707661725F3135203D20746D707661725F31343B0A20206D656469756D70207665633220746D707661725F31363B0A2020746D707661725F31362E78203D202828726C5F31202A20726C5F3129202A2028726C5F31202A20726C5F3129293B0A2020746D707661725F31362E79203D20746D707661725F31313B0A20206C6F7770207665633420746D707661725F31373B0A2020746D707661725F3137203D207465787475726532442028756E6974795F4E4878526F7567686E6573732C20746D707661725F3136293B0A20206D656469756D70207665633420746D707661725F31383B0A2020746D707661725F31382E77203D20312E303B0A2020746D707661725F31382E78797A203D202828280A2020202028786C765F544558434F4F5244322E78797A202A206F63636C7573696F6E5F39290A2020202A20746D707661725F3429202B20280A202020202828286864725F31322E78202A20280A202020202020286864725F31322E77202A2028746D707661725F31352E77202D20312E3029290A20202020202B20312E302929202A20746D707661725F31352E78797A29202A206F63636C7573696F6E5F39290A2020202A200A202020206D69782028746D707661725F352C20786C765F544558434F4F5244312E7777772C20786C765F544558434F4F5244352E777777290A20202929202B202828746D707661725F34202B200A202020202828746D707661725F31372E77202A2031362E3029202A20746D707661725F35290A202029202A2028746D707661725F36202A200A20202020636C616D702028646F742028786C765F544558434F4F5244352E78797A2C205F576F726C6453706163654C69676874506F73302E78797A292C20302E302C20312E30290A20202929293B0A20206D656469756D70207665633420786C61745F7661726F75747075745F31393B0A2020786C61745F7661726F75747075745F31392E78797A203D20746D707661725F31382E78797A3B0A2020786C61745F7661726F75747075745F31392E77203D20312E303B0A2020676C5F46726167446174615B305D203D20786C61745F7661726F75747075745F31393B0A7D0A0A0A23656E6469660A001B000000000000000100000000000000000000000000000000000000324F040C0500000031000000030000000000000000000000010000000400000053504F54421500002376657273696F6E203130300A0A236966646566205645525445580A6174747269627574652076656334205F676C65735665727465783B0A6174747269627574652076656333205F676C65734E6F726D616C3B0A6174747269627574652076656334205F676C65734D756C7469546578436F6F7264303B0A6174747269627574652076656334205F676C65734D756C7469546578436F6F7264313B0A756E69666F726D2068696768702076656333205F576F726C64537061636543616D657261506F733B0A756E69666F726D2068696768702076656334205F576F726C6453706163654C69676874506F73303B0A756E69666F726D206869676870206D61743420756E6974795F4F626A656374546F576F726C643B0A756E69666F726D206869676870206D61743420756E6974795F576F726C64546F4F626A6563743B0A756E69666F726D206869676870206D61743420756E6974795F4D617472697856503B0A756E69666F726D2068696768702076656334205F4D61696E5465785F53543B0A756E69666F726D2068696768702076656334205F44657461696C416C6265646F4D61705F53543B0A756E69666F726D206D656469756D7020666C6F6174205F55565365633B0A76617279696E67206869676870207665633420786C765F544558434F4F5244303B0A76617279696E67206D656469756D70207665633320786C765F544558434F4F5244313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244323B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244325F313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244325F323B0A76617279696E67206869676870207665633320786C765F544558434F4F5244353B0A76617279696E67206869676870207665633220786C765F544558434F4F5244363B0A766F6964206D61696E2028290A7B0A20206D656469756D70207665633320746D707661725F313B0A2020746D707661725F31203D205F676C65734E6F726D616C3B0A20206869676870207665633220746D707661725F323B0A2020746D707661725F32203D205F676C65734D756C7469546578436F6F7264302E78793B0A20206869676870207665633220746D707661725F333B0A2020746D707661725F33203D205F676C65734D756C7469546578436F6F7264312E78793B0A202068696768702076656333206C696768744469725F343B0A20206D656469756D70207665633420746D707661725F353B0A20206D656469756D70207665633420746D707661725F363B0A20206D656469756D70207665633420746D707661725F373B0A20206869676870207665633220746D707661725F383B0A20206869676870207665633420746D707661725F393B0A2020746D707661725F39203D2028756E6974795F4F626A656374546F576F726C64202A205F676C6573566572746578293B0A20206869676870207665633420746D707661725F31303B0A20206869676870207665633420746D707661725F31313B0A2020746D707661725F31312E77203D20312E303B0A2020746D707661725F31312E78797A203D205F676C65735665727465782E78797A3B0A2020746D707661725F3130203D2028756E6974795F4D61747269785650202A2028756E6974795F4F626A656374546F576F726C64202A20746D707661725F313129293B0A20206869676870207665633420746578636F6F72645F31323B0A2020746578636F6F72645F31322E7879203D2028285F676C65734D756C7469546578436F6F7264302E7879202A205F4D61696E5465785F53542E787929202B205F4D61696E5465785F53542E7A77293B0A20206869676870207665633220746D707661725F31333B0A202069662028285F5556536563203D3D20302E302929207B0A20202020746D707661725F3133203D20746D707661725F323B0A20207D20656C7365207B0A20202020746D707661725F3133203D20746D707661725F333B0A20207D3B0A2020746578636F6F72645F31322E7A77203D202828746D707661725F3133202A205F44657461696C416C6265646F4D61705F53542E787929202B205F44657461696C416C6265646F4D61705F53542E7A77293B0A20206D656469756D70207665633320746D707661725F31343B0A202068696768702076656333206E5F31353B0A20206E5F3135203D2028746D707661725F392E78797A202D205F576F726C64537061636543616D657261506F73293B0A2020746D707661725F3134203D206E5F31353B0A202068696768702076656333206E6F726D5F31363B0A20206E6F726D5F3136203D20746D707661725F313B0A20206869676870206D61743320746D707661725F31373B0A2020746D707661725F31375B305D203D20756E6974795F576F726C64546F4F626A6563745B305D2E78797A3B0A2020746D707661725F31375B315D203D20756E6974795F576F726C64546F4F626A6563745B315D2E78797A3B0A2020746D707661725F31375B325D203D20756E6974795F576F726C64546F4F626A6563745B325D2E78797A3B0A20206869676870207665633320746D707661725F31383B0A2020746D707661725F3138203D206E6F726D616C697A6528286E6F726D5F3136202A20746D707661725F313729293B0A2020746D707661725F352E78797A203D207665633328302E302C20302E302C20302E30293B0A2020746D707661725F362E78797A203D207665633328302E302C20302E302C20302E30293B0A2020746D707661725F372E78797A203D20746D707661725F31383B0A20206869676870207665633320746D707661725F31393B0A2020746D707661725F3139203D20285F576F726C6453706163654C69676874506F73302E78797A202D2028746D707661725F392E78797A202A205F576F726C6453706163654C69676874506F73302E7729293B0A20206D656469756D70207665633320746D707661725F32303B0A2020746D707661725F3230203D20746D707661725F31393B0A20206C696768744469725F34203D20746D707661725F32303B0A2020746D707661725F352E77203D206C696768744469725F342E783B0A2020746D707661725F362E77203D206C696768744469725F342E793B0A2020746D707661725F372E77203D206C696768744469725F342E7A3B0A2020676C5F506F736974696F6E203D20746D707661725F31303B0A2020786C765F544558434F4F524430203D20746578636F6F72645F31323B0A2020786C765F544558434F4F524431203D20746D707661725F31343B0A2020786C765F544558434F4F524432203D20746D707661725F353B0A2020786C765F544558434F4F5244325F31203D20746D707661725F363B0A2020786C765F544558434F4F5244325F32203D20746D707661725F373B0A2020786C765F544558434F4F524435203D20746D707661725F392E78797A3B0A2020786C765F544558434F4F524436203D20746D707661725F383B0A7D0A0A0A23656E6469660A23696664656620465241474D454E540A756E69666F726D206C6F77702076656334205F4C69676874436F6C6F72303B0A756E69666F726D206D656469756D702076656334205F436F6C6F723B0A756E69666F726D2073616D706C65723244205F4D61696E5465783B0A756E69666F726D206D656469756D7020666C6F6174205F4D6574616C6C69633B0A756E69666F726D206D656469756D7020666C6F6174205F476C6F7373696E6573733B0A756E69666F726D2073616D706C65723244205F4C6967687454657874757265303B0A756E69666F726D206869676870206D61743420756E6974795F576F726C64546F4C696768743B0A756E69666F726D2073616D706C65723244205F4C696768745465787475726542303B0A76617279696E67206869676870207665633420786C765F544558434F4F5244303B0A76617279696E67206D656469756D70207665633320786C765F544558434F4F5244313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244323B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244325F313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244325F323B0A76617279696E67206869676870207665633320786C765F544558434F4F5244353B0A766F6964206D61696E2028290A7B0A20206C6F777020666C6F617420617474656E5F313B0A202068696768702076656334206C69676874436F6F72645F323B0A20206C6F7770207665633420746D707661725F333B0A2020746D707661725F33203D2074657874757265324420285F4D61696E5465782C20786C765F544558434F4F5244302E7879293B0A20206D656469756D70207665633320746D707661725F343B0A2020746D707661725F34203D20285F436F6C6F722E78797A202A20746D707661725F332E78797A293B0A20206D656469756D70207665633320746D707661725F353B0A2020746D707661725F35203D206E6F726D616C697A6528786C765F544558434F4F5244325F322E78797A293B0A20206869676870207665633420746D707661725F363B0A2020746D707661725F362E77203D20312E303B0A2020746D707661725F362E78797A203D20786C765F544558434F4F5244353B0A20206C69676874436F6F72645F32203D2028756E6974795F576F726C64546F4C69676874202A20746D707661725F36293B0A20206C6F7770207665633420746D707661725F373B0A20206869676870207665633220505F383B0A2020505F38203D2028286C69676874436F6F72645F322E7879202F206C69676874436F6F72645F322E7729202B20302E35293B0A2020746D707661725F37203D2074657874757265324420285F4C6967687454657874757265302C20505F38293B0A2020686967687020666C6F617420746D707661725F393B0A2020746D707661725F39203D20646F7420286C69676874436F6F72645F322E78797A2C206C69676874436F6F72645F322E78797A293B0A20206C6F7770207665633420746D707661725F31303B0A2020746D707661725F3130203D2074657874757265324420285F4C696768745465787475726542302C207665633228746D707661725F3929293B0A2020686967687020666C6F617420746D707661725F31313B0A2020746D707661725F3131203D202828666C6F6174280A20202020286C69676874436F6F72645F322E7A203E20302E30290A202029202A20746D707661725F372E7729202A20746D707661725F31302E77293B0A2020617474656E5F31203D20746D707661725F31313B0A20206D656469756D70207665633320746D707661725F31323B0A2020746D707661725F31322E78203D20786C765F544558434F4F5244322E773B0A2020746D707661725F31322E79203D20786C765F544558434F4F5244325F312E773B0A2020746D707661725F31322E7A203D20786C765F544558434F4F5244325F322E773B0A20206D656469756D7020666C6F617420617474656E5F31333B0A2020617474656E5F3133203D20617474656E5F313B0A20206D656469756D70207665633320746D707661725F31343B0A2020746D707661725F3134203D205F4C69676874436F6C6F72302E78797A3B0A20206D656469756D70207665633320746D707661725F31353B0A2020746D707661725F3135203D206E6F726D616C697A6528746D707661725F3132293B0A2020746D707661725F3134203D2028746D707661725F3134202A20617474656E5F3133293B0A20206D656469756D7020666C6F61742073706563756C61725465726D5F31363B0A20206D656469756D70207665633320746D707661725F31373B0A20206D656469756D70207665633320696E5665635F31383B0A2020696E5665635F3138203D2028746D707661725F3135202D206E6F726D616C697A6528786C765F544558434F4F52443129293B0A2020746D707661725F3137203D2028696E5665635F3138202A20696E766572736573717274286D61782028302E3030312C200A20202020646F742028696E5665635F31382C20696E5665635F3138290A20202929293B0A20206D656469756D7020666C6F617420746D707661725F31393B0A2020746D707661725F3139203D20636C616D702028646F742028746D707661725F352C20746D707661725F3137292C20302E302C20312E30293B0A20206D656469756D7020666C6F617420746D707661725F32303B0A2020746D707661725F3230203D2028312E30202D205F476C6F7373696E657373293B0A20206D656469756D7020666C6F617420746D707661725F32313B0A2020746D707661725F3231203D2028746D707661725F3230202A20746D707661725F3230293B0A202073706563756C61725465726D5F3136203D202828746D707661725F3231202F20280A20202020286D61782028302E33322C20636C616D702028646F742028746D707661725F31352C20746D707661725F3137292C20302E302C20312E302929202A2028312E35202B20746D707661725F323129290A2020202A200A20202020282828746D707661725F3139202A20746D707661725F313929202A202828746D707661725F3231202A20746D707661725F323129202D20312E302929202B20312E3030303031290A20202929202D20302E30303031293B0A20206D656469756D7020666C6F617420746D707661725F32323B0A2020746D707661725F3232203D20636C616D70202873706563756C61725465726D5F31362C20302E302C203130302E30293B0A202073706563756C61725465726D5F3136203D20746D707661725F32323B0A20206D656469756D70207665633420746D707661725F32333B0A2020746D707661725F32332E77203D20312E303B0A2020746D707661725F32332E78797A203D202828280A2020202028746D707661725F34202A2028302E37373930383337202D20285F4D6574616C6C6963202A20302E373739303833372929290A2020202B200A2020202028746D707661725F3232202A206D697820287665633328302E323230393136332C20302E323230393136332C20302E32323039313633292C20746D707661725F342C2076656333285F4D6574616C6C69632929290A202029202A20746D707661725F313429202A20636C616D702028646F742028746D707661725F352C20746D707661725F3135292C20302E302C20312E3029293B0A20206D656469756D70207665633420786C61745F7661726F75747075745F32343B0A2020786C61745F7661726F75747075745F32342E78797A203D20746D707661725F32332E78797A3B0A2020786C61745F7661726F75747075745F32342E77203D20312E303B0A2020676C5F46726167446174615B305D203D20786C61745F7661726F75747075745F32343B0A7D0A0A0A23656E6469660A00001B000000000000000100000000000000000000000000000000000000324F040C0500000000000000000000000000000000000000020000000400000053504F540D000000534841444F57535F44455054480000000000000000000000000000000100000000000000000000000000000000000000324F040C0500000000000000000000000000000000000000020000000B000000444952454354494F4E414C000E000000534841444F57535F53435245454E00000000000000000000000000000100000000000000000000000000000000000000324F040C050000003F0000000300000001000000000000000200000005000000504F494E540000000C000000534841444F57535F43554245D41900002376657273696F6E203130300A0A236966646566205645525445580A6174747269627574652076656334205F676C65735665727465783B0A6174747269627574652076656333205F676C65734E6F726D616C3B0A6174747269627574652076656334205F676C65734D756C7469546578436F6F7264303B0A6174747269627574652076656334205F676C65734D756C7469546578436F6F7264313B0A756E69666F726D2068696768702076656333205F576F726C64537061636543616D657261506F733B0A756E69666F726D2068696768702076656334205F576F726C6453706163654C69676874506F73303B0A756E69666F726D206869676870206D61743420756E6974795F4F626A656374546F576F726C643B0A756E69666F726D206869676870206D61743420756E6974795F576F726C64546F4F626A6563743B0A756E69666F726D206869676870206D61743420756E6974795F4D617472697856503B0A756E69666F726D2068696768702076656334205F4D61696E5465785F53543B0A756E69666F726D2068696768702076656334205F44657461696C416C6265646F4D61705F53543B0A756E69666F726D206D656469756D7020666C6F6174205F55565365633B0A76617279696E67206869676870207665633420786C765F544558434F4F5244303B0A76617279696E67206D656469756D70207665633320786C765F544558434F4F5244313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244323B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244325F313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244325F323B0A76617279696E67206869676870207665633320786C765F544558434F4F5244353B0A76617279696E67206869676870207665633220786C765F544558434F4F5244363B0A766F6964206D61696E2028290A7B0A20206D656469756D70207665633320746D707661725F313B0A2020746D707661725F31203D205F676C65734E6F726D616C3B0A20206869676870207665633220746D707661725F323B0A2020746D707661725F32203D205F676C65734D756C7469546578436F6F7264302E78793B0A20206869676870207665633220746D707661725F333B0A2020746D707661725F33203D205F676C65734D756C7469546578436F6F7264312E78793B0A202068696768702076656333206C696768744469725F343B0A20206D656469756D70207665633420746D707661725F353B0A20206D656469756D70207665633420746D707661725F363B0A20206D656469756D70207665633420746D707661725F373B0A20206869676870207665633220746D707661725F383B0A20206869676870207665633420746D707661725F393B0A2020746D707661725F39203D2028756E6974795F4F626A656374546F576F726C64202A205F676C6573566572746578293B0A20206869676870207665633420746D707661725F31303B0A20206869676870207665633420746D707661725F31313B0A2020746D707661725F31312E77203D20312E303B0A2020746D707661725F31312E78797A203D205F676C65735665727465782E78797A3B0A2020746D707661725F3130203D2028756E6974795F4D61747269785650202A2028756E6974795F4F626A656374546F576F726C64202A20746D707661725F313129293B0A20206869676870207665633420746578636F6F72645F31323B0A2020746578636F6F72645F31322E7879203D2028285F676C65734D756C7469546578436F6F7264302E7879202A205F4D61696E5465785F53542E787929202B205F4D61696E5465785F53542E7A77293B0A20206869676870207665633220746D707661725F31333B0A202069662028285F5556536563203D3D20302E302929207B0A20202020746D707661725F3133203D20746D707661725F323B0A20207D20656C7365207B0A20202020746D707661725F3133203D20746D707661725F333B0A20207D3B0A2020746578636F6F72645F31322E7A77203D202828746D707661725F3133202A205F44657461696C416C6265646F4D61705F53542E787929202B205F44657461696C416C6265646F4D61705F53542E7A77293B0A20206D656469756D70207665633320746D707661725F31343B0A202068696768702076656333206E5F31353B0A20206E5F3135203D2028746D707661725F392E78797A202D205F576F726C64537061636543616D657261506F73293B0A2020746D707661725F3134203D206E5F31353B0A202068696768702076656333206E6F726D5F31363B0A20206E6F726D5F3136203D20746D707661725F313B0A20206869676870206D61743320746D707661725F31373B0A2020746D707661725F31375B305D203D20756E6974795F576F726C64546F4F626A6563745B305D2E78797A3B0A2020746D707661725F31375B315D203D20756E6974795F576F726C64546F4F626A6563745B315D2E78797A3B0A2020746D707661725F31375B325D203D20756E6974795F576F726C64546F4F626A6563745B325D2E78797A3B0A20206869676870207665633320746D707661725F31383B0A2020746D707661725F3138203D206E6F726D616C697A6528286E6F726D5F3136202A20746D707661725F313729293B0A2020746D707661725F352E78797A203D207665633328302E302C20302E302C20302E30293B0A2020746D707661725F362E78797A203D207665633328302E302C20302E302C20302E30293B0A2020746D707661725F372E78797A203D20746D707661725F31383B0A20206869676870207665633320746D707661725F31393B0A2020746D707661725F3139203D20285F576F726C6453706163654C69676874506F73302E78797A202D2028746D707661725F392E78797A202A205F576F726C6453706163654C69676874506F73302E7729293B0A20206D656469756D70207665633320746D707661725F32303B0A2020746D707661725F3230203D20746D707661725F31393B0A20206C696768744469725F34203D20746D707661725F32303B0A2020746D707661725F352E77203D206C696768744469725F342E783B0A2020746D707661725F362E77203D206C696768744469725F342E793B0A2020746D707661725F372E77203D206C696768744469725F342E7A3B0A2020676C5F506F736974696F6E203D20746D707661725F31303B0A2020786C765F544558434F4F524430203D20746578636F6F72645F31323B0A2020786C765F544558434F4F524431203D20746D707661725F31343B0A2020786C765F544558434F4F524432203D20746D707661725F353B0A2020786C765F544558434F4F5244325F31203D20746D707661725F363B0A2020786C765F544558434F4F5244325F32203D20746D707661725F373B0A2020786C765F544558434F4F524435203D20746D707661725F392E78797A3B0A2020786C765F544558434F4F524436203D20746D707661725F383B0A7D0A0A0A23656E6469660A23696664656620465241474D454E540A756E69666F726D2068696768702076656333205F576F726C64537061636543616D657261506F733B0A756E69666F726D2068696768702076656334205F4C69676874506F736974696F6E52616E67653B0A756E69666F726D206D656469756D702076656334205F4C69676874536861646F77446174613B0A756E69666F726D206869676870207665633420756E6974795F536861646F774661646543656E746572416E64547970653B0A756E69666F726D206869676870206D61743420756E6974795F4D6174726978563B0A756E69666F726D206C6F77702076656334205F4C69676874436F6C6F72303B0A756E69666F726D2068696768702073616D706C657243756265205F536861646F774D6170546578747572653B0A756E69666F726D206D656469756D702076656334205F436F6C6F723B0A756E69666F726D2073616D706C65723244205F4D61696E5465783B0A756E69666F726D206D656469756D7020666C6F6174205F4D6574616C6C69633B0A756E69666F726D206D656469756D7020666C6F6174205F476C6F7373696E6573733B0A756E69666F726D2073616D706C65723244205F4C6967687454657874757265303B0A756E69666F726D206869676870206D61743420756E6974795F576F726C64546F4C696768743B0A76617279696E67206869676870207665633420786C765F544558434F4F5244303B0A76617279696E67206D656469756D70207665633320786C765F544558434F4F5244313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244323B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244325F313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244325F323B0A76617279696E67206869676870207665633320786C765F544558434F4F5244353B0A766F6964206D61696E2028290A7B0A20206C6F777020666C6F617420736861646F775F313B0A20206C6F7770207665633420746D707661725F323B0A2020746D707661725F32203D2074657874757265324420285F4D61696E5465782C20786C765F544558434F4F5244302E7879293B0A20206D656469756D70207665633320746D707661725F333B0A2020746D707661725F33203D20285F436F6C6F722E78797A202A20746D707661725F322E78797A293B0A20206D656469756D70207665633320746D707661725F343B0A20206D656469756D70207665633320746D707661725F353B0A2020746D707661725F35203D206D697820287665633328302E323230393136332C20302E323230393136332C20302E32323039313633292C20746D707661725F332C2076656333285F4D6574616C6C696329293B0A2020746D707661725F34203D2028746D707661725F33202A2028302E37373930383337202D20285F4D6574616C6C6963202A20302E373739303833372929293B0A20206D656469756D70207665633320746D707661725F363B0A2020746D707661725F36203D206E6F726D616C697A6528786C765F544558434F4F5244325F322E78797A293B0A20206D656469756D70207665633320746D707661725F373B0A2020746D707661725F37203D206E6F726D616C697A6528786C765F544558434F4F524431293B0A20206869676870207665633420746D707661725F383B0A2020746D707661725F382E77203D20312E303B0A2020746D707661725F382E78797A203D20786C765F544558434F4F5244353B0A20206869676870207665633320746D707661725F393B0A2020746D707661725F39203D2028756E6974795F576F726C64546F4C69676874202A20746D707661725F38292E78797A3B0A20206869676870207665633420765F31303B0A2020765F31302E78203D20756E6974795F4D6174726978565B305D2E7A3B0A2020765F31302E79203D20756E6974795F4D6174726978565B315D2E7A3B0A2020765F31302E7A203D20756E6974795F4D6174726978565B325D2E7A3B0A2020765F31302E77203D20756E6974795F4D6174726978565B335D2E7A3B0A20206869676870207665633320746D707661725F31313B0A2020746D707661725F3131203D2028786C765F544558434F4F524435202D20756E6974795F536861646F774661646543656E746572416E64547970652E78797A293B0A20206D656469756D7020666C6F617420746D707661725F31323B0A2020686967687020666C6F617420746D707661725F31333B0A2020746D707661725F3133203D20636C616D70202828280A202020206D69782028646F742028285F576F726C64537061636543616D657261506F73202D20786C765F544558434F4F524435292C20765F31302E78797A292C207371727428646F742028746D707661725F31312C20746D707661725F313129292C20756E6974795F536861646F774661646543656E746572416E64547970652E77290A2020202A205F4C69676874536861646F77446174612E7A29202B205F4C69676874536861646F77446174612E77292C20302E302C20312E30293B0A2020746D707661725F3132203D20746D707661725F31333B0A202068696768702076656333207665635F31343B0A20207665635F3134203D2028786C765F544558434F4F524435202D205F4C69676874506F736974696F6E52616E67652E78797A293B0A2020686967687020666C6F6174206D79646973745F31353B0A20206D79646973745F3135203D20282873717274280A20202020646F7420287665635F31342C207665635F3134290A202029202A205F4C69676874506F736974696F6E52616E67652E7729202A20302E3937293B0A2020686967687020666C6F617420746D707661725F31363B0A2020746D707661725F3136203D20646F742028746578747572654375626520285F536861646F774D6170546578747572652C207665635F3134292C207665633428312E302C20302E3030333932313536392C20312E3533373837652D30352C20362E303330383633652D303829293B0A20206D656469756D7020666C6F617420746D707661725F31373B0A20206966202828746D707661725F3136203C206D79646973745F31352929207B0A20202020746D707661725F3137203D205F4C69676874536861646F77446174612E783B0A20207D20656C7365207B0A20202020746D707661725F3137203D20312E303B0A20207D3B0A20206D656469756D7020666C6F617420746D707661725F31383B0A2020746D707661725F3138203D20636C616D70202828746D707661725F3137202B20746D707661725F3132292C20302E302C20312E30293B0A2020736861646F775F31203D20746D707661725F31383B0A2020686967687020666C6F617420746D707661725F31393B0A2020746D707661725F3139203D20646F742028746D707661725F392C20746D707661725F39293B0A20206C6F777020666C6F617420746D707661725F32303B0A2020746D707661725F3230203D202874657874757265324420285F4C6967687454657874757265302C207665633228746D707661725F313929292E77202A20736861646F775F31293B0A20206D656469756D70207665633320746D707661725F32313B0A2020746D707661725F32312E78203D20786C765F544558434F4F5244322E773B0A2020746D707661725F32312E79203D20786C765F544558434F4F5244325F312E773B0A2020746D707661725F32312E7A203D20786C765F544558434F4F5244325F322E773B0A20206D656469756D7020666C6F617420617474656E5F32323B0A2020617474656E5F3232203D20746D707661725F32303B0A20206D656469756D70207665633320746D707661725F32333B0A2020746D707661725F3233203D205F4C69676874436F6C6F72302E78797A3B0A20206D656469756D70207665633320746D707661725F32343B0A2020746D707661725F3234203D206E6F726D616C697A6528746D707661725F3231293B0A2020746D707661725F3233203D2028746D707661725F3233202A20617474656E5F3232293B0A20206D656469756D7020666C6F61742073706563756C61725465726D5F32353B0A20206D656469756D70207665633320746D707661725F32363B0A20206D656469756D70207665633320696E5665635F32373B0A2020696E5665635F3237203D2028746D707661725F3234202D20746D707661725F37293B0A2020746D707661725F3236203D2028696E5665635F3237202A20696E766572736573717274286D61782028302E3030312C200A20202020646F742028696E5665635F32372C20696E5665635F3237290A20202929293B0A20206D656469756D7020666C6F617420746D707661725F32383B0A2020746D707661725F3238203D20636C616D702028646F742028746D707661725F362C20746D707661725F3236292C20302E302C20312E30293B0A20206D656469756D7020666C6F617420746D707661725F32393B0A2020746D707661725F3239203D2028312E30202D205F476C6F7373696E657373293B0A20206D656469756D7020666C6F617420746D707661725F33303B0A2020746D707661725F3330203D2028746D707661725F3239202A20746D707661725F3239293B0A202073706563756C61725465726D5F3235203D202828746D707661725F3330202F20280A20202020286D61782028302E33322C20636C616D702028646F742028746D707661725F32342C20746D707661725F3236292C20302E302C20312E302929202A2028312E35202B20746D707661725F333029290A2020202A200A20202020282828746D707661725F3238202A20746D707661725F323829202A202828746D707661725F3330202A20746D707661725F333029202D20312E302929202B20312E3030303031290A20202929202D20302E30303031293B0A20206D656469756D7020666C6F617420746D707661725F33313B0A2020746D707661725F3331203D20636C616D70202873706563756C61725465726D5F32352C20302E302C203130302E30293B0A202073706563756C61725465726D5F3235203D20746D707661725F33313B0A20206D656469756D70207665633420746D707661725F33323B0A2020746D707661725F33322E77203D20312E303B0A2020746D707661725F33322E78797A203D20282828746D707661725F34202B200A2020202028746D707661725F3331202A20746D707661725F35290A202029202A20746D707661725F323329202A20636C616D702028646F742028746D707661725F362C20746D707661725F3234292C20302E302C20312E3029293B0A20206D656469756D70207665633420786C61745F7661726F75747075745F33333B0A2020786C61745F7661726F75747075745F33332E78797A203D20746D707661725F33322E78797A3B0A2020786C61745F7661726F75747075745F33332E77203D20312E303B0A2020676C5F46726167446174615B305D203D20786C61745F7661726F75747075745F33333B0A7D0A0A0A23656E6469660A1B000000000000000100000000000000000000000000000000000000324F040C050000002A000000050000000000000000000000030000000B000000444952454354494F4E414C000E000000534841444F57535F53435245454E00000E0000005645525445584C494748545F4F4E0000B82300002376657273696F6E203130300A0A236966646566205645525445580A6174747269627574652076656334205F676C65735665727465783B0A6174747269627574652076656333205F676C65734E6F726D616C3B0A6174747269627574652076656334205F676C65734D756C7469546578436F6F7264303B0A6174747269627574652076656334205F676C65734D756C7469546578436F6F7264313B0A756E69666F726D2068696768702076656333205F576F726C64537061636543616D657261506F733B0A756E69666F726D206869676870207665633420756E6974795F344C69676874506F7358303B0A756E69666F726D206869676870207665633420756E6974795F344C69676874506F7359303B0A756E69666F726D206869676870207665633420756E6974795F344C69676874506F735A303B0A756E69666F726D206D656469756D70207665633420756E6974795F344C69676874417474656E303B0A756E69666F726D206D656469756D70207665633420756E6974795F4C69676874436F6C6F725B385D3B0A756E69666F726D206D656469756D70207665633420756E6974795F534841723B0A756E69666F726D206D656469756D70207665633420756E6974795F534841673B0A756E69666F726D206D656469756D70207665633420756E6974795F534841623B0A756E69666F726D206D656469756D70207665633420756E6974795F534842723B0A756E69666F726D206D656469756D70207665633420756E6974795F534842673B0A756E69666F726D206D656469756D70207665633420756E6974795F534842623B0A756E69666F726D206D656469756D70207665633420756E6974795F5348433B0A756E69666F726D206869676870206D61743420756E6974795F576F726C64546F536861646F775B345D3B0A756E69666F726D206869676870206D61743420756E6974795F4F626A656374546F576F726C643B0A756E69666F726D206869676870206D61743420756E6974795F576F726C64546F4F626A6563743B0A756E69666F726D206869676870206D61743420756E6974795F4D617472697856503B0A756E69666F726D2068696768702076656334205F4D61696E5465785F53543B0A756E69666F726D2068696768702076656334205F44657461696C416C6265646F4D61705F53543B0A756E69666F726D206D656469756D7020666C6F6174205F4D6574616C6C69633B0A756E69666F726D206D656469756D7020666C6F6174205F476C6F7373696E6573733B0A756E69666F726D206D656469756D7020666C6F6174205F55565365633B0A76617279696E67206869676870207665633420786C765F544558434F4F5244303B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244323B0A76617279696E67206869676870207665633420786C765F544558434F4F5244333B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244343B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244353B0A766F6964206D61696E2028290A7B0A20206D656469756D70207665633320746D707661725F313B0A2020746D707661725F31203D205F676C65734E6F726D616C3B0A20206869676870207665633220746D707661725F323B0A2020746D707661725F32203D205F676C65734D756C7469546578436F6F7264302E78793B0A20206869676870207665633220746D707661725F333B0A2020746D707661725F33203D205F676C65734D756C7469546578436F6F7264312E78793B0A20206D656469756D702076656333206E6F726D616C576F726C645F343B0A20206D656469756D702076656333206579655665635F353B0A20206D656469756D70207665633420746D707661725F363B0A20206D656469756D70207665633420746D707661725F373B0A20206D656469756D70207665633420746D707661725F383B0A20206869676870207665633420746D707661725F393B0A2020746D707661725F39203D2028756E6974795F4F626A656374546F576F726C64202A205F676C6573566572746578293B0A20206869676870207665633420746D707661725F31303B0A20206869676870207665633420746D707661725F31313B0A2020746D707661725F31312E77203D20312E303B0A2020746D707661725F31312E78797A203D205F676C65735665727465782E78797A3B0A2020746D707661725F3130203D2028756E6974795F4D61747269785650202A2028756E6974795F4F626A656374546F576F726C64202A20746D707661725F313129293B0A20206869676870207665633420746578636F6F72645F31323B0A2020746578636F6F72645F31322E7879203D2028285F676C65734D756C7469546578436F6F7264302E7879202A205F4D61696E5465785F53542E787929202B205F4D61696E5465785F53542E7A77293B0A20206869676870207665633220746D707661725F31333B0A202069662028285F5556536563203D3D20302E302929207B0A20202020746D707661725F3133203D20746D707661725F323B0A20207D20656C7365207B0A20202020746D707661725F3133203D20746D707661725F333B0A20207D3B0A2020746578636F6F72645F31322E7A77203D202828746D707661725F3133202A205F44657461696C416C6265646F4D61705F53542E787929202B205F44657461696C416C6265646F4D61705F53542E7A77293B0A20206869676870207665633320746D707661725F31343B0A2020746D707661725F3134203D206E6F726D616C697A652828746D707661725F392E78797A202D205F576F726C64537061636543616D657261506F7329293B0A20206579655665635F35203D20746D707661725F31343B0A202068696768702076656333206E6F726D5F31353B0A20206E6F726D5F3135203D20746D707661725F313B0A20206869676870206D61743320746D707661725F31363B0A2020746D707661725F31365B305D203D20756E6974795F576F726C64546F4F626A6563745B305D2E78797A3B0A2020746D707661725F31365B315D203D20756E6974795F576F726C64546F4F626A6563745B315D2E78797A3B0A2020746D707661725F31365B325D203D20756E6974795F576F726C64546F4F626A6563745B325D2E78797A3B0A20206869676870207665633320746D707661725F31373B0A2020746D707661725F3137203D206E6F726D616C697A6528286E6F726D5F3135202A20746D707661725F313629293B0A20206E6F726D616C576F726C645F34203D20746D707661725F31373B0A2020746D707661725F382E78797A203D206E6F726D616C576F726C645F343B0A2020746D707661725F362E78797A203D206579655665635F353B0A20206D656469756D70207665633420616D6269656E744F724C696768746D617055565F31383B0A2020616D6269656E744F724C696768746D617055565F31382E77203D20302E303B0A202068696768702076656333206C69676874436F6C6F72305F31393B0A20206C69676874436F6C6F72305F3139203D20756E6974795F4C69676874436F6C6F725B305D2E78797A3B0A202068696768702076656333206C69676874436F6C6F72315F32303B0A20206C69676874436F6C6F72315F3230203D20756E6974795F4C69676874436F6C6F725B315D2E78797A3B0A202068696768702076656333206C69676874436F6C6F72325F32313B0A20206C69676874436F6C6F72325F3231203D20756E6974795F4C69676874436F6C6F725B325D2E78797A3B0A202068696768702076656333206C69676874436F6C6F72335F32323B0A20206C69676874436F6C6F72335F3232203D20756E6974795F4C69676874436F6C6F725B335D2E78797A3B0A202068696768702076656334206C69676874417474656E53715F32333B0A20206C69676874417474656E53715F3233203D20756E6974795F344C69676874417474656E303B0A202068696768702076656333206E6F726D616C5F32343B0A20206E6F726D616C5F3234203D206E6F726D616C576F726C645F343B0A20206869676870207665633320636F6C5F32353B0A202068696768702076656334206E646F746C5F32363B0A202068696768702076656334206C656E67746853715F32373B0A20206869676870207665633420746D707661725F32383B0A2020746D707661725F3238203D2028756E6974795F344C69676874506F735830202D20746D707661725F392E78293B0A20206869676870207665633420746D707661725F32393B0A2020746D707661725F3239203D2028756E6974795F344C69676874506F735930202D20746D707661725F392E79293B0A20206869676870207665633420746D707661725F33303B0A2020746D707661725F3330203D2028756E6974795F344C69676874506F735A30202D20746D707661725F392E7A293B0A20206C656E67746853715F3237203D2028746D707661725F3238202A20746D707661725F3238293B0A20206C656E67746853715F3237203D20286C656E67746853715F3237202B2028746D707661725F3239202A20746D707661725F323929293B0A20206C656E67746853715F3237203D20286C656E67746853715F3237202B2028746D707661725F3330202A20746D707661725F333029293B0A20206869676870207665633420746D707661725F33313B0A2020746D707661725F3331203D206D617820286C656E67746853715F32372C20766563342831652D30362C2031652D30362C2031652D30362C2031652D303629293B0A20206C656E67746853715F3237203D20746D707661725F33313B0A20206E646F746C5F3236203D2028746D707661725F3238202A206E6F726D616C5F32342E78293B0A20206E646F746C5F3236203D20286E646F746C5F3236202B2028746D707661725F3239202A206E6F726D616C5F32342E7929293B0A20206E646F746C5F3236203D20286E646F746C5F3236202B2028746D707661725F3330202A206E6F726D616C5F32342E7A29293B0A20206869676870207665633420746D707661725F33323B0A2020746D707661725F3332203D206D617820287665633428302E302C20302E302C20302E302C20302E30292C20286E646F746C5F3236202A20696E76657273657371727428746D707661725F33312929293B0A20206E646F746C5F3236203D20746D707661725F33323B0A20206869676870207665633420746D707661725F33333B0A2020746D707661725F3333203D2028746D707661725F3332202A2028312E302F2828312E30202B200A2020202028746D707661725F3331202A206C69676874417474656E53715F3233290A2020292929293B0A2020636F6C5F3235203D20286C69676874436F6C6F72305F3139202A20746D707661725F33332E78293B0A2020636F6C5F3235203D2028636F6C5F3235202B20286C69676874436F6C6F72315F3230202A20746D707661725F33332E7929293B0A2020636F6C5F3235203D2028636F6C5F3235202B20286C69676874436F6C6F72325F3231202A20746D707661725F33332E7A29293B0A2020636F6C5F3235203D2028636F6C5F3235202B20286C69676874436F6C6F72335F3232202A20746D707661725F33332E7729293B0A2020616D6269656E744F724C696768746D617055565F31382E78797A203D20636F6C5F32353B0A20206D656469756D70207665633420746D707661725F33343B0A2020746D707661725F33342E77203D20312E303B0A2020746D707661725F33342E78797A203D206E6F726D616C576F726C645F343B0A20206D656469756D702076656333207265735F33353B0A20206D656469756D70207665633320785F33363B0A2020785F33362E78203D20646F742028756E6974795F534841722C20746D707661725F3334293B0A2020785F33362E79203D20646F742028756E6974795F534841672C20746D707661725F3334293B0A2020785F33362E7A203D20646F742028756E6974795F534841622C20746D707661725F3334293B0A20206D656469756D7020766563332078315F33373B0A20206D656469756D70207665633420746D707661725F33383B0A2020746D707661725F3338203D20286E6F726D616C576F726C645F342E78797A7A202A206E6F726D616C576F726C645F342E797A7A78293B0A202078315F33372E78203D20646F742028756E6974795F534842722C20746D707661725F3338293B0A202078315F33372E79203D20646F742028756E6974795F534842672C20746D707661725F3338293B0A202078315F33372E7A203D20646F742028756E6974795F534842622C20746D707661725F3338293B0A20207265735F3335203D2028785F3336202B202878315F3337202B2028756E6974795F5348432E78797A202A200A2020202028286E6F726D616C576F726C645F342E78202A206E6F726D616C576F726C645F342E7829202D20286E6F726D616C576F726C645F342E79202A206E6F726D616C576F726C645F342E7929290A20202929293B0A20206D656469756D70207665633320746D707661725F33393B0A2020746D707661725F3339203D206D617820282828312E303535202A200A20202020706F7720286D617820287265735F33352C207665633328302E302C20302E302C20302E3029292C207665633328302E343136363636372C20302E343136363636372C20302E3431363636363729290A202029202D20302E303535292C207665633328302E302C20302E302C20302E3029293B0A20207265735F3335203D20746D707661725F33393B0A2020616D6269656E744F724C696768746D617055565F31382E78797A203D2028616D6269656E744F724C696768746D617055565F31382E78797A202B206D617820287665633328302E302C20302E302C20302E30292C20746D707661725F333929293B0A2020746D707661725F372E797A77203D20286579655665635F35202D2028322E30202A20280A20202020646F7420286E6F726D616C576F726C645F342C206579655665635F35290A2020202A206E6F726D616C576F726C645F342929293B0A20206D656469756D7020666C6F617420785F34303B0A2020785F3430203D2028312E30202D20636C616D702028646F7420286E6F726D616C576F726C645F342C200A202020202D286579655665635F35290A2020292C20302E302C20312E3029293B0A2020746D707661725F382E77203D202828785F3430202A20785F343029202A2028785F3430202A20785F343029293B0A2020746D707661725F362E77203D20636C616D702028285F476C6F7373696E657373202B2028312E30202D200A2020202028302E37373930383337202D20285F4D6574616C6C6963202A20302E3737393038333729290A202029292C20302E302C20312E30293B0A2020676C5F506F736974696F6E203D20746D707661725F31303B0A2020786C765F544558434F4F524430203D20746578636F6F72645F31323B0A2020786C765F544558434F4F524431203D20746D707661725F363B0A2020786C765F544558434F4F524432203D20616D6269656E744F724C696768746D617055565F31383B0A2020786C765F544558434F4F524433203D2028756E6974795F576F726C64546F536861646F775B305D202A2028756E6974795F4F626A656374546F576F726C64202A205F676C657356657274657829293B0A2020786C765F544558434F4F524434203D20746D707661725F373B0A2020786C765F544558434F4F524435203D20746D707661725F383B0A7D0A0A0A23656E6469660A23696664656620465241474D454E540A23657874656E73696F6E20474C5F4558545F7368616465725F746578747572655F6C6F64203A20656E61626C650A6C6F7770207665633420696D706C5F6C6F775F74657874757265437562654C6F64455854286C6F77702073616D706C6572437562652073616D706C65722C206869676870207665633320636F6F72642C206D656469756D7020666C6F6174206C6F64290A7B0A23696620646566696E656428474C5F4558545F7368616465725F746578747572655F6C6F64290A0972657475726E2074657874757265437562654C6F644558542873616D706C65722C20636F6F72642C206C6F64293B0A23656C73650A0972657475726E2074657874757265437562652873616D706C65722C20636F6F72642C206C6F64293B0A23656E6469660A7D0A0A756E69666F726D206D656469756D702076656334205F576F726C6453706163654C69676874506F73303B0A756E69666F726D206D656469756D702076656334205F4C69676874536861646F77446174613B0A756E69666F726D206C6F77702073616D706C65724375626520756E6974795F5370656343756265303B0A756E69666F726D206D656469756D70207665633420756E6974795F5370656343756265305F4844523B0A756E69666F726D206C6F77702076656334205F4C69676874436F6C6F72303B0A756E69666F726D2073616D706C6572324420756E6974795F4E4878526F7567686E6573733B0A756E69666F726D206D656469756D702076656334205F436F6C6F723B0A756E69666F726D2073616D706C65723244205F4D61696E5465783B0A756E69666F726D206D656469756D7020666C6F6174205F4D6574616C6C69633B0A756E69666F726D206D656469756D7020666C6F6174205F476C6F7373696E6573733B0A756E69666F726D2073616D706C65723244205F4F63636C7573696F6E4D61703B0A756E69666F726D206D656469756D7020666C6F6174205F4F63636C7573696F6E537472656E6774683B0A756E69666F726D2068696768702073616D706C65723244205F536861646F774D6170546578747572653B0A76617279696E67206869676870207665633420786C765F544558434F4F5244303B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244323B0A76617279696E67206869676870207665633420786C765F544558434F4F5244333B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244343B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244353B0A766F6964206D61696E2028290A7B0A20206D656469756D7020666C6F617420726C5F313B0A20206D656469756D7020666C6F6174207265616C74696D65536861646F77417474656E756174696F6E5F323B0A20206C6F7770207665633420746D707661725F333B0A2020746D707661725F33203D2074657874757265324420285F4D61696E5465782C20786C765F544558434F4F5244302E7879293B0A20206D656469756D70207665633320746D707661725F343B0A2020746D707661725F34203D20285F436F6C6F722E78797A202A20746D707661725F332E78797A293B0A20206D656469756D70207665633320746D707661725F353B0A20206D656469756D70207665633320746D707661725F363B0A2020746D707661725F36203D206D697820287665633328302E323230393136332C20302E323230393136332C20302E32323039313633292C20746D707661725F342C2076656333285F4D6574616C6C696329293B0A2020746D707661725F35203D2028746D707661725F34202A2028302E37373930383337202D20285F4D6574616C6C6963202A20302E373739303833372929293B0A20206D656469756D70207665633320746D707661725F373B0A2020746D707661725F37203D205F4C69676874436F6C6F72302E78797A3B0A20206C6F777020666C6F617420746D707661725F383B0A2020686967687020666C6F6174206C69676874536861646F7744617461585F393B0A20206D656469756D7020666C6F617420746D707661725F31303B0A2020746D707661725F3130203D205F4C69676874536861646F77446174612E783B0A20206C69676874536861646F7744617461585F39203D20746D707661725F31303B0A2020686967687020666C6F617420746D707661725F31313B0A2020746D707661725F3131203D206D61782028666C6F6174282874657874757265324420285F536861646F774D6170546578747572652C20786C765F544558434F4F5244332E7879292E78203E20786C765F544558434F4F5244332E7A29292C206C69676874536861646F7744617461585F39293B0A2020746D707661725F38203D20746D707661725F31313B0A20207265616C74696D65536861646F77417474656E756174696F6E5F32203D20746D707661725F383B0A20206D656469756D7020666C6F6174206F63635F31323B0A20206C6F777020666C6F617420746D707661725F31333B0A2020746D707661725F3133203D2074657874757265324420285F4F63636C7573696F6E4D61702C20786C765F544558434F4F5244302E7879292E793B0A20206F63635F3132203D20746D707661725F31333B0A2020726C5F31203D20646F742028786C765F544558434F4F5244342E797A772C205F576F726C6453706163654C69676874506F73302E78797A293B0A20206D656469756D7020666C6F6174206F63636C7573696F6E5F31343B0A20206F63636C7573696F6E5F3134203D202828312E30202D205F4F63636C7573696F6E537472656E67746829202B20286F63635F3132202A205F4F63636C7573696F6E537472656E67746829293B0A20206869676870207665633420746D707661725F31353B0A2020746D707661725F3135203D20756E6974795F5370656343756265305F4844523B0A20206D656469756D7020666C6F617420746D707661725F31363B0A2020746D707661725F3136203D2028312E30202D205F476C6F7373696E657373293B0A20206D656469756D702076656334206864725F31373B0A20206864725F3137203D20746D707661725F31353B0A20206D656469756D70207665633420746D707661725F31383B0A2020746D707661725F31382E78797A203D20786C765F544558434F4F5244342E797A773B0A2020746D707661725F31382E77203D202828746D707661725F3136202A2028312E37202D200A2020202028302E37202A20746D707661725F3136290A20202929202A20362E30293B0A20206C6F7770207665633420746D707661725F31393B0A2020746D707661725F3139203D20696D706C5F6C6F775F74657874757265437562654C6F644558542028756E6974795F5370656343756265302C20786C765F544558434F4F5244342E797A772C20746D707661725F31382E77293B0A20206D656469756D70207665633420746D707661725F32303B0A2020746D707661725F3230203D20746D707661725F31393B0A20206D656469756D70207665633220746D707661725F32313B0A2020746D707661725F32312E78203D202828726C5F31202A20726C5F3129202A2028726C5F31202A20726C5F3129293B0A2020746D707661725F32312E79203D20746D707661725F31363B0A20206C6F7770207665633420746D707661725F32323B0A2020746D707661725F3232203D207465787475726532442028756E6974795F4E4878526F7567686E6573732C20746D707661725F3231293B0A20206D656469756D70207665633420746D707661725F32333B0A2020746D707661725F32332E77203D20312E303B0A2020746D707661725F32332E78797A203D202828280A2020202028786C765F544558434F4F5244322E78797A202A206F63636C7573696F6E5F3134290A2020202A20746D707661725F3529202B20280A202020202828286864725F31372E78202A20280A202020202020286864725F31372E77202A2028746D707661725F32302E77202D20312E3029290A20202020202B20312E302929202A20746D707661725F32302E78797A29202A206F63636C7573696F6E5F3134290A2020202A200A202020206D69782028746D707661725F362C20786C765F544558434F4F5244312E7777772C20786C765F544558434F4F5244352E777777290A20202929202B202828746D707661725F35202B200A202020202828746D707661725F32322E77202A2031362E3029202A20746D707661725F36290A202029202A20280A2020202028746D707661725F37202A207265616C74696D65536861646F77417474656E756174696F6E5F32290A2020202A200A20202020636C616D702028646F742028786C765F544558434F4F5244352E78797A2C205F576F726C6453706163654C69676874506F73302E78797A292C20302E302C20312E30290A20202929293B0A20206D656469756D70207665633420786C61745F7661726F75747075745F32343B0A2020786C61745F7661726F75747075745F32342E78797A203D20746D707661725F32332E78797A3B0A2020786C61745F7661726F75747075745F32342E77203D20312E303B0A2020676C5F46726167446174615B305D203D20786C61745F7661726F75747075745F32343B0A7D0A0A0A23656E6469660A1B000000000000000100000000000000000000000000000000000000324F040C0500000024000000050000000100000000000000020000000400000053504F540D000000534841444F57535F4445505448000000361500002376657273696F6E203130300A0A236966646566205645525445580A6174747269627574652076656334205F676C65735665727465783B0A6174747269627574652076656333205F676C65734E6F726D616C3B0A6174747269627574652076656334205F676C65734D756C7469546578436F6F7264303B0A6174747269627574652076656334205F676C65734D756C7469546578436F6F7264313B0A756E69666F726D2068696768702076656333205F576F726C64537061636543616D657261506F733B0A756E69666F726D2068696768702076656334205F576F726C6453706163654C69676874506F73303B0A756E69666F726D206869676870206D61743420756E6974795F4F626A656374546F576F726C643B0A756E69666F726D206869676870206D61743420756E6974795F576F726C64546F4F626A6563743B0A756E69666F726D206869676870206D61743420756E6974795F4D617472697856503B0A756E69666F726D2068696768702076656334205F4D61696E5465785F53543B0A756E69666F726D2068696768702076656334205F44657461696C416C6265646F4D61705F53543B0A756E69666F726D206D656469756D7020666C6F6174205F55565365633B0A76617279696E67206869676870207665633420786C765F544558434F4F5244303B0A76617279696E67206869676870207665633320786C765F544558434F4F5244313B0A76617279696E67206869676870207665633220786C765F544558434F4F5244323B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244333B0A76617279696E67206D656469756D70207665633320786C765F544558434F4F5244343B0A76617279696E67206D656469756D70207665633320786C765F544558434F4F5244353B0A766F6964206D61696E2028290A7B0A20206D656469756D70207665633320746D707661725F313B0A2020746D707661725F31203D205F676C65734E6F726D616C3B0A20206869676870207665633220746D707661725F323B0A2020746D707661725F32203D205F676C65734D756C7469546578436F6F7264302E78793B0A20206869676870207665633220746D707661725F333B0A2020746D707661725F33203D205F676C65734D756C7469546578436F6F7264312E78793B0A20206D656469756D702076656333206E6F726D616C576F726C645F343B0A20206D656469756D702076656333206579655665635F353B0A20206D656469756D702076656333206C696768744469725F363B0A20206869676870207665633220746D707661725F373B0A20206D656469756D70207665633420746D707661725F383B0A20206869676870207665633420746D707661725F393B0A2020746D707661725F39203D2028756E6974795F4F626A656374546F576F726C64202A205F676C6573566572746578293B0A20206869676870207665633420746D707661725F31303B0A20206869676870207665633420746D707661725F31313B0A2020746D707661725F31312E77203D20312E303B0A2020746D707661725F31312E78797A203D205F676C65735665727465782E78797A3B0A2020746D707661725F3130203D2028756E6974795F4D61747269785650202A2028756E6974795F4F626A656374546F576F726C64202A20746D707661725F313129293B0A20206869676870207665633420746578636F6F72645F31323B0A2020746578636F6F72645F31322E7879203D2028285F676C65734D756C7469546578436F6F7264302E7879202A205F4D61696E5465785F53542E787929202B205F4D61696E5465785F53542E7A77293B0A20206869676870207665633220746D707661725F31333B0A202069662028285F5556536563203D3D20302E302929207B0A20202020746D707661725F3133203D20746D707661725F323B0A20207D20656C7365207B0A20202020746D707661725F3133203D20746D707661725F333B0A20207D3B0A2020746578636F6F72645F31322E7A77203D202828746D707661725F3133202A205F44657461696C416C6265646F4D61705F53542E787929202B205F44657461696C416C6265646F4D61705F53542E7A77293B0A20206869676870207665633320746D707661725F31343B0A2020746D707661725F3134203D20285F576F726C6453706163654C69676874506F73302E78797A202D2028746D707661725F392E78797A202A205F576F726C6453706163654C69676874506F73302E7729293B0A20206C696768744469725F36203D20746D707661725F31343B0A20206D656469756D70207665633320746D707661725F31353B0A202068696768702076656333206E5F31363B0A20206E5F3136203D206C696768744469725F363B0A20206869676870207665633320746D707661725F31373B0A2020746D707661725F3137203D206E6F726D616C697A65286E5F3136293B0A2020746D707661725F3135203D20746D707661725F31373B0A20206C696768744469725F36203D20746D707661725F31353B0A20206869676870207665633320746D707661725F31383B0A2020746D707661725F3138203D206E6F726D616C697A652828746D707661725F392E78797A202D205F576F726C64537061636543616D657261506F7329293B0A20206579655665635F35203D20746D707661725F31383B0A202068696768702076656333206E6F726D5F31393B0A20206E6F726D5F3139203D20746D707661725F313B0A20206869676870206D61743320746D707661725F32303B0A2020746D707661725F32305B305D203D20756E6974795F576F726C64546F4F626A6563745B305D2E78797A3B0A2020746D707661725F32305B315D203D20756E6974795F576F726C64546F4F626A6563745B315D2E78797A3B0A2020746D707661725F32305B325D203D20756E6974795F576F726C64546F4F626A6563745B325D2E78797A3B0A20206869676870207665633320746D707661725F32313B0A2020746D707661725F3231203D206E6F726D616C697A6528286E6F726D5F3139202A20746D707661725F323029293B0A20206E6F726D616C576F726C645F34203D20746D707661725F32313B0A2020746D707661725F382E797A77203D20286579655665635F35202D2028322E30202A20280A20202020646F7420286E6F726D616C576F726C645F342C206579655665635F35290A2020202A206E6F726D616C576F726C645F342929293B0A2020676C5F506F736974696F6E203D20746D707661725F31303B0A2020786C765F544558434F4F524430203D20746578636F6F72645F31323B0A2020786C765F544558434F4F524431203D20746D707661725F392E78797A3B0A2020786C765F544558434F4F524432203D20746D707661725F373B0A2020786C765F544558434F4F524433203D20746D707661725F383B0A2020786C765F544558434F4F524434203D20746D707661725F31353B0A2020786C765F544558434F4F524435203D206E6F726D616C576F726C645F343B0A7D0A0A0A23656E6469660A23696664656620465241474D454E540A756E69666F726D206869676870206D61743420756E6974795F576F726C64546F536861646F775B345D3B0A756E69666F726D206D656469756D702076656334205F4C69676874536861646F77446174613B0A756E69666F726D206C6F77702076656334205F4C69676874436F6C6F72303B0A756E69666F726D2073616D706C6572324420756E6974795F4E4878526F7567686E6573733B0A756E69666F726D2068696768702073616D706C65723244205F536861646F774D6170546578747572653B0A756E69666F726D206D656469756D702076656334205F436F6C6F723B0A756E69666F726D2073616D706C65723244205F4D61696E5465783B0A756E69666F726D206D656469756D7020666C6F6174205F4D6574616C6C69633B0A756E69666F726D206D656469756D7020666C6F6174205F476C6F7373696E6573733B0A756E69666F726D2073616D706C65723244205F4C6967687454657874757265303B0A756E69666F726D206869676870206D61743420756E6974795F576F726C64546F4C696768743B0A756E69666F726D2073616D706C65723244205F4C696768745465787475726542303B0A76617279696E67206869676870207665633420786C765F544558434F4F5244303B0A76617279696E67206869676870207665633320786C765F544558434F4F5244313B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244333B0A76617279696E67206D656469756D70207665633320786C765F544558434F4F5244343B0A76617279696E67206D656469756D70207665633320786C765F544558434F4F5244353B0A766F6964206D61696E2028290A7B0A20206C6F777020666C6F617420617474656E5F313B0A20206C6F777020666C6F617420736861646F775F323B0A202068696768702076656334206C69676874436F6F72645F333B0A20206D656469756D70207665633320635F343B0A20206C6F7770207665633420746D707661725F353B0A2020746D707661725F35203D2074657874757265324420285F4D61696E5465782C20786C765F544558434F4F5244302E7879293B0A20206D656469756D70207665633320746D707661725F363B0A2020746D707661725F36203D20285F436F6C6F722E78797A202A20746D707661725F352E78797A293B0A20206D656469756D7020666C6F617420746D707661725F373B0A2020746D707661725F37203D20646F742028786C765F544558434F4F5244332E797A772C20786C765F544558434F4F524434293B0A20206D656469756D70207665633220746D707661725F383B0A2020746D707661725F382E78203D202828746D707661725F37202A20746D707661725F3729202A2028746D707661725F37202A20746D707661725F3729293B0A2020746D707661725F382E79203D2028312E30202D205F476C6F7373696E657373293B0A20206C6F7770207665633420746D707661725F393B0A2020746D707661725F39203D207465787475726532442028756E6974795F4E4878526F7567686E6573732C20746D707661725F38293B0A2020635F34203D20282828746D707661725F36202A200A2020202028302E37373930383337202D20285F4D6574616C6C6963202A20302E3737393038333729290A202029202B20280A2020202028746D707661725F392E77202A2031362E30290A2020202A200A202020206D697820287665633328302E323230393136332C20302E323230393136332C20302E32323039313633292C20746D707661725F362C2076656333285F4D6574616C6C696329290A20202929202A205F4C69676874436F6C6F72302E78797A293B0A20206869676870207665633420746D707661725F31303B0A2020746D707661725F31302E77203D20312E303B0A2020746D707661725F31302E78797A203D20786C765F544558434F4F5244313B0A20206C69676874436F6F72645F33203D2028756E6974795F576F726C64546F4C69676874202A20746D707661725F3130293B0A20206D656469756D7020666C6F6174207265616C74696D65536861646F77417474656E756174696F6E5F31313B0A20207265616C74696D65536861646F77417474656E756174696F6E5F3131203D20312E303B0A20206869676870207665633420746D707661725F31323B0A2020746D707661725F31322E77203D20312E303B0A2020746D707661725F31322E78797A203D20786C765F544558434F4F5244313B0A20206869676870207665633420746D707661725F31333B0A2020746D707661725F3133203D2028756E6974795F576F726C64546F536861646F775B305D202A20746D707661725F3132293B0A20206C6F777020666C6F617420746D707661725F31343B0A20206869676870207665633420746D707661725F31353B0A2020746D707661725F3135203D2074657874757265324450726F6A20285F536861646F774D6170546578747572652C20746D707661725F3133293B0A20206D656469756D7020666C6F617420746D707661725F31363B0A20206966202828746D707661725F31352E78203C2028746D707661725F31332E7A202F20746D707661725F31332E77292929207B0A20202020746D707661725F3136203D205F4C69676874536861646F77446174612E783B0A20207D20656C7365207B0A20202020746D707661725F3136203D20312E303B0A20207D3B0A2020746D707661725F3134203D20746D707661725F31363B0A20207265616C74696D65536861646F77417474656E756174696F6E5F3131203D20746D707661725F31343B0A2020736861646F775F32203D207265616C74696D65536861646F77417474656E756174696F6E5F31313B0A20206C6F7770207665633420746D707661725F31373B0A20206869676870207665633220505F31383B0A2020505F3138203D2028286C69676874436F6F72645F332E7879202F206C69676874436F6F72645F332E7729202B20302E35293B0A2020746D707661725F3137203D2074657874757265324420285F4C6967687454657874757265302C20505F3138293B0A2020686967687020666C6F617420746D707661725F31393B0A2020746D707661725F3139203D20646F7420286C69676874436F6F72645F332E78797A2C206C69676874436F6F72645F332E78797A293B0A20206C6F7770207665633420746D707661725F32303B0A2020746D707661725F3230203D2074657874757265324420285F4C696768745465787475726542302C207665633228746D707661725F313929293B0A2020686967687020666C6F617420746D707661725F32313B0A2020746D707661725F3231203D202828280A20202020666C6F617428286C69676874436F6F72645F332E7A203E20302E3029290A2020202A20746D707661725F31372E7729202A20746D707661725F32302E7729202A20736861646F775F32293B0A2020617474656E5F31203D20746D707661725F32313B0A2020635F34203D2028635F34202A2028617474656E5F31202A20636C616D7020280A20202020646F742028786C765F544558434F4F5244352C20786C765F544558434F4F524434290A20202C20302E302C20312E302929293B0A20206D656469756D70207665633420746D707661725F32323B0A2020746D707661725F32322E77203D20312E303B0A2020746D707661725F32322E78797A203D20635F343B0A20206D656469756D70207665633420786C61745F7661726F75747075745F32333B0A2020786C61745F7661726F75747075745F32332E78797A203D20746D707661725F32322E78797A3B0A2020786C61745F7661726F75747075745F32332E77203D20312E303B0A2020676C5F46726167446174615B305D203D20786C61745F7661726F75747075745F32333B0A7D0A0A0A23656E6469660A00001B000000000000000100000000000000000000000000000000000000324F040C0500000000000000000000000000000000000000010000000400000053504F540000000000000000000000000100000000000000000000000000000000000000324F040C0500000000000000000000000000000000000000000000000000000000000000000000000100000000000000000000000000000000000000324F040C0500000013000000020000000000000000000000010000000B000000444952454354494F4E414C00750E00002376657273696F6E203130300A0A236966646566205645525445580A6174747269627574652076656334205F676C65735665727465783B0A6174747269627574652076656333205F676C65734E6F726D616C3B0A6174747269627574652076656334205F676C65734D756C7469546578436F6F7264303B0A6174747269627574652076656334205F676C65734D756C7469546578436F6F7264313B0A756E69666F726D2068696768702076656333205F576F726C64537061636543616D657261506F733B0A756E69666F726D206D656469756D702076656334205F576F726C6453706163654C69676874506F73303B0A756E69666F726D206869676870206D61743420756E6974795F4F626A656374546F576F726C643B0A756E69666F726D206869676870206D61743420756E6974795F576F726C64546F4F626A6563743B0A756E69666F726D206869676870206D61743420756E6974795F4D617472697856503B0A756E69666F726D2068696768702076656334205F4D61696E5465785F53543B0A756E69666F726D2068696768702076656334205F44657461696C416C6265646F4D61705F53543B0A756E69666F726D206D656469756D7020666C6F6174205F55565365633B0A76617279696E67206869676870207665633420786C765F544558434F4F5244303B0A76617279696E67206869676870207665633320786C765F544558434F4F5244313B0A76617279696E67206869676870207665633220786C765F544558434F4F5244323B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244333B0A76617279696E67206D656469756D70207665633320786C765F544558434F4F5244343B0A76617279696E67206D656469756D70207665633320786C765F544558434F4F5244353B0A766F6964206D61696E2028290A7B0A20206D656469756D70207665633320746D707661725F313B0A2020746D707661725F31203D205F676C65734E6F726D616C3B0A20206869676870207665633220746D707661725F323B0A2020746D707661725F32203D205F676C65734D756C7469546578436F6F7264302E78793B0A20206869676870207665633220746D707661725F333B0A2020746D707661725F33203D205F676C65734D756C7469546578436F6F7264312E78793B0A20206D656469756D702076656333206E6F726D616C576F726C645F343B0A20206D656469756D702076656333206579655665635F353B0A20206D656469756D702076656333206C696768744469725F363B0A20206869676870207665633220746D707661725F373B0A20206D656469756D70207665633420746D707661725F383B0A20206869676870207665633420746D707661725F393B0A2020746D707661725F39203D2028756E6974795F4F626A656374546F576F726C64202A205F676C6573566572746578293B0A20206869676870207665633420746D707661725F31303B0A20206869676870207665633420746D707661725F31313B0A2020746D707661725F31312E77203D20312E303B0A2020746D707661725F31312E78797A203D205F676C65735665727465782E78797A3B0A2020746D707661725F3130203D2028756E6974795F4D61747269785650202A2028756E6974795F4F626A656374546F576F726C64202A20746D707661725F313129293B0A20206869676870207665633420746578636F6F72645F31323B0A2020746578636F6F72645F31322E7879203D2028285F676C65734D756C7469546578436F6F7264302E7879202A205F4D61696E5465785F53542E787929202B205F4D61696E5465785F53542E7A77293B0A20206869676870207665633220746D707661725F31333B0A202069662028285F5556536563203D3D20302E302929207B0A20202020746D707661725F3133203D20746D707661725F323B0A20207D20656C7365207B0A20202020746D707661725F3133203D20746D707661725F333B0A20207D3B0A2020746578636F6F72645F31322E7A77203D202828746D707661725F3133202A205F44657461696C416C6265646F4D61705F53542E787929202B205F44657461696C416C6265646F4D61705F53542E7A77293B0A20206869676870207665633320746D707661725F31343B0A2020746D707661725F3134203D20285F576F726C6453706163654C69676874506F73302E78797A202D2028746D707661725F392E78797A202A205F576F726C6453706163654C69676874506F73302E7729293B0A20206C696768744469725F36203D20746D707661725F31343B0A20206869676870207665633320746D707661725F31353B0A2020746D707661725F3135203D206E6F726D616C697A652828746D707661725F392E78797A202D205F576F726C64537061636543616D657261506F7329293B0A20206579655665635F35203D20746D707661725F31353B0A202068696768702076656333206E6F726D5F31363B0A20206E6F726D5F3136203D20746D707661725F313B0A20206869676870206D61743320746D707661725F31373B0A2020746D707661725F31375B305D203D20756E6974795F576F726C64546F4F626A6563745B305D2E78797A3B0A2020746D707661725F31375B315D203D20756E6974795F576F726C64546F4F626A6563745B315D2E78797A3B0A2020746D707661725F31375B325D203D20756E6974795F576F726C64546F4F626A6563745B325D2E78797A3B0A20206869676870207665633320746D707661725F31383B0A2020746D707661725F3138203D206E6F726D616C697A6528286E6F726D5F3136202A20746D707661725F313729293B0A20206E6F726D616C576F726C645F34203D20746D707661725F31383B0A2020746D707661725F382E797A77203D20286579655665635F35202D2028322E30202A20280A20202020646F7420286E6F726D616C576F726C645F342C206579655665635F35290A2020202A206E6F726D616C576F726C645F342929293B0A2020676C5F506F736974696F6E203D20746D707661725F31303B0A2020786C765F544558434F4F524430203D20746578636F6F72645F31323B0A2020786C765F544558434F4F524431203D20746D707661725F392E78797A3B0A2020786C765F544558434F4F524432203D20746D707661725F373B0A2020786C765F544558434F4F524433203D20746D707661725F383B0A2020786C765F544558434F4F524434203D206C696768744469725F363B0A2020786C765F544558434F4F524435203D206E6F726D616C576F726C645F343B0A7D0A0A0A23656E6469660A23696664656620465241474D454E540A756E69666F726D206C6F77702076656334205F4C69676874436F6C6F72303B0A756E69666F726D2073616D706C6572324420756E6974795F4E4878526F7567686E6573733B0A756E69666F726D206D656469756D702076656334205F436F6C6F723B0A756E69666F726D2073616D706C65723244205F4D61696E5465783B0A756E69666F726D206D656469756D7020666C6F6174205F4D6574616C6C69633B0A756E69666F726D206D656469756D7020666C6F6174205F476C6F7373696E6573733B0A76617279696E67206869676870207665633420786C765F544558434F4F5244303B0A76617279696E67206D656469756D70207665633420786C765F544558434F4F5244333B0A76617279696E67206D656469756D70207665633320786C765F544558434F4F5244343B0A76617279696E67206D656469756D70207665633320786C765F544558434F4F5244353B0A766F6964206D61696E2028290A7B0A20206D656469756D70207665633320635F313B0A20206C6F7770207665633420746D707661725F323B0A2020746D707661725F32203D2074657874757265324420285F4D61696E5465782C20786C765F544558434F4F5244302E7879293B0A20206D656469756D70207665633320746D707661725F333B0A2020746D707661725F33203D20285F436F6C6F722E78797A202A20746D707661725F322E78797A293B0A20206D656469756D7020666C6F617420746D707661725F343B0A2020746D707661725F34203D20646F742028786C765F544558434F4F5244332E797A772C20786C765F544558434F4F524434293B0A20206D656469756D70207665633220746D707661725F353B0A2020746D707661725F352E78203D202828746D707661725F34202A20746D707661725F3429202A2028746D707661725F34202A20746D707661725F3429293B0A2020746D707661725F352E79203D2028312E30202D205F476C6F7373696E657373293B0A20206C6F7770207665633420746D707661725F363B0A2020746D707661725F36203D207465787475726532442028756E6974795F4E4878526F7567686E6573732C20746D707661725F35293B0A2020635F31203D20282828746D707661725F33202A200A2020202028302E37373930383337202D20285F4D6574616C6C6963202A20302E3737393038333729290A202029202B20280A2020202028746D707661725F362E77202A2031362E30290A2020202A200A202020206D697820287665633328302E323230393136332C20302E323230393136332C20302E32323039313633292C20746D707661725F332C2076656333285F4D6574616C6C696329290A20202929202A205F4C69676874436F6C6F72302E78797A293B0A2020635F31203D2028635F31202A20636C616D702028646F742028786C765F544558434F4F5244352C20786C765F544558434F4F524434292C20302E302C20312E3029293B0A20206D656469756D70207665633420746D707661725F373B0A2020746D707661725F372E77203D20312E303B0A2020746D707661725F372E78797A203D20635F313B0A20206D656469756D70207665633420786C61745F7661726F75747075745F383B0A2020786C61745F7661726F75747075745F382E78797A203D20746D707661725F372E78797A3B0A2020786C61745F7661726F75747075745F382E77203D20312E303B0A2020676C5F46726167446174615B305D203D20786C61745F7661726F75747075745F383B0A7D0A0A0A23656E6469660A0000001B000000000000000100000000000000000000000000000000000000

 Block#1 Platform: 9 raw_size: 380156

 Block#2 Platform: 18 raw_size: 307148
  */
}