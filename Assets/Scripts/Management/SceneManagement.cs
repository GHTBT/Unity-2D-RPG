using UnityEngine;

public class SceneManagement : Singleton<SceneManagement>
{
    public string ScenceTransitionName { get; private set; }

    public void SetTransitionName(string sceneTransitionName)
    {
        this.ScenceTransitionName = sceneTransitionName;
    }
}
