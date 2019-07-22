pragma solidity >=0.4.24 <0.6.0;

contract EmptyContract {

    uint a;

    constructor () public {
        a = 1;
    }

    function test() public {
        assert (a == 1);
    }
}


contract DigiPulseWrapper is EmptyContract {
}

