Shader "2DxFX/Standard/Blood"
{
  Properties
  {
    _MainTex ("Base (RGB)", 2D) = "white" {}
    [HideInInspector] _MainTex2 ("Pattern (RGB)", 2D) = "white" {}
    _Color ("_Color", Color) = (1,1,1,1)
    _Distortion ("Distortion", Range(0, 1)) = 0
    _Alpha ("Alpha", Range(0, 1)) = 1
    _Speed ("Speed", Range(0, 1)) = 1
    EValue ("EValue", Range(0, 1)) = 1
    Light ("Light", Range(0, 1)) = 1
    TurnToLiquid ("TurnToLiquid", Range(0, 1)) = 1
    _StencilComp ("Stencil Comparison", float) = 8
    _Stencil ("Stencil ID", float) = 0
    _StencilOp ("Stencil Operation", float) = 0
    _StencilWriteMask ("Stencil Write Mask", float) = 255
    _StencilReadMask ("Stencil Read Mask", float) = 255
    _ColorMask ("Color Mask", float) = 15
  }
  SubShader
  {
    Tags
    { 
      "IGNOREPROJECTOR" = "true"
      "QUEUE" = "Transparent"
      "RenderType" = "Transparent"
    }
    Pass // ind: 1, name: 
    {
      Tags
      { 
        "IGNOREPROJECTOR" = "true"
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
      Blend SrcAlpha OneMinusSrcAlpha
      // m_ProgramMask = 6
      CGPROGRAM
      //#pragma target 4.0
      
      #pragma vertex vert
      #pragma fragment frag
      
      #include "UnityCG.cginc"
      
      
      #define CODE_BLOCK_VERTEX
      //uniform float4x4 unity_ObjectToWorld;
      //uniform float4x4 unity_MatrixVP;
      uniform float _Distortion;
      uniform float _Alpha;
      uniform float TurnToLiquid;
      uniform sampler2D _MainTex2;
      uniform sampler2D _MainTex;
      struct appdata_t
      {
          float4 vertex :POSITION0;
          float4 color :COLOR0;
          float2 texcoord :TEXCOORD0;
      };
      
      struct OUT_Data_Vert
      {
          float2 texcoord :TEXCOORD0;
          float4 color :COLOR0;
          float4 vertex :SV_POSITION;
      };
      
      struct v2f
      {
          float2 texcoord :TEXCOORD0;
          float4 color :COLOR0;
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
          out_v.texcoord.xy = float2(in_v.texcoord.xy);
          out_v.vertex = UnityObjectToClipPos(in_v.vertex);
          out_v.color = in_v.color;
          return out_v;
      }
      
      #define CODE_BLOCK_FRAGMENT
      float4 u_xlat0_d;
      float3 u_xlat1_d;
      float3 u_xlat10_1;
      float4 u_xlat2;
      float4 u_xlat10_3;
      float u_xlat4;
      float u_xlat8;
      float u_xlat12;
      OUT_Data_Frag frag(v2f in_f)
      {
          OUT_Data_Frag out_f;
          u_xlat0_d.x = (((-TurnToLiquid) * 2) + 1);
          u_xlat0_d.x = ((-u_xlat0_d.x) + 1.03999996);
          u_xlat4 = (in_f.texcoord.x * 16);
          u_xlat8 = (TurnToLiquid + 1);
          u_xlat4 = (u_xlat8 * u_xlat4);
          u_xlat4 = sin(u_xlat4);
          u_xlat4 = (u_xlat4 * TurnToLiquid);
          u_xlat8 = ((u_xlat4 * 0.5) + 1);
          u_xlat0_d.x = (u_xlat8 / u_xlat0_d.x);
          u_xlat8 = (u_xlat4 * 0.5);
          u_xlat12 = ((-in_f.texcoord.y) + 1);
          u_xlat0_d.x = ((u_xlat0_d.x * u_xlat12) + (-u_xlat8));
          u_xlat0_d.x = clamp(u_xlat0_d.x, 0, 1);
          u_xlat0_d.x = ((-u_xlat0_d.x) + 1);
          u_xlat8 = (in_f.texcoord.y + TurnToLiquid);
          u_xlat1_d.y = (u_xlat8 + (-0.200000003));
          u_xlat1_d.x = in_f.texcoord.x;
          u_xlat8 = (_Distortion + TurnToLiquid);
          u_xlat12 = ((u_xlat8 * 0.125) + 1);
          u_xlat12 = (2 / u_xlat12);
          u_xlat1_d.xy = float2((u_xlat1_d.xy / float2(u_xlat12, u_xlat12)));
          u_xlat10_1.xyz = tex2D(_MainTex2, u_xlat1_d.xy).xyz.xyz;
          u_xlat12 = (u_xlat8 * 20);
          u_xlat1_d.xyz = float3((float3(u_xlat12, u_xlat12, u_xlat12) * u_xlat10_1.xyz));
          u_xlat2.xy = float2(((u_xlat1_d.xy * float2(0.03125, 0.03125)) + in_f.texcoord.xy));
          u_xlat8 = (((-u_xlat8) * 0.25) + u_xlat2.x);
          u_xlat2.y = (((-_Distortion) * 0.434782624) + u_xlat2.y);
          u_xlat2.x = ((u_xlat4 * 0.125) + u_xlat8);
          u_xlat10_3 = tex2D(_MainTex, u_xlat2.xy);
          u_xlat4 = (u_xlat2.y + 0.400000006);
          u_xlat4 = dot(float2(u_xlat4, u_xlat4), float2(float2(TurnToLiquid, TurnToLiquid)));
          u_xlat4 = ((-u_xlat4) + 1);
          u_xlat2 = (u_xlat10_3 * in_f.color);
          u_xlat0_d.xzw = ((u_xlat0_d.xxx * float3(0.5, 0.5, 0.5)) + u_xlat2.xyz);
          u_xlat4 = (u_xlat4 * u_xlat2.w);
          out_f.color.x = ((u_xlat1_d.x * 0.125) + u_xlat0_d.x);
          out_f.color.yz = (((-u_xlat1_d.yz) * float2(0.125, 0.125)) + u_xlat0_d.zw);
          u_xlat0_d.x = ((-_Alpha) + 1);
          out_f.color.w = (u_xlat0_d.x * u_xlat4);
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack "Sprites/Default"
}
