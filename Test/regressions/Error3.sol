pragma solidity >=0.4.24 <0.6.0;

contract AssertFalse {

    // negative regression. 
    function test1() public pure  {
        assert (false);
    }

}
