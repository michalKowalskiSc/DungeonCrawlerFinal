using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blending2Texture : MonoBehaviour
{

    public Material mat;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mat.SetFloat("_LerpValue",Mathf.Sin(Time.time));
    }
}
