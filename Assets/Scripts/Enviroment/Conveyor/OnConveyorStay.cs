using UnityEngine;

public class OnConveyorStay : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.TryGetComponent(out PlayerCharacter player))
        {
           
        }
    }
}
