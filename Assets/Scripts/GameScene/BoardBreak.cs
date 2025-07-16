using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ハンターが板を破壊する処理
/// </summary>
public class BoardBreak : MonoBehaviour
{
    //板を破壊できるかどうか
    bool m_canBreak = false;

    //板を破壊している状態かどうか
    bool m_isBreak = false;

    //板破壊にかかる時間
    [SerializeField] int m_boardBreakTime = 0;
    float m_boardBreakTimer = 0.0f;

    //ハンターのゲームオブジェクト
    GameObject m_hunter = null;

    private void Update()
    {
        //タブキーが押されたとき、
        if (Input.GetKey(KeyCode.Tab)&& !m_isBreak)
        {
            //板を倒せる状態なら、
            if (m_canBreak)
            {
                //板破壊処理に移行
                m_isBreak = true;

                //デバック
                Debug.Log("板破壊開始");
                Debug.Log("かかる時間：" + m_boardBreakTime);

                //ハンターの動きを止める
                m_hunter.GetComponent<HunterMovement>().SetIsStop(true);
            }
        }

        //板を破壊しているときの処理
        if(m_isBreak)
        {
            //タイマーを経過させていく
            m_boardBreakTimer += 0.1f;

            //デバック
            Debug.Log(m_boardBreakTimer);

            //時間経過したら、
            if (m_boardBreakTimer > m_boardBreakTime)
            {
                //デバック
                Debug.Log("板破壊完了");

                //ハンターの動きを再生させる
                m_hunter.GetComponent<HunterMovement>().SetIsStop(false);

                //板オブジェクトを破壊
                Destroy(GameObject.Find("Board").gameObject);

            }
        }
    }

    //オブジェクトの領域に入ったら一度だけ呼ばれる処理
    void OnTriggerEnter(Collider other)
    {
        //このスクリプトが呼ばれていなかったら実行しない
        if(!this.enabled)
        {
            return;
        }

        //ハンターだったら、
        if (other.gameObject.tag == "Hunter")
        {
            //板を壊せる状態にする
            m_canBreak = true;

            m_hunter = other.gameObject;

            //デバック
            Debug.Log("板破壊可能状態");
        }
    }
    //オブジェクトの領域から出たら一度だけ呼ばれる処理
    void OnTriggerExit(Collider other)
    {
        //このスクリプトが呼ばれていなかったら実行しない
        if (!this.enabled)
        {
            return;
        }

        //ハンターだったら、
        if (other.gameObject.tag == "Hunter")
        {
            //板を壊せない状態にする
            m_canBreak = false;

            m_hunter = other.gameObject;

            //デバック
            Debug.Log("板破壊不可状態");
        }
    }
}