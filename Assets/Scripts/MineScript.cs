using UnityEngine;
using System.Collections;

public class MineScript : MonoBehaviour {


	public GameObject shardPrefab;
	private float countDown;
	private bool started;
	private float range;
	private float damage;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (started) {
			countDown = countDown - Time.deltaTime;
			if(countDown < 0){
				Explode ();
			}
		}
	}

	public void SetParameters(Vector3 pos, float countDown, float range, float damage){
		transform.position = pos;
		this.countDown = countDown;
		this.range = range;
		this.damage = damage;
		started = true;
	}

	void Explode(){

		// Explostion visual effect
		GameObject shard1 = Instantiate (shardPrefab) as GameObject;
		shard1.GetComponent<IceEffectController> ().setPositionDirection(transform.position, new Vector3(1,0,0), range);
		GameObject shard2 = Instantiate (shardPrefab) as GameObject;
		shard2.GetComponent<IceEffectController> ().setPositionDirection(transform.position, new Vector3(0,1,0), range);
		GameObject shard3 = Instantiate (shardPrefab) as GameObject;
		shard3.GetComponent<IceEffectController> ().setPositionDirection(transform.position, new Vector3(-1,0,0), range);
		GameObject shard4 = Instantiate (shardPrefab) as GameObject;
		shard4.GetComponent<IceEffectController> ().setPositionDirection(transform.position, new Vector3(0,-1,0), range);
		GameObject shard5 = Instantiate (shardPrefab) as GameObject;
		shard5.GetComponent<IceEffectController> ().setPositionDirection(transform.position, new Vector3(1,1,0), range);
		GameObject shard6 = Instantiate (shardPrefab) as GameObject;
		shard6.GetComponent<IceEffectController> ().setPositionDirection(transform.position, new Vector3(-1,-1,0), range);
		GameObject shard7 = Instantiate (shardPrefab) as GameObject;
		shard7.GetComponent<IceEffectController> ().setPositionDirection(transform.position, new Vector3(-1,1,0), range);
		GameObject shard8 = Instantiate (shardPrefab) as GameObject;
		shard8.GetComponent<IceEffectController> ().setPositionDirection(transform.position, new Vector3(1,-1,0), range);

		GameObject shard9 = Instantiate (shardPrefab) as GameObject;
		shard9.GetComponent<IceEffectController> ().setPositionDirection(transform.position, new Vector3(1,0.5f,0), range);
		GameObject shard10 = Instantiate (shardPrefab) as GameObject;
		shard10.GetComponent<IceEffectController> ().setPositionDirection(transform.position, new Vector3(1,-0.5f,0), range);
		GameObject shard11 = Instantiate (shardPrefab) as GameObject;
		shard11.GetComponent<IceEffectController> ().setPositionDirection(transform.position, new Vector3(0.5f,1,0), range);
		GameObject shard12 = Instantiate (shardPrefab) as GameObject;
		shard12.GetComponent<IceEffectController> ().setPositionDirection(transform.position, new Vector3(-0.5f,1,0), range);
		GameObject shard13 = Instantiate (shardPrefab) as GameObject;
		shard13.GetComponent<IceEffectController> ().setPositionDirection(transform.position, new Vector3(-1,0.5f,0), range);
		GameObject shard14 = Instantiate (shardPrefab) as GameObject;
		shard14.GetComponent<IceEffectController> ().setPositionDirection(transform.position, new Vector3(-1,-0.5f,0), range);
		GameObject shard15 = Instantiate (shardPrefab) as GameObject;
		shard15.GetComponent<IceEffectController> ().setPositionDirection(transform.position, new Vector3(0.5f,-1,0), range);
		GameObject shard16 = Instantiate (shardPrefab) as GameObject;
		shard16.GetComponent<IceEffectController> ().setPositionDirection(transform.position, new Vector3(-0.5f,-1,0), range);

		Destroy (gameObject);
	}
}
