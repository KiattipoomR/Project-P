using UnityEngine;

public class GamePause : MonoBehaviour
{
    [SerializeField] bool paused = false;

    public static GamePause instance;

    void Awake()
    {
        if (instance == null) {
            instance = this;
        }
        else {
            Destroy(this);
        }
        SetPause(false);
    }

    public void SetPause(bool p) {
        paused = p;
        Time.timeScale = paused ? 0f : 1f;
    }

    public bool TogglePause() {
        SetPause(!paused);
        return paused;
    }

    public bool GetPause() {
        return paused;
    }
}
