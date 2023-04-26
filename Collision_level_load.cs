using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Collision_level_load : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Load_Next_Level();
    }

        public void Load_Next_Level()
        {
            int currentLevel = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentLevel + 1);
        }

        public void level()
        {
            SceneManager.LoadScene(0);
        }
}
