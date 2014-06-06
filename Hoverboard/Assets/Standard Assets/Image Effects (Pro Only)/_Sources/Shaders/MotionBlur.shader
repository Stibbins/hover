Shader "Hidden/MotionBlur" {
Properties {
	_MainTex ("Base (RGB)", 2D) = "white" {}
	_AccumOrig("AccumOrig", Float) = 0.65
	_Size("Size",Float) = 1
}

    SubShader { 
		ZTest Always Cull Off ZWrite Off
		Fog { Mode off }
		Pass {
			Blend SrcAlpha OneMinusSrcAlpha
			ColorMask RGB
		    BindChannels { 
				Bind "vertex", vertex 
				Bind "texcoord", texcoord
			} 
		
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma fragmentoption ARB_precision_hint_fastest
	
			#include "UnityCG.cginc"
	
			struct appdata_t {
				float4 vertex : POSITION;
				float2 texcoord : TEXCOORD;
			};
	
			struct v2f {
				float4 vertex : POSITION;
				float2 texcoord : TEXCOORD;
			};
			
			float4 _MainTex_ST;
			float _AccumOrig;
			float _Size;
			
			v2f vert (appdata_t v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
				return o;
			}
	
			sampler2D _MainTex;
			
			half4 frag (v2f i) : COLOR
			{
				float d=0;
				d = distance(i.texcoord,(0.5,0.5)) * _Size;
				//half4(tex2D(_MainTex, i.texcoord).rgb, _AccumOrig )
				
				return half4(tex2D(_MainTex, i.texcoord).rgb, (_AccumOrig / d));
				//half4(tex2D(_MainTex, i.texcoord).rgb, (_AccumOrig * c.a) *0.5 ) ;
			}
			ENDCG 
		} 

		Pass {
			Blend One Zero
			ColorMask A
			
		    BindChannels { 
				Bind "vertex", vertex 
				Bind "texcoord", texcoord
			} 
		
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma fragmentoption ARB_precision_hint_fastest
	
			#include "UnityCG.cginc"
	
			struct appdata_t {
				float4 vertex : POSITION;
				float2 texcoord : TEXCOORD;
			};
	
			struct v2f {
				float4 vertex : POSITION;
				float2 texcoord : TEXCOORD;
			};
			
			float4 _MainTex_ST;
			
			v2f vert (appdata_t v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
				return o;
			}
	
			sampler2D _MainTex;
			
			half4 frag (v2f i) : COLOR
			{
				return tex2D(_MainTex, i.texcoord);
			}
			ENDCG 
		}
		
	}

SubShader {
	ZTest Always Cull Off ZWrite Off
	Fog { Mode off }
	Pass {
		Blend SrcAlpha OneMinusSrcAlpha
		ColorMask RGB
		SetTexture [_MainTex] {
			ConstantColor (0,0,0,[_AccumOrig])
			Combine texture, constant
		}
	}
	Pass {
		Blend One Zero
		ColorMask A
		SetTexture [_MainTex] {
			Combine texture
		}
	}
}

Fallback off

}