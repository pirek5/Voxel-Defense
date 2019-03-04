using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    //config
    [SerializeField] public float movementSpeed = 10f;

    //cached
    private Pathfinder pathfinder;

    //state
    private List<PathCube> currentPath;
    private bool findNewPath = false;

    void Start ()
    {
        pathfinder = GetComponent<Pathfinder>();
        FindAndFollowPath();
    }

    void FindAndFollowPath()
    {   
        var nearestPathNode = PathCubesData.Instance.FindNearestPathCube(gameObject.transform.position);
        currentPath = pathfinder.FindPath(nearestPathNode);
        StartCoroutine(FollowPath(currentPath));
    }

    IEnumerator FollowPath(List<PathCube> path)
    {
        PathCube currentTargetPathCube;
        foreach (PathCube pathElement in path)
        {   
            currentTargetPathCube = pathElement;
            float t1 = Time.time;  // służy do sprawdzenia czy obiekt nie zablokowal sie zbyt dlugo w jednym miejscu
            while (currentTargetPathCube.occupupiedBy != null && currentTargetPathCube.occupupiedBy != this.gameObject)
            {
                float maxIdleTime = 2f;
                float t2 = Time.time;
                if (t2 - t1 >= maxIdleTime) // wyszukanie nowej ścieżki
                {
                    StopAllCoroutines();
                    FindAndFollowPath();
                }
                yield return null;
            }
            pathElement.occupupiedBy = this.gameObject;
            Vector3 destination = pathElement.transform.position;
            float distance = Vector3.Distance(this.transform.position, destination);
            float timeBetweenTwoWaypoints = distance / movementSpeed;
            StartCoroutine(SmoothMovement(destination, timeBetweenTwoWaypoints));
            yield return new WaitForSeconds(timeBetweenTwoWaypoints);
        }
        GoalReached();     
    }

    public IEnumerator SmoothMovement(Vector3 endPosition, float timeToMove)
    {
        var currentPosition = this.transform.position;
        var fractionOfJourney = 0f;
        while (fractionOfJourney < 1)
        {
            fractionOfJourney += Time.deltaTime / timeToMove;
            transform.position = Vector3.Lerp(currentPosition, endPosition, fractionOfJourney);
            yield return null;
        }
        if (findNewPath)
        {
            findNewPath = false;
            StopAllCoroutines();
            FindAndFollowPath();
        }
    }

    void GoalReached()
    {
        EnemiesController.Instance.RemoveEnemyFromDictionary(this.gameObject);
        FriendlyBase.Instance.LoseHealth(); // TODO zmienic z magicznej liczby + nie pasuje do klasy
        GetComponent<EnemyParticleManager>().GoalExplosion();
        Destroy(gameObject);
    }

    public void FindNewPath(PathCube pathCubeBlocked)
    {
        if (currentPath.Contains(pathCubeBlocked))
        {
            findNewPath = true;
        }
    }

    public void FindNewPath()
    {
            findNewPath = true;
    }

    public void StopMovement()
    {
        StopAllCoroutines();
    }
}
