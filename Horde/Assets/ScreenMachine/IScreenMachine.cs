using System.Collections;
using System.Collections.Generic;
using ScreenMachine;
using UnityEngine;

public interface IScreenMachine
{
    void PresentState(IStateBase state);

    void PushState(IStateBase state);

    void PopState();
}
