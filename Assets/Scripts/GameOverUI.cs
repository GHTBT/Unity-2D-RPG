using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreValueText;
    [SerializeField] private GameObject characterUI;
    private int score;
    private TMP_Text goldText;
    const string COIN_AMOUNT_TEXT = "Gold Amount Text";
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        goldText = GameObject.Find(COIN_AMOUNT_TEXT).GetComponent<TMP_Text>(); 
        PlayerHealth.OnPlayerDeath += ActivateGameObject;
        this.gameObject.SetActive(false);
    }
    private void OnDestroy() 
    {
        PlayerHealth.OnPlayerDeath -= ActivateGameObject;
        //characterUI.SetActive(false);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
    private void ActivateGameObject()
    {
        this.gameObject.SetActive(true);
        scoreValueText.text = goldText.text;
    }
}
