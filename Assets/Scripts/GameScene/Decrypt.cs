using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 暗号機の解読進度
/// </summary>
public class Decrypt : MonoBehaviourPunCallbacks
{
    //解読進度
    float m_decryptProgress = 0.0f;

    //解読速度
    float m_codeMachineSpeed = 0.5f;  //暗号機
    float m_gateSwitchSpeed = 5.0f;    //ゲートスイッチ

    //解読中かどうか
    bool m_isDecoding = false;

    //解読進度バー
    Image m_decryptProgressBarImage = null;

    [SerializeField]TouchCodeMachine m_touchCodeMachine = null;

    void Update()
    {
        //解読中
        if (m_isDecoding)
        {
            //暗号機の解読進度を進める
            if (m_touchCodeMachine.GetmIsTouchingTargetCodeMachine())
            {
                //実行したい関数名、ルーム内の全員が実行する、関数に渡したい引数
                photonView.RPC(nameof(DecryptAdvance),RpcTarget.All, m_codeMachineSpeed);
            }
            //ゲートスイッチの解読進度を進める
            else
            {
                //実行したい関数名、ルーム内の全員が実行する、関数に渡したい引数
                photonView.RPC(nameof(DecryptAdvance), RpcTarget.All, m_gateSwitchSpeed);
            }

            //移動キーが押されたら、
            if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)||
               Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            {
                //解読中じゃなくする。
                ChangeIsDecoding(false);

                //解読開始できる状態に戻しておく
                m_touchCodeMachine.SetCanStartDecrypt(true);

                //デバック
                Debug.Log("解読中断");
            }

            //解読進度バーを伸ばしていく
            m_decryptProgressBarImage.rectTransform.sizeDelta = new Vector2(m_decryptProgress*1.5f,10);
        }
        //解読進度が100％になったら、
        if (m_decryptProgress >= 100.0f && this.tag != "Untagged")
        {
            //解読完了

            //タグを消して再度触れないようにする
            this.tag = "Untagged";

            //解読中じゃなくする。
            ChangeIsDecoding(false);

            //解読済みの暗号機は黄色くしておく。
            this.GetComponent<Renderer>().material.color = Color.yellow;

            //暗号機を解読していたら、
            if (m_touchCodeMachine.GetmIsTouchingTargetCodeMachine())
            {
                //残り解読数を-1する
                GameObject.Find("DontDecryptCodeYetText").
                GetComponent<ChangeLimitDecodingNumber>().
                    ReduceLimitDecodingNumber();

                //デバック
                Debug.Log("暗号機解読完了");
            }
            //ゲートスイッチを解読していたら、
            else
            {
                //デバック
                Debug.Log("ゲートスイッチ解読完了");

                //ゲート１のとき、
                if (this.gameObject.name == "GateSwitch1")
                {
                    GameObject.Find("Gate_Right1").GetComponent<GateSlide>().SlideStart();
                    GameObject.Find("Gate_Left1").GetComponent<GateSlide>().SlideStart();
                }
                //ゲート２のとき、
                if (this.gameObject.name == "GateSwitch2")
                {
                    GameObject.Find("Gate_Right2").GetComponent<GateSlide>().SlideStart();
                    GameObject.Find("Gate_Left2").GetComponent<GateSlide>().SlideStart();
                }
            }
        }
    }

    //解読中かどうかを切り替える処理
    public void ChangeIsDecoding(bool flag)
    {
        m_isDecoding = flag;

        //「暗号解読」テキストUIの表示を反転
        GameObject.Find("CodeDecryptText").GetComponent<Text>().enabled = flag;

        //解読進度バーの表示を反転
        GameObject.Find("DecryptProgressBackImage").GetComponent<Image>().enabled = flag;
        m_decryptProgressBarImage = GameObject.Find("DecryptProgressImage").GetComponent<Image>();
        m_decryptProgressBarImage.enabled = flag;
    }

    //オブジェクトの領域から出たら一度だけ呼ばれる処理
    void OnTriggerExit(Collider other)
    {
        //暗号機の接触可能範囲からサバイバーが離れたら、
        //かつ、そのとき解読中だったら、
        if (other.gameObject.tag == "Survivor" && m_isDecoding)
        {
            //解読中じゃなくする。
            ChangeIsDecoding(false);
        }
    }

    //解読を進める関数
    [PunRPC]
    void DecryptAdvance(float decryptSpeed)
    {
        m_decryptProgress += decryptSpeed;
        //デバック
        Debug.Log(m_decryptProgress);
    }
}