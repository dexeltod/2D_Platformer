using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCast : MonoBehaviour
{

    [SerializeField] private float _lenght = 1f;
    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, _lenght);

        Debug.DrawRay(transform.position, transform.up, Color.red);

        if (hit.collider)
        {
            Destroy(hit.collider.gameObject);
        }
    }
}
