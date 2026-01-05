using UnityEngine;
using PhikozzLibrary.Runtime.Command;

/// <summary>
/// 예시 ConcreteCommand 클래스입니다.
/// </summary>
public class SampleConcreteCommand : ICommand
{
    private Transform _target;
    private Vector3 _direction;
    private float _distance;
    private Vector3 _previousPosition;

    public SampleConcreteCommand(Transform target, Vector3 direction, float distance)
    {
        _target = target;
        _direction = direction.normalized;
        _distance = distance;
    }

    public void Execute()
    {
        _previousPosition = _target.position;
        _target.position += _direction * _distance;
    }

    public void Undo()
    {
        _target.position = _previousPosition;
    }
}
