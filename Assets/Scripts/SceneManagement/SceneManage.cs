using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour {

    [SerializeField] float waitTime = 5f;
    [SerializeField] float slowTime = 0.5f;
    [SerializeField] Interface _interface;
    [SerializeField] GameObject startingFloor;
    [SerializeField] List<GameObject> levelPrefabs = new List<GameObject>();

    List<GameObject> _loadedLevels = new List<GameObject>();
    public List<GameObject> LoadedLevels => _loadedLevels;

    // Loaded floor Object / isShown
    Dictionary<GameObject, bool> _shownFloors = new Dictionary<GameObject, bool>();

    void Start()
    {
        LoadedLevels.Add(startingFloor);
        _shownFloors.Add(startingFloor, true);
    }

    public void ProcessDeath()
    {
        StartCoroutine(WaitAndReset());
    }

    IEnumerator WaitAndReset()
    {
        WaitForSeconds wait = new WaitForSeconds(waitTime);
        _interface.DisplayDeathText(waitTime);
        yield return wait;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public bool LoadNextFloor(GameObject nextFloor)
    {
        GameObject nextFloorObj = Instantiate(nextFloor);
        _loadedLevels.Add(nextFloorObj);
        _shownFloors.Add(nextFloorObj, true);
        return true;
    }

    public bool TogglePreviousFloor(int index)
    {
        GameObject previousFloor = _loadedLevels[index - 1];
        Debug.Log($"Toggle Floor: {previousFloor}");
        if (previousFloor)
        {
            bool showPreviousFloor = _shownFloors[previousFloor];
            foreach (Transform child in previousFloor.transform)
            {
                if (child.name != "FloorTrigger")
                {
                    child.gameObject.SetActive(!showPreviousFloor);
                }
            }
            _shownFloors[previousFloor] = !showPreviousFloor;
        }
        if (LoadedLevels.Count > index + 2)
        {
            GameObject previousPreviousFloor = LoadedLevels[index - 2];
            if (previousPreviousFloor)
            {
                bool showPreviousPreviousFloor = _shownFloors[previousPreviousFloor];
                foreach (Transform child in previousPreviousFloor.transform)
                {
                    if (child.name == "FloorTrigger")
                    {
                        child.gameObject.SetActive(!showPreviousPreviousFloor);
                    }
                }
                _shownFloors[previousPreviousFloor] = !showPreviousPreviousFloor;
            }
        }
        return true;
    }

    public bool ToggleNextFloor(int index)
    {
        GameObject nextFloor = _loadedLevels[index];
        Debug.Log($"Toggle Floor: {nextFloor}");
        if (nextFloor)
        {
            bool showPreviousFloor = _shownFloors[nextFloor];
            foreach (Transform child in nextFloor.transform)
            {
                if (child.name != "FloorTrigger")
                {
                    child.gameObject.SetActive(!showPreviousFloor);
                }
            }
            _shownFloors[nextFloor] = !showPreviousFloor;
        }
        return true;
    }
}
