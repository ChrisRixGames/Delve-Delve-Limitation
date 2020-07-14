using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject levelHole;
    public List<GameObject> enemies = new List<GameObject>();
    public ControlLock controlLock;
    public ControlLock.Locks defaultLock;
    // Start is called before the first frame update
    void Start()
    {
        if (defaultLock.control != ControlLock.LockedControl.None)
        {
            controlLock.SetDefaultLocks(defaultLock);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (enemies.Count == 0)
        {
            levelHole.SetActive(true);
        }
        enemies.RemoveAll(item => item == null);
    }
}
