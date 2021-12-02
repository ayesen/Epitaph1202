Shader "examples/week 10/posterized color replace"
{
    Properties 
    {
        _MainTex ("render texture", 2D) = "white"{}
        _steps ("steps", Range(1, 16)) = 16
        _recolor ("_recolor referece", 2D) = "gray"{}
    }
    SubShader
    {
        Cull Off
        ZWrite Off
        ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            
            sampler2D _MainTex;
            sampler2D _recolor;
            float _steps;

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

            Interpolators vert (MeshData v)
            {
                Interpolators o;

                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;

                return o;
            }

            float4 frag (Interpolators i) : SV_Target
            {
                float3 color = 0;
                float2 uv = i.uv;
                float3 grayscaleReference = float3(0.299, 0.587, 0.144);
                color = tex2D(_MainTex, uv);
                float grayscale = dot(color, grayscaleReference);
                grayscale = floor(grayscale * _steps) / _steps;

                float3 recolor = tex2D(_recolor, float2(grayscale, 0.5));

                color = floor(color * _steps) / _steps;

                return float4(color, 1.0);
            }
            ENDCG
        }
    }
}
