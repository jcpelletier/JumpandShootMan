using UnityEngine;
using UnityEngine.Networking;

public class NetworkSpawner : MonoBehaviour
{
    public GameObject turretGuy;
    public bool spawned;


    public void Spawn()
    {
        GameObject turret = (GameObject)Instantiate(turretGuy, transform.position, transform.rotation);
        NetworkServer.Spawn(turret);
    }

    // Use this for initialization
    void Start () {
        spawned = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (spawned && Network.isServer)
        {
            Debug.Log("Network Spawn");   
            spawned = false;
        }
	}
}
