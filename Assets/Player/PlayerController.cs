using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region 필드
    Rigidbody2D rbody;
    float axisH = 0.0f;
    public float speed = 3.0f;
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
    }

    void FixedUpdate()
    {
        //속도 갱신
        rbody.velocity = new Vector2(axisH * speed, rbody.velocity.y);
    }
}
