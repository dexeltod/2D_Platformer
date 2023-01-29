using Infrastructure.Data;

public interface ISavedProgress
{
	void Save(PlayerProgress progress);
	void Load(PlayerProgress progress);
}