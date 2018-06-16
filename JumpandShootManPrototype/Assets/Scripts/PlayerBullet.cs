using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour {
    public Transform explosionPrefab;

    void Awake ()
    {
        //gameObject.GetComponent<Rigidbody>().AddForce(gameObject.transform.forward, ForceMode.Impulse);
    }

    void OnCollisionEnter(Collision collision)
    {
        ContactPoint contact = collision.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 pos = contact.point;
        Instantiate(explosionPrefab, pos, rot);

        gameObject.SetActive(false);
        Debug.Log("Bullet Collision with "+collision.transform.name);
    }
}
