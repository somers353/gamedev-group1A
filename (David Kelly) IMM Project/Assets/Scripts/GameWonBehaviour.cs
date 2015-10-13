using UnityEngine;
using System.Collections;

public class GameWonBehaviour : MonoBehaviour 
{

	public void PlaySound()
	{
		GetComponent<AudioSource>().Play();
		soundStarted = true;
	}
	
	private bool soundStarted = false;
	
	private void Update()
	{
		if(soundStarted && !GetComponent<AudioSource>().isPlaying)
		{
			// put in here whatever action you want to do when the sound finishes playing
			// e.g.
			Application.LoadLevel( 0 );
		}
	}
}
