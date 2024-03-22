using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBlock : MonoBehaviour
{
    [Header("이동 거리")]
    public float moveX = 0.0f;
    public float moveY = 0.0f;
    [Header("시간 / 정지시간")]
    public float times = 0.0f;
    public float weight = 0.0f;
    [Header("움직임 여부")]
    public bool playerOnOff = false; //플레이어가 올라가면 움직이게 할 것인지 설정
    public bool moveOnOff = true;
    float fpsX; //프레임당 x의 움직임
    float fpsY; //프레임당 y의 움직임
    Vector3 firstLocation; //처음 위치
    bool reverse = false; // 이동 반전 여부 (역방향 이동할지)



    // Start is called before the first frame update
    void Start()
    {
        firstLocation = transform.position; //처음 위치(되돌아 올 수 있는 위치)
        float timestep = Time.fixedDeltaTime;
        fpsX = moveX / (1.0f / timestep * times);
        fpsY = moveY / (1.0f / timestep * times);
        //1프레임의 X, Y 이동 값

        if (playerOnOff)
        {
            moveOnOff = false; //올라가면 움직이기 시작
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
                //반대로 이동
                if ((x <= firstLocation.x && 0.0f <= fpsX) || fpsX < 0.0f && firstLocation.x <= x)
                //이동량이 양수(0보다 크고) 이동 위치가 초기 위치보다 작음 
                {
                    endX = true;
                }
                if ((y <= firstLocation.y && 0.0f <= fpsY) || fpsY < 0.0f && firstLocation.y <= y)
                //이동량이 음수(0보다 작고) 이동 위치가 초기 위치보다 큼 
                {
                    endY = true;
                }

                transform.Translate(new Vector3(-fpsX, -fpsY, firstLocation.z));
            }
            else
            {
                //앞으로 이동
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
                //이동 종료
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
            Debug.Log("플레이어와 접촉 시작");
            //플레이어와 접촉하면 이동 블록의 자식으로 만들기
            collision.transform.SetParent(transform);
            if(playerOnOff)
            {
                moveOnOff = true;
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("플레이어와 접촉 종료해라!");
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("플레이어와 접촉 종료");
            //플레이어와 접촉이 끝나면 이동 블록의 자식에서 제외시키기
            collision.transform.SetParent(null);
        }
        
    }

}
