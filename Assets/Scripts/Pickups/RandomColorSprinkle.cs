using UnityEngine;
using System.Collections.Generic;

public class RandomColorSprinkle : MonoBehaviour {
	public Sprite color1, color2, color3, color4, color5, color6;

	void Start () {
		List<Sprite> colors = new List<Sprite>();
		colors.Add(color1);
		colors.Add(color2);
		colors.Add(color3);
		colors.Add(color4);
		colors.Add(color5);
		colors.Add(color6);

		var randomColor = colors[Random.Range(0, colors.Count)]; 
		GetComponent<SpriteRenderer>().sprite = randomColor;
		transform.Rotate(new Vector3(0.0f, 0.0f, Random.Range(0, 360)));
	}
}
