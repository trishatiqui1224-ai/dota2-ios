using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Dota.App
{
    public class MenuBootstrap : MonoBehaviour
    {
        private void Start()
        {
            // If Menu is loaded, transition to Map_dota (Scene 0)
            if (SceneManager.sceneCountInBuildSettings > 0)
            {
                SceneManager.LoadScene(0);
            }
        }
    }
}
