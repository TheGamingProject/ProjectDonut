using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour {
	private int score;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<GUIText>().text = "score: " + score;
	}
	
	public int CurrentScore {
		get {
			return score;	
		}
	}
	
	public void addScore(int amt) {
		score += amt;
	}
	
}
