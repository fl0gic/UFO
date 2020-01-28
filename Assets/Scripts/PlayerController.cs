using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float gameTimeSeconds;
    public float speed;
    public Text countText;
    public GameObject pickups;
    public Text winText;
    public Text timerText;
    private Rigidbody2D body;
    private bool gameOver;
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
        if (gameTimeSeconds <= 0.00)
            EndGame();
        else
        {
            if (body.velocity != Vector2.zero)
                gameTimeSeconds -= Time.deltaTime;
            
            timerText.text = gameTimeSeconds > 60 ?
                $"{(int) gameTimeSeconds / 60}:{gameTimeSeconds % 60:0.00}"
                : $"{gameTimeSeconds % 60:0.00}";

            if (Input.GetKeyDown(KeyCode.Escape))
                Cursor.visible = !Cursor.visible;
        }
    }

    private void FixedUpdate()
    {
        if (gameOver)
        {
            body.velocity = Vector2.zero;
            return;
        }
        
        float moveH = Input.GetAxis("Horizontal");
        float moveV = Input.GetAxis("Vertical");

        if (moveH > 0.2 || moveV > 0.2)
            Cursor.visible = false;
        
        body.AddForce(new Vector2(moveH, moveV) * speed);
    }

    private void EndGame()
    {
        if (!gameOver)
        {
            winText.text = "YOU LOSE!";
            timerText.text = "0.00";
            Cursor.visible = true;
            gameOver = true;
        }
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
            gameOver = true;
            winText.text = "YOU WIN!";
            Cursor.visible = true;
        }
    }
}