using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GimmickBlock : MonoBehaviour
{
    bool isFell = false; // ���� �÷���
    float fadeTime = 4.2f; // ���̵� �ƿ� �ð�

    [Header ("�ߵ��Ÿ� / ���� ����")]
    public float length = 0.0f; // �ڵ� ���� Ž�� �Ÿ�
    public bool isDelete = false; //���� �� �������� ����



    
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
    /// Collider�� is Trigger�� üũ���� �ʾ��� �� �ٸ� �����ΰ��� �����ϸ� ȣ��Ǵ� �޼���.
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //PlayerController���� ���� ������ ������� ���� �� ������, �ϴ� ����� �ʿ����� ����.
        //�� ���� Circle �� ���(�׷��� �Ų����� �̵���) �ϰ� Deadó�� Destroy �±׸� ���� �ٿ��� �ߵ��ǰ� ���� ����
        if (isDelete)
        {
            isFell = true;
        }


    }

}
