﻿Shader "Custom/PostEffect"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;

            fixed4 frag(v2f i) : SV_Target
            {
                i.uv -= fixed2(0.5, 0.5);
                i.uv.x *= 16.0 / 9.0;
                if (distance(i.uv, fixed2(0, 0)) < 0.1) {
                    discard;
                }
                return fixed4(0.0,0.0,0.0,1.0);
            }
            ENDCG
        }
    }
}
