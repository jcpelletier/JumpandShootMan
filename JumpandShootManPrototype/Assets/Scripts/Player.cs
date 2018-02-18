using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    //RPG Stats
    public int strength;
    public int dexterity;
    public int constitution;
    public int wisdom;
    public int intelligence;
    public int charisma;

    //Ability Bools
    public bool canjetpack;
    public bool cansprint;
    public bool canshield;
   
    //UI
    public Text healthLabelText;
    public Text energyLabelText;
    public Text manaLabelText;
    public Text fuelArmText;
    
    public Slider healthSlider;
    public Slider manaSlider;
    public Slider energySlider;

    //Sound
    public AudioSource hitSound;

    private int health;
    private int healthMax;
    private float mana;
    private int manaMax;
    private float energy;
    private int energyMax;

    private Rigidbody bod;


	// Use this for initialization
	void Start ()
    {
        canjetpack = true;
        bod = GetComponent<Rigidbody>();
        health = 100;
        mana = 0;
        energy = 0;

        //roll stats
        strength = 10 + Random.Range(0, 8);
        dexterity = 10 + Random.Range(0, 8);
        constitution = 10 + Random.Range(0, 8);
        wisdom = 10 + Random.Range(0, 8);
        intelligence = 10 + Random.Range(0, 8);
        charisma = 10 + Random.Range(0, 8);

        //set properties based on stats rolled
        healthMax = constitution * 10;
        healthSlider.maxValue = healthMax;
        health = healthMax;
        manaMax = intelligence * 20;
        manaSlider.maxValue = manaMax;
        energyMax = dexterity * 20;
        energySlider.maxValue = energyMax;

        //Set max fuel Displays
        healthLabelText.text = healthMax.ToString();
        energyLabelText.text = energyMax.ToString();
        manaLabelText.text = manaMax.ToString();

    }

    // Update is called once per frame
    void Update()
    {
        healthSlider.value = health;
        manaSlider.value = mana;
        energySlider.value = energy;
        int manaInt = (int)mana;
        fuelArmText.text = manaInt.ToString(); ;

        if (Input.GetMouseButtonDown(1) && canjetpack && energy > 19)
        {
            bod.AddForce(transform.up * 25, ForceMode.Impulse);
            DecrementEnergy();
        }

        if (mana > manaMax)
        {
            mana = manaMax;
        }

        if (energy > energyMax)
        {
            energy = energyMax;
        }


    }

    void FixedUpdate()
    {
        PassiveRegen();
    }

    public void PassiveRegen()
    {
        energy = energy + 0.02f * constitution;
        mana = mana + 0.02f * wisdom;
    }

    public void DecrementMana()
    {
        mana = mana - 20;
    }

    public void DecrementEnergy()
    {
        energy = energy - 20;
    }

    public void DecrementHealth()
    {
        hitSound.Play();
        health = health - 5;
    }
}
