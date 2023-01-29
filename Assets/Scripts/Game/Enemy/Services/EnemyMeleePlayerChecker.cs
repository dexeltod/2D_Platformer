using System;
using Game.PlayerScripts;
using UnityEngine;

namespace Game.Enemy.Services
{
    public class EnemyMeleePlayerChecker : MonoBehaviour
    {
        private BoxCollider2D _boxCollider2D;
	
        public event Action<bool> TouchedPlayer;

        private void Awake()
        {
            _boxCollider2D = GetComponent<BoxCollider2D>();
        }

        private void OnEnable() => 
            _boxCollider2D.enabled = true;

        private void OnDisable() => 
            _boxCollider2D.enabled = false;

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.TryGetComponent(out Player _)) 
                TouchedPlayer?.Invoke(true);
        }

        private void OnTriggerExit2D(Collider2D col)
        {
            if (col.TryGetComponent(out Player _)) 
                TouchedPlayer?.Invoke(false);
        }
    }
}
