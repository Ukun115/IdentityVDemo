using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

/// <summary>
/// タイトル画面がタップされたら陣営選択するシーンへ遷移
/// </summary>
public class TitleTap : MonoBehaviour
{
    //名前決めシーンに遷移するかどうか判定
    bool m_isGoDecideNameScene = false;

    //リセットボタン
    [SerializeField]GameObject m_resetButtonObject = null;

    void Start()
    {
        //FirstPlayのキーが存在しない場合はシーン遷移先を名前決めシーンに行くようにする
        if(!PlayerPrefs.HasKey("FirstPlay"))
        {
            m_isGoDecideNameScene = true;
            //FirstPlayのキーに値を入れることで、二度とこのネスト内を実行しないようにする
            PlayerPrefs.SetInt("FirstPlay", 1);
        }

        //FPSを30に固定
        Application.targetFrameRate = 30;
        //ウィンドウサイズを設定
        //(フルスクリーンの1/4サイズでウィンドウ表示)
        Screen.SetResolution(1920/4, 1080/4, false, 60);
    }

    void Update()
    {
        //リセットボタンが押されたときは画面タップ判定を行わない
        //EventSystem.current.IsPointerOverGameObject()はボタンがタップされたらtrueを返す関数
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        //画面タップされたとき、
        if (Input.GetMouseButtonDown(0))
        {
            //ゲーム初回プレイ時、
            if (m_isGoDecideNameScene)
            {
                //名前決めシーンに遷移
                SceneManager.LoadScene("DecideNameScene");
            }
            //ゲームプレイ１回目以降
            else
            {
                //陣営決めシーンに遷移
                SceneManager.LoadScene("CampSelectScene");
            }
        }
    }

    //ボタンを押したらデータを初期化する関数
    public void OnClickDataReset()
    {
        m_isGoDecideNameScene = true;
        //FirstPlayのキーに値を入れることで、二度とこのネスト内を実行しないようにする
        PlayerPrefs.SetInt("FirstPlay", 1);

        //１回ボタン押したらボタンを押して２回目押されないようにする。
        m_resetButtonObject.SetActive(false);

        //デバック
        Debug.Log("データをリセットしました。");
    }
}