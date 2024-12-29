using System.Collections;
using UnityEngine;
using UnityEngine.Timeline;

public class Sword : MonoBehaviour, IWeapon
{
    [SerializeField] private GameObject slashAnimationPrefab;
    [SerializeField] private Transform slashAnimationSpawnPoint;
    //[SerializeField] private float swordAttackCD = 0.5f;
    [SerializeField] private WeaponInfo weaponInfo;

    private Transform weaponCollider;
    private Animator animator;
    private GameObject slashAnimation;

    private void Awake() 
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        weaponCollider = Player_Controller.Instance.GetWeaponCollider();
        slashAnimationSpawnPoint = GameObject.Find("SwordSlashSpawnPoint").transform;
    }

    private void Update()
    {
        MouseFollowWithOffset();
    }

    public WeaponInfo GetWeaponInfo()
    {
        return weaponInfo;
    }

    public void Attack()
    {
        animator.SetTrigger("Attack");
        weaponCollider.gameObject.SetActive(true);
        slashAnimation = Instantiate(slashAnimationPrefab,slashAnimationSpawnPoint.position, Quaternion.identity);
        slashAnimation.transform.parent = this.transform.parent;  
    }

    private void DoneAttackingAnimationEvent()
    {
        weaponCollider.gameObject.SetActive(false);
    }

    private void UpswingFlipAnimationEvent()
    {
        slashAnimation.gameObject.transform.rotation = Quaternion.Euler(-180,0,0);
        if(Player_Controller.Instance.FacingLeft)
        {
            slashAnimation.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    private void DownswingFlipAnimationEvent()
    {
        slashAnimation.gameObject.transform.rotation = Quaternion.Euler(0,0,0);
        if(Player_Controller.Instance.FacingLeft)
        {
            slashAnimation.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    private void MouseFollowWithOffset()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(Player_Controller.Instance.transform.position);

        float angle = Mathf.Atan2(mousePos.y,mousePos.x)*Mathf.Rad2Deg;

        if(mousePos.x < playerScreenPoint.x)
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0,-180,angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0,-180,0);
        }
        else
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0,0,angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0,0,0);
        }
    }
}
