using System.Collections;
using UnityEngine;

public enum Side
{
	Left,
	Right,
	Up,
	Down
}

public class Control : MonoBehaviour
{
#pragma warning disable 0649

	[SerializeField] Transform doll;

	[SerializeField] float tiltLimit = 30f;
	[SerializeField] float tiltSpeed = 6.5f;
	[SerializeField] float rotateSeconds = 0.8f;

#pragma warning restore 0649

	bool leftPressed;
	bool rightPressed;
	bool upPressed;
	bool downPressed;

	bool rotating;

	private void TiltListener()
	{
		transform.rotation = Quaternion.RotateTowards(transform.rotation, doll.rotation, tiltSpeed * Time.deltaTime * 10);

		// Left and right 
		if (Input.GetKeyDown(KeyCode.A))
		{
			if (leftPressed) return;
			leftPressed = true;
			TiltDoll(Side.Left);
		}
		if (Input.GetKeyUp(KeyCode.A))
		{
			if (!leftPressed) return;
			leftPressed = false;
			TiltDoll(Side.Right);
		}
		if (Input.GetKeyDown(KeyCode.D))
		{
			if (rightPressed) return;
			rightPressed = true;
			TiltDoll(Side.Right);
		}
		if (Input.GetKeyUp(KeyCode.D))
		{
			if (!rightPressed) return;
			rightPressed = false;
			TiltDoll(Side.Left);
		}

		// Up and down
		if (Input.GetKeyDown(KeyCode.W))
		{
			if (upPressed) return;
			upPressed = true;
			TiltDoll(Side.Up);
		}
		if (Input.GetKeyUp(KeyCode.W))
		{
			if (!upPressed) return;
			upPressed = false;
			TiltDoll(Side.Down);
		}
		if (Input.GetKeyDown(KeyCode.S))
		{
			if (downPressed) return;
			downPressed = true;
			TiltDoll(Side.Down);
		}
		if (Input.GetKeyUp(KeyCode.S))
		{
			if (!downPressed) return;
			downPressed = false;
			TiltDoll(Side.Up);
		}
	}

	private void ReleaseAllButtons()
	{
		if (leftPressed)
		{
			TiltDoll(Side.Right);
			leftPressed = false;
		}
		if (rightPressed)
		{
			TiltDoll(Side.Left);
			rightPressed = false;
		}
		if (upPressed)
		{
			TiltDoll(Side.Down);
			upPressed = false;
		}
		if (downPressed)
		{
			TiltDoll(Side.Up);
			downPressed = false;
		}
	}

	IEnumerator Rotate()
	{
		if (rotating) yield break;
		rotating = true;

		float counter = 0;
		Quaternion currentRot = transform.rotation;

		while (counter < rotateSeconds)
		{
			counter += Time.deltaTime;
			transform.rotation = Quaternion.Lerp(currentRot, doll.rotation, counter / rotateSeconds);
			yield return null;
		}

		rotating = false;
	}

	private void TiltDoll(params Side[] sides)
	{
		foreach (Side side in sides)
		{
			if (side == Side.Left || side == Side.Right)
			{
				if (upPressed) ChangeDollAngle(tiltLimit, Side.Down);
				if (downPressed) ChangeDollAngle(tiltLimit, Side.Up);

				ChangeDollAngle(tiltLimit, side);

				if (upPressed) ChangeDollAngle(tiltLimit, Side.Up);
				if (downPressed) ChangeDollAngle(tiltLimit, Side.Down);
			}
			else
			{
				ChangeDollAngle(tiltLimit, side);
			}
		}
	}

	private void RotateDoll(Side side)
	{
		ChangeDollAngle(-90, side);
	}

	private void ChangeDollAngle(float angle, params Side[] sides)
	{
		foreach (Side side in sides)
		{
			switch (side)
			{
				case Side.Left:
					doll.RotateAround(Vector3.zero, Camera.main.transform.up, angle);
					break;

				case Side.Right:
					doll.RotateAround(Vector3.zero, Camera.main.transform.up, -angle);
					break;

				case Side.Up:
					doll.RotateAround(Vector3.zero, Camera.main.transform.right, angle);
					break;

				case Side.Down:
					doll.RotateAround(Vector3.zero, Camera.main.transform.right, -angle);
					break;
			}
		}
	}

	// Events
	private void RotateTriggerReached(Side side)
	{
		if (!rotating)
		{
			ReleaseAllButtons();
			RotateDoll(side);
			StartCoroutine(Rotate());
		}
	}

	// Unity
	private void Update()
	{
		if (!rotating)
		{
			TiltListener();
		}
	}

	private void Start()
	{
		RotateTrigger.OnRotateTriggerReached += RotateTriggerReached;
	}
}