using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

namespace UI.Custom_UI.scroll_buttons
{
    public class scroll_buttons : MonoBehaviour
    {
        public Action<int> actionBtClick;


        Button[] buttons;



        int ind_scene = -1;




        private void Buttons_Click()
        {
            ind_scene = buttons.Select((bt, index) => new { bt, index })
                .Where(item => item.bt.name == EventSystem.current.currentSelectedGameObject.name)
                .Select(item => item.index)
                .ToArray()[0]; 
                
            actionBtClick?.Invoke(ind_scene);
        }




        #region  Event handlers
        void Start()
        {
            buttons = GetComponentsInChildren<Button>();

            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].onClick.AddListener(Buttons_Click);
            }
        }

        void Update()
        {

        }
        #endregion
    }
}