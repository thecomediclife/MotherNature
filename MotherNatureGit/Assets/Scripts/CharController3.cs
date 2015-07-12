﻿using UnityEngine;
using System.Collections;

public class CharController3 : MonoBehaviour {
	public bool paused;

	public Transform checkFront;
	public Transform checkRight;
	public Transform checkLeft;
	public Transform checkBack;

	public float moveSpeed = 2;

	public Vector3[] endPosArray = new Vector3[3];
	public Vector3 endPos;
	private float[] addedAngle = new float[4];
	public float currentAngle;

	int counter = 0;
	int index;

	Vector3 imposVec = new Vector3 (1000,0,0);

	void Awake () 
	{
		checkFront = transform.Find ("CheckFront");
		checkRight = transform.Find ("CheckRight");
		checkLeft = transform.Find ("CheckLeft");
		checkBack = transform.Find ("CheckBack");
	}

	void Start () 
	{
		endPos = transform.position;
		currentAngle = transform.rotation.y;
		addedAngle [0] = 0f;
		addedAngle [1] = 90f;
		addedAngle [2] = -90f;
		addedAngle [3] = 180f;
	}

	void Update () 
	{
		Debug.DrawRay (transform.position, transform.forward * 5f, Color.green);

		if (!paused) {
			RaycastHit hit;
			if (Physics.Raycast (checkFront.position, -transform.up, out hit, 1.5f) && Vector3.Distance (transform.position, endPos) < moveSpeed * Time.deltaTime) {
				endPosArray [0] = new Vector3 (Mathf.Round (hit.point.x), transform.position.y, Mathf.Round (hit.point.z));
			} else {
				endPosArray [0] = imposVec;
			}
			if (Physics.Raycast (checkRight.position, -transform.up, out hit, 1.5f) && Vector3.Distance (transform.position, endPos) < moveSpeed * Time.deltaTime) {
				endPosArray [1] = new Vector3 (Mathf.Round (hit.point.x), transform.position.y, Mathf.Round (hit.point.z));
			} else {
				endPosArray [1] = imposVec;
			}
			if (Physics.Raycast (checkLeft.position, -transform.up, out hit, 1.5f) && Vector3.Distance (transform.position, endPos) < moveSpeed * Time.deltaTime) {
				endPosArray [2] = new Vector3 (Mathf.Round (hit.point.x), transform.position.y, Mathf.Round (hit.point.z));
			} else {
				endPosArray [2] = imposVec;
			}
			if (Physics.Raycast (checkBack.position, -transform.up, out hit, 1.5f) && !Physics.Raycast (checkFront.position, -transform.up, 1.5f) && !Physics.Raycast (checkRight.position, -transform.up, 1.5f) && !Physics.Raycast (checkLeft.position, -transform.up, 1.5f) && Vector3.Distance (transform.position, endPos) < moveSpeed * Time.deltaTime) {
				endPos = new Vector3 (Mathf.Round (hit.point.x), transform.position.y, Mathf.Round (hit.point.z));
				currentAngle += addedAngle [3];
				transform.rotation = Quaternion.Euler (new Vector3 (0f, currentAngle, 0f));
			}

			transform.position = Vector3.MoveTowards (transform.position, endPos, Time.deltaTime * moveSpeed);

			for (int i = 0; i < endPosArray.Length; i++) {
				if (endPosArray [i] != imposVec)
					counter++;
			}

			if (counter == 0) {

			} else if (counter == 1) {
				SimpleMove ();
				counter = 0;
			} else {
				RandomMove ();
				counter = 0;
			}
		}
	}


	void SimpleMove () 
	{
		for (int i = 0; i < endPosArray.Length; i++) {
			if (endPosArray[i] != imposVec) {
				endPos = endPosArray[i];
				if (i != 0) {
					currentAngle += addedAngle[i];
					transform.rotation = Quaternion.Euler(new Vector3(0f,currentAngle,0f));
				}
			}
		}
	}

	void RandomMove () 
	{
		while (endPos == transform.position || endPos == imposVec) {
			index = Random.Range (0, 3);
			endPos = endPosArray [index];
		}
		currentAngle += addedAngle [index];
		transform.rotation = Quaternion.Euler(new Vector3(0f, currentAngle, 0f));
	}

	public void PauseMove() {
		paused = true;
		transform.position = new Vector3 (Mathf.Round (transform.position.x), transform.position.y, Mathf.Round (transform.position.z));
		endPos = new Vector3 (Mathf.Round (transform.position.x), transform.position.y, Mathf.Round (transform.position.z));
	}

	public void ContinueMove() {
		paused = false;
		transform.position = new Vector3 (Mathf.Round (transform.position.x), transform.position.y, Mathf.Round (transform.position.z));
		endPos = new Vector3 (Mathf.Round (transform.position.x), transform.position.y, Mathf.Round (transform.position.z));
	}
}
