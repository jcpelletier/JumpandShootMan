using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GetIPForText : NetworkBehaviour {
    private string serverAddress;
    private Text serverAddressText;

	// Use this for initialization
	void Start () {
        serverAddressText = gameObject.GetComponent<Text>();
        Debug.Log("Server IP:" + NetworkManager.singleton.networkAddress);
        //serverAddress = NetworkManager.singleton.networkAddress;
        serverAddressText.text = NetworkManager.singleton.networkAddress;

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
