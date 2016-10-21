using UnityEngine;
using System.Collections;

public class IceEffectController : MonoBehaviour {

	public float speed;
	private bool started;
	private Vector3 direction;
	private Vector3 initialPosition;
	private float range;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if(started){
			transform.position = transform.position + (direction * speed * Time.deltaTime);
			if(Vector3.Distance(transform.position, initialPosition) > range){
				Destroy (gameObject);
			}
		}
	}

	public void setPositionDirection(Vector3 pos, Vector3 dir, float range){
		transform.position = pos;
		initialPosition = pos;
		direction = dir;
		this.range = range;
		started = true;
	}
}
