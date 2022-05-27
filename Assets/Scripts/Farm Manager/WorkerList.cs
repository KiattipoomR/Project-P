using System.Collections.Generic;
using UnityEngine;

namespace FarmManager
{
  public class WorkerList
  {
    private List<Worker> workers;
    private int maxCapacity;

    public List<Worker> Workers => workers;
    public int MaxCapacity => maxCapacity;

    public WorkerList(int maxCapacity)
    {
      this.workers = new List<Worker>();
      this.maxCapacity = maxCapacity;
    }

    public bool Contains(string workerId)
    {
      return workers.Find(s => s.WorkerId == workerId) != null;
    }

    public Worker GetWorkerById(string workerId)
    {
      return workers.Find(s => s.WorkerId == workerId);
    }

    public bool AddWorker(Worker worker)
    {
      if (workers.Count >= maxCapacity)
      {
        return false;
      }
      workers.Add(worker);
      return true;
    }

    public bool SetWorkerIsActive(string workerId, bool newIsActive)
    {
      int i = workers.FindIndex(s => s.WorkerId == workerId);
      if (i == -1)
      {
        return false;
      }
      else
      {
        workers[i].SetIsActive(newIsActive);
        return true;
      }
    }

    public bool RemoveWorker(string workerId)
    {
      int pos = workers.FindIndex(s => s.WorkerId == workerId);
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

    public void Work(int staminaUsed)
    {
      int n = workers.FindAll(s => s.IsActive).Count;
      foreach (Worker worker in workers)
      {
        if (worker.IsActive)
        {
          worker.SpendStamina((float)staminaUsed / n, true);
        }
        else
        {
          worker.RecoverStamina(10);
        }
      }
    }

    public void GenerateWorkerListToFull()
    {
      while (true)
      {
        if (!AddWorker(new Worker(WorkerData.GenerateRandomWorker())))
        {
          break;
        }
      }
    }

    public void ClearWorkerList()
    {
      workers = new List<Worker>();
    }
  }
}