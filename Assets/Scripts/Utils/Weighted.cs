using UnityEngine;
using System.Collections.Generic;

public static class Weighted {
	public static int getWeightedPick(Dictionary<int,float> d) {
		float total = 0;
		foreach (float value in d.Values) {
			total += value;
		}

		float r = Random.Range(0, total);

		float runningTotal = 0;
		foreach (int key in d.Keys) {
			runningTotal += d[key];
			if (r < runningTotal) {
				return key;
			}
		}

		return d.Count - 1;
	}
}


