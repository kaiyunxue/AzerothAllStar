// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/SingleColor"
{
	Properties{
		_Color("Color", Color) = (1.0,1.0,1.0,1.0)
		_MainTex("Diffuse Texture", 2D) = "white" {}
	}
		SubShader{
		// 1.) This will be the base forward rendering pass in which ambient, vertex, and
		// main directional light will be applied. Additional lights will need additional passes
		// using the "ForwardAdd" lightmode.
		// see: http://docs.unity3d.com/Manual/SL-PassTags.html
		Tags{ "LightMode" = "ForwardBase" "RenderType" = "Opaque" }
		Lighting Off

		Pass
	{
		CGPROGRAM
#include "UnityCG.cginc"
#pragma vertex vert
#pragma fragment frag

		// 2.) This matches the "forward base" of the LightMode tag to ensure the shader compiles
		// properly for the forward bass pass. As with the LightMode tag, for any additional lights
		// this would be changed from _fwdbase to _fwdadd.
#pragma multi_compile_fwdbase

		// 3.) Reference the Unity library that includes all the lighting shadow macros
#include "AutoLight.cginc"

		// User defined variables
		uniform fixed4 _Color;
		uniform sampler2D _MainTex;
		uniform half4 _MainTex_ST;

	// Base input structs
	struct vertexOutput
	{
		fixed4 color : COLOR;			// vertex color
		fixed4 pos : SV_POSITION;
		fixed2 tex[2] : TEXCOORD0;

		// 4.) The LIGHTING_COORDS macro (defined in AutoLight.cginc) defines the parameters needed to sample 
		// the shadow map. The (0,1) specifies which unused TEXCOORD semantics to hold the sampled values - 
		// As I'm not using any texcoords in this shader, I can use TEXCOORD0 and TEXCOORD1 for the shadow 
		// sampling. If I was already using TEXCOORD for UV coordinates, say, I could specify
		// LIGHTING_COORDS(1,2) instead to use TEXCOORD1 and TEXCOORD2.
		LIGHTING_COORDS(4,5)
	};

	// Vertex function 
	vertexOutput vert(appdata_full v)
	{
		vertexOutput o;
		UNITY_INITIALIZE_OUTPUT(vertexOutput, o);
		o.pos = UnityObjectToClipPos(v.vertex);
		o.tex[0] = TRANSFORM_TEX(v.texcoord, _MainTex);
		o.color = v.color;	// vertex color

							// 5.) The TRANSFER_VERTEX_TO_FRAGMENT macro populates the chosen LIGHTING_COORDS in the v2f structure
							// with appropriate values to sample from the shadow/lighting map
		TRANSFER_VERTEX_TO_FRAGMENT(o);

		return o;
	}

	// Fragment (pixel) function
	fixed4 frag(vertexOutput i) : COLOR
	{
		// 6.) The LIGHT_ATTENUATION samples the shadowmap (using the coordinates calculated by TRANSFER_VERTEX_TO_FRAGMENT
		// and stored in the structure defined by LIGHTING_COORDS), and returns the value as a float.
		fixed4 shadowColor = saturate(SHADOW_ATTENUATION(i) / (1 - (UNITY_LIGHTMODEL_AMBIENT * (1 - SHADOW_ATTENUATION(i)))));
	return _Color * tex2D(_MainTex, i.tex[0] * _MainTex_ST.xy + _MainTex_ST.zw) * shadowColor;
	}

		ENDCG
	}
	}
		// 7.) To receive or cast a shadow, shaders must implement the appropriate "Shadow Collector" or "Shadow Caster" pass.
		// Although we haven't explicitly done so in this shader, if these passes are missing they will be read from a fallback
		// shader instead, so specify one here to import the collector/caster passes used in that fallback.
		Fallback "VertexLit"
}
