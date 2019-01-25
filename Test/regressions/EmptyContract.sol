pragma solidity ^0.4.24;

contract EmptyContract {

    uint a;

    function EmptyContract() public {
        a = 1;
    }

    function test() public {
        assert (a == 1);
    }
}


contract DigiPulseWrapper is EmptyContract {
}

