pragma solidity >=0.4.24 <0.6.0;

contract InternalRecursiveFunctionCall {

    function sum(uint x) internal returns (uint ret) {
        if (x <= 0) {
            ret = 0;
        } else {
            ret = x + sum(x - 1);
        }
    }

    function fib(uint x) internal returns (uint ret) {
        if (x <= 0) {
            ret = 0;
        } else if (x == 1) {
            ret = 1;
        } else {
            ret = fib(x - 1) + fib(x - 2);
        }
    }

    function testSum() public {
        uint r = sum(5);
        assert (r == 15);
    }

    function testFib() public {
        uint r = fib(6);
        assert (r == 8);
    }

}
