using UnityEngine;

public class RandomN {
	public static float getRandomFloatByRange (Vector2 v) {
		return getRandomFloatByRange(v.x, v.y);
	}

	// f1 > f2 must be true?
	public static float getRandomFloatByRange (float f1, float f2) {
		return Random.Range(0, f2 - f1) + f1;
	}
}

