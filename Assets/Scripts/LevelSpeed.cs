using UnityEngine;
using System.Collections;

public class LevelSpeed : MonoBehaviour {
	public float baseSpeed = 6.5f;
	private float speedModifier = 1.0f;

	public float incrementalSpeedRate = .015f; //per second
	private float incrementedSpeed = 0.0f;

	public float Speed {
		get {
			return baseSpeed * speedModifier + incrementedSpeed;
		}
	}

	void Update () {
		incrementedSpeed += Time.deltaTime * incrementalSpeedRate;
		var metersTraveled = Time.deltaTime * Speed * .15f;
		GameObject.Find("GUI").GetComponentInChildren<Meters>().addMeters(metersTraveled);
		GameObject.Find("Debug").GetComponent<GUIText>().text = "Debug: " + Speed;
	}

	public void resetSpeedModifier () {
		speedModifier = 1.0f;
	}

	public void setSpeedModifier (float f) {
		speedModifier = f;
	}
}
