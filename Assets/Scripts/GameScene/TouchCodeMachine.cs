using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 暗号機との接触判定
/// </summary>
public class TouchCodeMachine : MonoBehaviour
{
    //解読開始できるかどうか
    bool m_canStartDecrypt = false;

    //判定を取っている対象の暗号機オブジェクト
    GameObject m_codeMachine = null;

    //触ったのが暗号機かどうかの判定
    bool m_isTouchingTargetCodeMachine = true;

    void Update()
    {
        //解読開始できるとき、
        if(m_canStartDecrypt)
        {
            //スペースキーが押されたら、
            if (Input.GetKey(KeyCode.Space))
            {
                //解読開始

                //デバック
                Debug.Log("解読開始");

                //解読中にする
                m_codeMachine.GetComponent<Decrypt>().ChangeIsDecoding(true);

                //解読開始できない判定にする
                m_canStartDecrypt = false;
            }
        }
    }

    //オブジェクトの領域に入ったら一度だけ呼ばれる処理
    void OnTriggerEnter(Collider other)
    {
        //暗号機の接触可能範囲に入っていたら、
        //かつ、残り解読数が0じゃないとき、
        if (other.gameObject.tag == "CodeMachine" &&
            GameObject.Find("DontDecryptCodeYetText").GetComponent<ChangeLimitDecodingNumber>().GetLimitDecodingNumber() != 0)
        {
            //デバック
            Debug.Log("暗号機解読可能状態");

            //どの暗号機かを保存
            m_codeMachine = other.gameObject;

            //解読開始できる判定にする
            m_canStartDecrypt = true;

            //触っているのが暗号機！
            m_isTouchingTargetCodeMachine = true;
        }
        //ゲートスイッチの接触可能範囲に入っていたら、
        //かつ、残り解読数が0のとき、
        if(other.gameObject.tag == "GateSwitch"&&
            GameObject.Find("DontDecryptCodeYetText").GetComponent<ChangeLimitDecodingNumber>().GetLimitDecodingNumber() == 0)
        {
            //デバック
            Debug.Log("ゲートスイッチ解読可能状態");

            //どの暗号機かを保存
            m_codeMachine = other.gameObject;

            //解読開始できる判定にする
            m_canStartDecrypt = true;

            //触っているのが暗号機じゃない！
            m_isTouchingTargetCodeMachine = false;
        }
    }

    //オブジェクトの領域から出たら一度だけ呼ばれる処理
    void OnTriggerExit(Collider other)
    {
        //暗号機の接触可能範囲から離れたら、
        //かつ、残り解読数が0じゃないとき、
        if (other.gameObject.tag == "CodeMachine" &&
            GameObject.Find("DontDecryptCodeYetText").GetComponent<ChangeLimitDecodingNumber>().GetLimitDecodingNumber() != 0)
        {
            //デバック
            Debug.Log("暗号機解読不可状態");

            //解読開始できない判定にする
            m_canStartDecrypt = false;
        }
        //ゲートスイッチの接触可能範囲に入っていたら、
        //かつ、残り解読数が0のとき、
        if (other.gameObject.tag == "GateSwitch" &&
            GameObject.Find("DontDecryptCodeYetText").GetComponent<ChangeLimitDecodingNumber>().GetLimitDecodingNumber() == 0)
        {
            //デバック
            Debug.Log("ゲートスイッチ解読不可状態");

            //解読開始できない判定にする
            m_canStartDecrypt = false;
        }
    }

    //解読開始できるかどうかを設定するセッター
    public void SetCanStartDecrypt(bool flag)
    {
        m_canStartDecrypt = flag;
    }

    //触っているのが暗号機かどうかをゲットするゲッター
    public bool GetmIsTouchingTargetCodeMachine()
    {
        return m_isTouchingTargetCodeMachine;
    }
}
