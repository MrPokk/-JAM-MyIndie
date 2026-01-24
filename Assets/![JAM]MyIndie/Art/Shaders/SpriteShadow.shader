Shader "Custom/2D/SpriteShadow"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Shadow Color", Color) = (0,0,0,0.5)
        _SkewX ("Skew X (Lean)", Range(-2, 2)) = 0.5
        _ScaleY ("Flatten Y", Range(0, 1)) = 0.5
    }

    SubShader
    {
        Tags
        { 
            "Queue"="Transparent" 
            "IgnoreProjector"="True" 
            "RenderType"="Transparent" 
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True" 
        }

        Cull Off
        Lighting Off
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex   : POSITION;
                float4 color    : COLOR;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex   : SV_POSITION;
                fixed4 color    : COLOR;
                float2 texcoord : TEXCOORD0;
            };

            fixed4 _Color;
            sampler2D _MainTex;
            float _SkewX;
            float _ScaleY;

            v2f vert(appdata_t IN)
            {
                v2f OUT;
                
                float4 v = IN.vertex;

                v.y *= _ScaleY;

                v.x += v.y * _SkewX;

                OUT.vertex = UnityObjectToClipPos(v);
                OUT.texcoord = IN.texcoord;
                OUT.color = IN.color * _Color; 
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