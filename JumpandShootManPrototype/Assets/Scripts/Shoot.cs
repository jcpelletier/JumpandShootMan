using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {

    public GameObject shot;

    public GameObject shootPoint;

    Vector3 shot1Start;
    Vector3 shot2Start;
    Vector3 shot3Start;

    private Player player;

    int shotCounter = 1;

    // Use this for initialization
    void Start () {
        //Vector3 shot1Start = shot1.transform.position;
        //Vector3 shot2Start = shot2.transform.position;
        //Vector3 shot3Start = shot3.transform.position;
        player = gameObject.GetComponent<Player>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetMouseButtonDown(0) && player.manaSlider.value > 20)
        {
            /*
            if (shotCounter == 1 && !shot1.activeInHierarchy)
            {
                shot1.SetActive(false);
                shot1.SetActive(true);
                shot1.transform.position = shootPoint.transform.position;
                shot1.transform.rotation = shootPoint.transform.rotation;
                shot1.GetComponent<Rigidbody>().velocity = shot1.transform.forward * 20;
                shootPoint.GetComponent<AudioSource>().Play();
                player.DecrementMana();

            }
            else if (shotCounter == 2 && !shot2.activeInHierarchy)
            {
                shot2.SetActive(false);
                shot2.SetActive(true);
                shot2.transform.position = shootPoint.transform.position;
                shot2.transform.rotation = shootPoint.transform.rotation;
                shot2.GetComponent<Rigidbody>().velocity = shot2.transform.forward * 20;
                shootPoint.GetComponent<AudioSource>().Play();
                player.DecrementMana();
            }
            else if (shotCounter == 3 && !shot3.activeInHierarchy)
            {
                shot3.SetActive(false);
                shot3.SetActive(true);
                shot3.transform.position = shootPoint.transform.position;
                shot3.transform.rotation = shootPoint.transform.rotation;
                shot3.GetComponent<Rigidbody>().velocity = shot3.transform.forward * 20;
                shootPoint.GetComponent<AudioSource>().Play();
                player.DecrementMana();
            }
            else if (shotCounter >= 4)
            {
                shotCounter = 0;
            }


            shotCounter++;
            */
            shoot();
            
        }

    }

    public void shoot()
    {
        shootPoint.GetComponent<AudioSource>().Play();
        player.DecrementMana();
        var bullet = (GameObject)Instantiate(
            shot,
            shootPoint.transform.position,
            shootPoint.transform.rotation);

        // Add velocity to the bullet
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 20;
    }
}
