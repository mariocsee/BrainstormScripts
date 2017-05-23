using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour {

	// Enter the name of the next scene in the Unity Engine
	public string nextScene;

	// Takes P1 (Player1) and P2 (Player2) 
	public Transform P1;
	public Transform P2;

	// Needs Player1Health and Player2Health
	PlayerHealth P1Health;
	PlayerHealth P2Health;

	// Reference to triggers that use CheckPlayer 
	public Transform checkP1Plane;
	public Transform checkP2Plane;

	// To check if P1 and P2 are in the trigger
	CheckPlayer checkP1;
	CheckPlayer checkP2;

	// Use this for initialization
	void Start () {
		// Instantiate references to player arrival checking scripts
		checkP1 = checkP1Plane.GetComponent<CheckPlayer> ();
		checkP2 = checkP2Plane.GetComponent<CheckPlayer> ();

		// Instantiate references to player healths
		P1Health = P1.GetComponent<PlayerHealth> ();
		P2Health = P2.GetComponent<PlayerHealth> ();
	}
	
	// Update is called once per frame
	void Update () {
		// Checks if both players are not dead
		if (!P1Health.isDeadForever && !P2Health.isDeadForever) {
			// Checks for arrival of both players
			if (checkP1.isPlayerHere && checkP2.isPlayerHere) {
				SceneManager.LoadScene (nextScene);
			}
		} else if (!P1Health.isDeadForever && P2Health.isDeadForever) { // P2 is dead so look for P1
			if (checkP1.isPlayerHere) {
				SceneManager.LoadScene (nextScene);
			}
		} else if (P1Health.isDeadForever && !P2Health.isDeadForever) { // P1 is dead so look for P2
			if (checkP2.isPlayerHere) {
				SceneManager.LoadScene (nextScene);
			}
		}
	}

}
