using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalState : State
{
    public NormalState(CarController _carController) : base(_carController)
    {
        carController = _carController;
    }
    public override IEnumerator Drive()
    {
        carController.speedInput = Input.GetAxis("Vertical") * carController.acceleration * 1000f;
        return base.Drive();
    }
    public override IEnumerator Break()
    {
        carController.speedInput = Input.GetAxis("Vertical") * carController.backAccel * 1000f;
        return base.Drive();
    }
}
