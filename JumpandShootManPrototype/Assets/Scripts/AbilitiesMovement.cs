using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using UnityEngine.UI;

public class AbilitiesMovement : MonoBehaviour {
    //Input
    private Player player; // The Rewired Player 
    public int playerId = 0; // The Rewired player id of this character

    //Display
    public Slider energy;

    //get playerstats for charging fuel
    public PlayerStats playerStats;

    //get player rigidybody for physics
    private Rigidbody bod;

    //New Energy system
    private int jetpackMax = 100;
    private int jetpack;
    private float jetpackEnergy;


    // Use this for initialization
    void Awake ()
    {
        playerStats = gameObject.GetComponent<PlayerStats>();
        player = ReInput.players.GetPlayer(playerId);
        bod = GetComponent<Rigidbody>();
        jetpackEnergy = jetpackMax;
        energy.maxValue = jetpackMax;
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        energy.value = jetpackEnergy;
        //jetpack ability
        if (player.GetButton("FireSecondary") && playerStats.canjetpack && jetpackEnergy > 0)
        {
            playerStats.isAirControl = true;
            bod.AddForce(transform.up * 200);
            bod.AddForce(transform.forward * 5);
            jetpackEnergy = jetpackEnergy - 2;
            //playerStats.DecrementEnergy(2);
            //turn on some sort of effects bool on playerstats here for performance
        }
        else if (jetpackEnergy < jetpackMax){
            ++jetpackEnergy;
            playerStats.isAirControl = false;
        }
    }
}
