using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ActionPanelScript : MonoBehaviour {

	public GameManagerScript GameManager;

	public Text ActionText;
	public Text CallRaiseButtonText;
	public InputField CallRaiseValueText;
	public Text CheckFoldButtonText;

	public Button IncreaseBetButton;
	public Button DecreaseBetButton;

	private int _bet;
	private int _modBet;
	private int _call;
	private int _minBet;
	private int _cash;
	private int _maxBet;

	private Game _game;
	private Player _player;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetActive (bool state) {
	
		gameObject.SetActive (state);
	}

	public void SetCallRaiseValue (int value, bool modifyText = true) {

		value = (int) (_minBet * Mathf.Ceil (value / (float)_minBet));

		if (value < _call) {

			value = _call;
		}

		if (value > _maxBet) {

			value = _maxBet;
		}

		if (modifyText) {
			CallRaiseValueText.text = "$" + value.ToString ();
		}

		if (value <= _call) {
		
			CallRaiseButtonText.text = "Call";
		} else if (value >= _maxBet) {

			CallRaiseButtonText.text = "All In";
		} else {
		
			CallRaiseButtonText.text = "Raise";
		}

		_modBet = value;

		IncreaseBetButton.interactable = _modBet < _maxBet;
		DecreaseBetButton.interactable = _modBet > Mathf.Max(_call, _game.MinimumBet);
	}

	public void Setup () {
	
		_game = GameManagerScript.CurrentGame;
		_player = _game.CurrentPlayer;

		_minBet = _game.MinimumBet;
		_call = _game.CurrentCall;
		_bet = _player.Bet;
		_cash = _player.Cash;
		_maxBet = _cash + _bet;

		ActionText.text = "Player " + _player.Index + " - Current Bet: $" + _player.Bet.ToString ();

		SetCallRaiseValue (Mathf.Max(_game.CurrentCall, _game.MinimumBet));

		if (_bet >= _call) {

			CheckFoldButtonText.text = "Check";
		} else {

			CheckFoldButtonText.text = "Fold";
		}
	}

	public void IncreaseBet () {

		_modBet += _minBet;

		SetCallRaiseValue (_modBet);
	}

	public void DecreaseBet () {

		_modBet -= _minBet;

		if (_modBet <= _call) {
		
			_modBet = _call;
		}

		SetCallRaiseValue (_modBet);
	}

	public void BetChanged (string valueStr) {
	
		int value = int.Parse (valueStr);

		SetCallRaiseValue (value);
	}

	public void CallOrRaise () {

		if (_modBet > _call) {
			_player.Raise (_modBet);
		} else {
			_player.Call (_modBet);
		}

		GameManager.PassToNextPlayer ();
	}

	public void CheckOrFold () {

		if (_bet < _call) {
			_player.Fold ();
		} else {
			_player.Check ();
		}

		GameManager.PassToNextPlayer ();
	}
}
