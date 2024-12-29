using UnityEngine;

public class Shopkeeper : MonoBehaviour
{
    [SerializeField] GameObject shopUI; // Assign in Inspector
    [SerializeField] float interactionDistance = 5f;
    [SerializeField] KeyCode interactionKey = KeyCode.E;
    [SerializeField] GameObject interactionIndicator; // Assign in Inspector

    private Transform player;
    private bool isPlayerNearby = false;
    [SerializeField] static bool isShopOpen = false; // Static variable to check if the shop is open

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (interactionIndicator != null)
        {
            interactionIndicator.SetActive(false); // Hide the indicator initially
        }
    }

    void Update()
    {
        if (player != null)
        {
            float distance = Vector3.Distance(transform.position, player.position);
            if (distance <= interactionDistance)
            {
                if (!isPlayerNearby)
                {
                    isPlayerNearby = true;
                    if (interactionIndicator != null)
                    {
                        interactionIndicator.SetActive(true); // Show the indicator
                    }
                }

                if (Input.GetKeyDown(interactionKey))
                {
                    OpenShop();
                }
            }
            else
            {
                if (isPlayerNearby)
                {
                    isPlayerNearby = false;
                    if (interactionIndicator != null)
                    {
                        interactionIndicator.SetActive(false); // Hide the indicator
                    }
                }
            }
        }
    }

    public void OpenShop()
    {
        shopUI.SetActive(true); // Open the shop UI
        isShopOpen = true;

        // Pause the game using the same method as the inventory
        if (Time.timeScale != 0f)
        {
            Time.timeScale = 0f;
        }
    }

    public void CloseShop()
    {
        shopUI.SetActive(false);
        isShopOpen = false;

        // Resume the game
        if (Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
        }
    }
}
