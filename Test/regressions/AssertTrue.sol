pragma solidity >=0.4.24 <0.6.0;

contract AssertTrue {

    function test1() public {
        assert(true);
    }

    function test2() public {
        assert(1 < 2);
    }

}
