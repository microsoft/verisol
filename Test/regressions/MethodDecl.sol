pragma solidity >=0.4.24 <0.6.0;

contract MethodDecl {

    function test1() public returns (uint);
    
    constructor () public {
        uint a;
        a = test1();
    }
}
