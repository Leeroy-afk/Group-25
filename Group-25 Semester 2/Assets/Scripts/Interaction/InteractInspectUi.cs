using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


namespace Interaction
{
    public class InteractInspectUI : MonoBehaviour
    {
        public static InteractInspectUI Instance { get; private set; }

        [SerializeField] private GameObject panel;

        [SerializeField] private Image itemImage;

        [SerializeField] private TextMeshProUGUI titleText;

        [SerializeField] private TextMeshProUGUI descriptionText;

        private GameObject currentTargetObject;
        public bool IsInspecting { get; private set; } = false;

       
        private void Awake()
        {
            if (Instance == null) 
            { 
                Instance = this; 
                Debug.Log("InteractInspectUI initialized!"); 
            }

            else
            {
                Destroy(this);
            }
            if (panel != null) {panel.SetActive(false);}
        }

        private void OnDestroy()
        {
            if (Instance == this) Instance = null;
            Time.timeScale = 1f;
        }

        public void ShowInspection(string title, string description, Sprite sprite, GameObject targetObject)
        {
            currentTargetObject = targetObject;

            if (currentTargetObject != null)
            {
               Interactable interactable = currentTargetObject.GetComponent<Interactable>();
               if(interactable != null)
                {
                    interactable.OnFocusLost();
                }
            }

            if (titleText != null) { titleText.text = title; }
            if (descriptionText != null) { descriptionText.text = description; }
            if (itemImage != null) 
            { 
                itemImage.sprite = sprite;
                itemImage.gameObject.SetActive(sprite != null);
            }

            panel.SetActive(true);
            Time.timeScale = 0f;
            IsInspecting = true;
        }

         public void CloseInspection()
        {
            panel.SetActive(false);
            Time.timeScale = 1f;
            IsInspecting = false;

            if (currentTargetObject != null)
            {
                Destroy(currentTargetObject);
            }




        }




    }

}