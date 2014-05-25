using UnityEngine;
using System.Collections.Generic;

public class RollingAnimationScript : MonoBehaviour {
	public Sprite donut1, donut2, donut3, donut4,
		donut5, donut6, donut7, donut8;

	private Sprite startingSprite;
	private List<Sprite> sprites;
	private SpriteRenderer spriteRenderer;

	public float animateRollRate = 1.0f;
	private float animateRollCooldown;
	private int rollNumber = 0;

	// Use this for initialization
	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer>();
		startingSprite = spriteRenderer.sprite;

		sprites = new List<Sprite>();
		sprites.Add(startingSprite);
		sprites.Add(donut1);
		sprites.Add(donut2);
		sprites.Add(donut3);
		sprites.Add(donut4);
		sprites.Add(donut5);
		sprites.Add(donut6);
		sprites.Add(donut7);
		sprites.Add(donut8);

		animateRollCooldown = animateRollRate;
	}
	
	// Update is called once per frame
	void Update () {
		if (animateRollCooldown > 0) {
			animateRollCooldown -= Time.deltaTime;

			if (animateRollCooldown <= 0f) {
				animateRollCooldown = animateRollRate;

				rollNumber = (rollNumber + 1) % sprites.Count;
				spriteRenderer.sprite = sprites[rollNumber];
			}
		}
	}
}
