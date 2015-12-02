using UnityEngine;
using System.Collections;

public class FinishLevel : MonoBehaviour {

	public string levelName;

	void OnTriggerEnter(Collider c){
		if (c.gameObject.tag == "Player") {
			int numItems = c.GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonCharacter>().numItems;
			int collectedItems = PlayerPrefs.GetInt("Aquaria");
			if(collectedItems<numItems)
			{
				PlayerPrefs.SetInt(levelName, numItems);
			}
			Application.LoadLevel("Scene0_menu");
		}
	}
}
