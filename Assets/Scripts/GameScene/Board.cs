using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �ɐڐG����
/// </summary>
public class Board : MonoBehaviour
{
    //�̏��
    enum EnBoardState
    {
        enNotCollapsed,      //�|��Ă��Ȃ����
        enFallingAnimation,  //�|���A�j���[�V�������
        enFallenDown         //�|��Ă�����
    }
    EnBoardState m_boardState = EnBoardState.enNotCollapsed;

    //�̃M�Y��
    GameObject m_boardGizmo = null;

    //���|���A�j���[�V�����̃^�C�}�[
    float m_boardFallingTimer = 0.0f;

    void Start()
    {
        //�̃M�Y���̃Q�[���I�u�W�F�N�g���擾����
        m_boardGizmo = GameObject.Find("BoardGizmo");
    }

    void Update()
    {
        //�̏�Ԃɂ���ĕύX
        switch(m_boardState)
        {
            //�|��Ă��Ȃ����
            case EnBoardState.enNotCollapsed:
                //�������Ȃ��B
                break;

            //�|���A�j���[�V�������
            case EnBoardState.enFallingAnimation:
                //���̂̓����蔻��͏���
                this.GetComponent<BoxCollider>().isTrigger = true;
                //�|���A�j���[�V����
                m_boardFallingTimer += 0.05f;
                m_boardGizmo.transform.rotation = new Quaternion(m_boardFallingTimer, 0.0f,0.0f,1.0f);

                //�T�o�C�o�[�̓������~�߂�
                GameObject.Find("Survivor").GetComponent<SurvivorMovement>().SetIsStop(true);

                //������x�|�ꂽ��A
                if (m_boardFallingTimer >= 0.6f)
                {
                    //���|��Ă����Ԃɂ���
                    m_boardState = EnBoardState.enFallenDown;

                    //���щz������悤�ɂ���
                    GameObject.Find("TouchCollider").GetComponent<TouchBoard>().SetBoardJumpStart(true);

                    //�T�o�C�o�[�̓������ĊJ������
                    GameObject.Find("Survivor").GetComponent<SurvivorMovement>().SetIsStop(false);
                }

                break;

            //�|��Ă�����
            case EnBoardState.enFallenDown:
                //���̃X�N���v�g���폜����
                this.enabled = false;
                //�|���̐ڐG������폜����
                GameObject.Find("TouchCollider").GetComponent<TouchBoard>().enabled = false;
                //���g��щz���X�N���v�g���N������
                //���g�Ɠ��������ɂȂ�
                GameObject.Find("TouchCollider").GetComponent<OvercomingTheWindowFrame>().enabled = true;
                //�n���^�[�̔󂵃X�N���v�g���N������
                GameObject.Find("TouchCollider").GetComponent<BoardBreak>().enabled = true;

                break;
        }
    }

    //�̏�Ԃ�ݒ肷��v���p�e�B
    public int GetSetBoardState
    {
        //�Q�b�^�[
        get { return (int)m_boardState; }
        //�Z�b�^�[
        set { m_boardState = (EnBoardState)value; }
    }
}