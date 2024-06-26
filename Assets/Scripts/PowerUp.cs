using UnityEngine;

public class PowerUp : MonoBehaviour
{
    private const string PLAYER_TAG = "Player";

    public enum Type
    {
        Coin,
        ExtraLife,
        MagicMushroom,
        Starpower,
    }

    public Type type;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(PLAYER_TAG))
        {
            Collect(other.gameObject);
        }
    }

    private void Collect(GameObject player)
    {
        switch (type)
        {
            case Type.Coin:
                GameManager.Instance.AddCoin();
                break;
            case Type.ExtraLife:
                GameManager.Instance.AddLife();
                break;
            case Type.MagicMushroom:
                player.GetComponent<PlayerManager>().Grow();
                break;
            case Type.Starpower:
                player.GetComponent<PlayerManager>().Starpower();
                break;
        }

        Destroy(gameObject);
    }
}
