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
            FireTrap.active = 0;
            this.notificationObj.SetActive(false);
            this.isNotifActive = false;
        }
    }

    public void ToggleNotification()
    {
        if (this.isNotifActive == true)
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
