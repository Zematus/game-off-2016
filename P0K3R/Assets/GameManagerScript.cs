using UnityEngine;
using System.Collections;

public class GameManagerScript : MonoBehaviour {

	public PlayerScript Player1;
	public PlayerScript Player2;
	public PlayerScript Player3;
	public PlayerScript Player4;
	public PlayerScript Player5;
	public PlayerScript Player6;

	public CommunityScript Community;

	public static Game CurrentGame = new Game ();

	// Use this for initialization
	void Start () {

		CurrentGame.Initialize ();

		Player1.ShowDealerButton (true);
		Player2.ShowDealerButton (false);
		Player3.ShowDealerButton (false);
		Player4.ShowDealerButton (false);
		Player5.ShowDealerButton (false);
		Player6.ShowDealerButton (false);

		Player1.SetCash (CurrentGame.Player1.Cash);
		Player2.SetCash (CurrentGame.Player2.Cash);
		Player3.SetCash (CurrentGame.Player3.Cash);
		Player4.SetCash (CurrentGame.Player4.Cash);
		Player5.SetCash (CurrentGame.Player5.Cash);
		Player6.SetCash (CurrentGame.Player6.Cash);

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

		Player1.Card1.ShowCard ();
		Player1.Card2.ShowCard ();

		Player2.Card1.HideCard ();
		Player2.Card2.HideCard ();
		Player3.Card1.HideCard ();
		Player3.Card2.HideCard ();
		Player4.Card1.HideCard ();
		Player4.Card2.HideCard ();
		Player5.Card1.HideCard ();
		Player5.Card2.HideCard ();
		Player6.Card1.HideCard ();
		Player6.Card2.HideCard ();

		Community.Card1.HideCard ();
		Community.Card2.HideCard ();
		Community.Card3.HideCard ();
		Community.Card4.HideCard ();
		Community.Card5.HideCard ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
