pragma solidity >=0.4.24 <0.6.0;

contract LoopDoWhile {

    uint[3] a;
    
    function test() public {

        uint i = 0;
        do {
            a[i] = i + 1;
            i += 1;
        } while (i < 3);

        assert (a[0] == 1);
        assert (a[1] == 2);
        assert (a[2] == 3);
    }

}
