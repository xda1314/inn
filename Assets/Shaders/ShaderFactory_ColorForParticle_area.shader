Shader "ShaderFactory/ColorForParticle_area"
{
    Properties
    {
         _MainTex ("Texture", 2D) = "white" { }
		 _Area ("Area", Vector) = (0,0,1,1)
    }
    SubShader
    {
        Tags { "CanUseSpriteAtlas" = "true" "IGNOREPROJECTOR" = "true" "PreviewType" = "Plane" "QUEUE" = "Transparent" "RenderType" = "Transparent" }
		
		ZTest Always
		Cull Off
		Lighting Off
		ZWrite Off
		ColorMask RGB
        Blend SrcAlpha One
        Pass{
        // No culling or depth
            CGPROGRAM
	        sampler2D _MainTex;
            #pragma vertex vert
            #pragma fragment frag
	        #pragma fragmentoption 

            #include "UnityCG.cginc"
	        float4 _Area;

	        struct appdata_t
            {
                float4 vertex : POSITION;
		        fixed4 color : COLOR;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f
            {             
                float4 vertex : SV_POSITION;
		        float2 uv : TEXCOORD0;
                fixed4 color : COLOR;
	            float2 worldPos : TEXCOORD1;
            };

			//vertex shader
			v2f vert(appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
				o.color = v.color;
				//uv���긳ֵ��output
                o.uv = v.texcoord;
				o.worldPos = mul(unity_ObjectToWorld, v.vertex).xy;
                return o;
            }

            
            fixed4 frag (v2f i) : SV_Target
            {
				bool inArea = i.worldPos.x >= _Area.x && i.worldPos.x <= _Area.z && i.worldPos.y >= _Area.y && i.worldPos.y <= _Area.w;
                fixed4 col = tex2D(_MainTex, i.uv);
                fixed4 xlv_COLOR = i.color;
                
                col.x = (col.x * xlv_COLOR.x);
                col.y = (col.y * xlv_COLOR.y);
                col.z = (col.z * xlv_COLOR.z);
                col.w = (col.w * xlv_COLOR.w);
			    return inArea? col : fixed4(0,0,0,0);                
            }
            ENDCG
        }
    }
	//��ֹshaderʧЧ�ı��ϴ�ʩ
	FallBack Off
}
