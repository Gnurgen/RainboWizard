Shader "Unlit/RainbowShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Speed ("Speed", float) = 1
		_Fireball ("Fireball", color) = (1,1,1,1)
		_FireballActive ("Fireball Active", int) = 0
		_MassFireball ("Mass Fireball", color) = (1,1,1,1)
		_MassFireballActive ("Mass Fireball Active", int) = 0
		_IceMine ("Ice Mine", color) = (1,1,1,1)
		_IceMineActive ("Ice Mine Active", int) = 0
		_Teleport ("Teleport", color) = (1,1,1,1)
		_TeleportActive ("Teleport Active", int) = 0
	}
	SubShader
	{
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.5

			struct appdata
			{
				float2 uv : TEXCOORD0;
				float4 vertex : POSITION;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 pos : SV_POSITION;
			};

			sampler2D _MainTex;
			float _Speed;

			v2f vert (appdata v)
			{
				// Basically just passes the position and texture along
				v2f o;
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = v.uv;
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// This is where the "magic" happens
				fixed4 col = tex2D(_MainTex, i.uv);
				return fixed4((col.x + _Time.y) % _Speed / _Speed, (col.y + _Time.y) % _Speed / _Speed, (col.z + _Time.y) % _Speed / _Speed, 0);

				//return col;
			}
			ENDCG
		}
	}
}
