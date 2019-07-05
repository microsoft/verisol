pragma solidity >=0.4.24 <0.6.0;

contract StructOfFixedSizeArray {

    struct S {
        uint x;
        uint[2] arr;
    }

    S s;

    function test() public {
        s.x = 10;
        s.arr[0] = 1;
        s.arr[1] = 2;
        
        assert (s.x == 10);
        assert (s.arr.length == 2);
        assert (s.arr[0] == 1);
        assert (s.arr[1] == 2);
    }
}
