using UnityEngine;
using System.Collections.Generic;

public class DonutEnemySpawner : MonoBehaviour {
	public Transform spawneePrefab;
	
	public float intialSpawnTime = 2.0f;
	private float spawnCooldown;
	
	public float spawnXRange = 3.15f; // from the center
	public float spawnY = 6f;
	
	public Vector2 spawnRate = new Vector2(.5f, 1.5f);
	public float spawnSpeedupConstant = 0.000005f;
	
	void Start () {
		spawnCooldown = intialSpawnTime;
	}
	
	void Update () {
		if (spawnCooldown > 0) {
			spawnCooldown -= Time.deltaTime;
		}
		
		if (CanSpawn) {
			spawn();
			spawnCooldown = RandomN.getRandomFloatByRange(spawnRate) - (Time.timeSinceLevelLoad*spawnSpeedupConstant);
			Debug.Log (spawnCooldown);
			Debug.Log (Time.timeSinceLevelLoad*spawnSpeedupConstant);
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
