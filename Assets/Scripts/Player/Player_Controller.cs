using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Controller : Singleton<Player_Controller>
{
    public bool FacingLeft {get {return facingLeft;}}

    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float dashSpeed = 4f;
    [SerializeField] private TrailRenderer trailRenderer;
    [SerializeField] private Transform weaponCollider;

    private Player_Controls playerControls;
    private Vector2 movements;
    private Rigidbody2D rb;

    private Animator animator;

    private SpriteRenderer spriteRenderer;
    private Knockback knockback;
    private float startingMoveSpeed;
    private bool facingLeft = false;
    private bool isDashing = false;

    private void Start()
    {
        playerControls.Combat.Dash.performed += _ => Dash();
        startingMoveSpeed = moveSpeed;
        ActiveInventory.Instance.EquipStartingWeapon();
    }

    protected override void Awake() 
    {
        base.Awake();
        playerControls = new Player_Controls();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        knockback = GetComponent<Knockback>();
    }
    // Update is called once per frame
    void Update()
    {
        PlayerInput();
    }
    private void FixedUpdate() 
    {
        Move();
        AdjustPlayerDirection();
    }
    public Transform GetWeaponCollider()
    {
        return weaponCollider;
    }
    private void OnEnable() 
    {
        playerControls.Enable();
    }
    private void OnDisable() 
    {
        playerControls.Disable();
    }
    private void PlayerInput()
    {
        movements=playerControls.Movements.Move.ReadValue<Vector2>();
        
        animator.SetFloat("moveX",movements.x);
        animator.SetFloat("moveY",movements.y);
    }
    private void Move()
    {
        if(knockback.GettingKnockedBack || PlayerHealth.Instance.isDead)
        {
            return;
        }
        rb.MovePosition(rb.position + movements*(moveSpeed*Time.fixedDeltaTime));
    }
    private void AdjustPlayerDirection ()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);

        if(mousePos.x < playerScreenPoint.x)
        {
            spriteRenderer.flipX = true;
            facingLeft = true;
        }
        else
        {
            spriteRenderer.flipX = false;
            facingLeft = false;
        }
    }

    private void Dash()
    {
        if(!isDashing && Stamina.Instance.CurrentStamina > 0)
        {
            Stamina.Instance.UseStamina();
            isDashing = true;
            moveSpeed *= dashSpeed;
            trailRenderer.emitting = true;
            StartCoroutine(EndDashRoutine());
        }
    }

    private IEnumerator EndDashRoutine()
    {
        float dashTime = 0.2f;
        float dashCD = 0.25f;
        yield return new WaitForSeconds(dashTime);  
        moveSpeed = startingMoveSpeed;
        trailRenderer.emitting = false;
        yield return new WaitForSeconds(dashCD);
        isDashing = false;
    }
    
}
