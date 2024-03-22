using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchAction : MonoBehaviour
{
    public GameObject targetMoveBlock;
    public Sprite imageOn;
    public Sprite imageOff;
    public bool switchOnOff;
    // Start is called before the first frame update
    void Start()
    {
        if (switchOnOff)
        {
            GetComponent<SpriteRenderer>().sprite = imageOn;
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = imageOff;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (switchOnOff)
            {
                switchOnOff = false;
                GetComponent<SpriteRenderer>().sprite = imageOff;
                MovingBlock movingBlock = targetMoveBlock.GetComponent<MovingBlock>();
                movingBlock.MoveOff();
            }
            else
            {
                switchOnOff = true;
                GetComponent<SpriteRenderer>().sprite = imageOff;
                MovingBlock movingBlock = targetMoveBlock.GetComponent<MovingBlock>();
                movingBlock.MoveOn();
            }
        }
    }
}
