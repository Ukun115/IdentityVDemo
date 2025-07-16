using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ハンターの攻撃処理
/// </summary>
public class HunterAttack : MonoBehaviour
{
    //攻撃が当たるかどうか
    bool m_canHitAttack = false;

    //ハンターの攻撃状態
    enum EnHunterAttackStatu
    {
        enBeforeAttack,     //攻撃前
        enAttacking,        //攻撃中
        enAfterAttack       //攻撃後(硬直)
    }
    EnHunterAttackStatu m_hunterAttackStatu = EnHunterAttackStatu.enBeforeAttack;

    //攻撃範囲に入ってきたサバイバーオブジェクト
    GameObject m_survivor = null;

    //攻撃タイマー
    int m_attackTimer = 0;
    //攻撃後の硬直時間タイマー
    int m_attackFreezeTimer = 0;

    void Update()
    {
        //攻撃の状態によって処理を分岐
        switch (m_hunterAttackStatu)
        {
            //攻撃前
            case EnHunterAttackStatu.enBeforeAttack:
                //Eキーで攻撃
                if (Input.GetKey(KeyCode.E))
                {
                    //攻撃開始
                    m_hunterAttackStatu = EnHunterAttackStatu.enAttacking;
                    //攻撃タイマーを初期化
                    m_attackTimer = 0;

                    //デバック
                    Debug.Log("ハンター攻撃開始");
                }
                break;

            //攻撃中
            case EnHunterAttackStatu.enAttacking:
                //攻撃タイマーを計測していく
                m_attackTimer++;

                //攻撃がヒットするタイミングのとき、
                if (m_attackTimer == 30)
                {
                    //攻撃が当たる範囲にサバイバーが入ってるとき
                    if (m_canHitAttack)
                    {
                        //攻撃ヒットしたサバイバーの体力を1減らす
                        m_survivor.GetComponent<SurvivorStatus>().HitPointUpDown(-1);

                        //デバック
                        Debug.Log("サバイバーに攻撃ヒット！体力を-1します。");
                    }
                    else
                    {
                        //デバック
                        Debug.Log("空振り");
                    }

                    //デバック
                    Debug.Log("攻撃後の硬直を開始");

                    //攻撃後(硬直)に移行
                    m_hunterAttackStatu = EnHunterAttackStatu.enAfterAttack;
                }
                break;

            //攻撃後(硬直)
            case EnHunterAttackStatu.enAfterAttack:

                //ハンターの動きを止める
                GameObject.Find("Hunter").GetComponent<HunterMovement>().SetIsStop(true);

                //攻撃後の硬直時間タイマーを計測する
                m_attackFreezeTimer++;
                //硬直時間が過ぎたら、
                if(m_attackFreezeTimer > 42)
                {
                    //ハンターの動きを再開させる
                    GameObject.Find("Hunter").GetComponent<HunterMovement>().SetIsStop(false);

                    //状態を攻撃前に移行
                    m_hunterAttackStatu = EnHunterAttackStatu.enBeforeAttack;

                    //硬直時間タイマーを初期化
                    m_attackFreezeTimer = 0;

                    //デバック
                    Debug.Log("攻撃後の硬直を終了");
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
            //攻撃当たる判定
            m_canHitAttack = true;

            //攻撃範囲に入ってきたサバイバーオブジェクトを保存
            m_survivor = other.gameObject;

            //デバック
            Debug.Log("攻撃が当たる範囲にサバイバーがいます");
        }
    }

    //オブジェクトの領域から出たら一度だけ呼ばれる処理
    void OnTriggerExit(Collider other)
    {
        //サバイバーだったら、
        if (other.gameObject.tag == "Survivor")
        {
            //攻撃当たらない判定
            m_canHitAttack = false;

            //デバック
            Debug.Log("攻撃が当たる範囲からサバイバーが出ました");
        }
    }
}