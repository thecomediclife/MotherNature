using UnityEngine;
using System.Collections;

public class TreeController2 : MonoBehaviour {
	public Transform player;
	public Transform platform;
	private Transform trunk;

	public bool growing = true;
	private bool moving;

	public Vector3 platformFinalPosition = new Vector3(0f,2f,0f);
	private Vector3 platformOriginalPosition;

	public float moveSpeed = 1.0f;
	private float currentPercent = 0f;

	// Use this for initialization
	void Start () {
		platform = transform.Find ("Platform");
		trunk = transform.Find ("Trunk");
		platformOriginalPosition = platform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
		if (growing) {
			if (Input.GetButtonDown ("Fire1")) {
				if (player != null) {
					player.GetComponent<CharControllerTest> ().PauseMove ();

					if (Vector3.Distance (player.transform.position, transform.position + new Vector3 (0, 1.5f, 0)) < 0.01) {
						player.transform.parent = platform;
					} else {
						player.GetComponent<CharControllerTest> ().ContinueMove ();
					}
				}
				moving = true;
				platform.GetComponent<MeshRenderer>().enabled = true;
				trunk.GetComponent<MeshRenderer>().enabled = true;
			}

			if (moving) {
				currentPercent += moveSpeed * Time.deltaTime;
				platform.localPosition = Vector3.Lerp(platformOriginalPosition, platformFinalPosition, currentPercent);
				trunk.localPosition = Vector3.Lerp (platformOriginalPosition, platformFinalPosition, currentPercent / 2f);
				if (Vector3.Distance(platform.localPosition, platformFinalPosition) < 0.01) {
					moving = false;
					growing = false;
					if (player != null) {
						player.GetComponent<CharControllerTest>().ContinueMove();
						player.transform.parent = null;
					}
				}
			}
		} else if (!growing) {
			if (Input.GetButtonDown ("Fire1")) {
				if (player != null) {
					player.GetComponent<CharControllerTest> ().PauseMove ();
					
					if (Vector3.Distance (player.transform.position, transform.position + new Vector3 (0, 3.5f, 0)) < 0.01) {
						player.transform.parent = platform;
					} else {
						player.GetComponent<CharControllerTest> ().ContinueMove ();
					}
				}
				moving = true;
			}
			
			if (moving) {
				currentPercent -= moveSpeed * Time.deltaTime;
				platform.localPosition = Vector3.Lerp(platformOriginalPosition, platformFinalPosition, currentPercent);
				trunk.localPosition = Vector3.Lerp (platformOriginalPosition, platformFinalPosition, currentPercent / 2f);
				if (Vector3.Distance(platform.localPosition, platformOriginalPosition) < 0.01) {
					moving = false;
					growing = true;
					if (player != null) {
						player.GetComponent<CharControllerTest>().ContinueMove();
						player.transform.parent = null;
					}
					platform.GetComponent<MeshRenderer>().enabled = false;
					trunk.GetComponent<MeshRenderer>().enabled = false;
				}
			}
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Player")
			player = other.transform;
	}

	void OnTriggerExit(Collider other) {
		if (player != null)
			player = null;
	}
}
