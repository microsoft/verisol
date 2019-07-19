pragma solidity >=0.4.24 <0.6.0;

contract LoopNestedWhile {

    uint[2][2] a;

    function testNestedWhileLoop() public {
        uint i = 0;
        while (i < 2) {
            uint j = 0;
            while (j < 2) {
                a[i][j] = i * 2 + j;
                j += 1;
            }
            i += 1;
        }
        assert (a[0][0] == 0);
        assert (a[0][1] == 1);
        assert (a[1][0] == 2);
        assert (a[1][1] == 3);
    }

}
