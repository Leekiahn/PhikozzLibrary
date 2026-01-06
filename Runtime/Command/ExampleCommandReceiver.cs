using UnityEngine;
using PhikozzLibrary.Runtime.Command;

/// <summary>
/// Receiver 역할을 하는 예시 MonoBehaviour 클래스입니다.
/// </summary>
public class ExampleCommandReceiver : MonoBehaviour
{
    public Transform player;
    CommandInvoker invoker;

    void Start()
    {
        invoker = new CommandInvoker();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            invoker.ExecuteCommand(new ExampleConcreteCommand(player, Vector3.right, 1f));
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            invoker.ExecuteCommand(new ExampleConcreteCommand(player, Vector3.left, 1f));
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            invoker.UndoCommand();
        }
    }
}
