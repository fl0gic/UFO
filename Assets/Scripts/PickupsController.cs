using UnityEngine;
using Random = System.Random;

public class PickupsController : MonoBehaviour
{
    public const int PickupCount = 15;
    public GameObject pickupPrefab;
    
    // Start is called before the first frame update
    private void Start()
    {
        Random rand = new Random();
        for (int i = 0; i < PickupCount; i++)
        {
            GameObject pickup = Instantiate(pickupPrefab, 
                new Vector3(rand.Next(-11, 11), rand.Next(-11, 11), 0), Quaternion.identity);
            pickup.transform.parent = transform;
        }
    }
}
