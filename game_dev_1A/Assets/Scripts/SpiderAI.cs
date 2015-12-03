using UnityEngine;
using System.Collections;

public class SpiderAI : MonoBehaviour {
	private GameObject player;
	private UnityStandardAssets.Characters.ThirdPerson.ThirdPersonCharacter controller;
	private bool playerInTerritory, hasPlayerItems, paused;
	private int numItems, waypointCounter = 0;
	private NavMeshAgent agent;
	public GameObject logicGate;
	public SphereCollider terrirory;
	public Transform target;
	public float speed = 3f, attackDist = 1f, knockBackRadius = 8f, knockBackPower = 300f, minDistance = 10f;
	public Transform destination;
	public Transform[] waypoints;



	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		controller = player.GetComponent <UnityStandardAssets.Characters.ThirdPerson.ThirdPersonCharacter>();
		playerInTerritory = false;
		hasPlayerItems = false;
		paused = false;
		agent = GetComponent<NavMeshAgent> ();
		agent.destination = waypoints [waypointCounter].position;
	}
	
	// Update is called once per frame
	void Update () {
		if(playerInTerritory && !hasPlayerItems)
		{
			MoveToPlayer();
		}else {
			if(Vector3.Distance(transform.position, agent.destination) < 1){
				waypointCounter++;
				if(waypointCounter > 2){
					waypointCounter = 0;
				}
			}
			agent.destination = waypoints[waypointCounter].position;
		}
	}

	void OnTriggerEnter(Collider c)
	{
		if(c.gameObject == player)
		{
			playerInTerritory = true;

			//If the player makes contact while the spider is running, kill spider and spawn stolen items
			if(hasPlayerItems)
			{
				float dist = 0.5f;
				for(int i = 1; i <= numItems; i++)
				{
					Instantiate(logicGate, new Vector3(transform.position.x + dist, transform.position.y + dist, transform.position.z + dist), Quaternion.identity);
					dist += 1f;
				}

				Destroy(this.gameObject);
			}
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

		if (!paused) {
			if (Vector3.Distance (transform.position, target.position) > attackDist) {
				agent.destination = player.transform.position;
			} else {
				Attack ();
			}
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
		//If the player is holding items, attack the player, steal the items and run away
		if (controller.hasItems) 
		{
			hasPlayerItems = true;
			numItems = controller.numItems;
			controller.hasItems = false;
			controller.numItems = 0;
			controller.changeItems();
			controller.playerHealth -= 25;
			controller.loseHealth();
			speed = 2f;
			knockBack ();
			decreaseSphereRadius();
			RunFromPlayer ();
		} 
		else 
		{
			controller.playerHealth -= 25;
			controller.loseHealth();
			knockBack();
			paused = true;
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
			if(hit.tag == "Player"){
				Rigidbody rb = hit.GetComponent<Rigidbody>();
				if(rb != null){
					rb.AddExplosionForce(knockBackPower, knockBackOrigin, knockBackRadius, 0.8f);
				}
			}
		}
	}

	//Pause for 2 seconds after attack to allow player to recover
	IEnumerator AttackPause()
	{
		yield return new WaitForSeconds(3);
		paused = false;
	}

	IEnumerator decreaseSphereRadius()
	{
		yield return new WaitForSeconds (2);
		terrirory.radius = 0.5f;
	}
}
