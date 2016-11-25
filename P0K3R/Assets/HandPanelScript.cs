using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class HandPanelScript : MonoBehaviour {

	public CardScript Card1;
	public CardScript Card2;
	public CardScript Card3;
	public CardScript Card4;
	public CardScript Card5;

	public Text PlayerText;
	public Text EarningsText;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Initialize (Player player) {
	
		PlayerText.text = "Player " + player.Index;

		bool hasBestHand = player.BestHand != null;

		Card1.SetActive (hasBestHand);
		Card2.SetActive (hasBestHand);
		Card3.SetActive (hasBestHand);
		Card4.SetActive (hasBestHand);
		Card5.SetActive (hasBestHand);

		if (hasBestHand) {

			Card1.SetCard (player.BestHand [0]);
			Card2.SetCard (player.BestHand [1]);
			Card3.SetCard (player.BestHand [2]);
			Card4.SetCard (player.BestHand [3]);
			Card5.SetCard (player.BestHand [4]);
		}

		Card1.Reveal (hasBestHand);
		Card2.Reveal (hasBestHand);
		Card3.Reveal (hasBestHand);
		Card4.Reveal (hasBestHand);
		Card5.Reveal (hasBestHand);

		EarningsText.text = "$" + player.Earnings.ToString ();
	}

	public void SetActive (bool state) {

		gameObject.SetActive (state);
	}
}
