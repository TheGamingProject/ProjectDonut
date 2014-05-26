using UnityEngine;
using System.Collections;

public class SprinkleSpawner : MonoBehaviour {
	public Transform spawneePrefab;
	public float spawnRate = 2.0f;
	
	private float spawnCooldown;
	private int spawnedCount = 0;

	public float spawnY = -20f;
	private float lastX;
	public float maxRandomDistX = 1;

	void Start () {
		spawnCooldown = spawnRate;

		lastX = 0;
	}

	void Update () {
		if (spawnCooldown > 0) {
			spawnCooldown -= Time.deltaTime;
		}
		
		if (CanSpawn) {
			spawnCooldown = spawnRate;
			spawnedCount++;

			float randomX = lastX + Random.Range(0, maxRandomDistX * 2) - maxRandomDistX;

			Transform newSprinkle = (Transform) Instantiate(spawneePrefab, new Vector3(randomX, spawnY, 0f), transform.rotation);
			newSprinkle.parent = transform;
		}
	}
	
	public bool CanSpawn {
		get{
			return spawnCooldown <= 0f;
		}
	}
	
	public void StopSpawning () {
		spawnRate = 10000000;
	}
}

