using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Door : MonoBehaviour
{

     [SerializeField] private GameObject _door;

    public void Open() 
    { 
        _door.SetActive(false);
    }
    public void Close()
    {
        _door.SetActive(true);
    }
}
