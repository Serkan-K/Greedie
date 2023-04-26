using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] GameObject present_cutscene;
    [SerializeField] AudioClip menu_soundtrack;
    [SerializeField] Slider sound_slider;
    [SerializeField] GameObject option_popup;


    private void Start()
    {
        Load_Audio();
        DontDestroyOnLoad(menu_soundtrack);
        Destroy(present_cutscene, 8f);
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







    public void Pop_up()
    {
        if (option_popup.activeInHierarchy == true)
        {
            option_popup.SetActive(false);
        }
        else
        {
            option_popup.SetActive(true);
        }
    }




//---------------------------Sound Option--------------------------------


    public void Set_audio(float sound_value)
    {
        AudioListener.volume = sound_value;
        Save_auido();
    }

    private void Save_auido()
    {
        PlayerPrefs.SetFloat("audio_Volume", AudioListener.volume);
    }

    private void Load_Audio()
    {
        if (PlayerPrefs.HasKey("audio_Volume"))
        {
        AudioListener.volume = PlayerPrefs.GetFloat("audio_Volume");
        sound_slider.value = PlayerPrefs.GetFloat("audio_Volume");
        }
        else
        {
            PlayerPrefs.SetFloat("audio_Volume", 0.5f);
            AudioListener.volume = PlayerPrefs.GetFloat("audio_Volume");
            sound_slider.value = PlayerPrefs.GetFloat("audio_Volume");
        }
        
    }


    public void OnApplicationQuit()
    {
        Application.Quit();
    }


}
