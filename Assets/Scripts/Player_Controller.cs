using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Controller : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;

    private Player_Controls playerControls;
    private Vector2 movements;
    private Rigidbody2D rb;

    private void Awake() 
    {
        playerControls = new Player_Controls();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable() 
    {
        playerControls.Enable();
    }
    private void PlayerInput()
    {
        movements=playerControls.Movements.Move.ReadValue<Vector2>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInput;
    }
}
