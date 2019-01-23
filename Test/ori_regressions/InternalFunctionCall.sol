pragma solidity ^0.4.24;

contract InternalFunctionCall {

    function foo(uint x) internal returns (uint ret) {
        ret = x + 1;
    }

    function testInternalFunctionCall(uint x) public {
        uint y = foo(x);
        assert (y == x + 1);
    }

}
