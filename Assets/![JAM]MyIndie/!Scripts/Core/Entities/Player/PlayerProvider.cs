using BitterECS.Integration;
using UnityEngine;

[RequireComponent(typeof(MovingComponentProvider), typeof(InputComponentProvider))]
[RequireComponent(typeof(CharacterController))]
public class PlayerProvider : EntitiesProvider
{

}
