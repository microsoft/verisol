pragma solidity >=0.4.24 <0.6.0;


contract A {
}

contract Mapping {

    mapping (uint => uint) m;
    mapping (uint => bool) n;
    mapping (string => int) l;
    mapping (string => bool) ll;
    mapping (string => A) lll;
    mapping (string => address) llll;

    mapping (uint => bytes32) uToBytes32;
    mapping (string => bytes32) sToBytes32;


    constructor () public {
        assert (m[10] == 0);
        assert (!n[4]);
        assert (l["a"] == 0);
        assert (!ll["a"]);
        assert (address(lll["a"]) == address(0x0));
        assert (llll["a"] == address(0x0));

        assert (sToBytes32["a"] == bytes32(0));
        assert (uToBytes32[1] == bytes32(0));

    }
    function test(bytes32 p) public {
        m[10] = 11;
        m[20] = 21;
        assert (m[10] == 11);
        assert (m[20] == 21);
        l["x"] = 1111;
        assert(l["x"] == 1111);
        ll["x"] = true;
        assert(ll["x"]);
        
        sToBytes32["a"] = p;
        assert (sToBytes32["a"] == p);
        assert (sToBytes32["b"] == bytes32(0));

        uToBytes32[3] = p;
        assert (uToBytes32[3] == p);
        assert (uToBytes32[5] == bytes32(0));


    }
}
