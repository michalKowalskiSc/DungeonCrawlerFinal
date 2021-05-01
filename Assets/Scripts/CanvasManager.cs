using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    private GameObject notificationObj;
    public Sprite sprDisarmDefault;
    public Sprite sprDisarmGreen;
    bool isBtnDisarmActive;
    bool isNotifActive;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("CanvasManager initialized");
        this.isBtnDisarmActive = false;
        this.isNotifActive = false;
        notificationObj = GameObject.Find("Notification");
        this.notificationObj.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleButton(string btnName, int mode) {
        GameObject btnObject = GameObject.Find(btnName);

        if (btnName == "ButtonDisarmTrap") {
            if (mode == 0)
            {
                btnObject.GetComponent<UnityEngine.UI.Image>().overrideSprite = this.sprDisarmDefault;
                this.isBtnDisarmActive = false;
            }
            else if (mode == 1)
            {
                btnObject.GetComponent<UnityEngine.UI.Image>().overrideSprite = this.sprDisarmGreen;
                this.isBtnDisarmActive = true;
            }
        }

    }
    public void DisarmAction()
    {
        if (isBtnDisarmActive) {
            this.ToggleButton("ButtonDisarmTrap", 0);
            if (Traps.trapId==1)
            {
                Traps.fireTrapActive = 0;
                Traps.trapId = 0;
                BloodCounterClass.bloodCollision = false;
                GameObject trapComponent = GameObject.Find("Eff_Fire");
                trapComponent.active = false;
            }
            if (Traps.trapId == 2)
            {
                Traps.needleTrapActive = 0;
                Traps.trapId = 0;
                BloodCounterClass.bloodCollision = false;
                GameObject trapComponent = GameObject.Find("Needle");
                trapComponent.active = false;
            }
            if (Traps.trapId == 3)
            {
                Traps.cutterTrapActive = 0;
                Traps.trapId = 0;
                BloodCounterClass.bloodCollision = false;
                GameObject trapComponent = GameObject.Find("Cutter");
                trapComponent.active = false;
            }
            if (DoorId.doorId==1)
            {
                GameObject door = GameObject.Find("Door 1");
                door.transform.position = new Vector3(6.0f, 0.0f, 22.2f);
                door.transform.Rotate(0.0f, 90.0f, 0.0f,Space.Self);
                door.GetComponent<BoxCollider>().enabled = false;
                DoorId.doorId = 0;
            }
            if (DoorId.doorId == 2)
            {
                GameObject door = GameObject.Find("Door 2");
                door.transform.position = new Vector3(6.0f, 0.0f, 28.2f);
                door.transform.Rotate(0.0f, 90.0f, 0.0f, Space.Self);
                door.GetComponent<BoxCollider>().enabled = false;
                DoorId.doorId = 0;
            }
            if (DoorId.doorId == 3)
            {
                GameObject door = GameObject.Find("Door 3");
                door.transform.position = new Vector3(19.0f, 0.0f, 22.2f);
                door.transform.Rotate(0.0f, -90.0f, 0.0f, Space.Self);
                door.GetComponent<BoxCollider>().enabled = false;
                DoorId.doorId = 0;
            }
            if (DoorId.doorId == 4)
            {
                GameObject door = GameObject.Find("Door 4");
                door.transform.position = new Vector3(19.0f, 0.0f, 28.2f);
                door.transform.Rotate(0.0f, -90.0f, 0.0f, Space.Self);
                door.GetComponent<BoxCollider>().enabled = false;
                DoorId.doorId = 0;
            }
            this.notificationObj.SetActive(false);
            this.isNotifActive = false;
        }
    }

    public void ToggleNotification(bool mode)
    {
        if (mode == false)
        {
            this.notificationObj.SetActive(false);
            this.isNotifActive = false;
        }
        else
        {
            this.notificationObj.SetActive(true);
            this.isNotifActive = true;
        }
    }
}
