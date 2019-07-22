pragma solidity >=0.4.24 <0.6.0;

contract B {
    function foo() public returns(uint) {
        return 1;
    }
}

contract C is B {
    function foo() public returns(uint) {
        return 2;
    }
    
    function test() public {
        uint a;
        a = B.foo();
        assert (a == 1);
    }
}
