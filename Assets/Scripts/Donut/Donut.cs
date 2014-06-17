using UnityEngine;
using System.Collections;

public class Donut : MonoBehaviour {

	private float slowedDownAmount = 1.0f, slowedDownAmountTotal = .5f; // none
	private float slowedDownCooldown = 0.0f, slowedDownTimeTotal = 0.0f;

	public float sizePerEat = .1f;

	void Start () {

	}

	void Update () {
		if (slowedDownCooldown > 0) {
			slowedDownCooldown -= Time.deltaTime;

			if (slowedDownCooldown <= 0) {
				transform.parent.parent.GetComponent<LevelSpeed>().resetSpeedModifier();
			} else {
				slowedDownAmount = 1.0f - slowedDownCooldown / slowedDownTimeTotal * slowedDownAmountTotal;
				//Debug.Log(slowedDownCooldown + ": " + slowedDownAmount);
				transform.parent.parent.GetComponent<LevelSpeed>().setSpeedModifier(slowedDownAmount);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D collider) {

		Sprinkle sprinkle = collider.gameObject.GetComponent<Sprinkle>();
		if (sprinkle != null) {
			GameObject.Find("GUI").BroadcastMessage("addScore", sprinkle.getPointWorth());
			AudioSource noise = GetComponents<AudioSource>()[0];
			noise.Play();

			var sprinkleSprite = sprinkle.gameObject.GetComponent<SpriteRenderer>().sprite;
			gameObject.BroadcastMessage("addSprinklesToDonut", sprinkleSprite);

			Destroy(sprinkle.gameObject);
		}
/*
		Sugarpile sugarpile = collider.gameObject.GetComponent<Sugarpile>();
		if (sugarpile != null) {
			//AudioSource noise = GetComponents<AudioSource>()[0];
			//noise.Play();
			
			//var sugarpileSprite = sugarpile.gameObject.GetComponent<SpriteRenderer>().sprite;
			//gameObject.BroadcastMessage("addSugarPileToDonut", sugarpileSprite);

			Debug.Log("Hit sugarpile");
			slowedDownAmount = slowedDownAmountTotal = sugarpile.getSlowDownModifier();
			slowedDownCooldown = slowedDownTimeTotal = sugarpile.getSlowDownTimeLength();
			Destroy(sugarpile.gameObject);
		}

		Ant ant = collider.gameObject.GetComponent<Ant>();
		if (ant != null) {
			GameObject.Find("GUI").GetComponentInChildren<InGameStates>().loseGame();
			GetComponent<Controls>().setGameOver();
			Destroy(ant.gameObject);
		}
		*/
		DonutEnemy de = collider.gameObject.GetComponent<DonutEnemy>();
		if (de != null) {
			if (de.getSize() > transform.localScale.x) { // its bigger, player loses
				GameObject.Find("GUI").GetComponentInChildren<InGameStates>().loseGame();
				GetComponent<Controls>().setGameOver();
			} else { // its smaller, player gets bigger
				float newSize = transform.localScale.x + sizePerEat;
				transform.localScale = new Vector3(newSize, newSize, 1.0f);
				GameObject.Find("GUI").BroadcastMessage("addScore", 10);
				Destroy(de.gameObject);
			}

		}
	}
}
