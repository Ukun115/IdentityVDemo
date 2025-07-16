using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �n���^�[�̈ړ�����
/// </summary>
public class HunterMovement : Photon.Pun.MonoBehaviourPun
{
    //����
    Rigidbody m_rigidbody = null;
    //�ړ�����
    Vector3 m_moveDirection = Vector3.zero;

    //�ړ����x
    [SerializeField] float m_speed = 9.5f;

    //���E���L�[�̒l(-1.0f�`1.0f)
    float m_horizontal = 0.0f;
    //�㉺���L�[�̒l(-1.0f�`1.0f)
    float m_vertical = 0.0f;

    //�ړ����~�߂�
    bool m_isStop = false;

    void Start()
    {
        //Rigidbody�̃R���|�[�l���g���擾����
        m_rigidbody = GetComponent<Rigidbody>();

        //�n���^�[�I�u�W�F�N�g���������ꂽ�Ƃ��ɖ��O��Hunter(Clone)�ɂȂ�Ȃ��悤�ɔO�̂��ߍēx���������Ă���
        this.name = "Hunter";
    }

    // Update is called once per frame
    void Update()
    {
        //���̃n���^�[�I�u�W�F�N�g�������̏��� PhotonNetwork.Instantiate ���Ă��Ȃ�������A
        if (!photonView.IsMine)
        {
            return;
        }

        //���E���L�[�̒l(-1.0f�`1.0f)���擾����
        m_horizontal = Input.GetAxis("Horizontal");
        //�㉺���L�[�̒l(-1.0f�`1.0f)���擾����
        m_vertical = Input.GetAxis("Vertical");

        //�ړ����~���Ă��Ȃ��Ƃ�
        if (!m_isStop)
        {
            //���͂��ꂽ�L�[�̒l��ۑ�
            m_moveDirection = new Vector3(m_horizontal, 0, m_vertical);
            m_moveDirection = transform.TransformDirection(m_moveDirection);
            //�΂߂̋����������Ȃ��2�{�ɂȂ�̂�h���B
            m_moveDirection.Normalize();

            //�ړ������ɑ��x���|����(�ʏ�ړ�)
            m_moveDirection *= m_speed;
        }
    }

    void FixedUpdate()
    {
        //���̂Ɉړ������蓖��(�ꏏ�ɏd�͂����蓖��)
        m_rigidbody.velocity = new Vector3(m_moveDirection.x, m_rigidbody.velocity.y, m_moveDirection.z);
    }

    //�ړ����~�߂�ݒ������Z�b�^�[
    public void SetIsStop(bool isStop)
    {
        m_isStop = isStop;
        m_moveDirection = new Vector3(0.0f, 0.0f, 0.0f);
    }
}
