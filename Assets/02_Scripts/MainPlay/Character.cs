using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float speed;     // 속도
    public float jumpPower; // 점프
    bool isGround;
    Rigidbody playerRigid;
    public BoxCollider myCollider;
    public Transform direction;
    public Transform playerTr;

    bool isTimer;
    float time;

    void Start()
    {
        speed = 5;
        isTimer = false;
        Cursor.visible = false;
        playerRigid = GetComponent<Rigidbody>();
    }

    void Update()
    {
        move();
        jump();
        lookAround();
        mouseClick();
        speedChange(speedChangeTimer());
    }

    void move()
    {
        Vector3 forward = direction.forward;
        Vector3 temp = Vector3.zero;
        if (Input.GetKey(KeyCode.W)) temp += forward; // 앞
        if (Input.GetKey(KeyCode.S)) temp -= forward;    // 뒤
        if (Input.GetKey(KeyCode.A)) temp -= direction.transform.right;    // 왼쪽
        if (Input.GetKey(KeyCode.D)) temp += direction.transform.right;   // 오른쪽 
        if(Vector3.Distance(temp, Vector3.zero) >= 0.1f)
        {
            Vector3 dir = temp * speed;
            dir.y = playerRigid.velocity.y;
            playerRigid.velocity = dir;
        }
        
    }

    void jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGround) playerRigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
    }
    private void OnTriggerStay(Collider other)
    {
        isGround = true;
    }
    private void OnTriggerExit(Collider other)
    {
        isGround = false;
    }

    void lookAround()
    {
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"))*2;
        Vector3 camAngle = playerTr.rotation.eulerAngles;
        float x = camAngle.x - mouseDelta.y;
        if(x < 180f)
        {
            x = Mathf.Clamp(x, -1f, 80f);
        }
        else
        {
            x = Mathf.Clamp(x, 280f, 361f);
        }
        playerTr.rotation = Quaternion.Euler(x, camAngle.y + mouseDelta.x, camAngle.z);
        direction.rotation = Quaternion.Euler(0, camAngle.y + mouseDelta.x, camAngle.z);
        
        
        
    }

    void mouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2));
            RaycastHit hit;
            if (Inventory.isHold)
            {
                if (Inventory.holdItem == "apple")
                {
                    GameManager.instance.hp += 10;
                    Inventory.isHold = false;
                    Inventory.stuffName.RemoveAt(Inventory.holdNumber-1);
                    Inventory.holdNumber = 0;
                }
                if (Inventory.holdItem == "cookie")
                {
                    GameManager.instance.hp += 10;
                    Inventory.isHold = false;
                    Inventory.stuffName.RemoveAt(Inventory.holdNumber-1);
                    Inventory.holdNumber = 0;
                }
                if (Inventory.holdItem == "water")
                {
                    GameManager.instance.hp += 5;
                    time = 5;
                    isTimer = true;
                    Inventory.isHold = false;
                    Inventory.stuffName.RemoveAt(Inventory.holdNumber - 1);
                    Inventory.holdNumber = 0;
                }
                if (Inventory.holdItem == "pie")
                {
                    GameManager.instance.hp += 30;
                    Inventory.isHold = false;
                    Inventory.stuffName.RemoveAt(Inventory.holdNumber - 1);
                    Inventory.holdNumber = 0;
                }


                GameObject.Find("Inventory").GetComponent<Inventory>().setItem();
            }
            else
            {
                if (Physics.Raycast(ray, out hit))
                {

                    GameObject obj = hit.collider.gameObject;
                    if (Mathf.Abs(Vector3.Distance(transform.position, obj.transform.position)) < 3.5f) clickedObj(obj);
                }
            } 
        }
    }

    void clickedObj(GameObject obj)
    {
        if(obj.tag == "Drawer")
        {
            for(int i=0;i<3;i++) obj = obj.transform.parent.gameObject;
            Drawer drawer = obj.GetComponent<Drawer>();
            drawer.changedIsOpen();
            return;
        }
        else if (obj.tag == "Door")
        {
            obj = obj.transform.parent.gameObject;
            Door door = obj.GetComponent<Door>();
            door.changedIsOpen();
            return;
        }





        if (Inventory.stuffName.Count < 6) // 아이템인 경우 
        {
            if (obj.tag == "Apple")
            {
                GameObject.Find("Inventory").GetComponent<Inventory>().getItem("apple");
                Destroy(obj);
            }
            else if(obj.tag == "Cookie")
            {
                GameObject.Find("Inventory").GetComponent<Inventory>().getItem("cookie");
                Destroy(obj);
            }
            else if (obj.tag == "Water")
            {
                GameObject.Find("Inventory").GetComponent<Inventory>().getItem("water");
                Destroy(obj);
            }
            else if (obj.tag == "Pie")
            {
                GameObject.Find("Inventory").GetComponent<Inventory>().getItem("pie");
                Destroy(obj.transform.parent.gameObject);
            }

            obj.GetComponent<Collider>().enabled = false;
        }
        
    }

    void speedChange(bool isSpeedUp)
    {
        if (!isSpeedUp) speed = 5;
        else speed = 9f;
    }

    bool speedChangeTimer()
    {
        if (isTimer)
        {
            time -= Time.deltaTime;
            if (time <= 0)
            {
                isTimer = false;
                return false;
            }
            else return true;
        }
        else return false;
    }
}
