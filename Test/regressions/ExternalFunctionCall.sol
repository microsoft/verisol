pragma solidity >=0.4.24 <0.6.0;

contract ExternalFunctionCall {

    function foo(uint x) public returns (uint ret) {
        ret = x + 2;
    }

    function testExternalFunctionCall(uint x) public {
        uint y = this.foo(x);
        assert (y == x + 2);
    }

}
