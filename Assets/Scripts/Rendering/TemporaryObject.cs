using UnityEngine;
using System.Collections;

public class TemporaryObject : MonoBehaviour {
	public float deathTimer; 
	private float deathCooldown;

	void Start () {
		deathCooldown = deathTimer;
	}

	void Update () {
		deathCooldown -= Time.deltaTime;

		if (deathCooldown <= 0) {

			Destroy(gameObject);
		}
	}
}

