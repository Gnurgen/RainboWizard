using UnityEngine;
using System.Collections;

public class AbilityManager : MonoBehaviour {

	public MeleeAbility melee;
    public FireballAbility fireball;
    public MassFireballAbility massFireball;
	public IceMineAbility iceMine;
    public TeleportAbility teleport;

	private float targetDist;

	// Use this for initialization
	void Start () {
		melee = GetComponent<MeleeAbility> ();
		fireball = GetComponent<FireballAbility> ();
		massFireball = GetComponent<MassFireballAbility> ();
		iceMine = GetComponent<IceMineAbility> ();
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
		if(massFireball != null && targetDist < massFireball.range){
			massFireball.UseAbility ();
		}
		if(iceMine != null && targetDist < iceMine.range){
			iceMine.UseAbility (target.transform.position);
		}
	}
}
