//
// WebCam.shader
//
// Author : 
//

Shader "Custom/WebCam"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _DissolveTex("Dissolve Texture", 2D) = "white" {}
        _Threshold("Threshold", Range(0, 1)) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 100

        Blend SrcAlpha OneMinusSrcAlpha

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
		        float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;          
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            sampler2D _DissolveTex;
            float _Threshold;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float2 uv = i.uv;
                //uv = uv / dot(uv, uv + sin(10));
                //uv.x *= 16.0 / 9.0;
                fixed4 col = tex2D(_MainTex, i.uv);
                fixed dissolve = tex2D(_DissolveTex, uv);
                col *= step(_Threshold, dissolve);

				float threshold = 0.99;
                if ((col.r >= threshold) || (col.g >= threshold) || (col.b >= threshold))
                {
                    discard;
                }

                return col;
            }
            ENDCG
        }
    }
}
