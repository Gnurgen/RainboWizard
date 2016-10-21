using UnityEngine;
using System.Collections;

public class IceMine : MonoBehaviour {

	public GameObject minePrefab;
	public float range;
	public float cooldown;
	public float mineCountdown;
	public float explosionRange;
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
			if (Vector3.Distance (transform.position, target) < range) {
				GameObject mine = Instantiate (minePrefab) as GameObject;
				mine.GetComponent<MineScript> ().SetParameters(target, mineCountdown, explosionRange, damage);
			} else {
				Vector3 userpos = new Vector3 (transform.position.x, transform.position.y, -1);
				Vector3 direction = target - userpos;
				direction = Vector3.Normalize (direction);
				direction = direction * range;
				GameObject mine = Instantiate (minePrefab) as GameObject;
				mine.GetComponent<MineScript> ().SetParameters (transform.position + direction, mineCountdown, explosionRange, damage);
			}
			internalCooldown = cooldown;
		}
	}
}
