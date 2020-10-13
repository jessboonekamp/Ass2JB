using System.Collections.Generic;

namespace TaM{
    interface ILevelHolder{
        void AddLevel(string name, int width, int height, string data);
        int LevelCount { get; }
        string CurrentLevelName { get; }
        int LevelWidth { get; }
        int LevelHeight { get; }
        List<string> LevelNames();
        void SetLevel(string name);
    }
}
