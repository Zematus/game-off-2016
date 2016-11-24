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

	public void Initialize (Player player, List<Card> hand, int earnings) {
	
		PlayerText.text = "Player " + player.Index;

		Card1.SetCard (hand [0]);
		Card2.SetCard (hand [1]);
		Card3.SetCard (hand [2]);
		Card4.SetCard (hand [3]);
		Card5.SetCard (hand [4]);

		Card1.Reveal (true);
		Card2.Reveal (true);
		Card3.Reveal (true);
		Card4.Reveal (true);
		Card5.Reveal (true);

		EarningsText.text = "$" + earnings.ToString ();
	}

	public void Initialize (Player player, int earnings) {

		PlayerText.text = "Player " + player.Index;

		Card1.SetActive (false);
		Card2.SetActive (false);
		Card3.SetActive (false);
		Card4.SetActive (false);
		Card5.SetActive (false);

		EarningsText.text = "$" + earnings.ToString ();
	}

	public void SetActive (bool state) {

		gameObject.SetActive (state);
	}
}
