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
    }

    function testStructConstructor() public {
        s = S(22, 33, "abc");
        assert (s.x == 22);
        assert (s.y == 33);
    }

   constructor() public {
       ss.push(S(1,2,"aa"));
       assert(ss[0].x == 1);
       S memory u = ss[0];
       u.x = 2;
       assert(ss[0].x == 1);
    }
}
