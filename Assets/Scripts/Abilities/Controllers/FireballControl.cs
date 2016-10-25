using UnityEngine;
using System.Collections;

public class FireballControl : MonoBehaviour {

	private GameObject owner;
	private float damage;
	private float range;
	private float speed;
	private Vector3 startPos;
	public Vector3 dir;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = transform.position + (dir * speed * Time.deltaTime);
		if(Vector3.Distance(transform.position, startPos) > range){
			Destroy (gameObject);
		}
	}

	public void SetParameters(GameObject owner, Vector3 target, float damage, float range, float speed){
		this.owner = owner;
		this.damage = damage;
		this.range = range;
		this.speed = speed;
		startPos = owner.transform.position;
		transform.position = owner.transform.position;

		// Calculate the direction to manually move in, and ensure y is not changed when moving;
		dir = target - owner.transform.position;
		dir = Vector3.Normalize (dir);
		dir = new Vector3 (dir.x, 0, dir.z);
	}

	void OnTriggerEnter(Collider col){
		if(col.gameObject.tag != owner.tag){
			if(col.gameObject.tag == "Monster" || col.gameObject.tag == "Player"){
				col.gameObject.GetComponent<Health> ().takeDamage (damage);
				Destroy (gameObject);
			}
		}
		if(col.gameObject.tag == "Obstacle"){
			Destroy (gameObject);
		}
	}
}
