using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;
using System;
using UnityEngine;

public class BloodCounterClass : MonoBehaviour
{
    // Start is called before the first frame update


    static public int bloodCounter=0; //licznik czasu zadawania obrazen
    static public int trapDelay = 0; //licznik opoznienia na rozbrojenie
    static public Boolean bloodCollision = false; //czy w kolizji z pulapka
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
