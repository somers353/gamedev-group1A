using UnityEngine;
using System.Collections;

public class SpiderAI : MonoBehaviour {
	GameObject player;
	public SphereCollider terrirory;
	bool playerInTerritory, hasPlayerItems;
	public Transform target;
	public float speed = 3f, attackDist = 1f;
	public Animation walk;


	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		playerInTerritory = false;
		hasPlayerItems = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(playerInTerritory)
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

		if(Vector3.Distance(transform.position, target.position) > attackDist)
		{
			transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
		}
	}

	void RunFromPlayer()
	{

	}
}
