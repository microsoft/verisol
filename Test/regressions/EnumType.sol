pragma solidity >=0.4.24 <0.6.0;

contract EnumType {

    enum Action {GoLeft, GoRight}

    function testEnumType() public {
        Action action = Action.GoRight;
        assert (action == Action.GoRight);
    }

}
