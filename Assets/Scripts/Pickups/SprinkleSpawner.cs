using UnityEngine;
using System.Collections.Generic;

public class SprinkleSpawner : MonoBehaviour {
	public Transform spawneePrefab;
	public float spawnRate = 2.0f;
	
	private float spawnCooldown;
	private int spawnedCount = 0;

	public float spawnY = -20f;
	public float maxRandomDistX = 1;

	private SpawnPattern currentSpawnPattern; 

	public float minGap = 2.0f;
	public float gapRange = 3.0f;

	void Start () {
		spawnCooldown = spawnRate;
		currentSpawnPattern = getLanePattern();
	}

	void Update () {
		if (spawnCooldown > 0) {
			spawnCooldown -= Time.deltaTime;
		}
		
		if (CanSpawn && currentSpawnPattern != null) {
			Spawn next = currentSpawnPattern.getNext();
			if (next != null) {
				Transform newSprinkle = (Transform) Instantiate(spawneePrefab, new Vector3(next.location.x, next.location.y, 0f), transform.rotation);
				newSprinkle.parent = transform;

				spawnCooldown = next.cooldown;
				spawnedCount++;
			} else {
				currentSpawnPattern = getLanePattern();
				spawnCooldown = Random.Range(0, gapRange) + minGap;
			}
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

	private SpawnPattern getLanePattern () {
		SpawnPattern pattern = new SpawnPattern();

		float x = Random.Range(0, 5) - 2.5f;

		int length = Random.Range(0, 15) + 5;

		for (int i=0; i<length; i++) {
			Spawn s = new Spawn();
			s.location = new Vector2(x, spawnY);
			s.cooldown = spawnRate;

			pattern.spawns.Add(s);
		}

		return pattern;
	}

	private void getDumbPattern () {
		//float lastX = 0;

		//float randomX = lastX + Random.Range(0, maxRandomDistX * 2) - maxRandomDistX;
	}
}

class Spawn {
	public Vector2 location;
	public float cooldown;
}

class SpawnPattern {
	public List<Spawn> spawns = new List<Spawn>();

	private int i = 0;

	public Spawn getNext() {
		if (i < spawns.Count) {
			return spawns[i++]; 
		}

		return null;
	}
}