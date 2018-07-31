using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LiveScoreboard : MonoBehaviour {

    Text lives;

	// Use this for initialization
	void Start () {
        GameObject LiveScoreboard = GameObject.Find("Live Scoreboard");
        if(LiveScoreboard != null)
        {
            lives = LiveScoreboard.GetComponent<Text>();
        }
        if(lives == null)
        {
            Debug.Log("Textelement of LiveScoreboard not found or LiveScoreboard not found.");
        }
	}
	
    void setDisplayLives(ArrayList Players)
    {
        string Text = "Lives:\n";
        foreach (Player currentPlayer in Players)
        {
             
            string Line = "";

            //TO-DO: Check if the Player uses Escapecodes/ format stuff or other crazy things.
            Line += currentPlayer.getName() + ": " + currentPlayer.GetComponentInChildren<status>().getHealth();

            Text += "<color=" + constants.getColorByTeamId(currentPlayer.getTeamId()) + ">" + Line + "</color>\n";
        }

        lives.supportRichText = true;
        lives.text = Text;
    }

	// Update is called once per frame
	void Update () {
		
	}
}
