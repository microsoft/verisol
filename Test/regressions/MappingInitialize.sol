pragma solidity ^0.4.24;

contract Mapping {

    mapping (uint => uint) m;
    mapping (uint => bool) n;
    mapping (string => int) l;
    mapping (string => bool) ll;

    function Mapping() {
        assert (m[10] == 0);
        assert (!n[4]);
        assert (l["a"] == 0);
        assert (!ll["a"]);
    }
    function test() public {
        m[10] = 11;
        m[20] = 21;
        assert (m[10] == 11);
        assert (m[20] == 21);
        l["x"] = 1111;  
        assert(l["x"] == 1111);
        ll["x"] = true;  
        assert(ll["x"]);
    }
}
