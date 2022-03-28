using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Linq;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{

    public TMP_Dropdown resolutionsDropDown;
    List<TMP_Dropdown.OptionData> resolutionOptions = new List<TMP_Dropdown.OptionData>();

    public TMP_Dropdown qualityDropDown;
    List<TMP_Dropdown.OptionData> qualityOptions = new List<TMP_Dropdown.OptionData>();

    public TMP_Dropdown shadowDropDown;
    List<TMP_Dropdown.OptionData> shadowOptions = new List<TMP_Dropdown.OptionData>();

    //public Toggle windowedToggle;

    public void Awake()
    {
        #region Resolutions
        resolutionOptions = new List<TMP_Dropdown.OptionData>();
        foreach (Resolution res in Screen.resolutions){
            resolutionOptions.Add(new TMP_Dropdown.OptionData($"{res.width}x{res.height} {res.refreshRate}hz"));
        }
        resolutionsDropDown.ClearOptions();
        resolutionsDropDown.AddOptions(resolutionOptions);
        #endregion

        #region Quality
        qualityOptions = new List<TMP_Dropdown.OptionData>();
        foreach(string s in QualitySettings.names)
        {
            qualityOptions.Add(new TMP_Dropdown.OptionData($"{s}"));
        }
        qualityDropDown.ClearOptions();
        qualityDropDown.AddOptions(qualityOptions);
        #endregion

        #region Shadows
        shadowOptions = new List<TMP_Dropdown.OptionData>();
        foreach(string s in Enum.GetNames(typeof(ShadowResolution))){
            shadowOptions.Add(new TMP_Dropdown.OptionData(s));
        }
        shadowDropDown.AddOptions(shadowOptions);
        #endregion

        #region Set Selected Values
        resolutionsDropDown.SetValueWithoutNotify(Screen.resolutions.ToList().IndexOf(Screen.currentResolution));
        qualityDropDown.SetValueWithoutNotify(QualitySettings.GetQualityLevel());
        #endregion
    }

    public void SetResolution()
    {
        Resolution selectedRes = new Resolution();
        TMP_Dropdown.OptionData selectedOption = resolutionOptions[resolutionsDropDown.value];
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
        string selectedQuality = qualityOptions[qualityDropDown.value].text;
        QualitySettings.SetQualityLevel(qualityDropDown.value);
    }

    public void SetShadowResolution()
    {
        Debug.Log((ShadowResolution)shadowDropDown.value);
        QualitySettings.shadowResolution = (ShadowResolution) shadowDropDown.value;
    }

    /*
    public void ToggleWindowed()
    {
        if (windowedToggle.isOn)
        {
            Screen.fullScreenMode = FullScreenMode.Windowed;
        } else
        {
            Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
        }
    }
    */
}
