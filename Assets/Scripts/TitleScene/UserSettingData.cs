using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// アウトゲームで決めたユーザーのデータを保存する場所
/// </summary>
public class UserSettingData : MonoBehaviour
{
    //サバイバー陣営かどうか
    bool m_survivorCamp = true;
    //ユーザーのID
    int m_id = 0;
    //ユーザーロール
    string m_role = "ロール名";

    void Start()
    {
        //シーン遷移してもこのゲームオブジェクトが破壊されないようにする
        DontDestroyOnLoad(this);
    }

    void Update()
    {
        //Escが押された時
        if (Input.GetKey(KeyCode.Escape))
        {

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;//ゲームプレイ終了
#else
            Application.Quit();//ゲームプレイ終了
#endif
        }
    }

    //サバイバー陣営かどうかのプロパティ
    public bool GetSetIsSurvivorCamp
    {
        get { return m_survivorCamp; }
        set { m_survivorCamp = value; }
    }

    //ユーザーIDのプロパティ
    public int GetSetPlayerId
    {
        get { return m_id; }
        set { m_id = value; }
    }

    //ユーザーロールのプロパティ
    public string GetSetPlayerRole
    {
        get { return m_role; }
        set { m_role = value; }
    }
}