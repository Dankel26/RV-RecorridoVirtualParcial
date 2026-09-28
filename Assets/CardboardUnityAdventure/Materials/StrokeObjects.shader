Shader "Custom/PermanentOutline"
{
    Properties
    {
        _OutlineColor ("Color del Borde", Color) = (0, 1, 1, 1)
        _OutlineWidth ("Grosor del Borde", Range(0.0001, 0.01)) = 0.002
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue"="Geometry+1" }

        Pass
        {
            Name "OUTLINE"
            Cull Front
            ZWrite On
            ZTest LEqual

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
            };

            float4 _OutlineColor;
            float _OutlineWidth;

            v2f vert(appdata v)
            {
                v2f o;
                // Convertir a Clip Space
                float4 norm = float4(UnityObjectToWorldNormal(v.normal), 0.0);
                float4 projNorm = mul(UNITY_MATRIX_VP, norm);
                
                o.pos = UnityObjectToClipPos(v.vertex);
                // Extruir en espacio de proyección proporcional a la profundidad
                o.pos.xy += normalize(projNorm.xy) * _OutlineWidth * o.pos.w;
                return o;
            }

            fixed4 frag(v2f i) : COLOR
            {
                return _OutlineColor;
            }
            ENDCG
        }
    }
}