using System;
using TouchScript.Gestures;
using TouchScript.Hit;
using UnityEngine;
using Random = UnityEngine.Random;

public class ControlsCatcher : MonoBehaviour
{
	// https://github.com/InteractiveLab/TouchScript/wiki/Gestures
	public Transform playerDonut;
	
	private void OnEnable()
	{
		GetComponent<PressGesture>().Pressed += pressedHandler;
		GetComponent<ReleaseGesture>().Released += releasedHandler;
	}
	
	private void OnDisable()
	{
		GetComponent<PressGesture>().Pressed -= pressedHandler;
		GetComponent<ReleaseGesture>().Released -= releasedHandler;
	}
	
	private void pressedHandler(object sender, EventArgs e)
	{
		var gesture = sender as PressGesture;
		ITouchHit hit;
		gesture.GetTargetHitResult(out hit);
		var hit3d = hit as ITouchHit3D;
		if (hit3d == null) return;

		var dir = hit3d.Point.x > 0 ? 1 : -1;

		playerDonut.GetComponent<Controls>().press(dir);
	}

	private void releasedHandler(object sender, EventArgs e)
	{
		playerDonut.GetComponent<Controls>().release();
	}
}