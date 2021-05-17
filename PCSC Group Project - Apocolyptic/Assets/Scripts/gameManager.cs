using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    public playerController player;
    public Image dashIcon;
    public Image missileIcon;
    public Image volleyIcon;
    public Image trackingIcon;
    public Text healthText;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Updates Health Text
        healthText.text = "Health: " + player.health;
        //

        //Dash Cooldown
        if (player.dashCooldown <= 0)
        {
            dashIcon.color = new Color32(0, 255, 0, 255);
        }
        else
        {
            dashIcon.color = new Color32(255, 0, 0, 255);
        }
        //

        //Missile Selection
        if (player.missileEquipped == true)
        {
            missileIcon.color = new Color32(0, 255, 0, 255);
        }
        else
        {
            missileIcon.color = new Color32(255, 0, 0, 255);
        }

        if (player.volleyEquipped == true)
        {
            volleyIcon.color = new Color32(0, 255, 0, 255);
        }
        else
        {
            volleyIcon.color = new Color32(255, 0, 0, 255);
        }

        if (player.trackingMissileEquipped == true)
        {
            trackingIcon.color = new Color32(0, 255, 0, 255);
        }
        else
        {
            trackingIcon.color = new Color32(255, 0, 0, 255);
        }
        //
    }
}
