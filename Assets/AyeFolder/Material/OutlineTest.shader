Shader "week19/hw/Outline"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_thickness ("thickness", Float) = 1
		_color ("color", Color) = (1, 1, 1, 1)
	}

		SubShader
	{
		Tags
		{
			"RenderType" = "Opaque"
		}

		Pass
		{
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag

			#include "UnityCG.cginc"

			sampler2D _MainTex;
			float2 _MainTex_TexelSize;
			float _thickness;
			float3 _color;

			//I have been looking for a bunch of outline shaders online and studying how to create them. 
			//This is a function I found from https://github.com/daniel-ilett/smo-shaders/blob/master/Assets/Shaders/Neon.shader
			float3 sobel(float2 uv)
			{
				float x = 0;
				float y = 0;

				float2 texelSize = _MainTex_TexelSize;

				x += tex2D(_MainTex, uv + float2(-texelSize.x, -texelSize.y)) * -1.0;
				x += tex2D(_MainTex, uv + float2(-texelSize.x,            0)) * -(1 + _thickness);
				x += tex2D(_MainTex, uv + float2(-texelSize.x,  texelSize.y)) * -1.0;

				x += tex2D(_MainTex, uv + float2(texelSize.x, -texelSize.y)) * 1.0;
				x += tex2D(_MainTex, uv + float2(texelSize.x,            0)) * (1 + _thickness);
				x += tex2D(_MainTex, uv + float2(texelSize.x,  texelSize.y)) * 1.0;

				y += tex2D(_MainTex, uv + float2(-texelSize.x, -texelSize.y)) * -1.0;
				y += tex2D(_MainTex, uv + float2(0, -texelSize.y)) * -(1 + _thickness);
				y += tex2D(_MainTex, uv + float2(texelSize.x, -texelSize.y)) * -1.0;

				y += tex2D(_MainTex, uv + float2(-texelSize.x,  texelSize.y)) * 1.0;
				y += tex2D(_MainTex, uv + float2(0,  texelSize.y)) * (1 + _thickness);
				y += tex2D(_MainTex, uv + float2(texelSize.x,  texelSize.y)) * 1.0;

				return sqrt(x * x + y * y);
			}

			struct MeshData
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct Interpolators
			{
				float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD0;
			};

			Interpolators vert(MeshData v)
			{
				Interpolators o;

				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;

				return o;
			}

			float4 frag(Interpolators i) : SV_Target
			{
				float3 s = -sobel(i.uv) * _color;
				float3 color = tex2D(_MainTex, i.uv);


				return float4(color +s, 1.0);
			}
			ENDCG
		}
	}
}
