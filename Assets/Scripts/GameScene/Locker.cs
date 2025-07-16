using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ロッカーの入退処理
public class Locker : MonoBehaviour
{
    //入室可能かどうか
    bool m_canEnter = false;

    //状態
    enum EnLockerStatu
    {
        enEnter,             //入る状態
        enEnterPlayerStop,   //入るのにプレイヤーを少し止めている状態
        enIn,                //入っている状態
        enExitPlayerStop,    //出た時に外でプレイヤーを少し止めている状態
    }
    EnLockerStatu m_lockerStatu = EnLockerStatu.enEnter;

    //連続して出入りしないように遅延をかけるタイマー
    int m_delayTimer = 60;
    //遅延時間
    int m_delayTime = 72;

    void Update()
    {
        switch (m_lockerStatu)
        {
            //入る状態
            case EnLockerStatu.enEnter:

                //遅延タイマーを計測する
                if (m_delayTimer < m_delayTime)
                {
                    m_delayTimer++;
                }

                //入室可能な時、
                if (m_canEnter && m_delayTimer == m_delayTime)
                {
                    //スペースキーが押されたら、
                    if (Input.GetKey(KeyCode.Space))
                    {
                        //ステートチェンジ
                        m_lockerStatu = EnLockerStatu.enEnterPlayerStop;

                        //プレイヤーの動きを止める
                        GameObject.Find("Survivor").GetComponent<SurvivorMovement>().SetIsStop(true);

                        //遅延タイマーを初期化
                        m_delayTimer = 0;

                        //デバック
                        Debug.Log("ロッカーに入室");
                    }
                }
                break;

            //入るのにプレイヤーを少し止めている状態
            case EnLockerStatu.enEnterPlayerStop:
                //遅延タイマーを計測する
                m_delayTimer++;
                if (m_delayTimer > m_delayTime)
                {
                    //ロッカーの中に入る

                    //Enterポイントにプレイヤーを飛ばす
                    GameObject.Find("Survivor").transform.position = GameObject.Find("EnterPoint").transform.position;

                    //ステートチェンジ
                    m_lockerStatu = EnLockerStatu.enIn;
                    //遅延タイマーを初期化
                    m_delayTimer = 0;
                }
                break;

            //入っている状態
            case EnLockerStatu.enIn:

                //遅延タイマーを計測する
                if (m_delayTimer < m_delayTime)
                {
                    m_delayTimer++;
                }

                if (m_delayTimer == m_delayTime)
                {
                    //スペースキーが押されたら、
                    if (Input.GetKey(KeyCode.Space))
                    {
                        //ロッカーから出る

                        //ステートチェンジ
                        m_lockerStatu = EnLockerStatu.enExitPlayerStop;

                        //Exitポイントにプレイヤーを飛ばす
                        GameObject.Find("Survivor").transform.position = GameObject.Find("ExitPoint").transform.position;

                        //遅延タイマーを初期化
                        m_delayTimer = 0;

                        //デバック
                        Debug.Log("ロッカーから出た");
                    }
                }
                break;

            //出た時に外でプレイヤーを少し止めている状態
            case EnLockerStatu.enExitPlayerStop:
                //遅延タイマーを計測する
                m_delayTimer++;
                if (m_delayTimer > m_delayTime)
                {
                    //ステートチェンジ
                    m_lockerStatu = EnLockerStatu.enEnter;
                    //遅延タイマーを初期化
                    m_delayTimer = 0;

                    //プレイヤーの動きを再開させる
                    GameObject.Find("Survivor").GetComponent<SurvivorMovement>().SetIsStop(false);
                }
                    break;
        }
    }

    //オブジェクトの領域に入ったら一度だけ呼ばれる処理
    void OnTriggerEnter(Collider other)
    {
        //サバイバーだったら、
        if (other.gameObject.tag == "Survivor")
        {
            //デバック
            Debug.Log("ロッカー入出可能");

            //入室可能にする
            m_canEnter = true;
        }
    }

    //オブジェクトの領域から出たら一度だけ呼ばれる処理
    void OnTriggerExit(Collider other)
    {
        //サバイバーだったら、
        if (other.gameObject.tag == "Survivor")
        {
            //デバック
            Debug.Log("ロッカー入出不可");

            //入室不可にする
            m_canEnter = false;
        }
    }
}