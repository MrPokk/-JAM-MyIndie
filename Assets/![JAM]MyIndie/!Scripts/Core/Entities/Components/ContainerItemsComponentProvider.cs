using System;
using System.Collections.Generic;
using System.Linq;
using BitterECS.Integration;

[Serializable]
public struct ContainerItemsComponent
{
    public List<ItemProvide> items;
    public bool Constants<T>() where T : IAbilityData
    {
        return items != null && items.Any(e => e.Ability.Is<T>());
    }
    public T Get<T>() where T : IAbilityData
    {
        var result = items.Find(e => e.Ability.Is<T>());
        return (T)result.Ability.data;
    }
}

public class ContainerItemsComponentProvider : ProviderEcs<ContainerItemsComponent>
{ }
