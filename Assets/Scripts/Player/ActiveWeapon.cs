using UnityEngine;

public class ActiveWeapon : Singleton<ActiveWeapon>
{
    public MonoBehaviour CurrentActiveWeapon { get; private set; }
    private Player_Controls playerControls;
    private bool attackButtonDown, isAttacking = false;

    protected override void Awake()
    {
        base.Awake();
        playerControls = new Player_Controls();
    }

    private  void OnEnable()
    {
        playerControls.Enable();
    }

    private void Start()
    {
        playerControls.Combat.Attack.started += _ => StartAttacking();
        playerControls.Combat.Attack.canceled += _ => StopAttacking();
    }

    private void Update()
    {
        Attack();
    }

    public  void NewWeapon(MonoBehaviour newWeapon)
    {
        CurrentActiveWeapon = newWeapon;
    }

    public void WeaponNull()
    {
        CurrentActiveWeapon = null;
    }

    public void ToggleIsAttacking(bool value)
    {
        isAttacking = value;
    }

    private void StartAttacking()
    {
        attackButtonDown = true;
    }

    private void StopAttacking()
    {
        attackButtonDown = false;
    }

    private void Attack()
    {
        if(attackButtonDown && !isAttacking)
        {
            isAttacking = true;
            (CurrentActiveWeapon as IWeapon).Attack();
        }
    }
}
