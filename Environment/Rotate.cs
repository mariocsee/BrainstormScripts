using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {

    public GameObject platform;
    public Vector3 turnspeed;

    // Use this for initialization
    void Start() {
        platform = GameObject.Find("Rotating_Platforms");
    }
	
	// Update is called once per frame
	void Update () {
        platform.transform.Rotate(turnspeed * Time.deltaTime);
    }
}
