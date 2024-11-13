using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MyUIHandler : MonoBehaviour
{
    // public float CurrentHealth = .5f;
    public static MyUIHandler instance { get; private set; }
    private VisualElement m_Healthbar;
    public float displayTime = 4.0f;
    private VisualElement m_NonPlayerDialgue;
    private float m_TimerDisplay;

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

        m_NonPlayerDialgue = uIDocument.rootVisualElement.Q<VisualElement>("NPCDialogue");
        m_NonPlayerDialgue.style.display = DisplayStyle.None;
        m_TimerDisplay = -1.0f;
    }

    public void SetHealthValue(float percentage)
    {
        m_Healthbar.style.width = Length.Percent(100 * percentage);
    }

    void Update()
    {
        if (m_TimerDisplay > 0)
        {
            m_TimerDisplay -= Time.deltaTime;
            if (m_TimerDisplay < 0)
            {
                m_NonPlayerDialgue.style.display = DisplayStyle.None;
            }
        }
    }

    public void DisplayDialogue()
    {
        m_NonPlayerDialgue.style.display = DisplayStyle.Flex;
        m_TimerDisplay = displayTime;
    }
}
