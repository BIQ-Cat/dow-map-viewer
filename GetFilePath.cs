using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetFilePath : MonoBehaviour
{
    public Text FilePathText;
    public InputField SkipFactorInputField;

    public void InitClick()
    {
        NativeFilePicker.Permission permission = NativeFilePicker.PickFile((path) =>
        {
            if (path != null)
            {   
                StaticData.MapPath = path;
                FilePathText.text = path;
            }
        }, new string[] { "*/*" });
    }
}
