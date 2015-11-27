using UnityEngine;
using System.Collections;

public class SandstormAI : MonoBehaviour {

	public float playerTargetDistance;
	public float enemyLookDistance;
	public float attackDistance;
	public float enemyMovementSpeed;
	public float damping;
	public Transform playerTarget;
	Rigidbody aiAlien;
	Renderer render;


	// Use this for initialization
	void Start () {
		render = GetComponent<Renderer> ();
		aiAlien = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		playerTargetDistance = Vector3.Distance (playerTarget.position, transform.position);
		if (playerTargetDistance < enemyLookDistance) {
			render.material.color = Color.white;
			lookAtPlayer ();
		}
		if (playerTargetDistance < attackDistance) {
			attack ();
		} 
		else {
			render.material.color = Color.red;
		}
	}

	void lookAtPlayer(){
		Quaternion rotation = Quaternion.LookRotation(playerTarget.position - transform.position);
		transform.rotation = Quaternion.Slerp (transform.rotation, rotation, Time.deltaTime*damping);
	}

	void attack(){
		aiAlien.AddForce (transform.forward * enemyMovementSpeed);
	}
}
