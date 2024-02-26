Shader "Custom/TestUnlitShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _CustomColor ("Custom Color", Color) = (1, 1, 1, 1)
        _Slider ("Slider", Range(0, 1)) = 0
    }
    SubShader
    {
        Tags {
            "RenderType"="Opaque"
            "Queue"="Geometry"
        }
        LOD 100
        
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normals : NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
                float3 normals : NORMAL;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            float4 _CustomColor; // même nom que la propriété déclarée en haut
            fixed _Slider;

            v2f vert (appdata v)
            {
                v.vertex.xyz += v.normals.xyz * _Slider * saturate(v.normals.z);
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                o.normals = v.normals;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);

                //----

                col.rgb = i.normals.xyz * 0.5 + 0.5;

                //fixed4 white = fixed4(1, 1, 1, 1);
                //col = lerp(_CustomColor, white, _Slider);

                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
