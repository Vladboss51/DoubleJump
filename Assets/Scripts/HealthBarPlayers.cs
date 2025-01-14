using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarPlayers : MonoBehaviour
{
    public int maxHealth;
    public int Health;

    public GameObject []lives;

    public void hill()
    {
        Health++;
        if (Health > maxHealth) Health = maxHealth;
        lives[Health - 1].GetComponent<Animator>().SetBool("Live", true);
    }

    public void damage()
    {
        Health--;
        if (Health <= 0)
        {
            Health = 0;
            //restart
        }
        lives[Health].GetComponent<Animator>().SetBool("Live", false);
    }
}
