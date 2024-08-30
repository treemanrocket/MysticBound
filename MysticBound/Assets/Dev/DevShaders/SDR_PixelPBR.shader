Shader "Custom/SDR_PixelPBR"
{
    Properties
    {
        [Header(Albedo)] [Space]
        [MainTexture] _MainTex ("Base Color", 2D) = "white" {}
        _BaseColor ("Tint", Color) = (1,1,1,1)
        
        [Space(5)]
        [Header(Metallic Smoothness)] [Space]
        _Metallic ("Metallic", Range(0,1)) = 0
        [Toggle(_METALLICMAP)] _MetallicMapToggle ("Use Metallic Map", float) = 0
        [NoScaleOffset] _MetallicMap ("Metallic Map", 2D) = "black" {}
        _Smoothness ("Smoothness", Range(0,1)) = 0
        
        [Space(5)]
        [Header(Alpha Clipping)] [Space]
        [Toggle(_ALPHATEST_ON)] _AlphaTestToggle ("Use Alpha Clipping", float) = 0
        _Cutoff ("Alpha Cutoff", Range(0,1)) = 0

        [Space(5)]
        [Header(Normal)] [Space]
        [Toggle(_NORMALMAP)] _BumpMapToggle ("Use Normal Map", float) = 0
        [NoScaleOffset] _BumpMap ("Normal Map", 2D) = "bump" {}
        _NormalScale ("Normal Scale", float) = 1

        [Space(5)]
        [Header(Occlusion)] [Space]
        [Toggle(_OCCLUSIONMAP)] _OcclusionMapToggle ("Use Occlusion Map", float) = 0
        [NoScaleOffset] _OcclusionMap ("Occlusion Map", 2D) = "black" {}
        _OcclusionStrength ("Occlusion Strength", Range(0,1)) = 1

        [Space(5)]
        [Header(Emission)] [Space]
        [Toggle(_EMISSION)] _EmissionToggle ("Emission", float) = 0
        [HDR] _EmissionColor ("Emissive Color", Color) = (0,0,0)
        [NoScaleOffset] _EmissionMap ("Emissive Map", 2D) = "black" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "RenderPipeline"="UniversalRenderPipeline"}

        HLSLINCLUDE
        //Material keywords
        #pragma shader_feature_local _NORMALMAP
        #pragma shader_feature_local _ALPHATEST_ON
        #pragma shader_feature_local _METALLICMAP
        #pragma shader_feature_local _OCCLUSIONMAP
        #pragma shader_feature_local _EMISSION
        
        //URP keywords
        #pragma multi_compile _ _MAIN_LIGHT_SHADOWS
        #pragma multi_compile _ _MAIN_LIGHT_SHADOWS_CASCADE

        #pragma multi_compile _ _ADDITIONAL_LIGHTS_VERTEX _ADDITIONAL_LIGHTS
        #pragma multi_compile_fragment _ _ADDITIONAL_LIGHTS_SHADOWS
        #pragma multi_compile_fragment _ _SHADOWS_SOFT
        #pragma multi_compile _ _LIGHTMAP_ON
        #pragma multi_compile _ _DIRLIGHTMAP_COMBINED
        #pragma multi_compile _ _LIGHTMAP_SHADOW_MIXING
        #pragma multi_compile _ _SHADOWS_SHADOWMASK
        #pragma multi_compile _ _SCREEN_SPACE_OCCLUSION

        //Unity keywords
        #pragma multi_compile_fog
        #pragma multi_compile_instancing
        #pragma multi_compile _ LIGHTMAP_ON
        #pragma multi_compile _ DIRLIGHTMAP_COMBINED
        
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/SurfaceInput.hlsl"

        //Needed for SRP Batching
        CBUFFER_START(UnityPerMaterial)
            half4 _BaseColor;
            float4 _MainTex_ST;
            float _Cutoff;
            float _Metallic;
            float _Smoothness;
            float _NormalScale;
            float _OcclusionStrength;
            float4 _EmissionColor;
        CBUFFER_END

        //_BumpMap and _EmissionMap are already defined in SurfaceInput.hlsl
        //_BaseMap is also defined in SurfaceInput.hlsl, but I opted to define _MainTex here instead
        TEXTURE2D(_MainTex); 
        SAMPLER(sampler_MainTex);
        TEXTURE2D(_MetallicMap);
        SAMPLER(sampler_MetallicMap);
        TEXTURE2D(_OcclusionMap);
        SAMPLER(sampler_OcclusionMap);

        //Mesh data
        struct VertexData {
            float4 position : POSITION;
            float2 uv : TEXCOORD0;
            float4 normalOS : NORMAL;
            #ifdef _NORMALMAP
                float4 tangentOS : TANGENT;
            #endif
            float2 lightmapUV : TEXCOORD1;
        };

        //Vertex to fragment data
        struct v2f {
            float4 positionCS : SV_POSITION;
            float4 positionWS : TEXCOORD4;
            float2 uv : TEXCOORD0;
            
            #ifdef _NORMALMAP
                half4 normalWS : TEXCOORD1;
                half4 tangentWS : TEXCOORD2;
                half4 bitangentWS : TEXCOORD3;
            #else
                half3 normalWS : TEXCOORD1;
            #endif

            DECLARE_LIGHTMAP_OR_SH(lightmapUV, vertexSH, 5);
        };

        ENDHLSL

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
            half SampleOcclusion(float2 uv)
            {
                #ifdef _OCCLUSIONMAP
                    #if defined(SHADER_API_GLES)
                        return SAMPLE_TEXTURE2D(_OcclusionMap, sampler_OcclusionMap, uv).g;
                    #else
                        half occ = SAMPLE_TEXTURE2D(_OcclusionMap, sampler_OcclusionMap, uv).g;
                        return LerpWhiteTo(occ, _OcclusionStrength);
                    #endif
                #else
                    return 1.0;
                #endif
            }

            half4 SampleMetallic(float2 uv)
            {
                #ifdef _METALLICMAP
                    half4 metalSmooth = SAMPLE_TEXTURE2D(_MetallicMap, sampler_MetallicMap, uv);
                    return metalSmooth;
                #else
                    half4 metalSmooth = half4(_Metallic, _Metallic, _Metallic, _Smoothness);
                    return metalSmooth;
                #endif
            }
            
            SurfaceData InitializeSurfaceData(v2f i)
            {
                SurfaceData surfaceData = (SurfaceData)0;

                //All this function does is sample the main texture, why is this even needed?????
                half4 albedoAlpha = SampleAlbedoAlpha(i.uv, TEXTURE2D_ARGS(_MainTex, sampler_MainTex));
                surfaceData.alpha = Alpha(albedoAlpha.a, _BaseColor, _Cutoff);
                surfaceData.albedo = albedoAlpha.rgb * _BaseColor.rgb;

                surfaceData.normalTS = SampleNormal(i.uv, TEXTURE2D_ARGS(_BumpMap, sampler_BumpMap), _NormalScale);
                surfaceData.emission = SampleEmission(i.uv, _EmissionColor, TEXTURE2D_ARGS(_EmissionMap, sampler_EmissionMap));
                surfaceData.occlusion = SampleOcclusion(i.uv);

                half4 metalSmooth = SampleMetallic(i.uv);
                surfaceData.metallic = metalSmooth.rgb;
                surfaceData.smoothness = metalSmooth.a;

                return surfaceData;
            }
            
            InputData InitializeInputData(v2f i, half3 normalTS)
            {
                InputData inputData = (InputData)0;

                inputData.positionWS = i.positionWS;

                #ifdef _NORMALMAP
                    half3 viewDirWS = half3(i.normalWS.w, i.tangentWS.w, i.bitangentWS.w);
                    inputData.normalWS = TransformTangentToWorld(normalTS, half3x3(i.tangentWS.xyz, i.bitangentWS.xyz, i.normalWS.xyz));
                #else
                    half3 viewDirWS = GetWorldSpaceNormalizeViewDir(inputData.positionWS);
                    inputData.normalWS = i.normalWS;
                #endif

                inputData.normalWS = NormalizeNormalPerPixel(inputData.normalWS);
                viewDirWS = SafeNormalize(viewDirWS);

                inputData.viewDirectionWS = viewDirWS;

                //Some lighting and shadow stuff should go here

                inputData.bakedGI = SAMPLE_GI(i.lightmapUV, i.vertexSH, inputData.normalWS);
                inputData.normalizedScreenSpaceUV = GetNormalizedScreenSpaceUV(i.positionCS);
                inputData.shadowMask = SAMPLE_SHADOWMASK(i.lightmapUV);

                return inputData;
            }

            v2f vert(VertexData v)
            {
                v2f o;

                VertexPositionInputs positionInputs = GetVertexPositionInputs(v.position.xyz);
                o.positionCS = positionInputs.positionCS;
                
                half3 viewDirWS = GetWorldSpaceViewDir(positionInputs.positionWS);

                #ifdef _NORMALMAP
                    VertexNormalInputs normalInputs = GetVertexNormalInputs(v.normalOS.xyz, v.tangentOS);

                    o.normalWS = half4(normalInputs.normalWS, viewDirWS.x);
                    o.tangentWS = half4(normalInputs.tangentWS, viewDirWS.y);
                    o.bitangentWS = half4(normalInputs.bitangentWS, viewDirWS.z);
                #else
                    VertexNormalInputs normalInputs = GetVertexNormalInputs(v.normalOS.xyz);

                    o.normalWS = NormalizeNormalPerVertex(normalInputs.normalWS);
                #endif

                half3 vertexLight = VertexLighting(positionInputs.positionWS, normalInputs.normalWS);
                half fogFactor = ComputeFogFactor(positionInputs.positionCS.z);

                OUTPUT_LIGHTMAP_UV(v.lightmapUV, unity_LightmapST, o.lightmapUV);
                OUTPUT_SH(o.normalWS.xyz, o.vertexSH);

                o.uv = TRANSFORM_TEX(v.uv, _MainTex);

                return o;
            }

            half4 frag(v2f i) : SV_TARGET
            {
                float4 mainTex = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv);
                
                SurfaceData surfaceData = InitializeSurfaceData(i);

                InputData inputData = InitializeInputData(i, surfaceData.normalTS);

                half4 output = UniversalFragmentPBR(inputData, surfaceData);
                output.rgb = MixFog(output.rgb, inputData.fogCoord);

                return output;
            }

            ENDHLSL
        }
    }
}
