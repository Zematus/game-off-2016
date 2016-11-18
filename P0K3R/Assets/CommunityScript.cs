using UnityEngine;
using System.Collections;

public class CommunityScript : MonoBehaviour {

	public CardScript Card1;
	public CardScript Card2;
	public CardScript Card3;
	public CardScript Card4;
	public CardScript Card5;

	private Community _community;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetCommunity (Community community) {

		_community = community;
	}

	public void UpdateState () {

		Card1.SetCard (_community.Card1);
		Card2.SetCard (_community.Card2);
		Card3.SetCard (_community.Card3);
		Card4.SetCard (_community.Card4);
		Card5.SetCard (_community.Card5);
	}

	public void RevealFlop (bool state) {
	
		Card1.Reveal (state);
		Card2.Reveal (state);
		Card3.Reveal (state);
	}

	public void RevealTurn (bool state) {

		Card4.Reveal (state);
	}

	public void RevealRiver (bool state) {

		Card5.Reveal (state);
	}

	public void RevealAllCards (bool state) {

		Card1.Reveal (state);
		Card2.Reveal (state);
		Card3.Reveal (state);
		Card4.Reveal (state);
		Card5.Reveal (state);
	}
}
