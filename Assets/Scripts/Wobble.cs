using UnityEngine;
using System.Collections;

public class Wobble : MonoBehaviour {

	void Start () {
	
	}

	float roll = 0.0f;
	float last;

	public float totalTimeLength = 1.0f / 6.0f;

	float timeLeft = 0;
	
	public Vector2 moveAmountRange = new Vector2(.01f, .03f);
	float added = 0.0f;

	public float oppositeDirectionPercent = 5.0f;
	public float stopDirectionPercent = 60.0f; // to neutral
	public float currentDirectionPercent = 35.0f;

	public float neutralDirectionPercent = 10.0f;
	public float neutralStayPercent = 80.0f;

	// 3 4 3    
	void Update () {
		if (timeLeft <= 0.0f) {
			roll = RandomN.getRandomFloatByRange (0, 100);

			float left = neutralDirectionPercent;
			float stay = neutralStayPercent;
			float right = neutralDirectionPercent;
		
			if (last == -1) {
				left = currentDirectionPercent;
				stay = stopDirectionPercent;
				right = oppositeDirectionPercent;
			} else if (last == 1) {
				left = oppositeDirectionPercent;
				stay = stopDirectionPercent;
				right = currentDirectionPercent;
			}

			if (roll < left) {
				added = - RandomN.getRandomFloatByRange(moveAmountRange);
			} else if (roll < left + stay) {
				added = 0.0f;
			} else if (roll < left + stay + right) {
				added = RandomN.getRandomFloatByRange(moveAmountRange);
			}

			last = added;
			timeLeft = totalTimeLength;
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
