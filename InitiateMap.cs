using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitiateMap : MonoBehaviour
{
    public void Initiate() 
    {
        if (StaticData.SkipFactor == 0)
        {
            StaticData.SkipFactor = 1;
        }

        if (StaticData.MapPath != null)
        {
            SceneManager.LoadScene("MapScene"); 
        }
    }
}
