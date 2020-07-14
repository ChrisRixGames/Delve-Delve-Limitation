using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToIcon : MonoBehaviour
{
    private Vector2 iconPosition;
    private Vector2 resetPosition;
    private RectTransform rectTransform;
    private float distance;
    private float timeToTravel;
    private float speed;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        iconPosition = rectTransform.anchoredPosition;
        resetPosition = iconPosition;
        resetPosition.x = 520;
        rectTransform.anchoredPosition = resetPosition;
        distance = resetPosition.x - iconPosition.x;
    }

    private void OnEnable()
    {        
        GetComponent<RectTransform>().anchoredPosition = resetPosition;
    }

    public void SetTime(float time)
    {
        timeToTravel = time;
        speed = distance / timeToTravel;
    }

    // Update is called once per frame
    void Update()
    {
        rectTransform.anchoredPosition = Vector2.MoveTowards(rectTransform.anchoredPosition, iconPosition, speed * Time.deltaTime);
        if (rectTransform.anchoredPosition == iconPosition)
        {
            gameObject.SetActive(false);
        }
    }
}
