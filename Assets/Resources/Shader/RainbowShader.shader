Shader "Unlit/RainbowShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Speed ("Speed", float) = 1
		_Colors ("Colors", float) = 2
		_Fireball ("Fireball", color) = (1,1,1,1)
		_MassFireball ("Mass Fireball", color) = (1,1,1,1)
		_IceMine ("Ice Mine", color) = (1,1,1,1)
		_Teleport ("Teleport", color) = (1,1,1,1)
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
			float _Colors;
			fixed4 _Fireball;
			fixed4 _MassFireball;
			fixed4 _IceMine;
			fixed4 _Teleport;

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
				fixed4 blend = fixed4(1,1,1,1);
				//return fixed4((col.x + _Time.y) % _Speed / _Speed, (col.y + _Time.y) % _Speed / _Speed, (col.z + _Time.y) % _Speed / _Speed, 0);
				float choose = (_Time.y * _Speed) % _Colors;
				if(choose >= 0 && choose <= 1){
					blend = _Fireball;
				}
				else if (choose > 1 && choose <= 2){
					blend = _MassFireball;
				}
				else if (choose > 2 && choose <= 3){
					blend = _IceMine;
				}
				else if (choose > 3 && choose <= 4){
					blend = _Teleport;
				}
				return atan2(blend, col);
			}
			ENDCG
		}
	}
}
