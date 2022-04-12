using System;
using UnityEngine;

public class SpawnLocation : MonoBehaviour
{
	private Coin coin;
	public bool IsEmpty => coin == null;

	public void SpawnCoin(Coin prefab)
	{
		if (IsEmpty)
		{
			coin = Instantiate(prefab, transform);
		}
	}

	private void DisableVisual()
	{
		MeshRenderer mesh = GetComponent<MeshRenderer>();
		if (mesh != null)
		{
			mesh.enabled = false;
		}
	}

	// Unity
	private void Start()
	{
		DisableVisual();
	}
}