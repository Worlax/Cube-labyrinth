using UnityEngine;
using UnityEngine.UI;

public class CoinsCounter : MonoBehaviour
{
#pragma warning disable 0649

	[SerializeField] Text text;

#pragma warning restore 0649

	public int Count { get; private set; }

	// Events
	private void CoinCollected()
	{
		++Count;
		text.text = Count.ToString();
	}

	// Unity
	private void Start()
	{
		Coin.OnCoinCollected += CoinCollected;
		Count = 0;
		text.text = "0";
	}
}