Shader "Custom/EndLevelShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "black" {}
        _SecondaryTex("Texture", 2D) = "white" {}
        _Color("Color", Color) = (1, 1, 1, 1)
    }
    SubShader
    {

        //Tags {"Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent"}
        //ZWrite Off
        //Blend SrcAlpha OneMinusSrcAlpha
        //Cull front

        Tags { "RenderType" = "Transparent" "Queue" = "Transparent" }

        //Tags { "RenderType"="Opaque" }
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
                //v2f o;
                //o.vertex = UnityObjectToClipPos(v.vertex);
                //o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                //UNITY_TRANSFER_FOG(o,o.vertex);
                //return o;
                v.vertex.x = v.vertex.x * ((sin(_Time[1]) + 3) * 0.4);
                v.vertex.y = v.vertex.y * ((sin(_Time[1]) + 3) * 0.4);
                v.vertex.z = v.vertex.z + 0.2;

                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                //o.pos = UnityObjectToClipPos(v.vertex);

                //float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;

                o.uv = TRANSFORM_TEX(v.uv, _MainTex);

                //o.vertex.y -= sin(_Time[1]);
                //o.vertex.x += cos(_Time[1]);
                //o.vertex.y -= _SinTime;

                //o.vertex.x += sin(worldPos.y+_Time.w);
                //o.vertex.x += sin(worldPos.z + _Time.w);
                //o.vertex.x += sin(worldPos.y + _Time.w) + cos(worldPos.z + _Time.w);
                //o.vertex.z += cos(worldPos.z + _Time.w);
                //o.vertex.x = o.vertex.x + sin(_Time[1] / 10);
                //o.vertex.x += ;
                //o = v.vertex + 2.0 * cross(quaternion.xyz, cross(quaternion.xyz, ray) + quaternion.w * v.vertex);
                //o.vertex.z += 2;
                //o.vertex.x = o.vertex.x * ((sin(_Time[1])+3)*0.25);
                //o.vertex.y = o.vertex.y * ((sin(_Time[1]) + 3) * 0.25);


                //o.vertex.x = worldPos.z;
                //o.vertex.x = worldPos.x+((sin(_Time[1]) + 3) * 0.25);

                //o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                //UNITY_TRANSFER_FOG(o, o.vertex);
                return o;


            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 firstCol = tex2D(_MainTex, i.uv);
                // apply fog
                //UNITY_APPLY_FOG(i.fogCoord, col);
                fixed4 secondCol = tex2D(_MainTex, i.uv);
                //fixed4 col = firstCol + secondCol;
                fixed4 col = lerp(tex2D(_MainTex, i.uv), tex2D(_SecondaryTex, i.uv), 0.5*sin(5*_Time[1])+0.5)*_Color;
                //col = tex2D(_MainTex, i.uv);
                col.a = 0;
               
                if (col.r < 0.1 && col.g<0.1 && col.b<0.1)
                    discard;

                return col;
            }
            ENDCG
        }
    }
}
