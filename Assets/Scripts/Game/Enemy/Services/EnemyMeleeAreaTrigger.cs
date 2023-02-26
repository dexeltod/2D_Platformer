using System;
using Game.PlayerScripts;
using UnityEngine;

namespace Game.Enemy.Services
{
    public class EnemyMeleeAreaTrigger : MonoBehaviour
    {
	    public event Action<bool> TouchedPlayer;

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
