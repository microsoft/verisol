pragma solidity ^0.4.24;

contract MappingLowerDimRef {

    mapping (uint => mapping (uint => uint)) x;

    function test() public {
        x[10][20] = 100;
        x[10][21] = 200;
        mapping (uint => uint) y = x[10];
        y[20] = 150;
        assert (y[20] == 150);
        assert (y[21] == 200);
        assert (x[10][20] == 150);
        assert (x[10][21] == 200);
    }

}
