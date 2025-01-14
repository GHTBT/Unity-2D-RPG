using System.Collections;
using UnityEngine;

public class BossHP : MonoBehaviour
{
    [SerializeField] private int startingHealth = 100;
    [SerializeField] private GameObject deathVFXPrefab;
    [SerializeField] private float knockBackThrust = 3f;

    private int currentHealth;
    private Knockback knockback;
    private WhiteFlash flash;
    private Animator animator;

    private void Awake()
    {
        flash = GetComponent<WhiteFlash>();
        knockback = GetComponent<Knockback>();
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        currentHealth = startingHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        knockback.GetKnockedBack(Player_Controller.Instance.transform, knockBackThrust);
        StartCoroutine(flash.FlashRoutine());
        StartCoroutine(CheckDetectDeathRoutine());
        if (currentHealth <= 50)
		{
			animator.SetBool("isEnraged", true);
		}
    }

    private IEnumerator CheckDetectDeathRoutine()
    {
        yield return new WaitForSeconds(flash.GetRestoreMaterialTime());
        DetectDeath();
    }
    public void DetectDeath()
    {
        if(currentHealth<=0)
        {
            //Instantiate(deathVFXPrefab, transform.position, Quaternion.identity);
            animator.SetBool("isDead", true);
            GetComponent<PickupSpawner>().DropItems(20);            
        }
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
