Shader "Custom/WigglyPulsatingBorder"
{
    Properties
    {
        _BorderSize ("Base Border Size", Range(0.05, 0.8)) = 0.15
        _WaveSpeed ("Wave Speed", Range(0, 5)) = 1.0
        _WaveStrength ("Wave Strength", Range(0, 0.1)) = 0.05
        _WaveFrequency ("Wave Frequency", Range(1, 10)) = 2.0
        _NoiseScale ("Noise Scale", Range(1, 20)) = 5.0
        _NoiseStrength ("Noise Strength", Range(0, 0.2)) = 0.05
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

            float _BorderSize;
            float _WaveSpeed;
            float _WaveStrength;
            float _WaveFrequency;
            float _NoiseScale;
            float _NoiseStrength;

            // Simple pseudo-random noise function
            float random(float2 uv)
            {
                return frac(sin(dot(uv, float2(12.9898, 78.233))) * 43758.5453);
            }

            // Smooth noise function
            float smoothNoise(float2 uv)
            {
                float2 i = floor(uv);
                float2 f = frac(uv);
                f = f * f * (3.0 - 2.0 * f); // Smoothstep interpolation

                float a = random(i);
                float b = random(i + float2(1.0, 0.0));
                float c = random(i + float2(0.0, 1.0));
                float d = random(i + float2(1.0, 1.0));

                return lerp(lerp(a, b, f.x), lerp(c, d, f.x), f.y);
            }

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Compute sine wave for smooth thickness variation
                float sineWave = sin(_Time.y * _WaveSpeed * _WaveFrequency) * _WaveStrength;

                // Generate noise-based distortion
                float noise = smoothNoise(i.uv * _NoiseScale + _Time.y) * 2.0 - 1.0;
                float noiseOffset = noise * _NoiseStrength;

                // Dynamic border thickness with sine wave and noise
                float dynamicBorder = _BorderSize + sineWave + noiseOffset;
                
                // Compute border effect based on distance to edge
                float dist = min(i.uv.x, min(i.uv.y, min(1.0 - i.uv.x, 1.0 - i.uv.y)));
                float alpha = smoothstep(dynamicBorder, dynamicBorder + 0.05, dist);

                return fixed4(0, 0, 0, 1.0 - alpha); // Black border with transparency fading inward
            }
            ENDCG
        }
    }
}