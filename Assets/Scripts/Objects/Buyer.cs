using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buyer : MonoBehaviour {

    [SerializeField] float speed;

    public void Init(PointsManager pointsManager)
    {
        points = pointsManager.GetPoints();

        SetupPoint(0, 1, currentPoint);
        SetupPoint(currentPoint.i, currentPoint.j, pastPoint);

        MAX_INDEX_J = points[0].Count - 1;
        MAX_INDEX_I = points.Count - 1;

        currentDirection = (transform.position - currentPoint.point.position).normalized;
    }

    #region private

    int MAX_INDEX_J;
    int MAX_INDEX_I;
    int LEFT = 0;
    int UP = 1;
    int RIGHT = 2;
    int DOWN = 3;

    List<List<Transform>> points;
    PointInMatrix currentPoint = new PointInMatrix();
    PointInMatrix pastPoint = new PointInMatrix();
    Vector3 currentDirection;

    void Move()
    {
        Vector3 newDirection = (transform.position - currentPoint.point.position).normalized;

        TryChangeDirection(newDirection);

        float newX = transform.position.x - currentDirection.x * Time.deltaTime * speed;
        float newZ = transform.position.z - currentDirection.z * Time.deltaTime * speed;

        transform.position = new Vector3(newX, 0, newZ);
    }

    void SetupPoint(int i, int j, PointInMatrix point)
    {
        point.i = i;
        point.j = j;
        point.point = points[i][j];
    }

    void TryChangeDirection(Vector3 newDir)
    {
        double x = System.Math.Round(currentDirection.x, 3);
        double z = System.Math.Round(currentDirection.z, 3);
        double newX = System.Math.Round(newDir.x, 3);
        double newZ = System.Math.Round(newDir.z, 3);

        if (x != newX || z != newZ)
        {
            ChangeCurrentPoint();
            currentDirection = (transform.position - currentPoint.point.position).normalized;
        }
    }

    void ChangeCurrentPoint()
    {
        int direction = Random.Range(0, 4);

        if (direction == LEFT)
        {
            int newJ = currentPoint.j - 1;

            if (currentPoint.j != 0 && pastPoint.j != newJ)
            {
                SetupPoint(currentPoint.i, currentPoint.j, pastPoint);
                SetupPoint(currentPoint.i, newJ, currentPoint);
                return;
            }
        }

        if (direction == RIGHT)
        {
            int newJ = currentPoint.j + 1;
            if (currentPoint.j != MAX_INDEX_J && pastPoint.j != newJ)
            {
                SetupPoint(currentPoint.i, currentPoint.j, pastPoint);
                SetupPoint(currentPoint.i, newJ, currentPoint);
                return;
            }
        }

        if (direction == UP)
        {
            int newI = currentPoint.i + 1;
            if (currentPoint.i != MAX_INDEX_I && pastPoint.i != newI)
            {
                SetupPoint(currentPoint.i, currentPoint.j, pastPoint);
                SetupPoint(newI, currentPoint.j, currentPoint);
                return;
            }
        }

        if (direction == DOWN)
        {
            int newI = currentPoint.i - 1;
            if (currentPoint.i != 0 && pastPoint.i != newI)
            {
                SetupPoint(currentPoint.i, currentPoint.j, pastPoint);
                SetupPoint(newI, currentPoint.j, currentPoint);
                return;
            }
        }

        ChangeCurrentPoint();
    }

    void Update()
    {
        Move();
    }

    #endregion

    class PointInMatrix
    {
        public int i;
        public int j;
        public Transform point;
    }

}
