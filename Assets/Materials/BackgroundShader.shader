Shader "Unlit/BackgroundShader"
{
    Properties
    {
        _MainTex ("Color Scheme", 2D) = "white" {}
        _Stencil ("Stencil", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType" = "Opaque" }
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
            sampler2D _Stencil;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.worldPos = mul(unity_ObjectToWorld, v.vertex);
                return o;
            }

            // Noise function from:
            // https://gist.github.com/nukeop/9877f5dc39f6ef57716a75e0aa0ffd27
            float hash(float n)
            {
                return frac(sin(n) * 43758.5453);
            }

            float noise( float3 x )
            {
                float3 p = floor(x);
                float3 f = frac(x);

                f = f * f * (3.0 - 2.0 * f);
                float n = p.x + p.y * 57.0 + 113.0 * p.z;

                return lerp(lerp(lerp(hash(n + 0.0), hash(n + 1.0), f.x),
                lerp(hash(n + 57.0), hash(n + 58.0),f.x), f.y),
                lerp(lerp(hash(n + 113.0), hash(n + 114.0), f.x),
                lerp(hash(n + 170.0), hash(n + 171.0),f.x), f.y), f.z);
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float n = noise(float3(i.worldPos.xy, 1));
                fixed4 color = tex2D(_MainTex, (length(i.worldPos) + n * 10) / 100);
                fixed4 stencil = tex2D(_Stencil, i.uv);
                return color * stencil.a;
            }
            ENDCG
        }
    }
}
