using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class RopeController2 : MonoBehaviour {
    public Transform StartPoint;
    public Transform EndPoint;

    private LineRenderer lineRenderer;
    private List<RopePoint> ropePoints = new List<RopePoint>();
    private int numRopePoints = 50;
    private float lineWidth = 0.3f;
    private float decay = 1f;
    private float decay_rate = 0.9998f;
    private Vector2 orthonormal_vector; // unit vector orthogonal to the string

    // Use this for initialization
    void Start() {
        this.lineRenderer = this.GetComponent<LineRenderer>();
        Vector3 ropeStartPoint = StartPoint.position;
        Vector3 ropeEndPoint = EndPoint.position;
        Vector3 segmentVector = (ropeEndPoint - ropeStartPoint)/this.numRopePoints;
        Vector3 startToEnd = ropeStartPoint - ropeEndPoint;
        this.orthonormal_vector = (new Vector2(startToEnd.y, startToEnd.x)).normalized;

        for (int i = 0; i < numRopePoints; i++) {
            this.ropePoints.Add(new RopePoint(ropeStartPoint + i*segmentVector));
        }
    }

    // Update is called once per frame
    void Update() {
        this.DrawRope();
    }
    void OnMouseDown() {
      Debug.Log("rope twangggggg");
    }

    private void FixedUpdate() {
      Simulate();

    }
    private void Simulate() {
      for (int i=1; i < this.numRopePoints; i++) {
        RopePoint point = this.ropePoints[i];
        float displacement =  decay * (float) Math.Sin(Math.PI * i / numRopePoints);
        point.posNow = point.posOrigin + displacement * this.orthonormal_vector * (float) Math.Cos(60*Time.time);
        this.ropePoints[i] = point;
        this.decay *= this.decay_rate;
      }
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

    public struct RopePoint {
        public Vector2 posNow;
        public Vector2 posOrigin;

        public RopePoint(Vector2 pos) {
            this.posNow = pos;
            this.posOrigin = pos;
        }
    }
}
