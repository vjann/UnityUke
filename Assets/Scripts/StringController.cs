using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StringController : MonoBehaviour {
	protected AudioSource[] audios;
	protected float upstrumDelay;
	protected float downstrumDelay;

	// Start is called before the first frame update
	protected void Start() {
	  	audios = GetComponents<AudioSource>();
	}

	// Update is called once per frame
	void Update() {
		float click = Input.GetAxis("Fire1");
		Debug.Log(click);
	}

	protected void PlayString(int fretNum) {
		audios[fretNum].Play();
	}
	protected void Strum(bool downstrum, int fretNum) {
		PlayWithDelay(audio[fretNum], downstrum ? downstrumDelay : upstrumDelay);
	}
	private IEnumerator PlayWithDelay(AudioSource audio, float delay) {
		print(Time.time);
		yield return new WaitForSeconds(delay);
		audio.Play();
		print(Time.time);
	}

}
