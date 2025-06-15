using UnityEngine;
using UnityEngine.SceneManagement;

public static class BartraSceneUtils
{
    public static void RetryScene() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    public static void GoToScene(string sceneName) => SceneManager.LoadScene(sceneName);
}
