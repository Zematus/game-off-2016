using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerScript : MonoBehaviour {
	
	public CardScript Card1;
	public CardScript Card2;

	public Text Cash;
	public Text Bet;

	public Image DealerButton;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetCash (int amount) {
	
		Cash.text = "$" + amount.ToString ();
	}

	public void SetBet (int amount) {

		Bet.text = "$" + amount.ToString ();
	}

	public void ShowDealerButton (bool state) {

		DealerButton.gameObject.SetActive (state);
	}

	public void SetActive (bool state) {

		Card1.SetActive (state);
		Card2.SetActive (state);

		DealerButton.gameObject.SetActive (state);

		if (!state) {
			Cash.text = "----";
			Bet.text = "----";
		}
	}
}
