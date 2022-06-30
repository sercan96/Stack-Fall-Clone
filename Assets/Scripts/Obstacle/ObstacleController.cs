using System.Collections;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    [SerializeField]
    private Obstacle[] _obstacles = null;

    public void ShatterAllObstacles()
    {
        if (transform.parent != null)
        {
            transform.parent = null;
        }

        foreach (Obstacle item in _obstacles)
        {
            item.Shatter();
        }

        StartCoroutine(RemoveAllShattterPart(1));
    }

    IEnumerator RemoveAllShattterPart(int waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Destroy(gameObject); // obje parçalandıktan 1 saniye sonra yok et.
    }


}
