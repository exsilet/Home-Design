Shader "GGMatch3/Diffuse Stencil"
{
  Properties
  {
    _MainTex ("Sprite Texture", 2D) = "white" {}
    _Color ("Tint", Color) = (1,1,1,1)
    [Enum(UnityEngine.Rendering.CompareFunction)] _StencilComp ("Stencil Comparison", float) = 3
    [Enum(UnityEngine.Rendering.StencilOp)] _StencilOpPass ("Stencil Operation", float) = 0
    [Enum(UnityEngine.Rendering.StencilOp)] _StencilOpFail ("Stencil Fail", float) = 0
    [Enum(UnityEngine.Rendering.StencilOp)] _StencilOpZFail ("Stencil ZFail", float) = 0
    _Stencil ("Stencil Reference", float) = 0
    _StencilWriteMask ("Stencil Write Mask", float) = 255
    _StencilReadMask ("Stencil Read Mask", float) = 255
    _ColorMask ("Color Mask", float) = 15
    [Toggle(UNITY_UI_ALPHACLIP)] _UseUIAlphaClip ("Use Alpha Clip", float) = 0
  }
  SubShader
  {
    Tags
    { 
      "CanUseSpriteAtlas" = "true"
      "IGNOREPROJECTOR" = "true"
      "PreviewType" = "Plane"
      "QUEUE" = "Transparent"
      "RenderType" = "Transparent"
    }
    Pass // ind: 1, name: 
    {
      Tags
      { 
        "CanUseSpriteAtlas" = "true"
        "IGNOREPROJECTOR" = "true"
        "PreviewType" = "Plane"
        "QUEUE" = "Transparent"
        "RenderType" = "Transparent"
      }
      ZWrite Off
      Cull Off
      Stencil
      { 
        Ref 0
        ReadMask 0
        WriteMask 0
        Pass Keep
        Fail Keep
        ZFail Keep
        PassFront Keep
        FailFront Keep
        ZFailFront Keep
        PassBack Keep
        FailBack Keep
        ZFailBack Keep
      } 
      Blend One OneMinusSrcAlpha
      ColorMask 0
      // m_ProgramMask = 6
      CGPROGRAM
      #pragma multi_compile UNITY_UI_ALPHACLIP
      //#pragma target 4.0
      
      #pragma vertex vert
      #pragma fragment frag
      
      #include "UnityCG.cginc"
      
      
      #define CODE_BLOCK_VERTEX
      //uniform float4x4 unity_ObjectToWorld;
      //uniform float4x4 unity_MatrixVP;
      uniform float4 _Color;
      uniform float _AlphaSplitEnabled;
      uniform sampler2D _MainTex;
      uniform sampler2D _AlphaTex;
      struct appdata_t
      {
          float4 vertex :POSITION0;
          float4 color :COLOR0;
          float2 texcoord :TEXCOORD0;
      };
      
      struct OUT_Data_Vert
      {
          float4 color :COLOR0;
          float2 texcoord :TEXCOORD0;
          float4 vertex :SV_POSITION;
      };
      
      struct v2f
      {
          float4 color :COLOR0;
          float2 texcoord :TEXCOORD0;
      };
      
      struct OUT_Data_Frag
      {
          float4 color :SV_Target0;
      };
      
      float4 u_xlat0;
      float4 u_xlat1;
      OUT_Data_Vert vert(appdata_t in_v)
      {
          OUT_Data_Vert out_v;
          out_v.vertex = UnityObjectToClipPos(in_v.vertex);
          u_xlat0 = (in_v.color * _Color);
          out_v.color = u_xlat0;
          out_v.texcoord.xy = float2(in_v.texcoord.xy);
          return out_v;
      }
      
      #define CODE_BLOCK_FRAGMENT
      float4 u_xlat10_0;
      int u_xlatb0;
      float u_xlat10_1;
      int u_xlatb1;
      float u_xlat16_2;
      float u_xlat16_3;
      float3 u_xlat16_6;
      OUT_Data_Frag frag(v2f in_f)
      {
          OUT_Data_Frag out_f;
          u_xlat10_0 = tex2D(_MainTex, in_f.texcoord.xy);
          if((float4(0, 0, 0, 0).x != float4(_AlphaSplitEnabled, _AlphaSplitEnabled, _AlphaSplitEnabled, _AlphaSplitEnabled).x && float4(0, 0, 0, 0).y != float4(_AlphaSplitEnabled, _AlphaSplitEnabled, _AlphaSplitEnabled, _AlphaSplitEnabled).y && float4(0, 0, 0, 0).z != float4(_AlphaSplitEnabled, _AlphaSplitEnabled, _AlphaSplitEnabled, _AlphaSplitEnabled).z && float4(0, 0, 0, 0).w != float4(_AlphaSplitEnabled, _AlphaSplitEnabled, _AlphaSplitEnabled, _AlphaSplitEnabled).w))
          {
              u_xlat10_1 = tex2D(_AlphaTex, in_f.texcoord.xy).x;
              u_xlat16_2 = u_xlat10_1;
          }
          else
          {
              u_xlat16_2 = u_xlat10_0.w;
          }
          u_xlat16_6.xyz = float3((u_xlat10_0.xyz * in_f.color.xyz));
          u_xlat16_3 = (u_xlat16_2 * in_f.color.w);
          out_f.color.xyz = float3((u_xlat16_6.xyz * float3(u_xlat16_3, u_xlat16_3, u_xlat16_3)));
          u_xlat16_2 = ((u_xlat16_2 * in_f.color.w) + (-0.00100000005));
          if((u_xlat16_2<0))
          {
              u_xlatb0 = 1;
          }
          else
          {
              u_xlatb0 = 0;
          }
          if(((int(u_xlatb0) * (-1))!=0))
          {
              discard;
          }
          out_f.color.w = u_xlat16_3;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
