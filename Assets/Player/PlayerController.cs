using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region 필드
    Rigidbody2D rbody;
    float axisH = 0.0f; // 입력값
    public float speed = 3.0f; // 이동 속도
    public float jump = 9.0f; // 점프력
    public LayerMask groundLayer; // 착지할 수 있는 레이어
    bool goJump = false; // 점프 개시 플래그
    bool onGround = false; // 지면에 서 있는 플래그
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        //Rigidbody2D 가져오기
        rbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //수평 방향으로의 입력 확인 > 오른쪽 키 눌리면 1.0f 반환 > 왼쪽은 - 1.0f > 아무것도 안 눌리면 0.0f
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
        //착지 판정
        //Physics2D.Linecast는 두 점을 연결하는 선에 오브젝트가 접촉하는지 조사해 true 혹은 false로 반환함
        //transform.up은 백터로 x = 0, y = 1, z = 0으로 나타냄
        onGround = Physics2D.Linecast(transform.position, transform.position - (transform.up * 0.1f), groundLayer);

        if(onGround || axisH != 0)
        {
            //지면 위 or 속도가 0이 아닐 경우
            rbody.velocity = new Vector2(axisH * speed, rbody.velocity.y);
        }

        if(onGround && goJump)
        {
            //지면 위 and 점프 키 눌렸을 경우
            Vector2 jumpPw = new Vector2(0, jump); // 점프를 위한 백터 생성
            rbody.AddForce(jumpPw, ForceMode2D.Impulse); // 순간적인 힘 가하기
            goJump = false;
        }

    }

    public void Jump()
    {
        goJump = true;
    }
}
