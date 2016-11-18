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

		Community.SetCommunity (CurrentGame.Community);

		Community.UpdateState ();

		Player1.RevealHand (true);
		Player2.RevealHand (true);
		Player3.RevealHand (true);
		Player4.RevealHand (true);
		Player5.RevealHand (true);
		Player6.RevealHand (true);

		Community.RevealAllCards (false);

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

	public void AdvancePlay () {

		ActionPanel.SetActive (false);

		bool continuePlay = true;

		if (CurrentGame.ParticipatingPlayers <= 1) {
		
			CurrentGame.FoldingEndPlay ();
			continuePlay = false;

		} else {

			Player player = CurrentGame.SetNextPlayer ();

			if (player == null) {

				continuePlay = false;
			}
		}

		UpdatePotText ();

		Player1.UpdateState ();
		Player2.UpdateState ();
		Player3.UpdateState ();
		Player4.UpdateState ();
		Player5.UpdateState ();
		Player6.UpdateState ();

		Community.UpdateState ();

		if (continuePlay) {
			
			Community.RevealFlop ((int)CurrentGame.Phase >= (int)PlayPhase.Flop);
			Community.RevealTurn ((int)CurrentGame.Phase >= (int)PlayPhase.Turn);
			Community.RevealRiver ((int)CurrentGame.Phase >= (int)PlayPhase.River);

			ActionPanel.Setup ();
			ActionPanel.SetActive (true);

		} else {

			ResetPlay ();
		}
	}

	public void ResetPlay () {


	}
}
