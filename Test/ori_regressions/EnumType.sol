pragma solidity ^0.4.24;

contract EnumType {

    enum Action {GoLeft, GoRight}

    function testEnumType() public {
        Action action = Action.GoRight;
        assert (action == Action.GoRight);
    }

}
