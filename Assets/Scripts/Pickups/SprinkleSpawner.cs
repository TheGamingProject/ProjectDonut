using UnityEngine;
using System.Collections.Generic;

public class SprinkleSpawner : MonoBehaviour {
	public Transform spawneePrefab;
	public float spawnRate = 2.0f;
	
	private float spawnCooldown;
	private int spawnedCount = 0;

	public float spawnXRange = 5.0f; // from the center
	public float spawnY = -20f;
	public float maxRandomDistX = 1;

	private SpawnPattern currentSpawnPattern; 

	public Vector2 timeInBetweenPatternsRange = new Vector2(.5f, 1.5f);

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
				currentSpawnPattern = getRandomPattern();
				spawnCooldown = Random.Range(0, timeInBetweenPatternsRange.y - timeInBetweenPatternsRange.x) + timeInBetweenPatternsRange.x;
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

	private SpawnPattern getRandomPattern () {
		Dictionary<int, float> weights = new Dictionary<int, float>();
		weights.Add(0, 40f);
		weights.Add(1, 40f);
		weights.Add(2, 20f);
		var pick = Weighted.getWeightedPick(weights);

		switch (pick) {
		case 0:
			return getDiagonalPattern();
		case 1:
			return getLanePattern();
		case 2:
			return getDumbPattern();
		default:
			return null;
		}
	}

	private SpawnPattern getLanePattern () {
		SpawnPattern pattern = new SpawnPattern();

		float x = Random.Range(0, spawnXRange) - spawnXRange/2;

		int length = Random.Range(0, 15) + 5;

		for (int i=0; i<length; i++) {
			Spawn s = new Spawn();
			s.location = new Vector2(x, spawnY);
			s.cooldown = spawnRate;

			pattern.spawns.Add(s);
		}

		return pattern;
	}

	private SpawnPattern getDumbPattern () {
		SpawnPattern pattern = new SpawnPattern();

		float x =  Random.Range(0, spawnXRange) - spawnXRange/2;
		int length = Random.Range(0, 15) + 5;

		for (int i=0; i<length; i++) {
			Spawn s = new Spawn();
			
			x = x + Random.Range(0, maxRandomDistX * 2) - maxRandomDistX;
			s.location = new Vector2(x, spawnY);
			s.cooldown = spawnRate;
			
			pattern.spawns.Add(s);
		}
		
		return pattern;
	}

	private SpawnPattern getDiagonalPattern () {
		SpawnPattern pattern = new SpawnPattern();
		
		float x = Random.Range(0, spawnXRange) - spawnXRange/2;
		float xGap = Random.Range(0, .3f) + .2f;
		xGap *= x < 0 ? 1 : -1;

		int length = Random.Range(0, 7) + 5;
		
		for (int i=0; i<length; i++) {
			Spawn s = new Spawn();
			x = x + xGap;
			s.location = new Vector2(x, spawnY);
			s.cooldown = spawnRate;
			
			pattern.spawns.Add(s);
		}
		
		return pattern;
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