using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region �ʵ�
    Rigidbody2D rbody;
    float axisH = 0.0f; // �Է°�
    public float speed = 3.0f; // �̵� �ӵ�
    public float jump = 9.0f; // ������
    public LayerMask groundLayer; // ������ �� �ִ� ���̾�
    bool goJump = false; // ���� ���� �÷���
    bool onGround = false; // ���鿡 �� �ִ� �÷���
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

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    void FixedUpdate()
    {
        //���� ����
        //Physics2D.Linecast�� �� ���� �����ϴ� ���� ������Ʈ�� �����ϴ��� ������ true Ȥ�� false�� ��ȯ��
        //transform.up�� ���ͷ� x = 0, y = 1, z = 0���� ��Ÿ��
        onGround = Physics2D.Linecast(transform.position, transform.position - (transform.up * 0.1f), groundLayer);

        if(onGround || axisH != 0)
        {
            //���� �� or �ӵ��� 0�� �ƴ� ���
            rbody.velocity = new Vector2(axisH * speed, rbody.velocity.y);
        }

        if(onGround && goJump)
        {
            //���� �� and ���� Ű ������ ���
            Vector2 jumpPw = new Vector2(0, jump); // ������ ���� ���� ����
            rbody.AddForce(jumpPw, ForceMode2D.Impulse); // �������� �� ���ϱ�
            goJump = false;
        }

    }

    public void Jump()
    {
        goJump = true;
    }
}
