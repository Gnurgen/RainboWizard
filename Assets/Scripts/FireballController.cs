using UnityEngine;
using System.Collections;

public class FireballController : MonoBehaviour {

	private float speed;
	private float lifeTime;


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

	public void fire(GameObject shooter, Vector3 target, float speed, float lifeTime){
		owner = shooter;
		this.speed = speed;
		this.lifeTime = lifeTime;
		transform.position = shooter.transform.position;
		//transform.rotation = shooter.transform.rotation;

		Vector3 shooterpos = new Vector3 (shooter.transform.position.x, shooter.transform.position.y, -1);
		direction = target - shooterpos;
		direction = Vector3.Normalize(direction);
	}
}
