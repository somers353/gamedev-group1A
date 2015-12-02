using UnityEngine;
using System.Collections;
using System.Linq;
using Assets.Unitility.SQLite;

public class ExampleScript : MonoBehaviour {

    ExampleDbAccessor db = new ExampleDbAccessor();

    // Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyDown(KeyCode.Space))
	    {
	        db.AddScore("Player 1", Random.Range(0,1), Random.Range(100, 10000));

	        Debug.Log("Top Score for specific level:");
            Debug.Log(db.GetHighscoreForLevel("Player 1", 0));
	        Debug.Log("All Level Highscores:");
	        Debug.Log(string.Join(",",db.GetHighscores("Player 1").Select(score => score.ToString()).ToArray()));
	    }
	}
}