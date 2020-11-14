using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G_StringController : StringController {
	void Start() {
		stringIndex = 0;
		base.Start();
	}
	void Update() {
		base.Update();
		if (Input.GetKey(KeyCode.Z)) {
			// Debug.Log("Z held");
			fretNum = 1;
		} else if (Input.GetKey(KeyCode.X)) {
			// Debug.Log("X held");
			fretNum = 2;
		} else if (Input.GetKey(KeyCode.C)) {
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
