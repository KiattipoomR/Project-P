using UnityEngine;
using UnityEngine.UI;

namespace Worker
{
  [System.Serializable]
  [CreateAssetMenu(menuName = "Scriptable Object/Worker", fileName = "Worker_WorkerData")]
  public class WorkerData : ScriptableObject
  {
    [SerializeField] private string workerName;
    [SerializeField] private Sprite workerImage;
    [SerializeField] private float workerStamina;
    [SerializeField] private int workerCost;

    public string WorkerName => workerName;
    public Sprite WorkerImage => workerImage;
    public float WorkerStamina => workerStamina;
    public int WorkerCost => workerCost;

    public static WorkerData GenerateRandomWorker()
    {
      WorkerData worker = ScriptableObject.CreateInstance<WorkerData>();
      worker.workerName = "Test_" + (char)('A' + Random.Range(0, 26));
      worker.workerStamina = Random.Range(0, 101);
      worker.workerCost = Random.Range(0, 11);
      return worker;
    }
  }
}
