//
//  State data that needs to be sync'd across multiplayer should all live here. display, effects, etc should be triggered by states in this class
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rewired;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.Networking;

public class PlayerStats : NetworkBehaviour
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
    public bool isgrounded;
    public bool isAirControl;

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
    
    //Network sync these values
    [SyncVar]
    public float health;
    [SyncVar]
    public float healthMax;
    [SyncVar]
    public float mana;
    [SyncVar]
    public int manaMax;
    [SyncVar]
    public float energy;
    [SyncVar]
    public int energyMax;
    [SyncVar]
    public int damage;
    [SyncVar]
    public float manaRegen;
    [SyncVar]
    public float energyRegen;

    //Player references
    //private GameObject playerController; 
    private Rigidbody bod;
    public Camera cam;
    private Vector3 startPosition;

    // Use this for initialization
    void Start ()
    {
        startPosition = new Vector3(0, 7, 0);
        StartCoroutine("rollStats");
        if (!isLocalPlayer)
        {
            gameObject.GetComponent<RigidbodyFirstPersonController>().enabled = !gameObject.GetComponent<RigidbodyFirstPersonController>(); //disable movement class if not local player
            cam.enabled = !cam.enabled; //disable camera if not local player
        }
    }

    void Update()
    {

        healthSlider.value = health;
        manaSlider.value = mana;
        energySlider.value = energy;
        int manaInt = (int)mana;
        fuelArmText.text = manaInt.ToString();

        

        if (mana > manaMax)
        {
            mana = manaMax;
            manaLabelText.text = mana.ToString();
        }

        if (energy > energyMax)
        {
            energy = energyMax;
            energyLabelText.text = energy.ToString();
        }

        if (isAirControl == true)
        {
            gameObject.GetComponent<RigidbodyFirstPersonController>().advancedSettings.airControl = true;
        } else {
            gameObject.GetComponent<RigidbodyFirstPersonController>().advancedSettings.airControl = false;
        }

        if (health <= 0)
        {
            CmdDeath();
        }

    }

    void FixedUpdate()
    {
        PassiveRegen();
    }

    [Command]
    public void CmdDeath()
    {
       if (isLocalPlayer)
       {
            Debug.Log("death called");
            //do death stuff?
            gameObject.transform.position = startPosition;
            health = healthMax;
            healthLabelText.text = health.ToString();
       }
        
    }

    public void PassiveRegen()
    {
        energy = energy + 0.02f * resolve;
        if (energy > energyMax)
        {
            energy = energyMax;
            energyLabelText.text = energy.ToString();
        }
        mana = mana + 0.02f * willpower;
        if (mana > manaMax)
        {
            mana = manaMax;
            manaLabelText.text = mana.ToString();
        }
        health = health + 0.02f * resolve;
        if (health > healthMax)
        {
            health = healthMax;
            healthLabelText.text = health.ToString();
        }
    }

    public void DecrementMana()
    {
        mana = mana - 20;
        manaLabelText.text = mana.ToString();
    }

    public void DecrementEnergy(float a)
    {
        energy = energy - a;
        energyLabelText.text = energy.ToString();
    }

    [Command]
    public void CmdDecrementHealth()
    {

        Debug.Log("Reducing health");
        hitSound.Play();
        Debug.Log("Health was " + health.ToString());
        health = health - 35;
        Debug.Log("Health is now " + health.ToString());
        healthLabelText.text = health.ToString();
        if (health <= 0)
        {
            CmdDeath();
        }

    }

    IEnumerator rollStats()
    {
        if (!isLocalPlayer)
        {
            yield return null;
        }
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
        yield return new WaitForSeconds(0.1f);
        menuthudSound.Play();
        cunningLabelText.text = "Cunning: " + cunning.ToString();
        yield return new WaitForSeconds(0.1f);
        menuthudSound.Play();
        resolveLabelText.text = "Resolve: " + resolve.ToString();
        yield return new WaitForSeconds(0.1f);
        menuthudSound.Play();
        willpowerLabelText.text = "Willpower: " + willpower.ToString();
        yield return new WaitForSeconds(0.1f);
        menuthudSound.Play();
        intelligenceLabelText.text = "Intelligence: " + intelligence.ToString();
        yield return new WaitForSeconds(0.1f);
        menuthudSound.Play();
        arcaneLabelText.text = "Arcane: " + arcane.ToString();
        yield return new WaitForSeconds(0.1f);
        menuthudSound.Play();
        healthmaxLabelText.text = "Max Health: " + healthMax.ToString();
        yield return new WaitForSeconds(0.1f);
        menuthudSound.Play();
        manamaxLabelText.text = "Max Mana: " + manaMax.ToString();
        yield return new WaitForSeconds(0.1f); menuthudSound.Play();
        menuthudSound.Play();
        energymaxLabelText.text = "Max Energy: " + energyMax.ToString();
        yield return new WaitForSeconds(0.1f); menuthudSound.Play();
        menuthudSound.Play();
        energyregenLabelText.text = "Energy Regen: " + energyRegen.ToString();
        yield return new WaitForSeconds(0.1f); menuthudSound.Play();
        menuthudSound.Play();
        manaregenLabelText.text = "Mana Regen: " + manaRegen.ToString();
        yield return new WaitForSeconds(0.1f); menuthudSound.Play();
        menuthudSound.Play();
        damageLabelText.text = "Damage: " + damage.ToString();
        yield return new WaitForSeconds(0.5f);

        fpsHud.SetActive(true);
        statsHud.SetActive(false);

        yield return null;

    }
}
