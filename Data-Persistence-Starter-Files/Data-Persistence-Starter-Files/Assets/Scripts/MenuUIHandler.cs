using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[DefaultExecutionOrder(1000)]
public class MenuUIHandler : MonoBehaviour
{
    public TMP_InputField input;
    public TMP_Text textArea;
    
    public void Start()
    {
       textArea.text = "BestScore: " +RealMainManager.Instance.nome +"  "+ RealMainManager.Instance.score;
    }
    
    public void StartNew()
    {
        RealMainManager.Instance.nomeAttuale = input.text;
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        RealMainManager.Instance.saveScore();
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
    
    
    
}
