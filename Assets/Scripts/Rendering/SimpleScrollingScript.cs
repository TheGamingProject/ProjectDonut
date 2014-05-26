using UnityEngine;
using System.Collections.Generic;

public class SimpleScrollingScript : MonoBehaviour {
	public Vector2 speed;
	public float removalY = 10;
	
	public void Update () {
		Vector3 movement = new Vector3(speed.x, speed.y, 0);
			
		movement *= Time.deltaTime;
		transform.Translate(movement);

		foreach(Transform t in transform) {
			if (transform.position.y + t.localPosition.y >= removalY) {
				Destroy(t.gameObject);
			}
		}
	}
}