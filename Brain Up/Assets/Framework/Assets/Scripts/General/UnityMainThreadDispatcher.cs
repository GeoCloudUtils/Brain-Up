/*
 Author: Unknown
 Edited: Ghercioglo Roman
 */

using System.Collections;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using Assets.Scripts.Framework.Other;


public class UnityMainThreadDispatcher : Singleton<UnityMainThreadDispatcher>
{
	private static readonly Queue<Action> _executionQueue = new Queue<Action>();

	public new void Awake()
	{
		base.Awake();
		Instance.enabled = true;
	}

	public void Update()
	{
		lock (_executionQueue)
		{
			while (_executionQueue.Count > 0)
			{
				_executionQueue.Dequeue().Invoke();
			}
		}
	}

	/// <summary>
	/// Locks the queue and adds the IEnumerator to the queue
	/// </summary>
	/// <param name="action">IEnumerator function that will be executed from the main thread.</param>
	public void Enqueue(IEnumerator action)
	{
		lock (_executionQueue)
		{
			_executionQueue.Enqueue(() =>
			{
				StartCoroutine(action);
			});
		}
	}

	/// <summary>
	/// Locks the queue and adds the Action to the queue
	/// </summary>
	/// <param name="action">function that will be executed from the main thread.</param>
	public void Enqueue(Action action)
	{
		Enqueue(ActionWrapper(action));
	}

	public void Enqueue<T>(Action<T> action, T p1)
	{
		Enqueue(ActionWrapper(action, p1));
	}

	public void Enqueue<T, V>(Action<T, V> action, T p1, V p2)
	{
		Enqueue(ActionWrapper(action, p1, p2));
	}


	/// <summary>
	/// Locks the queue and adds the Action to the queue, returning a Task which is completed when the action completes
	/// </summary>
	/// <param name="action">function that will be executed from the main thread.</param>
	/// <returns>A Task that can be awaited until the action completes</returns>
	public Task EnqueueAsync(Action action)
	{
		var tcs = new TaskCompletionSource<bool>();

		void WrappedAction()
		{
			try
			{
				action();
				tcs.TrySetResult(true);
			}
			catch (Exception ex)
			{
				tcs.TrySetException(ex);
			}
		}

		Enqueue(ActionWrapper(WrappedAction));
		return tcs.Task;
	}


	IEnumerator ActionWrapper(Action a)
	{
		a();
		yield return null;
	}

	IEnumerator ActionWrapper<T>(Action<T> a, T param)
	{
		a(param);
		yield return null;
	}

	IEnumerator ActionWrapper<T, V>(Action<T, V> a, T param, V param2)
	{
		a(param, param2);
		yield return null;
	}
}
