using UnityEngine;
using System.Collections;

public class Teleport : MonoBehaviour {

	public float range;
	public float cooldown;

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

			if (Vector2.Distance (transform.position, target) < range) {
				transform.position = target;
			} else {
				Vector3 userpos = new Vector3 (transform.position.x, transform.position.y, -1);
				Vector3 direction = target - userpos;
				direction = Vector3.Normalize (direction);
				direction = direction * range;
				transform.position = transform.position + direction;
			}

			internalCooldown = cooldown;
		}
	}
}
