using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character
{
    private int _health = 500;

    public bool IsAlive { get; private set; }

    public void DoDamage(int damage)
    {
        _health -= damage;
        if (_health <= 0)
        {
            IsAlive = false;
            _health = 0;
        }
    }
}
