using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarPlayers : MonoBehaviour
{
    public int maxHealth;
    public int Health;

    public GameObject []lives;
    //public Sprite fullLive;
    //public Sprite emptyLive;

    public void hill()
    {
        Health++;
    }

    public void damage()
    {
        Health--;
    }

    private void Update()
    {
        if (Health > maxHealth) Health = maxHealth;

        for (int i = 0; i < lives.Length; i++)
        {
            if (i < Health) lives[i].GetComponent<Animator>().SetBool("Live", true);

            else lives[i].GetComponent<Animator>().SetBool("Live", false);


            if (i < maxHealth) lives[i].GetComponent<Image>().enabled = true;

            else lives[i].GetComponent<Image>().enabled = false;
        }

        if (Health <= 0)
        {
            Health = 0;
        }
    }
}
