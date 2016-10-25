using UnityEngine;
using System.Collections;

public class IceMineAbility : MonoBehaviour {

	public float range;
	public float cooldown;
	public float delay;
	public float explosionRange;
	public float damage;
	public GameObject minePrefab;

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
			Vector3 realTarget = new Vector3 (target.x, 0, target.z);
			if(Vector3.Distance(transform.position,realTarget) < range){
				GameObject newMine = Instantiate (minePrefab) as GameObject;
				newMine.GetComponent<IceMineControl> ().SetParamters (realTarget,explosionRange,delay,damage, gameObject);
			}
			else {
				Vector3 direction = realTarget - transform.position;
				direction = Vector3.Normalize (direction);
				direction = direction * range;
				GameObject newMine = Instantiate (minePrefab) as GameObject;
				newMine.GetComponent<IceMineControl> ().SetParamters (direction, explosionRange, delay, damage, gameObject);
			}
			currentCooldown = cooldown;
		}
	}
}
