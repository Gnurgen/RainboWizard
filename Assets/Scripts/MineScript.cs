using UnityEngine;
using System.Collections;

public class MineScript : MonoBehaviour {


	public GameObject shardPrefab;
	private float countDown;
	private bool started;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (started) {
			countDown = countDown - Time.deltaTime;
			if(countDown < 0){
				Explode ();
				Destroy (gameObject);
			}
		}
	}

	public void SetParameters(Vector3 pos, float countDown){
		transform.position = pos;
		this.countDown = countDown;
		started = true;
	}

	void Explode(){
		GameObject shard1 = Instantiate (shardPrefab) as GameObject;
		shard1.GetComponent<EffectController> ().setPositionDirection(transform.position, new Vector3(1,0,0));
		GameObject shard2 = Instantiate (shardPrefab) as GameObject;
		shard2.GetComponent<EffectController> ().setPositionDirection(transform.position, new Vector3(0,1,0));
		GameObject shard3 = Instantiate (shardPrefab) as GameObject;
		shard3.GetComponent<EffectController> ().setPositionDirection(transform.position, new Vector3(-1,0,0));
		GameObject shard4 = Instantiate (shardPrefab) as GameObject;
		shard4.GetComponent<EffectController> ().setPositionDirection(transform.position, new Vector3(0,-1,0));
		GameObject shard5 = Instantiate (shardPrefab) as GameObject;
		shard5.GetComponent<EffectController> ().setPositionDirection(transform.position, new Vector3(1,1,0));
		GameObject shard6 = Instantiate (shardPrefab) as GameObject;
		shard6.GetComponent<EffectController> ().setPositionDirection(transform.position, new Vector3(-1,-1,0));
		GameObject shard7 = Instantiate (shardPrefab) as GameObject;
		shard7.GetComponent<EffectController> ().setPositionDirection(transform.position, new Vector3(-1,1,0));
		GameObject shard8 = Instantiate (shardPrefab) as GameObject;
		shard8.GetComponent<EffectController> ().setPositionDirection(transform.position, new Vector3(1,-1,0));

		GameObject shard9 = Instantiate (shardPrefab) as GameObject;
		shard9.GetComponent<EffectController> ().setPositionDirection(transform.position, new Vector3(1,0.5f,0));
		GameObject shard10 = Instantiate (shardPrefab) as GameObject;
		shard10.GetComponent<EffectController> ().setPositionDirection(transform.position, new Vector3(1,-0.5f,0));
		GameObject shard11 = Instantiate (shardPrefab) as GameObject;
		shard11.GetComponent<EffectController> ().setPositionDirection(transform.position, new Vector3(0.5f,1,0));
		GameObject shard12 = Instantiate (shardPrefab) as GameObject;
		shard12.GetComponent<EffectController> ().setPositionDirection(transform.position, new Vector3(-0.5f,1,0));
		GameObject shard13 = Instantiate (shardPrefab) as GameObject;
		shard13.GetComponent<EffectController> ().setPositionDirection(transform.position, new Vector3(-1,0.5f,0));
		GameObject shard14 = Instantiate (shardPrefab) as GameObject;
		shard14.GetComponent<EffectController> ().setPositionDirection(transform.position, new Vector3(-1,-0.5f,0));
		GameObject shard15 = Instantiate (shardPrefab) as GameObject;
		shard15.GetComponent<EffectController> ().setPositionDirection(transform.position, new Vector3(0.5f,-1,0));
		GameObject shard16 = Instantiate (shardPrefab) as GameObject;
		shard16.GetComponent<EffectController> ().setPositionDirection(transform.position, new Vector3(-0.5f,-1,0));
	}
}
