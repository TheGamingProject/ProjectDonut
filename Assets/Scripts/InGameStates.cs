using UnityEngine;
using System.Collections;

public class InGameStates : MonoBehaviour {
	enum States {
		game,
		showscore
	}
	States gameState = States.game;
		

	void Start () {

	}

	void Update () {
	
	}

	public void OnGUI () {
		if ( gameState == States.showscore &&
			GUI.Button(
			// Center in X, 1/3 of the height in Y
			new Rect(
			0,
			(1 * Screen.height / 3) - 100,
			Screen.width,
			200
			),
			"score/meters...play again!"
			) 
			) 
		{
			// Reload the level
			Application.LoadLevel("proto1");
		}
	}

	public void loseGame () {
		// stop movement?
		GameObject.Find("Level").GetComponentInChildren<LevelSpeed>().setGameOver();

		// get highscore/meters

		// change state
		gameState = States.showscore;
	}
}
