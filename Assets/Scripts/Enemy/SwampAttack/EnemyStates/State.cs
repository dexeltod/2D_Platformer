using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    [SerializeField] private List<Transition> _transitions;

    protected Player Target { get; set; }

    public void Enter(Player target)
    {
        if (enabled == false)
        {
            Target = target;
            enabled = true;
            foreach (var transition in _transitions)
            {
                transition.enabled = true;
                transition.Initialize(Target);
            }
        }
    }

    public State GetNextState()
    {
        foreach (var transition in _transitions)
        {
            if (transition.IsNeedTransition)
            {
                return transition.TargetNextState;
            }
        }
        return null;
    }

    public void Exit()
    {
        if (enabled == true)
        {
            foreach (var transition in _transitions)
            {
                transition.enabled = false;
                enabled = false;
            }
        }    
    }
}
