using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour {
    public Transform explosionPrefab;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision collision)
    {
        ContactPoint contact = collision.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 pos = contact.point;
        Instantiate(explosionPrefab, pos, rot);

        if (collision.gameObject.name == "Player")
        {
            collision.gameObject.GetComponent<PlayerStats>().DecrementHealth();
        }

        gameObject.SetActive(false);
        Debug.Log("EnemyBullet Collision");
    }
}
