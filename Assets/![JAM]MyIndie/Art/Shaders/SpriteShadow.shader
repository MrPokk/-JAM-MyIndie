Shader "Custom/2D/SimpleShadowOnly"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Shadow Color", Color) = (0,0,0,0.5)
        _SkewX ("Shadow Lean", Range(-2, 2)) = 0.5
        _ScaleY ("Shadow Length", Range(0, 2)) = 0.5
        _Taper ("Perspective Taper", Range(-1, 1)) = 0.3
    }

    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        Cull Off ZWrite Off Lighting Off
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t { float4 vertex : POSITION; float2 texcoord : TEXCOORD0; };
            struct v2f { float4 vertex : SV_POSITION; float2 texcoord : TEXCOORD0; };

            sampler2D _MainTex;
            fixed4 _Color;
            float _SkewX, _ScaleY, _Taper;

            v2f vert(appdata_t IN)
            {
                v2f OUT;
                float4 v = IN.vertex;
                float height = IN.texcoord.y;

                v.x *= (1.0 - height * _Taper);
                v.x += height * _SkewX * 10.0;
                v.y *= _ScaleY;

                OUT.vertex = UnityObjectToClipPos(v);
                OUT.texcoord = IN.texcoord;
                return OUT;
            }

            fixed4 frag(v2f IN) : SV_Target
            {
                fixed4 c = tex2D(_MainTex, IN.texcoord);
                return fixed4(_Color.rgb, c.a * _Color.a);
            }
            ENDCG
        }
    }
}