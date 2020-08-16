Shader "ArtExp/FadeShader"
{
	Properties{
		 _Color("Tint", Color) = (0, 0, 0, 1)
		 _MainTex("Texture", 2D) = "white" {}
	}

		SubShader{
			Tags{
				"RenderType" = "Transparent"
				"Queue" = "Transparent"
			}

			Blend SrcAlpha OneMinusSrcAlpha

			ZWrite off
			Cull off

			Pass{

				CGPROGRAM

				#include "UnityCG.cginc"

				#pragma vertex vert
				#pragma fragment frag

				sampler2D _MainTex;
				float4 _MainTex_ST;

				fixed4 _Color;

				struct appdata {
					float4 vertex : POSITION;
					float2 uv : TEXCOORD0;
					fixed4 color : COLOR;
					float3 normal : NORMAL;
				};

				struct v2f {
					float4 position : SV_POSITION;
					float2 uv : TEXCOORD0;
					fixed4 color : COLOR;
					float3 normal : NORMAL;
				};

				v2f vert(appdata v) {
					v2f o;
					o.position = UnityObjectToClipPos(v.vertex);
					o.uv = TRANSFORM_TEX(v.uv, _MainTex);
					o.normal = UnityObjectToWorldNormal(v.normal);
					o.color = v.color;
					return o;
				}

				fixed4 frag(v2f i) : SV_TARGET{
					float3 viewDir = normalize(UNITY_MATRIX_IT_MV[2].xyz);
					float3 nDir = normalize(i.normal);
					float dotP = abs(dot(viewDir, i.normal));
					float slope = (0 - 1) / (0 - 0.15);
					float output = 1 + slope * (dotP - 0.15);
					if (dotP < 0.15) dotP = output;
					else dotP = 1;
					//else dotP = 1;
					
					fixed4 col = tex2D(_MainTex, i.uv);
					col *= _Color;
					col *= i.color*dotP;
					//col.a = dotP;// (dotP);
					return col;
				}

				ENDCG
			}
	}
}
