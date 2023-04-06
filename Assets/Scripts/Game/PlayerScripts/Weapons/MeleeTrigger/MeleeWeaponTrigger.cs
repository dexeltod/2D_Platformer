using System;
using Game.Enemy;
using UnityEngine;

namespace Game.PlayerScripts.Weapons.MeleeTrigger
{
    public class MeleeWeaponTrigger : MonoBehaviour
    {
        public event Action<IWeaponVisitor> Touched;

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.TryGetComponent(out IWeaponVisitor enemy)) 
                Touched?.Invoke(enemy);
        }
    }
}