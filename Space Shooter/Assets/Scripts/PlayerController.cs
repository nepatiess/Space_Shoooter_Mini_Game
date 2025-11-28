using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 8f;
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

        // Rigidbody2D ayarlarýný optimize et
        rb.gravityScale = 0f; // Yerçekimini kapat
        rb.linearDamping = 0f; // Linear Drag'i sýfýrla (önemli!)
        rb.angularDamping = 0f; // Angular Drag'i sýfýrla
        rb.constraints = RigidbodyConstraints2D.FreezeRotation; // Dönmeyi engelle

        // Eðer firePoint atanmamýþsa, gemi pozisyonunu kullan
        if (firePoint == null)
        {
            firePoint = transform;
        }
    }

    void Update()
    {
        HandleShooting();
    }

    void FixedUpdate() // Fizik iþlemleri için FixedUpdate kullan
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        // Input al (WASD veya Ok tuþlarý)
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        // Hareket vektörü oluþtur
        Vector2 movement = new Vector2(moveX, moveY).normalized;

        // Yeni pozisyon hesapla - FixedUpdate için Time.fixedDeltaTime kullan
        Vector2 newPosition = rb.position + movement * moveSpeed * Time.fixedDeltaTime;

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
    }
}