using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// サバイバーの移動処理
/// </summary>
public class SurvivorMovement : Photon.Pun.MonoBehaviourPun
{
    //剛体
    Rigidbody m_rigidbody = null;
    //移動方向
    Vector3 m_moveDirection = Vector3.zero;

    //移動速度
    [SerializeField] float m_speed = 5.0f;

    //左右矢印キーの値(-1.0f～1.0f)
    float m_horizontal = 0.0f;
    //上下矢印キーの値(-1.0f～1.0f)
    float m_vertical = 0.0f;

    //移動を止める
    bool m_isStop = false;

    //ダウンしているかどうか
    bool m_isDownState = false;

    //移動の状態
    enum EnMovementStatu
    {
        enNormalMovement,   //通常移動
        enRunMovement,      //走り移動
        enSquatMovement,    //しゃがみ移動
        enDownMovement      //ダウン移動
    }
    EnMovementStatu m_movementStatu = EnMovementStatu.enNormalMovement;

    void Start()
    {
        //Rigidbodyのコンポーネントを取得する
        m_rigidbody = GetComponent<Rigidbody>();

        //サバイバーオブジェクトが生成されたときに名前がSurvivor(Clone)にならないように念のため再度初期化しておく
        this.name = "Survivor";
    }

    void Update()
    {
        //このサバイバーオブジェクトが自分の所で PhotonNetwork.Instantiate していなかったら、
        if (!photonView.IsMine)
        {
            return;
        }

        //左右矢印キーの値(-1.0f～1.0f)を取得する
        m_horizontal = Input.GetAxis("Horizontal");
        //上下矢印キーの値(-1.0f～1.0f)を取得する
        m_vertical = Input.GetAxis("Vertical");

        //移動を停止していないとき
        if (!m_isStop)
        {
            //入力されたキーの値を保存
            m_moveDirection = new Vector3(m_horizontal, 0, m_vertical);
            m_moveDirection = transform.TransformDirection(m_moveDirection);
            //斜めの距離が長くなる√2倍になるのを防ぐ。
            m_moveDirection.Normalize();

            //移動方向に速度を掛ける(通常移動)
            m_moveDirection *= m_speed;

            //通常移動状態にする
            m_movementStatu = EnMovementStatu.enNormalMovement;

            //ダウンしていないときのみ実行可能
            if (!m_isDownState)
            {
                //通常体サイズ
                this.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

                //左シフトキーを押していた時はダッシュ移動
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    //移動速度2倍
                    m_moveDirection *= 1.5f;

                    //走り移動状態に更新する
                    m_movementStatu = EnMovementStatu.enRunMovement;
                }
                //左コントロールキーを押している時はしゃがみ移動
                if (Input.GetKey(KeyCode.LeftControl))
                {
                    //移動速度1/2倍
                    m_moveDirection *= 0.5f;

                    //しゃがみ移動状態に更新する
                    m_movementStatu = EnMovementStatu.enSquatMovement;

                    //体を小さくする
                    this.transform.localScale = new Vector3(0.7f, 0.4f, 0.7f);
                }
            }
            else
            {
                //ダウン移動状態にする
                m_movementStatu = EnMovementStatu.enDownMovement;
            }
        }

        //ダウンしているときの移動
        if(m_isDownState)
        {
            //移動速度1/2倍
            m_moveDirection *= 0.5f;

            //体を小さくする
            this.transform.localScale = new Vector3(0.7f, 0.4f, 0.7f);

            //デバック
            Debug.Log("ダウン中");
        }
    }

    void FixedUpdate()
    {
        //剛体に移動を割り当て(一緒に重力も割り当て)
        m_rigidbody.velocity = new Vector3(m_moveDirection.x,m_rigidbody.velocity.y,m_moveDirection.z);
    }

    //移動を止める設定をするセッター
    public void SetIsStop(bool isStop)
    {
        m_isStop = isStop;
        m_moveDirection = new Vector3( 0.0f,0.0f,0.0f );
    }

    //ダウン状態かどうかのプロパティ
    public bool GetSetIsDownStatu
    {
        get
        {
            return m_isDownState;
        }
        set
        {
            m_isDownState = value;
        }
    }

    //現在のサバイバーの移動状態を取得するゲッター
    public int GetMovementStatu()
    {
        return (int)m_movementStatu;
    }
}