using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public int enemiesToKill;
	public int rangedActive;
	public int meleeActive;
	public float timeLimit;

	public GameObject melee;
	public GameObject ranged;
	public GameObject boss;

	private float currentTimeLimit;
	private int enemiesKilled;

	private List<GameObject> meleeEnemies;
	private List<GameObject> rangedEnemies;
	private GameObject bossInstance;

	// Use this for initialization
	void Start () {
		meleeEnemies = new List<GameObject> ();
		rangedEnemies = new List<GameObject> ();
		for(int i = 0; i < meleeActive; i++){
			CreateRandomMelee ();
		}
		for(int i = 0; i < rangedActive; i++){
			CreateRandomRanged ();
		}
		enemiesKilled = 0;
	}
	
	// Update is called once per frame
	void Update () {
		currentTimeLimit = currentTimeLimit - Time.deltaTime;
		if(currentTimeLimit < 0){
			// Lost
		}
	}

	public void RangedKilled(GameObject mob){
		enemiesKilled += 1;
		rangedEnemies.Remove (mob);
		if(enemiesKilled >= enemiesToKill){
			SpawnBoss ();
		}
		else{
			CreateRandomRanged ();
		}
	}

	public void MeleeKilled(GameObject mob){
		enemiesKilled += 1;
		meleeEnemies.Remove (mob);
		if(enemiesKilled >= enemiesToKill){
			SpawnBoss ();
		}
		else{
			CreateRandomMelee ();
		}
	}

	public void BossKilled(){
		// Won
	}

	void CreateRandomMelee(){
		GameObject newMob = Instantiate (melee) as GameObject;
		newMob.transform.position = new Vector3 (Random.Range (-24f, 24f), 0, Random.Range (-24f, 24f));
		meleeEnemies.Add (newMob);
	}

	void CreateRandomRanged(){
		GameObject newMob = Instantiate (ranged) as GameObject;
		newMob.transform.position = new Vector3 (Random.Range (-24f, 24f), 0, Random.Range (-24f, 24f));
		rangedEnemies.Add (newMob);
	}

	void SpawnBoss(){
		bossInstance = Instantiate (ranged) as GameObject;
		bossInstance.transform.position = new Vector3 (0, 0, 0);
		KillAllMobs ();

	}

	void KillAllMobs(){
		for(int i = 0; i < meleeEnemies.Count; i++){
			Destroy (meleeEnemies [i]);
		}
		for(int i = 0; i < rangedEnemies.Count; i++){
			Destroy (rangedEnemies [i]);
		}
	}
}
