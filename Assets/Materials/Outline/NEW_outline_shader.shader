Shader "Unlit/NEW_outline_shader"
{
    Properties
    {
        _Color("Main color", Color) = (0.5,0.5,0.5,1)
        _MainTex ("Texture", 2D) = "white" {}
        _OutlineColor("Outline color", Color) = (0,0,0,1)
        _OutlineWidth("Outline width", Range(1.0,5.0)) = 1.01
    }

    CGINCLUDE
    #include "UnityCG.cginc"

    struct appdata_outline
    {
        float4 vertex : POSITION;
        float3 normal : NORMAL;
    };

    struct v2f_outline
    {
        float4 pos : POSITION;
        float3 normal : NORMAL;
    };

    float _OutlineWidth;
    float4 _OutlineColor;

    v2f_outline vert(appdata_outline v)
    {
        v.vertex.xyz *= _OutlineWidth;

        v2f_outline o;
        o.pos = UnityObjectToClipPos(v.vertex);
        return o;
    }

    ENDCG

    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass // Render the Outline
        {
            ZWrite Off

            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            //float4 _OutlineColor;

            half4 frag(v2f_outline i) : COLOR
            {
                return _OutlineColor;
            }

            ENDCG
        }


        // Pass
        // {
        //     CGPROGRAM
        //     #pragma vertex vert
        //     #pragma fragment frag
        //     // make fog work
        //     #pragma multi_compile_fog

        //     #include "UnityCG.cginc"

        //     struct appdata
        //     {
        //         float4 vertex : POSITION;
        //         float2 uv : TEXCOORD0;
        //     };

        //     struct v2f
        //     {
        //         float2 uv : TEXCOORD0;
        //         UNITY_FOG_COORDS(1)
        //         float4 vertex : SV_POSITION;
        //     };

        //     sampler2D _MainTex;
        //     float4 _MainTex_ST;

        //     v2f vert (appdata v)
        //     {
        //         v2f o;
        //         o.vertex = UnityObjectToClipPos(v.vertex);
        //         o.uv = TRANSFORM_TEX(v.uv, _MainTex);
        //         UNITY_TRANSFER_FOG(o,o.vertex);
        //         return o;
        //     }

        //     fixed4 frag (v2f i) : SV_Target
        //     {
        //         // sample the texture
        //         fixed4 col = tex2D(_MainTex, i.uv);
        //         // apply fog
        //         UNITY_APPLY_FOG(i.fogCoord, col);
        //         return col;
        //     }
        //     ENDCG
        // }
    }
}
