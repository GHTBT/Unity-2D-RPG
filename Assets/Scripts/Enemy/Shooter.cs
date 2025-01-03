using UnityEngine;

public class Shooter : MonoBehaviour, IEnemy
{
    [SerializeField] private GameObject bulletPrefab;

    public void Attack()
    {
        Vector2 targetDirection = Player_Controller.Instance.transform.position - transform.position;
        GameObject newBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        newBullet.transform.right = targetDirection;
    }
}