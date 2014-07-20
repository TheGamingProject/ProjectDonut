using UnityEngine;
using System.Collections;

public class DonutEnemy : MonoBehaviour {
	
	public float startY = -5.0f;

	public Vector2 startSizeRange = new Vector2(.1f, 2.0f);
	public float speedConstant = -1.0f;
	private float speed;

	public float despawnY = -200f;
	
	private Vector2 movement = new Vector2(0,0);
	
	
	void Start () {
		float size = RandomN.getRandomFloatByRange(startSizeRange);
		transform.localScale = new Vector3(size, size, 1.0f);
		speed = speedConstant * size;
	}
	
	void Update () {

		var speedDifference = transform.parent.parent.GetComponent<LevelSpeed>().NonPlayerSpeedDifference;
		movement = new Vector3(0, (speed + speedDifference) * Time.deltaTime, 0);

		transform.Translate(movement);
		
		if (transform.position.y > despawnY) {
			Debug.Log("despawn ant");
			Destroy(gameObject);
		}
	}

	public float getSize () {
		return transform.localScale.x;
	}
}
