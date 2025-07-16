using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ClickRealyOkButton : MonoBehaviour
{
    //入力されたテキスト
    [SerializeField] Text[] m_inputText = null;

    //決定ボタン
    [SerializeField] GameObject[] m_decideButton = null;

    [SerializeField] GameObject m_realyOkBackImage = null;

    [SerializeField] GameObject m_randomNameDecideButton = null;

    //次のシーン名
    [SerializeField] string m_nextSceneName = "";

    string m_selectCampName = "";

    //はい
    public void Yes()
    {
        //設定したユーザー名を登録しておく
        for (int i = 0; i < m_inputText.Length; i++)
        {
            //プレイヤープレフスに登録
            PlayerPrefs.SetString("UserName",m_inputText[i].text.ToString());
            PlayerPrefs.Save();
        }

        if (m_selectCampName == "サバイバー")
        {
            //選ばれた陣営を保存
            GameObject.Find("UserSettingData").GetComponent<UserSettingData>().GetSetIsSurvivorCamp = true;
        }
        if (m_selectCampName == "ハンター")
        {
            //選ばれた陣営を保存
            GameObject.Find("UserSettingData").GetComponent<UserSettingData>().GetSetIsSurvivorCamp = false;
        }

        //シーンに遷移
        SceneManager.LoadScene(m_nextSceneName);
    }

    //いいえ
    public void No()
    {
        //最終確認ボタンを非表示
        m_realyOkBackImage.SetActive(false);

        //決定ボタンを表示
        for (int i = 0; i < m_decideButton.Length; i++)
        {
            m_decideButton[i].SetActive(true);
        }

        if(m_randomNameDecideButton)
        {
            m_randomNameDecideButton.SetActive(true);
        }
    }

    public string SetGetSelectCampName
    {
        set { m_selectCampName = value; }
        get { return m_selectCampName; }
    }
}
