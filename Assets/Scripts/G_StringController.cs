using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G_StringController : StringController {
	void Start() {
		upstrumDelay = 0.2f;
		downstrumDelay = 0f;
		base.Start();
	}
	void Update() {
		if (Input.GetKeyDown(KeyCode.UpArrow)) {
			Strum(false, 0);
		} else if (Input.GetKeyDown(KeyCode.DownArrow)) {
			Strum(true, 0);
		}
	}
	void OnMouseDown() {
		if (Input.GetKey(KeyCode.Z)) {
			Debug.Log("mouse click and Z held");
			PlayString(1);
		} else if (Input.GetKey(KeyCode.X)) {
			Debug.Log("mouse click and Z held");
			PlayString(2);
		} else if (Input.GetKey(KeyCode.C)) {
			Debug.Log("mouse click and Z held");
			PlayString(3);
		} else {
			PlayString(0);
			Debug.Log("mouse click");
		}

	}
}
