using UnityEngine;
using System;
using BitterECS.Integration;

[Serializable]
public struct HealthComponent
{
    [Tooltip("Maximum number of dash charges the entity can have.")]
    public int maxHealth;

    [Tooltip("Maximum number of dash charges the entity can have.")]
    public int currentHealth;

    public float timeImmunity;
    public int lastDamage;
}

public class HealthComponentProvider : ProviderEcs<HealthComponent>
{
}
