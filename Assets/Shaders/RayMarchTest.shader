// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/RayMarchTest"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Centre("Centre",Vector) = (0,0,0)
        _Radius("Radius",float) = 10.0
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

            sampler2D _MainTex;
            float _Radius;
            float _Centre;

            #define STEPS 64
            #define STEP_SIZE 0.01
            #define MIN_DISTANCE 0.1

            struct appdata {
                float4 vertex : POSITION;
            };

            struct v2f {
                float4 vertex : SV_POSITION;
                float3 wPos : TEXCOORD1; // World Position
            };

            v2f vert(appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.wPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                return o;
            }

            bool sphereHit(float3 p) {
                return distance(p, _Centre) < _Radius;
            }
            float sphereDistance(float3 p) {
                return distance(p, _Centre) - _Radius;
            }
            
            fixed4 raymarch(float3 position, float3 direction) {
                for (int i = 0; i < STEPS; i++) {
                    float distance = sphereDistance(position);
                    if (distance < MIN_DISTANCE)
                        return (1-(i / (float)STEPS)) * fixed4(1,0,0,1);
                    position += direction * STEP_SIZE;
                }
                return 1;
            }
            fixed4 frag(v2f i) : SV_Target
            {
                float3 worldPosition = i.wPos;
                float3 viewDirection = normalize(i.wPos - _WorldSpaceCameraPos);
                return raymarch(worldPosition, viewDirection);
            }
            ENDCG
        }
    }
}
