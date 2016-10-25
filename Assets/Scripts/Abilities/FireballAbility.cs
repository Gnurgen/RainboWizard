using UnityEngine;
using System.Collections;

public class FireballAbility : MonoBehaviour {

	public float range;
	public float damage;
	public float cooldown;
	public float speed;
	public GameObject prefab;

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
			GameObject fireball = Instantiate (prefab) as GameObject;
			fireball.GetComponent<FireballControl> ().SetParameters (gameObject, new Vector3(target.x, 0, target.z), damage, range, speed);
			currentCooldown = cooldown;
		}
	}

	public bool OnCooldown(){
		return currentCooldown >= 0;
	}
}
