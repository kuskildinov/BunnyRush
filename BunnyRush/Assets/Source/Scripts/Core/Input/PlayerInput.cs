using UnityEngine;

public class PlayerInput : IInput
{ 
    public bool Jump()
    {
       if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            return true;
        }

        return false;
    }
}
