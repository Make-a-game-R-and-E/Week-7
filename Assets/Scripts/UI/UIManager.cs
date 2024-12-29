using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    [SerializeField] Text notificationText;

    [SerializeField] static UIManager instance;

    [SerializeField] Text messageText; // Assign this in the Inspector

    void Awake()
    {
        // Singleton pattern
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    IEnumerator ClearNotificationAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (notificationText != null)
        {
            notificationText.text = "";
        }
    }

    public void ShowMessage(string message, float duration = 2f)
    {
        StartCoroutine(ShowMessageRoutine(message, duration));
    }

    private IEnumerator ShowMessageRoutine(string message, float duration)
    {
        messageText.text = message;
        messageText.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(duration);
        messageText.gameObject.SetActive(false);
    }
}
