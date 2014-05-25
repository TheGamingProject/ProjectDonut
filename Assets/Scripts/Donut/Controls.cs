using UnityEngine;
using System.Collections;

public class Controls : MonoBehaviour
{
	public float xSpeed = 10.0f;
	private Vector2 movement;

	// Use this for initialization
	void Start ()
	{

	}

	// Update is called once per frame
	void Update ()
	{
		int inputX = 0;

		if (Input.GetMouseButton(0)) {

			if (Input.mousePosition.x < 176) {
				inputX = -1;
			} else {
				inputX = 1;
			}
		} else {
			movement = new Vector2(0,0);
		}
		
		movement = new Vector2(xSpeed * inputX, 0);
	}

	void FixedUpdate() {
		rigidbody2D.velocity = movement;
		
		// from http://answers.unity3d.com/questions/509283/limit-a-sprite-to-not-go-off-screen.html 
		
		Vector3 playerSize = (GetComponent<BoxCollider2D>()).size;
		//playerSize = renderer.bounds.size;
		
		// Here is the definition of the boundary in world point
		var distance = (transform.position - Camera.main.transform.position).z;
		
		var leftBorder = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, distance)).x + (playerSize.x/2);
		var rightBorder = Camera.main.ViewportToWorldPoint (new Vector3 (1, 0, distance)).x - (playerSize.x/2);
		
		// Here the position of the player is clamped into the boundaries
		transform.position = (new Vector3 (
			Mathf.Clamp (transform.position.x, leftBorder - .2f, rightBorder + .2f),
			transform.position.y, transform.position.z)
		                      );
	}
}

