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

    [Fact]
    public void Test_Learn_From_Others()
    {
        var output = MarsWalk.ExecuteOrder(
            @"3 3
1 1 E
FFFLRLRLR
1 1 E
FFFLRLRLR
1 1 E
FFLFRF
1 1 E
FFLFRFLLLL"
        );

        Assert.Equal(
            @"3 1 E LOST
3 1 E
3 2 E LOST
3 2 E"
            ,output);
    }

    [Fact]
    public void Test_Execute_Order_66() //SPOILER ALERT: EVERYONE DIES
    {
        var output = MarsWalk.ExecuteOrder(
            @"5 5
1 1 N
FFFFLFF
2 5 N
RFRFFRFFLFFFRFF
1 1 E
FLFRFLFRFLFRFLFRF
3 3 E
FLFLFFFFRFRFRFLFFFRFLFRFRFLFLFRFRFRFLFLFRFRFLFLFRFLF"
        );

        Assert.Equal(
            @"0 5 W LOST
0 0 W LOST
5 5 E LOST
0 0 S LOST"
            ,output);
    }
}

