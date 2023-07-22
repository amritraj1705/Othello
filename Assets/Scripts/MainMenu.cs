using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void OPenMainMenu()
    {
        Debug.Log("MainMenu");
        SceneManager.LoadScene("MainMenu");
    }
    public void OPenGameSpecification()
    {
        Debug.Log("GameSpecification");
        SceneManager.LoadScene("GameSpecification");
    }
    public void OPenInstruction()
    {
        Debug.Log("Instructions");
        SceneManager.LoadScene("Instructions");
    }
    public void StartGame()
    {
        Invoke("OPenGameSpecification", 0.2f);
    }

    public void Instructions()
    {
        Invoke("OPenInstruction", 0.2f);
    }
    public void Exit()
    {
        Debug.Log("Quit");
        Application.Quit();

    }
    public void MainMenuOpen()
    {
        Invoke("OPenMainMenu", 0.2f);
    }
}
