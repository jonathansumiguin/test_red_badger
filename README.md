# Test Mars Walker

## Brief Description
This is the solution for the test of handling Mars Robots and making sure they learn from others mistakes. They're quite expensive, so we want as many of them to survive.

## Assumptions Made

### Scents have an orientation parameter
 The robot's scent(i.e. if a previous robot has fallen off the same grid coordinates before) is defined by both the coordinates and the direction. This assumption is made to make sure that if a robot has fallen off an edge, other robots won't follow suit in the same way. This creates an effect though on edges(or can be aptly named edge cases), where if a robot falls down one direction, other robots can still fall on the other direction. i.e. if a Robot on `(0,0)` fell down trying to move forward while facing South, then other robots will not move forward facing South but can still fall out of world though, if they move forward facing `West`. 

 The reasoning for this assumption is to save computation. If the same move, in the same coordinates, in the same direction results in being off-world, just skip it. Otherwise, if we are direction-agnostic, we would have to make the mistake, check if we are off world, then undo the mistake. I had a feeling that in this instance, prevention is better than the cure.

The project is marked as a class library, with tests added and ran on GH actions.

## Tech choices
Most of the tech I've written is in plain C#, and not much complexity has been added. I was thinking of making Interfaces for robot(which I was aptly thinking of naming IRobot), but I felt like it was overkill at the moment due to the fact that I'm not sure what the other inheritors will look like and what properties they share with the `Robot` class. I made `Robot` a class though, to make sure we can add more commands to it if the need arises.

At first, I was thinking of making a class named `MarsTerrain` with the dimensions and the known edges in it, but it wasn't being reused or passed over so I felt like we could still get away with it. If it held any other important thing though, we could make a case for the need for it(i.e. if the robots don't know what's the absolute coordinate limits, and are allowed to "map" it for other robots).

On the `Robot` class, I've made both `Turn` and `Move` items not receive any parameters deliberately, this is due to the fact that if we want to extend a different `IMovable` object, we wouldn't have a hard time making this work with another item. Also, I feel like the `Robot` should be flexible and agnostic enough and to hopefully perform the same way, just in case they needed to be sent to other planets.

## Local Development Instructions
The project is marked as a class library, with tests added. No special packages have been added, so we will probably not need to restore.

To run the build, simply run `dotnet build`.

To run the tests, simply run `dotnet test`.
