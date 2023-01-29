using UnityEngine;

namespace Game.PlayerScripts.StateMachine
{
	public interface IMovableState
	{
		void SetMoveDirection(Vector2 direction);
	}
}