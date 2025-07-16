using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewsButton : MonoBehaviour
{
    [SerializeField] GameObject m_newsWindow = null;

    bool m_isWindowActive = false;

    //お知らせボタンが押されたとき、
    public void OnClickNewsButton()
    {
        if (!m_isWindowActive)
        {
            //お知らせウィンドウを表示する
            m_newsWindow.SetActive(true);
        }
        else
        {
            //お知らせウィンドウを非表示にする
            m_newsWindow.SetActive(false);
        }
        m_isWindowActive = !m_isWindowActive;
    }
}