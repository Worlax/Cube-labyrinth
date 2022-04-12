using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
#pragma warning disable 0649

	[SerializeField] Coin coinPrefab;
	[SerializeField] int activeCoinsAtOnce;

#pragma warning restore 0649

	List<SpawnLocation> spawnLocations = new List<SpawnLocation>();

	private void SpawnCoins(int amount)
	{
		List<SpawnLocation> emptySpawns = spawnLocations.Where(x => x.IsEmpty).ToList();
		amount = amount > emptySpawns.Count() ? emptySpawns.Count() : amount;

		for (int i = 0; i < amount; ++i)
		{
			int random = Random.Range(0, emptySpawns.Count);
			emptySpawns[random].SpawnCoin(coinPrefab);
			emptySpawns.RemoveAt(random);
		}
	}

	// Events
	private void CoinCollected()
	{
		SpawnCoins(1);
	}

	// Unity
	private void Start()
	{
		foreach (SpawnLocation spawn in FindObjectsOfType<SpawnLocation>())
		{
			spawnLocations.Add(spawn);
		}

		activeCoinsAtOnce = activeCoinsAtOnce < spawnLocations.Count ? activeCoinsAtOnce : spawnLocations.Count;
		SpawnCoins(activeCoinsAtOnce);

		Coin.OnCoinCollected += CoinCollected;
	}
}