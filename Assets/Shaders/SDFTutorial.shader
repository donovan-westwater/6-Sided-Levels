Shader "Unlit/SDFTutorial"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _Centre("Centre",Vector) = (0,0,0)
        _Radius("Radius",float) = 10.0
        _SpecularPower("SpecularPower",float) = 1
        _Gloss("Gloss",float) = 1
        _Color("Color",Color) = (1,1,1,1)
    }
        SubShader
        {
            Tags { "RenderType" = "Opaque" }
            Tags{"LightMode" = "ForwardBase"}
            LOD 100

            Pass
            {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag

                #include "UnityCG.cginc"
                #include "Lighting.cginc"

                fixed4 _Color;
                sampler2D _MainTex;
                float _Radius;
                float _Centre;
                float _SpecularPower;
                float _Gloss;

                float3 viewDirection;
                #define STEPS 64
                #define STEP_SIZE 0.01
                #define MIN_DISTANCE 0.001

                struct appdata {
                    float4 vertex : POSITION;
                };

                struct v2f {
                    float4 vertex : SV_POSITION;
                    float3 wPos : TEXCOORD1; // World Position
                };

                float sdf_blend(float d1, float d2, float a)
                {
                    return a * d1 + (1 - a) * d2;
                }
                //Signed distance function Postive if outside
                //Negative if inside
                float sdf_sphere(float3 p, float3 c, float r) {
                    return distance(p, c) - r;
                }

                float sdf_box(float3 p, float3 c, float3 s)
                {
                    float x = max
                    (p.x - c.x - float3(s.x / 2., 0, 0),
                        c.x - p.x - float3(s.x / 2., 0, 0)
                    );

                    float y = max
                    (p.y - c.y - float3(s.y / 2., 0, 0),
                        c.y - p.y - float3(s.y / 2., 0, 0)
                    );

                    float z = max
                    (p.z - c.z - float3(s.z / 2., 0, 0),
                        c.z - p.z - float3(s.z / 2., 0, 0)
                    );

                    float d = x;
                    d = max(d, y);
                    d = max(d, z);
                    return d;
                }
                float sdf_animation(float3 p, float3 c, float r) {

                    float d = sdf_blend
                    (
                        sdf_sphere(p, 0, r),
                        sdf_box(p, 0, r),
                        (_SinTime[3] + 1.) / 2.
                    );

                    return d;
                }
                float map(float3 p) {
                    return min
                    (
                        sdf_animation(p, -float3 (1.25, 0, 0), 0.5), // Left sphere
                        sdf_sphere(p, +float3 (1.25, 0, 0), 0.5)  // Right sphere
                    );
                }
                //Takes a normal and the light source direction and uses the dot product
                //to adjust the colors to create shadows
                //right above 1 * color = normal color, 0*color = shadow
                fixed4 simpleLambert(fixed3 normal) {
                    fixed3 lightDir = _WorldSpaceLightPos0.xyz; // Light direction
                    fixed3 lightCol = _LightColor0.rgb; // Light color

                    fixed NdotL = max(dot(normal, lightDir), 0);
                    fixed4 c;
                    // Specular
                    fixed3 h = (lightDir - viewDirection) / 2.;
                    fixed s = pow(dot(normal, h), _SpecularPower) * _Gloss;
                    c.rgb = _Color * lightCol * NdotL + s;
                    c.a = 1;
                    return c;
                }
                //Generates a normal to the surface by sampling the surrounding area 
                //and getting the deltas from the center
                float3 normal(float3 p)
                {
                    const float eps = 0.01;

                    return normalize
                    (float3
                        (map(p + float3(eps, 0, 0)) - map(p - float3(eps, 0, 0)),
                            map(p + float3(0, eps, 0)) - map(p - float3(0, eps, 0)),
                            map(p + float3(0, 0, eps)) - map(p - float3(0, 0, eps))
                            )
                    );
                }

                v2f vert(appdata v) {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.wPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                    return o;
                }
                
                float sphereDistance(float3 p) {
                    return distance(p, _Centre) - _Radius;
                }
                fixed4 renderSurface(float3 p)
                {
                    float3 n = normal(p);
                    return simpleLambert(n);
                }

                fixed4 raymarch(float3 position, float3 direction) {
                    for (int i = 0; i < STEPS; i++) {
                        float distance = map(position);
                        if (distance < MIN_DISTANCE)
                            return renderSurface(position);
                        position += direction * distance;
                    }
                    return fixed4(1,1,1,1);
                }
                fixed4 frag(v2f i) : SV_Target
                {
                    float3 worldPosition = i.wPos;
                    viewDirection = normalize(i.wPos - _WorldSpaceCameraPos);
                    return raymarch(worldPosition, viewDirection);
                }
                ENDCG
            }
        }
}
