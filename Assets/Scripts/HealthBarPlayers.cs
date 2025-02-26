using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthBarPlayers : MonoBehaviour
{
    [SerializeField] private GameObject _diaedMenu;

    public int maxHealth = 3;
    public int Health = 3;

    public GameObject []lives;

    public void GetHill()
    {
        Health++;
        if (Health > maxHealth) Health = maxHealth;
        lives[Health - 1].GetComponent<Animator>().SetBool("Live", true);
    }

    public void GetDamage()
    {
        Health--;
        if (Health <= 0)
        {
            Health = 0;
            //_anim.SetTrigger("DIED");
            Died();
        }
        lives[Health].GetComponent<Animator>().SetBool("Live", false);

    }

    private void Died()
    {
        _diaedMenu.SetActive(true);
    }
}
