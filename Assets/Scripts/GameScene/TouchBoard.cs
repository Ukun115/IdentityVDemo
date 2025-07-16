using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchBoard : MonoBehaviourPunCallbacks
{
    //板が倒せる状態かどうか
    bool m_canBoardFalling = false;

    //板を倒し終わっているかどうか
    bool m_finishBoardFalling = false;

    //板を飛び越えれるかどうか
    bool m_isBoardJumpStart = false;

    //サバイバーとジャンプポイントとの距離
    float[] m_playerJumpPointLength = { 0.0f, 0.0f };

    //枠飛び越えポイント
    [SerializeField] GameObject[] m_frameJumpPoint = { null };
    //サバイバー
    [SerializeField] GameObject m_survivor = null;

    void Update()
    {
        //スペースキーが押されたとき、
        if (Input.GetKey(KeyCode.Space))
        {
            //板を倒せる状態なら、
            if (m_canBoardFalling)
            {
                //デバック
                Debug.Log("板を倒した");

                //板倒し開始処理
                photonView.RPC(nameof(BoardFallingStart), RpcTarget.All);

                //
                //サバイバーの位置をセットする
                //

                //ジャンプポイント0＆1とサバイバーの距離を取得
                for (int jumpPointNum = 0; jumpPointNum < 2; jumpPointNum++)
                {
                    m_playerJumpPointLength[jumpPointNum] = Vector3.Distance(m_frameJumpPoint[jumpPointNum].transform.position, m_survivor.transform.position);
                }
                //ジャンプポイント0のほうがジャンプポイント1よりも近かったら、
                if (m_playerJumpPointLength[0] < m_playerJumpPointLength[1])
                {
                    //サバイバーはジャンプポイント1に飛ばせる
                    m_survivor.transform.position = m_frameJumpPoint[0].transform.position;
                }
                else
                {
                    //サバイバーはジャンプポイント0に飛ばせる
                    m_survivor.transform.position = m_frameJumpPoint[1].transform.position;
                }
            }
            //板を倒し終わった状態だったら、
            //かつ、板を飛び越えられるとき、
            if (GameObject.Find("BoardModel").GetComponent<Board>().GetSetBoardState == 2&&m_isBoardJumpStart)
            {
                //板飛び越え処理

                //板を飛び越えられなくする
                m_isBoardJumpStart = false;

                //デバック
                Debug.Log("板を飛び越える");
            }
        }
    }

    //オブジェクトの領域に入ったら一度だけ呼ばれる処理
    void OnTriggerEnter(Collider other)
    {
        //板を倒し終わっているときは実行しない
        if(m_finishBoardFalling)
        {
            return;
        }

        //入ってきたオブジェクトがサバイバーだったら、
        if (other.gameObject.tag == "Survivor")
        {
            //板を倒せる状態にする
            m_canBoardFalling = true;

            //デバック
            Debug.Log("板倒し可能状態");
        }
    }

    //オブジェクトの領域から出たら一度だけ呼ばれる処理
    void OnTriggerExit(Collider other)
    {
        //板を倒し終わっているときは実行しない
        if (m_finishBoardFalling)
        {
            return;
        }

        //出たオブジェクトがサバイバーだったら、
        if (other.gameObject.tag == "Survivor")
        {
            //板を倒せない状態にする
            m_canBoardFalling = false;

            //デバック
            Debug.Log("板倒し不可能状態");
        }
    }

    //板を飛び越えれるかどうかを設定するセッター
    public void SetBoardJumpStart(bool isBoardJumpStart)
    {
        m_isBoardJumpStart = isBoardJumpStart;
    }

    //板倒し開始関数
    [PunRPC]
    void BoardFallingStart()
    {
        //板を倒すアニメーション状態に遷移
        GameObject.Find("BoardModel").GetComponent<Board>().GetSetBoardState = 1;
        //倒れた板の当たり判定をつけて通れなくする
        GameObject.Find("GuardCollider").GetComponent<BoxCollider>().isTrigger = false;
        //板を倒せない状態にする
        m_canBoardFalling = false;
        //板を倒し終わった判定にする
        m_finishBoardFalling = true;
    }
}