using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_StringController : StringController {
	void Start() {
		stringIndex = 2;
		base.Start();
	}
	void Update() {
		base.Update();
		if (Input.GetKey(KeyCode.Q)) {
			// Debug.Log("Z held");
			fretNum = 1;
		} else if (Input.GetKey(KeyCode.W)) {
			// Debug.Log("X held");
			fretNum = 2;
		} else if (Input.GetKey(KeyCode.E)) {
			// Debug.Log("C held");
			fretNum = 3;
		} else {
			fretNum = 0;
			// Debug.Log("nothing held");
		}
	}
	void OnMouseDown() {
		PlayString(fretNum);
	}
}
