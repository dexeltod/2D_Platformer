using System;
using Game.PlayerScripts;
using UnityEngine;

namespace Game.Enemy.Services
{
    public class EnemyMeleeTrigger : MonoBehaviour
    {
        private Collider2D _collider;
	
        public event Action<bool> TouchedPlayer;

        private void Awake()
        {
            _collider = GetComponent<BoxCollider2D>();
        }

        private void OnEnable() => 
            _collider.enabled = true;

        private void OnDisable() => 
            _collider.enabled = false;

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
