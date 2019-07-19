pragma solidity >=0.4.24 <0.6.0;

contract ReturnNamedParam {

    constructor () public {}

    function foo() public returns (uint r) {
        return 1;
    }

    function bar() public {
        uint a = foo();
        assert (a == 1);
    }
}
