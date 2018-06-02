using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EnemyBullet : NetworkBehaviour {
    public GameObject explosionPrefab;

    void OnCollisionEnter(Collision collision)
    {
        if (isServer)
        {
            ContactPoint contact = collision.contacts[0];
            Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
            Vector3 pos = contact.point;
            //Instantiate(explosionPrefab, pos, rot);
            //Debug.Log("Check before tag check");
            if (collision.gameObject.tag == "Player")
            {
                Debug.Log("Bullet hit player");
                collision.gameObject.GetComponent<PlayerStats>().CmdDecrementHealth();
                var bullet = (GameObject)Instantiate(
                explosionPrefab,
                gameObject.transform.position,
                gameObject.transform.rotation);
                NetworkServer.Spawn(bullet);
                Destroy(gameObject);
            }

            if (collision.gameObject.tag == "Environment")
            {
                Debug.Log("Bullet hit environment");
                var bullet = (GameObject)Instantiate(
                explosionPrefab,
                gameObject.transform.position,
                gameObject.transform.rotation);
                NetworkServer.Spawn(bullet);
                Destroy(gameObject);
            }

            Debug.Log("EnemyBullet Collided with not player or environment");

        }
        
   
    }

   
}
