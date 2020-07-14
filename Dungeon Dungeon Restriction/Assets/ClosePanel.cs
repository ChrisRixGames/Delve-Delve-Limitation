using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosePanel : MonoBehaviour
{

    public GameObject panel;

    public void Close()
    {
        panel.SetActive(false);
    }
}
