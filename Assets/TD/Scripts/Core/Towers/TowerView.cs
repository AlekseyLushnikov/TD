using UnityEngine;

public class TowerView : MonoBehaviour
{
    [SerializeField] private Transform _bulletSpawnPoint;
    [SerializeField] private Transform _turret;
    public Transform BulletSpawnPoint => _bulletSpawnPoint;
    public Transform Turret => _turret;
}