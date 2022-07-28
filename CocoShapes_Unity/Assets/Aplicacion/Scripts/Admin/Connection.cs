using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Connection : MonoBehaviour
{
    public Text txtStatus;
    public Image disconnected;
    public Image connected;

    private int cont = 0;
    
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow) && cont == 0)
        {
            txtStatus.text = "Connected";
            txtStatus.color = Color.green;

            disconnected.gameObject.SetActive(false);
            connected.gameObject.SetActive(true);

            cont++;
        }
    }

    void OnDisable()
    {
        cont = 0;
    }
}
