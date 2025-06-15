using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartDemoButtonBehaviour : MonoBehaviour
{
    public string className;

    public void StartDemo() {
        PersistantStaticData.playerClass = className;
        SceneManager.LoadSceneAsync("Demo_Level_1");
    }
}
