using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Enemy : NetworkBehaviour
{
    [SyncVar]
    private int health;

    private Vector3 startPosition;
    public GameObject player;

    private int playerBravery;
    private int damage;

    public Slider healthSlider;
    public Slider healthSlider2;

    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public Transform enemyDeathPrefab;

    // Use this for initialization
    void Start ()
    {
        startPosition = gameObject.transform.position;
        health = 4;
        player = GameObject.Find("Player");
        playerBravery = player.GetComponent<PlayerStats>().bravery;
        //StartCoroutine("shootOccasionally");
    }

    void OnEnable()
    {
        health = 4;
    }

    // Update is called once per frame
    void Update () {
        healthSlider.value = health;
        healthSlider2.value = health;
        damage = playerBravery;
        //
        //"Damage: " + damage);
    }

    public void TakeDamage ()
    {
        gameObject.GetComponent<AudioSource>().Play();
        health--;
        if (health <= 0)
        {
            Instantiate(enemyDeathPrefab, gameObject.transform.position, gameObject.transform.rotation);
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
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 20;
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
        int wait = 1 + Random.Range(0, 2);   
        yield return new WaitForSeconds(wait);
        shoot();
        StartCoroutine("shootOccasionally");
        yield return null;

    }
}
