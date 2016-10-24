using UnityEngine;
using System.Collections;

public class DeathTracker : MonoBehaviour {

	public enum type {melee, ranged, boss};

	public type enemyType;
	private GameManager gameManager;
	private bool started = false;

	// Use this for initialization
	void Start () {
		GameObject gm = GameObject.FindGameObjectWithTag ("GameManager");
		gameManager = gm.GetComponent<GameManager> ();
		started = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnDestroy(){
		if (started) {
			switch (enemyType) {
			case type.melee:
				gameManager.MeleeKilled (gameObject);
				break;
			case type.ranged:
				gameManager.RangedKilled (gameObject);
				break;
			case type.boss:
				gameManager.BossKilled ();
				break;
			default:
				Debug.Log ("Someone forgot to set a tag");
				break;
			}
		}
	}
}
