using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public Text countText;
    public GameObject pickups;
    public Text winText;
    private Rigidbody2D body;
    private int count;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
        count = 0;
        Cursor.visible = false;
        
        SetCountText();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Cursor.visible = !Cursor.visible;
    }

    private void FixedUpdate()
    {
        float moveH = Input.GetAxis("Horizontal");
        float moveV = Input.GetAxis("Vertical");

        if (moveH > 0.2 || moveV > 0.2)
            Cursor.visible = false;
        
        body.AddForce(new Vector2(moveH, moveV) * speed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            
            count++;
            SetCountText();
        }
    }

    private void SetCountText()
    {
        int pickupCount = pickups.GetComponent<PickupsController>().pickupCount;

        countText.text = $"{count} / {pickupCount}";

        if (count >= pickupCount)
        {
            winText.text = "YOU WIN!";
            Cursor.visible = true;
        }
    }
}
