using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drawer : MonoBehaviour
{
    public Transform drawer;
    [SerializeField]
    Transform whenOpened;
    private bool isOpen, isCoroutine;
    private int num;

    void Start()
    {
        isOpen = false;
        isCoroutine = false;
        num = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (isOpen) drawerOpen();
        else drawerClose();
        
    }

    public void drawerOpen()
    {
        drawer.position = Vector3.Lerp(drawer.transform.position, whenOpened.position, 5 * Time.deltaTime);
    }

    public void drawerClose()
    {
        drawer.position = Vector3.Lerp(drawer.transform.position, transform.position, 5 * Time.deltaTime);
    }

    public void changedIsOpen()
    {
        isOpen = !isOpen;
        if (isOpen)
        {
            num = 0;
            if(!isCoroutine) StartCoroutine(autoClose());
        }
    }
    IEnumerator autoClose()
    {
        isCoroutine = true;
        while (num < 5)
        {
            yield return new WaitForSeconds(1);
            if (!isOpen) break;
            num++;
        }
        if (num == 5) isOpen = false;
        isCoroutine = false;
    }
}
