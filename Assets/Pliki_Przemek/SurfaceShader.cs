using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfaceShader : MonoBehaviour
{

    public Material mat;

    //public GameObject FireTrap;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Traps.fireTrapActive==0)
        {
            GameObject trap = GameObject.Find("Pf_Trap_Fire");
            trap.GetComponent<MeshRenderer>().material = mat;
        }
        if (Traps.needleTrapActive == 0)
        {
            GameObject trap = GameObject.Find("Trap_Needle");
            trap.GetComponent<MeshRenderer>().material = mat;
        }
        if (Traps.cutterTrapActive == 0)
        {
            GameObject trap = GameObject.Find("Trap_Cutter");
            trap.GetComponent<MeshRenderer>().material = mat;
        }
    }


    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        
    }
}
