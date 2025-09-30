using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    #region Movement Inputs
    public Vector2 MoveInput { get; private set; }
    public Vector2 CameraInput { get; private set; }

    public bool JumpInput { get; private set; }
    public bool SprintInput { get; private set; }
    public bool CrouchInput { get; private set; }

    public void ReadMovementInputs()
    {
        MoveInput = new(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        CameraInput = new(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        JumpInput = Input.GetKeyDown(KeyCode.Space);
        SprintInput = Input.GetKey(KeyCode.LeftShift);
        CrouchInput = Input.GetKey(KeyCode.LeftControl);
    }
    #endregion

    #region Interaction Inputs
    public bool LeftMouseDown { get; private set; }
    public bool RightMouseDown { get; private set; }
    public bool InteractButton { get; private set; }
    public bool PickUpButton { get; private set; }
    public bool MouseScrollDown { get; private set; }
    public bool MouseScrollUp { get; private set; }
    public bool LeftMouse {  get; private set; }
    public bool RightMouse { get; private set; }

    public void ReadInteractionInputs()
    {
        LeftMouseDown = Input.GetMouseButtonDown(0);
        RightMouseDown = Input.GetMouseButtonDown(1);
        PickUpButton = Input.GetKeyDown(KeyCode.E);
        MouseScrollDown = Input.mouseScrollDelta.y < 0;
        MouseScrollUp = Input.mouseScrollDelta.y > 0;
        InteractButton = Input.GetKeyDown(KeyCode.C);
        LeftMouse = Input.GetMouseButton(0);
        RightMouse = Input.GetMouseButton(1);
    }
    #endregion

    #region Dialogue Inputs
    public bool SkipDialogue { get; private set; }

    public void ReadDialogueInputs()
    {
        SkipDialogue = Input.GetKeyDown(KeyCode.F);
    }
    #endregion
}
