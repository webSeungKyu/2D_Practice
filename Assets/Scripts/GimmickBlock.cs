using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GimmickBlock : MonoBehaviour
{
    bool isFell = false; // 낙하 플래그
    float fadeTime = 4.2f; // 페이드 아웃 시간

    [Header ("발동거리 / 제거 여부")]
    public float length = 0.0f; // 자동 낙하 탐지 거리
    public bool isDelete = false; //낙하 후 제거할지 여부



    
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Static;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            float d = Vector2.Distance(transform.position, player.transform.position);
            if(length >= d)
            {
                Rigidbody2D rb = GetComponent<Rigidbody2D>();
                if(rb.bodyType == RigidbodyType2D.Static)
                {
                    rb.bodyType = RigidbodyType2D.Dynamic;
                }
                
            }
        }
        if(isFell)
        {
            fadeTime -= Time.deltaTime;
            Color color = GetComponent<SpriteRenderer>().color;
            color.a = fadeTime;
            GetComponent<SpriteRenderer>().color = color;
            if(fadeTime < 0.0f)
            {
                Destroy(gameObject);
            }
        }
    }



    /// <summary>
    /// Collider의 is Trigger가 체크되지 않았을 때 다른 무언인가와 접촉하면 호출되는 메서드.
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //PlayerController에서 위만 밟으면 사라지게 만들 수 있지만, 일단 현재는 필요하지 않음.
        //양 옆을 Circle 더 길게(그래야 매끄럽게 이동됨) 하고 Dead처럼 Destroy 태그를 위에 붙여서 발동되게 만들 예정
        if (isDelete)
        {
            isFell = true;
        }


    }

}
