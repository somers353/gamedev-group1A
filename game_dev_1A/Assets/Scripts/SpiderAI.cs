using UnityEngine;
using System.Collections;

public class SpiderAI : MonoBehaviour {
	GameObject player;
	PlayerController controller;
	public SphereCollider terrirory;
	bool playerInTerritory, hasPlayerItems;
	public Transform target;
	public float speed = 3f, attackDist = 1f, knockBackRadius = 5f, knockBackPower = 30f, minDistance = 10f;
	public Animation walk;
	int numItems;


	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		controller = player.GetComponent <PlayerController>();
		playerInTerritory = false;
		hasPlayerItems = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(playerInTerritory && !hasPlayerItems)
		{
			MoveToPlayer();
		}

		if(hasPlayerItems)
		{
			RunFromPlayer();
		}
	}

	void OnTriggerEnter(Collider c)
	{
		if(c.gameObject == player)
		{
			playerInTerritory = true;
		}
	}

	void OnTriggerExit(Collider c)
	{
		if(c.gameObject == player)
		{
			playerInTerritory = false;
		}
	}

	void MoveToPlayer()
	{
		transform.LookAt (target.position);
		transform.Rotate (new Vector3(0,-90,0), Space.Self);

		if (Vector3.Distance (transform.position, target.position) > attackDist) {
			transform.Translate (new Vector3 (speed * Time.deltaTime, 0, 0));
		} 
		else 
		{
			Attack();
		}
	}

	void RunFromPlayer()
	{
		Vector3 direction = transform.position - target.position;
		direction.Normalize ();
		transform.position = Vector3.MoveTowards (transform.position, direction*minDistance, Time.deltaTime*speed);
	}

	void Attack()
	{
		if (controller.hasItems) 
		{
			hasPlayerItems = true;
			numItems = controller.numItems;
			controller.hasItems = false;
			controller.numItems = 0;
			speed = 2f;
			controller.playerHealth -= 25;
			knockBack ();
			RunFromPlayer ();
		} 
		else 
		{
			controller.playerHealth -= 25;
			knockBack();
			StartCoroutine (AttackPause ());
		}
	}

	//Use ExplosionForce to knock the player back after an attack
	void knockBack()
	{
		Vector3 knockBackOrigin = transform.position;
		Collider[] colliders = Physics.OverlapSphere (knockBackOrigin, knockBackRadius);
		foreach (Collider hit in colliders) 
		{
			Rigidbody rb = hit.GetComponent<Rigidbody>();
			if(rb != null)
			{
				rb.AddExplosionForce(knockBackPower, knockBackOrigin, knockBackRadius, 3f);
			}
		}
	}

	//Pause for 2 seconds after attack to allow player to recover
	IEnumerator AttackPause()
	{
		yield return new WaitForSeconds(2.0f);
	}
}
