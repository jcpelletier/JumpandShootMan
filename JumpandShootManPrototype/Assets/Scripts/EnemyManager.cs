using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {
    public GameObject enemyOne;
    public GameObject enemyTwo;


	// Use this for initialization
	void Start ()
    {
        StartCoroutine("checkLiving");

    }
	

    IEnumerator checkLiving()
    {
        Debug.Log("checking the living");
        yield return new WaitForSeconds(45.0F);
    
        if (!enemyOne.activeSelf)
        {
            enemyOne.SetActive(true);
            Debug.Log("Bring enemy back to the living");
        }
        if (!enemyTwo.activeSelf)
        {
            enemyTwo.SetActive(true);
            Debug.Log("Bring enemy back to the living");
        }
        StartCoroutine("checkLiving");
        yield return null;

    }
}
