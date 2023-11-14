using System.Text;
using System.Text.Json;

namespace NetController
{
    [Serializable]
    public class JsonDictionary
    {
        public Dictionary<string, string> Dictionary { get; set; }
        private bool _isInit = false;

        void Init()
        {
            Dictionary ??= new Dictionary<string, string>();
            _isInit = true;
        }
        public T? Get<T> (string key)
        {
            if (!_isInit)
            {
                Init();
            }
            return JsonSerializer.Deserialize<T>(Dictionary[key]);
        }
        public void Add<T>(string key, T @object)
        {
            if (!_isInit)
            {
                Init();
            }
            Dictionary.Add(key, JsonSerializer.Serialize<T>(@object));
        }
    }
}
