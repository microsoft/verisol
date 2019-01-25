pragma solidity ^0.4.24;

contract A {

   string t;

   function test() public view {        
        string storage s = t;
        bytes32 sb = keccak256(bytes(s));
        bytes32 st = keccak256(bytes(t));
 
        assert (sb == st);

    }

}
