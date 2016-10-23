using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

	// Movement speed
	public float speed;
	// Keys for abilities
	public string massFireballKey;
	public string iceMineKey;
	public string teleportKey;

	// Used for movement
	private float newX;
	private float newZ;

	private FireballAbility fireballAbility;
	private MassFireballAbility massFireballAbility;

	private TeleportAbility teleportAbility;

	// Use this for initialization
	void Start () {
		fireballAbility = GetComponent<FireballAbility> ();
		massFireballAbility = GetComponent<MassFireballAbility> ();

		teleportAbility = GetComponent<TeleportAbility> ();
	}
	
	// Update is called once per frame
	void Update () {

		// Player movement
		newX = transform.position.x;
		newZ = transform.position.z;
		if(Input.GetKey("w")){
			newZ += speed * Time.deltaTime;
		}
		if(Input.GetKey("a")){
			newX -= speed * Time.deltaTime;
		}
		if(Input.GetKey("s")){
			newZ -= speed * Time.deltaTime;
		}
		if(Input.GetKey("d")){
			newX += speed * Time.deltaTime;
		}
		transform.position = new Vector3 (newX, 0, newZ);

		// Facing the mouse
		Vector3 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
		float angle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis (angle-90, Vector3.forward);

		// Abilities
		if(fireballAbility != null && Input.GetMouseButtonDown(0)){
			fireballAbility.UseAbility (Camera.main.ScreenToWorldPoint (Input.mousePosition));
		}
		if(massFireballAbility != null && Input.GetKey(massFireballKey)){
			massFireballAbility.UseAbility ();
		}
		if(teleportAbility != null && Input.GetKey(teleportKey)){
			teleportAbility.UseAbility (Camera.main.ScreenToWorldPoint(Input.mousePosition));
		}
	}
}
