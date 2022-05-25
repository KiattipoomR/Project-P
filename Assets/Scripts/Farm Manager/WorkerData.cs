using UnityEngine;

namespace FarmManager
{
  [System.Serializable]
  [CreateAssetMenu(menuName = "Scriptable Object/Worker", fileName = "Worker_WorkerData")]
  public class WorkerData : ScriptableObject
  {
    [SerializeField] private string workerName;
    [SerializeField] private Sprite workerImage;
    [SerializeField] private int workerMaxStamina;
    [SerializeField] private int workerCost;

    public string WorkerName => workerName;
    public Sprite WorkerImage => workerImage;
    public int WorkerMaxStamina => workerMaxStamina;
    public int WorkerCost => workerCost;

    public static WorkerData GenerateRandomWorker()
    {
      WorkerData worker = ScriptableObject.CreateInstance<WorkerData>();
      worker.workerName = "Test_" + (char)('A' + Random.Range(0, 26));
      worker.workerMaxStamina = 100;
      worker.workerCost = 0;
      return worker;
    }
  }
}
