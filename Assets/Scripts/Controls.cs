using UnityEngine;
using System.Collections;

public class Controls : MonoBehaviour {

	public float speed;
	public string teleportButton;
	public string icemineButton;
	private float newX;
	private float newY;

	private Fireball fireball;
	private Teleport teleport;
	private IceMine icemine;

	// Use this for initialization
	void Start () {
		fireball = GetComponent<Fireball> ();
		teleport = GetComponent<Teleport> ();
		icemine = GetComponent<IceMine> ();
	}
	
	void Update () {
		newX = transform.position.x;
		newY = transform.position.y;
		if(Input.GetKey("w")){
			newY = newY + (speed * Time.deltaTime);
		}
		if(Input.GetKey("a")){
			newX = newX - (speed * Time.deltaTime);
		}
		if(Input.GetKey("s")){
			newY = newY - (speed * Time.deltaTime);
		}
		if(Input.GetKey("d")){
			newX = newX + (speed * Time.deltaTime);
		}
		transform.position = new Vector3 (newX, newY, -1);
		if(Input.GetMouseButtonDown(0)){
			Vector3 mousepos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			mousepos = new Vector3 (mousepos.x, mousepos.y, -1);
			fireball.UseAbility (mousepos);
		}
		if(Input.GetKeyDown(teleportButton)){
			Vector3 mousepos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			mousepos = new Vector3 (mousepos.x, mousepos.y, -1);
			teleport.UseAbility(mousepos);
		}
		if(Input.GetKeyDown(icemineButton)){
			Vector3 mousepos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			mousepos = new Vector3 (mousepos.x, mousepos.y, -1);
			icemine.UseAbility(mousepos);
		}
		//transform.LookAt (Input.mousePosition);
		//transform.right = Input.mousePosition - transform.position;
		Vector3 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
		float angle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis (angle, Vector3.forward);
		//transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards (transform.position, Camera.main.ScreenToWorldPoint (Input.mousePosition),1f,0.0f));
	}
}
