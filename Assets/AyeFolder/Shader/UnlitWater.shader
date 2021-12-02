Shader "Unlit/UnlitWater"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _color("color", Color) = (1, 1, 1, 1)
        _displacement("displacement", Range(0, 0.3)) = 0.05
        _scale("noise scale", Range(2, 100)) = 15.5
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float3 _color;
            float _scale;
            float _displacement;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
                float3 color : COLOR;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 normal : NORMAL;
            };

            float wave(float2 uv) {
                float wave1 = sin(((uv.x + uv.y) * _scale) + _Time.z) * 0.5 + 0.5;

                float wave2 = (cos(((uv.x - uv.y) * _scale / 2.568) + _Time.z) + 1) * sin(_Time.x * 5.2321 + (uv.x * uv.y)) * 0.5 + 0.5;

                return (wave1 + wave2) / 3;
            }

            v2f vert (appdata v)
            {
                v2f o;
                float3 worldPos = mul(unity_ObjectToWorld, v.vertex);
                float2 worldUV = worldPos.xz * 0.02;
                float displacement = wave(worldUV) * _displacement;
                o.uv = displacement;
                v.vertex.y += o.uv * v.color;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float3 color;
                
                fixed4 col = tex2D(_MainTex, i.uv);
                return col;
            }
            ENDCG
        }
    }
}
