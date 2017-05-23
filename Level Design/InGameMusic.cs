using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This class makes sures that the game music object do not disappear after scene change.
 */
public class InGameMusic : MonoBehaviour {

    void Awake() {    
		DontDestroyOnLoad(gameObject); // music will not be muted even if scene changes. 
    }
}
