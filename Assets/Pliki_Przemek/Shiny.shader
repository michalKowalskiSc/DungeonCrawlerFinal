Shader "Custom/Shiny"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {} //pierwsza tekstura - pozostawiona domyslna, white
        _SecondaryTex("Texture", 2D) = "white" {} //druga tektura, nalozony kolor zloty
        _Color("Color", Color) = (1, 1, 1, 1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" } //nieprzezroczysty
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
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

            float random(float2 uv) //zwraca zmienna pseudolosowa uzalezniona od czasu
            {
                return frac(sin(dot(uv, float2(12.9898, 78.233))) * 43758.5453123/_Time[0]);
            }

            float random2(float2 uv) //zwraca zmienna pseudolosowa
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
                fixed4 firstCol = tex2D(_MainTex, i.uv); //pierwsza tekstura - ostatecznie nie uzyty
                fixed4 secondCol = tex2D(_MainTex, i.uv); //druga tekstura - ostatczenie nie uzyty

                float k = random(i.uv); //losowanie pierwszej liczby
                float m = random2(i.uv); //losowanie drugiej liczby

                if ((k+m)>0.008) //jesli suma dwoch zmiennych losowych jest wieksza od X - wartosc X dobrana eksperymentalnie
                    discard; //wylacz naklanie koloru na ten piksel

                fixed4 col; //kolor ktory zostanie zwrocony
                
                col.r = 1; //skladowa r na 100%
                col.g = 0.84; //skladowa g na 84% (~214)
                float l = random2(i.uv); //losujemy trzecia liczbe
                col.b = l/5+0.2; //skladowa b losowa (rozne odcienie zlotego) - wspolczynniki dobrane eksperymentalnie

                return col; //zwroc kolor (naloz na piksel)
            }
            ENDCG
        }
    }
}
