using UnityEngine;
using System.Collections;
using TouchScript;

public class Controls : MonoBehaviour
{
	public float xSpeed = 10.0f;
	private Vector2 movement;

	private float iPx = 0.0f;
	public float tiltThreshold = .1f;
	public float fullSpeedTiltThreshold = .25f;
	private bool halfSpeed = false;

	// Use this for initialization
	void Start ()
	{

	}

	// Update is called once per frame
	void Update ()
	{
		iPx = Input.acceleration.x;
		if (Mathf.Abs(iPx) > tiltThreshold) {
			var inputX = Mathf.Sign(iPx);
			if (Mathf.Abs(iPx) < fullSpeedTiltThreshold) halfSpeed = true;
			else halfSpeed = false;
			press (inputX);
		} else if (Mathf.Abs(iPx) > 0 && Mathf.Abs(iPx) < tiltThreshold) {
			release();
			halfSpeed = false;
		}
		GameObject.Find("Debug").GetComponent<GUIText>().text = "Debug: " + iPx + " - " + halfSpeed;
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

	public void press(float dir) {
		float speed = xSpeed * (halfSpeed ? .5f : 1);
		movement = new Vector2(speed * dir, 0);
	}

	public void release() {
		if (Mathf.Abs(iPx) < tiltThreshold)
			movement = new Vector2(xSpeed * 0, 0);
	}
}

