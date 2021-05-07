using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;
using System;
using UnityEngine;


//[ExecuteInEditMode]

public class BloodScript : MonoBehaviour
{
    public Material mat; //shader z zadawaniem obrazen

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
        if (other.gameObject.tag == "Trap") //wejscie w pulapke
        {
            if (((other.gameObject.name == "Pf_Trap_Fire") && (Traps.fireTrapActive == 1)) || ((other.gameObject.name == "Pf_Trap_Needle") && (Traps.needleTrapActive == 1)) || ((other.gameObject.name == "Pf_Trap_Cutter (1)") && (Traps.cutterTrapActive == 1)))
            {
                BloodCounterClass.bloodCollision = true; //jest w kolizji z pulapka
                BloodCounterClass.trapDelay = 200; //opoznienie pulapki na rozbrojenie
            }
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Trap") //wyjscie z pulapki
        {
            if (((other.gameObject.name == "Pf_Trap_Fire") && (Traps.fireTrapActive==1)) || ((other.gameObject.name == "Pf_Trap_Needle") && (Traps.needleTrapActive == 1)) || ((other.gameObject.name == "Pf_Trap_Cutter (1)") && (Traps.cutterTrapActive == 1)))
            {
                if (BloodCounterClass.bloodCollision == true) //jesli nie rozbrojono
                {
                    BloodCounterClass.bloodCounter = 150; //czas zadawania obrazen
                }
                BloodCounterClass.bloodCollision = false; //wyjscie z kolizji z pulapka    
            }
        }

    }


    private void OnCollisionEnter(Collision collision)
    {

    }

    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (BloodCounterClass.trapDelay > 0)
        {
            BloodCounterClass.trapDelay--; //dekremenatacja licznika czasu na rozbrojenie
        }

        if (((BloodCounterClass.bloodCounter > 0) || (BloodCounterClass.bloodCollision == true)) && (BloodCounterClass.trapDelay < 1)) //(jesli czas zadawania obrazen dodatni lub kolizji z pulapka) i skonczyl sie czas na rozbrojenie
            {
            BloodCounterClass.bloodCounter--; //dekrementacja licznika zadawania obrazen
            Graphics.Blit(src, dest, mat); //wywolaj shader zadawania obrazen
        }
        else
        {
            Graphics.Blit(src, dest); //nie wywoluj shadera
        }
    }
}
