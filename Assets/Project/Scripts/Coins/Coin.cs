using System;
using UnityEngine;

public class Coin : MonoBehaviour
{
	public static event Action OnCoinCollected;

	private void OnTriggerEnter(Collider other)
	{
		if (other.GetComponent<Player>() != null)
		{
			OnCoinCollected?.Invoke();
			Destroy(gameObject);
		}
	}
}