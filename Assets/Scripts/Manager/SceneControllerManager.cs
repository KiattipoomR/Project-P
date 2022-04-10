using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Manager
{
    public class SceneControllerManager : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private CanvasGroup faderCanvasGroup;
        [SerializeField] private Image fadeImage;

        [Header("Attributes")]
        [SerializeField] private float fadeDuration = 1f;
        [SerializeField] private string startingSceneName;

        public static UnityAction OnSceneFadedOut;
        public static UnityAction OnSceneUnloaded;
        public static UnityAction OnSceneLoaded;
        public static UnityAction OnSceneFadedIn;

        public string StartingSceneName => startingSceneName;

        private bool _isFading;

        private IEnumerator Start()
        {
            fadeImage.color = Color.black;
            faderCanvasGroup.alpha = 1f;

            yield return StartCoroutine(LoadSceneAndSetActive(startingSceneName));
            OnSceneLoaded?.Invoke();

            StartCoroutine(FadeScreen(0f));
        }

        public void ChangeScene(string destinationSceneName, Vector3 spawnPosition)
        {
            if (_isFading) return;

            StartCoroutine(FadeAndChangeScene(destinationSceneName, spawnPosition));
        }

        private IEnumerator FadeAndChangeScene(string sceneName, Vector3 spawnPosition)
        {
            OnSceneFadedOut?.Invoke();
            yield return StartCoroutine(FadeScreen(1f));

            Player.Player.Instance.gameObject.transform.position = spawnPosition;

            OnSceneUnloaded?.Invoke();
            yield return SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);

            yield return StartCoroutine(LoadSceneAndSetActive(sceneName));
            OnSceneLoaded?.Invoke();

            yield return StartCoroutine(FadeScreen(0f));
            OnSceneFadedIn?.Invoke();
        }

        private IEnumerator FadeScreen(float finalAlpha)
        {

            _isFading = true;
            faderCanvasGroup.blocksRaycasts = true;

            float fadeSpeed = Mathf.Abs(faderCanvasGroup.alpha - finalAlpha) / fadeDuration;
            while (!Mathf.Approximately(faderCanvasGroup.alpha, finalAlpha))
            {
                faderCanvasGroup.alpha =
                    Mathf.MoveTowards(faderCanvasGroup.alpha, finalAlpha, fadeSpeed * Time.deltaTime);

                yield return null;
            }

            _isFading = !_isFading;
            faderCanvasGroup.blocksRaycasts = false;
        }

        private IEnumerator LoadSceneAndSetActive(string sceneName)
        {
            yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

            UnityEngine.SceneManagement.Scene loadedScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
            SceneManager.SetActiveScene(loadedScene);
        }
    }
}