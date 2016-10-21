using UnityEngine;
using System.Collections;

public class FireballController : MonoBehaviour {

	private float speed;
	private float lifeTime;
	private float damage;

	private float internalLifeTime;
	private GameObject owner;

	private Vector3 direction;

	// Use this for initialization
	void Start () {
	}
	
	void FixedUpdate () {
		transform.position = transform.position + (direction * speed * Time.deltaTime);
		lifeTime = lifeTime - Time.deltaTime;
		if (lifeTime < 0){
			Destroy (gameObject);
		}
	}

	public void fire(GameObject shooter, Vector3 target, float speed, float lifeTime, float damage){
		owner = shooter;
		this.speed = speed;
		this.lifeTime = lifeTime;
		this.damage = damage;
		transform.position = shooter.transform.position;
		//transform.rotation = shooter.transform.rotation;

		Vector3 shooterpos = new Vector3 (shooter.transform.position.x, shooter.transform.position.y, -1);
		direction = target - shooterpos;
		direction = Vector3.Normalize(direction);
	}

	void OnTriggerEnter(Collider coll){
		if(coll.gameObject == owner){
			Debug.Log ("Same!");
			return;
		}
		if(coll.gameObject.tag == "Monster" || coll.gameObject.tag == "Player"){
			coll.gameObject.GetComponent<Health> ().takeDamage (damage);
			Destroy (gameObject);
		}
		else{
			Destroy (gameObject);
		}
	}
}
