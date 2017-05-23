using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InGameMenuControl : MonoBehaviour {

	// pause menu UI pieces
	public Canvas pauseMenu;
	public Button pauseMenuResume;
	public Button pauseMenuQuit;
	public Image pauseMenuBackground;
	public Image pauseMenuText;

	// player and playermovement reference
	public GameObject Player1;
	public GameObject Player2;
	private PlayerMovement Player1Movement;
	private PlayerMovement Player2Movement;

	// Use this for initialization
	void Start () {
		Player1Movement = Player1.GetComponent<PlayerMovement> ();
		Player2Movement = Player2.GetComponent<PlayerMovement> ();

		pauseMenu = pauseMenu.GetComponent<Canvas> ();
		pauseMenuBackground = pauseMenuBackground.GetComponent<Image> ();
		pauseMenuText = pauseMenuText.GetComponent<Image> ();
		pauseMenuResume = pauseMenuResume.GetComponent<Button> ();
		pauseMenuQuit = pauseMenuQuit.GetComponent<Button> ();

		// hide pause menu
		pauseMenu.enabled = false;
		pauseMenuResume.enabled = false;
		pauseMenuQuit.enabled = false;
		pauseMenuBackground.enabled = false;
		pauseMenuText.enabled = false;

		// allow events 
        Time.timeScale = 1.0f;
        Player1Movement.isPaused = false;
        Player2Movement.isPaused = false;
    }
	
	public void PauseGame() {
		// show pause menu
		pauseMenu.enabled = true;
		pauseMenuResume.enabled = true;
		pauseMenuQuit.enabled = true;
		pauseMenuBackground.enabled = true;
		pauseMenuText.enabled = true;

		// stop time and prevent events
		Time.timeScale = 0.0f;
		Player1Movement.isPaused = true;
		Player2Movement.isPaused = true;

		pauseMenuResume.Select ();
	}

	public void ResumeGame() {
		// hide pause menu
		pauseMenu.enabled = false;
		pauseMenuResume.enabled = false;
		pauseMenuQuit.enabled = false;
		pauseMenuBackground.enabled = false;
		pauseMenuText.enabled = false;

		// allow events 
		Player1Movement.isPaused = false;
		Player2Movement.isPaused = false;
		Time.timeScale = 1.0f;
	}

	public void ReturnToMenu() {
		// hide pause menu
		pauseMenu.enabled = false;
		pauseMenuResume.enabled = false;
		pauseMenuQuit.enabled = false;
		pauseMenuBackground.enabled = false;
		pauseMenuText.enabled = false;

        Destroy(GameObject.Find("bgm")); // ends the background music

		// allow events
        Player1Movement.isPaused = false;
		Player2Movement.isPaused = false;
		Time.timeScale = 1.0f;

		// return to main menu
		SceneManager.LoadScene ("Start");
	}
}
