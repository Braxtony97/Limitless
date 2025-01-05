using UnityEngine;

namespace Limitless.Scripts.Gameplay.Root
{
    public class GameplayEntryPoint : MonoBehaviour
    {
        [SerializeField] private GameObject _gameRootBinder; // связь View и Model

        public void Run()
        {
            Debug.Log("Gameplay scene loaded");
        }
    }
}