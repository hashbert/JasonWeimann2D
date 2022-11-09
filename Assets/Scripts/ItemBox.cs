using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : HittableFromBelow
{
    [SerializeField] private GameObject _itemPrefab;
    [SerializeField] private GameObject _item;
    [SerializeField] private Vector3 _itemLaunchVelocity;
    private bool _used;

    private void Start()
    {
        if (_item != null)
        {
            _item.SetActive(false);
        }
    }
    protected override bool CanUse => _used == false ;

    protected override void Use()
    {
        Debug.Log("Used Itembox");
        _item = Instantiate(
            _itemPrefab, 
            transform.position + Vector3.up, 
            Quaternion.identity, 
            transform);
        base.Use();
        _used = true;
        _item.SetActive(true);
        var itemRigidbody = _item.GetComponent<Rigidbody2D>();
        if (itemRigidbody != null)
        {
            itemRigidbody.velocity = _itemLaunchVelocity;
        }
    }
}
