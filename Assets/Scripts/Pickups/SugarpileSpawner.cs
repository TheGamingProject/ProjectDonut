using UnityEngine;
using System.Collections.Generic;

public class SugarpileSpawner : MonoBehaviour {
	public Transform spawneePrefab;
	
	private float spawnCooldown;
	
	public float spawnXRange = 5.0f; // from the center
	public float spawnY = -20f;
	
	public Vector2 spawnNewPilesRateRange = new Vector2(2f, 5f);
	
	void Start () {
		spawnCooldown = RandomN.getRandomFloatByRange(spawnNewPilesRateRange);
	}
	
	void Update () {
		if (spawnCooldown > 0) {
			spawnCooldown -= Time.deltaTime;
		}
		
		if (CanSpawn) {
			float x = RandomN.getRandomFloatByRange(-spawnXRange, spawnXRange);

			Transform newSprinkle = (Transform) Instantiate(spawneePrefab, new Vector3(x, spawnY, 0f), transform.rotation);
			newSprinkle.parent = transform;

			spawnCooldown = RandomN.getRandomFloatByRange(spawnNewPilesRateRange);
		}
	}
	
	public bool CanSpawn {
		get{
			return spawnCooldown <= 0f;
		}
	}
	
	public void StopSpawning () {
		spawnCooldown = 10000000;
	}

}
