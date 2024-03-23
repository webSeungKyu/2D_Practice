using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    public GameObject shotObject;
    public float delayTime = 3.0f;
    public float shotX = 4.0f;
    public float shotY = 0.0f;
    public float length = 8.0f;

    GameObject player;
    GameObject gate;
    float shotTime;


    // Start is called before the first frame update
    void Start()
    {
        gate = transform.Find("Shot").gameObject;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        shotTime += Time.deltaTime;
        if (CheckLength(player.transform.position))
        {
            if(shotTime > delayTime)
            {
                //대포 발사
                shotTime = 0;
                Vector3 pos = new Vector3(gate.transform.position.x, gate.transform.position.y, transform.position.z);

                //Prefab으로 GameObject 만들기
                GameObject gameObject = Instantiate(shotObject, pos, Quaternion.identity);
                //발사 방향
                Rigidbody2D rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
                Vector2 vector2 = new Vector2(shotX, shotY);
                rigidbody2D.AddForce(vector2, ForceMode2D.Impulse);
            }
        }
    }

    bool CheckLength(Vector2 targetPos)
    {
        if(length >= Vector2.Distance(transform.position, targetPos))
        {
            return true;
        }

        return false;
    }
}
