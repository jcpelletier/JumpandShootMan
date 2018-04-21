using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour {
    public Transform explosionPrefab; //this is the thing you instantiate when the bullet dies

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Enemy Bullet Collision with " + collision.transform.name);
        Debug.Log("Bullet hit");
        ContactPoint contact = collision.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 pos = contact.point;
        Instantiate(explosionPrefab, pos, rot);
        Debug.Log("Check before tag check");
        if (collision.gameObject.tag =="Player")
        {
            Debug.Log("Bullet hit player");
            collision.gameObject.GetComponent<PlayerStats>().DecrementHealth();
        }

        Destroy(gameObject);
        Debug.Log("EnemyBullet Collided with not player");
    }

   
}
