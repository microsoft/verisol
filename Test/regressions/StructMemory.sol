pragma solidity >=0.4.24 <0.6.0;

//Simplest struct with an array 
//this version does not use any temporaries

contract StructType {

    struct S {
        uint x;
        uint y;
        string z;
    }

    S s;
    S[] ss;

    function testStructType() public {
        s.x = 1;
        s.y = 2;
        assert (s.x == 1);
        assert (s.y == 2);
        S memory t = s;
        t.x = 333;
        assert (s.x == 1);
    }
}
