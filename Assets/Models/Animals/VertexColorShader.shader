Shader "Custom/VertexColor"
{
    SubShader
    {
        Tags { "Queue"="Geometry" }
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float4 color : COLOR; // Vertex color input
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float4 color : COLOR;
            };

            v2f vert(appdata_t v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.color = v.color; // Pass vertex color to fragment shader
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                return i.color; // Use vertex color
            }
            ENDCG
        }
    }
}