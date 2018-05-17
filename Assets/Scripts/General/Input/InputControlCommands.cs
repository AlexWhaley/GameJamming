using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface InputCommandHandler
{
    void HandleBackButton();

    void HandleConfirmButton();

    void HandleUpButton();

    void HandleDownButton();

    void HandleLeftButton();

    void HandleRightButton();

    void RegisterInputHandlers();

    void UnregisterInputHandlers();
}
