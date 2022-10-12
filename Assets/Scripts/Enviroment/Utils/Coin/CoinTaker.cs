using UnityEngine;
using UnityEngine.Events;

public class CoinTaker : MonoBehaviour
{
    public UnityAction CoinTaked;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out PlayerEntity player))
        {
            Interact();            
        }
        
    }

    private void Interact()
    {
        CoinTaked.Invoke();
        Destroy(gameObject);
    }

}