using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockCountdown : MonoBehaviour
{
    public GameObject[] prepCountdowns;
    public GameObject[] iconCountdowns;
    public ControlLock controlLock;

    private float firstX = 25;
    private float secondX  = 50;
    private float thirdX = 70;
    private float defaultX = 100;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdatePreps()
    {
        ResetPreps();
        StartCountdowns();

        int lockToGet = 2;
        ControlLock.Locks currentLock = controlLock.GetLocks(0);
        for (int i = 0; i < 3; i++)
        {
            ControlLock.Locks previousLock = controlLock.GetLocks(lockToGet - 1);
            
            if (previousLock.timer != 0 && !(currentLock.timer == 0 && i ==0))
            {
                ControlLock.Locks locks = controlLock.GetLocks(lockToGet);
                SetPrepPos(locks, i);
                if (locks.timer == 0)
                {
                    ControlLock.Locks nextlocks = controlLock.GetLocks(++lockToGet);
                    SetPrepPos(nextlocks, i);
                }
                lockToGet++;
            }
            else
            {
                lockToGet += 1;
                ControlLock.Locks locks = controlLock.GetLocks(lockToGet);
                SetPrepPos(locks, i);
                if (locks.timer == 0)
                {
                    ControlLock.Locks nextlocks = controlLock.GetLocks(++lockToGet);
                    SetPrepPos(nextlocks, i);
                }
                lockToGet++;
            }
        }
    }

    private void StartCountdowns()
    {
        int lockToGet = 1;
        ControlLock.Locks previousLocks = controlLock.GetLocks(0);
        if (previousLocks.timer == 0)
        {
            lockToGet++;
        }
        ControlLock.Locks locks = controlLock.GetLocks(lockToGet);
        
        if (locks.timer == 0)
        {
            ControlLock.Locks nextlocks = controlLock.GetLocks(++lockToGet);
            Countdown(nextlocks, nextlocks.timer);
            Countdown(locks, nextlocks.timer);
        }
        else
        {
            Countdown(locks, locks.timer);
        }
    }

    private void Countdown(ControlLock.Locks locks, float time)
    {
        switch (locks.control)
        {
            case ControlLock.LockedControl.None:
                break;
            case ControlLock.LockedControl.Up:
                iconCountdowns[0].SetActive(true);
                iconCountdowns[0].GetComponent<MoveToIcon>().SetTime(time);
                break;
            case ControlLock.LockedControl.Left:
                iconCountdowns[1].SetActive(true);
                iconCountdowns[1].GetComponent<MoveToIcon>().SetTime(time);
                break;
            case ControlLock.LockedControl.Right:
                iconCountdowns[2].SetActive(true);
                iconCountdowns[2].GetComponent<MoveToIcon>().SetTime(time);
                break;
            case ControlLock.LockedControl.Down:
                iconCountdowns[3].SetActive(true);
                iconCountdowns[3].GetComponent<MoveToIcon>().SetTime(time);
                break;                                               
            case ControlLock.LockedControl.Melee:                    
                iconCountdowns[4].SetActive(true);                   
                iconCountdowns[4].GetComponent<MoveToIcon>().SetTime(time);
                break;                                               
            case ControlLock.LockedControl.Ranged:                   
                iconCountdowns[5].SetActive(true);                  
                iconCountdowns[5].GetComponent<MoveToIcon>().SetTime(time);
                break;                                               
            case ControlLock.LockedControl.Shield:                   
                iconCountdowns[6].SetActive(true);                   
                iconCountdowns[6].GetComponent<MoveToIcon>().SetTime(time);
                break;
            default:
                break;
        }
    }

    private void ResetPreps()
    {
        foreach (GameObject gameObject in prepCountdowns)
        {
            Vector2 newPos = gameObject.GetComponent<RectTransform>().anchoredPosition;
            newPos.x = defaultX;
            gameObject.GetComponent<RectTransform>().anchoredPosition = newPos;
        }
    }

    private void SetPrepPos(ControlLock.Locks locks, int i)
    {
        float posX;

        switch (i)
        {
            case 0:
                posX = firstX;
                break;
            case 1:
                posX = secondX;
                break;
            case 2:
                posX = thirdX;
                break;
            default:
                posX = 40;
                break;
        }

        Vector2 newPos = new Vector2();

        switch (locks.control)
        {
            case ControlLock.LockedControl.None:
                break;
            case ControlLock.LockedControl.Up:
                newPos = prepCountdowns[0].GetComponent<RectTransform>().anchoredPosition;
                newPos.x = posX;
                prepCountdowns[0].GetComponent<RectTransform>().anchoredPosition = newPos;
                break;
            case ControlLock.LockedControl.Left:
                newPos = prepCountdowns[1].GetComponent<RectTransform>().anchoredPosition;
                newPos.x = posX;
                prepCountdowns[1].GetComponent<RectTransform>().anchoredPosition = newPos;
                break;
            case ControlLock.LockedControl.Right:
                newPos = prepCountdowns[2].GetComponent<RectTransform>().anchoredPosition;
                newPos.x = posX;
                prepCountdowns[2].GetComponent<RectTransform>().anchoredPosition = newPos;
                break;
            case ControlLock.LockedControl.Down:
                newPos = prepCountdowns[3].GetComponent<RectTransform>().anchoredPosition;
                newPos.x = posX;
                prepCountdowns[3].GetComponent<RectTransform>().anchoredPosition = newPos;
                break;
            case ControlLock.LockedControl.Melee:
                newPos = prepCountdowns[4].GetComponent<RectTransform>().anchoredPosition;
                newPos.x = posX;
                prepCountdowns[4].GetComponent<RectTransform>().anchoredPosition = newPos;
                break;
            case ControlLock.LockedControl.Ranged:
                newPos = prepCountdowns[5].GetComponent<RectTransform>().anchoredPosition;
                newPos.x = posX;
                prepCountdowns[5].GetComponent<RectTransform>().anchoredPosition = newPos;
                break;
            case ControlLock.LockedControl.Shield:
                newPos = prepCountdowns[6].GetComponent<RectTransform>().anchoredPosition;
                newPos.x = posX;
                prepCountdowns[6].GetComponent<RectTransform>().anchoredPosition = newPos;
                break;
            default:
                break;
        }
    }
}
