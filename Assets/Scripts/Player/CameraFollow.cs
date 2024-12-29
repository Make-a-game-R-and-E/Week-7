using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;  // The player (warrior) to follow
    public Vector3 offset;    // Offset between the camera and the player

    void Start()
    {
        // Find the player dynamically by their tag
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;  // Assign the player's transform
        }

        if (player != null)
        {
            offset = transform.position - player.position;
        }
    }

    void LateUpdate()
    {
        if (player != null)
        {
            // Smooth transition of the camera's movement
            transform.position = Vector3.Lerp(transform.position, player.position + offset, 0.1f);
        }
    }

}
