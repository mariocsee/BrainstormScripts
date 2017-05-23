using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationTrigger1 : MonoBehaviour {

	public Transform playerDestination;
	Collider Mcollider;

	// Use this for initialization
	void Start () {
		Mcollider = GetComponent<MeshCollider>();
	}

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag ("Player")) {
			other.transform.position = playerDestination.position;
			other.transform.rotation = playerDestination.rotation;
		}
		Mcollider.enabled = false;
	}
}