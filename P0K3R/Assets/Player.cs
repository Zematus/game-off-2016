using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
	public int Earnings = 0;

	public List<Card> BestHand = null;

	public void Reset () {
	
		Card1 = Card.Empty;
		Card2 = Card.Empty;

		Earnings = 0;

		BestHand = null;

		HasFolded = false;
	}

	public void AddCash (int cash) {

		Cash += cash;
		Earnings += cash;
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

	public List<Card> GetHandFromAllSetsOfAKind (List<List<Card>> allSets) {

		List<Card> hand = new List<Card> (5);

		foreach (List<Card> set in allSets) {

			hand.AddRange (set);
		}

		return hand;
	}

	public List<Card> BuildBestHand () {

		List<Card> hand = new List<Card> (7);

		hand.Add (Card1);
		hand.Add (Card2);

		hand.Add (CurrentGame.Community.Card1);
		hand.Add (CurrentGame.Community.Card2);
		hand.Add (CurrentGame.Community.Card3);
		hand.Add (CurrentGame.Community.Card4);
		hand.Add (CurrentGame.Community.Card5);

		hand.Sort (Game.CompareCardsByValue);

		List<Card> straightFlush = Game.GetStraightFlush (hand);

		if (straightFlush != null) {

			BestHand = straightFlush;

			return straightFlush;
		}

		List<List<Card>> allSets = Game.GetAllSetsOfAKind (hand);

		List<Card> allSetsHand = GetHandFromAllSetsOfAKind (allSets);

		if (Game.HasFourOfAKind (allSets) || Game.HasFullHouse (allSets)) {

			BestHand = allSetsHand;

			return allSetsHand;
		}

		List<Card> straight = Game.GetStraight (hand);

		if (straight != null) {

			BestHand = straight;

			return straight;
		}

		List<Card> flush = Game.GetFlush (hand);

		if (flush != null) {

			BestHand = flush;

			return flush;
		}

		BestHand = allSetsHand;

		return allSetsHand;
	}
}
