pragma solidity ^0.4.24;

contract FieldAccess {

    uint field;

    function testLoad() public returns (uint r) {
        r = field;
        assert (r == field);
    }

    function testStore(uint a) public {
        field = a;
        assert (field == a);
    }

}
