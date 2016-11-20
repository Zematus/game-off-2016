using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum Card {
	
	Empty = -1,

	Spades2 = 0,
	Spades3 = 1,
	Spades4 = 2,
	Spades5 = 3,
	Spades6 = 4,
	Spades7 = 5,
	Spades8 = 6,
	Spades9 = 7,
	Spades10 = 8,
	SpadesJ = 9,
	SpadesQ = 10,
	SpadesK = 11,
	SpadesA = 12,

	Hearts2 = 13,
	Hearts3 = 14,
	Hearts4 = 15,
	Hearts5 = 16,
	Hearts6 = 17,
	Hearts7 = 18,
	Hearts8 = 19,
	Hearts9 = 20,
	Hearts10 = 21,
	HeartsJ = 22,
	HeartsQ = 23,
	HeartsK = 24,
	HeartsA = 25,

	Clubs2 = 26,
	Clubs3 = 27,
	Clubs4 = 28,
	Clubs5 = 29,
	Clubs6 = 30,
	Clubs7 = 31,
	Clubs8 = 32,
	Clubs9 = 33,
	Clubs10 = 34,
	ClubsJ = 35,
	ClubsQ = 36,
	ClubsK = 37,
	ClubsA = 38,

	Diamonds2 = 39,
	Diamonds3 = 40,
	Diamonds4 = 41,
	Diamonds5 = 42,
	Diamonds6 = 43,
	Diamonds7 = 44,
	Diamonds8 = 45,
	Diamonds9 = 46,
	Diamonds10 = 47,
	DiamondsJ = 48,
	DiamondsQ = 49,
	DiamondsK = 50,
	DiamondsA = 51,

	Back = 52
}

public enum Suit {

	Spades = 0,
	Hearts = 1,
	Clubs = 2,
	Diamonds =3
}

public enum PlayPhase {

	Start = 0,
	Flop = 1,
	Turn = 2,
	River = 3
}

public class Player {

	public Game CurrentGame;

	public int Index;

	public Card Card1;
	public Card Card2;

	public int Cash = 0;

	public bool IsActive = false;
	public bool IsDealer = false;
	public bool HasFolded = false;
	public bool HasRaised = false;

	public int Bet = 0;

	public void Reset () {
	
		Card1 = Card.Empty;
		Card2 = Card.Empty;
	}

	public void AddCash (int cash) {

		Cash += cash;
	}

	public void SetBet (int bet) {

		Cash -= bet - Bet;
		Bet = bet;
	}

	public void Fold () {

		HasFolded = true;

		CurrentGame.ParticipatingPlayers--;

		PushBetToPot ();
	}

	public void PushBetToPot () {

		CurrentGame.AddToPot (Bet);

		HasRaised = false;
		Bet = 0;
	}

	public void Raise (int newValue) {

		HasRaised = true;
		SetBet (newValue);
		CurrentGame.SetCall (Bet);
		CurrentGame.StopIndex = Index;
	}

	public void Call (int newValue) {

		HasRaised = false;
		SetBet (newValue);
	}

	public void Check () {

		HasRaised = false;
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

	public PlayPhase Phase = PlayPhase.Start;

	public int MinimumBet = 50;
	public int SmallBlind = 25;
	public int BigBlind = 50;

	public int Pot = 0;

	public int CurrentCall = 50;

	public int DealerIndex = -1;
	public int StopIndex = -1;

	public int ParticipatingPlayers = 6;

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

			player.Index = i;
			player.CurrentGame = this;
			player.Cash = 1000;
			player.IsActive = true;
		}

		SelectRandomDealer ();

		SetNextPlay ();
	}

	public void SetNextPlay () {

		StopIndex = -1;

		ResetDeck ();

		// Deal first card to each active player
		for (int i = 0; i < 6; i++) {

			int index = (i + DealerIndex) % 6;

			Player player = Players[index];

			if (!player.IsActive)
				continue;

			player.HasFolded = false;

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
		StopIndex = CurrentPlayer.Index;
	}

	public Player SetNextPlayer () {
	
		int index = (CurrentPlayer.Index + 1) % 6;

		Player player = Players [index];

		bool stop = index == StopIndex;

		while (!player.IsActive || player.HasFolded) {
		
			index = (index + 1) % 6;
			player = Players [index];

			stop |= index == StopIndex;
		}

		if (stop) {

			CurrentCall = 0;

			if (Phase == PlayPhase.River) {

				CurrentPlayer = player;
				return null;
			}

			Phase = (PlayPhase)((int)Phase + 1);

			PushAllBetsToPot ();

			index = (DealerIndex + 1) % 6;
			player = Players [index];

			while (!player.IsActive || player.HasFolded) {

				index = (index + 1) % 6;
				player = Players [index];
			}

			StopIndex = player.Index;
		}

		CurrentPlayer = player;

		return CurrentPlayer;
	}

	public void PushAllBetsToPot () {

		foreach (Player p in Players) {

			if (p.IsActive && !p.HasFolded) {
				p.PushBetToPot ();
			}
		}
	}

	public void FoldingEndPlay () {

		PushAllBetsToPot ();

		int index = (CurrentPlayer.Index + 1) % 6;

		Player player = Players [index];

		while (!player.IsActive || player.HasFolded) {

			index = (index + 1) % 6;
			player = Players [index];
		}

		player.AddCash (Pot);
		Pot = 0;

		EndPlay ();
	}

	public void ShowdownEndPlay () {

		PushAllBetsToPot ();

		List<Card> bestHand = null;
		List<Player> bestPlayers = new List<Player> (6);

		foreach (Player player in Players) {

			if (bestHand == null) {
			
				bestHand = GetPlayerHand (player);
				bestPlayers.Add (player);
			} else {
			
				List<Card> hand = GetPlayerHand (player);
				int result = CompareHands (bestHand, hand);

				if (result == 0) {
				
					bestPlayers.Add (player);

				} else if (result == 1) {
				
					bestPlayers.Clear ();

					bestHand = hand;
					bestPlayers.Add (player);
				}
			}
		}

		int winInTokens = Pot / (bestPlayers.Count * SmallBlind);

		foreach (Player player in bestPlayers) {

			int cash = winInTokens * SmallBlind;
			Pot -= cash;

			player.AddCash (cash);
		}

		bestPlayers [0].AddCash (Pot);

		Pot = 0;

		EndPlay ();
	}

	public List<Card> GetPlayerHand (Player player) {
	
		List<Card> hand = new List<Card> (7);

		hand.Add (player.Card1);
		hand.Add (player.Card2);

		hand.Add (Community.Card1);
		hand.Add (Community.Card2);
		hand.Add (Community.Card3);
		hand.Add (Community.Card4);
		hand.Add (Community.Card5);

		hand.Sort (CompareCardsByValue);

		return hand;
	}

	public void EndPlay () {

		AdvanceDealerButton ();

		SetNextPlay ();
	}

	public void AdvanceDealerButton () {

		List<Player> activePlayers = new List<Player> ();

		foreach (Player player in Players) {

			if (player.IsActive) {

				activePlayers.Add (player);
			}
		}

		int index = (DealerIndex + 1) % 6;

		for (int i = 0; i < activePlayers.Count; i++) {

			activePlayers [i].IsDealer = index == i;

			if (index == i) {

				CurrentPlayer = activePlayers [i];
				DealerIndex = CurrentPlayer.Index;
			}
		}
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

	public void SetCall (int value) {
	
		CurrentCall = value;
	}

	public void AddToPot (int value) {

		Pot += value;
	}

	public int CompareHands (List<Card> hand1, List<Card> hand2) {

		List<Card> straightFlush1 = GetStraightFlush (hand1);
		List<Card> straightFlush2 = GetStraightFlush (hand2);
	
		if (straightFlush1 != null) {

			if (straightFlush2 != null) {
			
				return CompareCardsByValue (straightFlush1 [0], straightFlush2 [0]);
			}

			return -1;
		}

		if (straightFlush2 != null) {
		
			return 1;
		}

		List<List<Card>> allSets1 = GetAllSetsOfAKind (hand1);
		List<List<Card>> allSets2 = GetAllSetsOfAKind (hand2);

		if (HasFourOfAKind (allSets1) || HasFourOfAKind (allSets2)) {
		
			return CompareAllSetsOfAKind (allSets1, allSets2);
		}

		if (HasFullHouse (allSets1)) {

			if (HasFullHouse (allSets2)) {
			
				return CompareAllSetsOfAKind (allSets1, allSets2);
			}

			return -1;
		}

		if (HasFullHouse (allSets2)) {
		
			return 1;
		}

		List<Card> straight1 = GetStraight (hand1);
		List<Card> straight2 = GetStraight (hand2);

		if (straight1 != null) {

			if (straight2 != null) {

				return CompareCardsByValue (straight1 [0], straight2 [0]);
			}

			return -1;
		}

		if (straight2 != null) {

			return 1;
		}

		List<Card> flush1 = GetFlush (hand1);
		List<Card> flush2 = GetFlush (hand2);

		if (flush1 != null) {

			if (flush2 != null) {

				return CompareCardsByValue (flush1 [0], flush2 [0]);
			}

			return -1;
		}

		if (flush2 != null) {

			return 1;
		}

		return CompareAllSetsOfAKind (allSets1, allSets2);
	}

	public bool HasFourOfAKind (List<List<Card>> allSets) {
	
		return (allSets [0].Count == 4);
	}

	public bool HasFullHouse (List<List<Card>> allSets) {

		return (allSets [0].Count == 3) && (allSets [1].Count == 2);
	}

	public bool HasThreeOfAKind (List<List<Card>> allSets) {

		return (allSets [0].Count == 3) && (allSets [1].Count == 1);
	}

	public bool HasTwoPair (List<List<Card>> allSets) {

		return (allSets [0].Count == 2) && (allSets [1].Count == 2);
	}

	public bool HasOnePair (List<List<Card>> allSets) {

		return (allSets [0].Count == 2) && (allSets [1].Count == 1);
	}

	public int CompareAllSetsOfAKind (List<List<Card>> allSets1, List<List<Card>> allSets2) {

		int setIndex = 0;

		while ((setIndex < allSets1.Count) && (setIndex < allSets2.Count)) {
		
			List<Card> set1 = allSets1 [setIndex];
			List<Card> set2 = allSets2 [setIndex];

			int compRes = CompareSetsOfAKind (set1, set2);

			if (compRes != 0)
				return compRes;

			setIndex++;
		}

		return 0;
	}

	public int CompareSetsOfAKind (List<Card> set1, List<Card> set2) {
	
		if (set1.Count > set2.Count)
			return -1;

		if (set1.Count < set2.Count)
			return 1;

		return CompareCardsByValue (set1 [0], set2 [0]);
	}

	public List<List<Card>> GetAllSetsOfAKind (List<Card> hand) {
	
		List<List<Card>> setsOfAKind = new List<List<Card>> (7);
		List<Card> remainingCards = new List<Card> (hand);

		while (remainingCards.Count > 0) {
		
			List<Card> setOfAKind = GetSetOfAKind (remainingCards);

			setsOfAKind.Add (setOfAKind);
		}

		setsOfAKind.Sort (CompareSetsOfAKind);

		List<List<Card>> allSetsOfAKind = new List<List<Card>> (5);

		int count = 0;

		foreach (List<Card> setOfAKind in setsOfAKind) {

			if (count >= 5)
				break;
		
			count += setOfAKind.Count;

			int overflow = Mathf.Max (0, count - 5);

			if (overflow > 0) {
			
				setOfAKind.RemoveRange (setOfAKind.Count - overflow, overflow);
			}

			allSetsOfAKind.Add (setOfAKind);
		}

		return allSetsOfAKind;
	}

	public List<Card> GetSetOfAKind (List<Card> remainingCards) {

		List<Card> setOfAKind = new List<Card>(4);

		bool first = true;
		int count = 0;

		Card[] cards = remainingCards.ToArray ();

		foreach (Card card in cards) {

			if (first) {
				
				setOfAKind.Add (card);
				count++;

				remainingCards.Remove (card);

				first = false;
				continue;
			}
		
			if (GetCardValue (setOfAKind [0]) == GetCardValue (card)) {

				setOfAKind.Add (card);
				count++;

				remainingCards.Remove (card);
				continue;
			}

			return setOfAKind;
		}

		return setOfAKind;
	}

	public int GetCardValue (Card card) {
	
		return 2 + ((int)card % 13);
	}

	public List<Card> GetStraightFlush (List<Card> hand) {

		List<Card> tempHand = GetFlush (hand);

		if (tempHand == null)
			return null;

		return GetStraight (tempHand);
	}

	public Suit GetCardSuit (Card card) {

		return (Suit)((int)card / 13);
	}

	public List<Card> GetFlush (List<Card> hand) {

		List<Card> hand2 = new List<Card> (hand);

		hand2.Sort (CompareCardsBySuit);

		List<Card> flush = new List<Card> (5);

		Suit lastSuit = GetCardSuit (hand2 [0]);
		int count = 1;

		flush.Add (hand2 [0]);

		foreach (Card card in hand2) {

			Suit suit = GetCardSuit (card);

			if (lastSuit == suit) {

				flush.Add (card);
				count++;

				if (count == 5) {
				
					return flush;
				}

				continue;
			}

			flush.Clear ();

			lastSuit = suit;
			count = 1;

			flush.Add (card);
		}

		return null;
	}

	public List<Card> GetStraight (List<Card> hand) {

		List<Card> straight = new List<Card> (5);

		int lastCardValue = GetCardValue (hand [0]);
		int count = 1;

		bool firstIsAce = lastCardValue == 14;

		straight.Add (hand [0]);

		foreach (Card card in hand) {

			int cardValue = GetCardValue (card);

			if (lastCardValue == cardValue)
				continue;

			if (lastCardValue == (cardValue + 1)) {
			
				lastCardValue--;
				count++;

				straight.Add (card);

				if (count == 5)
					return straight;

				if (cardValue == 2) {
					
					if (firstIsAce) {

						count++;

						straight.Add (hand [0]);
					}

					if (count == 5)
						return straight;

					return null;
				}

				continue;
			}

			straight.Clear ();
			straight.Add (card);
			count = 1;

			lastCardValue = cardValue;
		}

		return null;
	}

	public int CompareCardsByValue (Card a, Card b) {

		int aValue = GetCardValue (a);
		int bValue = GetCardValue (b);

		if (aValue > bValue)
			return -1;

		if (aValue < bValue)
			return 1;

		return 0;
	}

	public int CompareCardsBySuit (Card a, Card b) {

		Suit aSuit = GetCardSuit (a);
		Suit bSuit = GetCardSuit (b);

		if ((int)aSuit > (int)bSuit)
			return -1;

		if ((int)aSuit < (int)bSuit)
			return 1;

		return 0;
	}

	public int CompareCards (Card a, Card b) {

		int aValue = GetCardValue (a);
		int bValue = GetCardValue (b);

		if (aValue > bValue)
			return -1;

		if (aValue < bValue)
			return 1;

		if ((int)a > (int)b)
			return -1;

		if ((int)a < (int)b)
			return 1;

		return 0;
	}
}
