using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class YouWinMenuControl : MonoBehaviour {

	// canvas and its pieces
	public Canvas youWin;
	public Button youWinMainMenu;
	public Image youWinBackground;
	public Image greyBackground;

	// reference to player and movement
	public GameObject Player1;
	public GameObject Player2;
	private PlayerMovement Player1Movement;
	private PlayerMovement Player2Movement;

	// Use this for initialization
	void Start () {
		Player1Movement = Player1.GetComponent<PlayerMovement> ();
		Player2Movement = Player2.GetComponent<PlayerMovement> ();

		// hide all UI first!
		youWin.enabled = false;
		youWinMainMenu.enabled = false;
		youWinBackground.enabled = false;
		greyBackground.enabled = false;
	}
	
	public void YouWin() {
		// show UI when win function is called
		youWin.enabled = true;
		youWinMainMenu.enabled = true;
		youWinBackground.enabled = true;
		greyBackground.enabled = true;

		// stop time and prevent actions
		Time.timeScale = 0.0f;
		Player1Movement.isPaused = true;
		Player2Movement.isPaused = true;
        Destroy(GameObject.Find("bgm")); // ends the background music

		youWinMainMenu.Select ();
	}

	public void RestartGame() {
		// hide victory UI
		youWin.enabled = false;
		youWinMainMenu.enabled = false;
		youWinBackground.enabled = false;
		greyBackground.enabled = false;

		// allow time and events
		Time.timeScale = 1.0f;
		Player1Movement.isPaused = false;
		Player2Movement.isPaused = false;

		// start from beginning
		SceneManager.LoadScene ("Lv1Ch1");
	}

	public void ReturnToMainMenu() {
		// hide victory UI
		youWin.enabled = false;
		youWinMainMenu.enabled = false;
		youWinBackground.enabled = false;
		greyBackground.enabled = false;

		// allow time and events
		Time.timeScale = 1.0f;
		Player1Movement.isPaused = false;
		Player2Movement.isPaused = false;

		// return to main menu
		SceneManager.LoadScene ("Start");
	}
}
