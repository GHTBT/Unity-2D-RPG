using UnityEngine;

public class ActiveInventory : MonoBehaviour
{
    private int activeSlotIndex = 0;
    private Player_Controls playerControls;

    private void Awake() 
    {
        playerControls = new Player_Controls();
    }

    private void Start() 
    {
        playerControls.Inventory.Keyboard.performed += ctx => ToggleActiveSlot((int)ctx.ReadValue<float>());
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void ToggleActiveSlot(int numValue)
    {
        ToggleActiveHighlight(numValue-1);
    }

    private  void ToggleActiveHighlight(int indexNum)
    {
        activeSlotIndex = indexNum;
        foreach (Transform inventorySlot in this.transform)
        {
            inventorySlot.GetChild(0).gameObject.SetActive(false);
        }
        this. transform.GetChild(indexNum).GetChild(0).gameObject.SetActive(true);
    }
}
