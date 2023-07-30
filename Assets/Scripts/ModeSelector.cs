using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ModeSelector : MonoBehaviour
{
    public static int Mode;
    public void OpenSampleScene()
    {
        Debug.Log("SampleScene");
        SceneManager.LoadScene("SampleScene");
    }
    public void OPenGameSpecificationScene()
    {
        Debug.Log("GameSpecification");
        SceneManager.LoadScene("GameSpecification");
    }
    public void Twoplayer()
    {
        Mode = 0;
        Debug.Log(Mode);
        Invoke("OpenSampleScene", 0.2f);
    }
    public void VsComputer()
    {
        Mode = 1;
        Debug.Log(Mode);
        Invoke("OPenGameSpecificationScene", 0.2f);
    }
}
