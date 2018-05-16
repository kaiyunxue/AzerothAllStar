Shader "Custom/testShader" {
	Properties{
		_MainTex("Base (RGB)", 2D) = "white" {}
	_MainTint("Diffuse Tint", Color) = (1, 1, 1, 1)
		_ScrollXSpeed("X Scroll Speed", Range(0, 20)) = 2
		_ScrollYSpeed("Y Scroll Speed", Range(0, 20)) = 2
	}
		SubShader{
		Tags{ "RenderType" = "Opaque" }
		LOD 200

		CGPROGRAM
#pragma surface surf Lambert
		fixed4 _MainTint;
	fixed _ScrollXSpeed;
	fixed _ScrollYSpeed;
	sampler2D _MainTex;

	struct Input {
		float2 uv_MainTex;
	};

	void surf(Input IN, inout SurfaceOutput o) {
		fixed2 scrolledUV = IN.uv_MainTex;
		fixed xScrollValue = _ScrollXSpeed * _Time;
		fixed yScrollValue = _ScrollYSpeed * _Time;
		scrolledUV += fixed2(xScrollValue, yScrollValue);
		half4 c = tex2D(_MainTex, scrolledUV);
		o.Albedo = c.rgb * _MainTint;
		o.Alpha = c.a;
	}
	ENDCG
	}
		FallBack "Diffuse"
}