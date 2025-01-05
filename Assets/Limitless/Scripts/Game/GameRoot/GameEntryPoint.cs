using System.Collections;
using Limitless.Scripts.Gameplay.Root;
using Limitless.Scripts.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Limitless.Scripts
{
    public class GameEntryPoint
    {
        private static GameEntryPoint _instance;
        private Coroutines _coroutines;
        private UIRootView _uiRoot;
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void AutostartGame()
        {
            Application.targetFrameRate = 60;
            Screen.sleepTimeout = SleepTimeout.NeverSleep; // Экран не будет гаснуть при бездействии
            
            _instance = new GameEntryPoint();
            _instance.RunGame();
        }

        private GameEntryPoint()
        {
            _coroutines = new GameObject("[COROUTINES]").AddComponent<Coroutines>();
            Object.DontDestroyOnLoad(_coroutines);  
            
            var prefabUIRoot = Resources.Load<UIRootView>("UIRoot");
            _uiRoot = Object.Instantiate(prefabUIRoot);
            Object.DontDestroyOnLoad(_uiRoot);
        }

        private void RunGame()
        {
#if UNITY_EDITOR
            var sceneName = SceneManager.GetActiveScene().name;

            if (sceneName == ScenesNames.GAMEPLAY)
            {
                _coroutines.StartCoroutine(LoadGameplayCoroutine());
                
                return;
            }

            if (sceneName != ScenesNames.BOOT)
            {
                return;
            }
#endif

            _coroutines.StartCoroutine(LoadGameplayCoroutine());
        }
        
        private IEnumerator LoadGameplayCoroutine()
        {
            _uiRoot.ShowLoadingScreen();
            
            yield return LoadScene(ScenesNames.BOOT);
            yield return LoadScene(ScenesNames.GAMEPLAY);

            yield return new WaitForSeconds(2);

            var sceneEntryPoint = Object.FindFirstObjectByType<GameplayEntryPoint>();
            sceneEntryPoint.Run();
            
            _uiRoot.HideLoadingScreen();
        }

        private IEnumerator LoadScene(string sceneName)
        {
            yield return SceneManager.LoadSceneAsync(sceneName);
        }
    }
}