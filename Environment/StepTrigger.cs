using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepTrigger : MonoBehaviour {

	public GameObject Thing;

	void OnTriggerEnter(Collider other){
		if (other.CompareTag("Player")) {
			Thing.SendMessage ("Trigger");
		}
	}
}
