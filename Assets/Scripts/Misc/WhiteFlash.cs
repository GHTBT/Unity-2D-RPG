using System.Collections;
using UnityEngine;

public class WhiteFlash : MonoBehaviour
{
    [SerializeField] private Material whiteFlashMaterial;
    [SerializeField] private float restoreDefaultMaterialTime = 0.2f;

    private Material defaultMaterial;
    private SpriteRenderer spriteRenderer;

    private void Awake() 
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultMaterial = spriteRenderer.material;       
    }

    public float GetRestoreMaterialTime()
    {
        return restoreDefaultMaterialTime;
    }
    public IEnumerator FlashRoutine()
    {
        spriteRenderer.material = whiteFlashMaterial;
        yield return new WaitForSeconds(restoreDefaultMaterialTime);
        spriteRenderer.material = defaultMaterial;
    }
}
