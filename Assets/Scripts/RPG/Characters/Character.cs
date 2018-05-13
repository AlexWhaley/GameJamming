using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character
{
    private int _health = 1000;
    private float _shieldModifier = 1.0f;
    private int _shieldTurnDuration = 0;

    public bool IsAlive { get; private set; }

    public void ApplyDamage(int damage)
    {
        _health -= (int)(damage * _shieldModifier);
        if (_health <= 0)
        {
            IsAlive = false;
            _health = 0;
        }
    }

    public void ApplyHealing(int healing)
    {
        if (IsAlive)
        {
            _health += healing;
        }
    }

    public void ApplyShield(float newShieldModifier, int addTurnDuration)
    {
        if (IsAlive)
        {
            if (newShieldModifier < _shieldModifier)
            {
                _shieldModifier = newShieldModifier;
            }
            
        }
    }

    public void ProcessTurn()
    {
        ReduceShield();
    }

    private void ReduceShield()
    {
        if(_shieldTurnDuration > 0)
        {
            if (--_shieldTurnDuration == 0)
            {
                _shieldModifier = 1.0f;
            }
        }
    }
}
