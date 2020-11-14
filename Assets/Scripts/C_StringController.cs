using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_StringController : StringController {
	void Start() {
		stringIndex = 1;
		base.Start();
	}
	void Update() {
		base.Update();
		if (Input.GetKey(KeyCode.A)) {
			// Debug.Log("Z held");
			fretNum = 1;
		} else if (Input.GetKey(KeyCode.S)) {
			// Debug.Log("X held");
			fretNum = 2;
		} else if (Input.GetKey(KeyCode.D)) {
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
