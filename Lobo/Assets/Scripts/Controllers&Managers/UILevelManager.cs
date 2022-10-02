using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UILevelManager : MonoBehaviour
{
    [SerializeField] TMP_InputField levelIF;
    int levelSetting;
    [SerializeField] TMP_InputField densityIF;
    int densitySetting;

    bool hasParsedLevelSetting;
    bool hasParsedDensitySetting;

    public void ReadLevelIF()
    {
        hasParsedLevelSetting = int.TryParse(levelIF.GetComponent<TMP_InputField>().text, out var result);
        if (hasParsedLevelSetting)
        {
            levelSetting = result;
        }
        else
        {
            Debug.LogError("The level setting will only accept integers from 1-9 as input");
        }
    }

    public void ReadDensityIF()
    {
        hasParsedDensitySetting = int.TryParse(densityIF.GetComponent<TMP_InputField>().text, out var result);
        if (hasParsedDensitySetting && result > 0 && result < 6)
        {
            densitySetting = result;
        }
        else
        {
            Debug.LogError("The density setting will only accept integers from 1-5 as input");
        }
    }

    public int GetLevelSetting() => levelSetting;
    public int GetDensitySetting() => densitySetting;
    public bool GetLevelSettingBool() => hasParsedLevelSetting;
    public bool GetDensitySettingBool() => hasParsedDensitySetting;
}
