using UnityEngine;
using System.Collections;

public class SprinkledDonutManager : MonoBehaviour {
	public Transform donutSprinklePrefab;

	public float sprinkleSpeed = 1;

	private float donutHeight = .8f, donutWidth = .70f;
	private int bottomZ = 1, topZ = -1;

	void Start () {

	}

	void Update () {
		foreach (Transform donutSprinkle in transform) {
			if (donutSprinkle.localPosition.z == bottomZ) {
				donutSprinkle.Translate(new Vector3(0, sprinkleSpeed * Time.deltaTime, 0), donutSprinkle.parent);
				if (donutSprinkle.localPosition.y > donutHeight / 2) {
					donutSprinkle.localPosition = new Vector3(donutSprinkle.localPosition.x, donutSprinkle.localPosition.y, topZ);
				}
			} else 
			if (donutSprinkle.localPosition.z == topZ) {
				donutSprinkle.Translate(new Vector3(0, -sprinkleSpeed * Time.deltaTime, 0), donutSprinkle.parent);
				if (donutSprinkle.localPosition.y < -donutHeight / 2) {
					donutSprinkle.localPosition = new Vector3(donutSprinkle.localPosition.x, donutSprinkle.localPosition.y, bottomZ);
				}
			}
		}
	}

	void addSprinklesToDonut (Sprite sprinkleSprite) {

		Transform newSprinkle = (Transform) Instantiate(donutSprinklePrefab, new Vector3(0f, 0f, 0f), transform.rotation);
		newSprinkle.parent = transform;

		var x = Random.Range(0, donutWidth) - donutWidth / 2;
		var y = -donutHeight / 2;
		newSprinkle.localPosition = new Vector3(x, y, bottomZ);

		newSprinkle.Rotate(new Vector3(0.0f, 0.0f, Random.Range(0, 360)));
		newSprinkle.GetComponent<SpriteRenderer>().sprite = sprinkleSprite;
	}
}

