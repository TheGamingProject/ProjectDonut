using UnityEngine;
using System.Collections;

public class Ant : MonoBehaviour {

	public float startY = 5.0f;
	public float moveToStartSpeed = -5.0f;

	private float hungryCooldown;
	public Vector2 hungryTimerRange = new Vector2(5, 20);

	public Transform speedupWarnLabel;
	public float speedupWarnTime = 1.0f;
	private bool warned = false;
	private Transform warnLabel;

	private bool speedupActive = false;
	public float speedupSpeed = -2.0f;
	public float speedUpScaleSize = .75f;
	public float despawnY = -20f;

	private Vector2 movement = new Vector2(0,0);


	void Start () {

	}

	void Update () {

		if (hungryCooldown > 0) {
			hungryCooldown -= Time.deltaTime;


			if (!warned && hungryCooldown <= 0) {
				showSpeedupWarn();
			} else if (warned && hungryCooldown <= 0) {
				startSpeedup();
			}
		} else { 	
			moveToStart();
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

	void Destroy () {
		Destroy(warnLabel.gameObject);
	}

	void OnTriggerEnter2D(Collider2D collider) {
		
		Sprinkle sprinkle = collider.gameObject.GetComponent<Sprinkle>();
		if (sprinkle != null && hungryCooldown > 0) {
			Debug.Log ("hit spinkl as ant");
			//AudioSource noise = GetComponents<AudioSource>()[0];
			//noise.Play();

			showSpeedupWarn();

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
		} else {
			hungryCooldown = RandomN.getRandomFloatByRange(hungryTimerRange);
		}
	}

	void showSpeedupWarn () {
		if (warned) return;

		Vector2 v = new Vector2(transform.position.x, speedupWarnLabel.position.y);
		warnLabel = TransformFactory.make2dTransform(speedupWarnLabel, v, GameObject.Find ("GUI").transform);
		hungryCooldown = speedupWarnTime;
		warned = true;
	}

	void startSpeedup () {
		Debug.Log("ant speedup");
		GetComponent<Animator>().SetBool("speedup", true);
		transform.localScale = new Vector3(speedUpScaleSize, speedUpScaleSize, speedUpScaleSize);
		speedupActive = true;
	}
}
