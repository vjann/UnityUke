using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class StringController : MonoBehaviour {
	protected AudioSource[] audios;
	protected float delayScaling = 0.1f;
	protected int stringIndex;
	protected int fretNum;
	protected Vector3 prevMousePosition;
	private Vector3 stringPosition;
	private Quaternion stringRotation;

	// Start is called before the first frame update
	protected void Start() {
		stringPosition = transform.position;
		stringRotation = transform.rotation;
	  audios = GetComponents<AudioSource>();
	}

	// Update is called once per frame
	protected void Update() {
		// float rotation = transform.rotation.eulerAngles.z;
		// rotation = (rotation % 360 + 360)%360;
		// double slope = Math.Tan(rotation * (Math.PI/180f));
		// Debug.Log(transform.rotation.eulerAngles);
		// Debug.Log(slope);
		if (mouseCrossedString()) {
			PlayString(fretNum);
		} else if (Input.GetKeyDown(KeyCode.DownArrow)) {
			// Debug.Log("hi1");
			Strum(fretNum, delayScaling*stringIndex);
		} else if (Input.GetKeyDown(KeyCode.UpArrow)) {
			Strum(fretNum, delayScaling*(3-stringIndex));
		}
	}

	protected void PlayString(int fretNum) {
		audios[fretNum].Play();
		Debug.Log(stringIndex);
	}
	protected void Strum(int fretNum, float delay) {
		// Debug.Log("hi2");
		StartCoroutine(PlayWithDelay(fretNum, delay));
	}
	private IEnumerator PlayWithDelay(int fretNum, float delay) {
		// Debug.Log("hi3");
		print(Time.time);
		yield return new WaitForSeconds(delay);
		PlayString(fretNum);
		print(Time.time);
	}

	bool mouseCrossedString() {

		Vector3 point = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
		float mouseX = point.x;
		float mouseY = point.y;
		float prevMouseX = prevMousePosition.x;
		float prevMouseY = prevMousePosition.y;
		prevMousePosition = point;

		float rotation = transform.rotation.eulerAngles.z;
		rotation = (rotation % 360 + 360)%360;
		double slope = Math.Tan(rotation * (Math.PI/180f));
		// compare mouse and previous mouse position with the string.
		bool is_above = mouseY > slope*(mouseX - stringPosition.x) + stringPosition.y;
		bool was_above = prevMouseY > slope*(prevMouseX - stringPosition.x) + stringPosition.y;

		// Debug.Log(slope*(stringPosition.x - mouseX) + stringPosition.y);
		// Debug.Log(slope*(stringPosition.x - prevMouseX) + stringPosition.y);


		// if one's above and the other isn't, then mouse crossed over the string
		return is_above ^ was_above;
	}


}
