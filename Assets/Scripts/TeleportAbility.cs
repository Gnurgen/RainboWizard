using UnityEngine;
using System.Collections;

public class TeleportAbility : MonoBehaviour {

	public float range;
	public float cooldown;

	private float currentCooldown;

	// Use this for initialization
	void Start () {
		currentCooldown = 0;
	}
	
	// Update is called once per frame
	void Update () {
		currentCooldown = currentCooldown - Time.deltaTime;
	}

	public void UseAbility(Vector3 target){
		if(currentCooldown < 0){
			if(Vector3.Distance(transform.position, target) < range){
				transform.position = target;
			}
			else{
				Vector3 direction = Vector3.Normalize(target - transform.position) * range;
				direction = new Vector3 (direction.x, direction.y, -1);
				transform.position = transform.position + direction;
			}
			currentCooldown = cooldown;
		}
	}
}
