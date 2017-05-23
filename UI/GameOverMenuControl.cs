using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverMenuControl : MonoBehaviour {

	// game over UI pieces
	public Canvas gameOver;
	public Button gameOverRestart;
	public Button gameOverMainMenu;
	public Image gameOverBackground;
	public Image gameOverText;

	// designate with level you are in
	public string levelName;

	// player and movement references
	public GameObject Player1;
	public GameObject Player2;
	private PlayerMovement Player1Movement;
	private PlayerMovement Player2Movement;

	// Use this for initialization
	void Start () {
		Player1Movement = Player1.GetComponent<PlayerMovement> ();
		Player2Movement = Player2.GetComponent<PlayerMovement> ();

		// hide game over menu in the start 
		gameOver.enabled = false;
		gameOverRestart.enabled = false;
		gameOverMainMenu.enabled = false;
		gameOverBackground.enabled = false;
		gameOverText.enabled = false;
	}
		
	public void GameOver() {
		// show game over menu
		gameOver.enabled = true;
		gameOverRestart.enabled = true;
		gameOverMainMenu.enabled = true;
		gameOverBackground.enabled = true;
		gameOverText.enabled = true;

		// stop time and prevent action
		Time.timeScale = 0.0f;
		Player1Movement.isPaused = true;
		Player2Movement.isPaused = true;

		gameOverRestart.Select ();
	}

	public void RestartLevel() {
		// hide game over menu
		gameOver.enabled = false;
		gameOverRestart.enabled = false;
		gameOverMainMenu.enabled = false;
		gameOverBackground.enabled = false;
		gameOverText.enabled = false;

		// allow events and actions
		Time.timeScale = 1.0f;
		Player1Movement.isPaused = false;
		Player2Movement.isPaused = false;
        Destroy(GameObject.Find("bgm")); // ends the background music

		// reloads scene you are in
        SceneManager.LoadScene (levelName);
	}

	public void ReturnToMainMenu() {
		// hide game over menu
		gameOver.enabled = false;
		gameOverRestart.enabled = false;
		gameOverMainMenu.enabled = false;
		gameOverBackground.enabled = false;
		gameOverText.enabled = false;

		// allow time and actions
		Time.timeScale = 1.0f;
		Player1Movement.isPaused = false;
		Player2Movement.isPaused = false;
        Destroy(GameObject.Find("bgm")); // ends the background music

		// return to main menu
        SceneManager.LoadScene ("Start");
	}
}
