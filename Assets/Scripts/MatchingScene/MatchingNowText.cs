using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchingNowText : MonoBehaviour
{
    [SerializeField] Text m_matchingNowText = null;

    int m_timer = 0;

    void Update()
    {
        m_timer++;

        switch(m_timer)
        {
            case 15:
                m_matchingNowText.text = "マッチング中";
                break;
            case 30:
                m_matchingNowText.text = "マッチング中・";
                break;
            case 45:
                m_matchingNowText.text = "マッチング中・・";
                break;
            case 60:
                m_matchingNowText.text = "マッチング中・・・";
                break;
            case 75:
                m_timer = 14;
                break;
        }
    }
}
