﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Infrastructure.Services.AssetManagement
{
	[Serializable]
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

		public async UniTask<T> LoadAsyncByGUID<T>(string address) where T : class
		{
			if (_completedCache.TryGetValue(address, out AsyncOperationHandle completedHandle))
				return completedHandle.Result as T;

			var result = await RunWithCacheOnComplete(address, Addressables.LoadAssetAsync<T>(address));
			return result;
		}

		public async UniTask<T> LoadAsync<T>(string address) where T : class
		{
			if (address == string.Empty)
				return null;

			if (_completedCache.TryGetValue(address, out AsyncOperationHandle completedHandle))
				return completedHandle.Result as T;

			var result = await RunWithCacheOnComplete(address, Addressables.LoadAssetAsync<T>(address));
			return result;
		}

		public async Task<T> LoadAsync<T>(AssetReference reference) where T : class
		{
			if (_completedCache.TryGetValue(reference.AssetGUID, out AsyncOperationHandle completedHandle))
				return completedHandle.Result as T;

			var result = await RunWithCacheOnComplete(reference.AssetGUID, Addressables.LoadAssetAsync<T>(reference));
			return result;
		}

		public async UniTask<T> LoadAsyncWithoutCash<T>(string address) where T : class
		{
			var result = await Addressables.LoadAssetAsync<T>(address);
			return result;
		}

		public async UniTask<GameObject> InstantiateNoCash(string path, Vector3 position)
		{
			if (path == string.Empty)
				return default;

			if (_completedCache.TryGetValue(path, out AsyncOperationHandle completedHandle))
				return (GameObject)completedHandle.Result;

			var result = await RunWithCacheOnComplete(path,
				Addressables.InstantiateAsync(path, position, Quaternion.identity));
			
			return result;
		}

		public UniTask<GameObject> Instantiate(string path) =>
			Addressables.InstantiateAsync(path).ToUniTask();

		public UniTask<GameObject> Instantiate(string path, Vector3 position) =>
			Addressables.InstantiateAsync(path, position, Quaternion.identity).ToUniTask();

		private async Task<T> RunWithCacheOnComplete<T>(string cacheKey, AsyncOperationHandle<T> handle) where T : class
		{
			handle.Completed += completeHandle => _completedCache[cacheKey] = completeHandle;
			AddHandle(cacheKey, handle);
			var result = await handle.Task;
			return result;
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