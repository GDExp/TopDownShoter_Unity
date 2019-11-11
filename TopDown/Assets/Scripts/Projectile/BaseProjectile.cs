using System;
using UnityEngine;
using Character;
using GameCore;

[RequireComponent(typeof(Rigidbody))]
public class BaseProjectile : MonoBehaviour , IProjectile, IPoolableObject
{
    public AbstractCharacter _owner { get; private set; }

    private Transform _transform;
    private Rigidbody _rigidbody;
    
    private float _lifeTime;
    private int _damage;

    private void Awake()
    {
        _transform = transform;
        _rigidbody = GetComponent<Rigidbody>();

        //test
        _damage = 35;
    }

    private void OnCollisionEnter(Collision collision)
    {
        HitSomething();
    }

    public void HitSomething()
    {
        GameController.Instance.projectileModule.RemoveElementInList(this);
        ReturnInGamePool();
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

    public Type GetObjectType()
    {
        return GetType();
    }

    #region IProjectile

    public void SetupProjectile(AbstractCharacter owner)
    {
        _owner = owner;
        //to do 
        //_damage = ...
    }

    public void SetStartPosition(Transform point)
    {
        transform.position = point.position;
        transform.rotation = point.rotation;

        _lifeTime = Time.time + 5f;
        GameController.Instance.projectileModule.AddElementinList(this);
    }

    public void AddProjectileForce(float force)
    {
        _rigidbody.velocity = _transform.forward * force;
    }

    public bool CheckLifeTime()
    {
        return Time.time >= _lifeTime;
    }

    public void TakeDamage(AbstractCharacter invoker)
    {
        ICommand cmd = new AttackDamageCommand(invoker, _owner, _damage);
        cmd.Execute();
        DestroyProjectile();
    }

    public void DestroyProjectile()
    {
        GameController.Instance.objectsPool.ReturnObject(this);
    }

    #endregion

    #region IPoolableObject

    public GameObject GetObjectReference()
    {
        return this.gameObject;
    }
    
    public void EnablePoolObject()
    {
        gameObject.SetActive(true);
        transform.SetParent(null);
    }

    public void DisablePoolObject(Transform storage)
    {
        gameObject.SetActive(false);
        transform.SetParent(storage);
        transform.position = Vector3.zero;
    }

    #endregion
}
