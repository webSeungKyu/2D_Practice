using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region �ʵ�
    Rigidbody2D rbody;
    float axisH = 0.0f;
    public float speed = 3.0f;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        //Rigidbody2D ��������
        rbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //���� ���������� �Է� Ȯ�� > ������ Ű ������ 1.0f ��ȯ > ������ - 1.0f > �ƹ��͵� �� ������ 0.0f
        axisH = Input.GetAxisRaw("Horizontal");

        if(axisH > 0.0f)
        {
            transform.localScale = new Vector2(1, 1);
        }
        else if(axisH < 0.0f)
        {
            transform.localScale = new Vector2(-1, 1);
        }
    }

    void FixedUpdate()
    {
        //�ӵ� ����
        rbody.velocity = new Vector2(axisH * speed, rbody.velocity.y);
    }
}
