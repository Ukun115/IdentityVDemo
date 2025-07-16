using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 窓枠乗り越え処理
/// </summary>
public class OvercomingTheWindowFrame : MonoBehaviour
{
    //窓枠乗り越え可能かどうか判定
    bool m_canWindowFrameJump = false;

    //プレイヤーとジャンプポイントとの距離
    float[] m_playerJumpPointLength = { 0.0f,0.0f };

    //ジャンプ先の番号(数値はジャンプポイントの番号)
    int m_jumpNumber = 0;

    //窓枠の状態
    enum EnWindowFrameState
    {
        enJumpBefore,   //窓枠飛び越え前
        enJumping       //窓枠飛び越え中
    }
    EnWindowFrameState m_windowFrameStage = EnWindowFrameState.enJumpBefore;

    //枠飛び越えポイント
    [SerializeField]GameObject[] m_frameJumpPoint = { null };
    //プレイヤー
    GameObject m_player = null;

    //硬直待機タイマー
    int m_waitTimer = 0;

    //硬直時間
    [SerializeField]int m_initWaitTime = 0;
    int m_waitTime = 0;

    void Start()
    {
        //硬直時間の初期値を保存
        m_waitTime = m_initWaitTime;
    }

    void Update()
    {
        //窓枠の状態によって処理を変更
        switch (m_windowFrameStage)
        {
            //窓枠飛び越え前
            case EnWindowFrameState.enJumpBefore:
                //窓枠乗り越え可能のとき、
                if (m_canWindowFrameJump)
                {
                    //スペースキーが押されたとき、
                    //かつ、ダウン状態じゃないとき、
                    if (Input.GetKey(KeyCode.Space))
                    {
                        //サバイバーの時、
                        if (m_player.gameObject.tag == "Survivor")
                        {
                            //ダウン状態のときは実行しない
                            if(m_player.GetComponent<SurvivorMovement>().GetSetIsDownStatu)
                            {
                                return;
                            }
                        }

                        //どっちのジャンプポイントに行くか判定

                        //ジャンプポイント0＆1とプレイヤーの距離を取得
                        for (int jumpPointNum = 0; jumpPointNum < 2; jumpPointNum++)
                        {
                            m_playerJumpPointLength[jumpPointNum] = Vector3.Distance(m_frameJumpPoint[jumpPointNum].transform.position, m_player.transform.position);
                        }
                        //ジャンプポイント0のほうがジャンプポイント1よりも近かったら、
                        if (m_playerJumpPointLength[0] < m_playerJumpPointLength[1])
                        {
                            //プレイヤーはジャンプポイント1に飛ぶ
                            m_jumpNumber = 1;
                        }
                        else
                        {
                            //プレイヤーはジャンプポイント0に飛ぶ
                            m_jumpNumber = 0;
                        }

                        //プレイヤーの動きを止める
                        if (m_player.gameObject.tag == "Survivor")
                        {
                            m_player.GetComponent<SurvivorMovement>().SetIsStop(true);
                        }
                        if (m_player.gameObject.tag == "Hunter")
                        {
                            m_player.GetComponent<HunterMovement>().SetIsStop(true);
                        }

                        //プレイヤーの移動状態によって硬直時間を変更
                        //走り以外は硬直時間を長くする
                        if (m_player.gameObject.tag == "Survivor")
                        {
                            if (m_player.GetComponent<SurvivorMovement>().GetMovementStatu() != 1)
                            {
                                m_waitTime *= 2;

                                //デバック
                                Debug.Log("遅い飛び越え");
                            }
                            //走り移動状態の時、
                            else
                            {
                                m_waitTime = m_initWaitTime;

                                //デバック
                                Debug.Log("早い飛び越え");
                            }
                        }
                        if (m_player.gameObject.tag == "Hunter")
                        {
                            m_waitTime *= 3;

                            //デバック
                            Debug.Log("遅い飛び越え");
                        }

                        //飛び越え処理に移行
                        m_windowFrameStage = EnWindowFrameState.enJumping;
                    }
                }
                break;

            //窓枠飛び越え中
            case EnWindowFrameState.enJumping:

                //硬直待機タイマーを加算する
                m_waitTimer++;

                //飛び越え前の硬直
                if (m_waitTimer == m_waitTime)
                {
                    //デバック
                    Debug.Log(m_jumpNumber + "番に飛びます");
                    //プレイヤーをジャンプポイントにワープさせる
                    m_player.transform.position = m_frameJumpPoint[m_jumpNumber].transform.position;
                }

                //飛び越え後の硬直
                if (m_waitTimer > m_waitTime*2)
                {
                    //窓枠飛び越え前の処理に戻る
                    m_windowFrameStage = EnWindowFrameState.enJumpBefore;
                    //硬直待機タイマーを初期化
                    m_waitTimer = 0;
                    //硬直時間を初期化
                    m_waitTime = m_initWaitTime;

                    //プレイヤーの動きを再開させる
                    if (m_player.gameObject.tag == "Survivor")
                    {
                        m_player.GetComponent<SurvivorMovement>().SetIsStop(false);
                    }
                    if(m_player.gameObject.tag == "Hunter")
                    {
                        m_player.GetComponent<HunterMovement>().SetIsStop(false);
                    }
                }

                break;
        }
    }

    //オブジェクトの領域に入ったら一度だけ呼ばれる処理
    void OnTriggerEnter(Collider other)
    {
        //このスクリプトが呼ばれていなかったら実行しない
        if (!this.enabled)
        {
            return;
        }

        //サバイバーorハンターだったら、
        if (other.gameObject.tag == "Survivor"|| other.gameObject.tag == "Hunter")
        {
            //窓枠乗り越え可能状態にする
            m_canWindowFrameJump = true;

            m_player = other.gameObject;

            //デバック
            Debug.Log("窓枠乗り越え可能状態");
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

        //サバイバーorハンターだったら、
        if (other.gameObject.tag == "Survivor" || other.gameObject.tag == "Hunter")
        {
            //窓枠乗り越え不可状態にする
            m_canWindowFrameJump = false;

            m_player = other.gameObject;

            //デバック
            Debug.Log("窓枠乗り越え不可状態");
        }
    }
}