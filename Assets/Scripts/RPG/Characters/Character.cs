using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities.Prefabs;

public class Character : MonoBehaviour
{
    [Header("Character Attributes")]
    [SerializeField]
    private int _healthStat = 1000;
    [SerializeField, Tooltip("Lower is better - 0 takes no damage -> 1 takes normal damage.")]
    private float _armorModifier = 1.0f;
    [SerializeField, Tooltip("Higher is better - 1 deals normal damage -> 2 deals double damage.")]
    private float _attackModifier = 1.0f;
    [SerializeField, Tooltip("Higher is better - 1 heals a normal amount -> 2 heals double.")]
    private float _healModifier = 1.0f;

    // Current battle stats
    private float _currentHealth;
    
    // Current shield properties
    private float _shieldModifier = 1.0f;
    private int _shieldTurnDuration = 0;

    public bool IsAlive { get; private set; }

    private TargetIndicatorController _targetIndicatorController;

    private void Awake()
    {
        PrefabSpawner[] prefabSpawners = GetComponentsInChildren<PrefabSpawner>();
        foreach (var prefab in prefabSpawners)
        {
            prefab.Spawn();
        }
    }

    private void Start()
    {
        _targetIndicatorController = GetComponentInChildren<TargetIndicatorController>();
    }

    public void InitialiseBattle()
    {
        _currentHealth = _healthStat;
    }

    public void ApplyDamage(int damage)
    {
        _healthStat -= (int)(damage * _shieldModifier);
        if (_currentHealth <= 0)
        {
            IsAlive = false;
            _currentHealth = 0;
        }
    }

    public void ApplyHealing(int healing)
    {
        if (IsAlive)
        {
            _currentHealth += healing;
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

    public void TickTurnEffects()
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

    private void SetTargeted(PlayerCharacter.CharacterNames targetedBy, bool isTargeted)
    {
        _targetIndicatorController.SetActiveTargetingIndicator(targetedBy, isTargeted);
    }

    public float HealthPortionRemaining
    {
        get { return (float)_currentHealth / (float)_healthStat; }
    }

    public float EffectiveHealthRemaining
    {
        get { return (float)_currentHealth / (_armorModifier); }
    }
}
