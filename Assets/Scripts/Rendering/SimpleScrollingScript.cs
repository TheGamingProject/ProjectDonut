using UnityEngine;
using System.Collections.Generic;

public class SimpleScrollingScript : MonoBehaviour {
	public float removalY = 10;
	
	public void Update () {
		var speed = transform.parent.GetComponent<LevelSpeed>().Speed;
		Vector3 movement = new Vector3(0, speed, 0);
			
		movement *= Time.deltaTime;
		transform.Translate(movement);

		foreach(Transform t in transform) {
			if (transform.position.y + t.localPosition.y >= removalY) {
				Destroy(t.gameObject);
			}
		}
	}
}