using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_StringController : StringController {

	void OnMouseDown() {
		if (Input.GetKey(KeyCode.A)) {
			Debug.Log("mouse click and Z held");
			PlayString(1);
		} else if (Input.GetKey(KeyCode.S)) {
			Debug.Log("mouse click and Z held");
			PlayString(2);
		} else if (Input.GetKey(KeyCode.D)) {
			Debug.Log("mouse click and Z held");
			PlayString(3);
		} else {
			PlayString(0);
			Debug.Log("mouse click");
		}
		
	}
}
