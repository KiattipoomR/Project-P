using Misc;

namespace Manager
{
    public class GameManager: Singleton<GameManager>
    {
        public DataManager dataManager;
        public PauseManager pauseManager;
        public SceneControllerManager sceneControllerManager;
        public TimeManager timeManager;
    }
}