using UnityEngine;
using System.Collections;

public class TreeController : MonoBehaviour {

	public float timer;

//	public Rigidbody rb;

	public bool growing;
//	public Vector3 spawnPos;
	
	public float growSpeed = 0.6f;
//	public float rotSpeed = 5f;
	public float interval = 5f;
	float scale = 0f; 
	float percGrow = 0f;
	float percRot = 0f;

	public bool activateLift;
	public Transform lift;
	public float liftSpeed = 0.6f;
	float liftDest;

	public GameObject treeMesh;
	float treeHeight;
	


	void Start ()
	{
		treeMesh.transform.localScale = new Vector3 (1, 0, 1);

		treeHeight = GetComponent<Collider> ().bounds.size.y;
		liftDest = treeHeight - 0.5f;

		timer = Time.time;
		scale = 0f;
		growing = true;
	}

	void Update ()
	{
		treeMesh.transform.localScale = new Vector3 (1, scale, 1);

		if (growing) 
		{
			Grow ();
		} 
		else 
		{
			Rot ();
		}

		//	Rot after a certain amount of time
		if (Time.time > timer + interval)
			growing = false;
	}

	void Grow () 
	{
		//	Move lift up
		lift.Translate (Vector3.up * Time.deltaTime * liftSpeed);
		if (lift.localPosition.y > liftDest)
			lift.localPosition = new Vector3 (0, liftDest, 0);

		//	Animation
		percGrow += Time.deltaTime * growSpeed;
		scale = Mathf.Lerp (0f, 1f, percGrow);
	}

	void Rot () 
	{
		//	Move lift down
		lift.Translate (-Vector3.up * Time.deltaTime * liftSpeed);

		//	Animation
		percRot += Time.deltaTime * growSpeed;
		scale = Mathf.Lerp (1f, 0f, percRot);

		//	Destroy tree object when fully rot
		if (percRot > 1f)
			Destroy (gameObject);
	}

	void OnTriggerStay (Collider other) 
	{
		if (other.tag == "Player") 
		{
			print ("on lift");
			other.gameObject.transform.parent = lift;
		}

		if (other != null) {
			print ("blah");
		}
	}

	void OnCollisionStay (Collision other) 
	{
//		if (other != null) {
//			print ("blah");
//		}

		if (other.transform.tag == "Player") 
		{
			print ("on lift");
			other.gameObject.transform.parent = lift;
		}
	}







//	void Start () {
//
//		grow = true;
//
//		rb = GetComponent<Rigidbody> ();
//	}
//
//	void Update () {
//	
//	}
//
//	void FixedUpdate () {
//
//		if (grow) 
//		{
//			rb = GetComponent<Rigidbody>();
//			
//			//	Grow tree if it's below the grow position
//			if (rb.transform.position.y < spawnPos.y) {
//				Grow ();
//			}
//			//	Stabilize tree once it reaches grow position
//			else if (rb.transform.position.y >= spawnPos.y) {
//				rb.MovePosition(spawnPos);
//				grow = false;
//				timer = Time.time;
//			} 
//		}
//		else 	//!grow (rotting)
//		{
//			if (Time.time > timer + interval) {
//				Rot ();
//			}
//		}
//	}
//
//	void Grow () {
//		rb.MovePosition(rb.transform.position + transform.up * Time.deltaTime * growSpeed);
//	}
//
//	void Rot () {
//		rb.MovePosition (rb.transform.position - transform.up * Time.deltaTime * rotSpeed);
//	}
//
//	void OnCollisionExit (Collision other) {
//		if (other.collider.tag == "Dirt") {
//			print ("tree should die!");
//			Destroy (gameObject);
//		}
//	}
}
