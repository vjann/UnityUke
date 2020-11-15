using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ChordHelperController : MonoBehaviour {
  public Transform[] strings;
  public string prevChord;
  public string chord;
  private MeshRenderer[,] dotRenderers;

  // Start is called before the first frame update
  void Start() {
    this.chord = "0 0 0 0";
    MeshRenderer[,] renderers = new MeshRenderer[4, 3];
    for (int i=0; i < 4; i++) {
      //i is the stringIndex, with G string being index 0.
      //take the string at i, loop through its dots, adding its renderer to this.renderers
      for (int j=1; j < 4; j++) {
        Transform jth_dot = strings[i].Find("Fret " + j.ToString() + "/Cylinder");
        renderers[i, j-1] = jth_dot.GetComponent<MeshRenderer>();
      }
    }
    this.dotRenderers = renderers;
  }

  public void testFunc(string GCEA) {
    this.chord = GCEA;
    Debug.Log(GCEA);
  }

  // Update is called once per frame
  void Update() {
    if (this.prevChord == this.chord) {
      return;
    }
    // Debug.Log(dotRenderers);
    // this.dotRenderers[1, 1].material.color = Color.green;
    this.prevChord = this.chord;
    showChord(this.chord);
  }

  private void showChord(string chord) {
    string[] fretNum = chord.Split(' ');
    // for fret in fretNum (e.g. ['0', '2', '3', '2'] is a G chord)
    for (int i=0; i < 4; i++) {
      //i is the stringIndex, with G string being index 0.
      // convert to int (2 being 2nd fret on the i-th string)
      int fret = Int32.Parse(fretNum[i]);
      //take the string at i, loop through it's frets, setting correct color
      for (int j=0; j < 3; j++) {
        if (j + 1 == fret) {
          this.dotRenderers[i, j].material.color = Color.green;
        } else {
          this.dotRenderers[i, j].material.color = Color.grey;
        }
      }
    }
    return;
  }

}
