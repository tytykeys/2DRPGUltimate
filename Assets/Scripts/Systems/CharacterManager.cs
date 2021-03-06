﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CharacterManager : MonoBehaviour
{
    private float timer = 0f;
    private float timer1 = 0f;
    private float timer2 = 0f;
    private float holdDur = 3f;

    public Text txt;

    public int kichargevalue = 1;
    public int kipassivechargevalue = 1;

    public bool idleaura = false;
    public bool passivechargingKi = false;
    public bool chargingKi = false;



    public string CharacterName = "Default";
    static public int Level = 1;
    static public int BattlePower = 50;
    static public float Form = 1f;
    static public int Health = 100;
    public int maxHealth = 100;
    static public int Ki = 100;
    public int maxKi = 100;
    public static int Stamina = 100;
    public int maxStamina = 100;
    public int StaminaChargeRate = 1;
    static public int xp = 0;
    static public float maxForm = 1f;


    public byte rgbr = 255;
    public byte rgbg = 255;
    public byte rgbb = 255;
    public byte rgba = 255;


    public static bool Base = true;
    public static bool SS1 = false;


    public static bool SS1Unlocked = false;
    public static bool SS1AUnlocked = false;



    public static bool SkillTeleU = true; // "Tele" Being short for telekinesis
    public static bool SkillTeleExplodeU = true;
    public static bool SkillTeleDupeU = true;
    public static bool SkillFireballU = true;
    public static bool SkillKameU = true;
    public static bool SkillFlyU = true;
    public static bool SkillTeleportU = true;
    public static bool SkillDoubleJumpU = true;


    public Animator CharacterAnimator;
    public Animator auraAnimator;
    public GameObject Aura;
    public SpriteRenderer AuraColor;


    // Start is called before the first frame update
    void Start()
    {
        Aura.transform.gameObject.SetActive(true);
        //bool idleaura = false;
        AuraBaseColor();
        Color32 newColor = new Color32(rgbr, rgbg, rgbb, 0);
        AuraColor.color = newColor;
    }

    // Update is called once per frame
    void Update()
    {



        AuraScale();


        //Health Cap
        if (Health >= maxHealth) { Health = maxHealth; }

        //Ki Cap
        if (Ki >= maxKi) { Ki = maxKi; }

        if (idleaura == true)
        {
            Color32 newColor = new Color32(rgbr, rgbg, rgbb, 255);
            AuraColor.color = newColor;
            passivechargingKi = true;
        }

        if (Ki <= 0) { Ki = 0; }



        //Stamina Cap
        if (Stamina >= maxStamina) { Stamina = maxStamina; }
        Stamina = Stamina + StaminaChargeRate;


        if (Base == true)
        {
            //auraAnimator.Play("AuraAnimBase");
            AuraBaseColor();
        }

        if (Input.GetButtonDown("T")) // Revert to base form
        {
            Form = 0;
            Base = true;
        }

        if (Input.GetButton("I")) // Inventory probably
        {
            Debug.Log("Fuckin button pressed : I");
        }



        if (Input.GetButtonDown("H"))
        {
            timer = Time.time;
            timer1 = Time.time;
        }
        if (Input.GetButton("H")) // Transform/ next form
        {
            if (Time.time - timer > holdDur)
            {
                //by making it positive inf, we won't subsequently run this code by accident,
                //since X - +inf = -inf, which is always less than holdDur
                CharacterAnimator.Play("TransformSS1");
                if (Level >= 5 && Level <= 9)
                {
                    Form = Form + 0.5f;
                    Ki = Ki - 150;
                    if (Form >= maxForm) { Form = maxForm; }
                    Base = false;
                    timer1 = float.PositiveInfinity;

                }
                
                if (Form == 0f)
                {
                    auraAnimator.Play("AuraAnimBase");
                    AuraBaseColor();
                }

                if (Form == 0.5f)
                {
                    auraAnimator.Play("AuraAnimBase");
                    rgbr = 255;
                    rgbg = 251;
                    rgbb = 0;
                }

            }
        }
        else
        {
            timer = float.PositiveInfinity;
        }




        if (Input.GetButtonUp("P"))
        {
            LevelUp();
        }

        if (Input.GetButtonDown("C"))
        {
            Color32 newColor = new Color32(rgbr, rgbg, rgbb, 255);
            AuraColor.color = newColor;
        }


        if (Input.GetButtonUp("C"))
        {
            Color32 newColor = new Color32(rgbr, rgbg, rgbb, 0);
            AuraColor.color = newColor;
        }


        if (Input.GetButton("C"))
        {
            Ki = Ki + kichargevalue;
        }
        if (passivechargingKi == true)
        {
            Ki = Ki + kipassivechargevalue;
        }
}


   private void FixedUpdate()
    {
        Stamina = Stamina + StaminaChargeRate;
    }

    private void LevelUp()
    {
        Level = Level + 1;


        if (Level == 5)
        {
            Debug.Log("You unlocked Ascended Saiyan");
            maxForm = 0.5f;
        }

        if (Level == 10)
        {
            Debug.Log("You unlocked Super Saiyan 1");
            maxForm = 1f;
        }
    }




    private void AuraBaseColor()
    {
        rgbr = 255;
        rgbg = 255;
        rgbb = 255;
    }

    private void AuraScale()
    {

        float MaxAura = (Level / 40f + 1f);

        Aura.transform.localScale = new Vector3(MaxAura, MaxAura, 1);
        



    }



}
