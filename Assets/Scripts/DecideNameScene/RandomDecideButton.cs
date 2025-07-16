using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ランダムで名前を決定する処理
/// </summary>
public class RandomDecideButton : MonoBehaviour
{
    //名前候補
    [SerializeField]string[] m_randomName = null;

    [SerializeField]InputField m_inputText = null;

    //ボタンが押されたらランダムで名前を蹴っている処理
    public void OnClickDecideRandomName()
    {
        //名前を表示
        m_inputText.text = m_randomName[Random.Range(0, m_randomName.Length)];

        //デバック
        Debug.Log("決定された名前：" + m_inputText.text);
    }
}