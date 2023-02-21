namespace Infrastructure.GameLoading
{
	public interface ISceneLoad : ISceneLoadInformer
	{
		void InvokeSceneLoaded();
	}
}