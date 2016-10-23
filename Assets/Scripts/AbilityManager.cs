using UnityEngine;
using System.Collections;

public class AbilityManager : MonoBehaviour {

	private MeleeAbility melee;
	private FireballAbility fireball;
	private MassFireballAbility massFireball;
	private TeleportAbility teleport;

	private float targetDist;

	// Use this for initialization
	void Start () {
		melee = GetComponent<MeleeAbility> ();
		fireball = GetComponent<FireballAbility> ();
		massFireball = GetComponent<MassFireballAbility> ();
		teleport = GetComponent<TeleportAbility> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// We will expand this to take all ability into account. Although
	public void attack(GameObject target){
		targetDist = Vector3.Distance (transform.position, target.transform.position);
		if(melee != null && targetDist < melee.range){
			melee.UseAbility (target);
		}
		if(fireball != null && targetDist < fireball.range){
			fireball.UseAbility (target.transform.position);
		}
	}
}
