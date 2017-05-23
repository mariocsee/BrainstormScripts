using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class YouWinTrigger : MonoBehaviour {

	// Players
	public Transform P1;
	public Transform P2;

	// health scripts to check if players are alive
	PlayerHealth P1Health;
	PlayerHealth P2Health;

	// Reference to triggers that use CheckPlayer 
	public Transform checkP1Plane;
	public Transform checkP2Plane;

	// To check if P1 and P2 are in the trigger
	CheckPlayer checkP1;
	CheckPlayer checkP2;

	// Victory UI and Menu 
	public Canvas youWinCanvas;
	YouWinMenuControl youWinScript;

	// Use this for initialization
	void Start () {
		// Instantiate references to player arrival checking scripts
		checkP1 = checkP1Plane.GetComponent<CheckPlayer> ();
		checkP2 = checkP2Plane.GetComponent<CheckPlayer> ();

		// Instantiate references to health
		P1Health = P1.GetComponent<PlayerHealth> ();
		P2Health = P2.GetComponent<PlayerHealth> ();

		// access to the victory canvas and menu UI
		youWinScript = youWinCanvas.GetComponent<YouWinMenuControl> ();
	}

	// Update is called once per frame
	void Update () {
		if (!P1Health.isDeadForever && !P2Health.isDeadForever) { // if both players alive, check for both
			if (checkP1.isPlayerHere && checkP2.isPlayerHere) {
				youWinScript.YouWin ();
			}
		} else if (!P1Health.isDeadForever && P2Health.isDeadForever) { // p2 dead, check for p1
			if (checkP1.isPlayerHere) {
				youWinScript.YouWin ();
			}
		} else if (P1Health.isDeadForever && !P2Health.isDeadForever) { // p1 dead, check for p2
			if (checkP2.isPlayerHere) {
				youWinScript.YouWin (); // victory!
			}
		}
	}

}
