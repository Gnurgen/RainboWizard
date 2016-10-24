using UnityEngine;
using System.Collections;

public class MassFireballAbility : MonoBehaviour {


	public float range;
	public float damage;
	public float cooldown;
	public float speed;
	public GameObject prefab;

	private float currentCooldown;
	private GameObject fireball;

	// Use this for initialization
	void Start () {
		currentCooldown = 0;
	}

	// Update is called once per frame
	void Update () {
		currentCooldown = currentCooldown - Time.deltaTime;
	}

	public void UseAbility(){
		if(currentCooldown < 0){
			fireball = Instantiate (prefab) as GameObject;
			fireball.GetComponent<FireballControl> ().SetParameters (gameObject, new Vector3( transform.position.x + 1, 0, transform.position.z + 1), damage, range, speed);
			fireball = Instantiate (prefab) as GameObject;
			fireball.GetComponent<FireballControl> ().SetParameters (gameObject, new Vector3( transform.position.x - 1, 0, transform.position.z + 1), damage, range, speed);
			fireball = Instantiate (prefab) as GameObject;
			fireball.GetComponent<FireballControl> ().SetParameters (gameObject, new Vector3( transform.position.x + 1, 0, transform.position.z - 1), damage, range, speed);
			fireball = Instantiate (prefab) as GameObject;
			fireball.GetComponent<FireballControl> ().SetParameters (gameObject, new Vector3( transform.position.x - 1, 0, transform.position.z - 1), damage, range, speed);
			fireball = Instantiate (prefab) as GameObject;
			fireball.GetComponent<FireballControl> ().SetParameters (gameObject, new Vector3( transform.position.x + 1, 0, transform.position.z), damage, range, speed);
			fireball = Instantiate (prefab) as GameObject;
			fireball.GetComponent<FireballControl> ().SetParameters (gameObject, new Vector3( transform.position.x - 1, 0, transform.position.z), damage, range, speed);
			fireball = Instantiate (prefab) as GameObject;
			fireball.GetComponent<FireballControl> ().SetParameters (gameObject, new Vector3( transform.position.x, 0, transform.position.z - 1), damage, range, speed);
			fireball = Instantiate (prefab) as GameObject;
			fireball.GetComponent<FireballControl> ().SetParameters (gameObject, new Vector3( transform.position.x, 0, transform.position.z + 1), damage, range, speed);
			currentCooldown = cooldown;
		}
	}
}
