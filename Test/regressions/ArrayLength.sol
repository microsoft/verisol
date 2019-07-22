pragma solidity >=0.4.24 <0.6.0;

contract ArrayLength {

    uint[3][2] a;

    constructor () {}
    
    function test() public {
        assert (a.length == 2);
        assert (a[0].length == 3);
        
    }
}
