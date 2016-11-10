using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CardScript : MonoBehaviour {

	public Sprite[] Faces = new Sprite[52];

	public Image Image;

	// Use this for initialization
	void Start () {

		SetFace (12);
	}

	// Update is called once per frame
	void Update () {
	
	}

	public void SetFace (int index) {

		Image.sprite = Faces [index];
	}
}
