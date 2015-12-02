using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour {

	public float foreward = 2f, back = -2f, startPoint = 10.6f, endPoint = 40.6f, direction;
	public Rigidbody rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		direction = foreward;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (rb.position.z < startPoint) {
			direction = foreward;
		} 
		else if(rb.position.z > endPoint){
			direction = back;
		}

		rb.MovePosition (rb.position + new Vector3 (0, 0, direction) * Time.deltaTime);
	}
}
