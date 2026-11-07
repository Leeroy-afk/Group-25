using System.Collections;
using UnityEngine;
using TMPro;

namespace Interaction
{
    public class NotificationDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI notificationText;
        [SerializeField] private float notificationDuration = 2f;


        public static NotificationDisplay Instance { get; private set; }

        private Coroutine notificationCoroutine;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
                return;
            }

            if (notificationText != null)
            {
                notificationText.gameObject.SetActive(false);
            }
        }

        public void ShowNotification(string message)
        {
            if (notificationText == null) return;

            if (notificationCoroutine != null)
            {
                StopCoroutine(notificationCoroutine);
            }

            notificationCoroutine = StartCoroutine(CoShowNotification(message));
        }

        private IEnumerator CoShowNotification(string message)
        {
            notificationText.text = message;
            notificationText.gameObject.SetActive(true);

            float elapsed = 0f;
            while (elapsed < notificationDuration)
            {
                elapsed += Time.unscaledDeltaTime;
                yield return null;
            }

            notificationText.gameObject.SetActive(false);
            notificationCoroutine = null;
        }
    }
}