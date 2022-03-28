using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class OptionsMenu : MonoBehaviour
{

    public TMP_Dropdown resolutionsDropDown;
    List<TMP_Dropdown.OptionData> availableResolutions = new List<TMP_Dropdown.OptionData>();

    public TMP_Dropdown qualityDropDown;
    List<TMP_Dropdown.OptionData> availableQuality = new List<TMP_Dropdown.OptionData>();

    public TMP_Dropdown shadowDropdown;
    List<TMP_Dropdown.OptionData> availableShadow = new List<TMP_Dropdown.OptionData>();

    public void Initialize()
    {
        #region Resolutions
        availableResolutions = new List<TMP_Dropdown.OptionData>();
        foreach (Resolution res in Screen.resolutions){
            availableResolutions.Add(new TMP_Dropdown.OptionData($"{res.width}x{res.height} {res.refreshRate}hz"));
        }
        resolutionsDropDown.ClearOptions();
        resolutionsDropDown.AddOptions(availableResolutions);
        #endregion

        #region Quality
        availableQuality = new List<TMP_Dropdown.OptionData>();
        foreach(string s in QualitySettings.names)
        {
            availableQuality.Add(new TMP_Dropdown.OptionData($"{s}"));
        }
        qualityDropDown.ClearOptions();
        qualityDropDown.AddOptions(availableQuality);
        #endregion

        #region Shadows
        availableShadow = new List<TMP_Dropdown.OptionData>();
        foreach(string s in Enum.GetNames(typeof(ShadowResolution))){
            availableShadow.Add(new TMP_Dropdown.OptionData(s));
        }
        shadowDropdown.AddOptions(availableShadow);
        #endregion

        #region Set Selected Values
        // WIP
        #endregion
    }

    private void OnEnable()
    {
        Initialize();
    }

    public void SetResolution()
    {
        Resolution selectedRes = new Resolution();
        TMP_Dropdown.OptionData selectedOption = availableResolutions[resolutionsDropDown.value];
        string[] parameterSplit = selectedOption.text.Split(' ');
        string resolutionTxt = parameterSplit[0];          /*selectedOption.text.Substring(0, selectedOption.text.IndexOf(" "));*/
        string[] resolutionSplit = resolutionTxt.Split('x');
        selectedRes.width = int.Parse(resolutionSplit[0]);
        selectedRes.height = int.Parse(resolutionSplit[1]);

        string refreshRateTxt = parameterSplit[1];
        string refreshRate = refreshRateTxt.Substring(0, refreshRateTxt.Length - refreshRateTxt.IndexOf("hz"));
        selectedRes.refreshRate = int.Parse(refreshRate);

        Screen.SetResolution(selectedRes.width, selectedRes.height, FullScreenMode.FullScreenWindow, selectedRes.refreshRate);
    }

    public void SetQuality()
    {
        string selectedQuality = availableQuality[qualityDropDown.value].text;
        QualitySettings.SetQualityLevel(qualityDropDown.value);
    }

    public void SetShadowResolution()
    {
        Debug.Log((ShadowResolution)shadowDropdown.value);
        QualitySettings.shadowResolution = (ShadowResolution) shadowDropdown.value;
    }
}
