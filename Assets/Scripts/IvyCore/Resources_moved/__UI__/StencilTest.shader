﻿// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "IvyCore/UI/Teach/StencilTest" {
Properties {
	_StencilRef("Stencil ID", Float) = 3
	_TintColor ("Tint Color", Color) = (0.5,0.5,0.5,0.5)
	_MainTex ("Texture", 2D) = "white" {}
}

Category {
	Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
	Blend   SrcAlpha OneMinusSrcAlpha
	ColorMask ARGB
	Cull Off Lighting Off ZWrite Off
	
	SubShader {
		
		Stencil {
			Ref [_StencilRef]
			Comp NotEqual
		}

		Pass {
		
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_particles
			#pragma multi_compile_fog

			#include "UnityCG.cginc"

			sampler2D _MainTex;
			fixed4 _TintColor;
			
			struct appdata_t {
				float4 vertex : POSITION;
				fixed4 color : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f {
				float4 vertex : SV_POSITION;
				fixed4 color : COLOR;
				float2 texcoord : TEXCOORD0;
				UNITY_FOG_COORDS(1)

			};
			
			float4 _MainTex_ST;

			v2f vert (appdata_t v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				
				o.color = v.color;
				o.texcoord = TRANSFORM_TEX(v.texcoord,_MainTex);
				
				return o;
			}

			sampler2D_float _CameraDepthTexture;
			
			fixed4 frag (v2f i) : SV_Target
			{
				
				fixed4 col =  tex2D(_MainTex, i.texcoord);
				//fixed4 col = 2.0f * i.color * _TintColor * tex2D(_MainTex, i.texcoord);
				//UNITY_APPLY_FOG_COLOR(i.fogCoord, col, fixed4(0,0,0,0)); // fog towards black due to our blend mode
				col=i.color;
				return i.color;
			}
			ENDCG 
		}
	}	
}
}
