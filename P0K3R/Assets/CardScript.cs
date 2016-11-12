﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CardScript : MonoBehaviour {

	public Sprite[] Faces = new Sprite[53];

	public Image Image;

	private Card _currentCard = Card.Empty;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
	
	}

	public void SetCard (Card card) {

		_currentCard = card;

		if (_currentCard == Card.Empty) {

			gameObject.SetActive (false);
		} else {

			gameObject.SetActive (true);
		}
	}

	public void HideCard () {

		if (_currentCard == Card.Empty)
			return;

		SetFace (Card.Back);
	}

	public void ShowCard () {

		if (_currentCard == Card.Empty)
			return;

		SetFace (_currentCard);
	}

	private void SetFace (Card card) {

		Image.sprite = Faces [(int)card];
	}

	private void SetFace (int index) {

		Image.sprite = Faces [index];
	}
}
