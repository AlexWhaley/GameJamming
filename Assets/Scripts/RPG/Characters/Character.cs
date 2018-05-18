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

    private Animator _actionAnimator;

    // Current battle stats
    private float _currentHealth;
    
    // Current shield properties
    private float _shieldModifier = 1.0f;
    private int _shieldTurnDuration = 0;

    public bool IsAlive { get; private set; }

    public TargetIndicatorController TargetIndicatorController { get; private set; }
    private HealthBar _healthBar;

    public float ArmorModifier
    {
        get { return _armorModifier; }
    }

    public float AttackModifier
    {
        get { return _attackModifier; }
    }

    public float HealModifier
    {
        get { return _healModifier; }
    }

    public float CurrentHealth
    {
        get
        {
            return _currentHealth;
        }
        set
        {
            _currentHealth = value;
            _healthBar.SetSliderValue(_currentHealth / _healthStat);
        }
    }


    private void Awake()
    {
        PrefabSpawner[] prefabSpawners = GetComponentsInChildren<PrefabSpawner>();
        foreach (var prefab in prefabSpawners)
        {
            prefab.Spawn();
        }

        InitialiseBattle();
    }

    private void Start()
    {
        TargetIndicatorController = GetComponentInChildren<TargetIndicatorController>();
        _actionAnimator = GetComponentInChildren<Animator>();
        _healthBar = GetComponentInChildren<HealthBar>();
    }

    public void InitialiseBattle()
    {
        _currentHealth = _healthStat;
        IsAlive = true;
    }

    public void ApplyDamage(int damage)
    {
        if (IsAlive)
        {
            float potentialHealth = CurrentHealth - (int)(damage * _shieldModifier);

            _actionAnimator.SetTrigger("PlayDamage");

            if (potentialHealth <= 0)
            {
                IsAlive = false;
                CurrentHealth = 0.0f;
            }
            else
            {
                CurrentHealth = potentialHealth;
            }
        }
    }

    public void ApplyHealing(int healing)
    {
        if (IsAlive)
        {
            float potentialHealth = CurrentHealth + healing;

            _actionAnimator.SetTrigger("PlayHeal");

            if (potentialHealth > _healthStat)
            {
                CurrentHealth = _healthStat;
            }
            else
            {
                CurrentHealth = potentialHealth;
            }
        }
    }

    public void ApplyShield(float newShieldModifier)
    {
        if (IsAlive)
        {
            _actionAnimator.SetTrigger("PlayShield");
            _shieldModifier -= _shieldTurnDuration;
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

    private void SetTargeted(Character targetedBy, bool isTargeted)
    {
        TargetIndicatorController.SetActiveTargetingIndicator(targetedBy, isTargeted);
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
