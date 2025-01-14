using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class AreaExit : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    [SerializeField] private string sceneTransitionName;

    private float waitForLoadTime = 1f;
    private bool isEnemiesPresent = true;
    public GameObject[] enemies;

    private void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemies");
        if(enemies.Length == 0)
        {
            isEnemiesPresent = false;
        }

    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(!isEnemiesPresent)
        {
            if(other.gameObject.GetComponent<Player_Controller>())
                {
                    SceneManagement.Instance.SetTransitionName(sceneTransitionName);
                    UIFade.Instance.FadeToBlack();
                    StartCoroutine(LoadSceneRoutine());
                } 
        }   
    }

    private IEnumerator LoadSceneRoutine()
    {
        while(waitForLoadTime >= 0)
        {
            waitForLoadTime -= Time.deltaTime;
            yield return null;
        }
        SceneManager.LoadScene(sceneToLoad);
    }
}
