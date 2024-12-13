# Test Mars Walker

## Brief Description
This is the solution for the test of handling Mars Robots and making sure they don't fall off the face of the earth.

## Assumptions Made

### Scents have an orientation parameter
 The robot's scent(i.e. if a previous robot has fallen off the same grid coordinates before) is defined by both the coordinates and the direction. This assumption is made to make sure that if a robot has fallen off an edge, other robots won't follow suit in the same way. This creates an effect though on edges(or can be aptly named edge cases), where if a robot falls down one direction, other robots can still fall on the other direction. i.e. if a Robot on `(0,0)` fell down trying to move forward while facing South, then other robots will not move forward facing South but can still fall out of world though, if they move forward facing `West`. 

 The reasoning for this assumption is to save computation. If the same move, in the same coordinates, in the same direction results in being off-world, just skip it. Otherwise, if we are direction-agnostic, we would have to make the mistake, check if we are off world, then undo the mistake. I had a feeling that prevention is better than the cure.

The project is marked as a class library, with tests added.

## Local Development Instructions
The project is marked as a class library, with tests added.

To run the tests, simply run `dotnet test`.
