using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    protected CarController carController;

    public State(CarController _carController)
    {
        carController = _carController;
    }
    public virtual IEnumerator Drive()
    {
        yield break;
    }
    public virtual IEnumerator Break()
    {
        yield break;
    }


}
