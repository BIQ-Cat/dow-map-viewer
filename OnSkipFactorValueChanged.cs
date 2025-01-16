using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnSkipFactorValueChanged : MonoBehaviour
{
    public InputField SkipFactorInputField;

    void OnValueChanged()
    {
        if (!int.TryParse(SkipFactorInputField.text, out StaticData.SkipFactor))
        {
            StaticData.SkipFactor = 1; 
        }
    }
}
