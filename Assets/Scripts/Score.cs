using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour {
	private int score;

	void Start () {
		GetComponent<GUIText>().fontSize = Mathf.RoundToInt(Screen.width * 20f/262f);
	}

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
