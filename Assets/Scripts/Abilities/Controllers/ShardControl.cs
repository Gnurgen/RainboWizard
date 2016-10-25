using UnityEngine;
using System.Collections;

public class ShardControl : MonoBehaviour {

	public float speed;

	private float range;
	private Vector3 initialPosition;
	private Vector3 direction;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = transform.position + (direction * speed * Time.deltaTime);
		if(Vector3.Distance(transform.position, initialPosition) > range){
			Destroy (gameObject);
		}
	}

	public void SetParameters(Vector3 pos, Vector3 dir, float range){
		transform.position = pos;
		initialPosition = pos;
		direction = Vector3.Normalize (dir);
		this.range = range;
	}
}
