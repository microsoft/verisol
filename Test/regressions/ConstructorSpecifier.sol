pragma solidity >=0.4.24 <0.6.0;

contract A {
    uint public a;
    
    constructor (uint _a) public {
        a = _a;
    }
}

contract B is A(1) {
    
    function foo() public {
        assert(a == 1);
    }
}
