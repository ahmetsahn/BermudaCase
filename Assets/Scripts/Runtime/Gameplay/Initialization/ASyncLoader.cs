using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Debug = System.Diagnostics.Debug;

namespace Runtime.Gameplay.Initialization
{
    public class ASyncLoader : MonoBehaviour
    {
        [SerializeField]
        private Image loadingBar;

        private const string SCENE_NAME = "Game";

        private void Start()
        {
            LoadLevelAsync().Forget();
        }

        private async UniTask LoadLevelAsync()
        {
            AsyncOperation loadOperation = SceneManager.LoadSceneAsync(SCENE_NAME);

            Debug.Assert(loadOperation != null, nameof(loadOperation) + " != null");
            
            while (!loadOperation.isDone)
            {
                float progress = Mathf.Clamp01(loadOperation.progress / 0.9f);
                UpdateLoadingBar(progress);
                await UniTask.Yield();
            }
            
            UpdateLoadingBar(1.0f);
        }

        private void UpdateLoadingBar(float progress)
        {
            if (loadingBar != null)
            {
                loadingBar.fillAmount = progress;
            }
        }
    }
}