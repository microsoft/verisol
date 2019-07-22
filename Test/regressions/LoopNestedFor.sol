pragma solidity >=0.4.24 <0.6.0;

contract LoopNestedFor {

    uint[2][2] a;
    
    function testNestedForLoop() public {
        for (uint i = 0; i < 2; i += 1) {
            for (uint j = 0; j < 2; j += 1) {
                a[i][j] = 2 * i + j;
            }
        }
        assert (a[0][0] == 0);
        assert (a[0][1] == 1);
        assert (a[1][0] == 2);
        assert (a[1][1] == 3);
    }

}
