pragma solidity ^0.4.24;

contract C {

    constructor () {}
    function foo(uint x) public returns (uint ret) {
        ret = x + 1;
    }

}

contract CreateContract {


    function CreateContract() {}
    function testCreateContract() public {
        C c = new C();
        uint r = c.foo(1);
        assert (r == 2);
    }

}
