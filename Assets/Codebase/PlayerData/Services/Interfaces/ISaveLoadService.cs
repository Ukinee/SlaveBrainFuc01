namespace Codebase.PlayerData.Services.Interfaces
{
    public interface ISaveLoadService
    {
        public void Save(string key, string value);
        public string Load(string key);
        public void Delete(string key);
        public bool HasSave(string key);
    }
}
