using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A_StringController : StringController {
	void Start() {
		stringIndex = 3;
		base.Start();
	}
	void Update() {
		base.Update();
		if (Input.GetKey(KeyCode.Alpha1)) {
			// Debug.Log("Z held");
			fretNum = 1;
		} else if (Input.GetKey(KeyCode.Alpha2)) {
			// Debug.Log("X held");
			fretNum = 2;
		} else if (Input.GetKey(KeyCode.Alpha3)) {
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
