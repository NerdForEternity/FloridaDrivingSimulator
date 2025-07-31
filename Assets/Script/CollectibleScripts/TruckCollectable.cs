using UnityEngine;

public class TruckCollectable : Collectable
{
    public override void OnCollect()
    {
        ActionListener.OnTruckCollected();
    }
}
