using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsManager : MonoBehaviour {

    public void Init()
    {
        for (int indexI = 0; indexI < PointsManager.i.Length; indexI++)
        {
            points.Add(new List<Transform>());

            for (int indexJ = 0; indexJ < PointsManager.j.Length; indexJ++)
            {
                GameObject gO = new GameObject("point (z = " + PointsManager.i[indexI] + " : x = " + PointsManager.j[indexJ] + ")");
                gO.transform.SetParent(transform);
                gO.transform.position = new Vector3(PointsManager.j[indexJ], 0, PointsManager.i[indexI]);
                points[indexI].Add(gO.transform);
            }
        }
    }

    public List<List<Transform>> GetPoints()
    {
        return points;
    }

    #region private

    List<List<Transform>> points = new List<List<Transform>>();
    static float[] i = { -16, -8, 0, 8, 16 };
    static float[] j = { -30, -5, 18 };
    
        #endregion

}
