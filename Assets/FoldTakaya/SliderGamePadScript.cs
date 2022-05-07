using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class SliderGamePadScript : MonoBehaviour
{
    public TextMeshProUGUI TMP_value;
    Slider m_slider;
    float maxValue;
    float minValue;
    float sliderRange;
    float sliderChange;
    const float SLIDERSTEP = 500.0f;

    void Start()
    {
        m_slider = GetComponent<Slider>();
        maxValue = m_slider.maxValue;
        minValue = m_slider.minValue;
        sliderRange = maxValue - minValue;
        if(PlayerScriptNew.me != null)
            m_slider.value = PlayerScriptNew.me.rot_spd;
    }

    
    void Update()
    {
        if(gameObject == EventSystem.current.currentSelectedGameObject)
        {
            sliderChange = Input.GetAxis("HorizontalArrow") * sliderRange / SLIDERSTEP;
            float sliderValue = m_slider.value;
            float tempValue = sliderValue + sliderChange;
            if(tempValue <= maxValue && tempValue >= minValue)
            {
                sliderValue = tempValue;
            }
            else if(tempValue > maxValue)
            {
                sliderValue = maxValue;
            }
            else if(tempValue < minValue)
            {
                sliderValue = minValue;
            }
            m_slider.value = sliderValue;
            TMP_value.text = (Mathf.Round(m_slider.value * 100f) * 0.01f).ToString();
        }
    }

    public void syncPlayerRotSpd()
    {
        PlayerScriptNew.me.rot_spd = m_slider.value;
    }

    public void syncDataStorage()
    {
        StartSceneDataStorage.me.rotateSpd_startscene = m_slider.value;
    }
}
