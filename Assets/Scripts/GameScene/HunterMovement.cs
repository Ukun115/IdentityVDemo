using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ハンターの移動処理
/// </summary>
public class HunterMovement : Photon.Pun.MonoBehaviourPun
{
    //剛体
    Rigidbody m_rigidbody = null;
    //移動方向
    Vector3 m_moveDirection = Vector3.zero;

    //移動速度
    [SerializeField] float m_speed = 9.5f;

    //左右矢印キーの値(-1.0f～1.0f)
    float m_horizontal = 0.0f;
    //上下矢印キーの値(-1.0f～1.0f)
    float m_vertical = 0.0f;

    //移動を止める
    bool m_isStop = false;

    void Start()
    {
        //Rigidbodyのコンポーネントを取得する
        m_rigidbody = GetComponent<Rigidbody>();

        //ハンターオブジェクトが生成されたときに名前がHunter(Clone)にならないように念のため再度初期化しておく
        this.name = "Hunter";
    }

    // Update is called once per frame
    void Update()
    {
        //このハンターオブジェクトが自分の所で PhotonNetwork.Instantiate していなかったら、
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
        }
    }

    void FixedUpdate()
    {
        //剛体に移動を割り当て(一緒に重力も割り当て)
        m_rigidbody.velocity = new Vector3(m_moveDirection.x, m_rigidbody.velocity.y, m_moveDirection.z);
    }

    //移動を止める設定をするセッター
    public void SetIsStop(bool isStop)
    {
        m_isStop = isStop;
        m_moveDirection = new Vector3(0.0f, 0.0f, 0.0f);
    }
}
