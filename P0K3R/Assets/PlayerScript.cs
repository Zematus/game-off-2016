using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerScript : MonoBehaviour {
	
	public CardScript Card1;
	public CardScript Card2;

	public Text Cash;

	public Image DealerButton;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetCash (int amount) {
	
		Cash.text = "$" + amount.ToString ();
	}

	public void ShowDealerButton (bool state) {

		DealerButton.gameObject.SetActive (state);
	}
}
