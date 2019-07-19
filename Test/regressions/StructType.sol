pragma solidity >=0.4.24 <0.6.0;

//Simplest struct with an array 
//no nested a[i] expressions

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
       S memory t = S(1,2,"aa"); //cannot handle nested calls yet
       ss.push(t);
       S storage u = ss[0];
       assert(u.x == 1);
       u.x = 2;
       assert(u.x == 2);
    }
}
