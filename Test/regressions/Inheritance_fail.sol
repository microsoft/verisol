pragma solidity >=0.4.24 <0.6.0;

contract BaseContract {

    uint a;
    
    constructor () public {
        a = 1;
    }

}

contract DerivedContract is BaseContract {

    uint b;

    constructor () public {
        b = 2;
    }

    function test() public {
        assert (a == 1);
        assert (b != 2);
    }

}
