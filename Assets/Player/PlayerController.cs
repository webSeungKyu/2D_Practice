using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    //�ִϸ��̼� ó��
    Animator animator; // �ִϸ�����
    public string stopAnime = "PlayerStop";
    public string moveAnime = "PlayerMove";
    public string jumpAnime = "PlayerJump";
    public string goalAnime = "PlayerGoal";
    public string deadAnime = "PlayerOver";
    string nowAnime = "";
    string oldAnime = "";
    public static string gameState;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        //Rigidbody2D ��������
        rbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        nowAnime = stopAnime;
        oldAnime = stopAnime;
        gameState = "playing";
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
        if(gameState != "playing")
        {
            return;
        }
    }

    void FixedUpdate()
    {
        if(gameState != "playing")
        {
            return;
        }

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

        if (onGround)
        {
            //���� ���� ���
            if(axisH == 0)
            {
                nowAnime = stopAnime;
            }
            else
            {
                nowAnime = moveAnime;
            }
        }
        else
        {
            //������ ���
            nowAnime = jumpAnime;
        }

        if(nowAnime != oldAnime)
        {
            oldAnime = nowAnime;
            animator.Play(nowAnime);
        }

    }

    public void Jump()
    {
        goJump = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Goal")
        {
            Goal();
        }
        else if (collision.gameObject.tag == "Dead")
        {
            GameOver();
        }
    }

    public void Goal()
    {
        animator.Play(goalAnime);
        gameState = "gameClear";
        GameStop();
    }
    
    public void GameOver()
    {
        animator.Play(deadAnime);

        gameState = "gameOver";
        GameStop();

        GetComponent<CapsuleCollider2D>().enabled = false; //�÷��̾� �浹 ��Ȱ��ȭ
        rbody.AddForce(new Vector2(0, 5), ForceMode2D.Impulse);
    }

    public void GameStop()
    {
        
        rbody.velocity = new Vector2(0, 0);
        transform.position = GameObject.FindGameObjectWithTag("Goal").GetComponent<Transform>().position;
    }
}
