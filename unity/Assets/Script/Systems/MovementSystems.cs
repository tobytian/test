/****************************************************************************
 * Description:
 *
 * Author: hiramtan@live.com
 ****************************************************************************/
using UnityEngine;
using System.Collections;

public class MovementSystems : Feature
{
    public MovementSystems(Contexts contexts) : base("movement systems")
    {
        Add(new MoveSystem(contexts));
    }
}
