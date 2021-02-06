using UnityEngine;
using UnityEngine.SceneManagement;

public class UiLoader : MonoBehaviour
{
    void Awake() => LoadUi();

    void LoadUi()
    {
        if (!SceneManager.GetSceneByName("Ui").isLoaded)
            SceneManager.LoadScene("Ui", LoadSceneMode.Additive);
    }
}
