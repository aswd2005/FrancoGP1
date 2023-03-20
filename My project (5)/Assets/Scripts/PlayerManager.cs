using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    Movement movement;
    public int coinCount;

    void Start()
    {
        movement = GetComponent<Movement>();
    }

    public bool PickupItem(GameObject obj)
    {
        switch (obj.tag)
        {
            case "Currency":
                coinCount++;
                return true;
            case "Speed+":
                movement.SpeedPowerUp();
                return true;
            default:
                Debug.Log("Item tag or reference not set");
                return false;

        }
       
    }
    public void TakeDamage()
    {
        currentHealth -= 1;
    }
  public void PauseGame()
    {
        Time.timeScale = 0;
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
    }
}
