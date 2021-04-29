using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]

public class PortalScript : MonoBehaviour
{

    public Material mat;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
        //if ((BloodCounterClass.bloodCounter > 0) || (BloodCounterClass.bloodCollision == true))
        //{
        //    BloodCounterClass.bloodCounter--;
            Graphics.Blit(src, dest, mat);
        //}
        //else
        //{
        //    Graphics.Blit(src, dest);
        //}
    }
}
