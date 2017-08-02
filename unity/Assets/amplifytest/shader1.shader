// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "shader1"
{
	Properties
	{
		[HideInInspector] __dirty( "", Int ) = 1
		_Float0("Float 0", Range( 0 , 1)) = 0
		_smooth("smooth", Range( 0 , 1)) = 0
		_specular("specular", Range( 0 , 1)) = 0
		_711("711", 2D) = "white" {}
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf StandardSpecular keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _711;
		uniform float4 _711_ST;
		uniform float _Float0;
		uniform float _specular;
		uniform float _smooth;

		void surf( Input i , inout SurfaceOutputStandardSpecular o )
		{
			float2 uv_711 = i.uv_texcoord * _711_ST.xy + _711_ST.zw;
			o.Normal = tex2D( _711,uv_711).xyz;
			o.Albedo = lerp( float4(0.3161765,0.3045523,0.3045523,0) , float4(0.7358161,0.8455882,0.3481834,0) , _Float0 ).rgb;
			float3 temp_cast_2 = (_specular).xxx;
			o.Specular = temp_cast_2;
			o.Smoothness = _smooth;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=7003
87;206;1740;930;872;465;1;True;True
Node;AmplifyShaderEditor.ColorNode;3;-531,-148;Float;False;Constant;_Color1;Color 1;0;0;0.7358161,0.8455882,0.3481834,0;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;5;-599,48;Float;False;Property;_Float0;Float 0;0;0;0;0;1;FLOAT
Node;AmplifyShaderEditor.ColorNode;2;-521,-367;Float;False;Constant;_Color0;Color 0;0;0;0.3161765,0.3045523,0.3045523,0;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;7;-617,342;Float;False;Property;_specular;specular;2;0;0;0;1;FLOAT
Node;AmplifyShaderEditor.LerpOp;1;-209,23;Float;False;0;COLOR;0.0;False;1;COLOR;0.0,0,0,0;False;2;FLOAT;0.0;False;COLOR
Node;AmplifyShaderEditor.RangedFloatNode;6;-590,147;Float;False;Property;_smooth;smooth;1;0;0;0;1;FLOAT
Node;AmplifyShaderEditor.SamplerNode;8;-215,-132;Float;True;Property;_711;711;3;0;Assets/711.png;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;FLOAT4;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;2;Float;ASEMaterialInspector;0;StandardSpecular;shader1;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;0;False;0;0;Opaque;0.5;True;True;0;False;Opaque;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;False;0;4;10;25;False;0.5;True;0;Zero;Zero;0;Zero;Zero;Add;Add;0;False;0;0,0,0,0;VertexOffset;False;Cylindrical;Relative;0;;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0.0;False;5;FLOAT;0.0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0.0;False;9;FLOAT;0.0;False;10;OBJECT;0.0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;13;OBJECT;0.0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False
WireConnection;1;0;2;0
WireConnection;1;1;3;0
WireConnection;1;2;5;0
WireConnection;0;0;1;0
WireConnection;0;1;8;0
WireConnection;0;3;7;0
WireConnection;0;4;6;0
ASEEND*/
//CHKSM=FAD4CFC43890DA937161E18B43ACCCA575A2758E