using UnityEngine;
using System.Collections;

public class LocomotionBehavior : StateMachineBehaviour {
	protected GameObject gameObject;
	protected Transform transform;
	protected Rigidbody rigidbody;
	protected Animator _animator;
	protected BaseController _controller;
	
	protected Vector3 _moveDir;
	public void Start(){}
	
	// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state.
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		_animator = animator;
		gameObject = _animator.gameObject;
		transform = gameObject.transform;
		rigidbody = gameObject.GetComponent<Rigidbody> ();
		_controller = gameObject.GetComponent<BaseController> ();
	}
	
	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks.
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		HandleMovement ();
		HandleAnimation ();
	}
	
	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state.
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		
	}
	
	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here.
	override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		
	}
	
	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		
	}
	
	private void HandleMovement () {
		_moveDir = _controller.moveDir; // Get our current movement direction from _controller.
		_moveDir.y = 0; // Set Y to zero because we're not moving up or down, the rigidbody physics takes care of that for us.
		transform.LookAt (transform.position + _moveDir); // Look in the direction we're trying to go.
	}
	
	private void HandleAnimation () { 
		_animator.SetFloat ("h", 0); // We can set this to our Input X-Axis if we have good run left/right animations, otherwise leave it at 0.
		_animator.SetFloat ("v", _moveDir.sqrMagnitude); // Set our vertical speed so that the character knows to run forward.
	}
}
