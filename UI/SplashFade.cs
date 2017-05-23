using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SplashFade : MonoBehaviour {

	public Image splashImage;
	public Image splashImage1;
	public Image splashImage2;
	public Image splashImage3;
	public Image splashImage4;

	IEnumerator Start() {
		splashImage.canvasRenderer.SetAlpha (0.0f);
		splashImage1.canvasRenderer.SetAlpha (0.0f);
		splashImage2.canvasRenderer.SetAlpha (0.0f);
		splashImage3.canvasRenderer.SetAlpha (0.0f);
		splashImage4.canvasRenderer.SetAlpha (0.0f);

		Fade (1.0f, 1.5f);
		yield return new WaitForSeconds (2.5f);
		Fade (0.0f, 2.5f);
		yield return new WaitForSeconds (2.5f);

		SceneManager.LoadScene ("Lv1Ch4"); 
	}

	void Fade(float alpha, float time) {
		splashImage.CrossFadeAlpha (alpha, time, false);
		splashImage1.CrossFadeAlpha (alpha, time, false);
		splashImage2.CrossFadeAlpha (alpha, time, false);
		splashImage3.CrossFadeAlpha (alpha, time, false);
		splashImage4.CrossFadeAlpha (alpha, time, false);
	}
		
}
