// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "UIEffect/ImageGray"
{
    Properties
    {
        [PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
        _Color("Tint", Color) = (1,1,1,1)
        _Radius("半径",Range(0,10)) = 0
        _StencilComp("Stencil Comparison", Float) = 8
        _Stencil("Stencil ID", Float) = 0
        _StencilOp("Stencil Operation", Float) = 0
        _StencilWriteMask("Stencil Write Mask", Float) = 255
        _StencilReadMask("Stencil Read Mask", Float) = 255

        _ColorMask("Color Mask", Float) = 15
    }

        SubShader
        {
            Tags
            {
                "Queue" = "Transparent"
                "IgnoreProjector" = "True"
                "RenderType" = "Transparent"
                "PreviewType" = "Plane"
                "CanUseSpriteAtlas" = "True"
            }

            Stencil
            {
                Ref[_Stencil]
                Comp[_StencilComp]
                Pass[_StencilOp]
                ReadMask[_StencilReadMask]
                WriteMask[_StencilWriteMask]
            }

            Cull Off
            Lighting Off
            ZWrite Off
            ZTest[unity_GUIZTestMode]
            Fog { Mode Off }
            Blend SrcAlpha OneMinusSrcAlpha
            ColorMask[_ColorMask]

            Pass
            {
            CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #include "UnityCG.cginc"

                struct a2v
                {
                    float4 vertex   : POSITION;
                    float4 color    : COLOR;
                    float2 uv : TEXCOORD0;
                };

                struct v2f
                {
                    float4 vertex   : SV_POSITION;
                    fixed4 color : COLOR;
                    half2 uv  : TEXCOORD0;
                };

                fixed4 _Color;
                float4 _MainTex_TexelSize;
                float _Radius;
                sampler2D _MainTex;

                fixed4 Blur(sampler2D tex, half2 uv, half2 blurSize)
                {
                    int KERNEL_SIZE = 3;

                    float4 o = 0;
                    float sum = 0;
                    float weight;
                    half2 texcood;
                    for (int x = -KERNEL_SIZE / 2; x <= KERNEL_SIZE / 2; x++)
                    {
                        for (int y = -KERNEL_SIZE / 2; y <= KERNEL_SIZE / 2; y++)
                        {
                            texcood = uv;
                            texcood.x += blurSize.x * x;
                            texcood.y += blurSize.y * y;
                            weight = 1.0 / (abs(x) + abs(y) + 2);
                            o += tex2D(tex, texcood) * weight;
                            sum += weight;
                        }
                    }
                    return o / sum;
                }

                v2f vert(a2v IN)
                {
                    v2f OUT;
                    OUT.vertex = UnityObjectToClipPos(IN.vertex);
                    OUT.uv = IN.uv;
                    OUT.vertex.xy += (_ScreenParams.zw - 1.0) * float2(-1,1);
                    OUT.color = IN.color * _Color;
                    return OUT;
                }


                fixed4 frag(v2f IN) : SV_Target
                {
                    half4 color = tex2D(_MainTex, IN.uv) * IN.color;
                    clip(color.a - 0.01);
                    float gray = dot(color.rgb, fixed3(0.22, 0.707, 0.071));
                    color= half4(gray, gray, gray, color.a);
                    //模糊效果失败  学习再来
                    //fixed4 col = Blur(_MainTex, IN.uv, _Radius * _MainTex_TexelSize.xy);
                    return color;
                }
            ENDCG
            }
        }
}