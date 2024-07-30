Shader "Unlit/EnemyHitFx"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        
        [ShowAsVector2] _CentreCoords("Hit Centre" ,Vector ) = (0.5,0.5,0,0)
        _CurrentRadius ("CurrentRadius", range(0,1)) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Transparent"
                "Queue" = "Transparent"}
        
        zwrite off 
        blend one one
        cull off 
        
        
        Pass
        {
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
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            float ParametricBlend(float t)
            {
                float sqr = t * t;
                 return sqr / ( (sqr - t) + 0.5f);
            }
            
            sampler2D _MainTex;
            float4 _MainTex_ST;
            float2 _CentreCoords;
            float _CurrentRadius;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                //fixed4 col = tex2D(_MainTex, i.uv);

                //fixed4 col = float4 ((i.uv.yyy),1) ;

                fixed4 radialMask = float4 ((distance(i.uv.x,_CentreCoords.x)+ distance(i.uv.y,_CentreCoords.y)).xxx/2,0)*3;
                
                fixed4 col = step(radialMask,_CurrentRadius/2);
                col *= 1-step(radialMask,ParametricBlend(_CurrentRadius/2-0.1));
                return col;
            }
            ENDCG
        }
    }
}
