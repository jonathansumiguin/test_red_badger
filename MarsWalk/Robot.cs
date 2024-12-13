using System.ComponentModel;

namespace MarsWalk;

public enum Orientation {
    North,
    East,
    South,
    West,
}

public enum TurnDirection {
    Left = 'L',
    Right = 'R'
}

public class Robot {
    public Orientation CurrentOrientation { get; private set; }

    // made these private so the robot doesn't teleport
    public int CoordX { get; private set; }

    public int CoordY { get; private set; }

    public Robot(Orientation currentOrientation, int coordX, int coordY) {
        CurrentOrientation = currentOrientation;
        CoordX = coordX;
        CoordY = coordY;
    }

    public void Move() {
        switch(CurrentOrientation) {
            case Orientation.North:
                CoordY++;
                break;
            case Orientation.South:
                CoordY--;
                break;
            case Orientation.East:
                CoordX++;
                break;
            case Orientation.West:
                CoordX--;
                break;
            default:
                throw new Exception("The robot is now confused");
        }
    }

    public void Turn(TurnDirection turnDirection) 
    {
        var currentOrientationIndex = (int)CurrentOrientation;
        var changer = turnDirection == TurnDirection.Right ? 1 : -1;
        var noOfEnumValues = Enum.GetValues(typeof(Orientation)).Length;

        // this will ensure that the orientation is going to the end of the array if turning counter clockwise
        var newOrientationIndex = 
            (currentOrientationIndex == 0 && turnDirection == TurnDirection.Left) ?
                (noOfEnumValues - 1) : currentOrientationIndex + changer;

        CurrentOrientation = (Orientation)(newOrientationIndex % noOfEnumValues);
    }

}