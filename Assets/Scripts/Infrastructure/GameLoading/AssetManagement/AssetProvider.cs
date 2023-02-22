﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Infrastructure.GameLoading.AssetManagement
{
	public class AssetProvider : IAssetProvider
	{
		private readonly Dictionary<string, AsyncOperationHandle> _completedCache =
			new Dictionary<string, AsyncOperationHandle>();

		private readonly Dictionary<string, List<AsyncOperationHandle>> _handles =
			new Dictionary<string, List<AsyncOperationHandle>>();

		public void CleanUp()
		{
			foreach (List<AsyncOperationHandle> resourceHandles in _handles.Values)
			{
				foreach (var handle in resourceHandles)
					Addressables.Release(handle);
			}
			
			_completedCache.Clear();
			_handles.Clear();
		}

		public async Task<T> LoadAsync<T>(string address) where T : class
		{
			if (_completedCache.TryGetValue(address, out AsyncOperationHandle completedHandle))
				return completedHandle.Result as T;

			return await RunWithCacheOnComplete(address, Addressables.LoadAssetAsync<T>(address));
		}

		public async Task<T> LoadAsync<T>(AssetReference reference) where T : class
		{
			if (_completedCache.TryGetValue(reference.AssetGUID, out AsyncOperationHandle completedHandle))
				return completedHandle.Result as T;

			return await RunWithCacheOnComplete(reference.AssetGUID, Addressables.LoadAssetAsync<T>(reference));
		}

		public Task<GameObject> Instantiate(string path) =>
			Addressables.InstantiateAsync(path).Task;

		public Task<GameObject> Instantiate(string path, Vector3 position) =>
			Addressables.InstantiateAsync(path, position, Quaternion.identity).Task;

		private async Task<T> RunWithCacheOnComplete<T>(string cacheKey, AsyncOperationHandle<T> handle) where T : class
		{
			handle.Completed += completeHandle => _completedCache[cacheKey] = completeHandle;
			AddHandle(cacheKey, handle);
			return await handle.Task;
		}

		private void AddHandle<T>(string cacheKey, AsyncOperationHandle<T> handle) where T : class
		{
			if (!_handles.TryGetValue(cacheKey, out List<AsyncOperationHandle> resourceHandles))
			{
				resourceHandles = new List<AsyncOperationHandle>();
				_handles[cacheKey] = resourceHandles;
			}

			resourceHandles.Add(handle);
		}
	}
}