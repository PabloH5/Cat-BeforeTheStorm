using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void ReloadScene()
    {
        SceneManager.LoadScene("GAMEPLAY");
    }
    public void GoToMenu()
    {
        SceneManager.LoadScene("MENU");
    }
}
