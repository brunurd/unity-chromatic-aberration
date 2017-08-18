Shader "Hidden/ChromaticAberration" {

	Properties {
		_MainTex ("Texture", 2D) = "white" {}
		_ChromaticAberration ("Chromatic Aberration", Range(0.0,1.0)) = 0.001
		_Center ("Center",Range(0.0,0.5)) = 0.0
	}

	SubShader {
		Cull off
		Blend srcAlpha OneMinusSrcAlpha

		Pass {
			Name "Chromatic Aberration"
			
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata {
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f {
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			uniform sampler2D _MainTex;
			fixed _ChromaticAberration;
			fixed _Center;

			v2f vert (appdata v) {
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}

			fixed4 frag (v2f i) : COLOR {
				float2 rectangle = float2(i.uv.x - _Center, i.uv.y - _Center);
				float dist = sqrt(pow(rectangle.x,2) + pow(rectangle.y,2));

				float mov = _ChromaticAberration * dist;
				float2 uvR = float2(i.uv.x - mov, i.uv.y);
				float2 uvG = float2(i.uv.x + mov, i.uv.y);
				float2 uvB = float2(i.uv.x, i.uv.y - mov);
				fixed4 colR = tex2D(_MainTex, uvR);
				fixed4 colG = tex2D(_MainTex, uvG);
				fixed4 colB = tex2D(_MainTex, uvB);
				return fixed4(colR.r, colG.g, colB.b, 0.8f);
			}
			ENDCG
		}
	}
}
