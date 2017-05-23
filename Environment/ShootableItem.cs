using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootableItem : MonoBehaviour {

	public GameObject platform;

	public void TriggerShot() {
		platform.SendMessage ("Trigger");
	}
}
