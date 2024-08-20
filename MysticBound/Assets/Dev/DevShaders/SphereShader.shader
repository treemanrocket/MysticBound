// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/SphereShader" 
{  
    Properties
    {
        _Tint ("Tint", Color) = (1,1,1,1)
    }
    
    SubShader 
    {
        Pass 
        {
            CGPROGRAM

            #pragma vertex Vtx
            #pragma fragment Frag

            #include "UnityCG.cginc"

            float4 _Tint;

            struct Interpolators
            {
                float4 position : SV_POSITION;
                float3 localPosition : TEXCOORD0;
            };

            Interpolators Vtx(float4 position : POSITION)
            {
                Interpolators i;
                i.localPosition = position.xyz;
                i.position = UnityObjectToClipPos(position);
                return i;
            }

            float4 Frag(Interpolators i) : SV_TARGET
            {
                return float4(i.localPosition + 0.5, 1) * _Tint;
            }

            ENDCG
        }
    }
}