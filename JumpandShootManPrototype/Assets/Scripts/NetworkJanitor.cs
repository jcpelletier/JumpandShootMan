using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.Characters.FirstPerson;

public class NetworkJanitor : NetworkBehaviour //This class disables stuff based on network identity
{
    private GameObject player;
    public GameObject cam;
    private AudioListener listener;
    public GameObject hitsound;
    public GameObject fpsHud;

	// Use this for initialization
	void Start () {
        player = gameObject;
        //cam = transform.Find("MainCamera").gameObject;
        
        if (!isLocalPlayer)//if you aren't the local player, then your spawned players shouldn't have the below stuff
        {
            
            player.GetComponent<RigidbodyFirstPersonController>().enabled = !player.GetComponent<RigidbodyFirstPersonController>(); //disable movement class if not local player
            cam.GetComponent<Camera>().enabled = false;
            //cam.GetComponent<AudioListener>().enabled = !cam.GetComponent<AudioListener>().enabled;
            fpsHud.GetComponent<Canvas>().enabled = !fpsHud.GetComponent<Canvas>().enabled;
            //cam.SetActive(false);
            hitsound.SetActive(false);
            player.GetComponent<AbilitiesShoot>().enabled = !player.GetComponent<AbilitiesShoot>().enabled;
            //player.GetComponent<PlayerStats>().enabled = !player.GetComponent<PlayerStats>().enabled;
            player.GetComponent<AbilitiesMovement>().enabled = !player.GetComponent<AbilitiesMovement>().enabled;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
