using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockRandomizer : MonoBehaviour
{
    // Start is called before the first frame update
    public int clockInt;
    
    private Transform minuteHand;
    private Transform hourHand;
    private int clockAngle;

    void Start()
    {
        clockAngle = Random.Range(0, 361);
        minuteHand = transform.Find("MinuteHand");
        hourHand = transform.Find("HourHand");

        hourHand.localRotation = Quaternion.Euler(0, 180, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
