using UnityEngine;
using System.Collections;

//You must add the following Components to the object or else this script will fail.
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Animator))]

//Provides a set of default values to instance and build on top of.
public class Movement{
	public float walkSpeed = 2;
	public float runSpeed = 5;
	public float turnSpeed = 1;
	public float strafeSpeed = 3.5f;
}

public class BaseController : MonoBehaviour {
	protected Vector3 _moveDir = Vector3.zero;
	public Vector3 moveDir{
		get{
			return _moveDir;
		}
	}
	
	protected Movement movement;
	
	public float speed{
		get{
			return this.movement.walkSpeed;
		}
	}
	public float rotationSpeed{
		get{
			return this.movement.turnSpeed;
		}
	}
	
	//It was giving me warnings without the "new"... Unity5 maybe? Never seen that before.
	protected new Camera camera;
	protected new Rigidbody rigidbody;
	
	// Use this for initialization
	protected virtual void Start () {
		movement = new Movement(); //Create a new Movement object.
		Initialize ();
	}
	
	protected virtual void Update(){} //We're not doing anything here in the Base class.
	
	protected virtual void Initialize (){
		this.camera = Camera.main; //We might need this later.
	}
	
	protected virtual float GetSpeed(){
		return this.movement.walkSpeed; //We'll expand on this later to allow for movement modifiers.
	}
}
