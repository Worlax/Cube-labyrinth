using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class RotateTrigger : MonoBehaviour
{
	public static event Action<Side> OnRotateTriggerReached;

	private void OnTriggerEnter(Collider other)
	{
		if (other.GetComponent<Player>() != null)
		{
			if (!IsCurrentSideTrigger())
			{
				OnRotateTriggerReached?.Invoke(GetMyCurrentSide());
			}
		}
	}

	private bool IsCurrentSideTrigger()
	{
		float x = transform.position.x;
		float y = transform.position.y;
		float z = transform.position.z;

		return Mathf.Abs(y) > Mathf.Abs(x)
			&& Mathf.Abs(y) > Mathf.Abs(z);
	}

	private Side GetMyCurrentSide()
	{
		float x = transform.position.x;
		float y = transform.position.y;
		float z = transform.position.z;

		if (Mathf.Abs(x) > Mathf.Abs(z))
		{
			return x > 0 ? Side.Right : Side.Left;
		}
		else
		{
			return z > 0 ? Side.Up : Side.Down;
		}
	}
}