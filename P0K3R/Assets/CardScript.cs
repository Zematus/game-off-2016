﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CardScript : MonoBehaviour {

	public Sprite[] Faces = new Sprite[53];

	public Image Image;

	private Card _currentCard = Card.Empty;

	public bool _revealed = false;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
	
	}

	public void SetActive (bool state) {

		gameObject.SetActive (state);
	}

	public void SetCard (Card card) {

		_currentCard = card;

		if (_currentCard == Card.Empty) {

			gameObject.SetActive (false);
		} else {

			gameObject.SetActive (true);

			if (_revealed) {
				SetFace (_currentCard);
			}
		}
	}

	public void Reveal (bool state) {

		if (_currentCard == Card.Empty)
			return;

		if (state)
			SetFace (_currentCard);
		else
			SetFace (Card.Back);

		_revealed = state;
	}

	private void SetFace (Card card) {

		Image.sprite = Faces [(int)card];
	}

	private void SetFace (int index) {

		Image.sprite = Faces [index];
	}
}
