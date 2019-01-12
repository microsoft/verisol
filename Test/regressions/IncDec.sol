pragma solidity ^0.4.24;

contract IncDec {

    function test1() public {
        uint a = 10;
        ++a;
        assert (a == 11);
    }
    
    function test2() public {
        uint a = 10;
        a++;
        assert (a == 11);
    }
    
    function test3() public {
        uint a = 10;
        --a;
        assert (a == 9);
    }
    
    function test4() public {
        uint a = 10;
        a--;
        assert (a == 9);
    }
}
