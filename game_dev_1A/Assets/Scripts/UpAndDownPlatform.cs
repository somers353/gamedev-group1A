using UnityEngine;
using System.Collections;

public class UpAndDownPlatform : MonoBehaviour {

	public float up = 2f, down = -2f, startPoint, endPoint, direction;
	public Rigidbody rb;
	
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		direction = up;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (rb.position.y < startPoint) {
			direction = up;
		} 
		else if(rb.position.y > endPoint){
			direction = down;
		}
		
		rb.MovePosition (rb.position + new Vector3 (0, direction, 0) * Time.deltaTime);
	}
}
