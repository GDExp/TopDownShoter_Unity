using UnityEngine;
using Character;

[RequireComponent(typeof(Rigidbody))]
public class BaseProjectile : MonoBehaviour , IProjectile
{
    private Transform _transform;
    private Rigidbody _rigidbody;
    private AbstractCharacter _owner;
    
    private float _lifeTime;

    private void Awake()
    {
        _transform = transform;
        _rigidbody = GetComponent<Rigidbody>();
        _lifeTime = Time.time + 5f;

        GameCore.GameController.Instance.AddProjectileInList(this);
    }

    private void OnCollisionEnter(Collision collision)
    {
        HitSomething();
    }

    public void AddProjectileForce(float force)
    {
        _rigidbody.velocity = _transform.forward * force;
    }

    public bool CheckLifeTime()
    {
        return Time.time >= _lifeTime;
    }

    public void DestroyProjectile()
    {
        //to do - return n objects pool
        //test
        Destroy(gameObject);
    }

    public void HitSomething()
    {
        Debug.Log("HIT!");
        GameCore.GameController.Instance.RemoveProjectileInList(this);
        ReturnInGamePool();
    }

    public void SetProjectleOwner(AbstractCharacter owner)
    {
        _owner = owner;
    }

    private void ReturnInGamePool()
    {
        ResetProjectile();
    }

    private void ResetProjectile()
    {
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
    }
}
