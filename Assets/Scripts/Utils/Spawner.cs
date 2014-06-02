using UnityEngine;
using System.Collections.Generic;

public class Spawner : MonoBehaviour {
	public Transform spawneePrefab;

	public float intialSpawnTime = 10.0f;
	private float spawnCooldown;
	
	public float spawnXRange = 3.25f; // from the center
	public float spawnY = -20f;
	
	public Vector2 spawnRate = new Vector2(1f, 1f);
	
	void Start () {
		spawnCooldown = intialSpawnTime;
	}
	
	void Update () {
		if (spawnCooldown > 0) {
			spawnCooldown -= Time.deltaTime;
		}
		
		if (CanSpawn) {
			spawn();
			spawnCooldown = RandomN.getRandomFloatByRange(spawnRate);
		}
	}

	private void spawn () {
		float x = RandomN.getRandomFloatByRange(-spawnXRange, spawnXRange);
		TransformFactory.make2dTransform(spawneePrefab, new Vector2(x, spawnY), transform);
	}

	public bool CanSpawn {
		get{
			return spawnCooldown <= 0f;
		}
	}
	
	public void StopSpawning () {
		spawnCooldown = float.MaxValue;
	}
	
}
