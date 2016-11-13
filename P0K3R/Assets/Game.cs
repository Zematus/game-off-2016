using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum Card {

	SpadesA = 0,
	Spades2 = 1,
	Spades3 = 2,
	Spades4 = 3,
	Spades5 = 4,
	Spades6 = 5,
	Spades7 = 6,
	Spades8 = 7,
	Spades9 = 8,
	Spades10 = 9,
	SpadesJ = 10,
	SpadesQ = 11,
	SpadesK = 12,

	HeartsA = 13,
	Hearts2 = 14,
	Hearts3 = 15,
	Hearts4 = 16,
	Hearts5 = 17,
	Hearts6 = 18,
	Hearts7 = 19,
	Hearts8 = 20,
	Hearts9 = 21,
	Hearts10 = 22,
	HeartsJ = 23,
	HeartsQ = 24,
	HeartsK = 25,

	ClubsA = 26,
	Clubs2 = 27,
	Clubs3 = 28,
	Clubs4 = 29,
	Clubs5 = 30,
	Clubs6 = 31,
	Clubs7 = 32,
	Clubs8 = 33,
	Clubs9 = 34,
	Clubs10 = 35,
	ClubsJ = 36,
	ClubsQ = 37,
	ClubsK = 38,

	DiamondsA = 39,
	Diamonds2 = 40,
	Diamonds3 = 41,
	Diamonds4 = 42,
	Diamonds5 = 43,
	Diamonds6 = 44,
	Diamonds7 = 45,
	Diamonds8 = 46,
	Diamonds9 = 47,
	Diamonds10 = 48,
	DiamondsJ = 49,
	DiamondsQ = 50,
	DiamondsK = 51,

	Back = 52,
	Empty = 53
}

public class Player {

	public int Index;

	public Card Card1;
	public Card Card2;

	public int Cash = 0;

	public bool IsActive = false;
	public bool IsDealer = false;

	public int Bet = 0;

	public void Reset () {
	
		Card1 = Card.Empty;
		Card2 = Card.Empty;
	}

	public void SetBet (int bet) {

		Cash -= bet - Bet;
		Bet = bet;
	}
}

public class Community {

	public Card Card1;
	public Card Card2;
	public Card Card3;
	public Card Card4;
	public Card Card5;

	public void Reset () {

		Card1 = Card.Empty;
		Card2 = Card.Empty;
		Card3 = Card.Empty;
		Card4 = Card.Empty;
		Card5 = Card.Empty;
	}
}

public class Game {

	public const int DeckSize = 52;

	public int DeckCardCount = DeckSize;

	public List<Card> Deck = new List<Card> (DeckSize);

	public Player Player1 = new Player ();
	public Player Player2 = new Player ();
	public Player Player3 = new Player ();
	public Player Player4 = new Player ();
	public Player Player5 = new Player ();
	public Player Player6 = new Player ();

	public Player[] Players = new Player[6];

	public Community Community = new Community ();

	public int MinimumBet = 50;
	public int SmallBlind = 25;
	public int BigBlind = 50;

	public int DealerIndex = 0;

	public Player CurrentPlayer = null;

	public Game () {
	
		Players [0] = Player1;
		Players [1] = Player2;
		Players [2] = Player3;
		Players [3] = Player4;
		Players [4] = Player5;
		Players [5] = Player6;
	}

	public void Initialize () {

		for (int i = 0; i < 6; i++) {

			Player player = Players[i];

			player.Cash = 500;
			player.IsActive = true;
			player.Index = i;
		}

		SelectRandomDealer ();

		SetNextPlay ();
	}

	public void SetNextPlay () {

		ResetDeck ();

		// Deal first card to each active player
		for (int i = 0; i < 6; i++) {

			int index = (i + DealerIndex) % 6;

			Player player = Players[index];

			if (!player.IsActive)
				continue;

			player.Card1 = GetRandomCardFromDeck ();
		}

		// Deal second card to each active player
		for (int i = 0; i < 6; i++) {

			int index = (i + DealerIndex) % 6;

			Player player = Players[index];

			if (!player.IsActive)
				continue;

			player.Card2 = GetRandomCardFromDeck ();
		}

		// Burn a card
		GetRandomCardFromDeck ();

		// Deal flop
		Community.Card1 = GetRandomCardFromDeck ();
		Community.Card2 = GetRandomCardFromDeck ();
		Community.Card3 = GetRandomCardFromDeck ();

		// Burn a card
		GetRandomCardFromDeck ();

		// Deal turn
		Community.Card4 = GetRandomCardFromDeck ();

		// Burn a card
		GetRandomCardFromDeck ();

		// Deal river
		Community.Card5 = GetRandomCardFromDeck ();

		// Set Small Blind
		SetNextPlayer ().SetBet (SmallBlind);

		// Set Big Blind
		SetNextPlayer ().SetBet (BigBlind);

		// Set Current Player
		SetNextPlayer ();
	}

	public Player SetNextPlayer () {
	
		int index = (CurrentPlayer.Index + 1) % 6;

		while (!Players [index].IsActive) {
		
			index++;
		}

		CurrentPlayer = Players [index];

		return CurrentPlayer;
	}

	public void ResetDeck () {

		Deck.Clear ();

		DeckCardCount = DeckSize;

		for (int i = 0; i < DeckSize; i++) {
		
			Deck.Add ((Card)i);
		}
	}

	public Card GetRandomCardFromDeck () {

		if (DeckCardCount <= 0) {
		
			return Card.Empty;
		}

		int index = Random.Range (0, DeckCardCount);

		Card card = Deck [index];

		Deck.Remove (card);
		DeckCardCount--;

		return card;
	}

	public void SelectRandomDealer () {

		List<Player> activePlayers = new List<Player> ();

		foreach (Player player in Players) {
		
			if (player.IsActive) {
			
				activePlayers.Add (player);
			}
		}

		int index = Random.Range (0, activePlayers.Count);

		for (int i = 0; i < activePlayers.Count; i++) {

			activePlayers [i].IsDealer = index == i;

			if (index == i) {

				CurrentPlayer = activePlayers [i];
				DealerIndex = CurrentPlayer.Index;
			}
		}
	}
}
