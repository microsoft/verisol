pragma solidity >=0.4.24 <0.6.0;

contract LoopWhile {

    uint[2] a;

    function testBoundedWhileLoop() public {
        uint i = 0;
        while (i < 2) {
            a[i] = i;
            i += 1;
        }
        assert (a[0] == 0);
        assert (a[1] == 1);
    }

    uint[10] b;

    function testUnboundedWhileLoop(uint n) public {
        require (n > 0 && n < 10);

        uint i = 0;
        while (i < n) {
            b[i] = i;
            i += 1;
        }

        uint j = 0;
        while (j < n) {
            assert (b[j] == j);
            j += 1;
        }
    }

}
