using System.Collections;
using System.Collections.Generic;
using TMPro;
using UI.Custom_UI.scroll_buttons;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI.MainMenu
{
    public class manager_mainmenu : MonoBehaviour
    {
        [Header("Panels")]
        [SerializeField] GameObject panel_main;
        [SerializeField] GameObject panel_loading;

        [Header("Object to load")]
        [SerializeField] TMP_Text text_loading;
        [SerializeField] Image img_loading;

        [Header("Internal obj in scene")]
        [SerializeField] scroll_buttons scroll_ind_scene;


        private int ind_scene_clicked;



        #region Buttons methods

        public void Button_StartGamae()
        {
            StartCoroutine(LoadScene_Routine(ind_scene_clicked));

            panel_main.SetActive(false);
            panel_loading.SetActive(true);
        }
        public void Button_Exit()
        {
            Application.Quit();
        }

        #endregion

        
        IEnumerator LoadScene_Routine(int ind_scene)
        {
            yield return null;

            // AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("Scene3");
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(ind_scene);
            
            asyncOperation.allowSceneActivation = false;
            Debug.Log("Pro :" + asyncOperation.progress);
            
            while (!asyncOperation.isDone)
            {
                text_loading.text = "Loading progress: " + (asyncOperation.progress * 100f) + "%";
                img_loading.fillAmount = asyncOperation.progress;

                if (asyncOperation.progress >= 0.9f)
                {
                    text_loading.text = "Press the space bar to continue";
                    
                    if (Input.GetKeyDown(KeyCode.Space))
                        asyncOperation.allowSceneActivation = true;
                }

                yield return null;
            }
        }



        #region Event handlers
        void Start()
        {
            panel_main.SetActive(true);
            panel_loading.SetActive(false);

            scroll_ind_scene.actionBtClick += OnIndSceneChange;
        }

        void Update()
        {

        }

        void OnIndSceneChange(int ind)
        {
            ind_scene_clicked = ind;
            Debug.Log(ind);
        }

        // void OnDestroy()
        // {
        //     scroll_ind_scene.actionBtClick -= OnIndSceneChange;
        // }

        #endregion
    }
}