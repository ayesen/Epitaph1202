Shader "Unlit/PixelationChange"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _PixelSize("PixelSize", Range(0.001, 0.1)) = 0.1
        _FadeScale("FadeScale", float) = 1
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

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _PixelSize;
            float _FadeScale;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {

                fixed4 col;
                float ratioX = (int)(i.uv.x / _PixelSize + 0.5) * _PixelSize;
                float ratioY = (int)(i.uv.y / _PixelSize + 0.5) * _PixelSize;

                col = tex2D(_MainTex, float2(ratioX, ratioY));
                col = lerp(col, fixed4(0.09019608, 0.1254902, 0.1294118, 1), _PixelSize * _FadeScale);
                

                return col;
            }
            ENDCG
        }
    }
}
