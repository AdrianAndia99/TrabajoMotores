using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public Rigidbody2D rb;
    public LayerMask collisionMask;
    private SpriteRenderer _playerSprite;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _playerSprite = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(horizontalInput, verticalInput) * speed;
        rb.velocity = movement;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, movement, 1f);
        if (hit.collider != null)
        {
            Debug.Log("Raycast colisionó con: " + hit.collider.gameObject.name);

            SpriteRenderer collidedSpriteRenderer = hit.collider.GetComponent<SpriteRenderer>();

            if (collidedSpriteRenderer != null)
            {
                _playerSprite.sprite = collidedSpriteRenderer.sprite;
            }
        }

        Debug.DrawRay(transform.position, movement * 1f, Color.red); 
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Shape")
        {
            // Cambiar la forma del jugador
            Debug.Log("Colisión con Shape");

            SpriteRenderer shapeSpriteRenderer = collision.gameObject.GetComponent<SpriteRenderer>();
            if (shapeSpriteRenderer != null)
            {
                _playerSprite.sprite = shapeSpriteRenderer.sprite;
            }
        }
        else if (collision.gameObject.tag == "Color")
        {
            Debug.Log("Colisión con Color");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Shape" || other.gameObject.tag == "Color")
        {
            
            SpriteRenderer spriteRenderer = other.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                _playerSprite.sprite = spriteRenderer.sprite;
            }

            if (other.gameObject.tag == "Shape")
            {
                Debug.Log("Sprite del objeto: " + spriteRenderer.sprite.name);
            }
            else if (other.gameObject.tag == "Color")
            {
                Debug.Log("Color del objeto: " + spriteRenderer.color);
            }

        }
    }
}