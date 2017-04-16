Shader "Sprites/Palette-Swapper"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white"

	}

		SubShader
	{
		Tags
		{
			"Queue" = "Transparent"
		}

		Pass
		{
			Blend SrcAlpha OneMinusSrcAlpha
			Cull Off

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

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = v.uv;
				return o;
			}

			sampler2D _MainTex;

			float4 skin_1;
			float4 skin_2;
			float4 jacket_1;
			float4 jacket_2;
			float4 pants;

			float4 frag(v2f i) : SV_Target
			{
				float4 col = tex2D(_MainTex, i.uv);

				float4 orig_skin_1 = float4(255.0f / 255.0f, 212.0f / 255.0f, 168 / 255.0f, 255.0f / 255.0f);
				float4 orig_skin_2 = float4(239.0f / 255.0f, 150.0f / 255.0f, 98.0f / 255.0f, 255.0f / 255.0f);
				float4 orig_jacket_1 = float4(62.0f / 255.0f, 154.0f / 255.0f, 218.0f / 255.0f, 255.0f / 255.0f);
				float4 orig_jacket_2 = float4(70.0f / 255.0f, 101.0f / 255.0f, 200.0f / 255.0f, 255.0f / 255.0f);
				float4 orig_pants = float4(72.0f / 255.0f, 62.0f / 255.0f, 62.0f / 255.0f, 255.0f / 255.0f);

				// ugly with conditionals...

				col = (all(col.rgb == orig_skin_1.rgb)) ? skin_1 : 
				(all(col.rgb == orig_skin_2.rgb)) ? skin_2 :
				(all(col.rgb == orig_jacket_1.rgb)) ? jacket_1 :
				(all(col.rgb == orig_jacket_2.rgb)) ? jacket_2 :
				(all(col.rgb == orig_pants.rgb)) ? pants : 
													col;


					return col;
				}

			ENDCG
			}
	}
}
