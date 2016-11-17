using UnityEngine;
using System.Collections;

public class CommunityScript : MonoBehaviour {

	public CardScript Card1;
	public CardScript Card2;
	public CardScript Card3;
	public CardScript Card4;
	public CardScript Card5;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ShowFlop (bool state) {
	
		Card1.Show (state);
		Card2.Show (state);
		Card3.Show (state);
	}

	public void ShowTurn (bool state) {

		Card4.Show (state);
	}

	public void ShowRiver (bool state) {

		Card5.Show (state);
	}

	public void ShowAllCards (bool state) {

		Card1.Show (state);
		Card2.Show (state);
		Card3.Show (state);
		Card4.Show (state);
		Card5.Show (state);
	}
}
