pragma solidity >=0.4.24 <0.6.0;

contract StructOfMapping {

    struct S {
        uint x;
        mapping (uint => uint) m;
    }
    
    S s;
    
    function test() public {
        s.x = 10;
        s.m[0] = 1;
        s.m[1] = 2;
        
        assert (s.x == 10);
        assert (s.m[0] == 1);
        assert (s.m[1] == 2);
    }
}
