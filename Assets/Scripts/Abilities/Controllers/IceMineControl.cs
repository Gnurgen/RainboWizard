using UnityEngine;
using System.Collections;

public class IceMineControl : MonoBehaviour {

	public GameObject shardPrefab;

	private float explosionRange;
	private float delay;
	private float damage;

	private float currentDelay;
	private GameObject shard;
	private GameObject owner;

	// Use this for initialization
	void Start () {
		currentDelay = 1;
	}
	
	// Update is called once per frame
	void Update () {
		currentDelay = currentDelay - Time.deltaTime;
		if(currentDelay < 0){
			Explode ();
		}
	}

	public void SetParamters(Vector3 pos, float explosionRange, float delay, float damage, GameObject owner){
		transform.position = pos;
		this.explosionRange = explosionRange;
		this.delay = delay;
		this.damage = damage;
		this.owner = owner;
		currentDelay = delay;
	}

	void Explode(){
		Collider[] hits = Physics.OverlapSphere (transform.position, explosionRange);
		for(int i = 0; i < hits.Length; i++){
			if(hits[i].tag == "Monster" || hits[i].tag == "Player" && hits[i].gameObject != owner){
				hits [i].GetComponent<Health> ().takeDamage (damage);
			}
		}

		shard = Instantiate (shardPrefab) as GameObject;
		shard.GetComponent<ShardControl> ().SetParameters (transform.position, new Vector3 (1, 0, 0), explosionRange);
		shard = Instantiate (shardPrefab) as GameObject;
		shard.GetComponent<ShardControl> ().SetParameters (transform.position, new Vector3 (0, 0, 1), explosionRange);
		shard = Instantiate (shardPrefab) as GameObject;
		shard.GetComponent<ShardControl> ().SetParameters (transform.position, new Vector3 (-1, 0, 0), explosionRange);
		shard = Instantiate (shardPrefab) as GameObject;
		shard.GetComponent<ShardControl> ().SetParameters (transform.position, new Vector3 (0, 0, -1), explosionRange);
		shard = Instantiate (shardPrefab) as GameObject;
		shard.GetComponent<ShardControl> ().SetParameters (transform.position, new Vector3 (1, 0, 1), explosionRange);
		shard = Instantiate (shardPrefab) as GameObject;
		shard.GetComponent<ShardControl> ().SetParameters (transform.position, new Vector3 (1, 0, -1), explosionRange);
		shard = Instantiate (shardPrefab) as GameObject;
		shard.GetComponent<ShardControl> ().SetParameters (transform.position, new Vector3 (-1, 0, 1), explosionRange);
		shard = Instantiate (shardPrefab) as GameObject;
		shard.GetComponent<ShardControl> ().SetParameters (transform.position, new Vector3 (-1, 0, -1), explosionRange);

		Destroy (gameObject);
	}
}
