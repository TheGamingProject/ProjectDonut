using UnityEngine;
using System.Collections;

public class Meters : MonoBehaviour {
	private float meters = 0f;

	void Start () {
		GetComponent<GUIText>().fontSize = Mathf.RoundToInt(Screen.width * GetComponent<GUIText>().fontSize/262f);
	}

	void Update () {
		GetComponent<GUIText>().text = "meters: " + Mathf.Round(meters);
	}
	
	public float CurrentMeters {
		get {
			return meters;	
		}
	}
	
	public void addMeters(float amt) {
		meters += amt;
	}
	
}
