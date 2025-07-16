using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �Q�[�g���X���C�h�����鏈��
/// </summary>
public class GateSlide : MonoBehaviour
{
    //�X���C�h�������t���ǂ���
    [SerializeField] int m_isReverse = 1;

    //�X���C�h�p���[
    float m_slidePower = 0.0f;

    //�Q�[�g�̃X���C�h���J�n���Ă�����
    bool m_isStart = false;

    void Update()
    {
        //�Q�[�g�X���C�h�J�n
        if(m_isStart)
        {
            //�X���C�h�p���[�𑝂₵�Ă���
            m_slidePower += 0.00001f;

            //�Q�[�g���ړ������Ă���
            this.transform.position += new Vector3(m_slidePower* m_isReverse, 0.0f,0.0f);

            //�J����������A
            if(m_slidePower > 0.0058f)
            {
                //�X���C�h�����Ă�Q�[�g�I�u�W�F�N�g���폜����
                Destroy(this.gameObject);
            }
        }
    }

    //�Q�[�g�̃X���C�h���J�n���Ă�������ݒ肷��Z�b�^�[
    public void SlideStart()
    {
        m_isStart = true;
    }
}