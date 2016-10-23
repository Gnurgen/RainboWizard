using UnityEngine;
using System.Collections;

public class SimpleAI : MonoBehaviour {

	public GameObject player;
	public float speed;
	private MeleeAbility meleeAbility;
	private FireballAbility fireballAbility;
	private Vector3 direction;

	// Use this for initialization
	void Start () {
		meleeAbility = GetComponent<MeleeAbility> ();
		fireballAbility = GetComponent<FireballAbility> ();
	}

	// Update is called once per frame
	void Update () {
		if (meleeAbility != null) {
			if (Vector3.Distance (transform.position, player.transform.position) < meleeAbility.range) {
				meleeAbility.UseAbility (player);
			} else {
				direction = Vector3.Normalize (player.transform.position - transform.position);
				direction = new Vector3 (direction.x, direction.y, 0);
				transform.position += direction * speed * Time.deltaTime;
			}
		}
		else if(fireballAbility != null){
			if (Vector3.Distance (transform.position, player.transform.position) < fireballAbility.range) {
				fireballAbility.UseAbility (player.transform.position);
			} else {
				direction = Vector3.Normalize (player.transform.position - transform.position);
				direction = new Vector3 (direction.x, direction.y, 0);
				transform.position += direction * speed * Time.deltaTime;
			}
		}
	}
}
