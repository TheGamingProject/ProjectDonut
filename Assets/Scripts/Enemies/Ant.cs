using UnityEngine;
using System.Collections;

public class Ant : MonoBehaviour {

	public float startY = 5.0f;
	public float moveToStartSpeed = -5.0f;

	private float hungryCooldown;
	public Vector2 hungryTimerRange = new Vector2(5, 20);

	private bool speedupActive = false;
	public float speedupSpeed = -2.0f;
	public float speedUpScaleSize = .75f;
	public float despawnY = -20f;

	private Vector2 movement = new Vector2(0,0);


	void Start () {
		hungryCooldown = RandomN.getRandomFloatByRange(hungryTimerRange);
	}

	void Update () {
		moveToStart();

		if (hungryCooldown > 0) {
			hungryCooldown -= Time.deltaTime;

			if (hungryCooldown <= 0) {
				startSpeedup();
			}
		}

		if (speedupActive) {
			movement = new Vector3(0, speedupSpeed * Time.deltaTime, 0);
			transform.Translate(movement);
		}

		if (transform.position.y < despawnY) {
			Debug.Log("despawn ant");
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D collider) {
		
		Sprinkle sprinkle = collider.gameObject.GetComponent<Sprinkle>();
		if (sprinkle != null) {
			Debug.Log ("hit spinkl as ant");
			//AudioSource noise = GetComponents<AudioSource>()[0];
			//noise.Play();

			startSpeedup();

			Destroy(sprinkle.gameObject);
		}

		Sugarpile sugarpile = collider.gameObject.GetComponent<Sugarpile>();
		if (sugarpile != null) {
			//AudioSource noise = GetComponents<AudioSource>()[0];
			//noise.Play();

			//kill itself
			Destroy(gameObject);
		}
	}

	void moveToStart () {
		if (transform.position.y > startY) {
			Vector3 movement = new Vector3(0, moveToStartSpeed, 0);
			
			movement *= Time.deltaTime;
			transform.Translate(movement);
		} 
	}

	void startSpeedup () {
		Debug.Log("ant speedup");
		GetComponent<Animator>().SetBool("speedup", true);
		transform.localScale = new Vector3(speedUpScaleSize, speedUpScaleSize, speedUpScaleSize);
		speedupActive = true;
	}
}
