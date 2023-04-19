Shader ".Loli_Explorer.exe/AuraFXShader"
{
	Properties
	{
		_Cutoff( "Mask Clip Value", Float ) = -0.13
		[HDR]_OutterFlameColor("Outter Flame Color", Color) = (0.04313726,0.4509804,1,0)
		[HDR]_InnerFlameColor("Inner Flame Color", Color) = (0.04313726,0.4509804,1,0)
		_AuraSize("Aura Size", Range( 0 , 0.1)) = 0.027
		_NoiseTexture("Noise Texture", 2D) = "white" {}
		_NoiseScaleX("Noise Scale X", Float) = 2
		_NoiseScaleY("Noise Scale Y", Float) = 2
		_XSpeed("X Speed", Range( -1 , 1)) = 0
		_YSpeed("Y Speed", Range( -1 , 1)) = 0
		_OutterFlame("Outter Flame", Range( 0 , 1)) = 0
		_AuraBlur("Aura Blur", Range( 1.8 , 5)) = 3.8
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "TransparentCutout"  "Queue" = "Transparent+1" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Front
		ZWrite Off
		Stencil
		{
			Ref 15
			Comp NotEqual
		}
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Unlit keepalpha noshadow vertex:vertexDataFunc 
		struct Input
		{
			float3 viewDir;
			float3 worldPos;
			float3 worldNormal;
		};

		uniform float _AuraSize;
		uniform float4 _InnerFlameColor;
		uniform sampler2D _NoiseTexture;
		uniform float _XSpeed;
		uniform float _YSpeed;
		uniform float _NoiseScaleX;
		uniform float _NoiseScaleY;
		uniform float _AuraBlur;
		uniform float _OutterFlame;
		uniform float4 _OutterFlameColor;
		uniform float _Cutoff = -0.13;

		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float3 ase_vertexNormal = v.normal.xyz;
			float3 AuraSize427 = ( ase_vertexNormal * _AuraSize );
			v.vertex.xyz += AuraSize427;
		}

		inline half4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return half4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			float2 appendResult434 = (float2(_XSpeed , _YSpeed));
			float2 appendResult438 = (float2(_NoiseScaleX , _NoiseScaleY));
			float2 panner370 = ( _Time.y * appendResult434 + (i.viewDir*float3( appendResult438 ,  0.0 ) + 0.0).xy);
			float4 tex2DNode369 = tex2D( _NoiseTexture, panner370 );
			float3 ase_worldPos = i.worldPos;
			float3 ase_worldViewDir = normalize( UnityWorldSpaceViewDir( ase_worldPos ) );
			float3 ase_worldNormal = i.worldNormal;
			float fresnelNdotV376 = dot( ase_worldNormal, ase_worldViewDir );
			float fresnelNode376 = ( 0.0 + _AuraBlur * pow( 1.0 - fresnelNdotV376, -2.0 ) );
			float temp_output_377_0 = ( 1.0 - fresnelNode376 );
			float Rimlight429 = ( tex2DNode369.r + temp_output_377_0 );
			float4 FinalColor432 = ( ( _InnerFlameColor *  ( Rimlight429 - 0.0 > _OutterFlame ? 1.0 : Rimlight429 - 0.0 <= _OutterFlame && Rimlight429 + 0.0 >= _OutterFlame ? 0.0 : 0.0 )  ) + ( _OutterFlameColor *  ( Rimlight429 - 0.0 > _OutterFlame ? 0.0 : Rimlight429 - 0.0 <= _OutterFlame && Rimlight429 + 0.0 >= _OutterFlame ? 0.0 : 1.0 )  ) );
			o.Emission = FinalColor432.rgb;
			o.Alpha = 1;
			float Opacity425 = ( tex2DNode369.r + temp_output_377_0 );
			clip( Opacity425 - _Cutoff );
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}