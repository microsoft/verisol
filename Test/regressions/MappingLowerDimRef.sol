pragma solidity >=0.5.0 <0.6.0;
// this example does not work wiht 0.4.24 as storage
// TypeError: Storage location can only be given for array or struct types.
//        mapping (uint => uint) storage y = x[10]; 

contract MappingLowerDimRef {

    mapping (uint => mapping (uint => uint)) x;

    function test() public {
        x[10][20] = 100;
        x[10][21] = 200;
        mapping (uint => uint) storage y = x[10];
        y[20] = 150;
        assert (y[20] == 150);
        assert (y[21] == 200);
        assert (x[10][20] == 150);
        assert (x[10][21] == 200);
    }

}
