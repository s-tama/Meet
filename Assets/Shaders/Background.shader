//
// Background.shader
//
// Author : 
//

Shader "Custom/Background"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _Color("Color", Color) = (1, 1, 1, 1)

        _Threshold("Threshold", Range(0.0,3.0)) = 0.6
        _Freqency("Freqency", Range(0, 1)) = 0.5
    }
    SubShader
    {
        Tags { "RenderType"="Background" }
        LOD 100

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
                float3 worldPos : TEXCOORD1;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            fixed4 _Color;

            float _Threshold;
            float _Freqency;
            float _Radius;

            // 座標がオブジェクト内か？を返し，形状を定義する形状関数
            // 形状は原点を中心とした球．
            // 原点から一定の距離内の座標に存在するので球になる．
            bool isInObject(float3 pos) {
                float f = _Freqency;
                float t = _Threshold;
                const float PI2 = 6.28318530718;
                return
                    sin(PI2 * f * pos.x) < t &&
                    sin(PI2 * f * pos.y) < t &&
                    sin(PI2 * f * pos.z) < t;
            }

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 col;

                // 初期の色（黒）を設定
                col.xyz = 0.0;
                col.w = 1.0;

                // レイの初期位置
                float3 pos = i.worldPos.xyz;

                // レイの進行方向
                float3 forward = normalize(pos.xyz - _WorldSpaceCameraPos);

                // レイが進むことを繰り返す．
                // オブジェクト内に到達したら進行距離に応じて色決定
                // 当たらなかったらそのまま（今回は黒）
                const int StepNum = 30;
                const float MarchingDist = 0.03;
                for (int i = 0; i < StepNum; i++) {
                    if (isInObject(pos)) {
                        col.r = 1.0 - i * 0.02 * abs(sin(_Time.x));
                        col.g = 1.0 - i * 0.02 * abs(sin(_Time.y));
                        col.b = 1.0 - i * 0.02 * abs(sin(_Time.z));
                        col.a = pow(col.x, 0.3);
                        break;
                    }
                    pos.xyz += MarchingDist * forward.xyz;
                }

                return col;
            }
            ENDCG
        }
    }
}
