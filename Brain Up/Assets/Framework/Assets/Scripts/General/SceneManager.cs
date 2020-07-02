/*
 
Author: Ghercioglo Roman (Romeon0)

*/

using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using manager = UnityEngine.SceneManagement.SceneManager;

namespace Assets.Scripts.Framework.Other
{
    public class SceneManager: Singleton<SceneManager>
    {
        public GameObject blockScreen;

        private void Start()
        {
            blockScreen?.SetActive(false);
        }

        #region API__

        public async void LoadScene(string sceneName, Action callback = null, bool dontBlockScreen=true, bool unloadPrevious = true)
        {
            //Debug.Log("Loading scene "+sceneName+"...");

            if (!dontBlockScreen)
                blockScreen.SetActive(true);

            try
            {
                //Load scene
                AsyncOperation operation = manager.LoadSceneAsync(sceneName, unloadPrevious? LoadSceneMode.Single : LoadSceneMode.Additive);
               
                //Wait until loaded
                while (operation!=null && !operation.isDone)
                    await Task.Yield();

                //Call given callback
                if (operation != null && operation.isDone)
                    callback?.Invoke();
            }
            catch (Exception ex)
            {
                Debug.LogError("LoadScene failed. Error: " + ex.Message);
            }


            if (!dontBlockScreen)
                blockScreen.SetActive(false);

            //Debug.Log("Loading scene end.");
        }

        public async void UnloadScene(string sceneName)
        {
            //Debug.Log("Unloading scene...");
            try
            {
                //Unload scene
                AsyncOperation operation = manager.UnloadSceneAsync(sceneName);

                //Wait until unload
                while (operation!=null && !operation.isDone)
                    await Task.Yield();
            }
            catch (Exception ex)
            {
                Debug.LogError("UnloadScene failed. Error: " + ex.Message);
            }
            //Debug.Log("Unloading scene end.");
        }

        public void UnloadCurrentScene()
        {
            UnloadScene(manager.GetActiveScene().name);
        }

        #endregion
    }
}
