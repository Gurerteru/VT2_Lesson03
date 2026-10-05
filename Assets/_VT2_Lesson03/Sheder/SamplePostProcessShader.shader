Shader "Custom/SamplePostProcessShader"
{   
    // ==================================== //
    // インスペクター用のプロパティを定義
    // ==================================== //
    Properties
    {
        _MainColor("Main Color",Color) = (1,1,1,1)      // 色のプロパティを定義
     }

    SubShader
    {
        HLSLINCLUDE
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
        #include "Packages/com.unity.render-pipelines.core/Runtime/Utilities/Blit.hlsl"
        ENDHLSL

        Tags { "RenderType"="Opaque" }
        LOD 100
        ZWrite Off Cull Off
        Pass
        {
            Name "SamplePostProcessShader"

            HLSLPROGRAM
            
            #pragma vertex Vert
            #pragma fragment Frag

            float4 _MainColor = float4(1,1,1,1);  // 変数の宣言

            float4 Frag (Varyings input) : SV_Target
            {
                float4 color = SAMPLE_TEXTURE2D(_BlitTexture, sampler_LinearClamp, input.texcoord).rgba;
                return color * _MainColor;
            }
            
            ENDHLSL
        }
    }
}
