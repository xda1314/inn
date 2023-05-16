Shader "Effect/MoveLight"
{
    Properties
    {
        _MainTex ("Base (RGB), Alpha (A)", 2D) = "black" {}

        
        _Width("Width", range(0.1, 1)) = 0.25// 宽度
        
        _Angle("Angle", range(0, 89)) = 73// 角度
        
        _Light("Light", range(0, 1)) = 1// 亮度
        _Alpah("Alpah", range(0, 1)) = 1

        _Radius("Radius", range(0, 0.5)) = 0.5

        _Interval ("Interval", Int) = 3	// 间隔
        _Duration ("duration", Float) = 1.5	// 持续时间
        _FlowColor("Flow Color", Color) = (1, 1, 1, 1)		// 流光颜色
    }
    
    SubShader
    {
        LOD 200

        Tags
        {
            "Queue" = "Transparent"
            "IgnoreProjector" = "True"
            "RenderType" = "Transparent"
            "DisableBatching" = "True"
        }
        
        Pass
        {
            Cull Off
            Lighting Off
            ZWrite Off
            Fog { Mode Off }
            Offset -1, -1
            Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag			
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _MainTex_ST;
            
            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 texcoord : TEXCOORD0;
                fixed4 color : COLOR;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };
            
            struct v2f
            {
                float4 vertex : SV_POSITION;
                half2 texcoord : TEXCOORD0;
                fixed4 color : COLOR;
                UNITY_VERTEX_OUTPUT_STEREO
            };
            
            float _Width;
            float _Angle;
            float _Light;
            float _Alpah;
            int _Interval;
            float _Duration;
            float _Radius;
            fixed4 _FlowColor;

            v2f vert (appdata_t v)
            {
                v2f o;
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.texcoord = v.texcoord;
                o.color = v.color;
                return o;
            }


            fixed inFlow(float angle, float2 uv, fixed width, int interval, float duration)
            {
                float rad = angle * 0.0174444;
                float tanRad = tan(rad);

                float maxYProj2X = 1.0 / tanRad;
                float totalMovX = 1 + width + maxYProj2X;

                float totalTime = interval + duration;
                int cnt = _Time.y / totalTime;
                float currentTime = _Time.y - cnt * totalTime;

                fixed flow = 0;
                if(currentTime < duration)
                {
                    fixed x0 = currentTime / (duration / totalMovX);
                    float yProj2X = uv.y / tanRad;
                    float xLeft = x0 - width - yProj2X;
                    float xRight = xLeft + width;
                    float xMid = 0.5 * (xLeft + xRight);
                    flow = step(xLeft, uv.x) * step(uv.x, xRight);
                    // 插值，根据与中心的距离的比例来计算亮度
                    flow *= (width - 2 * abs(uv.x - xMid)) / width;
                }
                return flow;
            }

            
            fixed4 frag (v2f IN) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, IN.texcoord)* IN.color;
                
                if(col.a == 0)
                {
                    discard;
                }
                else
                {
                    fixed v=inFlow(_Angle,IN.texcoord,_Width,_Interval,_Duration);

                    float4 target = float4(_FlowColor.r, _FlowColor.g,_FlowColor.b, _FlowColor.a*lerp(0,_Alpah,v)) ;
                    col.rgba = lerp(float4(0, 0, 0, 0), target, _Light);

                    float2 endUV = IN.texcoord-float2(0.5,0.5);
                    if(abs(endUV.x) > 0.5-_Radius && abs(endUV.y) > 0.5-_Radius)
                    {
                        if(length(abs(endUV)-float2(0.5-_Radius,0.5-_Radius)) >_Radius)
                        {
                            discard;
                        } 
                    }
                }
                return col;
            }
            ENDCG
        }
    }

    SubShader
    {
        LOD 100

        Tags
        {
            "Queue" = "Transparent"
            "IgnoreProjector" = "True"
            "RenderType" = "Transparent"
            "DisableBatching" = "True"
        }
        
        Pass
        {
            Cull Off
            Lighting Off
            ZWrite Off
            Fog { Mode Off }
            Offset -1, -1
            //ColorMask RGB
            Blend SrcAlpha OneMinusSrcAlpha
            ColorMaterial AmbientAndDiffuse
            
            SetTexture [_MainTex]
            {
                Combine Texture * Primary
            }
        }
    }
}
