using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MyUIHandler : MonoBehaviour
{
    // public float CurrentHealth = .5f;
    public static MyUIHandler instance { get; private set; }
    private VisualElement m_Healthbar;
    // Start is called before the first frame update

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        UIDocument uIDocument = GetComponent<UIDocument>();
        m_Healthbar = uIDocument.rootVisualElement.Q<VisualElement>("HealthBar");
        // healthBar.style.width = Length.Percent(CurrentHealth * 100.0f);
        SetHealthValue(1.0f);
    }

    public void SetHealthValue(float percentage)
    {
        m_Healthbar.style.width = Length.Percent(100 * percentage);
    }

    // Update is called once per frame
    //     void Update()
    //     {

    //     }
}
