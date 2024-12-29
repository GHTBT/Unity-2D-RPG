using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 20f;
    [SerializeField] private GameObject particleOnHitPrefabVFX;

    private WeaponInfo weaponInfo;
    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        MoveProjectile();
        DetectFireDistance();
    }

    public void UpdateWeaponInfo(WeaponInfo weaponInfo)
    {
        this.weaponInfo = weaponInfo;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
        Indestructible indestructible = other.GetComponent<Indestructible>();

        if(!other.isTrigger && (enemyHealth || indestructible))
        {
            Instantiate(particleOnHitPrefabVFX, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    private void DetectFireDistance()
    {
        if(Vector3.Distance(transform.position, startPosition) > weaponInfo.weaponRange)
        {
            Destroy(gameObject);
        }
    }

    private void MoveProjectile()
    {
        transform.Translate(Vector3.right*Time.deltaTime*moveSpeed);
    }
}