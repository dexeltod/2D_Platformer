using UnityEngine;

namespace PlayerScripts.Weapons
{
    public interface IRangedWeapon
    {
        Transform BulletSpawnTransform { get; }
        float BulletSpeed { get; }
    }
}