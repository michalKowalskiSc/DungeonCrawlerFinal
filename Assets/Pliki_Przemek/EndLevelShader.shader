Shader "Custom/EndLevelShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "black" {} //tekstura nr 1, pozostawiona domyslna = czarna
        _SecondaryTex("Texture", 2D) = "white" {} //nalozona tekstura z portalem
        _Color("Color", Color) = (1, 1, 1, 1) //kolor bialy
    }
    SubShader
    {

        Tags { "RenderType" = "Transparent" "Queue" = "Transparent" } //transparentny

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

            sampler2D _MainTex;
            float4 _MainTex_ST;
            sampler2D _SecondaryTex;
            float4 _SecondaryTex_ST;
            float4 _Color;

            v2f vert (appdata v)
            {
                v.vertex.x = v.vertex.x * ((sin(_Time[1]) + 3) * 0.4); //wspolrzedna x zmienia sie sinusoidalnie w pewnym zakresie - wszpolczynniki dobrane eksperymentalnie
                v.vertex.y = v.vertex.y * ((sin(_Time[1]) + 3) * 0.4); //wspolrzedna y zmienia sie sinusoidalnie w pewnym zakresie - wszpolczynniki dobrane eksperymentalnie
                //razem daje efekt pulsowania
                
                v.vertex.z = v.vertex.z + 0.2; //przesuniecie wszystkich wierzcholkow 'przed' obiekt

                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex); //okreslenie pozycji

                o.uv = TRANSFORM_TEX(v.uv, _MainTex); //nalozenie tekstury

                //o.vertex.x = o.vertex.x * ((sin(_Time[1])+3)*0.25);
                //o.vertex.y = o.vertex.y * ((sin(_Time[1]) + 3) * 0.25);
                //to nie zadziala bo zly uklad wspolrzednych

                return o; //zwroc wierczholek
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 firstCol = tex2D(_MainTex, i.uv); //peirwszy tekstura
                // apply fog
                //UNITY_APPLY_FOG(i.fogCoord, col);
                fixed4 secondCol = tex2D(_MainTex, i.uv); //druga tekstura
                fixed4 col = lerp(tex2D(_MainTex, i.uv), tex2D(_SecondaryTex, i.uv), 0.5*sin(5*_Time[1])+0.5)*_Color; //nalozenie obu tektur i mieszanie (lerp) w proporcjach zaleznych od czasu (pulsuje)
                //mieszanie czarnego z tektura portalu pozwoli latwo 'wylaczyc' kolor czarny poniżej
                //col = tex2D(_MainTex, i.uv);
                col.a = 0; //kanal alfa =0
               
                if (col.r < 0.1 && col.g<0.1 && col.b<0.1) //jesli kolor czarny lub bardzo zblizony
                    discard; //nie nakladaj koloru na ten piksel

                return col; //zwroc kolor piksela
            }
            ENDCG
        }
    }
}
