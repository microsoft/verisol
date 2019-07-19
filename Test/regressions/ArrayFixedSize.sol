pragma solidity >=0.4.24 <0.6.0;

contract ArrayFixedSize {

    uint[2] a;

    constructor() public {}

    function test() public {
        a[0] = 1;
        a[1] = 2;
        assert(a[0] == 1);
        assert(a[1] == 2);
    }

}
