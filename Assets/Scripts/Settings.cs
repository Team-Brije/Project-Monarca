using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public GameObject settingsMenu;
    public GameObject menu;
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

    public void SetRes(int resIndex)
    {
        Resolution resolution = resolutions[resIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    public void SetFS(bool isFS)
    {
        Screen.fullScreen = isFS;
    }

    public void SettingsMenu()
    {
        settingsMenu.SetActive(true);
        menu.SetActive(false);

    }
    public void ExitSettingsMenu()
    {
        menu.SetActive(true);
        settingsMenu.SetActive(false);
    }
    public void Play()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }
    public void Quit()
    {
        Application.Quit();
    }
}
