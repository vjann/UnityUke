using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_StringController : StringController {

	void OnMouseDown() {
		if (Input.GetKey(KeyCode.Q)) {
			Debug.Log("mouse click and Z held");
			PlayString(1);
		} else if (Input.GetKey(KeyCode.W)) {
			Debug.Log("mouse click and Z held");
			PlayString(2);
		} else if (Input.GetKey(KeyCode.E)) {
			Debug.Log("mouse click and Z held");
			PlayString(3);
		} else {
			PlayString(0);
			Debug.Log("mouse click");
		}
		
	}
}
