using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;

public abstract class Turret<BehaviourT> :  Turret,IUpdatable
    where BehaviourT : BehaviourTower
{
    [SerializeField] private LayerMask _enemyLayer;
    public LayerMask EnemyLayer => _enemyLayer;
    protected BehaviourT _behaviourTower;
    [SerializeField] private TurretController<BehaviourT> _turret;
    private float _lastToShoot;
    private GameTowerFactory _towerFactory;
    protected ITowerUpgradeVisitor _visitor;
    [SerializeField]private UpgradeTileUI _upgradeTileUI;
    private TowerInfoLoader _towerInfoLoader;
    private Func<int, bool> _enoughMoney;
    public Turret<BehaviourT> Initialize(
        GameTowerFactory towerFactory,
        TowerInfoLoader towerInfoLoader,
        Func<int,bool> enoughMoney,
        ITowerUpgradeVisitor visitor)
    {
        _enoughMoney = enoughMoney;
        _towerInfoLoader = towerInfoLoader;
        towerFactory.Add(this);
        _visitor = visitor;
        _towerFactory = towerFactory;
        _behaviourTower = _towerInfoLoader.GetBehaviour<BehaviourT>(name.Replace("(Clone)",""));
        _turret.Initialize(this,_behaviourTower);
        _lastToShoot = _behaviourTower.SpeedFire;
        _upgradeTileUI.OnClick(UpgradeTower);
        _upgradeTileUI.SetPrice(_towerInfoLoader.GetPrice( name.Replace("(Clone)","") ));
        _upgradeTileUI.SetEvent(enoughMoney);
        return this;
    }

    public Turret<BehaviourT> Initialize(Turret<BehaviourT> tower)
    {
        Initialize(tower._towerFactory,tower._towerInfoLoader, tower._enoughMoney, _visitor);
        return this;
    }

    private void OnEnable() => _turret.enabled = true;

    private void OnDisable() => _turret.enabled = false;

    // ReSharper disable Unity.PerformanceAnalysis
    void IUpdatable.UpdateEntity()
    {
        if (!_turret.enabled)
            return;
        _turret.PursueTarget();
        if (_lastToShoot <= 0)
            _lastToShoot = _turret.Shoot() ? _behaviourTower.SpeedFire : 0;
        _lastToShoot -= Time.deltaTime;
    }

    private void OnDestroy()
    {
        _towerFactory.Remove(this);
    }

    private void OnValidate() => Serialize();
    
    private void Serialize()
    {
        var path = PathCollection.PATH_TO_TOWERS + name + ".json";
        var value = JsonExtension.GetClassFromJson<TowerData<BehaviourT>>(path);
        if (value == null)
            JsonExtension.SerializeClass(new TowerData<BehaviourT>(name,0,null),PathCollection.PATH_TO_TOWERS + name + ".json");
    }
}
