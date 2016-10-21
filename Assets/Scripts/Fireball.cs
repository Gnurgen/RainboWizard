using UnityEngine;
using System.Collections;

public class Fireball : MonoBehaviour {

	public GameObject fireballPrefab;
	public float cooldown;
	public float speed;
	public float lifeTime;
	public float damage;

	private float internalCooldown;

	// Use this for initialization
	void Start () {
		internalCooldown = 0;
	}
	
	// Update is called once per frame
	void Update () {
		internalCooldown = internalCooldown - Time.deltaTime;
	}

	public void UseAbility(Vector3 target){
		if(internalCooldown <= 0){
			GameObject shot = Instantiate (fireballPrefab) as GameObject;
			shot.GetComponent<FireballController> ().fire (gameObject, target, speed, lifeTime, damage);

			internalCooldown = cooldown;
		}
	}
}
