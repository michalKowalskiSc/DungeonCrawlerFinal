Shader "Custom/Shiny"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _SecondaryTex("Texture", 2D) = "white" {}
        //_GoldColor("Gold", Gold) = (1, 1, 0, 1)
        _Color("Color", Color) = (1, 1, 1, 1)
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
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                //UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            float random(float2 uv)
            {
                return frac(sin(dot(uv, float2(12.9898, 78.233))) * 43758.5453123/_Time[0]);
            }

            float random2(float2 uv)
            {
                return frac(sin(dot(uv, float2(12.9898, 78.233))) * 43758.5453123);
            }

            sampler2D _MainTex;
            float4 _MainTex_ST;
            sampler2D _SecondaryTex;
            float4 _SecondaryTex_ST;
            float4 _Color;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                //UNITY_TRANSFER_FOG(o,o.vertex);
                

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 firstCol = tex2D(_MainTex, i.uv);
                // apply fog
                //UNITY_APPLY_FOG(i.fogCoord, col);
                fixed4 secondCol = tex2D(_MainTex, i.uv);
                //fixed4 col = firstCol + secondCol;
                //fixed4 col = lerp(tex2D(_MainTex, i.uv), tex2D(_SecondaryTex, i.uv), 0.5 * sin(5 * _Time[1]) + 0.5) * _Color;
                // sample the texture
                //fixed4 col = tex2D(_MainTex, i.uv);
                // apply fog
                //UNITY_APPLY_FOG(i.fogCoord, col);
                //col = tex2D(_SecondaryTex, i.uv);


                float k = random(i.uv);
                float m = random2(i.uv);



                if ((k+m)>0.04)
                    discard;

                fixed4 col;

                col.r = 1;
                col.g = 0.84;
                float l = random2(i.uv);
                col.b = l/5+0.2;


                return col;
            }
            ENDCG
        }
    }
}
