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
	public Text PotText;

	public ActionPanelScript ActionPanel;

	public static Game CurrentGame = new Game ();

	// Use this for initialization
	void Start () {

		CurrentGame.Initialize ();

		ActionPanel.SetActive (false);

		UpdateBetBlindTexts ();
		UpdatePotText ();

		Player1.SetPlayer (CurrentGame.Player1);
		Player2.SetPlayer (CurrentGame.Player2);
		Player3.SetPlayer (CurrentGame.Player3);
		Player4.SetPlayer (CurrentGame.Player4);
		Player5.SetPlayer (CurrentGame.Player5);
		Player6.SetPlayer (CurrentGame.Player6);

		Player1.UpdateState ();
		Player2.UpdateState ();
		Player3.UpdateState ();
		Player4.UpdateState ();
		Player5.UpdateState ();
		Player6.UpdateState ();

		Community.Card1.SetCard (CurrentGame.Community.Card1);
		Community.Card2.SetCard (CurrentGame.Community.Card2);
		Community.Card3.SetCard (CurrentGame.Community.Card3);
		Community.Card4.SetCard (CurrentGame.Community.Card4);
		Community.Card5.SetCard (CurrentGame.Community.Card5);

		Player1.ShowCards (true);
		Player2.ShowCards (true);
		Player3.ShowCards (true);
		Player4.ShowCards (true);
		Player5.ShowCards (true);
		Player6.ShowCards (true);

		Community.ShowAllCards (false);

		ActionPanel.Setup ();
		ActionPanel.SetActive (true);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void UpdateBetBlindTexts () {

		MinimumBetText.text = "Minimum Bet:\n<b>$" + CurrentGame.MinimumBet.ToString () + "</b>";
		SmallBlindText.text = "Small Blind:\n<b>$" + CurrentGame.SmallBlind.ToString () + "</b>";
		BigBlindText.text = "Big Blind:\n<b>$" + CurrentGame.BigBlind.ToString () + "</b>";
	}

	public void UpdatePotText () {
	
		PotText.text = "Current Pot: <b>$" + CurrentGame.Pot.ToString () + "</b>";
	}

	public void PassToNextPlayer () {

		ActionPanel.SetActive (false);

		Player player = CurrentGame.SetNextPlayer ();

		if (player == null)
			return;

		Community.ShowFlop ((int)CurrentGame.Phase >= (int)PlayPhase.Flop);
		Community.ShowTurn ((int)CurrentGame.Phase >= (int)PlayPhase.Turn);
		Community.ShowRiver ((int)CurrentGame.Phase >= (int)PlayPhase.River);

		UpdatePotText ();

		Player1.UpdateState ();
		Player2.UpdateState ();
		Player3.UpdateState ();
		Player4.UpdateState ();
		Player5.UpdateState ();
		Player6.UpdateState ();

		ActionPanel.Setup ();
		ActionPanel.SetActive (true);
	}
}
