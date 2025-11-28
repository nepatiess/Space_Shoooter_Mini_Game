using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 8f; // H�z� art�rd�m
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

        // Rigidbody2D ayarlar�n� optimize et
        rb.gravityScale = 0f; // Yer�ekimini kapat
        rb.linearDamping = 0f; // Linear Drag'i s�f�rla (�nemli!)
        rb.angularDamping = 0f; // Angular Drag'i s�f�rla
        rb.constraints = RigidbodyConstraints2D.FreezeRotation; // D�nmeyi engelle

        // E�er firePoint atanmam��sa, gemi pozisyonunu kullan
        if (firePoint == null)
        {
            firePoint = transform;
        }
    }

    void Update()
    {
        HandleShooting();
    }

    void FixedUpdate() // Fizik i�lemleri i�in FixedUpdate kullan
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        // Input al (WASD veya Ok tu�lar�)
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        // Hareket vekt�r� olu�tur
        Vector2 movement = new Vector2(moveX, moveY).normalized;

        // Yeni pozisyon hesapla - FixedUpdate i�in Time.fixedDeltaTime kullan
        Vector2 newPosition = rb.position + movement * moveSpeed * Time.fixedDeltaTime;

        // S�n�rlar i�inde tut
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);

        // Pozisyonu g�ncelle
        rb.MovePosition(newPosition);
    }

    void HandleShooting()
    {
        // Space tu�u veya Sol Mouse tu�u
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
            Debug.LogWarning("Bullet Prefab atanmam��!");
        }
    }
}