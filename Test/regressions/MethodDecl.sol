pragma solidity ^0.4.24;

contract MethodDecl {

    function test1() public returns (uint);
    
    function MethodDecl() public {
        uint a;
        a = test1();
    }
}
