using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 板に接触する
/// </summary>
public class Board : MonoBehaviour
{
    //板の状態
    enum EnBoardState
    {
        enNotCollapsed,      //倒れていない状態
        enFallingAnimation,  //倒れるアニメーション状態
        enFallenDown         //倒れている状態
    }
    EnBoardState m_boardState = EnBoardState.enNotCollapsed;

    //板のギズモ
    GameObject m_boardGizmo = null;

    //板が倒れるアニメーションのタイマー
    float m_boardFallingTimer = 0.0f;

    void Start()
    {
        //板のギズモのゲームオブジェクトを取得する
        m_boardGizmo = GameObject.Find("BoardGizmo");
    }

    void Update()
    {
        //板の状態によって変更
        switch(m_boardState)
        {
            //倒れていない状態
            case EnBoardState.enNotCollapsed:
                //何もしない。
                break;

            //倒れるアニメーション状態
            case EnBoardState.enFallingAnimation:
                //板自体の当たり判定は消す
                this.GetComponent<BoxCollider>().isTrigger = true;
                //板倒しアニメーション
                m_boardFallingTimer += 0.05f;
                m_boardGizmo.transform.rotation = new Quaternion(m_boardFallingTimer, 0.0f,0.0f,1.0f);

                //サバイバーの動きを止める
                GameObject.Find("Survivor").GetComponent<SurvivorMovement>().SetIsStop(true);

                //ある程度倒れたら、
                if (m_boardFallingTimer >= 0.6f)
                {
                    //板が倒れている状態にする
                    m_boardState = EnBoardState.enFallenDown;

                    //板を飛び越えられるようにする
                    GameObject.Find("TouchCollider").GetComponent<TouchBoard>().SetBoardJumpStart(true);

                    //サバイバーの動きを再開させる
                    GameObject.Find("Survivor").GetComponent<SurvivorMovement>().SetIsStop(false);
                }

                break;

            //倒れている状態
            case EnBoardState.enFallenDown:
                //このスクリプトを削除する
                this.enabled = false;
                //板倒しの接触判定を削除する
                GameObject.Find("TouchCollider").GetComponent<TouchBoard>().enabled = false;
                //窓枠飛び越えスクリプトを起動する
                //窓枠と同じ処理になる
                GameObject.Find("TouchCollider").GetComponent<OvercomingTheWindowFrame>().enabled = true;
                //ハンターの板壊しスクリプトを起動する
                GameObject.Find("TouchCollider").GetComponent<BoardBreak>().enabled = true;

                break;
        }
    }

    //板の状態を設定するプロパティ
    public int GetSetBoardState
    {
        //ゲッター
        get { return (int)m_boardState; }
        //セッター
        set { m_boardState = (EnBoardState)value; }
    }
}