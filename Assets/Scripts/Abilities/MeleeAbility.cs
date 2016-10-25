using UnityEngine;
using System.Collections;

public class MeleeAbility : MonoBehaviour {

	public float range;
	public float damage;
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

	public void UseAbility(GameObject target){
		if(currentCooldown < 0 && (target.tag == "Player" || target.tag == "Monster") && Vector3.Distance(transform.position, target.transform.position) < range){
			target.GetComponent<Health> ().takeDamage (damage);
			currentCooldown = cooldown;
		}
	}
}
