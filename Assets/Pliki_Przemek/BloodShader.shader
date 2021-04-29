Shader "Custom/BloodShader"
{
    Properties
    {
        _MainTex ("Main Texture", 2D) = "white" {}
        _BloodTex ("Blood Texture", 2D) = "white" {}
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

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
                //fixed4 col = tex2D(_MainTex, IN.uv + float2(0,sin(IN.vertex.x/50 + _Time[1])/10));
                // just invert the colors
                //col.rgb = 1 - col.rgb;
                //col.r = 1;
                //col.rgb = 1 - col.rgb;


                float4 col = tex2D(_MainTex, IN.uv);
                float4 secondCol = tex2D(_BloodTex, IN.uv);
                //col.r = IN.uv;
                col.r = lerp(col, 1 - secondCol, 0.5) + 0.2*(sin(5*_Time[1])+2);
                //col.r = 0;
                //col.g = col.g * 2;
                //col.b = col.b * 2;
                return col;
            }
            ENDCG
        }
    }
}
