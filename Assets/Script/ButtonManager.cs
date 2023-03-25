using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public GameObject HelpPanel, HelpPanelItem1, HelpPanelItem2, HelpPanel_HowtoPlay1, HelpPanel_HowtoPlay2;
    public AudioClip PressButton;
    public GameObject PausePanel;

    public void StartAction()
    {
        SoundManager.instance.SFXPlay("DM-CGS-16", PressButton);
        SceneManager.LoadScene("Game");
        Time.timeScale = 1;
    }

    public void Title()
    {
        SoundManager.instance.SFXPlay("DM-CGS-16", PressButton);
        SceneManager.LoadScene("GameTitle");
        Time.timeScale = 1;
    }

    public void Resume()
    {
        SoundManager.instance.SFXPlay("DM-CGS-16", PressButton);
        PausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void Retry()
    {
        SoundManager.instance.SFXPlay("DM-CGS-16", PressButton);
        SceneManager.LoadScene("Game");
        Time.timeScale = 1;
    }

    public void OnHelpPanel()
    {
        SoundManager.instance.SFXPlay("DM-CGS-16", PressButton);
        HelpPanel.SetActive(true);
        Time.timeScale = 1;
    }

    public void BackHelpPanel()
    {
        SoundManager.instance.SFXPlay("DM-CGS-16", PressButton);
        HelpPanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void OnHelpPanel_Item()
    {
        SoundManager.instance.SFXPlay("DM-CGS-16", PressButton);
        HelpPanelItem1.SetActive(true);
        Time.timeScale = 1;
    }

    public void UnHelpPanel_Item()
    {
        SoundManager.instance.SFXPlay("DM-CGS-16", PressButton);
        HelpPanelItem1.SetActive(false);
        Time.timeScale = 1;
    }

    public void OnNextHelpPanel_Item2()
    {
        SoundManager.instance.SFXPlay("DM-CGS-16", PressButton);
        HelpPanelItem2.SetActive(true);
        Time.timeScale = 1;
    }

    public void BackNextHelpPanel_Item2()
    {
        SoundManager.instance.SFXPlay("DM-CGS-16", PressButton);
        HelpPanelItem2.SetActive(false);
        Time.timeScale = 1;
    }

    public void OnHelpPanel_HowtoPlay()
    {
        SoundManager.instance.SFXPlay("DM-CGS-16", PressButton);
        HelpPanel_HowtoPlay1.SetActive(true);
        Time.timeScale = 1;
    }

    public void UnHelpPanel_HowtoPlay()
    {
        SoundManager.instance.SFXPlay("DM-CGS-16", PressButton);
        HelpPanel_HowtoPlay1.SetActive(false);
        Time.timeScale = 1;
    }

    public void OnNextHelpPanel_HowtoPlay()
    {
        HelpPanel_HowtoPlay2.SetActive(true);
        Time.timeScale = 1;
    }

    public void UnNextHelpPanel_HowtoPlay()
    {
        HelpPanel_HowtoPlay2.SetActive(false);
        Time.timeScale = 1;
    }
}
