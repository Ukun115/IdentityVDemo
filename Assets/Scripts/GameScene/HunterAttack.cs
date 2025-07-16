using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �n���^�[�̍U������
/// </summary>
public class HunterAttack : MonoBehaviour
{
    //�U���������邩�ǂ���
    bool m_canHitAttack = false;

    //�n���^�[�̍U�����
    enum EnHunterAttackStatu
    {
        enBeforeAttack,     //�U���O
        enAttacking,        //�U����
        enAfterAttack       //�U����(�d��)
    }
    EnHunterAttackStatu m_hunterAttackStatu = EnHunterAttackStatu.enBeforeAttack;

    //�U���͈͂ɓ����Ă����T�o�C�o�[�I�u�W�F�N�g
    GameObject m_survivor = null;

    //�U���^�C�}�[
    int m_attackTimer = 0;
    //�U����̍d�����ԃ^�C�}�[
    int m_attackFreezeTimer = 0;

    void Update()
    {
        //�U���̏�Ԃɂ���ď����𕪊�
        switch (m_hunterAttackStatu)
        {
            //�U���O
            case EnHunterAttackStatu.enBeforeAttack:
                //E�L�[�ōU��
                if (Input.GetKey(KeyCode.E))
                {
                    //�U���J�n
                    m_hunterAttackStatu = EnHunterAttackStatu.enAttacking;
                    //�U���^�C�}�[��������
                    m_attackTimer = 0;

                    //�f�o�b�N
                    Debug.Log("�n���^�[�U���J�n");
                }
                break;

            //�U����
            case EnHunterAttackStatu.enAttacking:
                //�U���^�C�}�[���v�����Ă���
                m_attackTimer++;

                //�U�����q�b�g����^�C�~���O�̂Ƃ��A
                if (m_attackTimer == 30)
                {
                    //�U����������͈͂ɃT�o�C�o�[�������Ă�Ƃ�
                    if (m_canHitAttack)
                    {
                        //�U���q�b�g�����T�o�C�o�[�̗̑͂�1���炷
                        m_survivor.GetComponent<SurvivorStatus>().HitPointUpDown(-1);

                        //�f�o�b�N
                        Debug.Log("�T�o�C�o�[�ɍU���q�b�g�I�̗͂�-1���܂��B");
                    }
                    else
                    {
                        //�f�o�b�N
                        Debug.Log("��U��");
                    }

                    //�f�o�b�N
                    Debug.Log("�U����̍d�����J�n");

                    //�U����(�d��)�Ɉڍs
                    m_hunterAttackStatu = EnHunterAttackStatu.enAfterAttack;
                }
                break;

            //�U����(�d��)
            case EnHunterAttackStatu.enAfterAttack:

                //�n���^�[�̓������~�߂�
                GameObject.Find("Hunter").GetComponent<HunterMovement>().SetIsStop(true);

                //�U����̍d�����ԃ^�C�}�[���v������
                m_attackFreezeTimer++;
                //�d�����Ԃ��߂�����A
                if(m_attackFreezeTimer > 42)
                {
                    //�n���^�[�̓������ĊJ������
                    GameObject.Find("Hunter").GetComponent<HunterMovement>().SetIsStop(false);

                    //��Ԃ��U���O�Ɉڍs
                    m_hunterAttackStatu = EnHunterAttackStatu.enBeforeAttack;

                    //�d�����ԃ^�C�}�[��������
                    m_attackFreezeTimer = 0;

                    //�f�o�b�N
                    Debug.Log("�U����̍d�����I��");
                }
                break;
        }
    }

    //�I�u�W�F�N�g�̗̈�ɓ��������x�����Ă΂�鏈��
    void OnTriggerEnter(Collider other)
    {
        //�T�o�C�o�[��������A
        if (other.gameObject.tag == "Survivor")
        {
            //�U�������锻��
            m_canHitAttack = true;

            //�U���͈͂ɓ����Ă����T�o�C�o�[�I�u�W�F�N�g��ۑ�
            m_survivor = other.gameObject;

            //�f�o�b�N
            Debug.Log("�U����������͈͂ɃT�o�C�o�[�����܂�");
        }
    }

    //�I�u�W�F�N�g�̗̈悩��o�����x�����Ă΂�鏈��
    void OnTriggerExit(Collider other)
    {
        //�T�o�C�o�[��������A
        if (other.gameObject.tag == "Survivor")
        {
            //�U��������Ȃ�����
            m_canHitAttack = false;

            //�f�o�b�N
            Debug.Log("�U����������͈͂���T�o�C�o�[���o�܂���");
        }
    }
}