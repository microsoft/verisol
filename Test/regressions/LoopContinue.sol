pragma solidity >=0.4.24 <0.6.0;

contract LoopContinue {

    uint[3] a;

    function testLoopBreak() public {
        for (uint j = 0; j < 3; j += 1) {
            a[j] = 1;
        }
        for (uint i = 0; i < 3; i += 1) {
            if (i == 1) continue;
            a[i] = 10;
        }
        assert (a[0] == 10);
        assert (a[1] == 1);
        assert (a[2] == 10);
    }

    // NOTE: the number of columns comes first in the declaration!
    uint[3][2] b;

    function testNestedLoopBreak() public {
        for (uint x = 0; x < 2; x += 1) {
            for (uint y = 0; y < 3; y += 1) {
                b[x][y] = 1;
            }
        }
        for (uint i = 0; i < 2; i += 1) {
            for (uint j = 0; j < 3; j += 1) {
                if (j == 1) continue;
                b[i][j] = 10;
            }
        }
        assert (b[0][0] == 10);
        assert (b[0][1] == 1);
        assert (b[0][2] == 10);
        assert (b[1][0] == 10);
        assert (b[1][1] == 1);
        assert (b[1][2] == 10);
    }

}
