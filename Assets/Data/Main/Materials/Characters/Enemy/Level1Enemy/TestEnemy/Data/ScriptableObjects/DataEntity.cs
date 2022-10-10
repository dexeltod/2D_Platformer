using UnityEngine;

[CreateAssetMenu(fileName = "EntityData", menuName = "Data/EnemyEntity/EntityData")]
public class DataEntity : ScriptableObject
{
    public int Health;
    public float MoveSpeed;
}
