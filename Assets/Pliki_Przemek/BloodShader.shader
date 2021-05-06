Shader "Custom/BloodShader"
{
    Properties
    {
        _MainTex ("Main Texture", 2D) = "white" {} //pierwsza tekstura, pozostawiona domyslna, kolor bialy
        _BloodTex ("Blood Texture", 2D) = "white" {} //druga tekstura
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always //cull off - render all polygons, ZWrite Off - używać dla semitransparent

        Pass
        {
            CGPROGRAM
            #pragma vertex vert //shader wierczhołków
            #pragma fragment frag //shader frangemntów

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;
            sampler2D _BloodTex;

            fixed4 frag(v2f IN) : SV_Target
            {
                float4 col = tex2D(_MainTex, IN.uv); //pierwsza tekstura
                float4 secondCol = tex2D(_BloodTex, IN.uv); //druga tekstura
                col.r = lerp(col, 1 - secondCol, 0.5) + 0.2*(sin(5*_Time[1])+2); //nałożenie pierwszej tektury na drugą na kanale red + sinusoidalna intensywność składowej
                return col;
            }
            ENDCG
        }
    }
}
