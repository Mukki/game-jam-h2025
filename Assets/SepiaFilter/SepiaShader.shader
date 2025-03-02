Shader "Custom/SepiaShader"
{
    Properties
    {
        _SepiaTone ("Sepia Color", Color) = (1.4, 1.1, 0.7, 1.0) // Stronger sepia tone
        _Strength ("Effect Strength", Range(0, 2)) = 1.5 // Extended range for stronger effect
    }
    SubShader
    {
        Tags { "Queue" = "Overlay" "RenderType"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _SepiaTone;
            float _Strength;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);

                // Convert to grayscale (luminance method)
                float gray = dot(col.rgb, float3(0.3, 0.59, 0.11));

                // Stronger sepia tone by exaggerating the effect
                float3 sepiaColor = gray.xxx * _SepiaTone.rgb * 1.5; // Boost the sepia intensity

                // Apply effect with an adjustable strength factor
                col.rgb = lerp(col.rgb, sepiaColor, saturate(_Strength));

                return col;
            }
            ENDCG
        }
    }
}