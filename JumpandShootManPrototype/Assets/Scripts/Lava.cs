using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Lava : NetworkBehaviour
{
    public GameObject explosionPrefab;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Lava hit player");
            collision.gameObject.GetComponent<PlayerStats>().CmdDecrementHealth();
            Destroy(gameObject);
        }

    }


}
