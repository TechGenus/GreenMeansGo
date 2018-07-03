using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour {
	public Transform[] patrolPoints;
	public float moveSpeed;

	private Transform t;
	private int currentPoint;

	// Use this for initialization
	void Start() {
		t = GetComponent<Transform>();
		t.position = patrolPoints[0].position;
		currentPoint = 0;
	}

	// Update is called once per frame
	void Update() {
		if (t.position == patrolPoints[currentPoint].position) currentPoint++;

		if (currentPoint > patrolPoints.Length - 1) currentPoint = 0;

		t.position = Vector3.MoveTowards(t.position, patrolPoints[currentPoint].position, moveSpeed * Time.deltaTime);
	}
}
