using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickDecideButton : MonoBehaviour
{
    [SerializeField] GameObject m_realyOkBackImage = null;

    [SerializeField] GameObject[] m_decideButton = null;

    [SerializeField] GameObject m_randomNameDecideButton = null;

    [SerializeField] string m_selectCampName = "";

    //入力された名前
    [SerializeField] GameObject m_nameInputCharacter = null;

    //ボタンが押されたときに処理される
    //本当にこれでいいかの確認へ移る
    public void NextRealyOk()
    {
        //インプットフィールドに入力された名前がまだないときは最終確認テキストを表示させない。
        if(m_nameInputCharacter != null && m_nameInputCharacter.GetComponent<InputField>().text.Length == 0)
        {
            Debug.Log("名前が未入力です。");

            return;
        }

        m_realyOkBackImage.GetComponent<ClickRealyOkButton>().SetGetSelectCampName = m_selectCampName;

       //最終確認テキストを表示
        m_realyOkBackImage.SetActive(true);

       //決定ボタンをいったん非表示
       for (int i = 0; i < m_decideButton.Length; i++)
       {
           m_decideButton[i].SetActive(false);
       }

       if(m_randomNameDecideButton)
       {
            m_randomNameDecideButton.SetActive(false);
       }
    }
}