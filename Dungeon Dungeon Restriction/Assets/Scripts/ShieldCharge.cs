using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldCharge : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Color startColor;
    private float maxChargeAlpha;
    private float maxCharge = 5;
    private float timeOfDisable;
    private float currentCharge;

    public float chargeRate;
    public float dischargeRate;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        maxChargeAlpha = spriteRenderer.color.a;
        startColor = spriteRenderer.color;
        currentCharge = maxCharge;
    }
    // Start is called before the first frame update
    private void OnEnable()
    {
        currentCharge += (Time.time - timeOfDisable) * chargeRate;
        if (currentCharge > maxCharge)
        {
            currentCharge = maxCharge;
        }
    }

    // Update is called once per frame
    void Update()
    {
        currentCharge -= dischargeRate * Time.deltaTime;
        startColor.a = (currentCharge / maxCharge) * maxChargeAlpha;
        spriteRenderer.color = startColor;
        if (currentCharge <=0)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        timeOfDisable = Time.time;
    }
}
