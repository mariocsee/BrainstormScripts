using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIFadeInOut : MonoBehaviour {

	// parent of images for easy one GameObject access
	public Component parent;

	// images array and length
	Image[] images;
	float imagesLength;

	// how many seconds the UI tutorial is shown
	public float secondsShown;

	// Use this for initialization
	void Start () {
		// fill images array with UI images
		images = parent.GetComponentsInChildren<Image> ();
		imagesLength = images.Length; // length necessary for while exit clause

		// set all images to 0 alpha (invisible)
		int i = 0;
		while (i < imagesLength) {
			images [i].color = new Color(images[i].color.r,images[i].color.g,images[i].color.b, 0f);
			i = i + 1;
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag ("Player")) {
			// calling function for IEnumerator types
			StartCoroutine (ShowMessage ());
		}
	}

	IEnumerator ShowMessage() {

		// loop through array to show all photos by increasing alpha to 1
		int i = 0;
		while (i < imagesLength) {
			images [i].color = Color.Lerp (new Color (images [i].color.r, images [i].color.g, images [i].color.b, 0f), 
				new Color (images [i].color.r, images [i].color.g, images [i].color.b, 1f), 1f);
			i = i + 1;
		}

		// wait set time
		yield return new WaitForSeconds (secondsShown);

		// loop through array to hide all photos by decreasing alpha to 0
		i = 0;
		while (i < imagesLength) {
			images [i].color = Color.Lerp (new Color (images [i].color.r, images [i].color.g, images [i].color.b, 1f), 
				new Color (images [i].color.r, images [i].color.g, images [i].color.b, 0f), 1f);
			i = i + 1;
		}

		// UI triggers are only once as to not bombard player with instructions/tips
		Destroy (this);
	}
}
