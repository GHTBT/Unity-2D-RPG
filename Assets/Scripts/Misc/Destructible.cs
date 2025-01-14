using UnityEngine;

public class Destructible : MonoBehaviour
{
    [SerializeField] private GameObject DestroyVFX;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.GetComponent<DamageSource>() || other.gameObject.GetComponent<Projectile>())
        {
            GetComponent<PickupSpawner>().DropItems(1);
            Instantiate(DestroyVFX, transform.position,Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
