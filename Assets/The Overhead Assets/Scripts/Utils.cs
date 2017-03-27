using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class Utils{
    public static bool CheckTags(GameObject obj, List<string> tags)
    {
        foreach (var tag in tags)
        {
            if (obj.CompareTag(tag))
            {
                return true;
            }
        }
        return false;
    }
}
