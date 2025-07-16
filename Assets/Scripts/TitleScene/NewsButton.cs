using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewsButton : MonoBehaviour
{
    [SerializeField] GameObject m_newsWindow = null;

    bool m_isWindowActive = false;

    //���m�点�{�^���������ꂽ�Ƃ��A
    public void OnClickNewsButton()
    {
        if (!m_isWindowActive)
        {
            //���m�点�E�B���h�E��\������
            m_newsWindow.SetActive(true);
        }
        else
        {
            //���m�点�E�B���h�E���\���ɂ���
            m_newsWindow.SetActive(false);
        }
        m_isWindowActive = !m_isWindowActive;
    }
}