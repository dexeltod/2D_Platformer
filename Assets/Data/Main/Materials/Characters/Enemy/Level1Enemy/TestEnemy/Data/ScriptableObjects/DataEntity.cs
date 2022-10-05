using UnityEngine;

[CreateAssetMenu(fileName = "EntityData", menuName = "Data/Enemy/EntityData")]
public class DataEntity : ScriptableObject
{
    public int Health;
    public float MoveSpeed;
}
