﻿using UnityEngine;
using System.Collections;

public class Meters : MonoBehaviour {
	private float meters = 0f;

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