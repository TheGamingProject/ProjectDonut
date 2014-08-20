using UnityEngine;
using System.Collections;

public class Donut : MonoBehaviour {
	private LevelSpeed levelSpeed; 

	private float slowedDownAmount = 1.0f, slowedDownAmountTotal = .5f; // none
	private float slowedDownCooldown = 0.0f, slowedDownTimeTotal = 0.0f;

	private Transform nutShieldTransform;
	public Sprite nutShield1;
	public Sprite nutShield2;
	private int nutShieldState = 0;

	private Transform creamSpeedTransform;
	public Sprite creamSpeed;
	public float creamSpeedupTimeLength = 2.0f;
	public float creamSpeedupValue = 1.5f;
	public float creamPickupExtensionTimeLength = .5f;
	public int creamPickupsNeeded = 3;
	private int currentCreamPickups = 0;
	private bool creamState;
	private float creamCooldown = 0f;

	//public float sizePerEat = .1f;

	void Start () {
		levelSpeed = transform.parent.parent.GetComponent<LevelSpeed>();
		initPowerupsTransforms();

		//put this somewhere else?
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
	}

	void Update () {
		if (slowedDownCooldown > 0) {
			slowedDownCooldown -= Time.deltaTime;

			if (slowedDownCooldown <= 0) {
				levelSpeed.resetSpeedModifier();
			} else {
				slowedDownAmount = 1.0f - slowedDownCooldown / slowedDownTimeTotal * slowedDownAmountTotal;
				//Debug.Log(slowedDownCooldown + ": " + slowedDownAmount);
				levelSpeed.setSpeedModifier(slowedDownAmount);
			}
		}

		updateCreamFillingCooldown();
		if (creamState) {
			updateCreamFillingBoost();
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
		*/
		Nut nut = collider.gameObject.GetComponent<Nut> ();
		if (nut != null) {
			pickupNut(nut);
		}

		CreamFilling creamFilling = collider.gameObject.GetComponent<CreamFilling>();
		if (creamFilling) { 
			pickupCreamFilling(creamFilling);
		}

		DonutEnemy de = collider.gameObject.GetComponent<DonutEnemy>();
		if (de != null) {
			getHitByDonutEnemy(de);
			//if (de.getSize() > transform.localScale.x) { // its bigger, player loses
				//GameObject.Find("GUI").GetComponentInChildren<InGameStates>().loseGame();
				//GetComponent<Controls>().setGameOver();
			/*} else { // its smaller, player gets bigger
				float newSize = transform.localScale.x + sizePerEat;
				transform.localScale = new Vector3(newSize, newSize, 1.0f);
				GameObject.Find("GUI").BroadcastMessage("addScore", 10);
				Destroy(de.gameObject);
			}*/

		}
	}

	void initPowerupsTransforms() {
		nutShieldTransform = transform.FindChild("nutshield");
		creamSpeedTransform = transform.FindChild("creamboost");
	}

	void pickupNut (Nut nut) {
		if (nutShieldState == 2) return;

		nutShieldState++;
		setNutShieldSprite ();

		Destroy (nut.gameObject);
	}

	void setNutShieldSprite () {
		if (nutShieldState == 0) {
			nutShieldTransform.GetComponent<SpriteRenderer>().sprite = null;
		} else if (nutShieldState == 1) {
			nutShieldTransform.GetComponent<SpriteRenderer>().sprite = nutShield1;
		} else if (nutShieldState == 2) {
			nutShieldTransform.GetComponent<SpriteRenderer>().sprite = nutShield2;
		} 
	}

	void pickupCreamFilling (CreamFilling creamFilling) {
		if (creamState) {
			extendCreamFillingBoost();
		} else {
			currentCreamPickups++;
			Debug.Log("picked up " + currentCreamPickups + " pickups");
			if (currentCreamPickups >= creamPickupsNeeded) {
				startCreamFillingBoost();
			}
		}

		Destroy(creamFilling.gameObject);
	}

	void startCreamFillingBoost() {
		creamState = true;
		creamCooldown = creamSpeedupTimeLength;
		setCreamFillingSprite();
	}

	float speedUptoCruisingSpeedDuration = 1.00f;
	float startingSpeed = 1.0f;

	void updateCreamFillingBoost() {
		var cruisingSpeed = creamSpeedupValue;
		var currentTime = creamSpeedupTimeLength - creamCooldown;

		float speed = creamFillingEasing(creamSpeedupTimeLength - creamCooldown, 0, (cruisingSpeed - startingSpeed), speedUptoCruisingSpeedDuration) 
				/ currentTime;
		Debug.Log(currentTime + "/" + creamSpeedupTimeLength + " " + (speed) + "/" + (cruisingSpeed - startingSpeed) + " " + cruisingSpeed);
		levelSpeed.setSpeedModifier(startingSpeed + speed);//creamSpeedupValue);
	}

	//float creamFillingEasing(float time, float startValue, float changeInValue, float duration) {
	float creamFillingEasing(float t, float b, float c, float d) {
		var ts=(t/=d)*t;
		var tc=ts*t;
		return b+c*(0.0500000000000025f*tc*ts + -1.05f*ts*ts + 3.5f*tc + -5f*ts + 3.5f*t);
	}

	void extendCreamFillingBoost() {
		creamCooldown += creamPickupExtensionTimeLength;
	}

	void endCreamFillingBoost() {
		creamState = false;
		currentCreamPickups = 0;
		setCreamFillingSprite();
		levelSpeed.resetSpeedModifier();
	}

	void updateCreamFillingCooldown() {
		
		if (creamState && creamCooldown > 0) {
			creamCooldown -= Time.deltaTime;
			
			if (creamCooldown <= 0) {
				endCreamFillingBoost();
			}
		}
	}

	void setCreamFillingSprite () {
		if (creamState) {
			creamSpeedTransform.GetComponent<SpriteRenderer>().sprite = creamSpeed;
		} else {
			creamSpeedTransform.GetComponent<SpriteRenderer>().sprite = null;
		}
	}

	void getHitByDonutEnemy (DonutEnemy donutEnemy) {
		if (creamState) {
			// invunerable
		} else if (nutShieldState == 0) {
			GameObject.Find ("GUI").GetComponentInChildren<InGameStates> ().loseGame ();
			GetComponent<Controls> ().setGameOver ();
		} else {
			nutShieldState--;
			setNutShieldSprite();
			Destroy(donutEnemy.gameObject);
		}
	}
}
