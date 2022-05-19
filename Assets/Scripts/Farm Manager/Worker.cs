using System;

namespace FarmManager
{
  public class Worker
  {
    private string workerId;
    private WorkerData workerData;
    private int workerStamina;
    private bool isActive;
    private int overworkCounter;

    public string WorkerId => workerId;
    public WorkerData WorkerData => workerData;
    public int WorkerStamina => workerStamina;
    public bool IsActive => isActive;

    public Worker(WorkerData workerData)
    {
      this.workerId = Guid.NewGuid().ToString();
      this.workerData = workerData;
      this.workerStamina = this.workerData.WorkerMaxStamina;
      this.isActive = true;
      this.overworkCounter = 0;
    }

    public bool RecoverStamina(int n)
    {
      if (workerStamina >= workerData.WorkerMaxStamina)
      {
        return false;
      }
      workerStamina += n;
      if (workerStamina > workerData.WorkerMaxStamina)
      {
        workerStamina = workerData.WorkerMaxStamina;
      }
      return true;
    }

    public bool SpendStamina(int n, bool overworkPossible)
    {
      if (workerStamina < n && !overworkPossible)
      {
        return false;
      }
      workerStamina -= n;
      if (workerStamina < 0)
      {
        workerStamina = 0;
        overworkCounter++;
      }
      return true;
    }

    public void SetIsActive(bool newIsActive)
    {
      this.isActive = newIsActive;
    }
  }
}