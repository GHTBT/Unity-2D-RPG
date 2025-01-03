using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private float knockBackThrustAmount = 3f;
    [SerializeField] private float damageRecoveryTime = 1f;

    private int currentHealth;
    private bool canTakeDamage = true;
    private Knockback knockback;
    private WhiteFlash flash;

    private void Awake()
    {
        flash = GetComponent<WhiteFlash>(); 
        knockback = GetComponent<Knockback>();
    }

    private void Start() 
    {
        currentHealth = maxHealth;
    }

    private void OnCollisionStay2D(Collision2D other) 
    {
        EnemyAI enemy = other.gameObject.GetComponent<EnemyAI>();
        if(enemy)
        {
            TakeDamage(1, other.transform);
            knockback.GetKnockedBack(other.gameObject.transform, knockBackThrustAmount);
            StartCoroutine(flash.FlashRoutine());
        }
    }

    public void TakeDamage(int damageAmount, Transform hitTransform)
    {
        if(!canTakeDamage)
        {
            return;
        }

        ScreenShakeManagement.Instance.ShakeScreen();
        knockback.GetKnockedBack(hitTransform, knockBackThrustAmount);
        StartCoroutine(flash.FlashRoutine());
        canTakeDamage = false;
        currentHealth -= damageAmount;
        StartCoroutine(DamageRecoveryRoutine());
    }

    private IEnumerator DamageRecoveryRoutine()
    {
        yield return new WaitForSeconds(damageRecoveryTime);
        canTakeDamage = true;
    }
        
    
}
