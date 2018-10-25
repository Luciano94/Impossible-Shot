// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Dedalord/SphereLit"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_SphereMap("Sphere map", 2D) = "black" {}
        _SphLitFactor ("SphereLit Factor", Float) = 2.0
	}

	SubShader
	{
		Tags { "RenderType"="Opaque" } 
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
				float3 normal : NORMAL;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD0;
				float2 uv2 : TEXCOORD1;
			};

			sampler2D _MainTex;
			sampler2D _SphereMap;

            float _SphLitFactor;

			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				o.uv2 = mul((float3x3)UNITY_MATRIX_IT_MV, normalize(v.normal)).xy * .5 + .5;
				return o;
			}
			
			float4 frag (v2f i) : SV_Target
			{
				float4 lit = tex2D (_SphereMap, i.uv2);
				float4 col = tex2D(_MainTex, i.uv);

                col = col * lit * _SphLitFactor;
                col.a = 1;

				return col;
			}
			ENDCG
		}
	}

	//Fallback "Mobile/Diffuse"
}