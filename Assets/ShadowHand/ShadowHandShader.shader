Shader "Custom/ShadowHand"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _NoiseTex ("Noise Texture", 2D) = "white" {}
        _DissolveAmount ("Dissolve", Range(0,1)) = 0.0
        _EdgeColor ("Edge Color", Color) = (0,0,0,1)
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        Cull Off
        
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
            sampler2D _NoiseTex;
            float _DissolveAmount;
            fixed4 _EdgeColor;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 handTex = tex2D(_MainTex, i.uv);
                float noiseValue = tex2D(_NoiseTex, i.uv).r;

                // Dissolve effect
                float dissolveMask = step(_DissolveAmount, noiseValue);

                // Edge effect (smoky edges)
                float edge = smoothstep(_DissolveAmount - 0.1, _DissolveAmount, noiseValue);
                fixed4 finalColor = lerp(_EdgeColor, handTex, edge);

                finalColor.a *= dissolveMask; // Only show parts above dissolve threshold
                return finalColor;
            }
            ENDCG
        }
    }
}