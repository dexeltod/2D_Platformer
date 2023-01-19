using System;
using System.Collections.Generic;

namespace PlayerScripts.TestStateMachine
{
	public class StateService
	{
		private readonly Dictionary<Type, ITestState> _states = new();

		public void Register<T>(T instance) where T : class, ITestState
		{
			if (_states.ContainsKey(typeof(T)))
				throw new InvalidOperationException();
			
			_states.Add(typeof(T), instance);
		}

		public T Get<T>() where T : class, ITestState
		{
			if (_states.ContainsKey(typeof(T)) == false)
				throw new InvalidOperationException();

			return _states[typeof(T)] as T;
		}
	}
}