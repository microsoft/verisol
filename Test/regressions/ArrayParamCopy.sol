pragma solidity >=0.4.24 <0.6.0;

// array variable copy breaks

contract ArrayLength {
    uint[12] a;

    constructor (uint[12] memory d) public
    {
        require (d[1] == 5);
        a = d;       
        assert (d[1] == 5);
    }

}
