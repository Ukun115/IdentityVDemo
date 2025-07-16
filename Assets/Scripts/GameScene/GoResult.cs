using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoResult : MonoBehaviour
{
    //リザルトに行けるかどうか
    [SerializeField]bool m_canGoResult = false;

    //このコンポーネントが取り付けられているのがハッチかどうか
    [SerializeField]bool m_isHatch = false;

    bool m_ehehe = false;

    void Update()
    {
        if (m_ehehe)
        {
            //ハッチの時はキー入力を入れる
            if (m_isHatch)
            {
                //スペースキーが押されていないとリザルトに行く処理は実行しない
                if (Input.GetKey(KeyCode.Space))
                {
                    //リザルト画面に遷移

                    //デバック
                    Debug.Log("リザルト画面に移行");

                    //今は仮でテキスト「Win」表示
                    GameObject.Find("ResultText").GetComponent<Text>().enabled = true;
                }
            }
            else
            {
                //リザルト画面に遷移

                //デバック
                Debug.Log("リザルト画面に移行");

                //今は仮でテキスト「Win」表示
                GameObject.Find("ResultText").GetComponent<Text>().enabled = true;
            }
        }
    }

    //オブジェクトの領域に入ったら一度だけ呼ばれる処理
    void OnTriggerEnter(Collider other)
    {
        //入ってきたオブジェクトがサバイバーだったら、
        if (other.gameObject.tag == "Survivor")
        {
            //リザルト画面に行けるとき、
            if (m_canGoResult)
            {
                m_ehehe = true;

                //ハッチの時、
                if (m_isHatch)
                {
                    //デバック
                    Debug.Log("ハッチ使用可能");
                }
                //ゲートの時、
                else
                {
                    //デバック
                    Debug.Log("リザルト遷移");
                }
            }
            //まだリザルト画面に行けないとき、
            else
            {
                //ハッチの時、
                if (m_isHatch)
                {
                    //デバック
                    Debug.Log("ハッチ使用不可");
                }
                //ゲートの時、
                else
                {
                    //デバック
                    Debug.Log("リザルト不可");
                }
            }
        }
    }

    //リザルトに行けるかどうかを設定するセッター
    public void SetCanGoResult(bool canGoResult)
    {
        m_canGoResult = canGoResult;
    }
}