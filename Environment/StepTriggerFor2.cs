using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepTriggerFor2 : MonoBehaviour {

	public GameObject Thing1;
	public GameObject Thing2;

	void OnTriggerEnter(Collider other){
		if (other.CompareTag("Player")) {
			Thing1.SendMessage ("Trigger");
			Thing2.SendMessage ("Trigger");
		}
		Destroy (this);
	}
}
