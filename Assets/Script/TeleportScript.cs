using UnityEngine;

public class TeleportPlayer2D : MonoBehaviour
{
    [Header("Set this to the player GameObject")]
    public GameObject player;

    [Header("Where to teleport the player")]
    public Transform teleportTarget;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            player.transform.position = teleportTarget.position;
        }
    }
}
