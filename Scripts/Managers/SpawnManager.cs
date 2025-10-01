using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _enemy;
    [SerializeField] private Vector2 _xSpawnRange;
    [SerializeField] private float _ySpawnPos;
    [SerializeField] private float _zSpawnPos;

    private void Start()
    {
        GameManager.Instance.TimeManager.OnTickEvent += SpawnEnemy;
    }

    private void SpawnEnemy()
    {
        GameObject g = Instantiate(_enemy);
        g.GetComponent<Enemy>().Target = GameManager.Instance.playerManager.gameObject;
        g.transform.position = new(Random.Range(_xSpawnRange.x, _xSpawnRange.y), _ySpawnPos, _zSpawnPos);
    }
}
