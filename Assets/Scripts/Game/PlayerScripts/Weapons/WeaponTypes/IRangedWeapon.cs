using UnityEngine;

namespace Game.PlayerScripts.Weapons.WeaponTypes
{
    public interface IRangedWeapon
    {
        Transform BulletSpawnTransform { get; }
        float BulletSpeed { get; }
    }
}