using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleTable : MonoBehaviour
{
    //役職表が表示されているかどうか
    bool m_onRoleTable = false;
    //役職表をスライドできる状態かどうか
    bool m_isSlide = false;
    //役職表スライド範囲(Min)
    const float c_minSlideRange = 80.0f;
    //役職表スライド範囲(Max)
    const float c_maxSlideRange = 1100.0f;
    //スライドの速度
    const float c_slideSpeed = 40.0f;
    //
    [SerializeField] GameObject m_roleTableGameObject = null;

    void Update()
    {
        //スライドができる状態
        if(m_isSlide)
        {
            //役職表を表示！
            if(m_onRoleTable)
            {
                if (m_roleTableGameObject.transform.position.x < c_maxSlideRange)
                {
                    //右にスライド
                    m_roleTableGameObject.transform.position += new Vector3(c_slideSpeed,0.0f,0.0f);
                }
                else
                {
                    //スライドできない状態にする
                    m_isSlide = false;
                }
            }
            //役職表をしまう！
            else
            {
                if (m_roleTableGameObject.transform.position.x > c_minSlideRange)
                {
                    //左にスライド
                    m_roleTableGameObject.transform.position -= new Vector3(c_slideSpeed, 0.0f, 0.0f);
                }
                else
                {
                    //スライドできない状態にする
                    m_isSlide = false;
                }
            }
        }
    }

    //役職表のボタンを押したときに呼ばれる関数
    //機能：役職表をスライドさせる
    public void OnClickRoleTableButton()
    {
        //デバック
        Debug.Log("役職表のスライドボタンが押された");

        //役職表が表示されているとき、
        if(m_onRoleTable)
        {
            //役職表をしまう
            m_onRoleTable = !m_onRoleTable;
            m_isSlide = true;

        }
        //役職表が表示されていないとき、
        else
        {
            //役職表を表示させる
            m_onRoleTable = !m_onRoleTable;
            m_isSlide = true;
        }
    }
}
