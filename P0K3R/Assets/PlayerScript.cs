using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	public GameObject HighlightPanel;
	public GameObject FoldedPanel;
	
	public CardScript Card1;
	public CardScript Card2;

	public Text Cash;
	public Text Bet;

	public Image DealerButton;

	private Player _player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetPlayer (Player player) {
	
		_player = player;
	}

	public void UpdateState () {
	
		SetCash (_player.Cash);
		SetBet (_player.Bet);
		ShowDealerButton (_player.IsDealer);

		Card1.SetCard (_player.Card1);
		Card2.SetCard (_player.Card2);

		SetFolded (_player.HasFolded);

		Highlight (GameManagerScript.CurrentGame.CurrentPlayer == _player);
	}

	public void Highlight (bool state) {

		HighlightPanel.SetActive (state);
	}

	public void RevealHand (bool state) {

		Card1.Reveal (true);
		Card2.Reveal (true);
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

	public void SetFolded (bool state) {
	
		FoldedPanel.SetActive (state);
	}
}
