using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapButton : MonoBehaviour
{
    public MapOnOff ShowMap;

    private void Start()
    {
        ShowMap = GetComponent<MapOnOff>();
    }

    public void OnOffButton()
    {
        if (ShowMap.OnOffStatus == true)
        {
            ShowMap.ONMap();
        }
        else
        {
            ShowMap.OFFMap();
        }
        
    }
}
