pragma solidity >=0.4.24 <0.6.0;

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
