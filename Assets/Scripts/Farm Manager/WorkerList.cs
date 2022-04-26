using System.Collections.Generic;
using UnityEngine;

namespace Worker
{
  public class WorkerList
  {
    private List<WorkerData> workers;
    private int maxCapacity;

    public List<WorkerData> Workers => workers;
    public int MaxCapacity => maxCapacity;

    public WorkerList(int maxCapacity)
    {
      workers = new List<WorkerData>();
      this.maxCapacity = maxCapacity;
    }

    public bool AddWorker(WorkerData worker)
    {
      if (workers.Count >= maxCapacity)
      {
        return false;
      }
      workers.Add(worker);
      return true;
    }

    public bool RemoveWorker(WorkerData worker)
    {
      int pos = workers.IndexOf(worker);
      if (pos == -1)
      {
        return false;
      }
      else
      {
        workers.RemoveAt(pos);
        return true;
      }
    }

    public void GenerateWorkerListToFull()
    {
      while (true)
      {
        if (!AddWorker(WorkerData.GenerateRandomWorker()))
        {
          break;
        }
      }
    }

    public void ClearWorkerList()
    {
      workers = new List<WorkerData>();
    }
  }
}