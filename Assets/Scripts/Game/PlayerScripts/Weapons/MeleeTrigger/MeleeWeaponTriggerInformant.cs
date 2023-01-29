using System;
using UnityEngine;

namespace Game.PlayerScripts.Weapons.MeleeTrigger
{
    public class MeleeWeaponTriggerInformant : MonoBehaviour
    {
        public event Action<Enemy.Enemy> Touched;

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.TryGetComponent(out Enemy.Enemy enemy)) 
                Touched.Invoke(enemy);
        }
    }
}