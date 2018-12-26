using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EntityManager{
    private static EntityManager _Instance;
    public static EntityManager Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = new EntityManager();
            }
            return _Instance;
        }
    }

    public List<Enermy> enermyList = new List<Enermy>();


    public T CreateEntity<T>(string entityName, Vector3 pos, Vector3 dir) where T : Entity
    {
        GameObject loadobj = GameManager.Instance.LoadResources<GameObject>(entityName);
        GameObject obj = GameObject.Instantiate(loadobj);
        T entity = obj.GetComponent<T>();
        entity.InitEntity(pos, dir);
        if (entity == null)
        {
            obj.AddComponent<T>();
        }
        return (T)entity;
    }

    public Enermy CreateEnermy(string entityName,Vector3 pos,Vector3 dir)
    {
        Enermy enermy = CreateEntity<Enermy>(entityName, pos, dir);
        enermyList.Add(enermy);
        return enermy;
    }

    public void removeEnermy(Enermy enermy)
    {
        if(!enermyList.Contains(enermy))
        {
            return;
        }
        enermyList.Remove(enermy);
    }
}
