using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HillBlock : MonoBehaviour
{
    [SerializeField] private HealthBarPlayers healthBar;

    public void hill(){

        healthBar.hill();
        gameObject.SetActive(false);
    }
}
