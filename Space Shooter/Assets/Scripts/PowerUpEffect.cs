using UnityEngine;

public class PowerUpEffect : MonoBehaviour
{
    private PlayerController playerController;

    private bool rapidFireActive = false;
    private float rapidFireTimer = 0f;
    private float originalFireRate;
    [SerializeField] private float rapidFireMultiplier = 0.5f;

    void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        // Rapid Fire zamanlayýcý
        if (rapidFireActive)
        {
            rapidFireTimer -= Time.deltaTime;

            if (rapidFireTimer <= 0f)
            {
                DeactivateRapidFire();
            }
        }
    }

    public void ActivateRapidFire(float duration)
    {
        if (playerController == null) return;

        rapidFireActive = true;
        rapidFireTimer = duration;

        // Fire rate'i düþür (daha hýzlý ateþ)
        // Not: Bu kýsmý PlayerController'da public bir metod ile yapmalýsýnýz
        Debug.Log("Rapid Fire aktif! Süre: " + duration);
    }

    void DeactivateRapidFire()
    {
        rapidFireActive = false;

        // Fire rate'i normale döndür
        Debug.Log("Rapid Fire bitti!");
    }

    public bool IsRapidFireActive()
    {
        return rapidFireActive;
    }
}
