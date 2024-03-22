using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBlock : MonoBehaviour
{
    [Header("�̵� �Ÿ�")]
    public float moveX = 0.0f;
    public float moveY = 0.0f;
    [Header("�ð� / �����ð�")]
    public float times = 0.0f;
    public float weight = 0.0f;
    [Header("������ ����")]
    public bool playerOnOff = false; //�÷��̾ �ö󰡸� �����̰� �� ������ ����
    public bool moveOnOff = true;
    float fpsX; //�����Ӵ� x�� ������
    float fpsY; //�����Ӵ� y�� ������
    Vector3 firstLocation; //ó�� ��ġ
    bool reverse = false; // �̵� ���� ���� (������ �̵�����)



    // Start is called before the first frame update
    void Start()
    {
        firstLocation = transform.position; //ó�� ��ġ(�ǵ��� �� �� �ִ� ��ġ)
        float timestep = Time.fixedDeltaTime;
        fpsX = moveX / (1.0f / timestep * times);
        fpsY = moveY / (1.0f / timestep * times);
        //1�������� X, Y �̵� ��

        if (playerOnOff)
        {
            moveOnOff = false; //�ö󰡸� �����̱� ����
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (moveOnOff)
        {
            float x = transform.position.x;
            float y = transform.position.y;
            bool endX = false;
            bool endY = false;
            if (reverse)
            {
                //�ݴ�� �̵�
                if ((x <= firstLocation.x && 0.0f <= fpsX) || fpsX < 0.0f && firstLocation.x <= x)
                //�̵����� ���(0���� ũ��) �̵� ��ġ�� �ʱ� ��ġ���� ���� 
                {
                    endX = true;
                }
                if ((y <= firstLocation.y && 0.0f <= fpsY) || fpsY < 0.0f && firstLocation.y <= y)
                //�̵����� ����(0���� �۰�) �̵� ��ġ�� �ʱ� ��ġ���� ŭ 
                {
                    endY = true;
                }

                transform.Translate(new Vector3(-fpsX, -fpsY, firstLocation.z));
            }
            else
            {
                //������ �̵�
                if ((fpsX >= 0.0f && x >= firstLocation.x + moveX) || (fpsX < 0.0f && x <= firstLocation.x + moveX))
                {
                    endX = true;
                }
                if ((fpsY >= 0.0f && y >= firstLocation.y + moveY) || (fpsY < 0.0f && y <= firstLocation.y + moveY))
                {
                    endY = true;
                }
                transform.Translate(new Vector3(fpsX, fpsY, firstLocation.z));
            }
            if(endX && endY)
            {
                //�̵� ����
                if(reverse)
                {
                    transform.position = firstLocation;
                }
                reverse = !reverse;
                moveOnOff = false;
                if(playerOnOff == false)
                {
                    Invoke("MoveOn", weight);
                }
            }
        }
    }

    public void MoveOn()
    {
        moveOnOff = true;
    }

    public void MoveOff()
    {
        moveOnOff = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Debug.Log("�÷��̾�� ���� ����");
            //�÷��̾�� �����ϸ� �̵� ����� �ڽ����� �����
            collision.transform.SetParent(transform);
            if(playerOnOff)
            {
                moveOnOff = true;
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("�÷��̾�� ���� �����ض�!");
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("�÷��̾�� ���� ����");
            //�÷��̾�� ������ ������ �̵� ����� �ڽĿ��� ���ܽ�Ű��
            collision.transform.SetParent(null);
        }
        
    }

}
