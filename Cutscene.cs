using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cutscene : MonoBehaviour
{
    public float delay;
    public string scene_Name;

    private void Update()
    {
        delay -= Time.deltaTime;

        if (delay <= 0)
        {
            SceneManager.LoadScene(scene_Name);
        }
    }

}
