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
        
        SetCountText();
    }

    private void FixedUpdate()
    {
        float moveH = Input.GetAxis("Horizontal");
        float moveV = Input.GetAxis("Vertical");
        
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
    
    void SetCountText()
    {
        int pickupCount = pickups.GetComponent<PickupsController>().pickupCount;

        countText.text = "Count: " + count + " / " + pickupCount;

        if (count >= pickupCount)
        {
            winText.text = "You Win!";
        }
    }
}
