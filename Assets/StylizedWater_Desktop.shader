Shader "StylizedWater/Desktop" {
	Properties {
		[HDR] _WaterColor ("Water Color", Vector) = (0.1176471,0.6348885,1,0)
		[HDR] _WaterShallowColor ("WaterShallowColor", Vector) = (0.4191176,0.7596349,1,0)
		_Wavetint ("Wave tint", Range(-1, 1)) = 0.1
		[HDR] _FresnelColor ("Fresnel Color", Vector) = (1,1,1,0.484)
		[HDR] _RimColor ("Rim Color", Vector) = (1,1,1,0.5019608)
		_NormalStrength ("NormalStrength", Range(0, 1)) = 0.25
		_Transparency ("Transparency", Range(0, 1)) = 0.75
		_Glossiness ("Glossiness", Range(0, 1)) = 0.85
		_Fresnelexponent ("Fresnel exponent", Float) = 4
		_ReflectionStrength ("Reflection Strength", Range(0, 1)) = 0
		_ReflectionFresnel ("Reflection Fresnel", Range(2, 10)) = 5
		_RefractionAmount ("Refraction Amount", Range(0, 0.2)) = 0.05
		_ReflectionRefraction ("ReflectionRefraction", Range(0, 0.2)) = 0.05
		[Toggle] _Worldspacetiling ("Worldspace tiling", Float) = 1
		_NormalTiling ("NormalTiling", Range(0, 1)) = 0.9
		_EdgeFade ("EdgeFade", Range(0.01, 3)) = 0.2448298
		_RimSize ("Rim Size", Range(0, 20)) = 6
		_Rimfalloff ("Rim falloff", Range(0.1, 50)) = 10
		_Rimtiling ("Rim tiling", Float) = 0.5
		_FoamOpacity ("FoamOpacity", Range(-1, 1)) = 0.05
		_FoamDistortion ("FoamDistortion", Range(0, 3)) = 0.45
		[IntRange] _UseIntersectionFoam ("UseIntersectionFoam", Range(0, 1)) = 0
		_FoamSpeed ("FoamSpeed", Range(0, 1)) = 0.1
		_FoamSize ("FoamSize", Float) = 0
		_FoamTiling ("FoamTiling", Float) = 0.05
		_Depth ("Depth", Range(0, 100)) = 15
		_Wavesspeed ("Waves speed", Range(0, 10)) = 0.75
		_WaveHeight ("Wave Height", Range(0, 1)) = 0.05
		_WaveFoam ("Wave Foam", Range(0, 10)) = 0
		_WaveSize ("Wave Size", Range(0, 10)) = 0.1
		_WaveDirection ("WaveDirection", Vector) = (1,0,0,0)
		[NoScaleOffset] [Normal] _Normals ("Normals", 2D) = "bump" {}
		[NoScaleOffset] _Shadermap ("Shadermap", 2D) = "black" {}
		_RimDistortion ("RimDistortion", Range(0, 0.2)) = 0.1
		[NoScaleOffset] _GradientTex ("GradientTex", 2D) = "gray" {}
		_MacroBlendDistance ("MacroBlendDistance", Range(250, 3000)) = 2000
		[Toggle(_SECONDARY_WAVES_ON)] _SECONDARY_WAVES ("SECONDARY_WAVES", Float) = 0
		[Toggle(_MACRO_WAVES_ON)] _MACRO_WAVES ("MACRO_WAVES", Float) = 0
		_ENABLE_GRADIENT ("_ENABLE_GRADIENT", Range(0, 1)) = 0
		[Toggle] _ENABLE_SHADOWS ("ENABLE_SHADOWS", Float) = 0
		[Toggle] _ENABLE_VC ("ENABLE_VC", Float) = 0
		[Toggle] _Unlit ("Unlit", Float) = 0
		[Toggle(_LIGHTING_ON)] _LIGHTING ("LIGHTING", Float) = 1
		[Toggle] _USE_VC_INTERSECTION ("USE_VC_INTERSECTION", Float) = 0
		_Metallicness ("Metallicness", Range(0, 1)) = 0
		[HideInInspector] _texcoord ("", 2D) = "white" {}
		[HideInInspector] __dirty ("", Float) = 1
	}
	SubShader {
		LOD 200
		Tags { "FORCENOSHADOWCASTING" = "true" "IGNOREPROJECTOR" = "true" "QUEUE" = "Transparent+0" "RenderType" = "Transparent" }
		GrabPass {
			"_BeforeWater"
		}
		Pass {
			Name "FORWARD"
			LOD 200
			Tags { "FORCENOSHADOWCASTING" = "true" "IGNOREPROJECTOR" = "true" "LIGHTMODE" = "FORWARDBASE" "QUEUE" = "Transparent+0" "RenderType" = "Transparent" }
			Blend SrcAlpha OneMinusSrcAlpha, SrcAlpha OneMinusSrcAlpha
			ColorMask RGB -1
			ZWrite Off
			GpuProgramID 31853
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"
			struct v2f
			{
				float4 position : SV_POSITION0;
				float2 texcoord : TEXCOORD0;
				float4 texcoord1 : TEXCOORD1;
				float4 texcoord2 : TEXCOORD2;
				float4 texcoord3 : TEXCOORD3;
				float4 texcoord4 : TEXCOORD4;
				float4 color : COLOR0;
				float4 texcoord5 : TEXCOORD5;
				float4 texcoord9 : TEXCOORD9;
			};
			struct fout
			{
				float4 sv_target : SV_Target0;
			};
			// $Globals ConstantBuffers for Vertex Shader
			float _WaveHeight;
			float _ENABLE_VC;
			float _Worldspacetiling;
			float _WaveSize;
			float _Wavesspeed;
			float4 _WaveDirection;
			float4 _texcoord_ST;
			// $Globals ConstantBuffers for Fragment Shader
			float4 _LightColor0;
			float _EdgeFade;
			float _NormalTiling;
			float4 _RimColor;
			float _USE_VC_INTERSECTION;
			float _Rimfalloff;
			float _Rimtiling;
			float _RimDistortion;
			float _RimSize;
			float _NormalStrength;
			float _Glossiness;
			float _Unlit;
			float _RefractionAmount;
			float4 _WaterShallowColor;
			float4 _WaterColor;
			float _Depth;
			float _ENABLE_GRADIENT;
			float _Transparency;
			float _ReflectionRefraction;
			float _ReflectionStrength;
			float _ReflectionFresnel;
			float _Wavetint;
			float _FoamOpacity;
			float _FoamSize;
			float _FoamDistortion;
			float _FoamTiling;
			float _FoamSpeed;
			float _UseIntersectionFoam;
			float4 _FresnelColor;
			float _Fresnelexponent;
			float _WaveFoam;
			float _ENABLE_SHADOWS;
			// Custom ConstantBuffers for Vertex Shader
			// Custom ConstantBuffers for Fragment Shader
			// Texture params for Vertex Shader
			sampler2D _Shadermap;
			// Texture params for Fragment Shader
			sampler2D _CameraDepthTexture;
			sampler2D _ShadowMask;
			sampler2D _Normals;
			sampler2D _BeforeWater;
			sampler2D _GradientTex;
			sampler2D _ReflectionTex;
			
			// Keywords: DIRECTIONAL
			v2f vert(appdata_full v)
			{
                v2f o;
                float4 tmp0;
                float4 tmp1;
                float4 tmp2;
                float4 tmp3;
                float4 tmp4;
                tmp0.xy = v.vertex.yy * unity_ObjectToWorld._m01_m21;
                tmp0.xy = unity_ObjectToWorld._m00_m20 * v.vertex.xx + tmp0.xy;
                tmp0.xy = unity_ObjectToWorld._m02_m22 * v.vertex.zz + tmp0.xy;
                tmp0.xy = unity_ObjectToWorld._m03_m23 * v.vertex.ww + tmp0.xy;
                tmp0.zw = v.texcoord.xy * float2(-20.0, -20.0);
                tmp1.xy = tmp0.xy * float2(0.1, 0.1) + -tmp0.zw;
                tmp2.xy = tmp0.xy * float2(0.1, 0.1);
                tmp0.xy = _Worldspacetiling.xx * tmp1.xy + tmp0.zw;
                tmp0.xy = tmp0.xy * _WaveSize.xx;
                tmp0.z = _Wavesspeed * _Time.y;
                tmp0.z = tmp0.z * 0.1;
                tmp2.zw = tmp0.zz * _WaveDirection.xz;
                tmp0.zw = tmp2.zw * float2(0.5, 0.5);
                o.texcoord5 = tmp2;
                tmp0.xy = tmp0.xy * float2(0.1, 0.1) + tmp0.zw;
                tmp0.x = tex2Dlod(_Shadermap, float4(tmp0.xy, 0, 1.0));
                tmp0.y = saturate(-_ENABLE_VC * v.color.z + _WaveHeight);
                tmp0.x = tmp0.x * tmp0.y;
                tmp0.xyz = v.normal.xyz * tmp0.xxx + v.vertex.xyz;
                tmp1 = tmp0.yyyy * unity_ObjectToWorld._m01_m11_m21_m31;
                tmp1 = unity_ObjectToWorld._m00_m10_m20_m30 * tmp0.xxxx + tmp1;
                tmp0 = unity_ObjectToWorld._m02_m12_m22_m32 * tmp0.zzzz + tmp1;
                tmp1 = tmp0 + unity_ObjectToWorld._m03_m13_m23_m33;
                tmp0.xyz = unity_ObjectToWorld._m03_m13_m23 * v.vertex.www + tmp0.xyz;
                tmp2 = tmp1.yyyy * unity_MatrixVP._m01_m11_m21_m31;
                tmp2 = unity_MatrixVP._m00_m10_m20_m30 * tmp1.xxxx + tmp2;
                tmp2 = unity_MatrixVP._m02_m12_m22_m32 * tmp1.zzzz + tmp2;
                tmp1 = unity_MatrixVP._m03_m13_m23_m33 * tmp1.wwww + tmp2;
                o.position = tmp1;
                o.texcoord.xy = v.texcoord.xy * _texcoord_ST.xy + _texcoord_ST.zw;
                o.texcoord1.w = tmp0.x;
                tmp2.y = dot(v.normal.xyz, unity_WorldToObject._m00_m10_m20);
                tmp2.z = dot(v.normal.xyz, unity_WorldToObject._m01_m11_m21);
                tmp2.x = dot(v.normal.xyz, unity_WorldToObject._m02_m12_m22);
                tmp0.x = dot(tmp2.xyz, tmp2.xyz);
                tmp0.x = rsqrt(tmp0.x);
                tmp2.xyz = tmp0.xxx * tmp2.xyz;
                tmp3.xyz = v.tangent.yyy * unity_ObjectToWorld._m11_m21_m01;
                tmp3.xyz = unity_ObjectToWorld._m10_m20_m00 * v.tangent.xxx + tmp3.xyz;
                tmp3.xyz = unity_ObjectToWorld._m12_m22_m02 * v.tangent.zzz + tmp3.xyz;
                tmp0.x = dot(tmp3.xyz, tmp3.xyz);
                tmp0.x = rsqrt(tmp0.x);
                tmp3.xyz = tmp0.xxx * tmp3.xyz;
                tmp4.xyz = tmp2.xyz * tmp3.xyz;
                tmp4.xyz = tmp2.zxy * tmp3.yzx + -tmp4.xyz;
                tmp0.x = v.tangent.w * unity_WorldTransformParams.w;
                tmp4.xyz = tmp0.xxx * tmp4.xyz;
                o.texcoord1.y = tmp4.x;
                o.texcoord1.x = tmp3.z;
                o.texcoord1.z = tmp2.y;
                o.texcoord2.w = tmp0.y;
                o.texcoord3.w = tmp0.z;
                o.texcoord2.x = tmp3.x;
                o.texcoord3.x = tmp3.y;
                o.texcoord2.z = tmp2.z;
                o.texcoord3.z = tmp2.x;
                o.texcoord2.y = tmp4.y;
                o.texcoord3.y = tmp4.z;
                tmp0.x = tmp1.y * _ProjectionParams.x;
                tmp0.w = tmp0.x * 0.5;
                tmp0.xz = tmp1.xw * float2(0.5, 0.5);
                o.texcoord4.zw = tmp1.zw;
                o.texcoord4.xy = tmp0.zz + tmp0.xw;
                o.color = v.color;
                o.texcoord9 = float4(0.0, 0.0, 0.0, 0.0);
                return o;
			}
			// Keywords: DIRECTIONAL
			fout frag(v2f inp)
			{
                fout o;
                float4 tmp0;
                float4 tmp1;
                float4 tmp2;
                float4 tmp3;
                float4 tmp4;
                float4 tmp5;
                float4 tmp6;
                tmp0.xy = inp.texcoord.xy * float2(-20.0, -20.0);
                tmp0.zw = -inp.texcoord.xy * float2(-20.0, -20.0) + inp.texcoord5.xy;
                tmp0.xy = _Worldspacetiling.xx * tmp0.zw + tmp0.xy;
                tmp0.zw = tmp0.xy * _NormalTiling.xx;
                tmp0.zw = tmp0.zw * float2(0.5, 0.5) + inp.texcoord5.zw;
                tmp1.xyz = tex2D(_Normals, tmp0.zw);
                tmp1.x = tmp1.z * tmp1.x;
                tmp0.zw = tmp1.xy * float2(2.0, 2.0) + float2(-1.0, -1.0);
                tmp1.x = dot(tmp0.xy, tmp0.xy);
                tmp1.x = min(tmp1.x, 1.0);
                tmp1.x = 1.0 - tmp1.x;
                tmp1.x = sqrt(tmp1.x);
                tmp1.yz = _NormalTiling.xx * tmp0.xy + -inp.texcoord5.zw;
                tmp1.yzw = tex2D(_Normals, tmp1.yz);
                tmp1.y = tmp1.w * tmp1.y;
                tmp1.yz = tmp1.yz * float2(2.0, 2.0) + float2(-1.0, -1.0);
                tmp1.w = dot(tmp1.xy, tmp1.xy);
                tmp2.xy = tmp0.zw + tmp1.yz;
                tmp0.z = min(tmp1.w, 1.0);
                tmp0.z = 1.0 - tmp0.z;
                tmp0.z = sqrt(tmp0.z);
                tmp2.z = tmp1.x * tmp0.z;
                tmp0.z = dot(tmp2.xyz, tmp2.xyz);
                tmp0.z = rsqrt(tmp0.z);
                tmp1.xy = tmp0.zz * tmp2.xy;
                tmp3.x = inp.texcoord4.x;
                tmp0.w = inp.texcoord4.w + 0.0;
                tmp1.z = tmp0.w * 0.5;
                tmp1.w = -tmp0.w * 0.5 + inp.texcoord4.y;
                tmp3.y = -tmp1.w * _ProjectionParams.x + tmp1.z;
                tmp1.zw = tmp3.xy / tmp0.ww;
                tmp3.xyz = inp.texcoord4.zxy / tmp0.www;
                tmp1.xy = _RefractionAmount.xx * tmp1.xy + tmp1.zw;
                tmp4.xyz = tex2D(_BeforeWater, tmp1.xy);
                tmp0.w = tex2D(_CameraDepthTexture, tmp3.yz);
                tmp1.x = _ZBufferParams.z * tmp3.x + _ZBufferParams.w;
                tmp1.x = 1.0 / tmp1.x;
                tmp0.w = _ZBufferParams.z * tmp0.w + _ZBufferParams.w;
                tmp0.w = 1.0 / tmp0.w;
                tmp0.w = tmp0.w - tmp1.x;
                tmp1.x = 1.0 / _ProjectionParams.z;
                tmp1.x = tmp1.x - 1.0;
                tmp1.x = unity_OrthoParams.w * tmp1.x + 1.0;
                tmp0.w = tmp0.w / tmp1.x;
                tmp1.x = saturate(abs(tmp0.w) / _Depth);
                tmp3.xyz = _WaterColor.xyz - _WaterShallowColor.xyz;
                tmp3.xyz = tmp1.xxx * tmp3.xyz + _WaterShallowColor.xyz;
                tmp1.y = 1.0;
                tmp5 = tex2D(_GradientTex, tmp1.xy);
                tmp1.x = 1.0 - tmp1.x;
                tmp5.xyz = tmp5.xyz - tmp3.xyz;
                tmp3.xyz = _ENABLE_GRADIENT.xxx * tmp5.xyz + tmp3.xyz;
                tmp3.xyz = tmp3.xyz - tmp4.xyz;
                tmp1.y = tmp5.w - _WaterShallowColor.w;
                tmp1.y = _ENABLE_GRADIENT * tmp1.y + _WaterShallowColor.w;
                tmp1.y = 1.0 - tmp1.y;
                tmp2.w = -_ENABLE_VC * inp.color.x + 1.0;
                tmp2.w = tmp2.w - abs(tmp0.w);
                tmp2.w = _USE_VC_INTERSECTION * tmp2.w + abs(tmp0.w);
                tmp0.w = saturate(abs(tmp0.w) / _EdgeFade);
                o.sv_target.w = saturate(-_ENABLE_VC * inp.color.y + tmp0.w);
                tmp0.w = tmp2.w / _Rimfalloff;
                tmp2.w = tmp2.w / _RimSize;
                tmp5.xy = tmp0.xy * _Rimtiling.xx;
                tmp5.zw = inp.texcoord5.zw * float2(0.5, 0.5);
                tmp6.xy = tmp0.xy * _WaveSize.xx;
                tmp5.zw = tmp6.xy * float2(0.1, 0.1) + tmp5.zw;
                tmp3.w = tex2D(_Shadermap, tmp5.zw);
                tmp4.w = tmp3.w * _RimDistortion;
                tmp5.xy = tmp5.xy * float2(0.5, 0.5) + tmp4.ww;
                tmp4.w = tex2D(_Shadermap, tmp5.xy);
                tmp5.xy = tmp0.xy * _Rimtiling.xx + inp.texcoord5.zw;
                tmp5.x = tex2D(_Shadermap, tmp5.xy);
                tmp4.w = tmp4.w * tmp5.x;
                tmp0.w = tmp0.w * tmp4.w + tmp2.w;
                tmp0.w = 1.0 - tmp0.w;
                tmp0.w = saturate(tmp0.w * _RimColor.w);
                tmp2.w = tmp0.w + _Transparency;
                tmp1.x = saturate(-tmp1.x * tmp1.y + tmp2.w);
                tmp3.xyz = tmp1.xxx * tmp3.xyz + tmp4.xyz;
                tmp1.x = tmp1.x * _ReflectionStrength;
                tmp4.xy = tmp2.xy * tmp0.zz + tmp3.ww;
                tmp2.xyz = tmp2.xyz * tmp0.zzz + float3(-0.0, -0.0, -1.0);
                tmp4.xy = tmp4.xy * _ReflectionRefraction.xx + tmp1.zw;
                tmp0.z = tex2D(_ShadowMask, tmp1.zw);
                tmp1.yzw = tex2D(_ReflectionTex, tmp4.xy);
                tmp1.yzw = tmp1.yzw - tmp3.xyz;
                tmp4.x = inp.texcoord1.w;
                tmp4.y = inp.texcoord2.w;
                tmp4.z = inp.texcoord3.w;
                tmp4.xyz = _WorldSpaceCameraPos - tmp4.xyz;
                tmp2.w = dot(tmp4.xyz, tmp4.xyz);
                tmp2.w = rsqrt(tmp2.w);
                tmp4.xyz = tmp2.www * tmp4.xyz;
                tmp5.x = inp.texcoord1.z;
                tmp5.y = inp.texcoord2.z;
                tmp5.z = inp.texcoord3.z;
                tmp2.w = dot(tmp5.xyz, tmp4.xyz);
                tmp2.w = 1.0 - tmp2.w;
                tmp2.w = log(tmp2.w);
                tmp2.w = tmp2.w * _ReflectionFresnel;
                tmp2.w = exp(tmp2.w);
                tmp1.x = saturate(tmp1.x * tmp2.w);
                tmp1.xyz = tmp1.xxx * tmp1.yzw + tmp3.xyz;
                tmp1.xyz = -tmp3.www * _Wavetint.xxx + tmp1.xyz;
                tmp3.xyz = _RimColor.xyz * float3(3.0, 3.0, 3.0) + -tmp1.xyz;
                tmp1.xyz = tmp0.www * tmp3.xyz + tmp1.xyz;
                tmp0.w = saturate(tmp0.w + _NormalStrength);
                tmp2.xyz = tmp0.www * tmp2.xyz + float3(0.0, 0.0, 1.0);
                tmp0.w = tmp3.w * _FoamDistortion;
                tmp3.xy = tmp0.xy * float2(0.5, 0.5) + tmp0.ww;
                tmp0.xy = -tmp3.ww * _FoamDistortion.xx + tmp0.xy;
                tmp0.w = tmp3.w * tmp3.w;
                tmp0.w = tmp0.w * _WaveFoam;
                tmp3.zw = inp.texcoord5.zw * _FoamSpeed.xx;
                tmp3.xy = tmp3.xy * _FoamTiling.xx + tmp3.zw;
                tmp0.xy = _FoamTiling.xx * tmp0.xy + tmp3.zw;
                tmp0.xy = tex2D(_Shadermap, tmp0.xy);
                tmp1.w = tex2D(_Shadermap, tmp3.xy);
                tmp0.x = tmp1.w - tmp0.x;
                tmp0.y = 1.0 - tmp0.y;
                tmp0.x = tmp0.x >= _FoamSize;
                tmp1.w = tmp0.x ? -1.0 : -0.0;
                tmp0.x = tmp0.x ? 1.0 : 0.0;
                tmp0.y = tmp0.y + tmp1.w;
                tmp0.x = _UseIntersectionFoam * tmp0.y + tmp0.x;
                tmp1.xyz = _FoamOpacity.xxx * tmp0.xxx + tmp1.xyz;
                tmp0.x = saturate(tmp0.x * tmp0.w);
                tmp3.xyz = _FresnelColor.xyz - tmp1.xyz;
                tmp5.xyz = inp.texcoord2.zzz * unity_WorldToObject._m01_m11_m21;
                tmp5.xyz = unity_WorldToObject._m00_m10_m20 * inp.texcoord1.zzz + tmp5.xyz;
                tmp5.xyz = unity_WorldToObject._m02_m12_m22 * inp.texcoord3.zzz + tmp5.xyz;
                tmp0.y = dot(tmp5.xyz, abs(tmp4.xyz));
                tmp0.y = 1.0 - tmp0.y;
                tmp0.y = log(tmp0.y);
                tmp0.w = _Fresnelexponent * 100.0;
                tmp0.y = tmp0.y * tmp0.w;
                tmp0.y = exp(tmp0.y);
                tmp0.y = saturate(tmp0.y * _FresnelColor.w);
                tmp1.xyz = tmp0.yyy * tmp3.xyz + tmp1.xyz;
                tmp3.xyz = float3(2.0, 2.0, 2.0) - tmp1.xyz;
                tmp0.xyw = tmp0.xxx * tmp3.xyz + tmp1.xyz;
                tmp1.x = 1.0 - tmp0.z;
                tmp1.xyz = saturate(unity_AmbientSky.xyz / tmp1.xxx);
                tmp1.xyz = tmp1.xyz - float3(1.0, 1.0, 1.0);
                tmp1.xyz = _ENABLE_SHADOWS.xxx * tmp1.xyz + float3(1.0, 1.0, 1.0);
                tmp3.xyz = tmp0.xyw * tmp1.xyz;
                tmp3.xyz = tmp3.xyz * _LightColor0.xyz;
                tmp0.xyw = tmp0.xyw * tmp1.xyz + -tmp3.xyz;
                tmp0.xyw = _Unlit.xxx * tmp0.xyw + tmp3.xyz;
                tmp1.x = dot(inp.texcoord1.xyz, tmp2.xyz);
                tmp1.y = dot(inp.texcoord2.xyz, tmp2.xyz);
                tmp1.z = dot(inp.texcoord3.xyz, tmp2.xyz);
                tmp1.w = dot(-tmp4.xyz, tmp1.xyz);
                tmp1.w = tmp1.w + tmp1.w;
                tmp1.xyz = tmp1.xyz * -tmp1.www + -tmp4.xyz;
                tmp1.w = dot(tmp1.xyz, tmp1.xyz);
                tmp1.w = rsqrt(tmp1.w);
                tmp1.xyz = tmp1.www * tmp1.xyz;
                tmp1.w = dot(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz);
                tmp1.w = rsqrt(tmp1.w);
                tmp2.xyz = tmp1.www * _WorldSpaceLightPos0.xyz;
                tmp1.x = dot(tmp2.xyz, tmp1.xyz);
                tmp1.x = max(tmp1.x, 0.0);
                tmp1.x = log(tmp1.x);
                tmp1.y = _Glossiness * 128.0;
                tmp1.x = tmp1.x * tmp1.y;
                tmp1.x = exp(tmp1.x);
                tmp1.x = tmp1.x * _Glossiness;
                o.sv_target.xyz = tmp0.zzz * tmp1.xxx + tmp0.xyw;
                return o;
			}
			ENDCG
		}
	}
	Fallback "Diffuse"
}