using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class SettingsManager : MonoBehaviour
{
    [SerializeField]
    private PlayerSettings playerSet;
    [SerializeField]
    private Image volume, brightness; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        volume.fillAmount = (float)playerSet.volume / 10f;
        brightness.fillAmount = (float)playerSet.brightness / 10f;
    }
}
