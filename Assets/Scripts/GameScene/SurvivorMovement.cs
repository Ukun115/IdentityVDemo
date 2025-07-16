using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �T�o�C�o�[�̈ړ�����
/// </summary>
public class SurvivorMovement : Photon.Pun.MonoBehaviourPun
{
    //����
    Rigidbody m_rigidbody = null;
    //�ړ�����
    Vector3 m_moveDirection = Vector3.zero;

    //�ړ����x
    [SerializeField] float m_speed = 5.0f;

    //���E���L�[�̒l(-1.0f�`1.0f)
    float m_horizontal = 0.0f;
    //�㉺���L�[�̒l(-1.0f�`1.0f)
    float m_vertical = 0.0f;

    //�ړ����~�߂�
    bool m_isStop = false;

    //�_�E�����Ă��邩�ǂ���
    bool m_isDownState = false;

    //�ړ��̏��
    enum EnMovementStatu
    {
        enNormalMovement,   //�ʏ�ړ�
        enRunMovement,      //����ړ�
        enSquatMovement,    //���Ⴊ�݈ړ�
        enDownMovement      //�_�E���ړ�
    }
    EnMovementStatu m_movementStatu = EnMovementStatu.enNormalMovement;

    void Start()
    {
        //Rigidbody�̃R���|�[�l���g���擾����
        m_rigidbody = GetComponent<Rigidbody>();

        //�T�o�C�o�[�I�u�W�F�N�g���������ꂽ�Ƃ��ɖ��O��Survivor(Clone)�ɂȂ�Ȃ��悤�ɔO�̂��ߍēx���������Ă���
        this.name = "Survivor";
    }

    void Update()
    {
        //���̃T�o�C�o�[�I�u�W�F�N�g�������̏��� PhotonNetwork.Instantiate ���Ă��Ȃ�������A
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

            //�ʏ�ړ���Ԃɂ���
            m_movementStatu = EnMovementStatu.enNormalMovement;

            //�_�E�����Ă��Ȃ��Ƃ��̂ݎ��s�\
            if (!m_isDownState)
            {
                //�ʏ�̃T�C�Y
                this.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

                //���V�t�g�L�[�������Ă������̓_�b�V���ړ�
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    //�ړ����x2�{
                    m_moveDirection *= 1.5f;

                    //����ړ���ԂɍX�V����
                    m_movementStatu = EnMovementStatu.enRunMovement;
                }
                //���R���g���[���L�[�������Ă��鎞�͂��Ⴊ�݈ړ�
                if (Input.GetKey(KeyCode.LeftControl))
                {
                    //�ړ����x1/2�{
                    m_moveDirection *= 0.5f;

                    //���Ⴊ�݈ړ���ԂɍX�V����
                    m_movementStatu = EnMovementStatu.enSquatMovement;

                    //�̂�����������
                    this.transform.localScale = new Vector3(0.7f, 0.4f, 0.7f);
                }
            }
            else
            {
                //�_�E���ړ���Ԃɂ���
                m_movementStatu = EnMovementStatu.enDownMovement;
            }
        }

        //�_�E�����Ă���Ƃ��̈ړ�
        if(m_isDownState)
        {
            //�ړ����x1/2�{
            m_moveDirection *= 0.5f;

            //�̂�����������
            this.transform.localScale = new Vector3(0.7f, 0.4f, 0.7f);

            //�f�o�b�N
            Debug.Log("�_�E����");
        }
    }

    void FixedUpdate()
    {
        //���̂Ɉړ������蓖��(�ꏏ�ɏd�͂����蓖��)
        m_rigidbody.velocity = new Vector3(m_moveDirection.x,m_rigidbody.velocity.y,m_moveDirection.z);
    }

    //�ړ����~�߂�ݒ������Z�b�^�[
    public void SetIsStop(bool isStop)
    {
        m_isStop = isStop;
        m_moveDirection = new Vector3( 0.0f,0.0f,0.0f );
    }

    //�_�E����Ԃ��ǂ����̃v���p�e�B
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

    //���݂̃T�o�C�o�[�̈ړ���Ԃ��擾����Q�b�^�[
    public int GetMovementStatu()
    {
        return (int)m_movementStatu;
    }
}