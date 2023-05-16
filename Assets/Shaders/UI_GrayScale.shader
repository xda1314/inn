Shader "_old_UI/GrayScale"
{
	Properties
	{
	_MainTex("Sprite Texture", 2D) = "white" { }
	_Color("Tint", Color) = (1,1,1,1)
	_StencilComp("Stencil Comparison", Float) = 8
	_Stencil("Stencil ID", Float) = 0
	_StencilOp("Stencil Operation", Float) = 0
	_StencilWriteMask("Stencil Write Mask", Float) = 255
	_StencilReadMask("Stencil Read Mask", Float) = 255
	_ColorMask("Color Mask", Float) = 15
	_AdjustTex("Adjust Curve Tex", 2D) = "white" { }
	_AdjustPercentage("Adjust Percentage", Range(0, 1)) = 1
	__RampOffset("Ramp Offset", Range(0, 1)) = 0.5
	[Toggle(UNITY_UI_ALPHACLIP)] _UseUIAlphaClip("Use Alpha Clip", Float) = 0
	}
	SubShader
	{
		 Tags { "CanUseSpriteAtlas" = "true" "IGNOREPROJECTOR" = "true" "PreviewType" = "Plane" "QUEUE" = "Transparent" "RenderType" = "Transparent" }
		 Pass {
			Name "DEFAULT" 
			Tags { "CanUseSpriteAtlas" = "true" "IGNOREPROJECTOR" = "true" "PreviewType" = "Plane" "QUEUE" = "Transparent" "RenderType" = "Transparent" }
			
			ZTest Off
			ZWrite Off
			Cull Off
			GpuProgramID 51540
			Program "vp" 
			{
				SubProgram "gles hw_tier00 "
				{
				"#version 100

		#ifdef VERTEX
		attribute vec4 _glesVertex;
		attribute vec4 _glesColor;
		attribute vec4 _glesMultiTexCoord0;
		uniform highp mat4 unity_ObjectToWorld;
		uniform highp mat4 unity_MatrixVP;
		uniform lowp vec4 _Color;
		varying lowp vec4 xlv_COLOR;
		varying highp vec2 xlv_TEXCOORD0;
		varying highp vec4 xlv_TEXCOORD1;
		void main()
		{
		  highp vec4 tmpvar_1;
		  tmpvar_1 = _glesVertex;
		  lowp vec4 tmpvar_2;
		  highp vec4 tmpvar_3;
		  tmpvar_3.w = 1.0;
		  tmpvar_3.xyz = tmpvar_1.xyz;
		  tmpvar_2 = (_glesColor * _Color);
		  gl_Position = (unity_MatrixVP * (unity_ObjectToWorld * tmpvar_3));
		  xlv_COLOR = tmpvar_2;
		  xlv_TEXCOORD0 = _glesMultiTexCoord0.xy;
		  xlv_TEXCOORD1 = tmpvar_1;
		}


		#endif
		#ifdef FRAGMENT
		uniform lowp vec4 _TextureSampleAdd;
		uniform highp vec4 _ClipRect;
		uniform sampler2D _MainTex;
		uniform sampler2D _AdjustTex;
		uniform lowp float _AdjustPercentage;
		uniform mediump float _RampOffset;
		varying lowp vec4 xlv_COLOR;
		varying highp vec2 xlv_TEXCOORD0;
		varying highp vec4 xlv_TEXCOORD1;
		void main()
		{
		  lowp vec4 xlat_varoutput_1;
		  lowp float grayscale_2;
		  lowp vec4 color_3;
		  lowp vec4 tmpvar_4;
		  tmpvar_4 = ((texture2D (_MainTex, xlv_TEXCOORD0) + _TextureSampleAdd) * xlv_COLOR);
		  color_3.xyz = tmpvar_4.xyz;
		  highp float tmpvar_5;
		  highp vec2 tmpvar_6;
		  tmpvar_6.x = float((_ClipRect.z >= xlv_TEXCOORD1.x));
		  tmpvar_6.y = float((_ClipRect.w >= xlv_TEXCOORD1.y));
		  highp vec2 tmpvar_7;
		  tmpvar_7 = (vec2(greaterThanEqual(xlv_TEXCOORD1.xy, _ClipRect.xy)) * tmpvar_6);
		  tmpvar_5 = (tmpvar_7.x * tmpvar_7.y);
		  color_3.w = (tmpvar_4.w * tmpvar_5);
		  mediump vec3 rgb_8;
		  rgb_8 = color_3.xyz;
		  mediump float tmpvar_9;
		  tmpvar_9 = dot(rgb_8, vec3(0.22, 0.707, 0.071));
		  grayscale_2 = tmpvar_9;
		  mediump vec2 tmpvar_10;
		  tmpvar_10.y = 0.5;
		  tmpvar_10.x = (grayscale_2 + _RampOffset);
		  xlat_varoutput_1.xyz = mix(color_3, texture2D (_AdjustTex, tmpvar_10), vec4(_AdjustPercentage)).xyz;
		  xlat_varoutput_1.w = color_3.w;
		  gl_FragData[0] = xlat_varoutput_1;
		}


		#endif
		"
		}
		SubProgram "gles hw_tier01 " {
		"#version 100

		#ifdef VERTEX
		attribute vec4 _glesVertex;
		attribute vec4 _glesColor;
		attribute vec4 _glesMultiTexCoord0;
		uniform highp mat4 unity_ObjectToWorld;
		uniform highp mat4 unity_MatrixVP;
		uniform lowp vec4 _Color;
		varying lowp vec4 xlv_COLOR;
		varying highp vec2 xlv_TEXCOORD0;
		varying highp vec4 xlv_TEXCOORD1;
		void main()
		{
		  highp vec4 tmpvar_1;
		  tmpvar_1 = _glesVertex;
		  lowp vec4 tmpvar_2;
		  highp vec4 tmpvar_3;
		  tmpvar_3.w = 1.0;
		  tmpvar_3.xyz = tmpvar_1.xyz;
		  tmpvar_2 = (_glesColor * _Color);
		  gl_Position = (unity_MatrixVP * (unity_ObjectToWorld * tmpvar_3));
		  xlv_COLOR = tmpvar_2;
		  xlv_TEXCOORD0 = _glesMultiTexCoord0.xy;
		  xlv_TEXCOORD1 = tmpvar_1;
		}


		#endif
		#ifdef FRAGMENT
		uniform lowp vec4 _TextureSampleAdd;
		uniform highp vec4 _ClipRect;
		uniform sampler2D _MainTex;
		uniform sampler2D _AdjustTex;
		uniform lowp float _AdjustPercentage;
		uniform mediump float _RampOffset;
		varying lowp vec4 xlv_COLOR;
		varying highp vec2 xlv_TEXCOORD0;
		varying highp vec4 xlv_TEXCOORD1;
		void main()
		{
		  lowp vec4 xlat_varoutput_1;
		  lowp float grayscale_2;
		  lowp vec4 color_3;
		  lowp vec4 tmpvar_4;
		  tmpvar_4 = ((texture2D (_MainTex, xlv_TEXCOORD0) + _TextureSampleAdd) * xlv_COLOR);
		  color_3.xyz = tmpvar_4.xyz;
		  highp float tmpvar_5;
		  highp vec2 tmpvar_6;
		  tmpvar_6.x = float((_ClipRect.z >= xlv_TEXCOORD1.x));
		  tmpvar_6.y = float((_ClipRect.w >= xlv_TEXCOORD1.y));
		  highp vec2 tmpvar_7;
		  tmpvar_7 = (vec2(greaterThanEqual(xlv_TEXCOORD1.xy, _ClipRect.xy)) * tmpvar_6);
		  tmpvar_5 = (tmpvar_7.x * tmpvar_7.y);
		  color_3.w = (tmpvar_4.w * tmpvar_5);
		  mediump vec3 rgb_8;
		  rgb_8 = color_3.xyz;
		  mediump float tmpvar_9;
		  tmpvar_9 = dot(rgb_8, vec3(0.22, 0.707, 0.071));
		  grayscale_2 = tmpvar_9;
		  mediump vec2 tmpvar_10;
		  tmpvar_10.y = 0.5;
		  tmpvar_10.x = (grayscale_2 + _RampOffset);
		  xlat_varoutput_1.xyz = mix(color_3, texture2D (_AdjustTex, tmpvar_10), vec4(_AdjustPercentage)).xyz;
		  xlat_varoutput_1.w = color_3.w;
		  gl_FragData[0] = xlat_varoutput_1;
		}


		#endif
		"
		}
		SubProgram "gles hw_tier02 " {
		"#version 100

		#ifdef VERTEX
		attribute vec4 _glesVertex;
		attribute vec4 _glesColor;
		attribute vec4 _glesMultiTexCoord0;
		uniform highp mat4 unity_ObjectToWorld;
		uniform highp mat4 unity_MatrixVP;
		uniform lowp vec4 _Color;
		varying lowp vec4 xlv_COLOR;
		varying highp vec2 xlv_TEXCOORD0;
		varying highp vec4 xlv_TEXCOORD1;
		void main()
		{
		  highp vec4 tmpvar_1;
		  tmpvar_1 = _glesVertex;
		  lowp vec4 tmpvar_2;
		  highp vec4 tmpvar_3;
		  tmpvar_3.w = 1.0;
		  tmpvar_3.xyz = tmpvar_1.xyz;
		  tmpvar_2 = (_glesColor * _Color);
		  gl_Position = (unity_MatrixVP * (unity_ObjectToWorld * tmpvar_3));
		  xlv_COLOR = tmpvar_2;
		  xlv_TEXCOORD0 = _glesMultiTexCoord0.xy;
		  xlv_TEXCOORD1 = tmpvar_1;
		}


		#endif
		#ifdef FRAGMENT
		uniform lowp vec4 _TextureSampleAdd;
		uniform highp vec4 _ClipRect;
		uniform sampler2D _MainTex;
		uniform sampler2D _AdjustTex;
		uniform lowp float _AdjustPercentage;
		uniform mediump float _RampOffset;
		varying lowp vec4 xlv_COLOR;
		varying highp vec2 xlv_TEXCOORD0;
		varying highp vec4 xlv_TEXCOORD1;
		void main()
		{
		  lowp vec4 xlat_varoutput_1;
		  lowp float grayscale_2;
		  lowp vec4 color_3;
		  lowp vec4 tmpvar_4;
		  tmpvar_4 = ((texture2D (_MainTex, xlv_TEXCOORD0) + _TextureSampleAdd) * xlv_COLOR);
		  color_3.xyz = tmpvar_4.xyz;
		  highp float tmpvar_5;
		  highp vec2 tmpvar_6;
		  tmpvar_6.x = float((_ClipRect.z >= xlv_TEXCOORD1.x));
		  tmpvar_6.y = float((_ClipRect.w >= xlv_TEXCOORD1.y));
		  highp vec2 tmpvar_7;
		  tmpvar_7 = (vec2(greaterThanEqual(xlv_TEXCOORD1.xy, _ClipRect.xy)) * tmpvar_6);
		  tmpvar_5 = (tmpvar_7.x * tmpvar_7.y);
		  color_3.w = (tmpvar_4.w * tmpvar_5);
		  mediump vec3 rgb_8;
		  rgb_8 = color_3.xyz;
		  mediump float tmpvar_9;
		  tmpvar_9 = dot(rgb_8, vec3(0.22, 0.707, 0.071));
		  grayscale_2 = tmpvar_9;
		  mediump vec2 tmpvar_10;
		  tmpvar_10.y = 0.5;
		  tmpvar_10.x = (grayscale_2 + _RampOffset);
		  xlat_varoutput_1.xyz = mix(color_3, texture2D (_AdjustTex, tmpvar_10), vec4(_AdjustPercentage)).xyz;
		  xlat_varoutput_1.w = color_3.w;
		  gl_FragData[0] = xlat_varoutput_1;
		}


		#endif
		"
		}
		SubProgram "gles hw_tier00 " {
		Keywords { "UNITY_UI_ALPHACLIP" }
		"#version 100

		#ifdef VERTEX
		attribute vec4 _glesVertex;
		attribute vec4 _glesColor;
		attribute vec4 _glesMultiTexCoord0;
		uniform highp mat4 unity_ObjectToWorld;
		uniform highp mat4 unity_MatrixVP;
		uniform lowp vec4 _Color;
		varying lowp vec4 xlv_COLOR;
		varying highp vec2 xlv_TEXCOORD0;
		varying highp vec4 xlv_TEXCOORD1;
		void main()
		{
		  highp vec4 tmpvar_1;
		  tmpvar_1 = _glesVertex;
		  lowp vec4 tmpvar_2;
		  highp vec4 tmpvar_3;
		  tmpvar_3.w = 1.0;
		  tmpvar_3.xyz = tmpvar_1.xyz;
		  tmpvar_2 = (_glesColor * _Color);
		  gl_Position = (unity_MatrixVP * (unity_ObjectToWorld * tmpvar_3));
		  xlv_COLOR = tmpvar_2;
		  xlv_TEXCOORD0 = _glesMultiTexCoord0.xy;
		  xlv_TEXCOORD1 = tmpvar_1;
		}


		#endif
		#ifdef FRAGMENT
		uniform lowp vec4 _TextureSampleAdd;
		uniform highp vec4 _ClipRect;
		uniform sampler2D _MainTex;
		uniform sampler2D _AdjustTex;
		uniform lowp float _AdjustPercentage;
		uniform mediump float _RampOffset;
		varying lowp vec4 xlv_COLOR;
		varying highp vec2 xlv_TEXCOORD0;
		varying highp vec4 xlv_TEXCOORD1;
		void main()
		{
		  lowp vec4 xlat_varoutput_1;
		  lowp float grayscale_2;
		  lowp vec4 color_3;
		  lowp vec4 tmpvar_4;
		  tmpvar_4 = ((texture2D (_MainTex, xlv_TEXCOORD0) + _TextureSampleAdd) * xlv_COLOR);
		  color_3.xyz = tmpvar_4.xyz;
		  highp float tmpvar_5;
		  highp vec2 tmpvar_6;
		  tmpvar_6.x = float((_ClipRect.z >= xlv_TEXCOORD1.x));
		  tmpvar_6.y = float((_ClipRect.w >= xlv_TEXCOORD1.y));
		  highp vec2 tmpvar_7;
		  tmpvar_7 = (vec2(greaterThanEqual(xlv_TEXCOORD1.xy, _ClipRect.xy)) * tmpvar_6);
		  tmpvar_5 = (tmpvar_7.x * tmpvar_7.y);
		  color_3.w = (tmpvar_4.w * tmpvar_5);
		  mediump vec3 rgb_8;
		  rgb_8 = color_3.xyz;
		  mediump float tmpvar_9;
		  tmpvar_9 = dot(rgb_8, vec3(0.22, 0.707, 0.071));
		  grayscale_2 = tmpvar_9;
		  mediump vec2 tmpvar_10;
		  tmpvar_10.y = 0.5;
		  tmpvar_10.x = (grayscale_2 + _RampOffset);
		  xlat_varoutput_1.xyz = mix(color_3, texture2D (_AdjustTex, tmpvar_10), vec4(_AdjustPercentage)).xyz;
		  xlat_varoutput_1.w = color_3.w;
		  lowp float x_11;
		  x_11 = (color_3.w - 0.001);
		  if ((x_11 < 0.0)) {
			discard;
		  };
		  gl_FragData[0] = xlat_varoutput_1;
		}


		#endif
		"
		}
		SubProgram "gles hw_tier01 " {
		Keywords { "UNITY_UI_ALPHACLIP" }
		"#version 100

		#ifdef VERTEX
		attribute vec4 _glesVertex;
		attribute vec4 _glesColor;
		attribute vec4 _glesMultiTexCoord0;
		uniform highp mat4 unity_ObjectToWorld;
		uniform highp mat4 unity_MatrixVP;
		uniform lowp vec4 _Color;
		varying lowp vec4 xlv_COLOR;
		varying highp vec2 xlv_TEXCOORD0;
		varying highp vec4 xlv_TEXCOORD1;
		void main()
		{
		  highp vec4 tmpvar_1;
		  tmpvar_1 = _glesVertex;
		  lowp vec4 tmpvar_2;
		  highp vec4 tmpvar_3;
		  tmpvar_3.w = 1.0;
		  tmpvar_3.xyz = tmpvar_1.xyz;
		  tmpvar_2 = (_glesColor * _Color);
		  gl_Position = (unity_MatrixVP * (unity_ObjectToWorld * tmpvar_3));
		  xlv_COLOR = tmpvar_2;
		  xlv_TEXCOORD0 = _glesMultiTexCoord0.xy;
		  xlv_TEXCOORD1 = tmpvar_1;
		}


		#endif
		#ifdef FRAGMENT
		uniform lowp vec4 _TextureSampleAdd;
		uniform highp vec4 _ClipRect;
		uniform sampler2D _MainTex;
		uniform sampler2D _AdjustTex;
		uniform lowp float _AdjustPercentage;
		uniform mediump float _RampOffset;
		varying lowp vec4 xlv_COLOR;
		varying highp vec2 xlv_TEXCOORD0;
		varying highp vec4 xlv_TEXCOORD1;
		void main()
		{
		  lowp vec4 xlat_varoutput_1;
		  lowp float grayscale_2;
		  lowp vec4 color_3;
		  lowp vec4 tmpvar_4;
		  tmpvar_4 = ((texture2D (_MainTex, xlv_TEXCOORD0) + _TextureSampleAdd) * xlv_COLOR);
		  color_3.xyz = tmpvar_4.xyz;
		  highp float tmpvar_5;
		  highp vec2 tmpvar_6;
		  tmpvar_6.x = float((_ClipRect.z >= xlv_TEXCOORD1.x));
		  tmpvar_6.y = float((_ClipRect.w >= xlv_TEXCOORD1.y));
		  highp vec2 tmpvar_7;
		  tmpvar_7 = (vec2(greaterThanEqual(xlv_TEXCOORD1.xy, _ClipRect.xy)) * tmpvar_6);
		  tmpvar_5 = (tmpvar_7.x * tmpvar_7.y);
		  color_3.w = (tmpvar_4.w * tmpvar_5);
		  mediump vec3 rgb_8;
		  rgb_8 = color_3.xyz;
		  mediump float tmpvar_9;
		  tmpvar_9 = dot(rgb_8, vec3(0.22, 0.707, 0.071));
		  grayscale_2 = tmpvar_9;
		  mediump vec2 tmpvar_10;
		  tmpvar_10.y = 0.5;
		  tmpvar_10.x = (grayscale_2 + _RampOffset);
		  xlat_varoutput_1.xyz = mix(color_3, texture2D (_AdjustTex, tmpvar_10), vec4(_AdjustPercentage)).xyz;
		  xlat_varoutput_1.w = color_3.w;
		  lowp float x_11;
		  x_11 = (color_3.w - 0.001);
		  if ((x_11 < 0.0)) {
			discard;
		  };
		  gl_FragData[0] = xlat_varoutput_1;
		}


		#endif
		"
		}
		SubProgram "gles hw_tier02 " {
		Keywords { "UNITY_UI_ALPHACLIP" }
		"#version 100

		#ifdef VERTEX
		attribute vec4 _glesVertex;
		attribute vec4 _glesColor;
		attribute vec4 _glesMultiTexCoord0;
		uniform highp mat4 unity_ObjectToWorld;
		uniform highp mat4 unity_MatrixVP;
		uniform lowp vec4 _Color;
		varying lowp vec4 xlv_COLOR;
		varying highp vec2 xlv_TEXCOORD0;
		varying highp vec4 xlv_TEXCOORD1;
		void main()
		{
		  highp vec4 tmpvar_1;
		  tmpvar_1 = _glesVertex;
		  lowp vec4 tmpvar_2;
		  highp vec4 tmpvar_3;
		  tmpvar_3.w = 1.0;
		  tmpvar_3.xyz = tmpvar_1.xyz;
		  tmpvar_2 = (_glesColor * _Color);
		  gl_Position = (unity_MatrixVP * (unity_ObjectToWorld * tmpvar_3));
		  xlv_COLOR = tmpvar_2;
		  xlv_TEXCOORD0 = _glesMultiTexCoord0.xy;
		  xlv_TEXCOORD1 = tmpvar_1;
		}


		#endif
		#ifdef FRAGMENT
		uniform lowp vec4 _TextureSampleAdd;
		uniform highp vec4 _ClipRect;
		uniform sampler2D _MainTex;
		uniform sampler2D _AdjustTex;
		uniform lowp float _AdjustPercentage;
		uniform mediump float _RampOffset;
		varying lowp vec4 xlv_COLOR;
		varying highp vec2 xlv_TEXCOORD0;
		varying highp vec4 xlv_TEXCOORD1;
		void main()
		{
		  lowp vec4 xlat_varoutput_1;
		  lowp float grayscale_2;
		  lowp vec4 color_3;
		  lowp vec4 tmpvar_4;
		  tmpvar_4 = ((texture2D (_MainTex, xlv_TEXCOORD0) + _TextureSampleAdd) * xlv_COLOR);
		  color_3.xyz = tmpvar_4.xyz;
		  highp float tmpvar_5;
		  highp vec2 tmpvar_6;
		  tmpvar_6.x = float((_ClipRect.z >= xlv_TEXCOORD1.x));
		  tmpvar_6.y = float((_ClipRect.w >= xlv_TEXCOORD1.y));
		  highp vec2 tmpvar_7;
		  tmpvar_7 = (vec2(greaterThanEqual(xlv_TEXCOORD1.xy, _ClipRect.xy)) * tmpvar_6);
		  tmpvar_5 = (tmpvar_7.x * tmpvar_7.y);
		  color_3.w = (tmpvar_4.w * tmpvar_5);
		  mediump vec3 rgb_8;
		  rgb_8 = color_3.xyz;
		  mediump float tmpvar_9;
		  tmpvar_9 = dot(rgb_8, vec3(0.22, 0.707, 0.071));
		  grayscale_2 = tmpvar_9;
		  mediump vec2 tmpvar_10;
		  tmpvar_10.y = 0.5;
		  tmpvar_10.x = (grayscale_2 + _RampOffset);
		  xlat_varoutput_1.xyz = mix(color_3, texture2D (_AdjustTex, tmpvar_10), vec4(_AdjustPercentage)).xyz;
		  xlat_varoutput_1.w = color_3.w;
		  lowp float x_11;
		  x_11 = (color_3.w - 0.001);
		  if ((x_11 < 0.0)) {
			discard;
		  };
		  gl_FragData[0] = xlat_varoutput_1;
		}


		#endif
		"
		}
		}
		Program "fp" {
		SubProgram "gles hw_tier00 " {
		""
		}
		SubProgram "gles hw_tier01 " {
		""
		}
		SubProgram "gles hw_tier02 " {
		""
		}
		SubProgram "gles hw_tier00 " {
		Keywords { "UNITY_UI_ALPHACLIP" }
		""
		}
		SubProgram "gles hw_tier01 " {
		Keywords { "UNITY_UI_ALPHACLIP" }
		""
		}
		SubProgram "gles hw_tier02 " {
		Keywords { "UNITY_UI_ALPHACLIP" }
		""
		}
		}
		}
	}
}