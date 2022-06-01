using Misc;

namespace Manager
{
  public class GameManager : Singleton<GameManager>
  {
    public DataManager dataManager;
    public PauseManager pauseManager;
    public SceneControllerManager sceneControllerManager;
    public TileManager tileManager;
    public TimeManager timeManager;
    public CurrencyManager currencyManager;
    public WorkerManager workerManager;
    public DialogueManager dialogueManager;
  }
}