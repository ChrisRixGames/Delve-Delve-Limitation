using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using System;

public class ControlLock : MonoBehaviour
{
    public enum LockedControl
    {
        None = 0,
        Up = 1,
        Left = 2,
        Right = 3,
        Down = 4,
        Melee = 5,
        Ranged = 6,
        Shield = 7
    }

    [System.Serializable]
    public struct Locks
    {
        public Locks(LockedControl lockedControl, float timer, bool locked)
        {
            control = lockedControl;
            this.timer = timer;
            this.locked = locked;

        }
        public LockedControl control;
        public float timer;
        public bool locked;
    }

    public Image[] images;
    public Locks lockedControl;
    public PlayerController player;
    public LockCountdown countdown;

    private float timer = 3f;
    
    private List<Locks> locks = new List<Locks>();
    private List<Locks> randomLocks = new List<Locks>();
    private Locks defaultLocks;
    private int locksIterator;
    private bool twin;
    // Start is called before the first frame update
    void Start()
    {
        Locks l;
        l.control = LockedControl.None;
        l.timer = 5;
        l.locked = true;

        lockedControl = l;

        //TestLocks();
        Locks dl;
        dl.control = LockedControl.Melee;
        dl.timer = 0;
        dl.locked = false;
        //SetDefaultLocks(dl);

        for (int i = 0; i < 5; i++)
        {
            AddRandomLocks();
        }
        if (locks.Count == 0)
        {
            timer = randomLocks[0].timer;
        }
        else
        {
            timer = locks[0].timer;
        }
        countdown.UpdatePreps();
    }
    private void AddRandomLocks()
    {
        Locks l;
        l.control = (LockedControl)UnityEngine.Random.Range(0, images.Length - 1) + 1;
        if (l.control == defaultLocks.control)
        {
            AddRandomLocks();
            return;
        }
        l.timer = 3f;
        l.locked = true;

        randomLocks.Add(l);
    }

    public void SetDefaultLocks(Locks l)
    {
        defaultLocks = l;
    }

    public Locks GetDefaultLocks()
    {
        int imageToLock = (int)defaultLocks.control - 1;
        if (imageToLock >= 0)
        {
            Color lockColor = defaultLocks.locked ? Color.red : Color.green;
            images[imageToLock].color = lockColor;
        }
        return defaultLocks;
    }

    private void TestLocks()
    {
        Locks none;
        none.control = LockedControl.None;
        none.timer = 5;
        none.locked = true;

        AddLocks(none);

        Locks up;
        up.control = LockedControl.Up;
        up.timer = 5;
        up.locked = true;

        AddLocks(up);

        Locks left;
        left.control = LockedControl.Left;
        left.timer = 0;
        left.locked = true;

        AddLocks(left);

        Locks right;
        right.control = LockedControl.Right;
        right.timer = 5;
        right.locked = true;

        AddLocks(right);

        Locks down;
        down.control = LockedControl.Down;
        down.timer = 5;
        down.locked = true;

        AddLocks(down);
    }

    public void AddLocks(Locks controlLocks)
    {
        locks.Add(controlLocks);
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            UnlockControls();

            if (locks.Count == 0)
            {
                randomLocks.RemoveAt(0);
                AddRandomLocks();
                timer += randomLocks[0].timer;
            }
            else
            {
                if (twin)
                {
                    locksIterator += 2;
                }
                else
                {
                    locksIterator++;
                }
                locksIterator %= locks.Count;
                if (locks[locksIterator].timer == 0)
                {
                    twin = true;
                    timer += locks[locksIterator + 1].timer;
                }
                else
                {
                    twin = false;
                    timer += locks[locksIterator].timer;
                }
                
            }
            countdown.UpdatePreps();

        }
    }

    private void UnlockControls()
    {
        foreach(Image control in images)
        {
            control.color = Color.white;
        }
        lockedControl.control = LockedControl.None;
    }

    public Locks GetNextLocks(ref int next)
    {
        int nextLocksIterator = locksIterator;

        if (locks.Count == 0)
        {
            next = 0;
            int imageToLock = (int)randomLocks[0].control - 1;
            if (imageToLock >= 0)
            {
                Color lockColor = lockedControl.locked ? Color.red : Color.green;
                images[imageToLock].color = lockColor;
            }            
            return randomLocks[0];
        }
        else
        {
            if (next == 1)
            {
                nextLocksIterator += 1;
            }
            if (locks[nextLocksIterator].timer == 0)
            {
                next = 1;
            }
            else
            {
                next = 0;
            }
            int imageToLock = (int)locks[nextLocksIterator].control - 1;
            if (imageToLock >= 0)
            {
                Color lockColor = locks[nextLocksIterator].locked ? Color.red : Color.green;
                images[imageToLock].color = lockColor;
            }
            return locks[nextLocksIterator];
        }
    }

    public Locks GetLocks(int lockIterator)
    {
        if (locks.Count == 0)
        {
            return randomLocks[lockIterator];
        }
        else
        {
            int i = locksIterator + lockIterator;
            i %= locks.Count;
            return locks[i];
        }
    }

    public bool SameAsCurrentIterator(int i)
    {
        return i == locksIterator;
    }
}
