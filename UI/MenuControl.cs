using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour {

	// UI buttons and components
	public Button startText;
	public Button exitText;
	public Canvas quitMenu;
	public Button quitMenuNo;
	public Button quitMenuYes;

	// Use this for initialization
	void Start () {
		quitMenu = quitMenu.GetComponent<Canvas> ();
		startText = startText.GetComponent<Button> ();
		exitText = exitText.GetComponent<Button> ();

		// hide "are you sure menu" on first load
		quitMenu.enabled = false; 
		quitMenuNo.enabled = false;
		quitMenuYes.enabled = false;
	}

	public void ExitPress() {
		startText.enabled = false; 
		exitText.enabled = false; 

		// ask if they really wanna quit
		quitMenu.enabled = true; 
		quitMenuNo.enabled = true;
		quitMenuYes.enabled = true;

		// selects button for controller UI navigation
		quitMenuNo.Select();

	}

	public void NoPress() {
		startText.enabled = true; 
		exitText.enabled = true; 

		// hide "are u sure?" menu
		quitMenu.enabled = false; 
		quitMenuNo.enabled = false;
		quitMenuYes.enabled = false;
		exitText.Select ();
	}

	public void StartLevel() {
		SceneManager.LoadScene ("Lv1Ch1"); // load first checkpoint of first level
	}

	public void ExitGame() {
		Application.Quit(); // quits application
	}
}
