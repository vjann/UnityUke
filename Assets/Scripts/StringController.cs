using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class StringController : MonoBehaviour {
	protected AudioSource[] audioSources;
	public AudioClip[] audioClips;
	protected float delayScaling = 0.1f;
	public int stringIndex;
	public KeyCode[] fretKeyCodes;
	protected int fretNum;
	protected Vector3 prevMousePosition;

	public Transform StartPoint;
	public Transform EndPoint;
	public Color startColor;
	public Color endColor;
	public Material stringMaterial;
	private LineRenderer lineRenderer;
	private RopePoint[] ropePoints;
	private int numRopePoints = 50;
	private float lineWidth = 2.5f;
	private float decay = 0f;
	private float decay_rate = 0.998f;
	private Vector2 orthonormal_vector; // unit vector orthogonal to the string

	// Start is called before the first frame update
	protected void Start() {
		AudioSource[] audios = new AudioSource[this.audioClips.Length];
		for (int i=0; i< this.audioClips.Length; i++) {
			AudioSource audioSource = gameObject.AddComponent<AudioSource>();
			audioSource.clip = this.audioClips[i];
			audios[i] = audioSource;
		}
	  this.audioSources = audios;


		this.lineRenderer = this.GetComponent<LineRenderer>();
		lineRenderer.material = this.stringMaterial;
		lineRenderer.startColor = this.startColor;
		lineRenderer.endColor = this.endColor;
    Vector3 ropeStartPoint = StartPoint.position;
    Vector3 ropeEndPoint = EndPoint.position;
    Vector3 segmentVector = (ropeEndPoint - ropeStartPoint)/this.numRopePoints;
    Vector3 startToEnd = ropeStartPoint - ropeEndPoint;
    this.orthonormal_vector = (new Vector2(startToEnd.y, startToEnd.x)).normalized;
    this.ropePoints = new RopePoint[this.numRopePoints];

    for (int i = 0; i < this.numRopePoints; i++) {
      this.ropePoints[i] = new RopePoint(ropeStartPoint + i*segmentVector);
    }
	}

	// Update is called once per frame
	protected void Update() {
		this.DrawRope();
	}

	private void FixedUpdate() {
		bool openString = true;
		for (int i=0; i<this.fretKeyCodes.Length; i++) {
			if (Input.GetKey(this.fretKeyCodes[i])) {
				fretNum = i+1;
				openString = false;
				break;
			}
		}
		if (openString) fretNum = 0;

		if (mouseCrossedString()) {
			PlayString(fretNum);
		} else if (Input.GetKeyDown(KeyCode.DownArrow)) {
			// Debug.Log("hi1");
			Strum(fretNum, delayScaling*stringIndex);
		} else if (Input.GetKeyDown(KeyCode.UpArrow)) {
			Strum(fretNum, delayScaling*(3-stringIndex));
		}
		this.prevMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
		Simulate();
	}

	protected void PlayString(int fretNum) {
		this.audioSources[fretNum].Play();
		this.decay = 40f;
		// Debug.Log(stringIndex);
	}
	protected void Strum(int fretNum, float delay) {
		// Debug.Log("hi2");
		StartCoroutine(PlayWithDelay(fretNum, delay));
	}
	private IEnumerator PlayWithDelay(int fretNum, float delay) {
		// Debug.Log("hi3");
		print(Time.time);
		yield return new WaitForSeconds(delay);
		PlayString(fretNum);
		print(Time.time);
	}

	bool mouseCrossedString() {
		Vector3 currMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
		// source: https://stackoverflow.com/questions/3838329/how-can-i-check-if-two-segments-intersect
		Func<Vector3, Vector3, Vector3, bool> ccw = (A, B, C) => (C.y-A.y) * (B.x-A.x) > (B.y-A.y) * (C.x-A.x);
		return ccw(StartPoint.position, prevMousePosition, currMousePosition) != ccw( EndPoint.position, prevMousePosition, currMousePosition) &&
						ccw(StartPoint.position, EndPoint.position, prevMousePosition) != ccw(StartPoint.position, EndPoint.position, currMousePosition);
	}

	private void DrawRope() {
		float lineWidth = this.lineWidth;
		lineRenderer.startWidth = lineWidth;
		lineRenderer.endWidth = lineWidth;

		Vector3[] ropePointsPositions = new Vector3[this.numRopePoints];
		for (int i = 0; i < this.numRopePoints; i++) {
			ropePointsPositions[i] = this.ropePoints[i].posNow;
		}

		lineRenderer.positionCount = ropePointsPositions.Length;
		lineRenderer.SetPositions(ropePointsPositions);
	}

  private void Simulate() {
    for (int i=1; i < this.numRopePoints; i++) {
      RopePoint point = ropePoints[i];
      float displacement =  decay * (float) Math.Sin(Math.PI * i / numRopePoints);
      point.posNow = point.posOrigin + displacement * this.orthonormal_vector * (float) Math.Cos(60*Time.time);
      this.ropePoints[i] = point;
      this.decay *= this.decay_rate;
    }
  }

	public struct RopePoint {
    public Vector2 posNow;
    public Vector2 posOrigin;

    public RopePoint(Vector2 pos) {
      this.posNow = pos;
      this.posOrigin = pos;
    }
  }
}
