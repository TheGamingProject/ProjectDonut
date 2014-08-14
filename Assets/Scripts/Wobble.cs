using UnityEngine;
using System.Collections;

public class Wobble : MonoBehaviour {

	void Start () {
	
	}

	float roll = 0.0f;
	float last;

	float timeLeft = 0;
	float totalTime = 1.0f / 6.0f;

	float times = 20;
	float count = 0;
	
	Vector2 moveAmountRange = new Vector2(.01f, .03f);
	float added = 0.0f;

	// 3 4 3    
	void Update () {
		if (timeLeft <= 0.0f) {
			roll = RandomN.getRandomFloatByRange (0, 100);

			float left = 10.0f;
			float stay = 80.0f;

			float right = 10.0f;
		

			if (last == -1) {
					left = 35.0f;
					stay = 60.0f;
					right = 5.0f;
			} else if (last == 1) {
					left = 5.0f;
					stay = 60.0f;
					right = 35.0f;
			}

			if (roll < left) {
				added = - RandomN.getRandomFloatByRange(moveAmountRange);
			} else if (roll < left + stay) {
				added = 0.0f;
			} else if (roll < left + stay + right) {
				added = RandomN.getRandomFloatByRange(moveAmountRange);
			}

			last = added;
			timeLeft = totalTime;
		} else {
			timeLeft -= Time.deltaTime;
		}

		if (added != 0.0f) {
			Vector3 pos = transform.position;
			pos.x += added;
			transform.position = pos;
		}
	}
}
