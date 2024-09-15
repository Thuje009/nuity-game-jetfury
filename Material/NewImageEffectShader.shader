Shader "Hidden/BlurImageEffectShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _BlurAmount ("Blur Amount", Range(0.0, 10.0)) = 1.0 // ��Ҵ������
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

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

            sampler2D _MainTex;
            float _BlurAmount;
            float2 _MainTex_TexelSize; // ��Ҵ�ͧ�ԡ��

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 uv = i.uv;
                float2 blurStep = _MainTex_TexelSize * _BlurAmount;

                // �ӹǳ�������¨ҡ����� �ԡ���ͺ � ���˹觻Ѩ�غѹ
                fixed4 col = tex2D(_MainTex, uv) * 0.2270270270; // ���˹ѡ��ѡ�ç��ҧ
                col += tex2D(_MainTex, uv + blurStep * float2( 1.0,  0.0)) * 0.1945945946;
                col += tex2D(_MainTex, uv + blurStep * float2(-1.0,  0.0)) * 0.1945945946;
                col += tex2D(_MainTex, uv + blurStep * float2( 0.0,  1.0)) * 0.1945945946;
                col += tex2D(_MainTex, uv + blurStep * float2( 0.0, -1.0)) * 0.1945945946;

                return col;
            }
            ENDCG
        }
    }
}
