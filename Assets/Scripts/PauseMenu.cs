using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject settingsMenu;
    public bool menuON;

    public AudioMixer music;
    public AudioMixer ambient;
    Resolution[] resolutions;
    public TMP_Dropdown resDropdown;

    void Start()
    {
        resolutions = Screen.resolutions;

        resDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentRes = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentRes = i;
            }
        }
        resDropdown.AddOptions(options);
        resDropdown.value = currentRes;
        resDropdown.RefreshShownValue();
    }

    private void Update()
    {

        if (Input.GetKeyUp(KeyCode.Escape) && !menuON)
        {
            pauseMenu.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            menuON = true;
        }
        else if (Input.GetKeyUp(KeyCode.Escape) && menuON)
        {
            Resume();
        }
    }
    public void SetRes(int resIndex)
    {
        Resolution resolution = resolutions[resIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    public void SetFS(bool isFS)
    {
        Screen.fullScreen = isFS;
    }

    public void SetMusicVolume(float volume)
    {
        //Debug.Log(volume);
        music.SetFloat("Volume",volume);
    }
    public void SetAmbientVolume(float volume)
    {
        //Debug.Log(volume);
        ambient.SetFloat("Volume", volume);
    }

    public void OpenSettings()
    {
        settingsMenu.SetActive(true);
    }
    public void CloseSettings()
    {
        settingsMenu.SetActive(false);
    }
    public void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(false);
        menuON = false;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
