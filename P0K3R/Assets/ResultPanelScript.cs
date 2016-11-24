﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ResultPanelScript : MonoBehaviour {

	public HandPanelScript HandPanelPrefab;

	public Button ContinueButton;

	private HandPanelScript[] _handPanels;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Initialize (int playerCount) {

		_handPanels = new HandPanelScript[playerCount];

		Vector3 localScale = HandPanelPrefab.transform.localScale;

		for (int i = 0; i < playerCount; i++) {
			HandPanelScript handPanel = GameObject.Instantiate (HandPanelPrefab) as HandPanelScript;

			handPanel.transform.SetParent (gameObject.transform);
			handPanel.transform.localScale = localScale;
		}

		HandPanelPrefab.SetActive (false);

		ContinueButton.transform.SetParent (null);
		ContinueButton.transform.SetParent (gameObject.transform);
	}

	public void SetActive (bool state) {

		gameObject.SetActive (state);
	}

	public void SetPlayer (Player player, List<Card> hand, int earnings, int index) {
	
		_handPanels [index].Initialize (player, hand, earnings);
	}

	public void SetPlayer (Player player, int earnings, int index) {

		_handPanels [index].Initialize (player, earnings);
	}
}
