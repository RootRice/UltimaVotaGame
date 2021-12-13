Shader "Unlit/ToonShaderShield"
{

    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
		_ShieldTex("Shield Texture", 2D) = "white" {}
        _Brightness("Brightness", Range(0,1)) = 0.3
        _Strength("Strength", Range(0,1)) = 0.5
        _Color("Colour", COLOR) = (1,1,1,1)
        _Detail("Detail", Range(0,1)) = 0.3
        _ShadowColor("Shadow Colour", COLOR) = (0,0,0,0)
        _DetailRange("Shadow Colour Potency", Range(0,1)) = 0.5
        _MinimumShadow("Minimum Shadow", Range(0,1)) = 0.5
		_ShieldActive("Shield activation", Range(0,1)) = 1
    }
        SubShader
        {
            
            Tags { "RenderType" = "Opaque" }
         
            
            LOD 100
            Cull OFF
            Pass
            {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #include "AutoLight.cginc"
                #include "UnityCG.cginc"
                #include "Lighting.cginc"

                

                struct appdata
                {
                    float4 vertex : POSITION;
                    float2 uv : TEXCOORD0;
					float2 uv2 : TEXCOORD1;
                    float3 normal : NORMAL;
                };

                struct v2f
                {
                    float2 uv : TEXCOORD0;
					float2 uv2 : TEXCOORD1;
                    float4 vertex : SV_POSITION;
                    half3 worldNormal: NORMAL;
                };

                sampler2D _MainTex;
                float4 _MainTex_ST; 
				sampler2D _ShieldTex;
				float2 _ShieldTexCoord;
				float4 _ShieldTex_ST;
                sampler2D _MetalTex;
                float4 _MetalTex_ST;
                float _Brightness;
                float _Strength;
                float4 _Color;
                float _Detail;
                uniform fixed4 _ShadowColor;
                float _ShadowCalc;
                float _DetailRange;
                float _MinimumShadow;
				float _ShieldActive;

                float Toon(float3 normal, float3 lightDir) 
                {
                    float NdotL = max(_MinimumShadow,dot(normalize(normal), normalize(lightDir)));
                    NdotL = floor(NdotL / _Detail);
                    return NdotL;
                }

                v2f vert(appdata v)
                {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = TRANSFORM_TEX(v.uv, _MainTex);
					o.uv2 = TRANSFORM_TEX(v.uv2, _ShieldTex);
                    o.worldNormal = UnityObjectToWorldNormal(v.normal);
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target
                {
                    _ShadowCalc = Toon(i.worldNormal, _WorldSpaceLightPos0.xyz);
					_ShieldTexCoord = i.uv2 + frac(_Time.x * float2(3, 3));
                    fixed4 col = tex2D(_MainTex, i.uv);
					fixed4 col2 = tex2D(_ShieldTex, _ShieldTexCoord);
					col2.a *= _ShieldActive;
                    col *= _ShadowCalc * _Strength * ((_ShadowColor * ((_ShadowCalc * _Detail) < _DetailRange)) + (_Color * ((_ShadowCalc * _Detail) > _DetailRange))) + _Brightness;
					col = lerp(col, col2, col2.a);
                    return col;
                }

                ENDCG
            }
        }
}

