using UnityEngine;
using System.Collections;

public class Donut : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{

	}

	// Update is called once per frame
	void Update ()
	{

	}

	void OnTriggerEnter2D(Collider2D collider) {

		Sprinkle sprinkle = collider.gameObject.GetComponent<Sprinkle>();
		if (sprinkle != null) {
			GameObject.Find("GUI").BroadcastMessage("addScore", sprinkle.getPointWorth());
			AudioSource noise = GetComponents<AudioSource>()[0];
			noise.Play();

			var sprinkleSprite = sprinkle.gameObject.GetComponent<SpriteRenderer>().sprite;
			gameObject.BroadcastMessage("addSprinklesToDonut", sprinkleSprite);

			Destroy(sprinkle.gameObject);
		}
		
	}
}

