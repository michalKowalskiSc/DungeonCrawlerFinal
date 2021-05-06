Shader "Custom/SurfaceShaderShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1) //kolor
        _MainTex ("Albedo (RGB)", 2D) = "gray" {} //glowna tekstura, odbijanie
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        _Detail("Detail", 2D) = "gray" {} //podpieta tekstura z paskami
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        //#pragma surface surf Standard fullforwardshadows
        #pragma surface surf Lambert

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
            float4 screenPos;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        //sampler2D _MainTex;
        sampler2D _Detail;

        void surf (Input IN, inout SurfaceOutput o)
        {
            /*
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
            */
            //IN.screenPos.x = IN.screenPos.x + 1;
            //o.Metallic = _Metallic;
            //o.Smoothness = _Glossiness;
            //_Detail.ti
            
            o.Albedo = tex2D(_MainTex, IN.uv_MainTex).rgb;
            float2 screenUV = IN.screenPos.xy / IN.screenPos.w; //zmapuj teksture
            screenUV *= float2(4, 4); //wspolczynniki zmapowania
            o.Albedo *= tex2D(_Detail, screenUV).rgb * 2;
        }
        ENDCG
    }
    FallBack "Diffuse" //jesli karta nie obsluzy
}
