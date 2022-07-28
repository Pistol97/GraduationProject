// Upgrade NOTE: upgraded instancing buffer 'Props' to new syntax.

//------------------------------------------------------------------------
//All Access Assets
//Shader -- HeightBaedVertexBlend
//
//Function to blend a alpha mask with vertex colors.
//
//By:Steve Coyle
//Created: 8/22/2017
//------------------------------------------------------------------------

Shader "AllAccessAssets/SH_HeightBasedVertexBlend" {
	Properties {
		_Color ("Color 01", Color) = (1,1,1,1)
		_MainTex ("Albedo 01 (RGB) Smooth (A)", 2D) = "white" {}
		_BumpMap ("Normal Map 01",2D) = "bump" {}
		[Gamma] _Metallic("Metallic 01", Range(0.0, 1.0)) = 0.0
		_Glossiness("Smoothness 01", Range(0,1)) = 0.5
		
		_Color2("Color 02", Color) = (1,1,1,1)
		_MainTex2("Albedo 02 (RGB) Smooth (A)", 2D) = "white" {}
		_BumpMap2("Normal Map 02",2D) = "bump" {}
		[Gamma] _Metallic2("Metallic 02", Range(0.0, 1.0)) = 0.0
		_Glossiness2("Smoothness 02", Range(0,1)) = 0.5

		[KeywordEnum(Red,Green,Blue,Alpha)] _Channel("Vertex Channel", Float) = 0
		_HeightMap("Height Map 01",2D) = "white" {}
		_Sharpness("Sharpness", Range(0,1)) = 0.1
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		#pragma multi_compile _CHANNEL_RED _CHANNEL_BLUE _CHANNEL_GREEN CHANNEL_ALPHA

		sampler2D _MainTex;
		sampler2D _BumpMap;
		sampler2D _MainTex2;
		sampler2D _BumpMap2;
		sampler2D _HeightMap;
		struct Input {
			float2 uv_MainTex;
			float2 uv_BumpMap;
			float2 uv_MainTex2;
			float2 uv_BumpMap2;
			float2 uv_HeightMap;
			fixed4 vColor : COLOR;
		};

		half _Glossiness;
		half _Metallic;
		half _Glossiness2;
		half _Metallic2;
		half _Sharpness;
		fixed4 _Color;
		fixed4 _Color2;

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_BUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_BUFFER_END(Props)

		float BlendAlphaVertex(float alpha, float HeightMap,float ModMask)
		{
			//float temp = 2.5 

			float ModulationMask = HeightMap + ModMask;
			fixed result = clamp(((alpha * 2.5) - ModulationMask) / (1 - _Sharpness), 0, 1);
			return result;
		}

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			fixed4 c1 = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			float3 n1 = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
			float metallic1 = _Metallic;
			float smoothness1 = _Glossiness;

			fixed4 c2 = tex2D(_MainTex2, IN.uv_MainTex2) * _Color2;
			float3 n2 = UnpackNormal(tex2D(_BumpMap2, IN.uv_BumpMap2));
			float metallic2 = _Metallic2;
			float smoothness2 = _Glossiness2;
			fixed4 hm = tex2D(_HeightMap, IN.uv_HeightMap);
			float alpha = .5f;

#ifdef _CHANNEL_RED
			alpha = BlendAlphaVertex(IN.vColor.r, hm.r, hm.g);
#endif
#ifdef _CHANNEL_GREEN
			alpha = BlendAlphaVertex(IN.vColor.g, hm.r, hm.g);
#endif
#ifdef _CHANNEL_BLUE
			alpha = BlendAlphaVertex(IN.vColor.b, hm.r, hm.g);
#endif
#ifdef _CHANNEL_ALPHA
			alpha = BlendAlphaVertex(IN.vColor.a, hm.r, hm.g);
#endif

			o.Normal = lerp(n2, n1, alpha);
			o.Albedo = lerp(c2.rgb, c1.rgb, alpha);
			// Metallic and smoothness come from slider variables
			o.Metallic = lerp(_Metallic2, _Metallic, alpha);
			o.Smoothness = lerp(_Glossiness2 *c2.a, _Glossiness*c1.a, alpha);
			o.Alpha = 1.0f;
		}
		ENDCG
	}

			FallBack "Diffuse"

}
