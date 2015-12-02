using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour {

	public float healthPositionX, healthPositionY;
	public Texture full, half, threeQuater, oneQuater, currentHealth, noItems, oneItem, twoItems, threeItems, fourItems, currentItems;
	public GameObject player;
	public UnityStandardAssets.Characters.ThirdPerson.ThirdPersonCharacter controller;

	// Use this for initialization
	void Start () {
		healthPositionX = 15;
		healthPositionY = 15;
		player = GameObject.FindGameObjectWithTag ("Player");
		controller = player.GetComponent <UnityStandardAssets.Characters.ThirdPerson.ThirdPersonCharacter>();
		currentHealth = full;
		currentItems = noItems;
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnGUI(){
		GUI.Label (new Rect (healthPositionX,healthPositionY,Screen.width/3,Screen.height/10), currentHealth);
		GUI.Label (new Rect ((Screen.width - Screen.width/5), healthPositionY, Screen.width/3,Screen.height/10), currentItems);
	}

	public void changeHealth(int health)
	{
		if (health <= 25) {
			currentHealth = oneQuater;
		} else if (health <= 50) {
			currentHealth = half;
		} else if (health <= 75) {
			currentHealth = threeQuater;
		} else {
			currentHealth = full;
		}
	}

	public void itemNumberChange(int items)
	{
		switch (items) {
		case 0:
			currentItems = noItems;
			break;
		case 1:
			currentItems = oneItem;
			break;
		case 2:
			currentItems = twoItems;
			break;
		case 3:
			currentItems = threeItems;
			break;
		case 4:
			currentItems = fourItems;
			break;
		default:
			currentItems = noItems;
			break;
		}
	}
}
