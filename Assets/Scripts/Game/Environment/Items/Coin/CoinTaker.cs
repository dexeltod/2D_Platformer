using Game.PlayerScripts.PlayerData;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Environment.Items.Coin
{
    public class CoinTaker : MonoBehaviour
    {
        public event UnityAction CoinTaked;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out PlayerHealth player))
                Interact();
        }

        private void Interact()
        {
            CoinTaked.Invoke();
            Destroy(gameObject);
        }
    }
}
