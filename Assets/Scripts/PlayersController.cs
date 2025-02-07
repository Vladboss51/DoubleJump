using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersController : MonoBehaviour
{
    [SerializeField] private HealthBarPlayers _healthPlayers;
    [SerializeField] private GameObject _shieldZone;
    [SerializeField] private ShieldSystem _shieldSystem;
    private float _distance;
    public float distance = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damage()
    {
        _distance = Vector2.Distance(transform.position, _shieldZone.transform.position);
        if (_distance < distance && _shieldZone.activeSelf)
        {
            _shieldSystem.DeactivationShield();
        }

        else 
            _healthPlayers.GetDamage();
    }
}
