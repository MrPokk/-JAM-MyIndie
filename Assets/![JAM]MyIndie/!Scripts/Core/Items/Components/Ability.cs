using System;
using UnityEngine;

[Serializable]
public class Ability
{
    [SerializeReference, SelectImplementation(typeof(IAbilityData))]
    public IAbilityData data;

    public bool Is<T>() where T : IAbilityData => data is T;

    public bool Is<T>(out T result) where T : struct, IAbilityData
    {
        if (data is T t)
        {
            result = t;
            return true;
        }
        result = default;
        return false;
    }
}

public interface IAbilityComponent
{
    Ability Ability { get; set; }
}

public interface IAbilityData { }
