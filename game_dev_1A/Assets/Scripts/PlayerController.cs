using UnityEngine;
using System.Collections;
public class PlayerController : BaseController {
	// Use this for initialization
	protected Animator animator;
	public bool hasItems;
	public int numItems;
	public int playerHealth;

	protected override void Start () {
		base.Start (); // Call BaseController.Start, which will call Initialize. 
		animator = GetComponent<Animator> ();
		playerHealth = 100;
	}
	
	// Update is called once per frame
	protected new virtual void Update () { //We do a new virtual here so that we can call the base, but anything else that calls "base.Update" will refer to this.
		GetInput (); // Get the player's input BEFORE we Update.
		base.Update (); // Call BaseController.Update(), even though it's empty.
	}
	
	protected override void Initialize(){
		base.Initialize (); // Call the Base of Initialize.
	}
	
	void GetInput(){
		float h = Input.GetAxis ("Horizontal"); 
		float v = Input.GetAxis ("Vertical");
		
		Vector3 moveInput = new Vector3 (h, 0, v);
		
		_moveDir = this.camera.transform.TransformDirection (moveInput); // Modify _moveDirection to be relative to the camera.
		
		Debug.DrawRay (transform.position, _moveDir * 5, Color.red); //Display which way we're trying to go for Debug purposes in the Scene view.
	}
}
