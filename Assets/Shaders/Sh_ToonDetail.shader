Shader "Unlit/ToonShaderDetail"
{

    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
		_DetailTex("Detail Texture", 2D) = "white" {}
        _DetailTex2("Detail Texture", 2D) = "white" {}
        _LWing("Left Wing", Range(0,1)) = 0.0
        _RWing("Right Wing", Range(0,1)) = 0.0
		_ShieldTex("Shield Texture", 2D) = "white" {}
		_Brightness("Brightness", Range(0,1)) = 0.3
		_Strength("Strength", Range(0,1)) = 0.5
		_Color("Colour", COLOR) = (1,1,1,1)
		_DetailColor("Detail Colour", COLOR) = (1,1,1,1)
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
				sampler2D _DetailTex;
				float4 _DetailTex_ST;
                sampler2D _DetailTex2;
                float4 _DetailTex2_ST;
				sampler2D _ShieldTex;
				float4 _ShieldTex_ST;
				float2 _ShieldTexCoord;
                sampler2D _MetalTex;
                float4 _MetalTex_ST;
                float _Brightness;
                float _Strength;
                float4 _Color;
				float4 _DetailColor;
                float _Detail;
                uniform fixed4 _ShadowColor;
                float _ShadowCalc;
                float _DetailRange;
                float _MinimumShadow;
				float _ShieldActive;
                float _LWing;
                float _RWing;

				

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
					o.uv2 = TRANSFORM_TEX(v.uv2, _DetailTex);
                    o.worldNormal = UnityObjectToWorldNormal(v.normal);
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target
                {
                    _ShadowCalc = Toon(i.worldNormal, _WorldSpaceLightPos0.xyz);
					_ShieldTexCoord = i.uv2 + frac(_Time.x * float2(3, 3));
                    fixed4 col = tex2D(_MainTex, i.uv);
					fixed4 col2 = tex2D(_DetailTex, i.uv2);
					fixed4 col3 = tex2D(_ShieldTex, _ShieldTexCoord);
                    fixed4 col4 = tex2D(_DetailTex2, i.uv2);
					col3.a *= _ShieldActive;
                    col2.a *= _LWing;
                    col4.a *= _RWing;
                    col *= _ShadowCalc * _Strength * ((_ShadowColor * ((_ShadowCalc * _Detail) < _DetailRange)) + (_Color * ((_ShadowCalc * _Detail) > _DetailRange))) + _Brightness;
					col = lerp(col, col2, col2.a);
					col = lerp(col, col3, col3.a);
                    col = lerp(col, col4, col4.a);
                    return col;
                }

                ENDCG
            }
        }
}