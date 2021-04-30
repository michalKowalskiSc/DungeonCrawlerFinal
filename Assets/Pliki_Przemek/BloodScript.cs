using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;
using System;
using UnityEngine;


//[ExecuteInEditMode]

public class BloodScript : MonoBehaviour
{
    //private int bloodCounter = 0;
    //private Boolean bloodTri = false;

    public Material mat;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Trap")
        {
            if (((other.gameObject.name == "Pf_Trap_Fire") && (FireTrap.active == 1)) || ((other.gameObject.name == "Pf_Trap_Needle") && (NeedleTrap.active == 1)) || ((other.gameObject.name == "Pf_Trap_Cutter (1)") && (RotaryTrap.active == 1)))
            {
                //bloodCounter = 100;
                //bloodTri = true;
                //BloodCounterClass.bloodCounter = 1000;
                BloodCounterClass.bloodCollision = true;
                BloodCounterClass.trapDelay = 200;
                //Debug.Log("trigger");
                //Debug.Log(BloodCounterClass.bloodCounter);
            }
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Trap")
        {
            if (((other.gameObject.name == "Pf_Trap_Fire") && (FireTrap.active==1)) || ((other.gameObject.name == "Pf_Trap_Needle") && (NeedleTrap.active == 1)) || ((other.gameObject.name == "Pf_Trap_Cutter (1)") && (RotaryTrap.active == 1)))
            {
                //bloodCounter = 100;
                //bloodTri = true;
                //Debug.Log("trigger");
                if (BloodCounterClass.bloodCollision == true)
                {
                    BloodCounterClass.bloodCounter = 150;
                }
                BloodCounterClass.bloodCollision = false;
                //Debug.Log(BloodCounterClass.bloodCounter);
            }
        }

    }


    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.gameObject.tag == "Trap")
        //{
        //    bloodCounter = 100;
        //    Debug.Log("collision");
        //}
    }

    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {



        //src is the fully rendered scenen that you would normally
        //send directly to the monitor.

        /*
        Color[] pixels = new Color[1920 * 1080];

        for (int x=0;x<1920;x++)
        {
            for (int y=0;y<1080;y++)
            {
                pixels[x + y * 1080].r = 1.18f * 2.17f;
            }
        }
        */
        if (BloodCounterClass.trapDelay > 0)
        {
            BloodCounterClass.trapDelay--;
        }

        if (((BloodCounterClass.bloodCounter > 0) || (BloodCounterClass.bloodCollision== true)) && (BloodCounterClass.trapDelay<1) && (FireTrap.active == 1))
        {
            BloodCounterClass.bloodCounter--;
            Graphics.Blit(src, dest, mat);
        }
        else
        {
            Graphics.Blit(src, dest);
        }
    }
}
