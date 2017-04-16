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

			float4 _IN0;
			float4 _OUT0;

			float4 frag(v2f i) : SV_Target
			{
				float4 col = tex2D(_MainTex, i.uv);

				// ugly with if:s...

				if (col.r == 0.2)
					return _OUT0;

				/*if (all(col.r == _IN0.r))
					return _OUT0;*/

					/*if (col.r == _IN0.r)
						return _OUT0;*/

					return col;
				}

			ENDCG
			}
	}
}
