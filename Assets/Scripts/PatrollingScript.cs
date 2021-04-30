using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingScript : MonoBehaviour
{
    public float speed;
    public Transform[] moveSpots;
    public float startWaitTime;
    public Animator anim;
    private int randomSpot;
    private float waitTime;
    private bool isMoving;


    // Start is called before the first frame update
    void Start()
    {
        //this.randomSpot = Random.Range(0, moveSpots.Length);
        this.isMoving = true;
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("isMoving", this.isMoving);
        
        transform.position = Vector3.MoveTowards(transform.position, this.moveSpots[this.randomSpot].position, this.speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, moveSpots[this.randomSpot].position) < 0.2f) {
            if (this.waitTime <= 0)
            {
                this.isMoving = true;
                int prevRandomSpot = this.randomSpot;
                do
                {
                    this.randomSpot = Random.Range(0, this.moveSpots.Length);
                } while (this.randomSpot == prevRandomSpot);
                transform.LookAt(moveSpots[this.randomSpot].position);

                this.waitTime = this.startWaitTime;
            }
            else {
                this.waitTime -= Time.deltaTime;
                this.isMoving = false;
            }
        }
    }
}
