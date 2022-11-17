using UnityEngine;

public class DieTransition : Transition
{
    [SerializeField] private Enemy _enemy;
    public override void Enable()
    {
        _enemy.WasDying += ChangeState;
    }
    
    public void OnDisable()
    {
        _enemy.WasDying -= ChangeState;
    }

    private void ChangeState(Enemy enemy)
    {
        enemy.WasDying -= ChangeState;
        IsNeedTransition = true;
    }
}
