using UnityEngine;
using System.Collections;

public class Sugarpile : MonoBehaviour {
	public float slowDownModifier = .50f;
	public float slowDownTimeLength = 1.0f;

	void Start () {

	}

	void Update () {

	}

	public float getSlowDownModifier () {
		return slowDownModifier;
	}

	public float getSlowDownTimeLength () {
		return slowDownTimeLength;
	}
}

