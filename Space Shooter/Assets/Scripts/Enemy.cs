using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int health = 1;
    [SerializeField] private int scoreValue = 10;
    [SerializeField] private float moveSpeed = 3f;

    [SerializeField] private GameObject enemyBulletPrefab;
    [SerializeField] private float fireRate = 2f;
    [SerializeField] private bool canShoot = false;

    [SerializeField] private GameObject explosionPrefab;

    private float nextFireTime = 0f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = Vector2.down * moveSpeed;
    }

    void Update()
    {
        // Ekrandan çýktýysa yok et
        if (transform.position.y < -6f || transform.position.x < -10f || transform.position.x > 10f)
        {
            Destroy(gameObject);
        }

        // Ateþ et (eðer izin varsa)
        if (canShoot && Time.time >= nextFireTime && enemyBulletPrefab != null)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        Instantiate(enemyBulletPrefab, transform.position, Quaternion.identity);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Patlama efekti oluþtur
        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }

        /*DÜZENLENECEK
        // Skoru artýr
        if (GameManager.Instance != null)
        {
            GameManager.Instance.AddScore(scoreValue);
        }
        */

        // Düþmaný yok et
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Oyuncuya çarptýysa
        if (collision.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(1);
            }

            Die();
        }
    }
}
