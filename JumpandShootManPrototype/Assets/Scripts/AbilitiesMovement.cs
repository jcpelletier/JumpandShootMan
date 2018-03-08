using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class AbilitiesMovement : MonoBehaviour {
    //Input
    private Player player; // The Rewired Player 
    public int playerId = 0; // The Rewired player id of this character

    //get playerstats for charging fuel
    public PlayerStats playerStats;

    //get player rigidybody for physics
    private Rigidbody bod;


    // Use this for initialization
    void Awake ()
    {
        playerStats = gameObject.GetComponent<PlayerStats>();
        player = ReInput.players.GetPlayer(playerId);
        bod = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        //jetpack ability
        if (player.GetButton("FireSecondary") && playerStats.canjetpack && playerStats.energy > 19)
        {
            playerStats.isAirControl = true;
            bod.AddForce(transform.up * 200);
            bod.AddForce(transform.forward * 5);
            playerStats.DecrementEnergy(2);
            //turn on some sort of effects bool on playerstats here for performance
        }
        else{
            playerStats.isAirControl = false;
        }
    }
}
