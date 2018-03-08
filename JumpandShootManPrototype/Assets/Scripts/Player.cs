using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    //RPG Stats
    public int bravery;
    public int cunning;
    public int resolve;
    public int willpower;
    public int intelligence;
    public int arcane;

    //Ability Bools
    public bool canjetpack;
    public bool cansprint;
    public bool canshield;
    public bool canmove;

    //UI: Player Canvas
    public GameObject fpsHud;
    public GameObject statsHud;
    public Text healthLabelText;
    public Text energyLabelText;
    public Text manaLabelText;
    public Text fuelArmText;
    
    public Slider healthSlider;
    public Slider manaSlider;
    public Slider energySlider;

    //UI: CharacterSheet
    public Text braveryLabelText;
    public Text cunningLabelText;
    public Text resolveLabelText;
    public Text willpowerLabelText;
    public Text intelligenceLabelText;
    public Text arcaneLabelText;

    public Text healthmaxLabelText;
    public Text manamaxLabelText;
    public Text energymaxLabelText;
    public Text energyregenLabelText;
    public Text manaregenLabelText;
    public Text damageLabelText;

    //Sound
    public AudioSource hitSound;
    public AudioSource menuthudSound;

    private int health;
    private int healthMax;
    private float mana;
    private int manaMax;
    private float energy;
    private int energyMax;
    private int damage;
    private float manaRegen;
    private float energyRegen;

    private Rigidbody bod;


	// Use this for initialization
	void Start ()
    {
        StartCoroutine("rollStats");
        

    }


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
            bod.AddForce(transform.forward * 5, ForceMode.Impulse);
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
        energy = energy + 0.02f * resolve;
        mana = mana + 0.02f * willpower;
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

    IEnumerator rollStats()
    {
        canmove = false;
        //gameObject.GetComponent<RigidybodyFirstPersonController>
        //Time.timeScale = 0;
        Debug.Log("Roll Stats");
        yield return new WaitForSeconds(1);
        int wait = 1 + Random.Range(0, 2);
        
        
        bod = GetComponent<Rigidbody>();
        health = 100;
        mana = 0;
        energy = 0;
         
        fpsHud.SetActive(false);


        //roll stats
        bravery = 10 + Random.Range(0, 8);
        cunning = 10 + Random.Range(0, 8);
        resolve = 10 + Random.Range(0, 8);
        willpower = 10 + Random.Range(0, 8);
        intelligence = 10 + Random.Range(0, 8);
        arcane = 10 + Random.Range(0, 8);

        //set properties based on stats rolled
        healthMax = resolve * 10;
        healthSlider.maxValue = healthMax;
        health = healthMax;
        manaMax = intelligence * 20;
        mana = manaMax;
        manaSlider.maxValue = manaMax;
        energyMax = cunning * 20;
        energy = energyMax;
        energySlider.maxValue = energyMax;
        damage = bravery / 10;
        energyRegen = cunning / 10;
        manaRegen = willpower / 10;

        //Set movement ability
        canjetpack = true;
        //Set max fuel Displays
        healthLabelText.text = healthMax.ToString();
        energyLabelText.text = energyMax.ToString();
        manaLabelText.text = manaMax.ToString();

        //Start displaying UI in sequence with results
        menuthudSound.Play();
        braveryLabelText.text = "Bravery: " + bravery.ToString();
        yield return new WaitForSeconds(0.5f);
        menuthudSound.Play();
        cunningLabelText.text = "Cunning: " + cunning.ToString();
        yield return new WaitForSeconds(0.5f);
        menuthudSound.Play();
        resolveLabelText.text = "Resolve: " + resolve.ToString();
        yield return new WaitForSeconds(0.5f);
        menuthudSound.Play();
        willpowerLabelText.text = "Willpower: " + willpower.ToString();
        yield return new WaitForSeconds(0.5f);
        menuthudSound.Play();
        intelligenceLabelText.text = "Intelligence: " + intelligence.ToString();
        yield return new WaitForSeconds(0.5f);
        menuthudSound.Play();
        arcaneLabelText.text = "Arcane: " + arcane.ToString();
        yield return new WaitForSeconds(0.5f);
        menuthudSound.Play();
        healthmaxLabelText.text = "Max Health: " + healthMax.ToString();
        yield return new WaitForSeconds(0.5f);
        menuthudSound.Play();
        manamaxLabelText.text = "Max Mana: " + manaMax.ToString();
        yield return new WaitForSeconds(0.5f); menuthudSound.Play();
        menuthudSound.Play();
        energymaxLabelText.text = "Max Energy: " + energyMax.ToString();
        yield return new WaitForSeconds(0.5f); menuthudSound.Play();
        menuthudSound.Play();
        energyregenLabelText.text = "Energy Regen: " + energyRegen.ToString();
        yield return new WaitForSeconds(0.5f); menuthudSound.Play();
        menuthudSound.Play();
        manaregenLabelText.text = "Mana Regen: " + manaRegen.ToString();
        yield return new WaitForSeconds(0.5f); menuthudSound.Play();
        menuthudSound.Play();
        damageLabelText.text = "Damage: " + damage.ToString();
        yield return new WaitForSeconds(5.0f);

        fpsHud.SetActive(true);
        statsHud.SetActive(false);

        yield return null;

    }
}
