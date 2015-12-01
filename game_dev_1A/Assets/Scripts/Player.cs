using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public float delayBetweenDeaths = 0.5f;
	/** Reference to the amount of engine parts the player must collect on level 3*/
	private int partsLeft = 5;
	/** Reference to the amount of lives the player has on level 3*/
	private int livesLeft = 3;

	/** Reference to the audio clip file*/
	public AudioClip Die;

	/** Reference to the message that will appear*/
	public string died = "(message to appear)";
	
	/** \return Returns the int value of the remaining Engine Parts on level 3*/
	public int GetPartsLeft()
	{
		return partsLeft;
	}
	
	/** \return Returns the lives remaining for the player on level 3*/
	public int GetLivesLeft()
	{
		return livesLeft;
	}
	
	/** 
		Method that manages the items collided with by the referenced player
	*/
	private void OnTriggerEnter(Collider c)
	{
		string tag = c.tag;

		if ("Water" == tag)
		{
			loseLife();
		}

		else if ("Badguy" == tag)
		{
			loseLife();
		}
	}

	/** Method that manages the re spawning of the player after he dies*/
	private void MoveToStartPosition()
	{
		GameObject respawnGO = ChooseRandomObjectWithTag ("Spawn");
		Vector3 startPosition = respawnGO.transform.position;
		transform.position = startPosition;
	}
	
	/** 
		Method that finds the the game object with the string passed finds the 
		length of the array of objects with the same tag
		randomizes the selection and returns one of the game objects
	
	\param String with a tag word
	\return tagged object at a random index
	*/
	private GameObject ChooseRandomObjectWithTag (string tag)
	{
		GameObject [] taggedObjects = GameObject.FindGameObjectsWithTag (tag);
		int numTaggedObjects = taggedObjects.Length;
		int randomIndex = Random.Range (0, numTaggedObjects);
		return taggedObjects [randomIndex];
	}
	
	/** 
		Method that manages the lives of the player
	<pre>
	IF
		livesLeft = 0
		{
			Game Over
		}
	</pre>
	*/
	private void loseLife()
	{
		MoveToStartPosition ();
	}

}
