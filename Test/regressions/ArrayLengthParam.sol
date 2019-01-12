pragma solidity ^0.4.24;

contract ArrayLength {

    constructor () {}
    
    function test(uint[] a) public {
        assert (a.length == 2);
    }

}

contract B{

    constructor (uint[] b) {        
        require (b.length == 2);        
        ArrayLength al = new ArrayLength();
        al.test(b);
    }
}
