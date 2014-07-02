using UnityEngine;
using System.Collections;

public class CreamFilling : MonoBehaviour {
	
	void Start () {
		transform.Rotate(new Vector3(0.0f, 0.0f, Random.Range(0, 360)));
	}
	
	void Update () {
		
	}
}