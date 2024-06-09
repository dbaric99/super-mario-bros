using UnityEngine;

public class DeathBarrier : MonoBehaviour
{
    private const string PLAYER_TAG = "Player";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(PLAYER_TAG))
        {
            other.gameObject.SetActive(false);
            GameManager.Instance.PlayerDeath(3f);
        }
        else
        {
            Destroy(other.gameObject);
        }
    }
}
