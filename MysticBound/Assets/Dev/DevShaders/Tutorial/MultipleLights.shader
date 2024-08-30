// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/MultipleLights" 
{  
    Properties
    {
        _Tint ("Tint", Color) = (1,1,1,1)
        _MainTex ("Albedo", 2D) = "white" {}
        [Gamma] _Metallic ("Metallic", Range(0,1)) = 0
        _Smoothness ("Smoothness", Range(0,1)) = 0.5
    }
    
    SubShader 
    {
        Pass 
        {
            Tags
            {
                "LightMode" = "UniversalForward"
            }

            CGPROGRAM

            #pragma target 3.0
            
            #pragma vertex Vtx
            #pragma fragment Frag

            #include "Lighting.cginc"

            ENDCG
        }
        
        Pass 
        {
            Tags
            {
                "LightMode" = "UniversalForward"
            }

            CGPROGRAM

            #pragma target 3.0
            
            #pragma vertex Vtx
            #pragma fragment Frag

            #include "Lighting.cginc"

            ENDCG
        }
    }
}