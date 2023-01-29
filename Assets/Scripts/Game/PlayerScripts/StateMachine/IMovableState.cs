using UnityEngine;

namespace Game.PlayerScripts.TestStateMachine
{
	public interface IMovableState
	{
		void SetMoveDirection(Vector2 direction);
	}
}