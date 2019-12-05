using UnityEngine;
using Random = System.Random;

public class PickupsController : MonoBehaviour
{
    public int pickupCount;
    public GameObject pickupPrefab;
    
    // Start is called before the first frame update
    private void Start()
    {
        Random rand = new Random();
        
        for (int i = 0; i < pickupCount; i++)
        {
            GameObject pickup = Instantiate(pickupPrefab, new Vector3(rand.Next(-11, 11), rand.Next(-11, 11), 0), Quaternion.identity);
            pickup.transform.parent = transform;
        }
    }
}
