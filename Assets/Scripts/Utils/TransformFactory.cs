using UnityEngine;
using System.Collections;

public class TransformFactory : MonoBehaviour {

	public static Transform make2dTransform (Transform spawneePrefab, Vector2 spawnLocation, Transform parent) {
		Transform t =  (Transform) Instantiate(spawneePrefab, new Vector3(spawnLocation.x, spawnLocation.y, 0f), parent.rotation);
		t.parent = parent;
		t.localPosition = new Vector3(t.localPosition.x, t.localPosition.y, 0);
		return t;
	}
}
