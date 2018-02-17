﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {
    private int health;
    private Vector3 startPosition;
    public GameObject player;

    private int playerStrength;
    private int damage;

    public Slider healthSlider;
    public Slider healthSlider2;

    public GameObject bulletPrefab;
    public Transform bulletSpawn;


    // Use this for initialization
    void Start () {
        startPosition = gameObject.transform.position;
        health = 4;
        player = GameObject.Find("Player");
        playerStrength = player.GetComponent<Player>().strength;
        StartCoroutine("shootOccasionally");
    }

    void OnEnable()
    {
        health = 4;
    }

    // Update is called once per frame
    void Update () {
        healthSlider.value = health;
        healthSlider2.value = health;
        damage = playerStrength;
        Debug.Log("Damage: " + damage);
    }

    public void TakeDamage ()
    {
        gameObject.GetComponent<AudioSource>().Play();
        health--;
        if (health <= 0)
        {
            gameObject.transform.position = startPosition;
            gameObject.SetActive(false);
        }
    }

    public void shoot()
    {
        var bullet = (GameObject)Instantiate(
            bulletPrefab,
            bulletSpawn.position,
            bulletSpawn.rotation);

        // Add velocity to the bullet
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 6;
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "playershot")
        {
            TakeDamage();
        }
    }

    IEnumerator shootOccasionally()
    {
        int wait = 2 + Random.Range(0, 2);   
        yield return new WaitForSeconds(wait);
        shoot();
        StartCoroutine("shootOccasionally");
        yield return null;

    }
}
