using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

	public float health;
	public float regenerationPerSec;
	public float regenerationDelay;
	public float currentHealth;
	private float currentRegenerationDelay;

    private GameObject gameManager;
    private AudioSource gameManagerAudioSource;

    // Use this for initialization
    void Start () {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        gameManagerAudioSource = gameManager.GetComponent<AudioSource>();
        currentHealth = health;
		currentRegenerationDelay = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if(currentRegenerationDelay < 0){
			if(currentHealth < health){
				currentHealth = currentHealth + (regenerationPerSec * Time.deltaTime);
				if(currentHealth > health){
					currentHealth = health;
				}
			}
		}
		currentRegenerationDelay = currentRegenerationDelay - Time.deltaTime;
	}

	public void takeDamage(float damage){
		currentHealth = currentHealth - damage;
		currentRegenerationDelay = regenerationDelay;
		if(currentHealth <= 0){
			if (this.gameObject.tag == "Player")
            {
                gameManagerAudioSource.clip = gameManager.GetComponent<GameManager>().dieClip;
                gameManagerAudioSource.Play();
                StartCoroutine("QuitGame");
            }
            Destroy(gameObject);
        }
	}

    IEnumerator QuitGame()
    {
        yield return new WaitForSeconds(5);
        Application.Quit();
    }
}
