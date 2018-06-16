using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI;

public class NetworkLobby : MonoBehaviour
{
    public GameObject NetworkManagerObject;
    public Text ipFieldText;
    // Use this for initialization
    void Start()
    {


    }

    // Update is called once per frame d
    void Update()
    {
        /*
		if (Input.GetButton ("NetworkAsClient")) // start as network host and client
		{
			Debug.Log ("Start as Client");
			NetworkManager net = NetworkManagerObject.GetComponent<NetworkManager> ();
			net.networkAddress = "192.168.1.193";
			net.StartClient();
		}
		*/


    }

    public void StartHost()
    {
        Debug.Log("Start as Host");
        NetworkManager net = NetworkManagerObject.GetComponent<NetworkManager>();
        net.StartHost();
    }

    public void StartClient()
    {
        Debug.Log("Start as Client");
        NetworkManager net = NetworkManagerObject.GetComponent<NetworkManager>();
        net.networkAddress = ipFieldText.text;
        net.StartClient();
    }
}
