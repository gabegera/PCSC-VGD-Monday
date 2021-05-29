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

    public Sprite heart;
    public Sprite halfHeart;

    public Image health1;
    public Image health2;
    public Image health3;
    public Image halfheart1;
    public Image halfheart2;
    public Image halfheart3;

    public GameObject playButton;
    public GameObject restartButton;
    public GameObject menuButton;
    public GameObject quitButton;
    public Text deathText;


    public Text healthText;



    // Start is called before the first frame update
    void Start()
    {
        restartButton.SetActive(false);
        menuButton.SetActive(false);
        deathText.enabled = (false);
        player = GameObject.Find("player").GetComponent<playerController>();
    }

    // Update is called once per frame
    void Update()
    {
        //Updates Health Text
        //healthText.text = "Health: " + player.health;
        //

        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            SceneManager.LoadScene("Level1");
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            SceneManager.LoadScene("Level2");
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            SceneManager.LoadScene("BossLevel");
        }

        //Updates Health Icons
        if (player.health <= 5)
        {
            health3.enabled = false;
        }
        if (player.health <= 4)
        {
            halfheart3.enabled = false;
        }
        if (player.health <= 3)
        {
            health2.enabled = false;
        }
        if (player.health <= 2)
        {
            halfheart2.enabled = false;
        }
        if (player.health <= 1)
        {
            health1.enabled = false;
        }
        if (player.health <= 0)
        {
            halfheart1.enabled = false;
        }
        //

        if (player.health <= 0)
        {
            restartButton.SetActive(true);
            menuButton.SetActive(true);
            deathText.enabled = (true);
        }

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

    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void quit()
    {
        Application.Quit();
    }

    public void menu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void play()
    {
        SceneManager.LoadScene("Level1");
    }

}
