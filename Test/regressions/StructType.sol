pragma solidity ^0.4.24;

contract StructType {

    struct S {
        uint x;
        uint y;
    }

    S s;

    function testStructType() public {
        s.x = 1;
        s.y = 2;
        assert (s.x == 1);
        assert (s.y == 2);
    }

}
