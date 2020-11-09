﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A_StringController : StringController {

	void OnMouseDown() {
		if (Input.GetKey(KeyCode.Alpha1)) {
			Debug.Log("mouse click and Z held");
			PlayString(1);
		} else if (Input.GetKey(KeyCode.Alpha2)) {
			Debug.Log("mouse click and Z held");
			PlayString(2);
		} else if (Input.GetKey(KeyCode.Alpha3)) {
			Debug.Log("mouse click and Z held");
			PlayString(3);
		} else {
			PlayString(0);
			Debug.Log("mouse click");
		}
		
	}
}