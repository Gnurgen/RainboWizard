using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

	public float health;
	public float regenerationPerSec;
	public float regenerationDelay;
	public float currentHealth;
	private float currentRegenerationDelay;


	// Use this for initialization
	void Start () {
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
			Destroy (gameObject);
		}
	}
}
