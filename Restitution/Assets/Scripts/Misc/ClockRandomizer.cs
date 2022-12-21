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
    private int hour;
    private int minutes;

    void Start()
    {
        minutes = Random.Range(0, 60);
        hour = Random.Range(1, 13);

        minuteHand = transform.Find("MinuteHand");
        hourHand = transform.Find("HourHand");
    }

    public void setTime()
    {
        clockInt = hour * 100 + minutes;

        minuteHand.localRotation = Quaternion.Euler(0, 6 * minutes, 0);
        hourHand.localRotation = Quaternion.Euler(0, 90 + (hour * 30) + (minutes * 0.5f), 0);
    }
}
