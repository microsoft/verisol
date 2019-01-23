pragma solidity ^0.4.24;

contract ReturnNamedParam {

    function ReturnNamedParam() public {}

    function foo() public returns (uint r) {
        return 1;
    }

    function bar() public {
        uint a = foo();
        assert (a == 1);
    }
}
