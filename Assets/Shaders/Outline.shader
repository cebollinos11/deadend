Shader "Pablo/OutLine" {
	Properties{
		[PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
		_OutlineSize("Outline Size", Range(0.0, 0.1)) = 0.0 
		_OutlineColor("Outline Color", Color) = (1, 1, 1, 1)
		

	}
	SubShader
		{
			Tags{
				"Queue" = "Transparent"
				"IgnoreProjector" = "True"
				"RenderType" = "Transparent"
				"PreviewType" = "Plane"
				"CanUseSpriteAtlas" = "True"
			}
			Cull Off
			Lighting Off
			ZWrite Off
			Fog{ Mode Off }
			Blend One OneMinusSrcAlpha
			Pass
				{
					CGPROGRAM
#pragma vertex vert             
#pragma fragment frag             
//#pragma multi_compile DUMMY PIXELSNAP_ON             
#include "UnityCG.cginc"             
					struct appdata_t
					{
						float4 vertex   : POSITION;
						float4 color    : COLOR;
						float2 texcoord : TEXCOORD0;
					};

					struct v2f
					{
						float4 vertex   : SV_POSITION;
						fixed4 color : COLOR;
						half2 texcoord  : TEXCOORD0;
					};

					
					float _OutlineSize;
					float4 _OutlineColor;
					
					v2f vert(appdata_t IN)
					{
						v2f OUT;
						OUT.vertex = mul(UNITY_MATRIX_MVP, IN.vertex);
						OUT.texcoord = IN.texcoord;
						OUT.color = IN.color ;
/*
#ifdef PIXELSNAP_ON                 
						OUT.vertex = UnityPixelSnap(OUT.vertex);
#endif
						'*/
						return OUT;
					}

					

					sampler2D _MainTex;
					fixed4 frag(v2f IN) : SV_Target
					{
						fixed4 c = tex2D(_MainTex, IN.texcoord) * IN.color;
						
						c.rgb *= c.a;

						fixed4 t;

						if (c.a == 0)
						{
							t = tex2D(_MainTex, IN.texcoord+ float2(0.0,_OutlineSize));

							
							if (t.a > 0.0)
							{
								c = _OutlineColor;
								return c;
							}

							

							t = tex2D(_MainTex, IN.texcoord + float2(0.0, -_OutlineSize));
							if (t.a > 0.0)
							{
								c = _OutlineColor;
								return c;
							}

							t = tex2D(_MainTex, IN.texcoord + float2(_OutlineSize,0.0));
							if (t.a > 0.0)
							{
								c = _OutlineColor;
								return c;
							}

							t = tex2D(_MainTex, IN.texcoord + float2(-_OutlineSize, 0.0));
							if (t.a > 0.0)
							{
								c = _OutlineColor;
								return c;
							}
						}
							

						return c;
					}
						ENDCG

				}
		}
}