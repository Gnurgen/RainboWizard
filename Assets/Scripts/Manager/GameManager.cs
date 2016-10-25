using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

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

    private bool bossSpawned = false;

    private AudioSource bossdieAudio;

    public int nextLevel = 1;


    // Use this for initialization
    void Awake()
    {
        bossdieAudio = this.GetComponent<AudioSource>();
        GameObject.FindGameObjectWithTag("Player").GetComponent<Renderer>().material.SetFloat("_Colors", (nextLevel));
    }

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
		if(enemiesKilled == enemiesToKill)
        {
            SpawnBoss ();
		}
		else if (bossSpawned != true){
			CreateRandomRanged ();
		}
        else
        {
            //do nothing!
        }
	}

	public void MeleeKilled(GameObject mob){
		enemiesKilled += 1;
		meleeEnemies.Remove (mob);
        if (enemiesKilled == enemiesToKill)
        {
            SpawnBoss();
        }
        else if (bossSpawned != true)
        {
            CreateRandomRanged();
        }
        else
        {
            //do nothing!
        }
    }

	public void BossKilled(){
        // Won
        GameObject.FindGameObjectWithTag("Player").GetComponent<Renderer>().material.SetFloat("_Colors", (nextLevel + 1));
        bossdieAudio.Play();
        StartCoroutine("GoNextLevel");
	}

	void CreateRandomMelee(){
		GameObject newMob = Instantiate (melee) as GameObject;
		newMob.transform.position = new Vector3 (Random.Range (-20f, 20f), 0, Random.Range (-20f, 20f));
		meleeEnemies.Add (newMob);
	}

	void CreateRandomRanged(){
		GameObject newMob = Instantiate (ranged) as GameObject;
		newMob.transform.position = new Vector3(Random.Range(-20f, 20f), 0, Random.Range(-20f, 20f));
		rangedEnemies.Add (newMob);
	}

	void SpawnBoss(){
        bossSpawned = true;
		bossInstance = Instantiate (boss) as GameObject;
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

    IEnumerator GoNextLevel()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(nextLevel);
    }
}
