namespace tests;

using System.Diagnostics.CodeAnalysis;
using MarsWalk;

//Create a fixture 
public class TestMarsWalk
{
    MarsWalk MarsWalk;
    public TestMarsWalk() {
        MarsWalk = new MarsWalk();
    }
    [Fact]
    public void Test_Mars_Walk_Default()
    {
        var output = MarsWalk.ExecuteOrder(
            @"5 3
1 1 E
RFRFRFRF
3 2 N
FRRFLLFFRRFLL
0 3 W
LLFFFLFLFL"
        );

        Assert.Equal(
            @"1 1 E
3 3 N LOST
2 3 S"
            ,output);
    }
}
