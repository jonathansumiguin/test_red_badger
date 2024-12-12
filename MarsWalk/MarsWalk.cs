using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace MarsWalk;


public class KnownEdge {
    public int CoordinateX { get; private set; }
    public int CoordinateY { get; private set; }

    public Orientation Orientation { get; private set; }

    public KnownEdge(int coordinateX, int coordinateY, Orientation orientation) {
        CoordinateX = coordinateX;
        CoordinateY = coordinateY;
        Orientation = orientation;
    }
}

public class MarsOrders
{
    private List<KnownEdge> KnownEdges = new List<KnownEdge>();
    private int LimitX;
    private int LimitY;

    public string ExecuteOrder(string orders) {
        var returnValue = new StringBuilder("");

        // check if order can be parsed correctly, if not 
        if(orders.IndexOf("\n") < 0) {
            throw new Exception("Order is not in the right format");
        }

        var splitOrders = orders.Split('\n', 2);

        var (terrainDimensions, robotOrders) = (splitOrders[0], splitOrders[1]);

        // create the map
        var splitMapCoordinates = terrainDimensions.Split(' ', 2);

        (LimitX, LimitY) = (int.Parse(splitMapCoordinates[0]), int.Parse(splitMapCoordinates[1]));

        //robot orders
        var robotListWithOrders = robotOrders.Split("\n\n");


        foreach(var robotWithOrders in robotListWithOrders) {
            var splitRobotsAndOrders = robotWithOrders.Split('\n', 2);
            var (robotSettings, robotMovement) = (splitRobotsAndOrders[0], splitRobotsAndOrders[1]);

            var splitSettings = robotSettings.Split(' ', 3);

            var robot = new Robot(StringToOrientation(splitSettings[2]), int.Parse(splitSettings[0]), int.Parse(splitSettings[1]));

            foreach(char movement in robotMovement.ToUpper()) {
                if(movement == 'L' || movement == 'R') {
                    robot.Turn(movement == 'L' ? TurnDirection.Left : TurnDirection.Right);
                } else if(movement == 'F') {
                    // check if a previous robot has fallen down doing the move it's trying to do, skip if yes
                    if(KnownEdges.Any(
                        (edge) => edge.CoordinateX == robot.CoordX && 
                            edge.CoordinateY == robot.CoordY &&
                            edge.Orientation == robot.CurrentOrientation
                        )
                    ) {
                        // Robot skips the move, learns from others
                        continue;
                    }

                    // clone the robot before moving
                    // doing a copy of the robot since this should be small in-memory
                    // A more memory efficient way would be to try the move, then commit the move in 2 steps
                    var robotCopy = DeepCloneRobot(robot);

                    // Move the robot now
                    robot.Move();
                    // Check if the robot is still within bounds
                    if (
                        robot.CoordX < 0 ||
                        robot.CoordX > LimitX ||
                        robot.CoordY > LimitY ||
                        robot.CoordY < 0) {
                            // add the edge to the known edges
                            KnownEdges.Add(new KnownEdge(robotCopy.CoordX, robotCopy.CoordY, robotCopy.CurrentOrientation));

                            //add the robot's last known coordinates to the output
                            returnValue.AppendLine($"{robotCopy.CoordX} {robotCopy.CoordY} {robotCopy.CurrentOrientation} LOST");
                            break;
                        }

                }
            }

            // Log the survival of the robot
            returnValue.AppendLine($"{robot.CoordX} {robot.CoordY} {robot.CurrentOrientation}");
            
        }

        return returnValue.ToString();
    }

    private Robot DeepCloneRobot(Robot robot) {
        var robotCopy = JsonSerializer.Deserialize<Robot>(JsonSerializer.Serialize(robot));

        if(robotCopy == null) {
            throw new Exception("Robot copying failed");
        }

        return robotCopy;
    }

    private Orientation StringToOrientation(string orientation) {
        switch(orientation.ToUpper()) {
            case "N":
                return Orientation.North;
            case "W":
                return Orientation.West;
            case "E":
                return Orientation.East;
            case "S":
                return Orientation.South;
            default:
                throw new Exception("Orientation not valid.");
        }
    }
}
