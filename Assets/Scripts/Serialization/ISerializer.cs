public interface ISerializer
{
    void Serialize(string path, GameData gameData);
    T Deserialize<T>(string path);
}
