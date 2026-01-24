using System;
using BitterECS.Integration;
using UnityEngine;

[Serializable]
public struct HealthComponent
{
    private const int MaxHealthLimit = 99999;

    [SerializeField, Range(1, MaxHealthLimit)]
    private int _maxHealth;

    private int _currentHealth;

    public readonly int GetCurrentHealth() => _currentHealth > 0 ? _currentHealth : _maxHealth;

    public readonly int GetMaxHealth() => _maxHealth;

    public void SetHealth(int health) => _currentHealth = Mathf.Clamp(health, 0, _maxHealth);

    public void SetMaxHealth(int health) => _maxHealth = Mathf.Clamp(health, 1, MaxHealthLimit);

    public void ResetHealth() => _currentHealth = _maxHealth;
}

public class HealthComponentProvider : ProviderEcs<HealthComponent> { }
