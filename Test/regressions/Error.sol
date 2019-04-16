pragma solidity ^0.4.24;

contract AssertFalse {

    // negative regression. 
    function test1() public {
        assert (false);
    }

}
