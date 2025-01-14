using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    [SerializeField] private GameObject goldCoin, healthOrb, staminaOrb;

    public void DropItems(int goldAmount)
    {
        for(int i = 0; i < goldAmount; i++)
        {
            Instantiate(goldCoin, transform.position, Quaternion.identity);
        }
        int randomNum = Random.Range(1,4);
        if(randomNum == 1)
        {
            Instantiate(healthOrb, transform.position, Quaternion.identity);
        }
        if(randomNum == 2)
        {
            Instantiate(staminaOrb, transform.position, Quaternion.identity);
        }
    }
}
