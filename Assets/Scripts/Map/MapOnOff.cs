using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapOnOff : MonoBehaviour
{
    public GameObject OnOffMap;
    public Image MapBlind;

    public bool OnOffStatus;


    private void Start()
    {
        OFFMap();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.M) && OnOffStatus == true)
        {
            ONMap();
        }
        else if (Input.GetKeyDown(KeyCode.M) && OnOffStatus == false)
        {
            OFFMap();
        }
    }

    public void ONMap()
    {
        OnOffMap.SetActive(true);
        MapBlind.gameObject.SetActive(true);
        OnOffStatus = false;
    }

    public void OFFMap()
    {
        OnOffMap.SetActive(false);
        MapBlind.gameObject.SetActive(false);
        OnOffStatus = true;
    }
}
