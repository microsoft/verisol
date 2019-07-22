pragma solidity >=0.4.24 <0.6.0;

contract EnumParam {

    enum Action {GoLeft, GoRight}
    Action action;

    function foo(EnumParam.Action a) public {
        action = a;
    }

    function bar(Action a) public {
        action = a;
    }

    function test() public {
        foo(Action.GoRight);
        assert (action == Action.GoRight);
        bar(Action.GoLeft);
        assert (action == Action.GoLeft);
    }

}
