pragma solidity >=0.4.24 <0.6.0;

contract Base1 {

    uint a;
    
    constructor () public {
        a = 1;
    }

}

contract Base2 {

    uint a;

    constructor () public {
        a = 2;
    }

}


contract Derived12 is Base1, Base2 {

    // state vairable `a' is overriden in order

    function test12() public {
        assert (a == 2);
    }

}

contract Derived21 is Base2, Base1 {

    function test21() public {
        assert (a == 1);
    }

}
