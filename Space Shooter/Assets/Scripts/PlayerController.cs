using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    [SerializeField] private float minX = -8f;
    [SerializeField] private float maxX = 8f;
    [SerializeField] private float minY = -4f;
    [SerializeField] private float maxY = 4f;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireRate = 0.25f;

    private float nextFireTime = 0f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Eðer firePoint atanmamýþsa, gemi pozisyonunu kullan
        if (firePoint == null)
        {
            firePoint = transform;
        }
    }

    void Update()
    {
        HandleMovement();
        HandleShooting();
    }

    void HandleMovement()
    {
        // Input al (WASD veya Ok tuþlarý)
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        // Hareket vektörü oluþtur
        Vector2 movement = new Vector2(moveX, moveY).normalized;

        // Yeni pozisyon hesapla
        Vector2 newPosition = rb.position + movement * moveSpeed * Time.deltaTime;

        // Sýnýrlar içinde tut
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);

        // Pozisyonu güncelle
        rb.MovePosition(newPosition);
    }

    void HandleShooting()
    {
        // Space tuþu veya Sol Mouse tuþu
        if ((Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0)) && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        if (bulletPrefab != null)
        {
            Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Bullet Prefab atanmamýþ!");
        }
    }
}
