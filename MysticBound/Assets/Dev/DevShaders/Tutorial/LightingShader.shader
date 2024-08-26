// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/LightingShader" 
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

            #pragma vertex Vtx
            #pragma fragment Frag

            #include "UnityStandardBRDF.cginc"
            #include "UnityStandardUtils.cginc"

            float4 _Tint;
            sampler2D _MainTex;
            float4 _MainTex_ST;

            float _Metallic;
            float _Smoothness;

            struct Interpolators
            {
                float4 position : SV_POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : TEXCOORD1;
                float3 worldPos : TEXCOORD2;
            };

            struct VertexData
            {
                float4 position : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
            };

            Interpolators Vtx(VertexData v)
            {
                Interpolators i;

                i.position = UnityObjectToClipPos(v.position);
                i.worldPos = mul(unity_ObjectToWorld, v.position);

                i.uv = TRANSFORM_TEX(v.uv, _MainTex);

                i.normal = UnityObjectToWorldNormal(v.normal);
                return i;
            }

            float4 Frag(Interpolators i) : SV_TARGET
            {
                i.normal = normalize(i.normal);

                float3 lightDir = _WorldSpaceLightPos0.xyz;
                float3 lightColor = _LightColor0.rgb;

                float3 viewDir = normalize(_WorldSpaceCameraPos - i.worldPos);
                float3 halfVector = normalize(lightDir + viewDir);
                
                float3 albedo = tex2D(_MainTex, i.uv) * _Tint;
                float3 specularTint;
                float oneMinusReflectivity;
                albedo = DiffuseAndSpecularFromMetallic(albedo, _Metallic, specularTint, oneMinusReflectivity);
                
                float3 diffuse = albedo * lightColor * DotClamped(lightDir, i.normal);
                float3 specular = pow(DotClamped(halfVector, i.normal), _Smoothness * 100) * lightColor * specularTint;

                return float4(diffuse + specular, 1);
            }

            ENDCG
        }
    }
}