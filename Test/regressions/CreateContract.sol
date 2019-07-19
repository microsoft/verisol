pragma solidity >=0.4.24 <0.6.0;

contract C {

    constructor () public {}
    function foo(uint x) public returns (uint ret) {
        ret = x + 1;
    }

}

contract CreateContract {


    constructor () public {}
    function testCreateContract() public {
        C c = new C();
        uint r = c.foo(1);
        assert (r == 2);
    }

}
