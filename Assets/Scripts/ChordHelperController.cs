using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ChordHelperController : MonoBehaviour {
  public Transform[] strings;
  public string prevChord;
  public string chord;
  public StringController gString;
  public StringController cString;
  public StringController eString;
  public StringController aString;
  private int[] fretsPressed;
  private MeshRenderer[,] dotRenderers;

  // Start is called before the first frame update
  void Start() {
    this.chord = "0 0 0 0";
    MeshRenderer[,] renderers = new MeshRenderer[4, 5];
    for (int i=0; i < 4; i++) {
      //i is the stringIndex, with G string being index 0.
      //take the string at i, loop through its dots, adding its renderer to this.renderers
      for (int j=1; j < 6; j++) {
        Transform jth_dot = strings[i].Find("Fret " + j.ToString() + "/Cylinder");
        renderers[i, j-1] = jth_dot.GetComponent<MeshRenderer>();
      }
    }
    this.dotRenderers = renderers;
  }

  public void setChord(string GCEA) {
    this.chord = GCEA;
  }

  // Update is called once per frame
  void Update() {
    this.updateFretsPressed();
    // if (this.prevChord == this.chord) {
    //   return;
    // }
    this.prevChord = this.chord;
    showChord(this.chord);
  }

  private void updateFretsPressed() {
    Debug.Log("hi");
    int GFretNum = this.gString.getFretNum();
    int CFretNum = this.cString.getFretNum();
    int EFretNum = this.eString.getFretNum();
    int AFretNum = this.aString.getFretNum();
    this.fretsPressed = new int[]{GFretNum, CFretNum, EFretNum, AFretNum};
  }

  private void showChord(string chord) {
    string[] fretNum = chord.Split(' ');
    // for string in fretNum (e.g. ['0', '2', '3', '2'] is a G chord)
    for (int i=0; i < 4; i++) {
      //i is the stringIndex, with G string being index 0.
      // convert to int (2 being 2nd fret on the i-th string)
      int correctFret = Int32.Parse(fretNum[i]);
      int pressedFret = this.fretsPressed[i];
      //take the string at i, loop through it's frets, setting correct color
      for (int j=0; j < 5; j++) {
        if (j + 1 == correctFret && j + 1 == pressedFret) {
          this.dotRenderers[i, j].material.color = Color.green;
        } else if (j + 1 == correctFret) {
          this.dotRenderers[i, j].material.color = Color.blue;
        } else if (j + 1 == pressedFret && this.chord != "0 0 0 0") {
          this.dotRenderers[i, j].material.color = Color.red;
        } else {
          this.dotRenderers[i, j].material.color = Color.grey;
        }
      }
    }
    return;
  }

}
