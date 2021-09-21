using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowState : State
{
    public SlowState(CarController _carController) : base(_carController)
    {
        carController = _carController;
    }
    public override IEnumerator Drive()
    {
        carController.speedInput = Input.GetAxis("Vertical") * carController.acceleration * 500f;
        return base.Drive();
    }
    public override IEnumerator Break()
    {
        carController.speedInput = Input.GetAxis("Vertical") * carController.backAccel * 500f;
        return base.Drive();
    }
}
