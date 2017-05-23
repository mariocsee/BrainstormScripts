using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveY : MonoBehaviour {

	public GameObject referencePoint;
	public float MoveSpeed;
	public float maxDist;
	public float minDist;
	public bool On;
	
	// Update is called once per frame
	void Update () {
		RaiseEnemy ();
	}

	void RaiseEnemy() {
		if (On) {
			if (referencePoint.transform.position.y <= maxDist) {
				referencePoint.transform.position += transform.up * MoveSpeed;
				if (referencePoint.transform.position.y >= maxDist) {
					On = false;
				}
			}
			else if (referencePoint.transform.position.y >= minDist) {
				referencePoint.transform.position -= transform.up * MoveSpeed;
				if (referencePoint.transform.position.y <= minDist) {
					On = false;
				}
			}
		} 
	}

	public void Trigger() {
		On = true;
	}

}
