using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSpecification : MonoBehaviour
{
    public static int Gamediff;
    public void OpenSampleScene()
    {
        Debug.Log("SampleScene");
        SceneManager.LoadScene("SampleScene");
    }
    public void DiffEasy()
    {
        Gamediff = 0;
        Invoke("OpenSampleScene", 0.2f);
    }
    public void DiffMedium()
    {
        Gamediff = 1;
        Invoke("OpenSampleScene", 0.2f);
    }
    public void DiffHard()
    {
        Gamediff = 2;
        Invoke("OpenSampleScene", 0.2f);
    }
}
