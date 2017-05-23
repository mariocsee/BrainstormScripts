using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicDetector : MonoBehaviour {

    public GameObject music;

	// Use this for initialization
	void Start () {
		if (GameObject.Find("bgm") == null) {
            GameObject bgm = (GameObject)Instantiate(music, Vector3.zero, Quaternion.identity);
            bgm.name = "bgm";
        } 
    }
}
