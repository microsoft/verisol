pragma solidity ^0.4.24;

contract A {
    uint public a;
    
    function A(uint _a) public {
        a = _a;
    }
}

contract B is A(1) {
    
    function foo() public {
        assert(a == 1);
    }
}
