using UnityEngine;
using System.Collections;

public class EffectController : MonoBehaviour {

	public float speed;
	private bool started;
	public float lifeTime;
	private Vector3 direction;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if(started){
			transform.position = transform.position + (direction * speed * Time.deltaTime);
			lifeTime = lifeTime - Time.deltaTime;
			if(lifeTime < 0){
				Destroy (gameObject);
			}
		}
	}

	public void setPositionDirection(Vector3 pos, Vector3 dir){
		transform.position = pos;
		direction = dir;
		started = true;
	}
}
