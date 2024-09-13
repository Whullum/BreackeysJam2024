using System.Collections.Generic;
using System.Linq;
using _Scripts.Extentions;
using _Scripts.Gameplay.SpotSystem;
using _Scripts.Infrastructure;
using UnityEngine;
using Zenject;

namespace _Scripts.Gameplay.Weapon
{
    public class WeaponOnGroundFactory : SpotObjectFactory<WeaponOnGround>
    {
        public void SpawnWeapon(WeaponType weaponType, Vector2Int coordinates)
        {
            WeaponOnGround weapon = SpawnObject(null, coordinates);
            weapon.Init(weaponType);
        }
    }
}