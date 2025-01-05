using System.Collections;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private enum PickupType
    {
        GoldCoin,
        HealthOrb,
        StaminaOrb,
    }
    [SerializeField] private PickupType pickupType;
    [SerializeField] private float pickUpDistance = 5f;
    [SerializeField] private float accelerationRate = 0.2f;
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private AnimationCurve animationCurve;
    [SerializeField] private float heightY = 1.5f;
    [SerializeField] private float popDuration = 1f;

    private Vector3 moveDir;
    private Rigidbody2D rb;

    private void Awake() 
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start() 
    {
        StartCoroutine(AnimationCurveSpawnRoutine());
    }

    private void Update()
    {
        Vector3 playerPos = Player_Controller.Instance.transform.position;
        if(Vector3.Distance(transform.position, playerPos) < pickUpDistance)
        {
            moveDir = (playerPos - transform.position).normalized;
            moveSpeed += accelerationRate;
        }
        else
        {
            moveDir = Vector3.zero;
            moveSpeed = 0;
        }
    }

    private void FixedUpdate() 
    {
        rb.linearVelocity = moveDir*moveSpeed*Time.deltaTime; 
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.GetComponent<Player_Controller>())
        {
            DetectPickupType();
            Destroy(gameObject);
        }
    }

    private IEnumerator AnimationCurveSpawnRoutine()
    {
        Vector2 startPoint = transform.position;
        float randomX = transform.position.x + Random.Range(-2f,2f);
        float randomY = transform.position.y + Random.Range(-1f,1f);

        Vector2 endPoint = new Vector2(randomX, randomY);

        float timePassed =  0f;

        while(timePassed < popDuration)
        {
            timePassed += Time.deltaTime;
            float linearT = timePassed/popDuration;
            float heightT = animationCurve.Evaluate(linearT);
            float height = Mathf.Lerp(0f, heightY, heightT);

            transform.position = Vector2.Lerp(startPoint, endPoint, linearT) + new Vector2(0f, height);
            yield return null;
        }
    }

    private void DetectPickupType()
    {
        switch(pickupType)
        {
            case PickupType.GoldCoin:
                EconomyManager.Instance.UpdateCurrentGold();
                Debug.Log("Gold");
                break;
            case PickupType.HealthOrb:
                PlayerHealth.Instance.HealPlayer();
                Debug.Log("Health");
                break;
            case PickupType.StaminaOrb:
                Stamina.Instance.RefreshStamina();
                Debug.Log("Stamina");
                break;
        }
    }

}
