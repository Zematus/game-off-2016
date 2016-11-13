using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManagerScript : MonoBehaviour {

	public PlayerScript Player1;
	public PlayerScript Player2;
	public PlayerScript Player3;
	public PlayerScript Player4;
	public PlayerScript Player5;
	public PlayerScript Player6;

	public CommunityScript Community;

	public Text MinimumBetText;
	public Text SmallBlindText;
	public Text BigBlindText;

	public static Game CurrentGame = new Game ();

	// Use this for initialization
	void Start () {

		CurrentGame.Initialize ();

		SetBetBlindTexts ();

		Player1.ShowDealerButton (CurrentGame.Player1.IsDealer);
		Player2.ShowDealerButton (CurrentGame.Player2.IsDealer);
		Player3.ShowDealerButton (CurrentGame.Player3.IsDealer);
		Player4.ShowDealerButton (CurrentGame.Player4.IsDealer);
		Player5.ShowDealerButton (CurrentGame.Player5.IsDealer);
		Player6.ShowDealerButton (CurrentGame.Player6.IsDealer);

		Player1.SetCash (CurrentGame.Player1.Cash);
		Player2.SetCash (CurrentGame.Player2.Cash);
		Player3.SetCash (CurrentGame.Player3.Cash);
		Player4.SetCash (CurrentGame.Player4.Cash);
		Player5.SetCash (CurrentGame.Player5.Cash);
		Player6.SetCash (CurrentGame.Player6.Cash);

		Player1.SetBet (CurrentGame.Player1.Bet);
		Player2.SetBet (CurrentGame.Player2.Bet);
		Player3.SetBet (CurrentGame.Player3.Bet);
		Player4.SetBet (CurrentGame.Player4.Bet);
		Player5.SetBet (CurrentGame.Player5.Bet);
		Player6.SetBet (CurrentGame.Player6.Bet);

		Player1.Card1.SetCard (CurrentGame.Player1.Card1);
		Player1.Card2.SetCard (CurrentGame.Player1.Card2);
		Player2.Card1.SetCard (CurrentGame.Player2.Card1);
		Player2.Card2.SetCard (CurrentGame.Player2.Card2);
		Player3.Card1.SetCard (CurrentGame.Player3.Card1);
		Player3.Card2.SetCard (CurrentGame.Player3.Card2);
		Player4.Card1.SetCard (CurrentGame.Player4.Card1);
		Player4.Card2.SetCard (CurrentGame.Player4.Card2);
		Player5.Card1.SetCard (CurrentGame.Player5.Card1);
		Player5.Card2.SetCard (CurrentGame.Player5.Card2);
		Player6.Card1.SetCard (CurrentGame.Player6.Card1);
		Player6.Card2.SetCard (CurrentGame.Player6.Card2);

		Community.Card1.SetCard (CurrentGame.Community.Card1);
		Community.Card2.SetCard (CurrentGame.Community.Card2);
		Community.Card3.SetCard (CurrentGame.Community.Card3);
		Community.Card4.SetCard (CurrentGame.Community.Card4);
		Community.Card5.SetCard (CurrentGame.Community.Card5);

		Player1.Card1.ShowCard (true);
		Player1.Card2.ShowCard (true);

		Player2.Card1.ShowCard (false);
		Player2.Card2.ShowCard (false);
		Player3.Card1.ShowCard (false);
		Player3.Card2.ShowCard (false);
		Player4.Card1.ShowCard (false);
		Player4.Card2.ShowCard (false);
		Player5.Card1.ShowCard (false);
		Player5.Card2.ShowCard (false);
		Player6.Card1.ShowCard (false);
		Player6.Card2.ShowCard (false);

		Community.Card1.ShowCard (false);
		Community.Card2.ShowCard (false);
		Community.Card3.ShowCard (false);
		Community.Card4.ShowCard (false);
		Community.Card5.ShowCard (false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetBetBlindTexts () {

		MinimumBetText.text = "Minimum Bet:\n<b>$" + CurrentGame.MinimumBet.ToString () + "</b>";
		SmallBlindText.text = "Small Blind:\n<b>$" + CurrentGame.SmallBlind.ToString () + "</b>";
		BigBlindText.text = "Big Blind:\n<b>$" + CurrentGame.BigBlind.ToString () + "</b>";
	}
}
