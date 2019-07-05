pragma solidity >=0.4.24 <0.6.0;

contract LoopFor {

    uint[2] a;

    function testBoundedForLoop() public {
        for (uint i = 0; i < 2; i += 1) {
            a[i] = i;
        }
        assert (a[0] == 0);
        assert (a[1] == 1);
    }

    uint[10] b;
    
    function testUnboundedForLoop(uint n) public {
        require (n > 0 && n < 10);
        for (uint i = 0; i < n; i += 1) {
            b[i] = i;
        }
        for (uint j = 0; j < n; j += 1) {
            assert (b[j] == j);
        }
    }

}
