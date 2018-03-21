/****************************************************************************
 * Description:
 *
 * Author: hiramtan@live.com
 ****************************************************************************/

using UnityEngine;
using System.Collections;

public class InputSystems : Feature
{
    public InputSystems(Contexts contexts) : base("input systems")
    {
        Add(new EmitInputSystem(contexts));
        Add(new CreateMoverSystem(contexts));
        Add(new CommandMoveSystem(contexts));
    }
}