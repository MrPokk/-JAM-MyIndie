using BitterECS.Integration;
using UnityEngine;

public class ItemProvide : ProviderEcs<ItemPresenter>
{
    [field: SerializeField] public Ability Ability { get; set; }
}
