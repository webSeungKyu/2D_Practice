using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VirtualPad : MonoBehaviour
{
    public float maxLength = 70.0f; // 탭이 움직이는 최대 거리
    public bool is4DPad = false; // 상화자우로 움직이는지 여부
    GameObject player;
    Vector2 startLocation;
    Vector2 touchLocation;
    // Start is called before the first frame update
    void Start()
    {
        //player = GameObject.FindWithTag("Player");
        player = GameObject.FindGameObjectWithTag("Player");
        startLocation = GetComponent<RectTransform>().localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// 패드 눌렀을 때 마우스 포인트의 스크린 좌표
    /// </summary>
    public void PadDown()
    {
        touchLocation = Input.mousePosition;
    }


    public void PadDrag()
    {
        Vector2 mousePosition = Input.mousePosition;
        Vector2 newTabPosition = mousePosition - touchLocation;

        if (is4DPad == false)
        {
            newTabPosition.y = 0; // 횡스크롤 게임에는 Y를 0
        }
        Vector2 axis = newTabPosition.normalized; // 벡터 정규화
        float distance = Vector2.Distance(startLocation, newTabPosition);
        if(distance > maxLength)
        {
            newTabPosition.x = axis.x * maxLength;
            newTabPosition.y = axis.y * maxLength;
            //두 점 사이의 거리를 구한 후 한계 거리를 넘기면 한계 좌표로 설정
        }
        //탭 이동
        
        GetComponent<RectTransform>().localPosition = newTabPosition;
        PlayerController playerController = player.GetComponent<PlayerController>();
        playerController.SetAxis(axis.x, axis.y);
    }

    /// <summary>
    /// 패드 떼면 패드의 위치를 시작 위치로, 플레이어는 안 움직이게
    /// </summary>
    public void PadUp()
    {
        GetComponent<RectTransform>().localPosition = startLocation;
        PlayerController playerController = player.GetComponent<PlayerController>();
        playerController.SetAxis(0, 0);
    }
}
