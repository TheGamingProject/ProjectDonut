using System;
using TouchScript.Gestures;
using TouchScript.Hit;
using UnityEngine;
using Random = UnityEngine.Random;

public class ControlsCatcher : MonoBehaviour
{
	// https://github.com/InteractiveLab/TouchScript/wiki/Gestures
	public Transform playerDonut;
	
	private void OnEnable() {
		GetComponent<PressGesture>().Pressed += pressedHandler;
		GetComponent<ReleaseGesture>().Released += releasedHandler;
		GetComponent<FlickGesture>().Flicked += flickHandler;
	}
	
	private void OnDisable() {
		GetComponent<PressGesture>().Pressed -= pressedHandler;
		GetComponent<ReleaseGesture>().Released -= releasedHandler;
		GetComponent<FlickGesture>().Flicked -= flickHandler;
	}
	
	private void pressedHandler(object sender, EventArgs e) {
		var gesture = sender as PressGesture;
		ITouchHit hit;
		gesture.GetTargetHitResult(out hit);
		var hit3d = hit as ITouchHit3D;
		if (hit3d == null) return;

		var dir = hit3d.Point.x > 0 ? 1 : -1;

		playerDonut.GetComponent<Controls>().press(dir);
	}

	private void releasedHandler(object sender, EventArgs e) {
		playerDonut.GetComponent<Controls>().release();
	}

	private void flickHandler (object sender, EventArgs e) {
		FlickGesture gesture = sender as FlickGesture;

		if (gesture.ScreenFlickVector.x > 0) {
			playerDonut.GetComponent<Controls>().flickedHorizontally(1);
		} else {
			playerDonut.GetComponent<Controls>().flickedHorizontally(-1);
		}
	}

	void OnGUI () {
		int buttonWidth = Screen.width / 2;
		int buttonHeight = (Screen.height / 6);
		bool modified = false;

		if (
			GUI.RepeatButton(
			new Rect(
			0,
			Screen.height * 5 / 6,
			buttonWidth,
			buttonHeight
			),
			"LEFT"
			)
			)
		{
			modified = true;
			playerDonut.GetComponent<Controls>().press(-1);
		} 
		if (
			GUI.RepeatButton(
			new Rect(
			buttonWidth,
			(Screen.height * 5 / 6),
			buttonWidth,
			buttonHeight
			),
			"RIGHT"
			)
			)
		{
			modified = true;
			playerDonut.GetComponent<Controls>().press(1);
		} 

		if (modified == false) {
			playerDonut.GetComponent<Controls>().release();
		}

	}
}