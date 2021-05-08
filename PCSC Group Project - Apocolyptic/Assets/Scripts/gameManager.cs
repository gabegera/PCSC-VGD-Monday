using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    public playerController player;
    public Image dashIcon;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (player.dashCooldown <= 0)
        {
            dashIcon.color = new Color32(0, 255, 0, 255);
        }
        else
        {
            dashIcon.color = new Color32(255, 0, 0, 255);
        }
    }
}
