using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float totalGameTimeSeconds;
    private float gameTimeSeconds = 10;
    public float speed;
    public Text countText;
    public GameObject replayButton;
    public Text winText;
    public Text timerText;
    private Rigidbody2D body;
    private bool gameOver;
    private int pickedUpCount;

    private void Start()
    {
        replayButton.SetActive(false);
        body = GetComponent<Rigidbody2D>();
        gameTimeSeconds = totalGameTimeSeconds;
        pickedUpCount = 0;
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
                $"{(int) gameTimeSeconds / 60}:{gameTimeSeconds % 60:00.00}"
                : $"{gameTimeSeconds % 60:00.00}";

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
            if (pickedUpCount >= PickupsController.PickupCount)
            {
                gameOver = true;
                //The time ratio is doubled to make completing the task in 1/2 the time = 3 stars.
                int starCount = (int) Math.Round(gameTimeSeconds / totalGameTimeSeconds * 2.0 * 3.0);
                if (starCount > 3)
                    starCount = 3;
                winText.text = "YOU WIN!\n" + new string('\u2605', starCount);
                Cursor.visible = true;
            }
            else
            {
                winText.text = "YOU LOSE!";
                timerText.text = "0.00";
            }

            replayButton.SetActive(true);
            Cursor.visible = true;
            gameOver = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            
            pickedUpCount++;
            SetCountText();
        }
    }

    private void SetCountText()
    {
        int pickupCount = PickupsController.PickupCount;

        countText.text = $"{pickedUpCount} / {pickupCount}";

        if (pickedUpCount >= pickupCount)
            EndGame();
    }
}